Namespace DataObjects

    Public Class ApplicationVehicleDriverTime

#Region "PROPERTIES"

        Public Property ApplicationVehicleDriverTimeID As Guid
        Public Property ApplicationID As Guid
        Public Property VehicleID As Guid?
        Public Property StartDate As Date
        Public Property EndDate As Date
        Property Notes As String
        Public Property ApplicationDriverId As Guid?
        Property DriverName As String
        Property VehicleName As String
        Public Property PassengerID As Guid?


#End Region

#Region "COSTRUCTORS"


        Public Sub New(avdt As FMS.Business.ApplicationVehicleDriverTime)

            With avdt
                Me.ApplicationVehicleDriverTimeID = .ApplicationVehicleDriverTimeID
                Me.ApplicationID = .ApplicationID
                Me.VehicleID = .VehicleID
                Me.StartDate = If(.StartDateTime.HasValue, .StartDateTime.Value.timezoneToClient, Nothing)
                Me.EndDate = If(.EndDateTime.HasValue, .EndDateTime.Value.timezoneToClient, Nothing)
                Me.Notes = .Notes
                Me.PassengerID = avdt.PassengerID
                Me.ApplicationDriverId = .ApplicationDriverID
                Me.PassengerID = .PassengerID
            End With

        End Sub

        Public Sub New()

        End Sub

#End Region

