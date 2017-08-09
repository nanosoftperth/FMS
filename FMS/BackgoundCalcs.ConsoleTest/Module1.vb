Imports System.Net.Mail
Imports System.Text

Module Module1

    Dim version As String = "11.6"

    Public Property fileName As String = String.Empty

    Public Sub LogMsg(msg As String, ByVal ParamArray args() As String)

        msg = String.Format(msg, args)


        If String.IsNullOrEmpty(fileName) Then
            fileName = String.Format("c:\temp\logs\BackGroundCalcsLog_{0}.txt", Now.ToString("yyyyMMddHHmmss"))
            System.IO.File.Create(fileName).Close()
        End If

        msg = Now.ToString("yyyyMMdd HH:mm:ss") & vbTab & msg


        Console.WriteLine(msg)
        System.IO.File.AppendAllText(fileName, msg & vbNewLine)

    End Sub

    Sub Main(args() As String)

        LogMsg("Started on " & Now.ToString("HH:mm:ss"))

        LogMsg("Version: {0}", version)

        If args.Count > 0 Then
            Select Case args(0)
                Case "TestReport" : TestReport()
                Case "sendEmail" : sendEmail()
                Case "geoFenceTest" : geoFenceTest()
                Case "sendSMSTest" : sendSMSTest()
                Case "timeZoneList" : timeZoneList()
                Case "distancecalc" : distancecalc()

                Case "main_method" : While True : main_method() : End While
            End Select

            Console.ReadKey()
            Exit Sub
        End If


        MainMethod.ExecuteInfinateLoop()

    End Sub


    Private Sub distancecalc()

        'FMS.Business.BackgroundCalculations.SpeedTimeCalcs.RecalcDistanceValues("cannon038", CDate("14 july 2017 07:10:00"), CDate("14 july 2017 07:47:00"))


    End Sub

    Public Sub main_method()
        'THIS IS RUN INFINITELY BY THE SERVICE 

        LogMsg("Version ""{0}"" ", version)

        Dim lst As List(Of FMS.Business.DataObjects.Application) = FMS.Business.DataObjects.Application.GetAllApplications()

        LogMsg("approved apps: {0}", My.Settings.ApprovedCalcList)

        For Each o As FMS.Business.DataObjects.Application In lst.Where(Function(x) _
                                                    My.Settings.ApprovedCalcList.ToLower.Contains(x.ApplicationName.ToLower))

            LogMsg("Processing ""{0}"" at {1}", o.ApplicationName, Now.ToLongDateString)

            LogMsg("Processing GeoFence Collissions")
            Dim GeoFenceCollissionEarliestStartDate As Date = _
                FMS.Business.BackgroundCalculations.GeoFenceCalcs.ProcessGeoFenceCollisions(o.ApplicationID)



            LogMsg("Processing Speed/Time values")
            Dim wasSpeedtimeValsSuccess As Boolean = _
                    FMS.Business.BackgroundCalculations.SpeedTimeCalcs.ProcessSpeedtimeVals(o.ApplicationID) ', CDate("13/07/2017"), True)

            GeoFenceCollissionEarliestStartDate = Now.AddMonths(-1)

            LogMsg("Processing GeoFence collission alerts")
            FMS.Business.BackgroundCalculations.GeoFenceCalcs.ProcessGeoFenceCollissionAlerts(o.ApplicationID, _
                                                                                              GeoFenceCollissionEarliestStartDate)


            LogMsg("Processing CANBUS Events") 'TODO

            LogMsg("Processing CANBUS Alarms")


        Next

        LogMsg("Exited at " & Now.ToString("HH:mm:ss"))

        'Console.ReadKey()

    End Sub


    Private Sub geoFenceTest()

        'TODO: how do we determine this, just cheat and see the last 1 days
        Dim startTime As DateTime = New Date(Now.Year, Now.Month, Now.Day)
        Dim endTime As DateTime = Now.AddSeconds(-5)


        Dim lat1 As Decimal = -31.946999
        Dim lng1 As Decimal = 115.821398

        Dim lat2 As Decimal = -31.946811
        Dim lng2 As Decimal = 115.823442


        Dim pos1 As New FMS.Business.BackgroundCalculations.Loc(lat1, lng1)
        Dim pos2 As New FMS.Business.BackgroundCalculations.Loc(lat2, lng2)

        Dim answerinM As Decimal = FMS.Business.BackgroundCalculations.GeoFenceCalcs.CoordDistanceM(pos1, pos2)

        LogMsg(answerinM)

        Console.ReadKey()


    End Sub


    Public Sub timeZoneList()


        'TimeZoneInfo.FromSerializedString()

        For Each x In FMS.Business.DataObjects.TimeZone.GetMSoftTZones



            LogMsg(x.ToString & vbNewLine)

        Next


        Console.ReadKey()

    End Sub

    Public Sub sendSMSTest()


        'dave and graydons SMS numbers
        '0467280912, 0405581834

        FMS.Business.BackgroundCalculations.EmailHelper.SendSMS("0467280912;0405581834", "test copmany", "Zubair", "a lake", Now.AddMinutes(-10), FMS.Business.DataObjects.AlertType.ActionType.Enters)

    End Sub

    Private Sub sendEmail()

        'Command line argument must the the SMTP host.
        With New SmtpClient()

            .Port = 587
            .Host = "smtp.zoho.com"
            .EnableSsl = True
            .Timeout = 10000
            .DeliveryMethod = SmtpDeliveryMethod.Network
            .UseDefaultCredentials = False
            .Credentials = New System.Net.NetworkCredential("dave@nanosoft.com.au", "Placeb0!")

            Dim mm As New MailMessage("no-reply@nanosoft.com.au", "davegardner99@gmail.com", "from code", "from code")
            mm.BodyEncoding = UTF8Encoding.UTF8
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure

            .Send(mm)

        End With

    End Sub



    Private Sub TestReport()


        Dim x As FMS.Business.DataObjects.Application = FMS.Business.DataObjects.Application.GetFromApplicationName("ppjs")

        Dim v As FMS.Business.DataObjects.ApplicationVehicle = FMS.Business.DataObjects.ApplicationVehicle.GetAll(x.ApplicationID)(1)

        Dim obj As Object = FMS.Business.ReportGeneration.ReportGenerator.GetActivityReportLines_ForVehicle( _
                                                CDate("31 march 2016"), CDate("01 apr 2016"), v.ApplicationVehileID)

    End Sub

End Module
