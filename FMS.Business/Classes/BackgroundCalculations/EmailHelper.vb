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
        Public Const BookingEmailContent As String = _
        "dear {0}, " & vbNewLine & vbNewLine & _
        "Your driver {1} is {2} away from your location now. They are driving a {3}." & vbNewLine & _
        "Their number is {4}, if you wish to contact them. " & vbNewLine & _
        "Thank you," & vbNewLine & vbNewLine & _
        "Nanosoft Automated Services"

        Public Const EmailContentForForgotPassword As String = _
        "Alert from {0}.nanosoft.com.au, " & vbNewLine & vbNewLine & _
        "You can change your password by clicking the link below : " & vbNewLine & _
        "{1}" & vbNewLine & _
        "The link will expire after 24 hours."

        Public Const EmailContentForNewUser As String = _
        "Alert from {0}.nanosoft.com.au, " & vbNewLine & vbNewLine & _
        "Your account has been successfully created. " & vbNewLine & _
        "Username : {1}" & vbNewLine & _
        "Password : {2}"

        Friend Shared Function SendEmail(emailList As String, companyName As String, _
                                         driverName As String, geofence_Name As String, _
                                         EntryOrExitTime As Date, typ As DataObjects.AlertType.ActionType) As String


            Dim messageBody As String = String.Empty

            Try
                messageBody = My.Settings.EmailMessage
                Dim EntryOrExitTime_formatted As String = EntryOrExitTime.ToString("dd/MMM/yyyy HH:mm:ss")
                Dim enteredorexited As String = If(typ = DataObjects.AlertType.ActionType.Enters, "entered", "exited")
                Dim subject  = String.Format("geo-fence alert from {0}.nanosoft.com.au", companyName)
                
                messageBody = String.Format(EmailContent, companyName, driverName, enteredorexited, geofence_Name, EntryOrExitTime_formatted)



                SendEmail(emailList, subject, messageBody)
            Catch ex As Exception
                Dim x As String = ex.Message
            End Try

            Return messageBody

        End Function

        'BY RYAN: Email for booking!
        Friend Shared Function SendEmail(emailList As String, companyName As String, _
                                  recipient As String, driverName As String, distance As String, cartype As String, number As String) As String


            Dim messageBody As String = String.Empty

            Try
                messageBody = My.Settings.EmailMessage
                Dim subject As String


                subject = String.Format("booking alert from {0}.nanosoft.com.au", companyName)

                messageBody = String.Format(BookingEmailContent, recipient, driverName, distance, cartype, number)



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
                Dim subject As String = String.Format("request change password alert from {0}.nanosoft.com.au", companyName)
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
                Dim subject As String = String.Format("new user alert from {0}.nanosoft.com.au", companyName)
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
                    .Credentials = New System.Net.NetworkCredential("dave@nanosoft.com.au", "Placeb0!")

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