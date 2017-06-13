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
    Dim canBusDef As New List(Of CanBusDefinitionValues)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack And Membership.ApplicationName <> "/" Then Exit Sub

        Dim client As HttpClient = New HttpClient()
        Dim baseAddress As String = ConfigurationManager.AppSettings("BaseAddressURI")
        Dim standard As String = ConfigurationManager.AppSettings("Standard")
        Dim spn As String = ConfigurationManager.AppSettings("SPN")
        client.BaseAddress = New Uri(baseAddress)
        client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
        Dim id = DeviceID
        'Dim url = "api/vehicle?vehicleID=" + id + "&standard=" + standard + "&spn=" + spn + "&startdate=" + Date.Now.ToString("dd/MM/yyyy") + "&enddate=" + Date.Now.ToString("dd/MM/yyyy")
        Dim url = "api/vehicle/GetCanMessageValue?deviceid=" + id
        Dim response As HttpResponseMessage = client.GetAsync(url).Result

        If response.IsSuccessStatusCode Then
            Dim canMessDef As List(Of CanValueMessageDefinition) = JsonConvert.DeserializeObject(Of List(Of CanValueMessageDefinition))(response.Content.ReadAsStringAsync.Result().ToString())
            For Each messageValue As CanValueMessageDefinition In canMessDef
                Dim cbd As New CanBusDefinitionValues()
                cbd.label = messageValue.MessageDefinition.Description
                cbd.description = messageValue.CanValues(0).Value
                canBusDef.Add(cbd)
            Next
            
            grid.DataBind()
        End If
    End Sub

    Protected Sub grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        ' Assign the data source in grid_DataBinding
        grid.DataSource = canBusDef
    End Sub

End Class

Public Class CanBusDefinitionValues
    Public Property label As String
    Public Property description As String
End Class
