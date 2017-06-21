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
        Dim url = "api/vehicle/GetCanMessageValue?deviceid=" + id
        Dim response As HttpResponseMessage = client.GetAsync(url).Result

        If response.IsSuccessStatusCode Then
            Dim canMessDef As List(Of CanValueMessageDefinition) = JsonConvert.DeserializeObject(Of List(Of CanValueMessageDefinition))(response.Content.ReadAsStringAsync.Result().ToString())
            For Each messageValue As CanValueMessageDefinition In canMessDef
                Dim cbd As New CanBusDefinitionValues()
                cbd.label = messageValue.MessageDefinition.Description
                cbd.spn = messageValue.MessageDefinition.SPN
                If Not messageValue.CanValues.Count.Equals(0) Then
                    If Not messageValue.CanValues(0).Value Is Nothing Then
                        If Not messageValue.MessageDefinition.Units Is Nothing And _
                            Not messageValue.CanValues(0).Value.ToString().Equals("0") Then
                            cbd.description = Format(messageValue.CanValues(0).Value, "##.#").ToString() + " " + messageValue.MessageDefinition.Units
                        Else
                            If messageValue.MessageDefinition.SPN = 5 Then
                                cbd.description = messageValue.CanValues(messageValue.CanValues.Count - 1).Value.ToString()
                            Else
                                cbd.description = messageValue.CanValues(0).Value.ToString()
                            End If
                        End If
                    End If
                    cbd.dtTime = messageValue.CanValues(0).Time.ToString("HH:mm:ss")
                End If
                If Not canBusDef.Where(Function(x) x.spn.Equals(cbd.spn)).Count > 0 Then canBusDef.Add(cbd)
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
    Public Property dtTime As String
    Public Property spn As Integer
End Class
