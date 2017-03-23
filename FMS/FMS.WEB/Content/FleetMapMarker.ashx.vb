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

                Dim id_str As String = .Request.QueryString("Id")

                If Not String.IsNullOrEmpty(id_str) Then

                    Dim id As Guid = Guid.Parse(id_str)
                    Dim img = DataObjects.ApplicationImage.GetImageFromID(id)

                    'check if this imate is associated with the applicationId
                    Dim appIsAllowedAccess As Boolean = img.ApplicationID Is Nothing OrElse img.ApplicationID.Value = FMS.Business.ThisSession.ApplicationID
                    'if it is not, then return to default.
                    If Not appIsAllowedAccess Then img = DataObjects.ApplicationImage.GetDefaultHomeImage

                    .Response.Clear()
                    .Response.ContentType = contType
                    .Response.BinaryWrite(img.Img)

                    Exit Sub

                End If

                'TODO: we need to remove all below this line, especially the GOTO :) !
                If type.ToLower.Equals("vehicle") And vehicleName IsNot Nothing Then 'BY RYAN: Check if vehicle has unique icon
                    Dim v = DataObjects.ApplicationVehicle.GetFromName(vehicleName, .Session("ApplicationID"))

                    Dim img = DataObjects.ApplicationImage.GetImageFromID(v.ApplicationImageID)
                    If Not img Is Nothing Then
                        ImageData = img.Img
                    Else 'Use default image
                        ImageData = GetDefaultImage(type, .Session("ApplicationID"))
                    End If
                Else
                    'Use default image
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