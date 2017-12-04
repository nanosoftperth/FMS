Namespace DataObjects
    Public Class tblRevenueChangeReason
#Region "Properties / enums"
        Public Property RevenueChangeReasonID As System.Guid
        Public Property Rid As Integer
        Public Property RevenueChangeReason As String
        Public Property RevenueChangeReasonSortOrder As System.Nullable(Of Integer)
#End Region
#Region "CRUD"
        Public Shared Sub Create(RCR As DataObjects.tblRevenueChangeReason)
            Dim objRCR As New FMS.Business.tblRevenueChangeReason
            With objRCR
                .RevenueChangeReasonID = Guid.NewGuid
                .Rid = tblProjectID.RevenueChangeReasonIDCreateOrUpdate()
                .RevenueChangeReason = RCR.RevenueChangeReason
            End With
            SingletonAccess.FMSDataContextContignous.tblRevenueChangeReasons.InsertOnSubmit(objRCR)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(RCR As DataObjects.tblRevenueChangeReason)
            Dim objRCR As FMS.Business.tblRevenueChangeReason = (From c In SingletonAccess.FMSDataContextContignous.tblRevenueChangeReasons
                                                           Where c.RevenueChangeReasonID.Equals(RCR.RevenueChangeReasonID)).SingleOrDefault
            With objRCR
                .RevenueChangeReason = RCR.RevenueChangeReason
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(RCR As DataObjects.tblRevenueChangeReason)
            Dim objRCR As FMS.Business.tblRevenueChangeReason = (From c In SingletonAccess.FMSDataContextContignous.tblRevenueChangeReasons
                                                         Where c.RevenueChangeReasonID.Equals(RCR.RevenueChangeReasonID)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblRevenueChangeReasons.DeleteOnSubmit(objRCR)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblRevenueChangeReason)
            Dim objRCR = (From c In SingletonAccess.FMSDataContextContignous.tblRevenueChangeReasons
                            Order By c.RevenueChangeReason
                            Select New DataObjects.tblRevenueChangeReason(c)).ToList
            Return objRCR
        End Function
        Public Shared Function GetPreviousSupplierSortOrder(Rid As Integer) As DataObjects.tblRevenueChangeReason
            Dim objRCR = (From c In SingletonAccess.FMSDataContextContignous.usp_GetRevenueChangeReason
                            Where c.Rid.Equals(Rid)
                            Order By c.RevenueChangeReason
                            Select New DataObjects.tblRevenueChangeReason() With {.Rid = c.Rid, .RevenueChangeReason = c.RevenueChangeReason,
                                                                             .RevenueChangeReasonID = c.RevenueChangeReasonID, .RevenueChangeReasonSortOrder = c.SortOrder}).SingleOrDefault
            Return objRCR
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objRevenueChangeReason As FMS.Business.tblRevenueChangeReason)
            With objRevenueChangeReason
                Me.RevenueChangeReasonID = .RevenueChangeReasonID
                Me.Rid = .Rid
                Me.RevenueChangeReason = .RevenueChangeReason
                Me.RevenueChangeReasonSortOrder = 0
            End With
        End Sub
#End Region
    End Class
End Namespace

