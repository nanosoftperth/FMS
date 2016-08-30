
Imports FMS.Business


Public Class test_Settings
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        CType(Me.Master, FMS.WEB.MainLightMaster).HeaderText = "Test Setting Page"


        If Not IsPostBack Then

            'show the binary image, if it exists:

            Dim thisapp As DataObjects.Application = _
                                   DataObjects.Application.GetFromAppID(ThisSession.ApplicationID)

            Dim Settings As List(Of DataObjects.Setting) = DataObjects.Setting.GetSettingsForApplication(thisapp.ApplicationName)


            Dim logoBytes() As Byte = DataObjects.Setting.GetLogoForApplication(thisapp.ApplicationName)

            Me.imgCompanylogo.ContentBytes = logoBytes


        End If



    End Sub

    Private Sub imgCompanylogo_CustomCallback(sender As Object, e As DevExpress.Web.CallbackEventArgsBase) Handles imgCompanylogo.CustomCallback

    End Sub

    Private Sub imgCompanylogo_ValueChanged(sender As Object, e As EventArgs) Handles imgCompanylogo.ValueChanged

    End Sub

    Protected Sub ASPxButton1_Click(sender As Object, e As EventArgs) Handles ASPxButton1.Click

        If Me.imgCompanylogo.ContentBytes Is Nothing Then Exit Sub

        Dim logoBytes() As Byte = Me.imgCompanylogo.ContentBytes

        Dim thisapp As DataObjects.Application = _
                                  DataObjects.Application.GetFromAppID(ThisSession.ApplicationID)

        Dim s As DataObjects.Setting = DataObjects.Setting.GetSettingsForApplication(thisapp.ApplicationName) _
                                                                        .Where(Function(itm) itm.Name = "Logo").Single

        s.ValueObj = Me.imgCompanylogo.ContentBytes

        DataObjects.Setting.Update(s)

    End Sub
End Class