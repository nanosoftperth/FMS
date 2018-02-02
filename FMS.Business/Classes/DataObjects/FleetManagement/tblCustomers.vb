Namespace DataObjects
    Public Class tblCustomers
#Region "Properties / enums"
        Public Property ApplicationId As System.Guid
        Public Property CustomerID As System.Guid
        Public Property Cid As Integer
        Public Property CustomerName As String
        Public Property AddressLine1 As String
        Public Property AddressLine2 As String
        Public Property State As System.Nullable(Of Integer)
        Public Property Suburb As String
        Public Property PostCode As String
        Public Property CustomerContactName As String
        Public Property CustomerPhone As String
        Public Property CustomerMobile As String
        Public Property CustomerFax As String
        Public Property CustomerComments As String
        Public Property CustomerAgent As System.Nullable(Of Integer)
        Public Property AgentSortOrder As System.Nullable(Of Integer)
        Public Property CustomerRating As System.Nullable(Of Short)
        Public Property Zone As System.Nullable(Of Integer)
        Public Property ZoneSortOrder As System.Nullable(Of Integer)
        Public Property MYOBCustomerNumber As String
        Public Property CustomerValue As System.Nullable(Of Double)
        Public Property InactiveCustomer As Boolean
        Public Property CustomerCommencementDate As System.Nullable(Of Date)
        Public Property chkCustomerExcludeFuelLevy As Boolean
        Public Property cmbRateIncrease As System.Nullable(Of Short)
        Public Property RateIncreaseSortOrder As System.Nullable(Of Integer)
        Public Property CustomerSortOrder As System.Nullable(Of Integer)
