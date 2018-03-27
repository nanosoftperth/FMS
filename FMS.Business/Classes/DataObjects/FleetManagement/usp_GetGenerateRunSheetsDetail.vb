Namespace DataObjects
    Public Class usp_GetGenerateRunSheetsDetail
#Region "Properties / enums"
        Public Property SortOrder As String
        Public Property Cid As System.Nullable(Of Integer)
        Public Property SiteName As String
        Public Property Add As String
        Public Property Suburb As String
        Public Property SiteContactName As String
        Public Property SiteContactPhone As String
        Public Property SiteContactMobile As String
        Public Property RunDriver As System.Nullable(Of Integer)
        Public Property GeneralSiteServiceComments As String
        Public Property RunNumber As System.Nullable(Of Integer)
        Public Property DriverName As String
        Public Property RunDescription As String
        Public Property Notes As String
#End Region
#Region "Get methods"
        Public Shared Function GetGenerateRunSheetsDetail() As List(Of DataObjects.usp_GetGenerateRunSheetsDetail)
            Try
                Dim objGeneralRunSheets As New List(Of DataObjects.usp_GetGenerateRunSheetsDetail)
                With New LINQtoSQLClassesDataContext
                    objGeneralRunSheets = (From c In .usp_GetGenerateRunSheetsDetail
                                           Select New DataObjects.usp_GetGenerateRunSheetsDetail(c)).ToList
                    .Dispose()
                End With
                Return objGeneralRunSheets
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objGenerateRunSheetsDetail As FMS.Business.usp_GetGenerateRunSheetsDetailResult)
            With objGenerateRunSheetsDetail
                Me.SortOrder = .SortOrder
                Me.Cid = .Cid
                Me.SiteName = .SiteName
                Me.Add = .Add
                Me.Suburb = .Suburb
                Me.SiteContactName = .SiteContactName
                Me.SiteContactPhone = .SiteContactPhone
                Me.SiteContactMobile = .SiteContactMobile
                Me.RunDriver = .RunDriver
                Me.GeneralSiteServiceComments = .GeneralSiteServiceComments
                Me.RunNumber = .RunNumber
                Me.DriverName = .DriverName
                Me.RunDescription = .RunDescription
                Me.Notes = .Notes
            End With
        End Sub
#End Region
    End Class
End Namespace

