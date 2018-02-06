Namespace DataObjects
    Public Class tbZone
#Region "Properties / enums"
        Public Property ZoneID As System.Guid
        Public Property Aid As Integer
        Public Property ApplicationID As System.Nullable(Of System.Guid)
        Public Property AreaDescription As String
#End Region
#Region "CRUD"
        Public Shared Sub Create(Zone As DataObjects.tbZone)
            With New LINQtoSQLClassesDataContext
                Dim objZone As New FMS.Business.tbZone

                With objZone
                    .ZoneID = Guid.NewGuid
                    .Aid = tblProjectID.ZoneIDCreateOrUpdate(Zone.ApplicationID)
                    .ApplicationID = Zone.ApplicationID
                    .AreaDescription = Zone.AreaDescription
                End With

                .tbZones.InsertOnSubmit(objZone)
                .SubmitChanges()
                .Dispose()

            End With

        End Sub
        Public Shared Sub Update(Zone As DataObjects.tbZone)
            With New LINQtoSQLClassesDataContext
                Dim objZone As FMS.Business.tbZone = (From c In .tbZones
                                                      Where c.ZoneID.Equals(Zone.ZoneID) And c.ApplicationID.Equals(Zone.ApplicationID)).SingleOrDefault
                With objZone
                    .AreaDescription = Zone.AreaDescription
                End With

                .SubmitChanges()
                .Dispose()

            End With

        End Sub
        Public Shared Sub Delete(Zone As DataObjects.tbZone)
            With New LINQtoSQLClassesDataContext
                Dim objZone As FMS.Business.tbZone = (From c In .tbZones
                                                      Where c.ZoneID.Equals(Zone.ZoneID) And c.ApplicationID.Equals(Zone.ApplicationID)).SingleOrDefault

                .tbZones.DeleteOnSubmit(objZone)
                .SubmitChanges()
                .Dispose()
            End With

        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tbZone)
            Try
                Dim objZones As New List(Of DataObjects.tbZone)

                With New LINQtoSQLClassesDataContext
                    objZones = (From c In .tbZones
                                Order By c.AreaDescription
                                Select New DataObjects.tbZone(c)).ToList
                    .Dispose()
                End With

                Return objZones

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Shared Function GetAllByApplicationID(appID As System.Guid) As List(Of DataObjects.tbZone)
            Try
                Dim objZones As New List(Of DataObjects.tbZone)
                With New LINQtoSQLClassesDataContext
                    objZones = (From c In .tbZones
                                Where c.ApplicationID.Equals(appID)
                                Order By c.AreaDescription
                                Select New DataObjects.tbZone(c)).ToList
                    .Dispose()
                End With
                Return objZones

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Shared Function GetZoneSortOrder(zID As Integer) As DataObjects.tbZone
            Try
                Dim objZones As New DataObjects.tbZone
                With New LINQtoSQLClassesDataContext
                    objZones = (From c In .tbZones
                                Where c.Aid.Equals(zID)
                                Order By c.AreaDescription
                                Select New DataObjects.tbZone(c)).ToList().SingleOrDefault
                    .Dispose()
                End With
                Return objZones

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Shared Function GetAllForRevenueReport() As List(Of DataObjects.tbZone)
            Try
                Dim appId = ThisSession.ApplicationID
                Dim objZones As New List(Of DataObjects.tbZone)
                With New LINQtoSQLClassesDataContext
                    objZones = (From c In .tbZones
                                Where c.ApplicationID = appId
                                Order By c.AreaDescription
                                Select New DataObjects.tbZone(c)).ToList
                    .Dispose()
                End With
                Return objZones

            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTbZone As FMS.Business.tbZone)
            With objTbZone
                Me.ZoneID = .ZoneID
                Me.Aid = .Aid
                Me.ApplicationID = .ApplicationID
                Me.AreaDescription = .AreaDescription
            End With
        End Sub
#End Region
    End Class
End Namespace

