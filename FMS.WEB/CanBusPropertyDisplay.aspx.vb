Imports System.Net.Http
Imports System.Net.Http.Headers
Imports Newtonsoft.Json
Imports FMS.Business.DataObjects

Public Class CanBusPropertyDisplay
    Inherits System.Web.UI.Page

    Public ReadOnly Property DeviceID As String
        Get
            Return Request.QueryString("DeviceID")
        End Get
    End Property

    Public Property DriverID As String
    Public Property DriverName As String
    Public Property VehicleName As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack And Membership.ApplicationName <> "/" Then Exit Sub

        Dim client As HttpClient = New HttpClient()
        Dim baseAddress As String = ConfigurationManager.AppSettings("BaseAddressURI")
        Dim standard As String = ConfigurationManager.AppSettings("Standard")
        Dim spn As String = ConfigurationManager.AppSettings("SPN")
        client.BaseAddress = New Uri(baseAddress)
        client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
        Dim id = DeviceID
        Dim url = "api/vehicle?vehicleID=" + id + "&standard=" + standard + "&spn=" + spn + "&startdate=" + Date.Now.ToString("dd/MM/yyyy") + "&enddate=" + Date.Now.ToString("dd/MM/yyyy")
        Dim response As HttpResponseMessage = client.GetAsync(url).Result

        If response.IsSuccessStatusCode Then
            Dim canMessDef As CanValueMessageDefinition = JsonConvert.DeserializeObject(Of CanValueMessageDefinition)(response.Content.ReadAsStringAsync.Result().ToString())
            txtStandard.Text = canMessDef.MessageDefinition.Standard
            txtPGN.Text = canMessDef.MessageDefinition.PGN
            txtSPN.Text = canMessDef.MessageDefinition.SPN
            txtDescription.Text = canMessDef.MessageDefinition.Description
            txtResolution.Text = canMessDef.MessageDefinition.Resolution
            txtUnits.Text = canMessDef.MessageDefinition.Units
            txtOffset.Text = canMessDef.MessageDefinition.offset
            txtPos.Text = canMessDef.MessageDefinition.pos
            txtSPN_Length.Text = canMessDef.MessageDefinition.SPN_Length
            txtPGN_Length.Text = canMessDef.MessageDefinition.PGN_Length
            txtData_Range.Text = canMessDef.MessageDefinition.Data_Range
            txtResolution_Multiplier.Text = canMessDef.MessageDefinition.Resolution_Multiplier
            txtPos_start.Text = canMessDef.MessageDefinition.pos_start
            txtPos_end.Text = canMessDef.MessageDefinition.pos_end
        End If
    End Sub

End Class
