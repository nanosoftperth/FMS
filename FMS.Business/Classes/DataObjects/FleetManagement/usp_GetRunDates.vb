Namespace DataObjects
    Public Class usp_GetRunDates
#Region "Properties / enums"
        Public Property RID As System.Nullable(Of Integer)
        Public Property DateOfRun As System.Nullable(Of Date)
#End Region
#Region "Get methods"
        Public Shared Function GetRunDatesReport(_rid As Integer) As List(Of DataObjects.usp_GetRunDates)
            Try
                Dim objRunDates As New List(Of DataObjects.usp_GetRunDates)
                With New LINQtoSQLClassesDataContext
                    objRunDates = (From c In .usp_GetRunDates(_rid)
                                   Select New DataObjects.usp_GetRunDates(c)).ToList
                    .Dispose()
                End With
                Return objRunDates
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objRundDates As FMS.Business.usp_GetRunDatesResult)
            With objRundDates
                Me.RID = .RID
                Me.DateOfRun = .DateOfRun
            End With
        End Sub
#End Region
    End Class
End Namespace


