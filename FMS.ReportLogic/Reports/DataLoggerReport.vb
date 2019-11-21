Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports OSIsoft.AF

Public Class DataLoggerReport
    Inherits DevExpress.XtraReports.UI.XtraReport

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub XtraReport1_DataSourceDemanded(ByVal sender As Object, ByVal e As EventArgs) Handles Me.DataSourceDemanded


        'lblCompanyName.Text = FMS.Business.ThisSession.ApplicationName

        Dim mkr As Boolean = Marker.Value
        Dim dId As String = DeviceID.Value.ToString()
        Dim sDate As Date = StartDate.Value
        Dim eDate As Date = EndDate.Value
        Dim sTime As String = IIf(StartTime.Value.ToString().Equals(""), "1", StartTime.Value.ToString().Split(":")(0))
        Dim eTime As String = IIf(EndTime.Value.ToString().Equals(""), "24", EndTime.Value.ToString().Split(":")(0))
        Dim dt1 As Date = sDate.AddHours(sTime)
        Dim dt2 As Date = eDate.AddHours(eTime)

        'Dim dId As String = "auto19"
        'Dim dt1 As Date = "06/06/2017 11:00:00 AM"
        'Dim dt2 As Date = "15/06/2017 12:00:00 PM"
        'Dim dt2 As Date = "6/11/2017 11:13:42 AM"
        'Dim dt2 As Date = "6/11/2017 11:11:47 AM"

        Dim lstLatLong = FMS.Business.DataObjects.DataLoggerReport.GetLatLongLog(dId, dt1, dt2)
        Dim xMarker As New List(Of String)
        Dim lstPath As New List(Of String)
        If lstLatLong IsNot Nothing Then
            Dim intMaxMark As Integer = 20
            Dim intCounted As Integer
            intCounted = Math.Abs((lstLatLong.Count / intMaxMark))

            Dim intCounter As Integer = 0
            Dim intCount As Integer = intCounted
            Dim ListCount As Integer = lstLatLong.Count
            Dim countMark As Integer = 0
            For Each latLong In lstLatLong
                If intCount.Equals(intCounted) Or ListCount <= intMaxMark Then
                    If intCounter <= intMaxMark Then
                        xMarker.Add("&markers=size:tiny%7Ccolor:red%7Clabel:S%7C" & latLong.Latitude.ToString() & "," & latLong.Longitude.ToString())
                        lstPath.Add(latLong.Latitude.ToString() & "," & latLong.Longitude.ToString())
                        intCounter += 1
                        intCount = 0
                    End If
                End If
                intCount += 1
                countMark += 1
            Next
        End If

        Dim strMarker As String = String.Join("", xMarker)
        Dim strPat As String = String.Join("|", lstPath)

        strPat = IIf(lstPath.Count > 0, String.Format("&path=color:0x0000ff|weight:5|{0}", strPat), String.Empty)

        Dim url = String.Format("https://maps.googleapis.com/maps/api/staticmap?size=720x350&maptype=hybrid{0}{1}&key=AIzaSyA2FG3uZ6Pnj8ANsyVaTwnPOCZe4r6jd0g", strMarker, strPat)

        'If lstLatLong IsNot Nothing AndAlso lstLatLong.Count > 0 Then
        '    If mkr Then
        '        'url = "https://maps.googleapis.com/maps/api/staticmap?center=" & lstLatLong(0).Latitude & "," & lstLatLong(0).Longitude & "&zoom=7&size=720x350" & strMarker
        '        url = "https://maps.googleapis.com/maps/api/staticmap?size=720x350" & strMarker & " &key=AIzaSyA2FG3uZ6Pnj8ANsyVaTwnPOCZe4r6jd0g"
        '    Else
        '        url = "http://maps.googleapis.com/maps/api/staticmap?size=720x350&path=color:0x0000ff|weight:5|" & strPat & "&sensor=false &key=AIzaSyA2FG3uZ6Pnj8ANsyVaTwnPOCZe4r6jd0g"
        '    End If
        'End If

        ''https://maps.googleapis.com/maps/api/staticmap?maptype=hybrid&size=720x350&path=size:tiny%7Ccolor:red%7Clabel:S%7C-20.7588343333333,116.761815333333&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-20.7590311666667,116.761771833333&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-20.7592311666667,116.7617715&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-20.7590368333333,116.761749166667&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-20.7588345,116.761721&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-20.758652,116.761705666667&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-20.758463,116.761700833333&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-20.7586705,116.7617145&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-20.7588645,116.761725333333&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-20.7590428333333,116.761773833333&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-20.7592345,116.761791333333&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-20.759764,116.762081666667&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-20.7599613333333,116.762085666667&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-20.7601441666667,116.762103666667&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-20.7603251666667,116.762121&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-20.7601403333333,116.7621045&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-20.7599431666667,116.762085833333&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-20.7597546666667,116.762066&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-20.7595625,116.762045333333&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-20.7597438333333,116.762107666667&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-20.759625,116.762039833333%20&key=AIzaSyA2FG3uZ6Pnj8ANsyVaTwnPOCZe4r6jd0g&path=color:0xff0000ff|weight:5|-20.7588343333333,116.761815333333|-20.7590311666667,116.761771833333|-20.7592311666667,116.7617715|-20.7590368333333,116.761749166667|-20.7588345,116.761721|-20.758652,116.761705666667|-20.758463,116.761700833333|-20.7586705,116.7617145|-20.7588645,116.761725333333|-20.7590428333333,116.761773833333|-20.7592345,116.761791333333|-20.759764,116.762081666667|-20.7599613333333,116.762085666667|-20.7601441666667,116.762103666667|-20.7603251666667,116.762121|-20.7601403333333,116.7621045|-20.7599431666667,116.762085833333|-20.7597546666667,116.762066|-20.7595625,116.762045333333|-20.7597438333333,116.762107666667|-20.759625,116.762039833333


        Dim bc() As Byte = Nothing
        Dim bmp As Bitmap
        Try
            Using wc As New WebClient()
                bc = wc.DownloadData(url)
                Using stream As Stream = New MemoryStream(bc)
                    stream.Seek(0, SeekOrigin.Begin)
                    bmp = TryCast(Bitmap.FromStream(stream), Bitmap)
                End Using
            End Using
        Catch ex As Exception
            Using wc As New WebClient()
                bc = wc.DownloadData("https://maps.googleapis.com/maps/api/staticmap?center=-31.9538987,115.85823189999996&zoom=12&size=720x350&key=AIzaSyA2FG3uZ6Pnj8ANsyVaTwnPOCZe4r6jd0g")
                Using stream As Stream = New MemoryStream(bc)
                    stream.Seek(0, SeekOrigin.Begin)
                    bmp = TryCast(Bitmap.FromStream(stream), Bitmap)
                End Using
            End Using
        End Try
        XrPictureBox1.Image = bmp
        'imgCompanyLogo.Image = ObjectDataSource1.
    End Sub

    Private Sub DataLoggerReport_BeforePrint(sender As Object, e As PrintEventArgs) Handles Me.BeforePrint



    End Sub
End Class