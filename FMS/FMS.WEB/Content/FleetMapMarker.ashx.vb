Imports System.Web
Imports System.Web.Services
Imports FMS.Business

Public Class FleetMapMarker
    Implements System.Web.IHttpHandler, IRequiresSessionState

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        With context
            Try
                Dim type As String = .Request.QueryString("type")
                If String.IsNullOrEmpty(type.Trim()) Then
                    Throw New Exception("Failed to load image")
                End If

                Dim id As Guid
                Dim mm = DataObjects.FleetMapMarker.GetApplicationFleetMapMarket(.Session("ApplicationID"))
                If type.ToLower.Equals("home") Then
                    id = mm.Home_ApplicationImageID
                ElseIf type.ToLower.Equals("vehicle") Then
                    id = mm.Vehicle_ApplicationImageID
                Else
                    Throw New Exception("Failed to load image")
                End If
                Dim img = DataObjects.ApplicationImage.GetImageFromID(id)
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