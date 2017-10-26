Namespace DataObjects
    Public Class tblCIRHistory
#Region "Properties / enums"
        Public Property HistoryID As System.Guid
        Public Property NCId As Integer
        Public Property Cid As System.Nullable(Of Short)
        Public Property NCRDate As System.Nullable(Of Date)
        Public Property NCRNumber As System.Nullable(Of Integer)
        Public Property NCRDescription As String
        Public Property NCRRecordedBY As String
        Public Property NCRClosedBy As String
        Public Property Driver As System.Nullable(Of Short)
        Public Property DriverSortOrder As System.Nullable(Of Integer)
        Public Property NCRReason As System.Nullable(Of Short)
        Public Property NCRReasonSortOrder As System.Nullable(Of Integer)
#End Region
#Region "CRUD"
        Public Shared Sub Create(History As DataObjects.tblCIRHistory)
            Dim objHistory As New FMS.Business.tblCIRHistory
            With objHistory
                .HistoryID = Guid.NewGuid
                .Cid = History.Cid
                .NCRDate = History.NCRDate
                .NCRNumber = History.NCRNumber
                .NCRDescription = History.NCRDescription
                .NCRRecordedBY = History.NCRRecordedBY
                .NCRClosedBy = History.NCRClosedBy
                .Driver = History.Driver
                .NCRReason = History.NCRReason
            End With
            SingletonAccess.FMSDataContextContignous.tblCIRHistories.InsertOnSubmit(objHistory)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(History As DataObjects.tblCIRHistory)
            Dim objHistory As FMS.Business.tblCIRHistory = (From c In SingletonAccess.FMSDataContextContignous.tblCIRHistories
                                                           Where c.HistoryID.Equals(History.HistoryID)).SingleOrDefault
            With objHistory
                .Cid = History.Cid
                .NCRDate = History.NCRDate
                .NCRNumber = History.NCRNumber
                .NCRDescription = History.NCRDescription
                .NCRRecordedBY = History.NCRRecordedBY
                .NCRClosedBy = History.NCRClosedBy
                .Driver = History.Driver
                .NCRReason = History.NCRReason
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(History As DataObjects.tblCIRHistory)
            Dim objHistory As FMS.Business.tblCIRHistory = (From c In SingletonAccess.FMSDataContextContignous.tblCIRHistories
                                                         Where c.HistoryID.Equals(History.HistoryID)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblCIRHistories.DeleteOnSubmit(objHistory)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblCIRHistory)
            Dim objHistory = (From c In SingletonAccess.FMSDataContextContignous.tblCIRHistories
                            Order By c.NCRDescription
                            Select New DataObjects.tblCIRHistory(c)).ToList
            Return objHistory
        End Function
        Public Shared Function GetAllByCID(cid As Integer) As List(Of DataObjects.tblCIRHistory)
            Dim objHistory = (From c In SingletonAccess.FMSDataContextContignous.tblCIRHistories
                              Where c.Cid.Equals(cid)
                            Order By c.NCRDescription
                            Select New DataObjects.tblCIRHistory(c)).ToList
            Return objHistory
        End Function
        Public Shared Function GetAllByCIDWithSortOrder(cid As Short) As List(Of DataObjects.tblCIRHistory)
            Dim objHistory = (From c In SingletonAccess.FMSDataContextContignous.usp_GetCIRHistory
                              Where c.Cid.Equals(cid)
                            Order By c.NCRDescription
                            Select New DataObjects.tblCIRHistory() With {.HistoryID = c.HistoryID, .NCId = c.NCId, .Cid = c.Cid,
                                                                         .NCRDate = c.NCRDate, .NCRNumber = c.NCRNumber, .NCRDescription = c.NCRDescription,
                                                                         .NCRRecordedBY = c.NCRRecordedBY, .NCRClosedBy = c.NCRClosedBy, .Driver = c.Driver,
                                                                         .DriverSortOrder = c.DriverSortOrder, .NCRReason = c.NCRReason,
                                                                         .NCRReasonSortOrder = c.NCRReasonSortOrder}).ToList
            Return objHistory
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objHistory As FMS.Business.tblCIRHistory)
            With objHistory
                Me.HistoryID = .HistoryID
                Me.NCId = .NCId
                Me.Cid = .Cid
                Me.NCRDate = .NCRDate
                Me.NCRNumber = .NCRNumber
                Me.NCRDescription = .NCRDescription
                Me.NCRRecordedBY = .NCRRecordedBY
                Me.NCRClosedBy = .NCRClosedBy
                Me.Driver = .Driver
                Me.DriverSortOrder = 0
                Me.NCRReason = .NCRReason
                Me.NCRReasonSortOrder = 0
            End With
        End Sub
#End Region
    End Class
End Namespace

