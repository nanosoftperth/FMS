﻿Imports System.Net.Http
Imports System.Net.Http.Headers
Imports Newtonsoft.Json
Imports FMS.Business.DataObjects
Imports DevExpress.Web
Imports System.Net

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
        Dim baseAddress As String = ""
        Dim standard As String = ConfigurationManager.AppSettings("Standard")
        Dim spn As String = ConfigurationManager.AppSettings("SPN")
        baseAddress = "http://" + HttpContext.Current.Request.Url.Authority + "/"
        client.BaseAddress = New Uri(baseAddress)
        client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
        Dim id = DeviceID
        Dim response As HttpResponseMessage = Nothing
        Dim intCounter As Integer = 0

        While intCounter <= 10
            Dim url = "api/vehicle/GetCanMessageValue?deviceid=" + id
            response = client.GetAsync(url).Result
            If response.StatusCode.ToString().Equals("InternalServerError") Then
                intCounter += 1
            Else
                Exit While
            End If
        End While

        If response.IsSuccessStatusCode Then
            Dim canMessDef As List(Of CanValueMessageDefinition) = JsonConvert.DeserializeObject(Of List(Of CanValueMessageDefinition))(response.Content.ReadAsStringAsync.Result().ToString())
            For Each messageValue As CanValueMessageDefinition In canMessDef.Where(Function(x) Not x.MessageDefinition.SPN.Equals(6))
                Dim cbd As New CanBusDefinitionValues()
                If Not messageValue.CanValues.Count.Equals(0) Then
                    cbd.label = messageValue.MessageDefinition.Description
                    'this is for temporary only
                    If messageValue.MessageDefinition.Standard.ToLower.Equals("zagro500") And Not messageValue.MessageDefinition.SPN = 7 Then
                        cbd.spn = 10 + messageValue.MessageDefinition.SPN
                    Else
                        cbd.spn = messageValue.MessageDefinition.SPN
                    End If

                    If Not messageValue.CanValues(0).Value Is Nothing Then
                        If Not messageValue.MessageDefinition.Units Is Nothing And _
                            Not messageValue.CanValues(0).Value.ToString().Equals("0") Then
                            If Not messageValue.CanValues(0).Value.ToString().Equals("") Then
                                If Not Format(messageValue.CanValues(0).Value, "##.#").ToString().Equals("") Then
                                    cbd.description = Convert.ToDouble(Format(messageValue.CanValues(0).Value, "##.#").ToString()) & " " & messageValue.MessageDefinition.Units
                                Else
                                    cbd.description = "0"
                                End If
                            Else
                                cbd.description = "0"
                            End If

                        Else
                            cbd.description = messageValue.CanValues(0).Value.ToString()
                        End If
                    End If
                    cbd.dtTime = messageValue.CanValues(0).Time.ToString("MM/dd/yy HH:mm:ss")
                    canBusDef.Add(cbd)
                End If
            Next

            Dim batVolt = canBusDef.Where(Function(xx) xx.spn = 7).ToList()
            Dim intMyIndex As Integer
            If batVolt.Count > 1 Then
                If batVolt(0).dtTime < batVolt(1).dtTime Then
                    intMyIndex = canBusDef.FindIndex(Function(x) x.dtTime.Equals(batVolt(0).dtTime))
                    canBusDef.RemoveAt(intMyIndex)
                Else
                    intMyIndex = canBusDef.FindIndex(Function(x) x.dtTime.Equals(batVolt(1).dtTime))
                    canBusDef.RemoveAt(intMyIndex)
                End If

            End If

            grid.DataBind()

        End If
    End Sub

    Protected Sub grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        ' Assign the data source in grid_DataBinding
        grid.DataSource = canBusDef
    End Sub

    Protected Sub hyperLink_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim link As ASPxHyperLink = CType(sender, ASPxHyperLink)

        Dim templateContainer As GridViewDataItemTemplateContainer = CType(link.NamingContainer, GridViewDataItemTemplateContainer)

        Dim rVI As Integer = templateContainer.VisibleIndex
        Dim getLabel As String = templateContainer.Grid.GetRowValues(rVI, "label").ToString()
        Dim getDescription As String = templateContainer.Grid.GetRowValues(rVI, "description").ToString()
        Dim desc As String = ""

        If getLabel.Equals("Fault Codes") Then
            Dim getCodeDescription As String = getDescription
            For Each arr In getDescription.Split(",")
                Dim canBusFaultDef = FMS.Business.DataObjects.CanDataPoint.CanBusFaultDefinition.GetFaultCodeList()
                Dim canDescription = canBusFaultDef.Where(Function(canDef) canDef.Key.Equals(arr)).ToList()
                If Not canDescription.Count.Equals(0) Then
                    For Each arr2 In canDescription
                        desc &= arr2.Key + " = " + arr2.Value.Split(",")(1) + ","
                    Next
                End If
            Next
        End If

        Dim contentUrl As String = String.Format("{0}", desc)

        link.NavigateUrl = "javascript:void(0);"
        link.Text = String.Format(getDescription)
        link.ClientSideEvents.Click = String.Format("function(s, e) {{ OnFaultCodesClick('{0}'); }}", contentUrl)
    End Sub

    Protected Sub grid_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs) Handles grid.HtmlDataCellPrepared
        Dim getLabelName As String = (TryCast(sender, ASPxGridView)).GetRowValues(e.VisibleIndex, "label").ToString()
        If Not getLabelName.Equals("Fault Codes") Then
            If e.DataColumn.FieldName = "description" Then
                e.Cell.Enabled = False
            End If
        End If

    End Sub
End Class

Public Class CanBusDefinitionValues
    Public Property label As String
    Public Property description As String
    Public Property dtTime As String
    Public Property spn As Integer
End Class
