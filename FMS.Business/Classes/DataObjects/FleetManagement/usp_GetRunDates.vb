Namespace DataObjects
    Public Class usp_GetRunDates
#Region "Properties / enums"
        Public Property RID As System.Nullable(Of Integer)
        Public Property DateOfRun As System.Nullable(Of Date)
#End Region
#Region "Get methods"
        Public Shared Function GetRunDatesReport(_rid As Integer) As List(Of DataObjects.usp_GetRunDates)
            Dim objRunDates = (From c In SingletonAccess.FMSDataContextContignous.usp_GetRunDates(_rid)
                            Select New DataObjects.usp_GetRunDates(c)).ToList
            Return objRunDates
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


