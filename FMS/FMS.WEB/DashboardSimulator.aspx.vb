Imports FMS.Business.DataObjects
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports DevExpress.Web
Imports System.Net

Public Class DashboardSimulator
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            txtParking.Text = "Parking Break ON"
            txtDriving.Text = "Diagonal mode road"
            txtSpeed.Text = "100"
            txtBattery.Text = "83"
            txtLCD_Speed.Text = txtSpeed.Text
            txtLCD_hour.Text = "05:38"
            txtLCD_SteeringProgram.Text = "Diagonal mode road"
            txtFaultCode.Text = "S 1"
        End If

        'Case "Parking Break"
        'ListRow.Parking_Break = strValue

        'If strValue = "Parking Break ON" Then
        '    ListRow.StopControl = "ON"
        'Else
        '    If strValue = "Parking Break OFF" Then
        '        ListRow.StopControl = "OFF"
        '    End If
        'End If

        '                Case "Battery Voltage"
        'If Not strValue = Nothing Then
        '    ListRow.Battery_Voltage = strValue
        'Else
        '    If ListRow.Battery_Voltage.Length = 0 Then
        '        ListRow.Battery_Voltage = "0"
        '    End If
        'End If

        '                Case "Speed"
        'ListRow.LCD_Speed = strValue

        '                Case "Driving Mode"
        'ListRow.LCD_Driving_Mode = strValue
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim arrSteer() As String = {"MS 1", "MS 11", "MS 12", "MS 14", "MS 21", "MS 22", "MS 23",
                                        "MS 31", "MS 32", "MS 34", "MS 41", "MS 42"}
        Dim arrDrive() As String = {"M1 1", "M1 2", "M1 4", "M2 1", "M2 2", "M2 4",
                                    "M3 1", "M3 2", "M3 4", "M4 1", "M4 2", "M4 4"}
        Dim arrSpeed() As String = {"M1 3", "M2 3", "M3 3", "M4 3", "MS 7", "MS 8", "MS 9", "MS 9", "IO 4"}
        Dim arrWarning() As String = {"M1 3", "M1 5", "M1 6", "M2 3", "M2 5", "M2 6",
                                    "M3 3", "M3 5", "M3 6", "M4 3", "M4 5", "M4 6",
                                    "MS 6", "MS 7", "MS 8", "MS 9",
                                    "IO 1", "IO 3", "IO 4", "IO 8", "IO 30", "IO 32", "IO 33", "IO 34",
                                    "IO 35", "IO 40", "IO 41", "IO 71", "S 4", "S 5",
                                    "Canopen 1", "Canopen 2", "Canopen 3", "Canopen 4", "Canopen 6",
                                    "Canopen 7", "Canopen 8"}
        Dim arrAlign() As String = {"IO 11", "IO 12", "IO 13", "IO 14", "IO 41"}
        Dim arrIFM() As String = {"M1 1", "M2 1", "M3 1", "M4 1"}
        Dim arrCAN() As String = {"Can 1", "Can 2", "Can 3", "Can 4", "Can 5", "Can 6", "Can 7", "Can 8",
                                "Can 31", "Can 101", "Can 102", "Can 103", "Can 104", "Can 105", "Can 106",
                                "Can 107", "Can 108", "Can 131", "Can 205", "Can 206", "Can 207", "Can 208"}
        Dim arrCANOPEN() As String = {"Canopen 5"}
        Dim arrDataLogger() As String = {"IO 30"}
        Dim arrSafety() As String = {"S 1", "S 2", "S 3", "S 4", "S 5", "S 10"}
        Dim arrDrive_M1() As String = {"M1 1", "M1 2", "M1 3", "M1 4", "M1 5", "M1 6"}
        Dim arrDrive_M2() As String = {"M2 1", "M2 2", "M2 3", "M2 4", "M2 5", "M2 6"}
        Dim arrDrive_M3() As String = {"M3 1", "M3 2", "M3 3", "M3 4", "M3 5", "M3 6"}
        Dim arrDrive_M4() As String = {"M4 1", "M4 2", "M4 3", "M4 4", "M4 5", "M4 6"}
        Dim arrIO() As String = {"IO 1", "IO 2", "IO 3", "IO 4", "IO 8", "IO 11",
                                "IO 12", "IO 13", "IO 14", "IO 20", "IO 30", "IO 32",
                                "IO 33", "IO 34", "IO 35", "IO 40", "IO 41", "IO 71"}

        Dim oList As List(Of DashboardValue) = New List(Of DashboardValue)
        'Dim oListErrCat As List(Of DashboardValues.clsErrCategory) = New List(Of DashboardValues.clsErrCategory)
        Dim oListErrCat As New List(Of DashboardValue.clsErrCategory)()

        Dim ListRow = New DashboardValue
        Dim rowErrCat = New DashboardValue.clsErrCategory

        ListRow.Parking_Break = txtParking.Text
        ListRow.Driving = txtDriving.Text
        ListRow.SpeedControl = txtSpeed.Text
        ListRow.Battery_Voltage = txtBattery.Text
        ListRow.LCD_Speed = txtLCD_Speed.Text
        ListRow.LCD_Hour = txtLCD_hour.Text
        ListRow.LCD_Driving_Mode = txtLCD_SteeringProgram.Text

        Dim strFC = txtFaultCode.Text
        Dim fc As String() = Nothing
        fc = strFC.Split(",")
        Dim sfc As String
        Dim strValue As String

        For count = 0 To fc.Length - 1
            sfc = fc(count)

            Dim valErrCat As String = ""
            Dim valErrCode As String = ""
            Dim chrpos = sfc.IndexOf(" ")
            Dim sLen = sfc.Length - chrpos
            strValue = sfc.Substring(chrpos, sLen).Trim()


            Dim strSteer As String = Array.Find(arrSteer, Function(x) (x.StartsWith(sfc)))
            Dim strDrive As String = Array.Find(arrDrive, Function(x) (x.StartsWith(sfc)))
            Dim strSpeed As String = Array.Find(arrSpeed, Function(x) (x.StartsWith(sfc)))
            Dim strWarning As String = Array.Find(arrWarning, Function(x) (x.StartsWith(sfc)))
            Dim strAlign As String = Array.Find(arrAlign, Function(x) (x.StartsWith(sfc)))
            Dim strIFM As String = Array.Find(arrIFM, Function(x) (x.StartsWith(sfc)))
            Dim strCAN As String = Array.Find(arrCAN, Function(x) (x.StartsWith(sfc)))
            Dim strCANOPEN As String = Array.Find(arrCANOPEN, Function(x) (x.StartsWith(sfc)))
            Dim strDataLogger As String = Array.Find(arrDataLogger, Function(x) (x.StartsWith(sfc)))
            Dim strSafety As String = Array.Find(arrSafety, Function(x) (x.StartsWith(sfc)))
            Dim strDrvM1 As String = Array.Find(arrDrive_M1, Function(x) (x.StartsWith(sfc)))
            Dim strDrvM2 As String = Array.Find(arrDrive_M2, Function(x) (x.StartsWith(sfc)))
            Dim strDrvM3 As String = Array.Find(arrDrive_M3, Function(x) (x.StartsWith(sfc)))
            Dim strDrvM4 As String = Array.Find(arrDrive_M4, Function(x) (x.StartsWith(sfc)))
            Dim strIO As String = Array.Find(arrIO, Function(x) (x.StartsWith(sfc)))

            If Not strSteer = Nothing Then
                valErrCat = "Steering"
                valErrCode = strValue
            End If

            If Not strSafety = Nothing Then
                valErrCat = "Safety"
                valErrCode = strValue
            End If

            If Not strDrvM1 = Nothing Then
                valErrCat = "DriveM1"
                valErrCode = strValue
            End If

            If Not strDrvM2 = Nothing Then
                valErrCat = "DriveM2"
                valErrCode = strValue
            End If

            If Not strDrvM3 = Nothing Then
                valErrCat = "DriveM3"
                valErrCode = strValue
            End If

            If Not strDrvM4 = Nothing Then
                valErrCat = "DriveM4"
                valErrCode = strValue
            End If

            If Not strCAN = Nothing Then
                valErrCat = "CAN"
                valErrCode = strValue
            End If

            If Not strCANOPEN = Nothing Then
                valErrCat = "CANOPEN"
                valErrCode = strValue
            End If

            If Not strIO = Nothing Then
                valErrCat = "InOut"
                valErrCode = strValue
            End If

            'oListErrCat.Add(rowErrCat)

            ' Add parts to the list.
            oListErrCat.Add(New DashboardValue.clsErrCategory() With { _
                 .Err_Category = valErrCat, _
                 .Err_Value = valErrCode _
            })


        Next

        ListRow.LCD_ErrorCategory = oListErrCat

        oList.Add(ListRow)


        Session("ses_DashboardRecord") = oList
        MsgBx("Record Saved!")

    End Sub

    Private Sub MsgBx(msg As String)
        Dim message As String = "Record saved for simuulator."
        Dim script As String = "<script type='text/javascript'> alert('" + msg + "');</script>"

        ClientScript.RegisterClientScriptBlock(Me.GetType(), "AlertBox", script)
    End Sub

    Protected Sub btnGetDataFromAPI_Click(sender As Object, e As EventArgs) Handles btnGetDataFromAPI.Click
        Dim response As HttpResponseMessage = Nothing
        Dim client As HttpClient = New HttpClient()
        Dim baseAddress As String = ""
        baseAddress = "http://" + HttpContext.Current.Request.Url.Authority + "/"
        client.BaseAddress = New Uri(baseAddress)
        client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

        'Dim url = "api/vehicle/GetDashboardDataForSimulator?simdevID=" + txtDevID.Text
        Dim url = "api/vehicle/GetDashboardDataForSimulator?simdevID=" + txtDevID.Text
        response = client.GetAsync(url).Result

        



        If response.IsSuccessStatusCode = True Then
            Dim canMessDef As List(Of CanValueMessageDefinition) = JsonConvert.DeserializeObject(Of List(Of CanValueMessageDefinition))(response.Content.ReadAsStringAsync.Result().ToString())
            'myWorkFlows = JsonConvert.DeserializeObject(Of List(Of WorkFlows))(jobj("Workflows"))

            Dim json As String = response.Content.ReadAsStringAsync.Result().ToString()
            Dim ser As JObject = JObject.Parse(json)
            Dim data As List(Of JToken) = ser.Children().ToList
            Dim output As String = ""

            For Each item As JProperty In data
                item.CreateReader()

                Select Case item.Name
                    Case "CAN_Protocol_Type"

                End Select

            Next



            'Dim canMessDef As List(Of FMS.Business.DataObjects.ApplicationVehicle) = JsonConvert.DeserializeObject(Of List(Of FMS.Business.DataObjects.ApplicationVehicle))(strCMD)

        End If




    End Sub
End Class