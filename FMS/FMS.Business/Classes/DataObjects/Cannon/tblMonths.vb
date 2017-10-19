Namespace DataObjects
    Public Class tblMonths
#Region "Properties / enums"
        Public Property MonthID As System.Guid
        Public Property MonthNo As System.Nullable(Of Short)
        Public Property MonthDescription As String
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblMonths)
            Dim objMonth = (From c In SingletonAccess.FMSDataContextContignous.tblMonths
                            Order By c.MonthNo
                            Select New DataObjects.tblMonths(c)).ToList
            Return objMonth
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objMonth As FMS.Business.tblMonth)
            With objMonth
                Me.MonthID = .MonthID
                Me.MonthNo = .MonthNo
                Me.MonthDescription = .MonthDescription
            End With
        End Sub
#End Region
    End Class
End Namespace