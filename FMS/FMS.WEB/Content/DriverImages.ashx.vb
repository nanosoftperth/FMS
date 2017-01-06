Imports System.Web
Imports System.Web.Services
Imports FMS.Business

Public Class DriverImages
    Implements System.Web.IHttpHandler, IRequiresSessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        With context
            Try
                Dim fileId As String = .Request.QueryString("Id")
                Dim id = Guid.Parse(fileId)
                Dim contType = "image/png"
                Dim ImageData As Byte()

                Dim img = DataObjects.ApplicationDriver.GetDriverFromID(id)
                If Not img Is Nothing Then
                    ImageData = img.PhotoBinary

                    .Response.Clear()
                    .Response.ContentType = contType
                    .Response.BinaryWrite(ImageData)
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