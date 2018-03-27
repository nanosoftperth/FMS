Namespace DataObjects
    Public Class tblPreviousSuppliers
#Region "Properties / enums"
        Public Property PreviousSupplierID As System.Guid
        Public Property ApplicationID As System.Nullable(Of System.Guid)
        Public Property Aid As Integer
        Public Property PreviousSupplier As String
        Public Property PreviousSupplierSortOrder As System.Nullable(Of Integer)
#End Region
#Region "CRUD"
        Public Shared Sub Create(PSupplier As DataObjects.tblPreviousSuppliers)
            With New LINQtoSQLClassesDataContext
                Dim objTblPreviousSupplier As New FMS.Business.tblPreviousSupplier
                Dim appId = ThisSession.ApplicationID
                With objTblPreviousSupplier
                    .PreviousSupplierID = Guid.NewGuid
                    .ApplicationID = appId
                    .Aid = tblProjectID.PreviousSupplierIDCreateOrUpdate(appId)
                    .PreviousSupplier = PSupplier.PreviousSupplier
                End With
                .tblPreviousSuppliers.InsertOnSubmit(objTblPreviousSupplier)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(PSupplier As DataObjects.tblPreviousSuppliers)
            With New LINQtoSQLClassesDataContext
                Dim objPSupplier As FMS.Business.tblPreviousSupplier = (From c In .tblPreviousSuppliers
                                                                        Where c.PreviousSupplierID.Equals(PSupplier.PreviousSupplierID) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                With objPSupplier
                    .PreviousSupplier = PSupplier.PreviousSupplier
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(PSupplier As DataObjects.tblPreviousSuppliers)
            With New LINQtoSQLClassesDataContext
                Dim objPSupplier As FMS.Business.tblPreviousSupplier = (From c In .tblPreviousSuppliers
                                                                        Where c.PreviousSupplierID.Equals(PSupplier.PreviousSupplierID) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                .tblPreviousSuppliers.DeleteOnSubmit(objPSupplier)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblPreviousSuppliers)
            Try
                Dim objPSupplier As New List(Of DataObjects.tblPreviousSuppliers)
                With New LINQtoSQLClassesDataContext
                    objPSupplier = (From c In .tblPreviousSuppliers
                                    Where c.ApplicationID.Equals(ThisSession.ApplicationID)
                                    Order By c.PreviousSupplier
                                    Select New DataObjects.tblPreviousSuppliers(c)).ToList
                    .Dispose()
                End With
                Return objPSupplier
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetPreviousSupplierSortOrder(aID As Integer) As DataObjects.tblPreviousSuppliers
            Try
                Dim objPreviousSupplier As New DataObjects.tblPreviousSuppliers
                With New LINQtoSQLClassesDataContext
                    objPreviousSupplier = (From c In .usp_GetPreviousSupplier(ThisSession.ApplicationID)
                                           Where c.Aid.Equals(aID)
                                           Order By c.PreviousSupplier
                                           Select New DataObjects.tblPreviousSuppliers() With {.Aid = c.Aid, .PreviousSupplier = c.PreviousSupplier,
                                                                                                .PreviousSupplierID = c.PreviousSupplierID, .PreviousSupplierSortOrder = c.SortOrder}).SingleOrDefault
                    .Dispose()
                End With
                Return objPreviousSupplier
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTblPreviousSupplier As FMS.Business.tblPreviousSupplier)
            With objTblPreviousSupplier
                Me.PreviousSupplierID = .PreviousSupplierID
                Me.ApplicationID = .ApplicationID
                Me.Aid = .Aid
                Me.PreviousSupplier = .PreviousSupplier
                Me.PreviousSupplierSortOrder = 0
            End With
        End Sub
#End Region
    End Class    
End Namespace

