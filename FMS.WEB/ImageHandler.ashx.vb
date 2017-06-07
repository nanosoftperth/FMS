Imports System.Web
Imports System.Web.Services
Imports FMS.Business
Public Class ImageHandler
    Implements System.Web.IHttpHandler

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

         
        With context
            Try
                Dim imageid As String = .Request.QueryString("imgID")
                Dim id = Guid.Parse(imageid)
                Dim contType = "image/png"
                Dim ImageData As Byte()

                Dim img = DataObjects.ApplicationImage.GetVehicleImageFromID(id)
                If Not img Is Nothing Then
                    ImageData = img.Img

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