#Region "CRUD"

        Public Shared Function Insert(obj As FMS.Business.DataObjects.ApplicationVehicleDriverTime) As Guid

            Dim objToinsert As New FMS.Business.ApplicationVehicleDriverTime

            With objToinsert
                .ApplicationVehicleDriverTimeID = If(obj.ApplicationVehicleDriverTimeID <> Guid.Empty, obj.ApplicationVehicleDriverTimeID, Guid.NewGuid)
                .ApplicationID = obj.ApplicationID
                .EndDateTime = obj.EndDate.timezoneToPerth
                .StartDateTime = obj.StartDate.timezoneToPerth
                .Notes = obj.Notes
                .VehicleID = obj.VehicleID
                .ApplicationDriverID = obj.ApplicationDriverId
                .PassengerID = obj.PassengerID
            End With

            SingletonAccess.FMSDataContextContignous.ApplicationVehicleDriverTimes.InsertOnSubmit(objToinsert)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

            Return SingletonAccess.FMSDataContextNew.ApplicationVehicleDriverTimes.Where( _
                    Function(x) x.ApplicationVehicleDriverTimeID = objToinsert.ApplicationVehicleDriverTimeID).Single.ApplicationVehicleDriverTimeID

        End Function

        Public Shared Sub Update(avdt As FMS.Business.DataObjects.ApplicationVehicleDriverTime)

            'Need to add logic here to test if the primary key exists, if not, then this is an insert
            Dim dbObj As FMS.Business.ApplicationVehicleDriverTime = _
                        SingletonAccess.FMSDataContextContignous.ApplicationVehicleDriverTimes _
                        .Where(Function(x) x.ApplicationVehicleDriverTimeID = avdt.ApplicationVehicleDriverTimeID).SingleOrDefault

            If dbObj Is Nothing Then

                Insert(avdt)

            Else

                With dbObj
                    .ApplicationID = avdt.ApplicationID
                    .EndDateTime = avdt.EndDate.timezoneToPerth
                    .Notes = avdt.Notes
                    .StartDateTime = avdt.StartDate.timezoneToPerth
                    .VehicleID = avdt.VehicleID
                    .ApplicationDriverID = avdt.ApplicationDriverId
                    .PassengerID = avdt.PassengerID
                End With

                SingletonAccess.FMSDataContextContignous.SubmitChanges()

            End If

        End Sub

        Public Shared Sub Delete(avdt As FMS.Business.DataObjects.ApplicationVehicleDriverTime)

            Dim dbObj As FMS.Business.ApplicationVehicleDriverTime = _
                      SingletonAccess.FMSDataContextContignous.ApplicationVehicleDriverTimes _
                      .Where(Function(x) x.ApplicationVehicleDriverTimeID = avdt.ApplicationVehicleDriverTimeID).Single

            SingletonAccess.FMSDataContextContignous.ApplicationVehicleDriverTimes.DeleteOnSubmit(dbObj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

#End Region

#Region "GET / SET"



        Public Shared Function GetAllForApplicationAndDatePeriodIncludingDuds(applicationid As Guid,
                    startdate As Date, enddate As Date) As List(Of FMS.Business.DataObjects.ApplicationVehicleDriverTime)

            startdate = startdate.timezoneToPerth
            enddate = enddate.timezoneToPerth

            'get all of the vehicle driver entries which  fall within the timeframe 
            Dim lst As List(Of FMS.Business.usp_GetVehiclesAndDriversFortimePeriodResult) = _
                        SingletonAccess.FMSDataContextNew.usp_GetVehiclesAndDriversFortimePeriod( _
                                                                        applicationid, startdate, enddate).ToList

            Dim retobj As New List(Of FMS.Business.DataObjects.ApplicationVehicleDriverTime)

            'merge the two result sets (like a left join) and return the results to the server 
            For Each o As usp_GetVehiclesAndDriversFortimePeriodResult In lst

                Dim objToAdd As New FMS.Business.DataObjects.ApplicationVehicleDriverTime

                With objToAdd

                    .ApplicationID = applicationid
                    .VehicleID = o.ApplicationVehicleID

                    If o.ApplicationDriverID.HasValue Then .ApplicationDriverId = o.ApplicationDriverID

                    If o.ApplicationVehicleDriverTimeID.HasValue Then
                        .ApplicationVehicleDriverTimeID = o.ApplicationVehicleDriverTimeID
                    Else
                        .ApplicationVehicleDriverTimeID = Guid.NewGuid
                    End If

                    .StartDate = If(o.StartDateTime.HasValue, o.StartDateTime.Value.timezoneToClient, startdate.timezoneToClient)
                    .EndDate = If(o.EndDateTime.HasValue, o.EndDateTime.Value.timezoneToClient, enddate.timezoneToClient)

                    .PassengerID = o.PassengerID
                End With

                retobj.Add(objToAdd)
            Next

            Return retobj

        End Function
        Public Shared Function GetAllForApplicationAndDatePeriodIncludingDuds(applicationid As Guid
                   ) As List(Of FMS.Business.DataObjects.ApplicationVehicleDriverTime)


            'get all of the vehicle driver entries which  fall within the timeframe 
            Dim lst As List(Of FMS.Business.usp_GetAssignedVehiclestoDriversResult) = _
                        SingletonAccess.FMSDataContextNew.usp_GetAssignedVehiclestoDrivers(applicationid).ToList


            Dim retobj As New List(Of FMS.Business.DataObjects.ApplicationVehicleDriverTime)

            'merge the two result sets (like a left join) and return the results to the server 
            For Each o As usp_GetAssignedVehiclestoDriversResult In lst

                Dim objToAdd As New FMS.Business.DataObjects.ApplicationVehicleDriverTime

                With objToAdd

                    .ApplicationID = applicationid
                    .VehicleID = o.ApplicationVehicleID

                    If o.ApplicationDriverID.HasValue Then .ApplicationDriverId = o.ApplicationDriverID

                    If o.ApplicationVehicleDriverTimeID.HasValue Then
                        .ApplicationVehicleDriverTimeID = o.ApplicationVehicleDriverTimeID
                    Else
                        .ApplicationVehicleDriverTimeID = Guid.NewGuid
                    End If

                    .StartDate = If(o.StartDateTime.HasValue, o.StartDateTime.Value.timezoneToClient, DateTime.Now.timezoneToClient)
                    .EndDate = If(o.EndDateTime.HasValue, o.EndDateTime.Value.timezoneToClient, DateTime.Now.timezoneToClient)

                    .PassengerID = o.PassengerID
                    .VehicleName = o.Name

                    If IsNothing(o.ApplicationDriverID) Then
                        .DriverName = ""
                    Else 
                        .DriverName = ApplicationDriver.GetDriverNameFromID(o.ApplicationDriverID)
                    End If

                End With

                retobj.Add(objToAdd)
            Next

            Return retobj

        End Function


        Public Shared Function GetAllForApplication(applicationID As Guid, queryDateTime As Date) _
                                                As List(Of FMS.Business.DataObjects.ApplicationVehicleDriverTime)

            queryDateTime = queryDateTime.timezoneToPerth

            Dim lst = SingletonAccess.FMSDataContextNew.ApplicationVehicleDriverTimes _
                                .Where(Function(x) x.ApplicationID = applicationID _
                                           AndAlso x.StartDateTime >= queryDateTime _
                                           AndAlso x.EndDateTime <= queryDateTime).ToList

            Return lst.Select(Function(y) New FMS.Business.DataObjects.ApplicationVehicleDriverTime(y)).ToList

            Return Nothing
        End Function

#End Region

    End Class

End Namespace