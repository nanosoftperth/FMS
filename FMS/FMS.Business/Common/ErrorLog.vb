Imports System.IO
Imports System.Net
Imports System.Web
Public Class ErrorLog

    Public Shared Sub WriteErrorLog(ex As Exception)

        Dim message As String = String.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"))
        message += Environment.NewLine
        message += "-----------------------------------------------------------"
        message += Environment.NewLine
        message += String.Format("Message: {0}", ex.Message)
        message += Environment.NewLine
        message += String.Format("StackTrace: {0}", ex.StackTrace)
        message += Environment.NewLine
        message += String.Format("Source: {0}", ex.Source)
        message += Environment.NewLine
        message += String.Format("TargetSite: {0}", ex.TargetSite.ToString())
        message += Environment.NewLine
        message += "-----------------------------------------------------------"
        message += Environment.NewLine

        Dim path As String = HttpContext.Current.Server.MapPath("~/Log/")
        If Not Directory.Exists(path) Then
            Directory.CreateDirectory(path)
        End If

        Dim FileName As String = HttpContext.Current.Server.MapPath("~/Log/" + DateTime.Now.ToString("ddMMyyyy") + ".txt")
        If Not File.Exists(FileName) Then
            File.Create(FileName)
        End If


        Using writer As New StreamWriter(FileName, True)
            writer.WriteLine(message)
            writer.Close()
        End Using

    End Sub
End Class
