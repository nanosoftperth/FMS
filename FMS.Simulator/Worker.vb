Imports FMS.Business
Imports System.Net

Public Class Worker

    Public Shared Sub DoWork()

        Console.WriteLine(vbNewLine & "FLEET MANAGEMENT ""DEMO"" SIMULATOR " & vbNewLine & vbNewLine & vbNewLine & vbNewLine)

        Dim settings As List(Of DataObjects.SimulatorSetting) = DataObjects.SimulatorSetting.GetAll

        Dim ts As TimeSpan = TimeSpan.FromSeconds(2)

        Dim timeiterators As New List(Of TimeIterator)


        For Each Setting As DataObjects.SimulatorSetting In settings

            Dim ti As New TimeIterator With {.settingObj = Setting}

            Dim sourceDevice As DataObjects.Device = DataObjects.Device.GetAllDevices.Where( _
                                                            Function(x) x.DeviceID = ti.settingObj.SourceDeviceID).Single

            Dim st As DateTime = ti.settingObj.StartTime
            Dim et As DateTime = ti.settingObj.EndTime

            While st < et

                Dim ll As New LatLongDate

                ll.datetime = st

                With sourceDevice.GetLatLongs(ll.datetime)
                    ll.latitude = .Key
                    ll.longitude = .Value
                End With

                ti.latlongs.Add(ll)

                st = st + ts

            End While

            timeiterators.Add(ti)

        Next


        Dim baseURL As String = "http://ppjs.nanosoft.com.au:9000/api/dataaccess?truckid={0}&lat={1}&lng={2}&time={3}"

        While True

            For Each x In timeiterators

                'using(WebClient client = new WebClient()) {
                'string s = client.DownloadString(url);
                '}

                Using client As New WebClient

                    Dim lld As LatLongDate = x.GetNext

                    Dim url As String = String.Format(baseURL, x.settingObj.DestinationDeviceID, lld.latitude, lld.longitude, Now.ToString("dd/MMM/yyyy HH:mm:ss"))

                    Dim s As String = client.DownloadString(url)
                End Using


            Next

            Threading.Thread.Sleep(ts)
        End While

    End Sub


End Class


''' <summary>
''' Starts at the beginning of the list and iterates through to the top 
''' THEN, goes from the top to the bottom again, repeates indefinitley
''' </summary>
Public Class TimeIterator

    Public Property settingObj As DataObjects.SimulatorSetting

    Public Property latlongs As New List(Of LatLongDate)

    ''' <summary>
    ''' The direction which the list iterates, this should be set to either +1 or -1
    ''' </summary>
    Public Property direction As Integer = 1
    Public Property currentIndex As Integer = 0

    Public Function GetNext() As LatLongDate

        Dim maxIndex As Integer = latlongs.Count - 1

        If currentIndex = 0 Then direction = 1
        If currentIndex = maxIndex Then direction = -1

        currentIndex = currentIndex + direction

        'get the  next index
        Return latlongs(currentIndex)

    End Function

End Class


Public Class LatLongDate

    Public Property latitude As Decimal
    Public Property longitude As Decimal
    Public Property datetime As DateTime

    Public Sub New()

    End Sub

End Class


