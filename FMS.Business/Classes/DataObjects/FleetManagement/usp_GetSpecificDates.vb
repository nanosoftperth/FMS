Namespace DataObjects
    Public Class usp_GetSpecificDates
#Region "Properties / enums"
        Public Property DRid As Integer
        Public Property Rid As System.Nullable(Of Integer)
        Public Property DateOfRun As System.Nullable(Of Date)
        Public Property DateOfRun1 As System.Nullable(Of Date)
        Public Property Rid1 As System.Nullable(Of Integer)
#End Region
#Region "Get methods"
        Public Shared Function GetSpecificDates(SqlDate As String, Rid As String) As List(Of DataObjects.usp_GetSpecificDates)
            Dim objSpecificDates = (From c In SingletonAccess.FMSDataContextContignous.usp_GetSpecificDates(SqlDate, Rid, ThisSession.ApplicationID)
                                    Select New DataObjects.usp_GetSpecificDates(c)).ToList
            Return objSpecificDates
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objSpecificDates As FMS.Business.usp_GetSpecificDatesResult)
            With objSpecificDates
                Me.DRid = .DRid
                Me.Rid = .Rid
                Me.DateOfRun = .DateOfRun
                Me.DateOfRun1 = .DateOfRun1
                Me.Rid1 = .Rid1
            End With
        End Sub
#End Region
    End Class
End Namespace


