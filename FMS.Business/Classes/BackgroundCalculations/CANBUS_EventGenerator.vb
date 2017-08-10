
Namespace BackgroundCalculations


    Public Class CANBUS_EventGenerator

        Public Shared Function ProcessCanbusEvents(applicationId As Guid) As Boolean
            'for each vehicle 
            Dim applicationVehicle As New FMS.Business.DataObjects.ApplicationVehicle()
            Dim appVehicles = FMS.Business.DataObjects.ApplicationVehicle.GetAll(applicationId)
            For Each vehicle In appVehicles
                applicationVehicle.DeviceID = vehicle.DeviceID
                Dim x = applicationVehicle.GetAvailableCANTags()
            Next

            Return True
        End Function



        'for each SPN 

        'get the last time that valid data was obtained for that SPN

        'read the new SPN data

        'generte events from this new data


        ' for ( vehicle v in vehicles){
        '   for ( spn in vehicle.SPNs){
        '
        '       date lastProcessedDate =  vehicle.getLastProcessedDateForSPN(spn.ID);
        '
        '       List<values> lst =  spn.GetData(lastProcessedDate,Datetime.Now()) // spn.GetData(<<startDate>>,<<EndDate>>)       
        '
        '       for ( value v in lst) {
        '           //process value and generate event if neccessary ( in SQL)
        '       }
        '       
        '       //if everything was ok
        '       vehicle.SaveSPNLastProcessedDate(lst.Last().EventTime);
        '
        '   }
        '}



    End Class


End Namespace