#End Region
#Region "CRUD"
        Public Shared Sub Create(customer As DataObjects.tblCustomers)
            With New LINQtoSQLClassesDataContext
                Dim appID = ThisSession.ApplicationID

                Dim objCustomer As New FMS.Business.tblCustomer
                With objCustomer
                    .ApplicationID = appID
                    .CustomerID = Guid.NewGuid
                    .Cid = tblProjectID.CustomerIDCreateOrUpdate(appID)
                    .CustomerName = customer.CustomerName
                    .AddressLine1 = customer.AddressLine1
                    .AddressLine2 = customer.AddressLine2
                    .State = customer.State
                    .Suburb = customer.Suburb
                    .PostCode = customer.PostCode
                    .CustomerContactName = customer.CustomerContactName
                    .CustomerPhone = customer.CustomerPhone
                    .CustomerMobile = customer.CustomerMobile
                    .CustomerFax = customer.CustomerFax
                    .CustomerComments = customer.CustomerComments
                    .CustomerAgent = customer.CustomerAgent
                    .CustomerRating = customer.CustomerRating
                    .Zone = customer.Zone
                    .MYOBCustomerNumber = customer.MYOBCustomerNumber
                    .CustomerValue = customer.CustomerValue
                    .InactiveCustomer = customer.InactiveCustomer
                    .CustomerCommencementDate = customer.CustomerCommencementDate
                    .chkCustomerExcludeFuelLevy = customer.chkCustomerExcludeFuelLevy
                    .cmbRateIncrease = customer.cmbRateIncrease
                End With

                .tblCustomers.InsertOnSubmit(objCustomer)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(customer As DataObjects.tblCustomers)
            With New LINQtoSQLClassesDataContext
                Dim objCustomer As FMS.Business.tblCustomer = (From c In .tblCustomers
                                                               Where c.Cid.Equals(customer.Cid) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                With objCustomer
                    .CustomerName = customer.CustomerName
                    .AddressLine1 = customer.AddressLine1
                    .AddressLine2 = customer.AddressLine2
                    .State = customer.State
                    .Suburb = customer.Suburb
                    .PostCode = customer.PostCode
                    .CustomerContactName = customer.CustomerContactName
                    .CustomerPhone = customer.CustomerPhone
                    .CustomerMobile = customer.CustomerMobile
                    .CustomerFax = customer.CustomerFax
                    .CustomerComments = customer.CustomerComments
                    .CustomerAgent = customer.CustomerAgent
                    .CustomerRating = customer.CustomerRating
                    .Zone = customer.Zone
                    .MYOBCustomerNumber = customer.MYOBCustomerNumber
                    .CustomerValue = customer.CustomerValue
                    .InactiveCustomer = customer.InactiveCustomer
                    .CustomerCommencementDate = customer.CustomerCommencementDate
                    .chkCustomerExcludeFuelLevy = customer.chkCustomerExcludeFuelLevy
                    .cmbRateIncrease = customer.cmbRateIncrease
                End With

                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub UpdateCustomerValue(cid As Integer, custVal As Double)
            With New LINQtoSQLClassesDataContext
                Dim objCustomer As FMS.Business.tblCustomer = (From c In .tblCustomers
                                                               Where c.Cid.Equals(cid) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                With objCustomer
                    .CustomerValue = custVal
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(Customer As DataObjects.tblCustomers)
            With New LINQtoSQLClassesDataContext
                Dim objCustomer As FMS.Business.tblCustomer = (From c In .tblCustomers
                                                               Where c.Cid.Equals(Customer.Cid) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                .tblCustomers.DeleteOnSubmit(objCustomer)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblCustomers)
            Try
                Dim objCustomer As New List(Of DataObjects.tblCustomers)
                With New LINQtoSQLClassesDataContext
                    objCustomer = (From c In .tblCustomers
                                   Where c.ApplicationID.Equals(ThisSession.ApplicationID)
                                   Select New DataObjects.tblCustomers(c)).ToList
                    .Dispose()
                End With
                Return objCustomer
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetAllOrderByCustomerName() As List(Of DataObjects.tblCustomers)
            Try
                Dim objCustomer As New List(Of DataObjects.tblCustomers)
                With New LINQtoSQLClassesDataContext
                    objCustomer = (From c In .tblCustomers
                                   Where c.ApplicationID.Equals(ThisSession.ApplicationID)
                                   Order By c.CustomerName
                                   Select New DataObjects.tblCustomers(c)).ToList
                    .Dispose()
                End With
                Return objCustomer
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function GetAllWithZoneSortOrder() As List(Of DataObjects.tblCustomers)
            Try
                Dim objCustomer As New List(Of DataObjects.tblCustomers)
                With New LINQtoSQLClassesDataContext
                    objCustomer = (From c In .usp_GetCustomers
                                   Select New DataObjects.tblCustomers() With {.AddressLine1 = c.AddressLine1, .AddressLine2 = c.AddressLine2,
                                                                                .chkCustomerExcludeFuelLevy = c.chkCustomerExcludeFuelLevy, .Cid = c.Cid,
                                                                                .cmbRateIncrease = c.cmbRateIncrease, .CustomerAgent = c.CustomerAgent,
                                                                                .CustomerCommencementDate = c.CustomerCommencementDate, .CustomerComments = c.CustomerComments,
                                                                                .CustomerContactName = c.CustomerContactName, .CustomerFax = c.CustomerFax,
                                                                                .CustomerID = c.CustomerID, .CustomerMobile = c.CustomerMobile,
                                                                                .CustomerName = c.CustomerName, .CustomerPhone = c.CustomerPhone,
                                                                                .CustomerRating = c.CustomerRating, .CustomerValue = c.CustomerValue,
                                                                                .InactiveCustomer = c.InactiveCustomer, .MYOBCustomerNumber = c.MYOBCustomerNumber,
                                                                                .PostCode = c.PostCode, .State = c.State, .Suburb = c.Suburb, .Zone = c.Zone,
                                                                                .ZoneSortOrder = c.ZoneSortOrder, .AgentSortOrder = c.AgentSortOrder,
                                                                                .RateIncreaseSortOrder = c.RateIncreaseSortOrder}).ToList

                    .Dispose()
                End With
                Return objCustomer
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function GetACustomerByCID(cid As Integer) As DataObjects.tblCustomers
            Try
                Dim objCustomer As DataObjects.tblCustomers
                With New LINQtoSQLClassesDataContext
                    objCustomer = (From c In .usp_GetCustomers
                                   Where c.Cid.Equals(cid)
                                   Select New DataObjects.tblCustomers() With {.AddressLine1 = c.AddressLine1, .AddressLine2 = c.AddressLine2,
                                                                               .chkCustomerExcludeFuelLevy = c.chkCustomerExcludeFuelLevy, .Cid = c.Cid,
                                                                               .cmbRateIncrease = c.cmbRateIncrease, .CustomerAgent = c.CustomerAgent,
                                                                               .CustomerCommencementDate = c.CustomerCommencementDate, .CustomerComments = c.CustomerComments,
                                                                               .CustomerContactName = c.CustomerContactName, .CustomerFax = c.CustomerFax,
                                                                               .CustomerID = c.CustomerID, .CustomerMobile = c.CustomerMobile,
                                                                               .CustomerName = c.CustomerName, .CustomerPhone = c.CustomerPhone,
                                                                               .CustomerRating = c.CustomerRating, .CustomerValue = c.CustomerValue,
                                                                               .InactiveCustomer = c.InactiveCustomer, .MYOBCustomerNumber = c.MYOBCustomerNumber,
                                                                               .PostCode = c.PostCode, .State = c.State, .Suburb = c.Suburb, .Zone = c.Zone,
                                                                               .ZoneSortOrder = c.ZoneSortOrder, .AgentSortOrder = c.AgentSortOrder,
                                                                               .RateIncreaseSortOrder = c.RateIncreaseSortOrder, .CustomerSortOrder = c.CustomerSortOrder}).SingleOrDefault
                    .Dispose()
                End With
                Return objCustomer
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function GetMYOBCustomer() As List(Of DataObjects.tblCustomers)
            Try
                Dim objCustomer As New List(Of DataObjects.tblCustomers)
                With New LINQtoSQLClassesDataContext
                    objCustomer = (From c In SingletonAccess.FMSDataContextContignous.tblCustomers
                                   Where c.MYOBCustomerNumber Is Nothing And c.ApplicationID.Equals(ThisSession.ApplicationID)
                                   Select New DataObjects.tblCustomers(c)).ToList

                    .Dispose()
                End With
                Return objCustomer
            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTblCustomer As FMS.Business.tblCustomer)
            With objTblCustomer
                Me.CustomerID = .CustomerID
                Me.Cid = .Cid
                Me.CustomerName = .CustomerName
                Me.CustomerSortOrder = 0
                Me.AddressLine1 = .AddressLine1
                Me.AddressLine2 = .AddressLine2
                Me.State = .State
                Me.Suburb = .Suburb
                Me.PostCode = .PostCode
                Me.CustomerContactName = .CustomerContactName
                Me.CustomerPhone = .CustomerPhone
                Me.CustomerMobile = .CustomerMobile
                Me.CustomerFax = .CustomerFax
                Me.CustomerComments = .CustomerComments
                Me.CustomerAgent = .CustomerAgent
                Me.AgentSortOrder = 0
                Me.CustomerRating = .CustomerRating
                Me.Zone = .Zone
                Me.ZoneSortOrder = 0
                Me.MYOBCustomerNumber = .MYOBCustomerNumber
                Me.CustomerValue = .CustomerValue
                Me.InactiveCustomer = .InactiveCustomer
                Me.CustomerCommencementDate = .CustomerCommencementDate
                Me.chkCustomerExcludeFuelLevy = .chkCustomerExcludeFuelLevy
                Me.cmbRateIncrease = .cmbRateIncrease
                Me.RateIncreaseSortOrder = 0
            End With
        End Sub
#End Region
    End Class
End Namespace

