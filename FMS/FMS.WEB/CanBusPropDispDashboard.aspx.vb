Imports System.Net.Http
Imports System.Net.Http.Headers
Imports Newtonsoft.Json
Imports FMS.Business.DataObjects

Public Class CanBusPropDispDashboard
    Inherits System.Web.UI.Page

    Public ReadOnly Property deviceID As String
        Get
            Return Request.QueryString("deviceID")
        End Get
    End Property

    Public ReadOnly Property VehicleName As String
        Get
            Return Request.QueryString("VehicleName")
        End Get
    End Property

    Public ReadOnly Property ClickEvent As String
        Get
            Return Request.QueryString("ClickEvent")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then

            'Page.ClientScript.RegisterStartupScript(Me.[GetType](), "CallMyFunction", "MyFunction()", True)

            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "CallGMInit", "<script>initialize()</script>", False)

            'lblVehicleID.Text = vehicleID

            'Dim scriptKey As String = "UniqueKeyForThisScript"
            'Dim javaScript As String = "<script type='text/javascript'>GetDashboardData();</script>"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

        End If
        'If IsPostBack And Membership.ApplicationName <> "/" Then Exit Sub

        'Dim client As HttpClient = New HttpClient()
        'Dim baseAddress As String = ConfigurationManager.AppSettings("BaseAddressURI")
        ''Dim standard As String = ConfigurationManager.AppSettings("Standard")
        ''Dim spn As String = ConfigurationManager.AppSettings("SPN")
        'client.BaseAddress = New Uri(baseAddress)
        'client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
        'Dim id = vehicleID
        'Dim url = "api/vehicle/GetDashboardData?vehicleID=" + id
        'Dim response As HttpResponseMessage = client.GetAsync(url).Result

        'If response.IsSuccessStatusCode Then

        '    'Dim canMessDef As List(Of CanValueMessageDefinition) = JsonConvert.DeserializeObject(Of List(Of CanValueMessageDefinition))(response.Content.ReadAsStringAsync.Result().ToString())
        '    'For Each messageValue As CanValueMessageDefinition In canMessDef
        '    '    Dim cbd As New CanBusDefinitionValues()
        '    '    cbd.label = messageValue.MessageDefinition.Description
        '    '    'this is for temporary only
        '    '    If messageValue.MessageDefinition.Standard.ToLower.Equals("zagro500") Then
        '    '        cbd.spn = 10 + messageValue.MessageDefinition.SPN
        '    '    Else
        '    '        cbd.spn = messageValue.MessageDefinition.SPN
        '    '    End If

        '    '    If Not messageValue.CanValues.Count.Equals(0) Then
        '    '        If Not messageValue.CanValues(0).Value Is Nothing Then
        '    '            If Not messageValue.MessageDefinition.Units Is Nothing And _
        '    '                Not messageValue.CanValues(0).Value.ToString().Equals("0") Then
        '    '                If Not Format(messageValue.CanValues(0).Value, "##.#").ToString().Equals("") Then
        '    '                    cbd.description = Format(messageValue.CanValues(0).Value, "##.#").ToString() + " " + messageValue.MessageDefinition.Units
        '    '                Else
        '    '                    cbd.description = "0"
        '    '                End If
        '    '            Else
        '    '                cbd.description = messageValue.CanValues(0).Value.ToString()
        '    '            End If
        '    '        End If
        '    '        cbd.dtTime = messageValue.CanValues(0).Time.ToString("HH:mm:ss")
        '    '    End If
        '    '    canBusDef.Add(cbd)
        '    'Next

        '    'grid.DataBind()

        'End If
    End Sub

End Class