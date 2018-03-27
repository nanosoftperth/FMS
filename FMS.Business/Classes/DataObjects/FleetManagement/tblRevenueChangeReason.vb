Namespace DataObjects
    Public Class tblRevenueChangeReason
#Region "Properties / enums"
        Public Property RevenueChangeReasonID As System.Guid
        Public Property ApplicationID As System.Nullable(Of System.Guid)
        Public Property Rid As Integer
        Public Property RevenueChangeReason As String
        Public Property RevenueChangeReasonSortOrder As System.Nullable(Of Integer)
#End Region
#Region "CRUD"
        Public Shared Sub Create(RCR As DataObjects.tblRevenueChangeReason)
            With New LINQtoSQLClassesDataContext
                Dim objRCR As New FMS.Business.tblRevenueChangeReason
                Dim appId = ThisSession.ApplicationID
                With objRCR
                    .RevenueChangeReasonID = Guid.NewGuid
                    .ApplicationID = appId
                    .Rid = tblProjectID.RevenueChangeReasonIDCreateOrUpdate(appId)
                    .RevenueChangeReason = RCR.RevenueChangeReason
                End With
                .tblRevenueChangeReasons.InsertOnSubmit(objRCR)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(RCR As DataObjects.tblRevenueChangeReason)
            With New LINQtoSQLClassesDataContext
                Dim objRCR As FMS.Business.tblRevenueChangeReason = (From c In .tblRevenueChangeReasons
                                                                     Where c.RevenueChangeReasonID.Equals(RCR.RevenueChangeReasonID) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                With objRCR
                    .RevenueChangeReason = RCR.RevenueChangeReason
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(RCR As DataObjects.tblRevenueChangeReason)
            With New LINQtoSQLClassesDataContext
                Dim objRCR As FMS.Business.tblRevenueChangeReason = (From c In .tblRevenueChangeReasons
                                                                     Where c.RevenueChangeReasonID.Equals(RCR.RevenueChangeReasonID) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                .tblRevenueChangeReasons.DeleteOnSubmit(objRCR)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblRevenueChangeReason)
            Try
                Dim objRCR As New List(Of DataObjects.tblRevenueChangeReason)
                With New LINQtoSQLClassesDataContext
                    objRCR = (From c In .tblRevenueChangeReasons
                              Where c.ApplicationID.Equals(ThisSession.ApplicationID)
                              Order By c.RevenueChangeReason
                              Select New DataObjects.tblRevenueChangeReason(c)).ToList
                    .Dispose()
                End With
                Return objRCR
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetRevenueChangeReasonSortOrder(Rid As Integer) As DataObjects.tblRevenueChangeReason
            Try
                Dim objRCR As New DataObjects.tblRevenueChangeReason
                With New LINQtoSQLClassesDataContext
                    objRCR = (From c In .usp_GetRevenueChangeReason(ThisSession.ApplicationID)
                              Where c.Rid.Equals(Rid)
                              Order By c.RevenueChangeReason
                              Select New DataObjects.tblRevenueChangeReason() With {.Rid = c.Rid, .RevenueChangeReason = c.RevenueChangeReason,
                                                                                 .RevenueChangeReasonID = c.RevenueChangeReasonID, .RevenueChangeReasonSortOrder = c.SortOrder}).SingleOrDefault
                    .Dispose()
                End With
                Return objRCR
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objRevenueChangeReason As FMS.Business.tblRevenueChangeReason)
            With objRevenueChangeReason
                Me.RevenueChangeReasonID = .RevenueChangeReasonID
                Me.ApplicationID = .ApplicationID
                Me.Rid = .Rid
                Me.RevenueChangeReason = .RevenueChangeReason
                Me.RevenueChangeReasonSortOrder = 0
            End With
        End Sub
#End Region
    End Class
End Namespace

