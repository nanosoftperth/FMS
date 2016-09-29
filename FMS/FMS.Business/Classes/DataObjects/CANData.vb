
Namespace DataObjects

    Public Class CANData

        Public Property ArbritrationID As Integer
        Public Property Data As String
        Public Property DLC As Integer
        Public Property is_extended_id As Boolean
        Public Property is_error_frame As Boolean
        Public Property is_remote_frame As Boolean
        Public Property TimeStamp As DateTime
        Public Property Id As Integer
        Public Property DeviceID As String

        Public Sub New(x As Business.CAN_Data)

            With x

                Me.ArbritrationID = .arbritration_id
                Me.Data = .data
                Me.DeviceID = .DeviceID
                Me.DLC = .dlc
                Me.Id = .Id
                Me.is_error_frame = .is_error_frame
                Me.is_extended_id = .is_extended_id
                Me.is_remote_frame = .is_remote_frame
                Me.TimeStamp = .timestamp

            End With

        End Sub

        Public Sub New()

        End Sub

        Public Shared Function GetAll() As List(Of Business.DataObjects.CANData)

            Dim retobj As New List(Of DataObjects.CANData)

            With New LINQtoSQLClassesDataContext
                .SubmitChanges()
                retobj = (From x In .CAN_Datas Order By x.timestamp Descending Select New DataObjects.CANData(x)).ToList

                .Dispose()
            End With

            Return retobj

        End Function

    End Class


End Namespace