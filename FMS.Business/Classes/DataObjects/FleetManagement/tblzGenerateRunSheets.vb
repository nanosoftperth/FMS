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
#End Region
#Region "CRUD"
        Public Shared Sub Create(GenerateRunSheets As DataObjects.tblzGenerateRunSheets)
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
            End With
            SingletonAccess.FMSDataContextContignous.tblzGenerateRunSheets.InsertOnSubmit(objGenerateRunSheet)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(GenerateRunSheets As DataObjects.tblzGenerateRunSheets)
            Dim objGenerateRunSheet As FMS.Business.tblzGenerateRunSheet = (From c In SingletonAccess.FMSDataContextContignous.tblzGenerateRunSheets
                                                           Where c.RunSheetId.Equals(GenerateRunSheets.RunSheetId)).SingleOrDefault
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
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(GenerateRunSheets As DataObjects.tblzGenerateRunSheets)
            Dim objGenerateRunSheets As FMS.Business.tblzGenerateRunSheet = (From c In SingletonAccess.FMSDataContextContignous.tblzGenerateRunSheets
                                                         Where c.RunSheetId.Equals(GenerateRunSheets.RunSheetId)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblzGenerateRunSheets.DeleteOnSubmit(objGenerateRunSheets)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub DeleteAllGenerateRunSheets()
            Dim objGenerateRunSheets = (From c In SingletonAccess.FMSDataContextContignous.tblzGenerateRunSheets
                            Select c).ToList()
            SingletonAccess.FMSDataContextContignous.tblzGenerateRunSheets.DeleteAllOnSubmit(objGenerateRunSheets)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblzGenerateRunSheets)
            Dim objGenerateRunSheets = (From c In SingletonAccess.FMSDataContextContignous.tblzGenerateRunSheets
                            Select New DataObjects.tblzGenerateRunSheets(c)).ToList
            Return objGenerateRunSheets
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
            End With
        End Sub
#End Region
    End Class
End Namespace

