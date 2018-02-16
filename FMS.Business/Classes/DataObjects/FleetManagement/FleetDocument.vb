﻿Imports FMS.Business.DataObjects.FileMaintenance
Namespace DataObjects
    Public Class FleetDocument
#Region "Properties / enums"
        Public Property DocumentID As System.Guid
        Public Property Cid As System.Nullable(Of Integer)
        Public Property Rid As System.Nullable(Of Integer)
        Public Property Description As String
        Public Property PhotoBinary() As Byte()
        Public Property PhotoLocation As String
        Public Property CreatedDate As System.Nullable(Of Date)
#End Region
#Region "CRUD"
        Public Shared Sub Create(Document As DataObjects.FleetDocument)
            With New LINQtoSQLClassesDataContext
                Dim fleetDocument As New FMS.Business.FleetDocument
                With fleetDocument
                    .DocumentID = Guid.NewGuid
                    .Cid = Document.Cid
                    .Rid = Document.Rid
                    .Description = Document.Description
                    .PhotoLocation = SaveImageToFolder(Document.PhotoBinary, Document.Cid, Document.Rid)
                    .CreatedDate = Date.Now
                End With
                .FleetDocuments.InsertOnSubmit(fleetDocument)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(Document As DataObjects.FleetDocument)

            With New LINQtoSQLClassesDataContext
                Dim fleetDocument As FMS.Business.FleetDocument = (From i In .FleetDocuments
                                                                   Where i.DocumentID.Equals(Document.DocumentID)).SingleOrDefault
                With fleetDocument
                    .DocumentID = Document.DocumentID
                    .Cid = Document.Cid
                    .Rid = Document.Rid
                    .Description = Document.Description
                    .PhotoLocation = SaveImageToFolder(Document.PhotoBinary, Document.Cid, Document.Rid, fleetDocument.PhotoLocation)
                    .CreatedDate = Date.Now
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(Document As DataObjects.FleetDocument)
            Dim fleetDocument As FMS.Business.FleetDocument
            With New LINQtoSQLClassesDataContext
                fleetDocument = (From i In .FleetDocuments
                                 Where i.DocumentID.Equals(Document.DocumentID)).SingleOrDefault
                .FleetDocuments.DeleteOnSubmit(fleetDocument)
                .SubmitChanges()
                .Dispose()
            End With
            DeleteImageFile(fleetDocument.PhotoLocation)
        End Sub
        Public Shared Sub DeleteByRunID(RunID As Guid)
            With New LINQtoSQLClassesDataContext
                Dim fleetDocs = (From i In .FleetDocuments
                                 Where i.Rid.Equals(RunID)).ToList()
                For Each fleetDoc In fleetDocs
                    Dim fleetDocument As FMS.Business.FleetDocument = fleetDoc
                    .FleetDocuments.DeleteOnSubmit(fleetDocument)
                    .SubmitChanges()
                Next
                .Dispose()
            End With
        End Sub
        Public Shared Sub DeleteByClientID(ClientID As Guid)
            With New LINQtoSQLClassesDataContext
                Dim fleetDocs = (From i In .FleetDocuments
                                 Where i.Cid.Equals(ClientID)).ToList()
                For Each fleetDoc In fleetDocs
                    Dim fleetDocument As FMS.Business.FleetDocument = fleetDoc
                    .FleetDocuments.DeleteOnSubmit(fleetDoc)
                    .SubmitChanges()
                Next
                .Dispose()
            End With
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.FleetDocument)
            Try
                Dim fleetDocuments As New List(Of DataObjects.FleetDocument)

                With New LINQtoSQLClassesDataContext

                    fleetDocuments = (From i In .FleetDocuments
                                      Order By i.Description
                                      Select New DataObjects.FleetDocument(i)).ToList()
                    .Dispose()
                End With

                Return fleetDocuments
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetAllByClient(CID As Guid) As List(Of DataObjects.FleetDocument)
            Try
                Dim fleetDocuments As New List(Of DataObjects.FleetDocument)

                With New LINQtoSQLClassesDataContext
                    fleetDocuments = (From i In .FleetDocuments
                                      Order By i.Description
                                      Where i.Cid.Equals(CID)
                                      Select New DataObjects.FleetDocument(i)).ToList()
                    .Dispose()
                End With

                Return fleetDocuments
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetAllByClientCID(CID As Integer) As List(Of DataObjects.FleetDocument)
            Try
                Dim fleetDocuments As New List(Of DataObjects.FleetDocument)

                With New LINQtoSQLClassesDataContext
                    fleetDocuments = (From i In .FleetDocuments
                                      Order By i.Description
                                      Where i.Cid.Equals(CID)
                                      Select New DataObjects.FleetDocument(i)).ToList()
                    .Dispose()
                End With

                For Each fDoc In fleetDocuments
                    fDoc.PhotoBinary = imgToByteConverter(fDoc.PhotoLocation)
                Next

                Return fleetDocuments
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetAllByRun(RID As Guid) As List(Of DataObjects.FleetDocument)
            Try
                Dim fleetDocuments As New List(Of DataObjects.FleetDocument)

                With New LINQtoSQLClassesDataContext
                    fleetDocuments = (From i In .FleetDocuments
                                      Order By i.Description
                                      Where i.Rid.Equals(RID)
                                      Select New DataObjects.FleetDocument(i)).ToList()
                    .Dispose()
                End With
                Return fleetDocuments
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetAllByRunRID(RID As Integer) As List(Of DataObjects.FleetDocument)
            Try
                Dim fleetDocuments As New List(Of DataObjects.FleetDocument)

                With New LINQtoSQLClassesDataContext
                    fleetDocuments = (From i In .FleetDocuments
                                      Order By i.Description
                                      Where i.Rid.Equals(RID)
                                      Select New DataObjects.FleetDocument(i)).ToList()
                    .Dispose()
                End With

                For Each fDoc In fleetDocuments
                    fDoc.PhotoBinary = imgToByteConverter(fDoc.PhotoLocation)
                Next

                Return fleetDocuments
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objDocument As FMS.Business.FleetDocument)
            With objDocument
                Me.DocumentID = .DocumentID
                Me.Cid = .Cid
                Me.Rid = .Rid
                Me.Description = .Description
                Me.PhotoLocation = .PhotoLocation
                Me.CreatedDate = .CreatedDate
            End With
        End Sub
#End Region
    End Class
End Namespace

