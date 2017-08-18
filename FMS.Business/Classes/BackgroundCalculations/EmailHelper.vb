Imports System.Net.Mail
Imports System.Text

Namespace BackgroundCalculations

    Public Class EmailHelper

        Public Const SMS_PROVIDER As String = "send.smsbroadcast.com.au"

        '***************************    EMAIL CONTENT AS OF 05/APR/2016     ***************************
        Public Const EmailContent As String = _
        "Alert from {0}.nanosoft.com.au, " & vbNewLine & vbNewLine & _
        "Driver ""{1}"" {2} location ""{3}"" at {4}. """

        'BY RYAN: 
        Public Const BookingEmailContent_Arriving As String = _
        "Dear {0}, " & vbNewLine & vbNewLine & _
        "Your driver is {1} away from your location now." & _
        "Your drivers name is {2} and is driving a {3}." & vbNewLine & _
        "Their number is {4}, if you wish to contact them. " & vbNewLine & _
        "Thank you," & vbNewLine & vbNewLine & _
        "Nanosoft Automated Services"

        Public Const BookingEmailContent_Leaving As String = _
        "Dear {0}, " & vbNewLine & vbNewLine & _
        "Your driver has left and is on their way." & _
        "Your drivers name is {1} and is driving a {2}." & vbNewLine & _
        "Their number is {3}, if you wish to contact them. " & vbNewLine & _
        "Thank you," & vbNewLine & vbNewLine & _
        "Nanosoft Automated Services"

        Public Const EmailContentForForgotPassword As String = _
        "Message from {0}.nanosoft.com.au, " & vbNewLine & vbNewLine & _
        "You can change your password by clicking the link below : " & vbNewLine & _
        "{1}" & vbNewLine & _
        "The link will expire after 24 hours."

        Public Const EmailContentForNewUser As String = _
        "Alert from {0}.nanosoft.com.au, " & vbNewLine & vbNewLine & _
        "Your account has been successfully created. " & vbNewLine & _
        "Username :  {1}" & vbNewLine & _
        "Password :  {2}"

        Public Const CanbusAlertEmailContent As String = _
        "Dear {0}, " & vbNewLine & vbNewLine & _
        "There has been an alarm fired for {1} to {2} " & vbNewLine & _
        "This alarm was : {3} " & vbNewLine & _
        "If you would like more information, please visit {4} " & vbNewLine & _
        "Thank you," & vbNewLine & vbNewLine & _
        "Nanosoft Automated Services"
        Friend Shared Function SendAlertMail(emailList As String, companyName As String, RecipientName As String, vehicleName As String, startTime As String, alertType As String, url As String) As String
            Dim RetValue As String = String.Empty
            Dim messageBody As String = String.Empty
            Try
                Dim subject = String.Format("Canbus alert from {0}.nanosoft.com.au", companyName)
                messageBody = String.Format(CanbusAlertEmailContent, RecipientName, vehicleName, startTime, alertType, url)
                SendEmail(emailList, subject, messageBody)
                RetValue = messageBody
            Catch ex As Exception
                RetValue = ex.Message
            End Try
            Return RetValue
        End Function
        Friend Shared Function SendEmail(emailList As String, companyName As String, _
                                         driverName As String, geofence_Name As String, _
                                         EntryOrExitTime As Date, typ As DataObjects.AlertType.ActionType) As String


            Dim messageBody As String = String.Empty

            Try
                messageBody = My.Settings.EmailMessage
                Dim EntryOrExitTime_formatted As String = EntryOrExitTime.ToString("dd/MMM/yyyy HH:mm:ss")
                Dim enteredorexited As String = If(typ = DataObjects.AlertType.ActionType.Enters, "entered", "exited")
                Dim subject = String.Format("geo-fence alert from {0}.nanosoft.com.au", companyName)

                messageBody = String.Format(EmailContent, companyName, driverName, enteredorexited, geofence_Name, EntryOrExitTime_formatted)

                SendEmail(emailList, subject, messageBody)

            Catch ex As Exception
                Dim x As String = ex.Message
            End Try

            Return messageBody

        End Function

        'BY RYAN: Email for booking!
        Friend Shared Function SendCarBookingEmail(emailList As String,
                                         companyName As String,
                                          recipient As String,
                                          driverName As String,
                                          cartype As String,
                                          number As String,
                                          actionType As Business.DataObjects.AlertType.ActionType) As String

            Dim messageBody As String = String.Empty

            Try

                Dim subject As String = String.Format("Booking alert from {0}.nanosoft.com.au", companyName)

                Select Case actionType

                    Case DataObjects.AlertType.ActionType.Leaves
                        messageBody = String.Format(BookingEmailContent_Leaving, recipient, driverName, cartype, number)

                    Case DataObjects.AlertType.ActionType.Enters
                        messageBody = String.Format(BookingEmailContent_Arriving, recipient, "2 km", driverName, cartype, number)
                End Select

                SendEmail(emailList, subject, messageBody)

            Catch ex As Exception
                Dim x As String = ex.Message
            End Try

            Return messageBody

        End Function

        'BY RYAN: 
        Public Shared Function SendEmailChangePasswordRequest(emailList As String, companyName As String, uri As String) As String
            Dim messageBody As String = String.Empty
            Try
                Dim subject As String = String.Format("request change of user credential / password from {0}.nanosoft.com.au", companyName)
                messageBody = String.Format(EmailContentForForgotPassword, companyName, uri)
                SendEmail(emailList, subject, messageBody)
            Catch ex As Exception
                Dim x As String = ex.Message
            End Try
            Return messageBody
        End Function

        'BY RYAN: 
        Public Shared Function SendEmailUserCreated(emailList As String, companyName As String, username As String, password As String) As String
            Dim messageBody As String = String.Empty
            Try
                Dim subject As String = String.Format("New user alert from {0}.nanosoft.com.au", companyName)
                messageBody = String.Format(EmailContentForNewUser, companyName, username, password)
                SendEmail(emailList, subject, messageBody)
            Catch ex As Exception
                Dim x As String = ex.Message
            End Try
            Return messageBody
        End Function

        ''' <summary>
        ''' Sends SMS messages using the paid-for SMS gateway
        ''' which converts emails to SMS's
        ''' </summary>
        ''' <param name="textList">a semi-colon seperated list of SMS numbers</param>       
        Public Shared Function SendSMS(textList As String, companyName As String, _
                                         driverName As String, geofence_Name As String, _
                                         EntryOrExitTime As Date, typ As DataObjects.AlertType.ActionType) As String

            'format: (has to be from no-reply@nanosoft.com.au)
            '04XXXXXXXX@app.wholesalesms.com.au / send.smsbroadcast.com.au
            'the content is the subject of the 

            Dim textListToEmail As String = String.Empty
            Dim textEmailFormat As String = "{0}@{1}"

            'get a list of "04XXXXXXXX@app.wholesalesms.com.au"
            Dim strList As List(Of String) = (From s In textList.Split(";").ToList _
                                               Where Not String.IsNullOrEmpty(s.Trim)
                                               Select String.Format(textEmailFormat, s.Trim, SMS_PROVIDER)).ToList

            For Each s As String In strList
                textListToEmail &= If(String.IsNullOrEmpty(textListToEmail), "", ";") & s
            Next

            Dim messageBody As String = String.Empty

            Try

                messageBody = My.Settings.EmailMessage
                Dim EntryOrExitTime_formatted As String = EntryOrExitTime.ToString("dd/MMM/yyyy HH:mm:ss")
                Dim enteredorexited As String = If(typ = DataObjects.AlertType.ActionType.Enters, "entered", "exited")
                Dim subject As String = String.Format(EmailContent, companyName, driverName, enteredorexited, geofence_Name, EntryOrExitTime_formatted)

                subject = subject.Replace(vbNewLine, ". ")

                SendEmail(textListToEmail, subject, subject)

            Catch ex As Exception
                Dim x As String = ex.Message
            End Try

            Return messageBody

        End Function
        ''' <summary>
        ''' Sends SMS messages using the paid-for SMS gateway
        ''' which converts emails to SMS's
        ''' </summary>
        ''' <param name="textList">a semi-colon seperated list of SMS numbers</param>       
        Public Shared Function CanbusSendSMS(textList As String, companyName As String, RecipientName As String, vehicleName As String, startTime As String, alertType As String, url As String) As String

            'format: (has to be from no-reply@nanosoft.com.au)
            '04XXXXXXXX@app.wholesalesms.com.au / send.smsbroadcast.com.au
            'the content is the subject of the 

            Dim textListToEmail As String = String.Empty
            Dim textEmailFormat As String = "{0}@{1}"

            'get a list of "04XXXXXXXX@app.wholesalesms.com.au"
            Dim strList As List(Of String) = (From s In textList.Split(";").ToList _
                                               Where Not String.IsNullOrEmpty(s.Trim)
                                               Select String.Format(textEmailFormat, s.Trim, SMS_PROVIDER)).ToList

            For Each s As String In strList
                textListToEmail &= If(String.IsNullOrEmpty(textListToEmail), "", ";") & s
            Next

            Dim messageBody As String = String.Empty

            Try
                messageBody = String.Format(CanbusAlertEmailContent, RecipientName, vehicleName, startTime, alertType, url)
                Dim subject = String.Format("Canbus alert from {0}.nanosoft.com.au", companyName)
                If textListToEmail.Contains("|") Then
                    SendEmail(textListToEmail.Split("|")(0), subject, messageBody)
                Else
                    SendEmail(textListToEmail, subject, messageBody)
                End If


            Catch ex As Exception
                Dim x As String = ex.Message
            End Try

            Return messageBody

        End Function

        Public Shared Function sendSMS(phoneNumber As String, message As String) As Boolean

            Dim emailAddress As String = String.Format("{0}@{1}", phoneNumber.Trim, SMS_PROVIDER)

            Return SendEmail(emailAddress, message, message)

        End Function

        Public Shared Function sendEmail(recipient As String, subject As String, content As String) As Boolean

            Dim wasSuccess As Boolean = False

            Try
                'Command line argument must the the SMTP host.
                With New SmtpClient()

                    .Port = 587
                    .Host = "smtp.zoho.com"
                    .EnableSsl = True
                    .Timeout = 10000
                    .DeliveryMethod = SmtpDeliveryMethod.Network
                    .UseDefaultCredentials = False
                    .Credentials = New System.Net.NetworkCredential("no-reply@nanosoft.com.au", "notastrongpassword")

                    Dim mm As New MailMessage()

                    mm.From = New MailAddress("no-reply@nanosoft.com.au")
                    mm.Subject = subject.Replace(vbNewLine, ". ")
                    mm.Body = content

                    For Each s As String In recipient.Split(";").ToList
                        mm.To.Add(New Net.Mail.MailAddress(s))
                    Next

                    mm.BodyEncoding = UTF8Encoding.UTF8
                    mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure

                    .Send(mm)
                End With

                wasSuccess = True

            Catch ex As Exception
                Dim message As String = ex.Message
            End Try

            Return wasSuccess

        End Function

    End Class

End Namespace