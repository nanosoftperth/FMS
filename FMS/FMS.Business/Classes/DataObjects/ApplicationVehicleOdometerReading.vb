Namespace DataObjects

    Public Class ApplicationVehicleOdometerReading

#Region "properties"
        Public Property ApplicationVehicleOdometerReadingID As Guid
        Public Property OdometerReading As Decimal
        Public Property ApplicationVehicleID As Guid
        Public Property TimeReadingTaken As Date
        Public Property recordCreationDate As Date
#End Region


#Region "constructors"
        Public Sub New()

        End Sub
        Public Sub New(x As Business.ApplicationVehicleOdometerReading)

            Me.ApplicationVehicleID = x.ApplicationVehicleID
            Me.ApplicationVehicleOdometerReadingID = x.AppliationVehicleOdometerReadingID
            Me.OdometerReading = x.OdometerReading
            Me.recordCreationDate = x.RecordCreatedDate.timezoneToClient ' If(x.RecordCreatedDate.HasValue, x.RecordCreatedDate.Value.timezoneToClient, Nothing)
            Me.TimeReadingTaken = x.TimeReadingTaken.timezoneToClient 'If(x.TimeReadingTaken.HasValue, x.TimeReadingTaken.Value.timezoneToClient, Nothing)

        End Sub

#End Region

#Region "crud"

        Public Shared Function Create(x As DataObjects.ApplicationVehicleOdometerReading)

            Dim newObj As New Business.ApplicationVehicleOdometerReading

            Try

                With newObj

                    If x.ApplicationVehicleOdometerReadingID = Guid.Empty Then
                        .AppliationVehicleOdometerReadingID = Guid.NewGuid
                    Else
                        .AppliationVehicleOdometerReadingID = x.ApplicationVehicleOdometerReadingID
                    End If

                    .ApplicationVehicleID = x.ApplicationVehicleID
                    .OdometerReading = x.OdometerReading
                    .RecordCreatedDate = Now
                    .TimeReadingTaken = x.TimeReadingTaken.timezoneToPerth
                End With

                SingletonAccess.FMSDataContextContignous.ApplicationVehicleOdometerReadings.InsertOnSubmit(newObj)
                SingletonAccess.FMSDataContextContignous.SubmitChanges()


            Catch ex As Exception
                Throw
            End Try

            Return newObj.AppliationVehicleOdometerReadingID

        End Function

        Public Shared Sub Update(x As DataObjects.ApplicationVehicleOdometerReading)


            Dim dbObj As FMS.Business.ApplicationVehicleOdometerReading

            dbObj = (From i In SingletonAccess.FMSDataContextContignous.ApplicationVehicleOdometerReadings _
                      Where i.AppliationVehicleOdometerReadingID = x.ApplicationVehicleOdometerReadingID).Single

            With dbObj
                .ApplicationVehicleID = x.ApplicationVehicleID
                .OdometerReading = x.OdometerReading
                .RecordCreatedDate = Now
                .TimeReadingTaken = x.TimeReadingTaken.timezoneToPerth
            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub Delete(x As DataObjects.ApplicationVehicleOdometerReading)


            Dim dbObj As FMS.Business.ApplicationVehicleOdometerReading

            dbObj = (From i In SingletonAccess.FMSDataContextContignous.ApplicationVehicleOdometerReadings _
                      Where i.AppliationVehicleOdometerReadingID = x.ApplicationVehicleOdometerReadingID).Single

            SingletonAccess.FMSDataContextContignous.ApplicationVehicleOdometerReadings.DeleteOnSubmit(dbObj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub

#End Region

#Region "gets and sets"


        Public Shared Function GetAllForApplication(appid As Guid) As List(Of DataObjects.ApplicationVehicleOdometerReading)

            'had to join to applicationVehicle here to get the applicationID
            Return (From v In SingletonAccess.FMSDataContextContignous.ApplicationVehicles _
                    Join avor In SingletonAccess.FMSDataContextContignous.ApplicationVehicleOdometerReadings _
                    On v.ApplicationVehicleID Equals avor.ApplicationVehicleID _
                    Where v.ApplicationID = appid _
                    Select New DataObjects.ApplicationVehicleOdometerReading(avor)).ToList

        End Function

        Public Shared Function GetForVehicleID(appVehicleID As Guid) As List(Of DataObjects.ApplicationVehicleOdometerReading)


            Return (From x In SingletonAccess.FMSDataContextContignous.ApplicationVehicleOdometerReadings _
                     Where x.ApplicationVehicleID = appVehicleID _ 
                     Select New DataObjects.ApplicationVehicleOdometerReading(x)).ToList

        End Function

#End Region

    End Class


End Namespace


