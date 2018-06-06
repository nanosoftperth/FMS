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
        'Dim url As String = "https://maps.googleapis.com/maps/api/staticmap?size=512x512&zoom=12&amp;format=png&center=-31.9538987,115.85823189999996&zoom=12&size=700x700&maptype=terrain"
        'Dim url As String = "https://maps.googleapis.com/maps/api/staticmap?center=63.259591,-144.667969&zoom=6&size=400x400&markers=color:blue%7Clabel:S%7C62.107733,-145.541936&markers=size:tiny%7Ccolor:green%7CDelta+Junction,AK&markers=size:mid%7Ccolor:0xFFFF00%7Clabel:C%7CTok,AK"
        'key=AIzaSyA2FG3uZ6Pnj8ANsyVaTwnPOCZe4r6jd0g
        'Dim url As String = "https://maps.googleapis.com/maps/api/staticmap?center=63.259591,-144.667969&zoom=6&size=400x400&maptype=terrain"


        Dim dt1 As Date = "6/11/2017 11:00:00 AM"
        'Dim dt2 As Date = "6/11/2017 01:00:00 PM"
        Dim dt2 As Date = "6/11/2017 11:13:42 AM"
        'Dim dt2 As Date = "6/11/2017 11:11:47 AM"
        Dim lstLatLong = FMS.Business.DataObjects.DataLoggerReport.GetLatLongLog("auto19", dt1, dt2)
        Dim intMaxMark As Integer = 20
        Dim intCounted As Integer
        intCounted = Math.Abs((lstLatLong.Count / intMaxMark))

        Dim xMarker As String() = New String(intMaxMark) {}
        Dim intCounter As Integer = 0
        Dim intCount As Integer = intCounted
        Dim ListCount As Integer = lstLatLong.Count
        For Each latLong In lstLatLong
            If intCount.Equals(intCounted) Or ListCount <= intMaxMark Then
                xMarker(intCounter) = "&markers=size:tiny%7Ccolor:red%7Clabel:S%7C" & latLong.Latitude.ToString() & "," & latLong.Longitude.ToString()
                intCounter += 1
                intCount = 0
            End If
            intCount += 1
        Next
        Dim strMarker As String = String.Join("", xMarker)
        'Dim url As String = "https://maps.googleapis.com/maps/api/staticmap?center=-31.9538987,115.85823189999996&zoom=&size=200x200&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-31.81608833333333,115.81080833333333&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-31.916061666666663,115.81092666666667"
        'Dim url As String = "https://maps.googleapis.com/maps/api/staticmap?center=-31.9538987,115.85823189999996&zoom=10&size=650x650&markers=size:tiny%7Ccolor:red%7Clabel:S%7C-31.81608833333333,115.81080833333333" & strMarker
        Dim url As String = "https://maps.googleapis.com/maps/api/staticmap?center=-31.9538987,115.85823189999996&zoom=10&size=650x350" & strMarker

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