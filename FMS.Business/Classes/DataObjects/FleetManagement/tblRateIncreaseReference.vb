Namespace DataObjects
    Public Class tblRateIncreaseReference
#Region "Properties / enums"
        Public Property RateIncreaseID As System.Guid
        Public Property Aid As Integer
        Public Property ApplicationID As System.Nullable(Of System.Guid)
        Public Property RateIncreaseDescription As String
        Public Property AnnualIncreaseApplies As Boolean
        Public Property AlreadyDoneThisYear As Boolean
#End Region
#Region "CRUD"
        Public Shared Sub Create(RateIncreaseReference As DataObjects.tblRateIncreaseReference)
            With New LINQtoSQLClassesDataContext
                Dim objRateIncreaseReference As New FMS.Business.tblRateIncreaseReference
                With objRateIncreaseReference
                    .RateIncreaseID = Guid.NewGuid
                    .Aid = tblProjectID.RateIncreaseIDCreateOrUpdate(RateIncreaseReference.ApplicationID)
                    .ApplicationID = RateIncreaseReference.ApplicationID
                    .RateIncreaseDescription = RateIncreaseReference.RateIncreaseDescription
                    .AnnualIncreaseApplies = RateIncreaseReference.AnnualIncreaseApplies
                    .AlreadyDoneThisYear = RateIncreaseReference.AlreadyDoneThisYear
                End With
                .tblRateIncreaseReferences.InsertOnSubmit(objRateIncreaseReference)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(RateIncreaseReference As DataObjects.tblRateIncreaseReference)
            With New LINQtoSQLClassesDataContext
                Dim objRateIncreaseReference As FMS.Business.tblRateIncreaseReference = (From c In .tblRateIncreaseReferences
                                                                                         Where c.RateIncreaseID.Equals(RateIncreaseReference.RateIncreaseID) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                With objRateIncreaseReference
                    .RateIncreaseDescription = RateIncreaseReference.RateIncreaseDescription
                    .AnnualIncreaseApplies = RateIncreaseReference.AnnualIncreaseApplies
                    .AlreadyDoneThisYear = RateIncreaseReference.AlreadyDoneThisYear
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(RateIncreaseReference As DataObjects.tblRateIncreaseReference)
            With New LINQtoSQLClassesDataContext
                Dim objRateIncreaseReference As FMS.Business.tblRateIncreaseReference = (From c In .tblRateIncreaseReferences
                                                                                         Where c.RateIncreaseID.Equals(RateIncreaseReference.RateIncreaseID) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                .tblRateIncreaseReferences.DeleteOnSubmit(objRateIncreaseReference)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblRateIncreaseReference)
            Try
                Dim objRateIncreaseReference As New List(Of DataObjects.tblRateIncreaseReference)
                With New LINQtoSQLClassesDataContext
                    objRateIncreaseReference = (From c In .tblRateIncreaseReferences
                                                Where c.ApplicationID.Equals(ThisSession.ApplicationID)
                                                Order By c.RateIncreaseDescription
                                                Select New DataObjects.tblRateIncreaseReference(c)).ToList
                    .Dispose()
                End With
                Return objRateIncreaseReference
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTblRateIncreaseReference As FMS.Business.tblRateIncreaseReference)
            With objTblRateIncreaseReference
                Me.RateIncreaseID = .RateIncreaseID
                Me.Aid = .Aid
                Me.ApplicationID = .ApplicationID
                Me.RateIncreaseDescription = .RateIncreaseDescription
                Me.AnnualIncreaseApplies = .AnnualIncreaseApplies
                Me.AlreadyDoneThisYear = .AlreadyDoneThisYear
            End With
        End Sub
#End Region
    End Class
End Namespace

