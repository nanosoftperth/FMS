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
            Dim objRateIncreaseReference As New FMS.Business.tblRateIncreaseReference
            With objRateIncreaseReference
                .RateIncreaseID = Guid.NewGuid
                .Aid = tblProjectID.RateIncreaseIDCreateOrUpdate(RateIncreaseReference.ApplicationID)
                .ApplicationID = RateIncreaseReference.ApplicationID
                .RateIncreaseDescription = RateIncreaseReference.RateIncreaseDescription
                .AnnualIncreaseApplies = RateIncreaseReference.AnnualIncreaseApplies
                .AlreadyDoneThisYear = RateIncreaseReference.AlreadyDoneThisYear
            End With
            SingletonAccess.FMSDataContextContignous.tblRateIncreaseReferences.InsertOnSubmit(objRateIncreaseReference)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(RateIncreaseReference As DataObjects.tblRateIncreaseReference)
            Dim objRateIncreaseReference As FMS.Business.tblRateIncreaseReference = (From c In SingletonAccess.FMSDataContextContignous.tblRateIncreaseReferences
                                                           Where c.RateIncreaseID.Equals(RateIncreaseReference.RateIncreaseID) And c.ApplicationID.Equals(RateIncreaseReference.ApplicationID)).SingleOrDefault
            With objRateIncreaseReference
                .RateIncreaseDescription = RateIncreaseReference.RateIncreaseDescription
                .AnnualIncreaseApplies = RateIncreaseReference.AnnualIncreaseApplies
                .AlreadyDoneThisYear = RateIncreaseReference.AlreadyDoneThisYear
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(RateIncreaseReference As DataObjects.tblRateIncreaseReference)
            Dim objRateIncreaseReference As FMS.Business.tblRateIncreaseReference = (From c In SingletonAccess.FMSDataContextContignous.tblRateIncreaseReferences
                                                         Where c.RateIncreaseID.Equals(RateIncreaseReference.RateIncreaseID) And c.ApplicationID.Equals(RateIncreaseReference.ApplicationID)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblRateIncreaseReferences.DeleteOnSubmit(objRateIncreaseReference)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblRateIncreaseReference)
            Dim objRateIncreaseReference = (From c In SingletonAccess.FMSDataContextContignous.tblRateIncreaseReferences
                                            Order By c.RateIncreaseDescription
                                          Select New DataObjects.tblRateIncreaseReference(c)).ToList
            Return objRateIncreaseReference
        End Function
        Public Shared Function GetAllByApplicationID(appID As System.Guid) As List(Of DataObjects.tblRateIncreaseReference)
            Dim objRateIncreaseReference = (From c In SingletonAccess.FMSDataContextContignous.tblRateIncreaseReferences
                                            Where c.ApplicationID.Equals(appID)
                                            Order By c.RateIncreaseDescription
                                          Select New DataObjects.tblRateIncreaseReference(c)).ToList
            Return objRateIncreaseReference
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

