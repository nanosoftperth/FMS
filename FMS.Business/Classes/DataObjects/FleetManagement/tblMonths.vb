Namespace DataObjects
    Public Class tblMonths
#Region "Properties / enums"
        Public Property MonthID As System.Guid
        Public Property MonthNo As System.Nullable(Of Short)
        Public Property MonthDescription As String
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblMonths)
            Try
                Dim objMonth As New List(Of DataObjects.tblMonths)
                With New LINQtoSQLClassesDataContext
                    objMonth = (From c In .tblMonths
                                Order By c.MonthNo
                                Select New DataObjects.tblMonths(c)).ToList
                    .Dispose()
                End With
                Return objMonth
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetAllByMonthDescription() As List(Of DataObjects.tblMonths)
            Try
                Dim objMonth As New List(Of DataObjects.tblMonths)
                With New LINQtoSQLClassesDataContext
                    objMonth = (From c In .tblMonths
                                Order By c.MonthDescription
                                Select New DataObjects.tblMonths(c)).ToList
                    .Dispose()
                End With
                Return objMonth
            Catch ex As Exception
                Throw ex
            End Try
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