﻿Namespace DataObjects
    Public Class tblSalesPersons
#Region "Properties / enums"
        Public Property SalesPersonID As System.Guid
        Public Property Aid As Integer
        Public Property SalesPerson As String
        Public Property SalesPersonStartDate As System.Nullable(Of Date)
        Public Property SalesPersonComments As String
#End Region
#Region "CRUD"
        Public Shared Sub Create(sPerson As DataObjects.tblSalesPersons)
            Dim objSalesPerson As New FMS.Business.tblSalesPerson
            With objSalesPerson
                .SalesPersonID = Guid.NewGuid
                .Aid = tblProjectID.SalesPersonIDCreateOrUpdate()
                .SalesPerson = sPerson.SalesPerson
                .SalesPersonStartDate = sPerson.SalesPersonStartDate
                .SalesPersonComments = sPerson.SalesPersonComments
            End With
            SingletonAccess.FMSDataContextContignous.tblSalesPersons.InsertOnSubmit(objSalesPerson)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(sPerson As DataObjects.tblSalesPersons)
            Dim objSalesPerson As FMS.Business.tblSalesPerson = (From c In SingletonAccess.FMSDataContextContignous.tblSalesPersons
                                                           Where c.SalesPersonID.Equals(sPerson.SalesPersonID)).SingleOrDefault
            With objSalesPerson
                .SalesPerson = sPerson.SalesPerson
                .SalesPersonStartDate = sPerson.SalesPersonStartDate
                .SalesPersonComments = sPerson.SalesPersonComments
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(sPerson As DataObjects.tblSalesPersons)
            Dim objSalesPerson As FMS.Business.tblSalesPerson = (From c In SingletonAccess.FMSDataContextContignous.tblSalesPersons
                                                         Where c.SalesPersonID.Equals(sPerson.SalesPersonID)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblSalesPersons.DeleteOnSubmit(objSalesPerson)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblSalesPersons)
            Dim objSalesPerson = (From c In SingletonAccess.FMSDataContextContignous.tblSalesPersons
                                Order By c.SalesPerson
                                Select New DataObjects.tblSalesPersons(c)).ToList
            Return objSalesPerson
        End Function
        Public Shared Function GetSalesPersonSortOrder(aID As Integer) As DataObjects.tblSalesPersons
            Dim objSalesPerson = (From c In SingletonAccess.FMSDataContextContignous.tblSalesPersons
                                Where c.Aid.Equals(aID)
                                Order By c.SalesPerson
                                Select New DataObjects.tblSalesPersons(c)).SingleOrDefault
            Return objSalesPerson
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objSalesPerson As FMS.Business.tblSalesPerson)
            With objSalesPerson
                Me.SalesPersonID = .SalesPersonID
                Me.Aid = .Aid
                Me.SalesPerson = .SalesPerson
                Me.SalesPersonStartDate = .SalesPersonStartDate
                Me.SalesPersonComments = .SalesPersonComments
            End With
        End Sub
#End Region
    End Class
End Namespace
