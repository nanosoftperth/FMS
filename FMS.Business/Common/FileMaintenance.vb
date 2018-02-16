Imports System.Drawing
Imports System.IO
Imports System.Web
Namespace DataObjects
    Public Class FileMaintenance

        Public Shared Function SaveImageToFolder(PhotoImg As Byte(), cid As Integer?, rid As Integer?, Optional ByVal oldImage As String = "") As String
            Try
                Dim folderLocation As String = "Images\Bin"
                If Not cid Is Nothing Then
                    folderLocation = "Images\Site"
                End If
                If Not rid Is Nothing Then
                    folderLocation = "Images\Run"
                End If

                Dim folderLoc As String = folderLocation
                Dim folderPath As String = HttpContext.Current.Server.MapPath(folderLoc)
                Dim folderFilename As String = folderPath + "\" + Guid.NewGuid().ToString("N") + ".jpg"

                If Not Directory.Exists(folderPath) Then
                    Directory.CreateDirectory(folderPath)
                End If

                If Not oldImage.Equals("") Then
                    If File.Exists(oldImage) Then
                        File.Delete(oldImage)
                    End If
                End If

                SaveToFile(PhotoImg, folderFilename)
                Return folderFilename
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function imgToByteConverter(ByVal imgLoc As String) As Byte()
            Dim imgCon As New ImageConverter()
            Dim bt As Byte()
            Dim imgStr As Image
            If File.Exists(imgLoc) Then
                Using str As Stream = File.OpenRead(imgLoc)
                    imgStr = Image.FromStream(str)
                    bt = DirectCast(imgCon.ConvertTo(imgStr, GetType(Byte())), Byte())
                End Using
            Else
                bt = Nothing
            End If

            Return bt
        End Function
        Public Shared Sub SaveToFile(ByVal byteArr As Byte(), fname As String)
            Try
                Dim buff As Byte() = byteArr
                Dim memstream As New MemoryStream(buff)
                Dim fileWrite As New FileStream(fname, FileMode.Create, FileAccess.Write)
                memstream.WriteTo(fileWrite)
                fileWrite.Close()
                memstream.Close()
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Shared Sub DeleteImageFile(imageFile As String)
            If File.Exists(imageFile) Then
                File.Delete(imageFile)
            End If
        End Sub
    End Class
End Namespace

