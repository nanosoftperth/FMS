Imports System.Web
Imports System.Web.Services
Imports FMS.Business

Public Class FleetMapMarker
    Implements System.Web.IHttpHandler, IRequiresSessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        With context
            Try
                Dim type As String = .Request.QueryString("type")
                Dim vehicleName As String = .Request.QueryString("vname")

                Dim contType = "image/png"
                Dim ImageData As Byte()

                If type.ToLower.Equals("vehicle") And vehicleName IsNot Nothing Then 'BY RYAN: Check if vehicle has unique icon
                    Dim v = DataObjects.ApplicationVehicle.GetFromName(vehicleName, .Session("ApplicationID"))
                    
                    Dim img = DataObjects.ApplicationImage.GetImageFromID(v.ApplicationImageID)
                    If Not img Is Nothing Then
                        ImageData = img.Img
                    Else 'Use default image
                        GoTo DefaultImage
                    End If
                Else
DefaultImage:       'Use default image
                    ImageData = GetDefaultImage(type, .Session("ApplicationID"))
                End If


                .Response.Clear()
                .Response.ContentType = contType
                .Response.BinaryWrite(ImageData)
            Catch ex As Exception
                Dim s = ex.InnerException
            End Try
        End With

    End Sub
    Private Function GetDefaultImage(type As String, appid As Guid) As Byte()
        If String.IsNullOrEmpty(type.Trim()) Then
            Throw New Exception("Failed to load image")
        End If

        Dim id As Guid
        Dim mm = DataObjects.FleetMapMarker.GetApplicationFleetMapMarket(appid)
        If type.ToLower.Equals("home") Then
            id = mm.Home_ApplicationImageID
        ElseIf type.ToLower.Equals("vehicle") Then
            id = mm.Vehicle_ApplicationImageID
        Else
            Throw New Exception("Failed to load image")
        End If
        Dim img = DataObjects.ApplicationImage.GetImageFromID(id)

        If Not img Is Nothing Then
            Return img.Img
        Else
            Throw New Exception("Failed to load image")
        End If
    End Function
    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class