Namespace DataObjects
        Public Class tblIndustryGroups
#Region "Properties / enums"
        Public Property IndustryID As System.Guid
        Public Property Aid As Integer
        Public Property IndustryDescription As String
        Public Property IndustrySortOrder As System.Nullable(Of Integer)
#End Region
#Region "CRUD"
        Public Shared Sub Create(IndustryGroup As DataObjects.tblIndustryGroups)
            Dim objIndustryGroup As New FMS.Business.tblIndustryGroup
            With objIndustryGroup
                .IndustryID = Guid.NewGuid
                .Aid = tblProjectID.IndustryGroupIDCreateOrUpdate()
                .IndustryDescription = IndustryGroup.IndustryDescription
            End With
            SingletonAccess.FMSDataContextContignous.tblIndustryGroups.InsertOnSubmit(objIndustryGroup)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(IndustryGroup As DataObjects.tblIndustryGroups)
            Dim objIndustryGroup As FMS.Business.tblIndustryGroup = (From c In SingletonAccess.FMSDataContextContignous.tblIndustryGroups
                                                           Where c.IndustryID.Equals(IndustryGroup.IndustryID)).SingleOrDefault
            With objIndustryGroup
                .IndustryDescription = IndustryGroup.IndustryDescription
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(IndustryGroup As DataObjects.tblIndustryGroups)
            Dim objIndustryGroup As FMS.Business.tblIndustryGroup = (From c In SingletonAccess.FMSDataContextContignous.tblIndustryGroups
                                                         Where c.IndustryID.Equals(IndustryGroup.IndustryID)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblIndustryGroups.DeleteOnSubmit(objIndustryGroup)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblIndustryGroups)
            Dim objIndustryGroups = (From c In SingletonAccess.FMSDataContextContignous.tblIndustryGroups
                            Order By c.IndustryDescription
                            Select New DataObjects.tblIndustryGroups(c)).ToList
            Return objIndustryGroups
        End Function
        Public Shared Function GetIndustryGroupSortOrder(aID As Integer) As DataObjects.tblIndustryGroups
            Dim objIndustryGroup = (From c In SingletonAccess.FMSDataContextContignous.usp_GetIndustryGroup
                            Where c.Aid.Equals(aID)
                            Order By c.IndustryDescription
                            Select New DataObjects.tblIndustryGroups() With {.Aid = c.Aid, .IndustryDescription = c.IndustryDescription,
                                                                             .IndustryID = c.IndustryID, .IndustrySortOrder = c.SortOrder}).SingleOrDefault
            Return objIndustryGroup
        End Function

#End Region
#Region "Constructors"
            Public Sub New()

            End Sub
        Public Sub New(objTblIndustryGroup As FMS.Business.tblIndustryGroup)
            With objTblIndustryGroup
                Me.IndustryID = .IndustryID
                Me.Aid = .Aid
                Me.IndustryDescription = .IndustryDescription
                Me.IndustrySortOrder = 0
            End With
        End Sub
#End Region
        End Class
    End Namespace

