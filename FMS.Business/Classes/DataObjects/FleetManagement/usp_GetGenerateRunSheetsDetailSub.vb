Namespace DataObjects
    Public Class usp_GetGenerateRunSheetsDetailSub
#Region "Properties / enums"
        Public Property Cid As System.Nullable(Of Integer)
        Public Property CSid As System.Nullable(Of Integer)
        Public Property SiteName As String
        Public Property SortOrder As String
        Public Property Add As String
        Public Property Suburb As String
        Public Property SiteContactName As String
        Public Property SiteContactPhone As String
        Public Property SiteContactMobile As String
        Public Property ServiceDesc As String
        Public Property ServiceUnits As System.Nullable(Of Double)
        Public Property ServiceComments As String
        Public Property RunDriver As System.Nullable(Of Integer)
        Public Property DriverName As String
        Public Property GeneralSiteServiceComments As String
        Public Property RunNumber As System.Nullable(Of Integer)
#End Region
#Region "Get methods"
        Public Shared Function GetGenerateRunSheetsDetailSub() As List(Of DataObjects.usp_GetGenerateRunSheetsDetailSub)
            Try
                Dim objGenerateRunSheetsDetailSub As New List(Of DataObjects.usp_GetGenerateRunSheetsDetailSub)
                With New LINQtoSQLClassesDataContext
                    objGenerateRunSheetsDetailSub = (From c In .usp_GetGenerateRunSheetsDetailSub
                                                     Select New DataObjects.usp_GetGenerateRunSheetsDetailSub(c)).ToList
                    .Dispose()
                End With
                Return objGenerateRunSheetsDetailSub
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objGenerateRunSheetsDetailsSub As FMS.Business.usp_GetGenerateRunSheetsDetailSubResult)
            With objGenerateRunSheetsDetailsSub
                Me.Cid = .Cid
                Me.CSid = .CSid
                Me.SiteName = .SiteName
                Me.SortOrder = .SortOrder
                Me.Add = .Add
                Me.Suburb = .Suburb
                Me.SiteContactName = .SiteContactName
                Me.SiteContactPhone = .SiteContactPhone
                Me.SiteContactMobile = .SiteContactMobile
                Me.ServiceDesc = .ServiceDesc
                Me.ServiceUnits = .ServiceUnits
                Me.ServiceComments = .ServiceComments
                Me.RunDriver = .RunDriver
                Me.DriverName = .DriverName
                Me.GeneralSiteServiceComments = .GeneralSiteServiceComments
                Me.RunNumber = .RunNumber
            End With
        End Sub
#End Region
    End Class
End Namespace

