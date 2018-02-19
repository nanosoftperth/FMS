Namespace DataObjects
    Public Class tblRunSite
#Region "Properties / enums"
        Public Property RunSiteID As System.Guid
        Public Property Rid As System.Nullable(Of Integer)
        Public Property Cid As System.Nullable(Of Integer)
        Public Property ApplicationID As System.Nullable(Of System.Guid)
#End Region
#Region "CRUD"
        Public Shared Sub Create(Run As DataObjects.tblRunSite)
            With New LINQtoSQLClassesDataContext
                Dim RunSite As New FMS.Business.tblRunSite
                With RunSite
                    .RunSiteID = Guid.NewGuid
                    .Rid = Run.Rid
                    .Cid = Run.Cid
                    .ApplicationID = ThisSession.ApplicationID
                End With
                .tblRunSites.InsertOnSubmit(RunSite)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(Run As DataObjects.tblRunSite)
            With New LINQtoSQLClassesDataContext
                Dim RunSite As FMS.Business.tblRunSite = (From i In .tblRunSites
                                                          Where i.RunSiteID.Equals(Run.RunSiteID) And i.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                With RunSite
                    .Rid = Run.Rid
                    .Cid = Run.Cid
                    .ApplicationID = ThisSession.ApplicationID
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(Run As DataObjects.tblRunSite)
            With New LINQtoSQLClassesDataContext
                Dim RunSite As FMS.Business.tblRunSite = (From i In .tblRunSites
                                                          Where i.RunSiteID.Equals(Run.RunSiteID) And i.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                .tblRunSites.DeleteOnSubmit(RunSite)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblRunSite)
            Try
                Dim RunSite As New List(Of DataObjects.tblRunSite)

                With New LINQtoSQLClassesDataContext

                    RunSite = (From i In .tblRunSites
                               Where i.ApplicationID.Equals(ThisSession.ApplicationID)
                               Select New DataObjects.tblRunSite(i)).ToList
                    .Dispose()
                End With

                Return RunSite

            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetRunSiteByRidCid(ByVal Rid As Integer, ByVal Cid As Integer) As DataObjects.tblRunSite
            Try
                Dim RunSite As New DataObjects.tblRunSite

                With New LINQtoSQLClassesDataContext

                    RunSite = (From i In .tblRunSites
                               Where i.Rid.Equals(Rid) And i.Cid.Equals(Cid) And i.ApplicationID.Equals(ThisSession.ApplicationID)
                               Select New DataObjects.tblRunSite(i)).FirstOrDefault
                    .Dispose()
                End With

                Return RunSite

            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub

        Public Sub New(objFleetRun As FMS.Business.tblRunSite)
            With objFleetRun
                Me.RunSiteID = .RunSiteID
                Me.Rid = .Rid
                Me.Cid = .Cid
                Me.ApplicationID = .ApplicationID
            End With
        End Sub
#End Region
    End Class
End Namespace

