Imports FMS.Business

Namespace BackgroundCalculations
    Public Class CANBUS_EventGenerator
        Public Shared Function ProcessCanbusEvents(applicationId As Guid) As Boolean
            Dim applicationVehicles = DataObjects.ApplicationVehicle.GetAll(applicationId)
            For Each vehicle In applicationVehicles 'for each vehicle 
                GetEachSPN(vehicle.DeviceID) 'for each SPN 
            Next
            Return True
        End Function
        Private Shared Sub GetEachSPN(strDeviceId As String)
            Dim appVehicle As New DataObjects.ApplicationVehicle()
            appVehicle.DeviceID = strDeviceId
            Dim AvailableCanTags = appVehicle.GetAvailableCANTags()
            For Each availCanTag In AvailableCanTags
                'get the last time that valid data was obtained for that SPN
                Dim lastTimeValid As DateTime = DataObjects.CanBusLogs.GetLastTimeValidData(strDeviceId, availCanTag.PGN, availCanTag.SPN, availCanTag.Standard)
                'read the new SPN data
                Dim CanData = DataObjects.CanDataPoint.GetPointWithLatestDataByDeviceId(Date.Now, lastTimeValid, availCanTag.SPN, strDeviceId, availCanTag.Standard)
                If Not CanData.CanValues.Count.Equals(0) AndAlso Not lastTimeValid.ToString().Equals(CanData.CanValues(0).Time.ToString()) Then
                    ' Save the last time that valid data was obtained for that SPN
                    SaveTheLastTimeValidData(CanData, strDeviceId, availCanTag.SPN, availCanTag.Standard)
                End If
            Next
        End Sub
        Private Shared Sub SaveTheLastTimeValidData(cdt As DataObjects.CanDataPoint, strDeviceId As String, intSPN As Integer, strStandard As String)
            If Not cdt.CanValues.Count.Equals(0) Then
                Dim cbl As New DataObjects.CanBusLogs
                cbl.DeviceId = strDeviceId
                cbl.PGN = cdt.MessageDefinition.PGN
                cbl.SPN = intSPN
                cbl.Standard = strStandard
                cbl.DateLog = cdt.CanValues(0).Time
                DataObjects.CanBusLogs.Create(cbl)
            End If
        End Sub
        Private Shared Sub GenerateEventsFromThisNewData()

        End Sub

        '1. for each vehicle 

        '2. for each SPN 

        '3. get the last time that valid data was obtained for that SPN

        '4. read the new SPN data

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
