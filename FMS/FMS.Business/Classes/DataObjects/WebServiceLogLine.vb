Namespace DataObjects

    Public Class WebServiceLogLine

        Public Property WebServiceLogID As Integer
        Public Property RequestMethod As String
        Public Property Login As String
        Public Property XMLRequest As String
        Public Property XMLResponse As String
        Public Property Company As String
        Public Property DateLogged As Date

        Public ReadOnly Property DateLoggedStr As String
            Get
                Return DateLogged.ToString("dd/MMM/yyyy HH:mm:ss")
            End Get
        End Property

        Public Sub New()

        End Sub

        Public Sub New(x As Business.WebServiceLog)

            With x
                Me.Company = .company
                Me.Login = .login
                Me.RequestMethod = .RequestMethod
                Me.WebServiceLogID = .WebServiceLog
                Me.XMLRequest = .XMLRequest
                Me.XMLResponse = .XMLResponse
                Me.DateLogged = .DateLogged
            End With

        End Sub

        Public Shared Function Create(company As String, _
                                      login As String, _
                                      requestMethod As String, _
                                      xmlRequest As String, _
                                      xmlResponse As String) As Integer


            Dim newdbobj As New Business.WebServiceLog

            With New LINQtoSQLClassesDataContext

                With newdbobj
                    .company = company
                    .login = login
                    .RequestMethod = requestMethod
                    .XMLRequest = xmlRequest
                    .XMLResponse = xmlResponse
                    .DateLogged = Now
                End With

                .WebServiceLogs.InsertOnSubmit(newdbobj)
                .SubmitChanges()

            End With

            Return newdbobj.WebServiceLog

        End Function


        Public Shared Function GetAll() As List(Of Business.DataObjects.WebServiceLogLine)

            Dim retobj As List(Of DataObjects.WebServiceLogLine)

            With New LINQtoSQLClassesDataContext

                retobj = .WebServiceLogs.Select(Function(x) New DataObjects.WebServiceLogLine(x)).ToList
                .Dispose()
            End With

            Return retobj

        End Function

    End Class


End Namespace