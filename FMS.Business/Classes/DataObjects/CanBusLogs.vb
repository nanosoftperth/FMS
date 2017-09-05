Namespace DataObjects
    Public Class CanBusLogs
#Region "PROPERTIES"
        Public Property CanLogId As System.Guid
        Public Property DeviceId As String
        Public Property PGN As Double
        Public Property SPN As Double
        Public Property Standard As String
        Public Property DateLog As Date
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(obj As FMS.Business.CanBusLog)
            With obj
                Me.CanLogId = .CanLogId
                Me.DeviceId = .DeviceId
                Me.PGN = .PGN
                Me.SPN = .SPN
                Me.Standard = .Standard
                Me.DateLog = .DateLog
            End With
        End Sub
#End Region
#Region "CRUD"
        Public Shared Sub Create(CbLogs As DataObjects.CanBusLogs)
            Dim canLogs As New FMS.Business.CanBusLog
            Try
                With canLogs
                    .CanLogId = Guid.NewGuid
                    .DeviceId = CbLogs.DeviceId
                    .PGN = CbLogs.PGN
                    .SPN = CbLogs.SPN
                    .Standard = CbLogs.Standard
                    .DateLog = CbLogs.DateLog
                End With
                SingletonAccess.FMSDataContextContignous.CanBusLogs.InsertOnSubmit(canLogs)
                SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Catch ex As Exception
            End Try
        End Sub
#End Region

#Region "GET Methods"
        Public Shared Function GetLastTimeValidData(_deviceId As String, _pgn As Double, _spn As Double, _standard As String) As DateTime
            Dim dtLastValid As DateTime
            Try
                Dim getLastValidDate = (From logs In SingletonAccess.FMSDataContextNew.CanBusLogs
                                   Where logs.DeviceId.Equals(_deviceId) And logs.SPN.Equals(_spn) And logs.Standard.Equals(_standard) And logs.PGN.Equals(_pgn)
                                   Group logs By logs.DeviceId, logs.PGN, logs.SPN, logs.Standard Into g = Group
                                   Order By g.Max(Function(orderDateLog) orderDateLog.DateLog)
                                   Select New DataObjects.CanBusLogs() With {.DateLog = g.Max(Function(dtLog) dtLog.DateLog)}).SingleOrDefault
                If getLastValidDate Is Nothing Then
                    dtLastValid = DateTime.Now
                Else
                    dtLastValid = getLastValidDate.DateLog
                End If
            Catch ex As Exception
                Return DateTime.Now
            End Try

            Return dtLastValid
        End Function
#End Region
    End Class
End Namespace

