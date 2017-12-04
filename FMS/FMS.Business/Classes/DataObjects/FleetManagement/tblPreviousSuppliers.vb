Namespace DataObjects
    Public Class tblPreviousSuppliers
#Region "Properties / enums"
        Public Property PreviousSupplierID As System.Guid
        Public Property Aid As Integer
        Public Property PreviousSupplier As String
        Public Property PreviousSupplierSortOrder As System.Nullable(Of Integer)
#End Region
#Region "CRUD"
        Public Shared Sub Create(PSupplier As DataObjects.tblPreviousSuppliers)
            Dim objTblPreviousSupplier As New FMS.Business.tblPreviousSupplier
            With objTblPreviousSupplier
                .PreviousSupplierID = Guid.NewGuid
                .Aid = tblProjectID.PreviousSupplierIDCreateOrUpdate()
                .PreviousSupplier = PSupplier.PreviousSupplier
            End With
            SingletonAccess.FMSDataContextContignous.tblPreviousSuppliers.InsertOnSubmit(objTblPreviousSupplier)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(PSupplier As DataObjects.tblPreviousSuppliers)
            Dim objPSupplier As FMS.Business.tblPreviousSupplier = (From c In SingletonAccess.FMSDataContextContignous.tblPreviousSuppliers
                                                           Where c.PreviousSupplierID.Equals(PSupplier.PreviousSupplierID)).SingleOrDefault
            With objPSupplier
                .PreviousSupplier = PSupplier.PreviousSupplier
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(PSupplier As DataObjects.tblPreviousSuppliers)
            Dim objPSupplier As FMS.Business.tblPreviousSupplier = (From c In SingletonAccess.FMSDataContextContignous.tblPreviousSuppliers
                                                         Where c.PreviousSupplierID.Equals(PSupplier.PreviousSupplierID)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblPreviousSuppliers.DeleteOnSubmit(objPSupplier)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblPreviousSuppliers)
            Dim objPSupplier = (From c In SingletonAccess.FMSDataContextContignous.tblPreviousSuppliers
                            Order By c.PreviousSupplier
                            Select New DataObjects.tblPreviousSuppliers(c)).ToList
            Return objPSupplier
        End Function
        Public Shared Function GetPreviousSupplierSortOrder(aID As Integer) As DataObjects.tblPreviousSuppliers
            Dim objPreviousSupplier = (From c In SingletonAccess.FMSDataContextContignous.usp_GetPreviousSupplier
                            Where c.Aid.Equals(aID)
                            Order By c.PreviousSupplier
                            Select New DataObjects.tblPreviousSuppliers() With {.Aid = c.Aid, .PreviousSupplier = c.PreviousSupplier,
                                                                             .PreviousSupplierID = c.PreviousSupplierID, .PreviousSupplierSortOrder = c.SortOrder}).SingleOrDefault
            Return objPreviousSupplier
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTblPreviousSupplier As FMS.Business.tblPreviousSupplier)
            With objTblPreviousSupplier
                Me.PreviousSupplierID = .PreviousSupplierID
                Me.Aid = .Aid
                Me.PreviousSupplier = .PreviousSupplier
                Me.PreviousSupplierSortOrder = 0
            End With
        End Sub
#End Region
    End Class    
End Namespace

