Namespace DataObjects
    Public Class usp_GetSalesReportSuburb

#Region "Properties / enums"
        Public Property ApplicationId As System.Guid
        Public Property Customer As System.Nullable(Of Integer)
        Public Property Cid As System.Nullable(Of Integer)
        Public Property SiteName As String
        Public Property ServiceDescription As String
        Public Property ServiceUnits As System.Nullable(Of Double)
        Public Property ServicePrice As System.Nullable(Of Double)
        Public Property PerAnnumCharge As System.Nullable(Of Double)
        Public Property UnitsHaveMoreThanOneRun As System.Nullable(Of Boolean)

        
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(obj As FMS.Business.usp_GetSalesReportSuburbResult)
            With obj
                Me.ApplicationId = .ApplicationId
                Me.Customer = .Customer
                Me.Cid = .Cid
                Me.SiteName = .SiteName
                Me.ServiceDescription = .ServiceDescription
                Me.ServiceUnits = .ServiceUnits
                Me.ServicePrice = .ServicePrice
                Me.PerAnnumCharge = .PerAnnumCharge
                Me.UnitsHaveMoreThanOneRun = .UnitsHaveMoreThanOneRun

            End With
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAllSalesReportSuburb() As List(Of DataObjects.usp_GetSalesReportSuburb)
            Try
                Dim appId = ThisSession.ApplicationID
                Dim obj As New List(Of DataObjects.usp_GetSalesReportSuburb)

                With New LINQtoSQLClassesDataContext
                    .CommandTimeout = 180

                    obj = (From s In .usp_GetSalesReportSuburb()
                           Where s.ApplicationId = appId
                           Select New DataObjects.usp_GetSalesReportSuburb(s)).ToList
                    .Dispose()
                End With
                Return obj

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Shared Function GetAllSalesReportSuburbPerCID(cid As Integer) As List(Of DataObjects.usp_GetSalesReportSuburb)
            Try
                Dim appId = ThisSession.ApplicationID
                Dim obj As New List(Of DataObjects.usp_GetSalesReportSuburb)

                With New LINQtoSQLClassesDataContext
                    .CommandTimeout = 180
                    obj = (From s In .usp_GetSalesReportSuburb()
                           Where s.Cid = cid And s.ApplicationId = appId
                           Select New DataObjects.usp_GetSalesReportSuburb(s)).ToList
                    .Dispose()
                End With
                Return obj

            Catch ex As Exception
                Throw ex
            End Try

        End Function


#End Region

    End Class
End Namespace

