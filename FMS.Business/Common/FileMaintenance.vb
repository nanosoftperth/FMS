Imports System.Drawing
Imports System.IO
Imports System.Web
Namespace DataObjects
    Public Class FileMaintenance

        Public Shared Function SaveImageToFolder(PhotoImg As Byte(), Optional ByVal oldImage As String = "") As String
            Try
                Dim folderLocation As String = "Files"

                Dim fileName As String = Guid.NewGuid().ToString("N") + ".jpg"
                Dim folderPath As String = HttpContext.Current.Server.MapPath(folderLocation)
                Dim folderFilename As String = folderPath + "\" + fileName
                Dim oldFileLoc As String = HttpContext.Current.Server.MapPath(folderLocation + "/" + oldImage)

                If Not Directory.Exists(folderPath) Then
                    Directory.CreateDirectory(folderPath)
                End If

                If Not oldFileLoc.Equals("") Then
                    If File.Exists(oldFileLoc) Then
                        File.Delete(oldFileLoc)
                    End If
                End If

                SaveToFile(PhotoImg, folderFilename)
                Return fileName
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function SavePdfToFolder(pdfFile As Byte(), Optional ByVal oldPdf As String = "") As String
            Try
                Dim folderLocation As String = "Files"

                Dim fileName As String = Guid.NewGuid().ToString("N") + ".pdf"
                Dim folderPath As String = HttpContext.Current.Server.MapPath(folderLocation)
                Dim folderFilename As String = folderPath + "\" + fileName
                Dim oldFileLoc As String = HttpContext.Current.Server.MapPath(folderLocation + "/" + oldPdf)

                If Not Directory.Exists(folderPath) Then
                    Directory.CreateDirectory(folderPath)
                End If

                If Not oldFileLoc.Equals("") Then
                    If File.Exists(oldFileLoc) Then
                        File.Delete(oldFileLoc)
                    End If
                End If

                ByteToPdfConverter(pdfFile, folderFilename)
                Return fileName
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
        Public Shared Function PdfToByteConverter(ByVal pdfLoc As String) As Byte()
            Dim fInfo As New FileInfo(pdfLoc)
            Dim numBytes As Long = fInfo.Length

            Dim fStream As New FileStream(pdfLoc, FileMode.Open, FileAccess.Read)
            Dim br As New BinaryReader(fStream)
            Dim bt As Byte() = br.ReadBytes(CInt(numBytes))

            Return bt
        End Function
        Public Shared Sub ByteToPdfConverter(ByVal pdfByte As Byte(), fname As String)
            Dim bt As Byte() = pdfByte
            Dim fStream As New FileStream(fname, FileMode.OpenOrCreate)
            fStream.Write(bt, 0, bt.Length)
            fStream.Close()
        End Sub
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
            Dim imgFileLoc As String = HttpContext.Current.Server.MapPath("Files/" + imageFile)
            If File.Exists(imgFileLoc) Then
                File.Delete(imgFileLoc)
            End If
        End Sub
    End Class
End Namespace

