Namespace DataObjects
    Public Class tblCUAScheduleOfRates
#Region "Properties / enums"
        Public Property RatesId As System.Guid
        Public Property ApplicationID As System.Nullable(Of System.Guid)
        Public Property RatesAutoId As Integer
        Public Property Service As System.Nullable(Of Short)
        Public Property FromUnits As System.Nullable(Of Short)
        Public Property ToUnits As System.Nullable(Of Short)
        Public Property UnitPrice As System.Nullable(Of Single)
#End Region
#Region "CRUD"
        Public Shared Sub Create(CuaScheduleOfRates As DataObjects.tblCUAScheduleOfRates)
            With New LINQtoSQLClassesDataContext
                Dim tblCUAScheduleOfRate As New FMS.Business.tblCUAScheduleOfRate
                Dim AppID = ThisSession.ApplicationID
                With tblCUAScheduleOfRate
                    .RatesId = Guid.NewGuid
                    .ApplicationID = AppID
                    .RatesAutoId = tblProjectID.RatesIDCreateOrUpdate(AppID)
                    .Service = CuaScheduleOfRates.Service
                    .FromUnits = CuaScheduleOfRates.FromUnits
                    .ToUnits = CuaScheduleOfRates.ToUnits
                    .UnitPrice = CuaScheduleOfRates.UnitPrice
                End With
                .tblCUAScheduleOfRates.InsertOnSubmit(tblCUAScheduleOfRate)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(CuaScheduleOfRates As DataObjects.tblCUAScheduleOfRates)
            With New LINQtoSQLClassesDataContext
                Dim tblCUAScheduleOfRate As FMS.Business.tblCUAScheduleOfRate = (From i In .tblCUAScheduleOfRates
                                                                                 Where i.RatesAutoId.Equals(CuaScheduleOfRates.RatesAutoId)).SingleOrDefault
                With tblCUAScheduleOfRate
                    .Service = CuaScheduleOfRates.Service
                    .FromUnits = CuaScheduleOfRates.FromUnits
                    .ToUnits = CuaScheduleOfRates.ToUnits
                    .UnitPrice = CuaScheduleOfRates.UnitPrice
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(CuaScheduleOfRates As DataObjects.tblCUAScheduleOfRates)
            With New LINQtoSQLClassesDataContext
                Dim tblCUAScheduleOfRate As FMS.Business.tblCUAScheduleOfRate = (From i In .tblCUAScheduleOfRates
                                                                                 Where i.RatesAutoId.Equals(CuaScheduleOfRates.RatesAutoId)).SingleOrDefault
                .tblCUAScheduleOfRates.DeleteOnSubmit(tblCUAScheduleOfRate)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblCUAScheduleOfRates)
            Try
                Dim tblCUAScheduleOfRates As New List(Of DataObjects.tblCUAScheduleOfRates)

                With New LINQtoSQLClassesDataContext
                    tblCUAScheduleOfRates = (From i In .tblCUAScheduleOfRates
                                             Where i.ApplicationID.Equals(ThisSession.ApplicationID)
                                             Select New DataObjects.tblCUAScheduleOfRates(i)).ToList()
                    .Dispose()
                End With

                Return tblCUAScheduleOfRates

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetAllCustomer() As List(Of DataObjects.FleetClient)
            Try
                Dim fleetCustomers As New List(Of DataObjects.FleetClient)

                With New LINQtoSQLClassesDataContext

                    fleetCustomers = (From i In .tblCustomers
                                      Order By i.CustomerName
                                      Select New DataObjects.FleetClient() With {.Name = i.CustomerName, .Address = i.AddressLine1, .CustomerID = i.Cid}).ToList()
                    .Dispose()
                End With

                Return fleetCustomers
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objClient As FMS.Business.tblCUAScheduleOfRate)
            With objClient
                Me.RatesId = .RatesId
                Me.ApplicationID = .ApplicationID
                Me.RatesAutoId = .RatesAutoId
                Me.Service = .Service
                Me.FromUnits = .FromUnits
                Me.ToUnits = .ToUnits
                Me.UnitPrice = .UnitPrice
            End With
        End Sub
#End Region
    End Class
End Namespace

