Imports System.Web
Imports System.Web.Services
Imports FMS.Business.DataObjects

Public Class MapImages
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        With context
            Try
                Dim fileId As String = .Request.QueryString("imgId")
                Dim id = Guid.Parse(fileId)
                Dim img = ApplicationImage.GetImageFromID(id)
                Dim contType = "image/png"
                If Not img Is Nothing Then
                    '    Select Case img.Name.Split(".").Last
                    '        Case "tiff"
                    '            contType = "image/tiff"
                    '        Case "jpg"
                    '            contType = "image/jpg"
                    '        Case "jpeg"
                    '            contType = "image/jpeg"
                    '        Case "png"
                    '            contType = "image/png"
                    '        Case "gif"
                    '            contType = "image/gif"
                    '        Case "ico"
                    '            contType = "image/ico"
                    '        Case "bmp"
                    '            contType = "image/bmp"
                    '        Case Else
                    '            Throw New Exception("Failed to load image. Unknown file type.")
                    '    End Select

                    .Response.Clear()
                    .Response.ContentType = contType
                    .Response.BinaryWrite(img.Img)
                Else
                    Throw New Exception("Failed to load image")
                End If
            Catch ex As Exception
                Dim s = ex.InnerException
            End Try
        End With

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class