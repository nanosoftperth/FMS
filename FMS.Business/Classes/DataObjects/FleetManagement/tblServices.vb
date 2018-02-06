Namespace DataObjects
    Public Class tblServices
#Region "Properties / enums"
        Public Property ServicesID As System.Guid
        Public Property Sid As Integer
        Public Property ApplicationID As System.Nullable(Of System.Guid)
        Public Property ServiceCode As String
        Public Property ServiceDescription As String
        Public Property CostOfService As System.Nullable(Of Single)
#End Region
#Region "CRUD"
        Public Shared Sub Create(Services As DataObjects.tblServices)
            With New LINQtoSQLClassesDataContext
                Dim appID = ThisSession.ApplicationID
                Dim obj As New FMS.Business.tblService

                With obj
                    .ServicesID = Guid.NewGuid
                    .Sid = tblProjectID.ServicesIDCreateOrUpdate(Services.ApplicationID)
                    .ApplicationID = Services.ApplicationID
                    .ServiceCode = Services.ServiceCode
                    .ServiceDescription = Services.ServiceDescription
                    .CostOfService = Services.CostOfService
                End With

                .tblServices.InsertOnSubmit(obj)
                .SubmitChanges()
            End With

        End Sub
        Public Shared Sub Update(Services As DataObjects.tblServices)
            With New LINQtoSQLClassesDataContext
                Dim appID = ThisSession.ApplicationID
                Dim obj As FMS.Business.tblService = (From c In .tblServices
                                                      Where c.ServicesID.Equals(Services.ServicesID) And c.ApplicationID.Equals(Services.ApplicationID)).SingleOrDefault
                With obj
                    .ServiceCode = Services.ServiceCode
                    .ServiceDescription = Services.ServiceDescription
                    .CostOfService = Services.CostOfService
                End With
                .SubmitChanges()
                .Dispose()

            End With

        End Sub
        Public Shared Sub Delete(Services As DataObjects.tblServices)
            With New LINQtoSQLClassesDataContext
                Dim appID = ThisSession.ApplicationID
                Dim obj As FMS.Business.tblService = (From c In .tblServices
                                                      Where c.ServicesID.Equals(Services.ServicesID) And c.ApplicationID.Equals(Services.ApplicationID)).SingleOrDefault
                .tblServices.DeleteOnSubmit(obj)
                .SubmitChanges()
                .Dispose()
            End With

        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblServices)
            Try
                Dim obj As New List(Of DataObjects.tblServices)

                With New LINQtoSQLClassesDataContext
                    obj = (From c In .tblServices
                           Where Not c.ServiceCode Is Nothing And c.ApplicationID.Equals(ThisSession.ApplicationID)
                           Order By c.ServiceCode
                           Select New DataObjects.tblServices(c)).ToList
                    .Dispose()
                End With
                Return obj

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Shared Function GetAllByApplicationID(appID As System.Guid) As List(Of DataObjects.tblServices)
            Try
                Dim obj As New List(Of DataObjects.tblServices)
                With New LINQtoSQLClassesDataContext
                    obj = (From c In .tblServices
                           Where Not c.ServiceCode Is Nothing And c.ApplicationID.Equals(appID)
                           Order By c.ServiceCode
                           Select New DataObjects.tblServices(c)).ToList
                End With
                Return obj

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Shared Function GetAllWithNull() As List(Of DataObjects.tblServices)
            Try
                Dim obj As New List(Of DataObjects.tblServices)
                With New LINQtoSQLClassesDataContext
                    obj = (From c In .tblServices
                           Order By c.ServiceDescription
                           Select New DataObjects.tblServices(c)).ToList

                End With
                Return obj

            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objService As FMS.Business.tblService)
            With objService
                Me.ServicesID = .ServicesID
                Me.Sid = .Sid
                Me.ApplicationID = .ApplicationID
                Me.ServiceCode = .ServiceCode
                Me.ServiceDescription = .ServiceDescription
                Me.CostOfService = .CostOfService
            End With
        End Sub
#End Region
    End Class
End Namespace

