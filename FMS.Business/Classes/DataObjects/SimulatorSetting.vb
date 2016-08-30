
Namespace DataObjects


    Public Class SimulatorSetting

#Region "properties"
        Public Property SimulatorSettingID As Guid
        Public Property SourceDeviceID As String
        Public Property DestinationDeviceID As String
        Public Property StartTime As Date
        Public Property EndTime As Date

#End Region

#Region "constructors"


        Public Sub New(x As Business.SimulatorSetting)

            Me.DestinationDeviceID = x.DestinationDeviceID
            Me.EndTime = x.EndTime
            Me.StartTime = x.StartTime
            Me.SimulatorSettingID = x.SimulatorSettingID
            Me.SourceDeviceID = x.SourceDeviceID

        End Sub

        ''' <summary>
        ''' for serialization purposes only
        ''' </summary>
        Public Sub New()

        End Sub

#End Region


#Region "CRUD"


        Public Shared Function Create(x As DataObjects.SimulatorSetting)

            Dim newObj As New Business.SimulatorSetting

            newObj.DestinationDeviceID = x.DestinationDeviceID
            newObj.EndTime = x.EndTime
            'if there is no guid defined (empty guid), then make a new one
            newObj.SimulatorSettingID = If(Guid.Empty = x.SimulatorSettingID, Guid.NewGuid, x.SimulatorSettingID)
            newObj.SourceDeviceID = x.SourceDeviceID
            newObj.StartTime = x.StartTime

            SingletonAccess.FMSDataContextContignous.SimulatorSettings.InsertOnSubmit(newObj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

            Return DataObjects.SimulatorSetting.GetForID(newObj.SimulatorSettingID)

        End Function

        Public Shared Sub Update(x As DataObjects.SimulatorSetting)

            Dim y = SingletonAccess.FMSDataContextContignous.SimulatorSettings.Where( _
                            Function(q) q.SimulatorSettingID = x.SimulatorSettingID).Single

            y.EndTime = x.EndTime
            y.SourceDeviceID = x.SourceDeviceID
            y.DestinationDeviceID = x.DestinationDeviceID
            y.StartTime = x.StartTime

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub Delete(x As DataObjects.SimulatorSetting)


            Dim y = SingletonAccess.FMSDataContextContignous.SimulatorSettings.Where( _
                            Function(q) q.SimulatorSettingID = x.SimulatorSettingID).Single

            SingletonAccess.FMSDataContextContignous.SimulatorSettings.DeleteOnSubmit(y)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

#End Region

#Region "gets & sets"

        Public Shared Function GetForID(id As Guid) As DataObjects.SimulatorSetting


            Dim y = SingletonAccess.FMSDataContextNew.SimulatorSettings.Where( _
                Function(x) x.SimulatorSettingID = id).Single

            Return New DataObjects.SimulatorSetting(y)

        End Function

        Public Shared Function GetAll() As List(Of DataObjects.SimulatorSetting)


            Return (From x In SingletonAccess.FMSDataContextNew.SimulatorSettings _
                    Select New DataObjects.SimulatorSetting(x) _
                    ).ToList

        End Function

#End Region


    End Class

End Namespace