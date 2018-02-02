Namespace DataObjects
    Public Class tblSiteReSignDetails
#Region "Properties / enums"
        Public Property ResignHistoryID As System.Guid
        Public Property ApplicationID As System.Nullable(Of System.Guid)
        Public Property Cid As Integer
        Public Property SiteCId As System.Nullable(Of Integer)
        Public Property ReSignDate As System.Nullable(Of Date)
        Public Property ReSignPeriod As System.Nullable(Of Short)
        Public Property ServiceAgreementNo As String
        Public Property SalesPerson As System.Nullable(Of Short)
        Public Property ContractExpiryDate As System.Nullable(Of Date)
#End Region
#Region "CRUD"
        Public Shared Sub Create(SiteReSignDetails As DataObjects.tblSiteReSignDetails)
            With New LINQtoSQLClassesDataContext
                Dim objSiteReSignDetails As New FMS.Business.tblSiteReSignDetail
                Dim appId = ThisSession.ApplicationID
                With objSiteReSignDetails
                    .ResignHistoryID = Guid.NewGuid
                    .ApplicationID = appId
                    .Cid = tblProjectID.SiteReSignDetailsIDCreateOrUpdate(appId)
                    .SiteCId = SiteReSignDetails.SiteCId
                    .ReSignDate = SiteReSignDetails.ReSignDate
                    .ReSignPeriod = SiteReSignDetails.ReSignPeriod
                    .ServiceAgreementNo = SiteReSignDetails.ServiceAgreementNo
                    .SalesPerson = SiteReSignDetails.SalesPerson
                    .ContractExpiryDate = SiteReSignDetails.ContractExpiryDate
                End With
                .tblSiteReSignDetails.InsertOnSubmit(objSiteReSignDetails)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(SiteReSignDetails As DataObjects.tblSiteReSignDetails)
            With New LINQtoSQLClassesDataContext
                Dim objSiteReSignDetails As FMS.Business.tblSiteReSignDetail = (From c In .tblSiteReSignDetails
                                                                                Where c.ResignHistoryID.Equals(SiteReSignDetails.ResignHistoryID) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                With objSiteReSignDetails
                    .SiteCId = SiteReSignDetails.SiteCId
                    .ReSignDate = SiteReSignDetails.ReSignDate
                    .ReSignPeriod = SiteReSignDetails.ReSignPeriod
                    .ServiceAgreementNo = SiteReSignDetails.ServiceAgreementNo
                    .SalesPerson = SiteReSignDetails.SalesPerson
                    .ContractExpiryDate = SiteReSignDetails.ContractExpiryDate
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(SiteReSignDetails As DataObjects.tblSiteReSignDetails)
            With New LINQtoSQLClassesDataContext
                Dim objSiteReSignDetails As FMS.Business.tblSiteReSignDetail = (From c In .tblSiteReSignDetails
                                                                                Where c.ResignHistoryID.Equals(SiteReSignDetails.ResignHistoryID) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault

                .tblSiteReSignDetails.DeleteOnSubmit(objSiteReSignDetails)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblSiteReSignDetails)
            Try
                Dim objSiteResignDetails As New List(Of DataObjects.tblSiteReSignDetails)
                With New LINQtoSQLClassesDataContext
                    objSiteResignDetails = (From c In .tblSiteReSignDetails
                                            Where c.ApplicationID.Equals(ThisSession.ApplicationID)
                                            Order By c.ReSignDate
                                            Select New DataObjects.tblSiteReSignDetails(c)).ToList
                    .Dispose()
                End With
                Return objSiteResignDetails
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetAllSiteID(siteID As Integer) As List(Of DataObjects.tblSiteReSignDetails)
            Try
                Dim objSiteResignDetails As New List(Of DataObjects.tblSiteReSignDetails)
                With New LINQtoSQLClassesDataContext
                    objSiteResignDetails = (From c In .tblSiteReSignDetails
                                            Where c.SiteCId.Equals(siteID) And c.ApplicationID.Equals(ThisSession.ApplicationID)
                                            Order By c.ReSignDate
                                            Select New DataObjects.tblSiteReSignDetails(c)).ToList
                    .Dispose()
                End With
                Return objSiteResignDetails
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objSiteReSignDetail As FMS.Business.tblSiteReSignDetail)
            With objSiteReSignDetail
                Me.ResignHistoryID = .ResignHistoryID
                Me.ApplicationID = .ApplicationID
                Me.Cid = .Cid
                Me.SiteCId = .SiteCId
                Me.ReSignDate = .ReSignDate
                Me.ReSignPeriod = .ReSignPeriod
                Me.ServiceAgreementNo = .ServiceAgreementNo
                Me.SalesPerson = .SalesPerson
                Me.ContractExpiryDate = .ContractExpiryDate
            End With
        End Sub
#End Region
    End Class
End Namespace

