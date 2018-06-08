Imports System.Drawing
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
        Dim dId As String = DeviceID.Value.ToString()
        Dim sDate As Date = StartDate.Value
        Dim eDate As Date = EndDate.Value
        Dim sTime As String = IIf(StartTime.Value.ToString().Equals(""), "1", StartTime.Value.ToString().Split(":")(0))
        Dim eTime As String = IIf(EndTime.Value.ToString().Equals(""), "24", EndTime.Value.ToString().Split(":")(0))
        Dim dt1 As Date = sDate.AddHours(sTime)
        Dim dt2 As Date = eDate.AddHours(eTime)

        'Dim dt1 As Date = "6/11/2017 11:00:00 AM"
        'Dim dt2 As Date = "6/11/2017 01:00:00 PM"
        'Dim dt2 As Date = "6/11/2017 11:13:42 AM"
        'Dim dt2 As Date = "6/11/2017 11:11:47 AM"

        'Dim x = FMS.Business.DataObjects.VehicleLocation.IsUserSeeAllVehicle()

        Dim lstLatLong = FMS.Business.DataObjects.DataLoggerReport.GetLatLongLog(dId, dt1, dt2)
        Dim intMaxMark As Integer = 20
        Dim intCounted As Integer
        intCounted = Math.Abs((lstLatLong.Count / intMaxMark))

        Dim xMarker As String() = New String(intMaxMark) {}
        Dim intCounter As Integer = 0
        Dim intCount As Integer = intCounted
        Dim ListCount As Integer = lstLatLong.Count
        Dim countMark As Integer = 0
        For Each latLong In lstLatLong
            If intCount.Equals(intCounted) Or ListCount <= intMaxMark Then
                If intCounter <= intMaxMark Then
                    xMarker(intCounter) = "&markers=size:tiny%7Ccolor:red%7Clabel:S%7C" & latLong.Latitude.ToString() & "," & latLong.Longitude.ToString()
                    intCounter += 1
                    intCount = 0
                End If
            End If
            intCount += 1
            countMark += 1
        Next
        Dim strMarker As String = String.Join("", xMarker)
        Dim url As String = "https://maps.googleapis.com/maps/api/staticmap?center=-31.9538987,115.85823189999996&zoom=12&size=720x350" & strMarker
        If lstLatLong.Count > 0 Then
            url = "https://maps.googleapis.com/maps/api/staticmap?center=" & lstLatLong(0).Latitude & "," & lstLatLong(0).Longitude & "&zoom=12&size=720x350" & strMarker
        End If

        Using wc As New WebClient()
            Dim bc() As Byte = wc.DownloadData(url)
            Dim bmp As Bitmap = Nothing
            Using stream As Stream = New MemoryStream(bc)
                stream.Seek(0, SeekOrigin.Begin)
                bmp = TryCast(Bitmap.FromStream(stream), Bitmap)
            End Using
            XrPictureBox1.Image = bmp
        End Using
    End Sub
End Class