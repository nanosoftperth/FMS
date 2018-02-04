Namespace DataObjects
    Public Class usp_GetPerAnnumValuesReport

#Region "Properties / enums"
        Public Property ApplicationId As System.Guid
        Public Property CustomerName As String
        Public Property SumOfPerAnnumCharge As System.Nullable(Of Double)
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(obj As FMS.Business.usp_GetPerAnnumValuesReportResult)
            With obj
                Me.ApplicationId = .ApplicationID
                Me.CustomerName = .CustomerName
                Me.SumOfPerAnnumCharge = .SumOfPerAnnumCharge
            End With
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAllPerApplication() As List(Of DataObjects.usp_GetPerAnnumValuesReport)
            Try
                Dim obj As New List(Of DataObjects.usp_GetPerAnnumValuesReport)
                Dim appId = ThisSession.ApplicationID

                With New LINQtoSQLClassesDataContext
                    obj = (From v In .usp_GetPerAnnumValuesReport
                           Where v.ApplicationID = appId
                           Select New DataObjects.usp_GetPerAnnumValuesReport(v)).ToList
                    .Dispose()
                End With

                Return obj
            Catch ex As Exception
                Throw ex
            End Try

        End Function


        Public Class CustomerPerAnnumCharge
            Public Property ApplicationId As System.Guid
            Public Property Customer As Integer
            Public Property PerAnnumCharge As System.Nullable(Of Double)

        End Class


        'Public Shared Function GetCustPerAnnumCharge() As List(Of CustomerPerAnnumCharge)
        '    Dim appId = ThisSession.ApplicationID

        '    Dim obj = (From s In SingletonAccess.FMSDataContextContignous.tblSites
        '               Group Join cs In SingletonAccess.FMSDataContextContignous.tblCustomerServices
        '                On s.Cid Equals cs.CId
        '                  Into services = Group
        '               From service In services
        '               Select New CustomerPerAnnumCharge With {
        '                    .ApplicationId = s.ApplicationId,
        '                    .Customer = s.Customer,
        '                    .PerAnnumCharge = service.PerAnnumCharge
        '                }).ToList()

        '    Return obj


        'End Function

        'Public Shared Function GetRIDPerCustomerCharge(charge As Double) As Integer
        '    Dim appId = ThisSession.ApplicationID

        '    Dim intRID As Integer = 0

        '    Dim rating = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerRatings
        '                  Where charge >= c.FromValue And charge <= c.ToValue).ToList()

        '    If (rating.Count > 0) Then
        '        intRID = rating.FirstOrDefault.Rid
        '    End If

        '    Return intRID

        'End Function

#End Region

    End Class


End Namespace


