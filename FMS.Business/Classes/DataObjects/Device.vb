Namespace DataObjects

    Public Class Device


#Region "Properties"
        Public Property DeviceID As String
        Public Property IMEI As String
        Public Property notes As String
        Public Property PhoneNumber As String
        Public Property ApplicatinID As Guid
        Public Property CreationDate As Date

#End Region

#Region "constructors"
        Public Sub New(d As FMS.Business.Device)

            With d
                Me.DeviceID = .DeviceID
                Me.IMEI = .IMEI
                Me.notes = .notes
                Me.PhoneNumber = .PhoneNumber
                Me.ApplicatinID = .ApplicationID
                Me.CreationDate = .CreationDate
            End With

        End Sub

        ''' <summary>
        ''' retain for serialization purposes
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub
#End Region

        Public Shared Function GetAvailableCANTags(deviceid As String) As List(Of DataObjects.CAN_MessageDefinition)

            Dim lst As New List(Of DataObjects.CAN_MessageDefinition)

            'search for all the pi points which exist for the device
            Dim plst As PISDK.PointList = SingletonAccess.HistorianServer.GetPoints( _
                                                        String.Format("tag = 'CAN*{0}*'", deviceid))

            Dim cnt As Integer = plst.Count

            For Each p As PISDK.PIPoint In plst


                Try

                    Dim ppName As String = p.Name

                    'get the SPN number
                    Dim replaceStr As String = String.Format("CAN_{0}_", deviceid)
                    Dim spn As Integer = ppName.Replace(replaceStr, "")

                    lst.AddRange(CAN_MessageDefinition.GetForSPN(spn))
                Catch ex As Exception

                    Dim msg As String = ex.Message

                End Try
               


            Next

            Return lst

        End Function


        Public Function GetLatLongs(dt As DateTime) As KeyValuePair(Of Decimal, Decimal)

            Dim pipNameLat As String = String.Format("{0}_lat", Me.DeviceID)
            Dim pipNameLng As String = String.Format("{0}_long", Me.DeviceID)

            Dim pit As New PITimeServer.PITime With {.LocalDate = dt}

            Dim lat = SingletonAccess.HistorianServer.PIPoints(pipNameLat).Data.ArcValue(pit, PISDK.RetrievalTypeConstants.rtInterpolated).Value
            Dim lng = SingletonAccess.HistorianServer.PIPoints(pipNameLng).Data.ArcValue(pit, PISDK.RetrievalTypeConstants.rtInterpolated).Value

            Return New KeyValuePair(Of Decimal, Decimal)(lat, lng)

        End Function

        Public Shared Function GetLatLongs(deviceid As String, dt As DateTime) As KeyValuePair(Of Decimal, Decimal)

            Dim pipNameLat As String = String.Format("{0}_lat", deviceid)
            Dim pipNameLng As String = String.Format("{0}_long", deviceid)

            Dim pit As New PITimeServer.PITime With {.LocalDate = dt}

            Dim lat = SingletonAccess.HistorianServer.PIPoints(pipNameLat).Data.ArcValue(pit, PISDK.RetrievalTypeConstants.rtInterpolated).Value
            Dim lng = SingletonAccess.HistorianServer.PIPoints(pipNameLng).Data.ArcValue(pit, PISDK.RetrievalTypeConstants.rtInterpolated).Value

            Return New KeyValuePair(Of Decimal, Decimal)(lat, lng)

        End Function


        Public Structure LogLatLng
            Public Property LogEntry As String
            Public Property Lat As String
            Public Property Lng As String

        End Structure

        Public Shared Function GetDeviceLogLatLng(deviceID As String, d As Date) As LogLatLng

            Dim LogEntry As String = GetLastLogEntryForDeviceID(deviceID)
            Dim latlng As KeyValuePair(Of Decimal, Decimal) = GetLatLongs(deviceID, d)

            Return New LogLatLng With {.LogEntry = LogEntry, .Lat = latlng.Key, .Lng = latlng.Value}


        End Function


        Public Shared Function GetLastLogEntryForDeviceID(deviceID As String) As String


            Dim pipName As String = deviceID & "_log"

            Dim pit As New PITimeServer.PITime With {.LocalDate = Now}

            Dim retVal = SingletonAccess.HistorianServer.PIPoints(pipName).Data.Snapshot.Value

            Return retVal

        End Function


        Public Shared Function GetFromDeviceID(deviceID As String) As Business.DataObjects.Device

            Dim retobj As DataObjects.Device

            With New LINQtoSQLClassesDataContext


                retobj = (From x In .Devices
                          Where x.DeviceID = deviceID _
                            Select New DataObjects.Device(x) _
                                           ).SingleOrDefault

                .Dispose()
            End With

            Return retobj

        End Function

        Public Shared Sub Update(d As DataObjects.Device)

            Dim dvce As FMS.Business.Device = SingletonAccess.FMSDataContextContignous.Devices.Where( _
                                                                    Function(x) x.DeviceID = d.DeviceID).Single()

            With dvce
                .ApplicationID = d.ApplicatinID
                .CreationDate = Now
                .DeviceID = d.DeviceID
                .IMEI = d.IMEI
                .notes = d.notes
                .PhoneNumber = d.PhoneNumber
            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub Create(d As DataObjects.Device)

            Dim contextDevce As New FMS.Business.Device

            With contextDevce
                .ApplicationID = d.ApplicatinID
                .CreationDate = Now
                .DeviceID = d.DeviceID
                .IMEI = d.IMEI
                .notes = d.notes
                .PhoneNumber = d.PhoneNumber
            End With

            SingletonAccess.FMSDataContextContignous.Devices.InsertOnSubmit(contextDevce)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub delete(deviceid As String)

            Dim dvce As FMS.Business.Device = SingletonAccess.FMSDataContextContignous.Devices.Where(Function(x) x.DeviceID = deviceid).Single()

            SingletonAccess.FMSDataContextContignous.Devices.DeleteOnSubmit(dvce)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub

        Public Shared Function AssignDeviceToApplication(deviceid As String, applicationid As Guid)

            Dim dev As FMS.Business.Device = SingletonAccess.FMSDataContextContignous.Devices _
                                                    .Where(Function(x) x.DeviceID = deviceid).SingleOrDefault

            'if device does not exist, return FALSE
            If dev Is Nothing Then Return False

            dev.ApplicationID = applicationid

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

            Return True

        End Function

        Public Shared Function GetAllDevices() As List(Of DataObjects.Device)

            Return SingletonAccess.FMSDataContextNew.Devices.Select( _
                                        Function(x) New DataObjects.Device(x)).ToList

        End Function

        Public Shared Function GetDevicesforApplication(appid As Guid) As List(Of DataObjects.Device)

            Return SingletonAccess.FMSDataContextNew.Devices.Where(Function(y) y.ApplicationID = appid).Select( _
                                        Function(x) New DataObjects.Device(x)).ToList

        End Function

        Public Function GetSpecificdevice(deviceID As String) As DataObjects.Device

            Return SingletonAccess.FMSDataContextNew.Devices.Where(Function(x) x.DeviceID = deviceID) _
                                                .ToList.Select(Function(y) New DataObjects.Device(y)).Single

        End Function

    End Class

End Namespace

