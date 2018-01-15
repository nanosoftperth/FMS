Namespace DataObjects
    Public Class tblRunFortnightlyCycle
#Region "Properties / enums"
        Public Property FortnightlyCyclesID As System.Guid
        Public Property ApplicationID As System.Nullable(Of System.Guid)
        Public Property Aid As Integer
        Public Property CycleDescription As String
        Public Property FortnightlyCyclesSortOrder As System.Nullable(Of Integer)
#End Region
#Region "CRUD"
        Public Shared Sub Create(FNCycles As DataObjects.tblRunFortnightlyCycle)
            Dim objFNCycles As New FMS.Business.tblRunFortnightlyCycle
            Dim appId = ThisSession.ApplicationID
            With objFNCycles
                .FortnightlyCyclesID = Guid.NewGuid
                .ApplicationID = appId
                .Aid = tblProjectID.FortnightlyCyclesIDCreateOrUpdate(appId)
                .CycleDescription = FNCycles.CycleDescription
            End With
            SingletonAccess.FMSDataContextContignous.tblRunFortnightlyCycles.InsertOnSubmit(objFNCycles)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(FNCycles As DataObjects.tblRunFortnightlyCycle)
            Dim objFNCycles As FMS.Business.tblRunFortnightlyCycle = (From c In SingletonAccess.FMSDataContextContignous.tblRunFortnightlyCycles
                                                                      Where c.FortnightlyCyclesID.Equals(FNCycles.FortnightlyCyclesID) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
            With objFNCycles
                .CycleDescription = FNCycles.CycleDescription
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(FNCycles As DataObjects.tblRunFortnightlyCycle)
            Dim objFNCycles As FMS.Business.tblRunFortnightlyCycle = (From c In SingletonAccess.FMSDataContextContignous.tblRunFortnightlyCycles
                                                                      Where c.FortnightlyCyclesID.Equals(FNCycles.FortnightlyCyclesID) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblRunFortnightlyCycles.DeleteOnSubmit(objFNCycles)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblRunFortnightlyCycle)
            Dim objFNCycles = (From c In SingletonAccess.FMSDataContextContignous.tblRunFortnightlyCycles
                               Where c.ApplicationID.Equals(ThisSession.ApplicationID)
                               Order By c.CycleDescription
                               Select New DataObjects.tblRunFortnightlyCycle(c)).ToList
            Return objFNCycles
        End Function
        Public Shared Function GetPreviousSupplierSortOrder(aID As Integer) As DataObjects.tblRunFortnightlyCycle
            Dim objFNCycles = (From c In SingletonAccess.FMSDataContextContignous.usp_GetFortNightlyCycles(ThisSession.ApplicationID)
                               Where c.Aid.Equals(aID)
                               Order By c.CycleDescription
                               Select New DataObjects.tblRunFortnightlyCycle() With {.Aid = c.Aid, .CycleDescription = c.CycleDescription,
                                                                             .FortnightlyCyclesID = c.FortnightlyCyclesID, .FortnightlyCyclesSortOrder = c.SortOrder}).SingleOrDefault
            Return objFNCycles
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objFNCycle As FMS.Business.tblRunFortnightlyCycle)
            With objFNCycle
                Me.FortnightlyCyclesID = .FortnightlyCyclesID
                Me.ApplicationID = .ApplicationID
                Me.Aid = .Aid
                Me.CycleDescription = .CycleDescription
                Me.FortnightlyCyclesSortOrder = 0
            End With
        End Sub
#End Region
    End Class
End Namespace

