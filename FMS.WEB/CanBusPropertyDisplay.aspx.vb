Imports System.Net.Http
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
    Public ReadOnly Property DeviceName As String
        Get
            Return Request.QueryString("DeviceName")
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
        baseAddress = "http://" + HttpContext.Current.Request.Url.Authority + "/"
        client.BaseAddress = New Uri(baseAddress)
        client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
        Dim id = DeviceID
        If Session("deviceId") IsNot Nothing AndAlso Not Session("deviceId").Equals(DeviceID) Then
            Session("canBusDef") = Nothing
            Session("deviceId") = id
        End If
        If Session("deviceId") Is Nothing Then
            Session("deviceId") = id
        End If

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
        Try
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
                                        Dim dblValue As Double
                                        If Double.TryParse(messageValue.CanValues(0).Value, dblValue) Then
                                            cbd.description = Convert.ToDouble(Format(messageValue.CanValues(0).Value, "##.#").ToString()) & " " & messageValue.MessageDefinition.Units
                                        Else

                                            If messageValue.MessageDefinition.Units = "hrs" Or messageValue.MessageDefinition.Description = "Hour Counter" Then
                                                cbd.description = messageValue.CanValues(0).Value & " " & messageValue.MessageDefinition.Units
                                            Else
                                                cbd.description = messageValue.CanValues(0).Value
                                            End If


                                        End If
                                    Else
                                        cbd.description = "0"
                                    End If
                                Else
                                    cbd.description = "0"
                                End If
                            Else
                                cbd.description = messageValue.CanValues(0).Value.ToString()
                            End If
                        Else
                            cbd.description = ""
                        End If
                        If Not DeviceName.ToUpper.IndexOf("XL") > 0 AndAlso (cbd.label.Equals("PressureValues3") Or cbd.label.Equals("PressureValues4")) Then
                            cbd.description = "Not implemented"
                        End If

                        cbd.dtTime = messageValue.CanValues(0).Time.ToString("MM/dd/yy HH:mm:ss")

                        If Not cbd.label = "LED" Or Not cbd.label = "Stop" Or Not cbd.label = "AC Fault Codes" Then '---> filter not to display LED entry on NTD
                            canBusDef.Add(cbd)
                        End If
                        'If Not cbd.label = "LED" Or Not cbd.label = "Stop" Then '---> filter not to display LED entry on NTD
                        '    canBusDef.Add(cbd)
                        'End If


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
            End If
        Catch ex As Exception
            Dim x As String = ex.Message
        End Try

        grid.DataBind()
    End Sub

    Protected Sub grid_DataBinding(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If Session("canBusDef") Is Nothing And canBusDef.Count > 0 Then
                Session("canBusDef") = canBusDef
            End If
            If canBusDef.Count.Equals(0) And Not Session("canBusDef") Is Nothing Then
                canBusDef = Session("canBusDef")
            End If

            For row As Integer = 0 To canBusDef.Count - 1
                Select Case canBusDef(row).label
                    Case "Speed"
                        canBusDef(row).sortNdx = -2
                    Case "Parking Brake"
                        canBusDef(row).sortNdx = -1
                    Case "Beacon Operation"
                        canBusDef(row).sortNdx = 0
                    Case "Horn"
                        canBusDef(row).sortNdx = 3
                    Case "Hour Counter"
                        canBusDef(row).sortNdx = 4
                    Case "Battery Voltage"
                        canBusDef(row).sortNdx = 5
                    Case "PressureValues1"
                        canBusDef(row).sortNdx = 6
                    Case "PressureValues2"
                        canBusDef(row).sortNdx = 7
                    Case "PressureValues3"
                        canBusDef(row).sortNdx = 8
                    Case "PressureValues4"
                        canBusDef(row).sortNdx = 9
                    Case "Driving Mode"
                        canBusDef(row).sortNdx = 10
                    Case "Motor Temp1"
                        canBusDef(row).sortNdx = 11
                    Case "Motor Temp2"
                        canBusDef(row).sortNdx = 12
                    Case "Motor Temp3"
                        canBusDef(row).sortNdx = 13
                    Case "Motor Temp4"
                        canBusDef(row).sortNdx = 14
                    Case "Fault Codes"
                        canBusDef(row).sortNdx = 15
                    Case "LED"
                        canBusDef(row).sortNdx = 16
                    Case "Stop"
                        canBusDef(row).sortNdx = 17
                    Case "AC Fault Codes"
                        canBusDef(row).sortNdx = 18
                End Select
            Next

        Catch ex As Exception
            canBusDef = Nothing
        End Try

        grid.DataSource = canBusDef
    End Sub

    Protected Sub hyperLink_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim link As ASPxHyperLink = CType(sender, ASPxHyperLink)
        Try
            Dim templateContainer As GridViewDataItemTemplateContainer = CType(link.NamingContainer, GridViewDataItemTemplateContainer)

            Dim rVI As Integer = templateContainer.VisibleIndex
            Dim getLabel As String = templateContainer.Grid.GetRowValues(rVI, "label").ToString()
            Dim getDescription As String = templateContainer.Grid.GetRowValues(rVI, "description").ToString()
            Dim desc As String = ""

            If Not DeviceName.ToUpper.IndexOf("XL") > 0 AndAlso (getLabel.Equals("PressureValues3") Or getLabel.Equals("PressureValues4")) Then
                Dim getCodeDescription As String = getDescription
                desc = "Not implemented in the E-Maxis S, M & L series"
            End If

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
            If Not DeviceName.ToUpper.IndexOf("XL") > 0 Then
                If getLabel.Equals("PressureValues3") Or getLabel.Equals("PressureValues4") Then
                    pcLogin2.HeaderText = "Pressure Values Information"
                    link.ClientSideEvents.Click = String.Format("function(s, e) {{ OnPressureValuesClick('{0}'); }}", contentUrl)
                Else
                    pcLogin.HeaderText = "Fault Code Information"
                    link.ClientSideEvents.Click = String.Format("function(s, e) {{ OnFaultCodesClick('{0}'); }}", contentUrl)
                End If
            Else
                pcLogin.HeaderText = "Fault Code Information"
                link.ClientSideEvents.Click = String.Format("function(s, e) {{ OnFaultCodesClick('{0}'); }}", contentUrl)
            End If

        Catch ex As Exception
        End Try
    End Sub

    Protected Sub grid_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs) Handles grid.HtmlDataCellPrepared
        Dim getLabelName As String = (TryCast(sender, ASPxGridView)).GetRowValues(e.VisibleIndex, "label").ToString()
        If Not DeviceName.ToUpper.IndexOf("XL") > 0 Then
            If Not getLabelName.Equals("Fault Codes") And Not getLabelName.Equals("PressureValues3") And Not getLabelName.Equals("PressureValues4") Then
                If e.DataColumn.FieldName = "description" Then
                    e.Cell.Enabled = False
                End If
            End If
        Else
            If Not getLabelName.Equals("Fault Codes") Then
                If e.DataColumn.FieldName = "description" Then
                    e.Cell.Enabled = False
                End If
            End If
        End If

    End Sub
End Class

Public Class CanBusDefinitionValues
    Public Property label As String
    Public Property description As String
    Public Property dtTime As String
    Public Property spn As Integer
    Public Property sortNdx As Integer
End Class
