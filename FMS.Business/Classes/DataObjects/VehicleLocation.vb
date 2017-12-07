
Namespace DataObjects

    Public Class VehicleLocation


#Region "Properties"
        Public Property LocationID As Guid
        Public Property VehicleID As Guid
        Public Property BusinessLocationID As Guid

        ' start for Lookup
        Public Property Name As String
        Public Property DeviceID As String
        Public Property ApplicationID As Guid
        ' end for lookup

#End Region

#Region "Constructors"
        Public Sub New(vl As FMS.Business.VehicleLocation)

            With vl
                Me.LocationID = vl.LocationID
                Me.VehicleID = vl.VehicleID
                Me.BusinessLocationID = vl.BusinessLocationID
            End With

        End Sub

        Public Sub New()

        End Sub
#End Region

#Region "CRUD"
        Public Shared Sub Create(vl As FMS.Business.DataObjects.VehicleLocation)

            Dim contextLocation As New FMS.Business.VehicleLocation

            With contextLocation
                .LocationID = Guid.NewGuid
                .VehicleID = vl.VehicleID
                .BusinessLocationID = vl.BusinessLocationID

            End With

            SingletonAccess.FMSDataContextContignous.VehicleLocations.InsertOnSubmit(contextLocation)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub Update(vl As FMS.Business.DataObjects.VehicleLocation)
            Dim currObj As FMS.Business.VehicleLocation = (From i In SingletonAccess.FMSDataContextContignous.VehicleLocations
                                                          Where i.LocationID = vl.LocationID).Single


            With currObj
                '.ApplicationVehicleID = Guid.NewGuid
                .VehicleID = vl.VehicleID
                .BusinessLocationID = vl.BusinessLocationID

            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub

        Public Shared Sub Delete(vl As FMS.Business.DataObjects.VehicleLocation)
            Dim currObj As FMS.Business.VehicleLocation = (From i In SingletonAccess.FMSDataContextContignous.VehicleLocations
                                                             Where i.LocationID = vl.LocationID).Single

            SingletonAccess.FMSDataContextContignous.VehicleLocations.DeleteOnSubmit(currObj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub
#End Region

#Region "Selectors"
        Public Shared Function GetAll(LocationID As Guid) As List(Of VehicleLocation)

            If LocationID = Guid.Empty Then Return Nothing

            Dim retobj As Object = SingletonAccess.FMSDataContextNew.VehicleLocations

            'Dim retobj As Object = SingletonAccess.FMSDataContextNew.VehicleLocations.Where(Function(y) y.ApplicationID = appplicationID).OrderBy(Function(m) m.DeviceID).Select( _
            '                                                                Function(x) New DataObjects.ApplicationVehicle(x)).ToList

            Return retobj

        End Function

        Public Shared Function GetAllForResourceMgmt(appplicationID As Guid) As List(Of ApplicationVehicle)

            If appplicationID = Guid.Empty Then Return Nothing

            Dim retobj As Object = SingletonAccess.FMSDataContextNew.ApplicationVehicles.Where(Function(y) y.ApplicationID = appplicationID).OrderBy(Function(m) m.DeviceID).Select( _
                                                                            Function(x) New DataObjects.ApplicationVehicle(x)).ToList

            Return retobj

        End Function

        Public Shared Function GetPerAppVehicleID(appvehicleID As Guid) As List(Of VehicleLocation)

            Return (From x In SingletonAccess.FMSDataContextContignous.VehicleLocations _
                    Where x.VehicleID = appvehicleID _
                    Select New DataObjects.VehicleLocation(x)).ToList

        End Function

        Public Shared Function GetPerDeviceID(devID As String) As Object

            '-- Old Code
            'Return (From x In SingletonAccess.FMSDataContextContignous.vw_GetVehicles _
            '        Where x.DeviceID = deviceID).ToList

            ' Updated code for approval (this will minimize going back to DB again for fetch)
            Dim appID = FMS.Business.ThisSession.ApplicationID
            Dim listVehicle = (From vl In SingletonAccess.FMSDataContextContignous.VehicleLocations
                              Join al In SingletonAccess.FMSDataContextContignous.ApplicationLocations
                              On vl.BusinessLocationID Equals al.ApplicationLocationID
                              Join av In SingletonAccess.FMSDataContextContignous.ApplicationVehicles
                              On vl.VehicleID Equals av.ApplicationVehicleID
                              Where (av.DeviceID = devID)
                              Select al.ApplicationID, al.ApplicationLocationID, al.Name, al.Address,
                                    vl.LocationID, vl.VehicleID, Vehicle_Name = av.Name, av.DeviceID
                                    ).ToList()

            Return listVehicle

        End Function

        Public Shared Function GetAssignedLocationPerApplicationID(applicationID As Guid) As Object
            Dim listVehicle = SingletonAccess.FMSDataContextContignous.vw_GetVehicleLocations().Where(Function(v) _
                                            v.ApplicationID = applicationID).Where(Function(y) _
                                            y.DeviceID IsNot Nothing).Where(Function(z) _
                                    z.LocationID IsNot Nothing).ToList().OrderBy(Function(vid) vid.BusinessLocationID).ToList()


            Dim objVehicleList As New List(Of Vehicles)
            Dim strLoc As Guid

            For nRow = 0 To listVehicle.Count - 1

                If strLoc.ToString() = "00000000-0000-0000-0000-000000000000" Then
                    strLoc = listVehicle(nRow).BusinessLocationID
                Else
                    If (strLoc <> listVehicle(nRow).BusinessLocationID) Then
                        strLoc = listVehicle(nRow).BusinessLocationID

                        Dim listRow = New Vehicles

                        listRow.BusinessLocationID = strLoc
                        listRow.Loc_Name = listVehicle(nRow).AL_Name

                        objVehicleList.Add(listRow)

                    End If
                End If

            Next

            ' New Code Remarks
            'Dim listVehicle = (From vl In SingletonAccess.FMSDataContextContignous.VehicleLocations
            '                  Join al In SingletonAccess.FMSDataContextContignous.ApplicationLocations
            '                  On vl.BusinessLocationID Equals al.ApplicationLocationID
            '                  Join av In SingletonAccess.FMSDataContextContignous.ApplicationVehicles
            '                  On vl.VehicleID Equals av.ApplicationVehicleID
            '                  Where (av.ApplicationID = applicationID)
            '                  Select App_LocID = al.ApplicationID, al.ApplicationLocationID, al.Name, al.Address,
            '                        vl.LocationID, vl.VehicleID, Vehicle_Name = av.Name, av.DeviceID
            '                        ).ToList()

            Return objVehicleList

        End Function

        Public Shared Function GetPerAppID(applicationID As Guid, Optional IncludeDefault As Boolean = False) As List(Of Vehicles)
            Dim strFeatureID As String = ""
            Dim userID = FMS.Business.ThisSession.User.UserId
            Dim userRoledID = FMS.Business.ThisSession.User.RoleID

            Dim listVeh As List(Of Vehicles) = New List(Of Vehicles)

            Dim oList As New List(Of FMS.Business.DataObjects.ApplicationLocation)

            Dim featureList = FMS.Business.DataObjects.Feature.GetAllFeatures().Where(Function(f) f.Name.Contains("Vehicle and Driver Management - See All Vehicle")).ToList()

            If (featureList.Count > 0) Then
                'do comment or remark if want to test within business location only automatically
                strFeatureID = featureList(0).FeatureID.ToString()
            End If

            'check if user have access to all vehicles (Note: Need to change FeatureID field value for criteria whenever the Vehicle and Driver Management - See All Vehicle id changes)
            Dim retafr = FMS.Business.DataObjects.ApplicationFeatureRole.GetAllApplicationFeatureRoles(applicationID).Where(Function(l) l.RoleID = userRoledID And l.FeatureID.ToString() = strFeatureID).ToList()

            If (retafr.Count > 0) Then
                'Dim lstLocation = (From al In SingletonAccess.FMSDataContextContignous.ApplicationLocations
                '                  Select al.ApplicationLocationID, al.Name).ToList()

                Dim lstLocation = SingletonAccess.FMSDataContextContignous.ApplicationLocations().OrderBy(Function(l) l.Name).ToList()

                If (lstLocation.Count > 0) Then
                    For rLoc = 0 To lstLocation.Count - 1
                        Dim listRow = New Vehicles
                        listRow.BusinessLocationID = lstLocation(rLoc).ApplicationLocationID
                        listRow.Name = lstLocation(rLoc).Name
                        listRow.Address = lstLocation(rLoc).Address

                        listVeh.Add(listRow)
                    Next

                End If

            Else
                oList = FMS.Business.SingletonAccess.ClientSelected_BusinessLocation

                If (oList.Count > 0) Then

                    For row = 0 To oList.Count - 1
                        Dim listRow = New Vehicles
                        listRow.BusinessLocationID = oList(row).ApplicationLocationID
                        listRow.Name = oList(row).Name
                        listRow.Address = oList(row).Address

                        listVeh.Add(listRow)

                    Next

                End If

            End If

            'Dim listVeh As List(Of Vehicles) = New List(Of Vehicles)

            'Dim oList = FMS.Business.SingletonAccess.ClientSelected_BusinessLocation

            'If (oList.Count > 0) Then

            '    For row = 0 To oList.Count - 1
            '        Dim listRow = New Vehicles
            '        listRow.BusinessLocationID = oList(row).ApplicationLocationID
            '        listRow.Name = oList(row).Name
            '        listRow.Address = oList(row).Address

            '        listVeh.Add(listRow)

            '    Next

            'End If

            Return listVeh

        End Function

        ' Will remove if the new get all function is approve
        Public Shared Function GetPerAppIDAndDevID(applicationID As Guid, deviceID As String, Optional IncludeDefault As Boolean = False) As List(Of Vehicles)
            Dim strFeatureID As String = ""
            Dim userID = FMS.Business.ThisSession.User.UserId
            Dim userRoledID = FMS.Business.ThisSession.User.RoleID

            Dim listVeh As List(Of Vehicles) = New List(Of Vehicles)

            Dim oList As New List(Of FMS.Business.DataObjects.ApplicationLocation)

            Dim featureList = FMS.Business.DataObjects.Feature.GetAllFeatures().Where(Function(f) f.Name.Contains("Vehicle and Driver Management - See All Vehicle")).ToList()

            If (featureList.Count > 0) Then
                'do comment or remark if want to test within business location only automatically
                strFeatureID = featureList(0).FeatureID.ToString()
            End If

            'check if user have access to all vehicles (Note: Need to change FeatureID field value for criteria whenever the Vehicle and Driver Management - See All Vehicle id changes)
            Dim retafr = FMS.Business.DataObjects.ApplicationFeatureRole.GetAllApplicationFeatureRoles(applicationID).Where(Function(l) l.RoleID = userRoledID And l.FeatureID.ToString() = strFeatureID).ToList()

            If (retafr.Count > 0) Then
                Dim lstLocation = SingletonAccess.FMSDataContextContignous.ApplicationLocations

            Else
                oList = FMS.Business.SingletonAccess.ClientSelected_BusinessLocation

            End If


            If (oList.Count > 0) Then

                For row = 0 To oList.Count - 1
                    Dim listRow = New Vehicles
                    listRow.BusinessLocationID = oList(row).ApplicationLocationID
                    listRow.Name = oList(row).Name
                    listRow.Address = oList(row).Address

                    listVeh.Add(listRow)

                Next

            End If

            

            Return listVeh

        End Function

        
#End Region

#Region "Extended CRUD"

        Public Shared Sub Save(objVL As Object, appvehicleID As Guid)

            Dim oList = GetPerAppVehicleID(appvehicleID)
            Dim ctr As Integer = 0

            If (oList IsNot Nothing) Then

                'For Each element As Object In oList
                '    ctr = ctr + 1
                'Next

                ctr = oList.Count

                If (ctr > 0) Then
                    DoSave(objVL, appvehicleID, False)
                    'Update_Ext(objVL, appvehicleID)
                Else
                    DoSave(objVL, appvehicleID, True)
                    'Create_Ext(objVL, appvehicleID)
                End If

            Else
                DoSave(objVL, appvehicleID, True)
                'Create_Ext(objVL, appvehicleID)
            End If

        End Sub

        Public Shared Sub DoSave(objVL As Object, appvehicleID As Guid, blnInsert As Boolean)

            Dim listRow = New VehicleLocation
            listRow.VehicleID = appvehicleID

            If (blnInsert = False) Then '
                DeletePerVehicle(listRow)
            End If

            If (objVL IsNot Nothing) Then
                Dim objType = objVL.GetType()
                If (objType.Name() = "List`1") Then
                    Dim oCtr = DirectCast(objVL.Count, Integer)
                    Dim val As Guid

                    If oCtr > 0 Then
                        For Each element As Object In objVL
                            val = element

                            'If (listRow.VehicleID.ToString().Length <= 0) Then listRow.VehicleID = appvehicleID
                            listRow.BusinessLocationID = val

                            Create(listRow)

                        Next

                    End If

                End If
            End If

        End Sub

        ' due for removal
        'Public Shared Sub Create_Ext(objVL As Object, appvehicleID As Guid)

        '    Dim listRow = New VehicleLocation

        '    If (objVL IsNot Nothing) Then
        '        Dim objType = objVL.GetType()
        '        If (objType.Name() = "List`1") Then
        '            Dim oCtr = DirectCast(objVL.Count, Integer)
        '            Dim val As Guid

        '            If oCtr > 0 Then
        '                For Each element As Object In objVL
        '                    val = element

        '                    listRow.VehicleID = appvehicleID
        '                    listRow.BusinessLocationID = val

        '                    Create(listRow)

        '                Next

        '            End If

        '        End If
        '    End If

        'End Sub

        'Public Shared Sub Update_Ext(objVL As Object, appvehicleID As Guid)
        '    Dim listRow = New VehicleLocation

        '    listRow.VehicleID = appvehicleID

        '    DeletePerVehicle(listRow)

        '    If (objVL IsNot Nothing) Then

        '        Dim objType = objVL.GetType()

        '        If (objType.Name() = "List`1") Then
        '            Dim oCtr = DirectCast(objVL.Count, Integer)
        '            Dim val As Guid

        '            If oCtr > 0 Then
        '                For Each element As Object In objVL
        '                    val = element

        '                    listRow.VehicleID = appvehicleID
        '                    listRow.BusinessLocationID = val

        '                    Create(listRow)

        '                Next

        '            End If

        '        End If


        '    End If


        'End Sub

        Public Shared Sub DeletePerVehicle(vl As FMS.Business.DataObjects.VehicleLocation)

            Dim tblVehicleLocations = (From i In SingletonAccess.FMSDataContextContignous.VehicleLocations
                                                             Where i.VehicleID = vl.VehicleID).ToList()

            If Not tblVehicleLocations Is Nothing Then
                SingletonAccess.FMSDataContextContignous.VehicleLocations.DeleteAllOnSubmit(tblVehicleLocations)
                SingletonAccess.FMSDataContextContignous.SubmitChanges()
            End If


        End Sub

#End Region

#Region "not static methods"
        Public Class Vehicles

            Public Property LocationID As Guid
            Public Property VehicleID As Guid
            Public Property BusinessLocationID As Guid

            ' start for Lookup
            Public Property Name As String
            Public Property Loc_Name As String
            Public Property DeviceID As String
            Public Property Address As String
            Public Property ApplicationID As Guid
            ' end for lookup
            Public Sub New()

            End Sub
        End Class
#End Region


    End Class

End Namespace


