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
                    .Service = ThisSession.ServiceID
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
                                                                                 Where i.RatesAutoId.Equals(CuaScheduleOfRates.RatesAutoId) And i.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                With tblCUAScheduleOfRate
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
                                                                                 Where i.RatesAutoId.Equals(CuaScheduleOfRates.RatesAutoId) And i.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
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
        Public Shared Function GetCUAScheduleOfRatesByService(service As Integer) As List(Of DataObjects.tblCUAScheduleOfRates)
            Try
                Dim tblCUAScheduleOfRates As New List(Of DataObjects.tblCUAScheduleOfRates)

                With New LINQtoSQLClassesDataContext
                    tblCUAScheduleOfRates = (From i In .tblCUAScheduleOfRates
                                             Where i.Service.Equals(service) And i.ApplicationID.Equals(ThisSession.ApplicationID)
                                             Select New DataObjects.tblCUAScheduleOfRates(i)).ToList()
                    .Dispose()
                End With

                Return tblCUAScheduleOfRates

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetCUAScheduleOfRatesByServiceOrderbyFromUnits(service As Integer) As List(Of DataObjects.tblCUAScheduleOfRates)
            Try
                Dim tblCUAScheduleOfRates As New List(Of DataObjects.tblCUAScheduleOfRates)

                With New LINQtoSQLClassesDataContext
                    tblCUAScheduleOfRates = (From i In .tblCUAScheduleOfRates
                                             Where i.Service.Equals(service) And i.ApplicationID.Equals(ThisSession.ApplicationID)
                                             Order By i.FromUnits
                                             Select New DataObjects.tblCUAScheduleOfRates(i)).ToList()
                    .Dispose()
                End With

                Return tblCUAScheduleOfRates

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetServiceIdInt(ServiceId As Guid) As Integer
            Try
                Return FMS.Business.DataObjects.tblServices.GetServiceBySid(ServiceId)
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

