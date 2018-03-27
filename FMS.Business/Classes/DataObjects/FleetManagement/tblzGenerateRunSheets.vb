Namespace DataObjects
    Public Class tblzGenerateRunSheets
#Region "Properties / enums"
        Public Property RunSheetId As System.Guid
        Public Property Aid As Integer
        Public Property RunNumber As System.Nullable(Of Integer)
        Public Property RunDriver As System.Nullable(Of Integer)
        Public Property RunDescription As String
        Public Property Cid As System.Nullable(Of Integer)
        Public Property CSid As System.Nullable(Of Integer)
        Public Property ServiceUnits As System.Nullable(Of Double)
        Public Property ProductId As System.Nullable(Of Integer)
        Public Property ServiceComments As String
        Public Property SortOrder As String
        Public Property ApplicationID As System.Nullable(Of System.Guid)
#End Region
#Region "CRUD"
        Public Shared Sub Create(GenerateRunSheets As DataObjects.tblzGenerateRunSheets)
            With New LINQtoSQLClassesDataContext
                Dim objGenerateRunSheet As New FMS.Business.tblzGenerateRunSheet
                With objGenerateRunSheet
                    .RunSheetId = Guid.NewGuid
                    .Aid = tblProjectID.RunSheetIDCreateOrUpdate()
                    .RunNumber = GenerateRunSheets.RunNumber
                    .RunDriver = GenerateRunSheets.RunDriver
                    .RunDescription = GenerateRunSheets.RunDescription
                    .Cid = GenerateRunSheets.Cid
                    .CSid = GenerateRunSheets.CSid
                    .ServiceUnits = GenerateRunSheets.ServiceUnits
                    .ProductId = GenerateRunSheets.ProductId
                    .ServiceComments = GenerateRunSheets.ServiceComments
                    .SortOrder = GenerateRunSheets.SortOrder
                    .ApplicationID = ThisSession.ApplicationID
                End With
                .tblzGenerateRunSheets.InsertOnSubmit(objGenerateRunSheet)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(GenerateRunSheets As DataObjects.tblzGenerateRunSheets)
            With New LINQtoSQLClassesDataContext
                Dim objGenerateRunSheet As FMS.Business.tblzGenerateRunSheet = (From c In .tblzGenerateRunSheets
                                                                                Where c.RunSheetId.Equals(GenerateRunSheets.RunSheetId) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                With objGenerateRunSheet
                    .RunNumber = GenerateRunSheets.RunNumber
                    .RunDriver = GenerateRunSheets.RunDriver
                    .RunDescription = GenerateRunSheets.RunDescription
                    .Cid = GenerateRunSheets.Cid
                    .CSid = GenerateRunSheets.CSid
                    .ServiceUnits = GenerateRunSheets.ServiceUnits
                    .ProductId = GenerateRunSheets.ProductId
                    .ServiceComments = GenerateRunSheets.ServiceComments
                    .SortOrder = GenerateRunSheets.SortOrder
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(GenerateRunSheets As DataObjects.tblzGenerateRunSheets)
            With New LINQtoSQLClassesDataContext
                Dim objGenerateRunSheets As FMS.Business.tblzGenerateRunSheet = (From c In .tblzGenerateRunSheets
                                                                                 Where c.RunSheetId.Equals(GenerateRunSheets.RunSheetId) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                .tblzGenerateRunSheets.DeleteOnSubmit(objGenerateRunSheets)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub DeleteAllGenerateRunSheets()
            With New LINQtoSQLClassesDataContext
                Dim objGenerateRunSheets = (From c In .tblzGenerateRunSheets
                                            Where c.ApplicationID.Equals(ThisSession.ApplicationID)
                                            Select c).ToList()
                .tblzGenerateRunSheets.DeleteAllOnSubmit(objGenerateRunSheets)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblzGenerateRunSheets)
            Try
                Dim objGenerateRunSheets As New List(Of DataObjects.tblzGenerateRunSheets)
                With New LINQtoSQLClassesDataContext
                    objGenerateRunSheets = (From c In .tblzGenerateRunSheets
                                            Where c.ApplicationID.Equals(ThisSession.ApplicationID)
                                            Select New DataObjects.tblzGenerateRunSheets(c)).ToList
                    .Dispose()
                End With

                Return objGenerateRunSheets
            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTblzGenerateRunSheet As FMS.Business.tblzGenerateRunSheet)
            With objTblzGenerateRunSheet
                Me.RunSheetId = .RunSheetId
                Me.Aid = .Aid
                Me.RunNumber = .RunNumber
                Me.RunDriver = .RunDriver
                Me.RunDescription = .RunDescription
                Me.Cid = .Cid
                Me.CSid = CSid
                Me.ServiceUnits = .ServiceUnits
                Me.ProductId = .ProductId
                Me.ServiceComments = .ServiceComments
                Me.SortOrder = .SortOrder
                Me.ApplicationID = .ApplicationID
            End With
        End Sub
#End Region
    End Class
End Namespace

