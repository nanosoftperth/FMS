Public Class Form1




    Private Sub btnCreateSetting_Click(sender As Object, e As EventArgs) Handles btnCreateSetting.Click

        FMS.Business.SingletonAccess.CreateSetting(txtCreateSetting.Text)
        MessageBox.Show("done")
    End Sub

    Private Sub btnCreateApplication_Click(sender As Object, e As EventArgs) Handles btnCreateApplication.Click

        FMS.Business.DataObjects.Application.CreateNew(txtCreateApplication.Text)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim o As Object = FMS.Business.DataObjects.Setting.GetSettingsForApplication("ppjs")
    End Sub

    Private Sub cbSelectedApplication_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbSelectedApplication.SelectedIndexChanged

        Try

            Dim selectedObject As FMS.Business.DataObjects.Application = cbSelectedApplication.SelectedItem

            Me.dgvApplicationSettings.DataSource = _
                FMS.Business.DataObjects.Setting.GetSettingsForApplication(selectedObject.ApplicationName)


        Catch ex As Exception
            MessageBox.Show(ex.Message & vbNewLine & ex.StackTrace)
        End Try

    End Sub

  
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.cbSelectedApplication.DataSource = FMS.Business.DataObjects.Application.GetAllApplications
        Me.cbFMApplication.DataSource = FMS.Business.DataObjects.Application.GetAllApplications
        Me.batchComboApplications.DataSource = FMS.Business.DataObjects.Application.GetAllApplications

    End Sub

    Private Sub btnUpdateSetting_Click(sender As Object, e As EventArgs) Handles btnUpdateSetting.Click


        Dim appid As Guid = CType(cbSelectedApplication.SelectedItem, 
                                        FMS.Business.DataObjects.Application).ApplicationID

        For Each r As DataGridViewRow In dgvApplicationSettings.Rows


            CType(r.DataBoundItem, FMS.Business.DataObjects.Setting).SaveValueToDB(appid)
        Next

        Me.dgvApplicationSettings.Refresh()

    End Sub

    Private Sub btnCreateFeature_Click(sender As Object, e As EventArgs) Handles btnCreateFeature.Click

        Try
            Dim f As New FMS.Business.DataObjects.Feature

            f.Description = txtFeatureDesc.Text
            f.Name = txtFeatureName.Text


            f.FeatureID = FMS.Business.DataObjects.Feature.InsertNewFeature(f)

            MessageBox.Show(String.Format("Succes, new FeatureID: {0}", f.FeatureID))

        Catch ex As Exception
            MessageBox.Show(ex.Message & vbNewLine & ex.StackTrace)
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim appID As Guid = FMS.Business.DataObjects.Application.GetAllApplications.Where(Function(y) y.ApplicationName = "ppjs").Single.ApplicationID

        Dim userid As Guid = FMS.Business.DataObjects.User.GetAllUsersForApplication(appID).Where(Function(y) y.UserName.ToLower = "dave").Single.UserId

        Dim x As New FMS.Business.DataObjects.ApplicationGeoFence

        With x
            ' x.ApplicationGeoFenceID = (Guid.NewGuid)
            x.ApplicationID = appID
            x.DateCreated = Now
            x.Description = "this is  a test"
            x.Name = InputBox("What do you want it to be called?")
            x.UserID = userid
        End With

        Dim id As Guid = FMS.Business.DataObjects.ApplicationGeoFence.Create(x)

        MessageBox.Show(String.Format("new  id = {0}", id))

    End Sub

    Private Sub DeviceBindingSource_CurrentChanged(sender As Object, e As EventArgs) Handles DeviceBindingSource.CurrentChanged

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        For Each rw As DataGridViewRow In dgvDevices.Rows

            Dim device As FMS.Business.DataObjects.Device = rw.DataBoundItem

            'skip iteration if the device is "notihng" (can happen at the end of a row collection)
            If device Is Nothing Then Continue For

            'if this an insert or update?
            Dim exists As Boolean = FMS.Business.DataObjects.Device.GetAllDevices.Where( _
                                    Function(x) x.DeviceID = device.DeviceID).SingleOrDefault IsNot Nothing

            If exists Then
                FMS.Business.DataObjects.Device.Update(device)
            Else
                FMS.Business.DataObjects.Device.Create(device)
                'create PI Points
                FMS.Business.SingletonAccess.CreateLatLongTagsForDevice(device.DeviceID)
            End If
        Next
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ApplicationBindingSource.DataSource = FMS.Business.DataObjects.Application.GetAllApplications
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        DeviceBindingSource.DataSource = FMS.Business.DataObjects.Device.GetAllDevices()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click


        Dim startdate As Date = CDate(InputBox("start date?"))

        Dim piserver As PISDK.Server = (New PISDK.PISDK).Servers.DefaultServer


        Dim messagepip As PISDK.PIPoint = piserver.PIPoints("MessagesFromDevices")


        Dim pitStart As New PITimeServer.PITime With {.LocalDate = startdate}
        Dim pitEnd As New PITimeServer.PITime With {.LocalDate = Now}


        Dim pivs As PISDK.PIValues = messagepip.Data.RecordedValues(pitStart, pitEnd)

        Dim count As Integer = pivs.Count
        Dim i As Integer = 0

        For Each piv As PISDK.PIValue In pivs

            Try

                i += 1
                If i Mod 100 = 0 Then
                    Me.Button7.Text = String.Format("{0} of {1} complete", i, count)
                    Me.Button7.Refresh()

                End If

                Dim pivalue As String = piv.Value

                Dim devicename As String = pivalue.Split(":")(0)

                Dim pointName As String = String.Format("{0}_log", devicename)

                Dim newpimessagepoint As PISDK.PIPoint = piserver.PIPoints(pointName)

                newpimessagepoint.Data.UpdateValue(pivalue, piv.TimeStamp)


            Catch ex As Exception
                Dim msg As String = ex.Message
            End Try

        Next



    End Sub

    Private Sub btnCreateApp_Click(sender As Object, e As EventArgs) Handles btnCreateApp.Click

        Dim appname As String = txtApplicationName.Text

        If MessageBox.Show(String.Format("are you sure you want to create an application called ""{0}""", appname), "question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> Windows.Forms.DialogResult.Yes Then Exit Sub

        Dim app As FMS.Business.DataObjects.Application

        app = FMS.Business.DataObjects.Application.GetFromApplicationName(appname)


        If app Is Nothing Then app = FMS.Business.DataObjects.Application.CreateNew(appname)


        Dim msg As String = String.Format("An application was created with ID ""{0}"" and name ""{1}""", app.ApplicationID, app.ApplicationName)

        ' MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Information)

        'now we need to add a  "general" user, if it doesnot exists and add access to the admin page

        Dim rGeneral As New FMS.Business.DataObjects.Role With {.ApplicationID = app.ApplicationID, .Name = "General", .RoleID = Guid.NewGuid, .Description = "General access to site (cannot be deleted)"}

        Dim rAdmin As New FMS.Business.DataObjects.Role With {.ApplicationID = app.ApplicationID, .Name = "Admin", .RoleID = Guid.NewGuid, .Description = "Admin access to site"}

        rGeneral.RoleID = FMS.Business.DataObjects.Role.insert(rGeneral)
        rAdmin.RoleID = FMS.Business.DataObjects.Role.insert(rAdmin)

        Dim featurelistGeneral As List(Of String) = {"209F096F-4EC1-4A9D-A4B4-209758BADA80",
                                                     "D8E9ABBE-D5D6-4704-9650-351F7AB0043D",
                                                     "5CD03979-748A-49FA-943C-12594438F24B"}.ToList


        For Each flo As String In featurelistGeneral

            Dim afr As New FMS.Business.DataObjects.ApplicationFeatureRole

            afr.ApplicationFeatureRoleID = Guid.NewGuid
            afr.ApplicationID = app.ApplicationID
            afr.RoleID = rGeneral.RoleID
            afr.FeatureID = New Guid(flo)


            FMS.Business.DataObjects.ApplicationFeatureRole.insert(afr)
        Next


        For Each flo2 As Business.DataObjects.Feature In FMS.Business.DataObjects.Feature.GetAllFeatures

            Dim afr As New FMS.Business.DataObjects.ApplicationFeatureRole

            afr.ApplicationFeatureRoleID = Guid.NewGuid
            afr.ApplicationID = app.ApplicationID
            afr.RoleID = rAdmin.RoleID
            afr.FeatureID = flo2.FeatureID

            FMS.Business.DataObjects.ApplicationFeatureRole.insert(afr)
        Next

        MessageBox.Show("inserted all ApplicationFeatureRoles, admin and general")

    End Sub

    Private Sub btnSimulationSettingsLoad_Click(sender As Object, e As EventArgs) Handles btnSimulationSettingsLoad.Click

        SimulatorSettingBindingSource.DataSource = FMS.Business.DataObjects.SimulatorSetting.GetAll
    End Sub

    Private Sub btnSimulationSettingsSave_Click(sender As Object, e As EventArgs) Handles btnSimulationSettingsSave.Click


        For Each rw As DataGridViewRow In dgvSimulationSettings.Rows

            Dim rwObj As FMS.Business.DataObjects.SimulatorSetting = rw.DataBoundItem

            'skip iteration if the rwObj is "notihng" (can happen at the end of a row collection)
            If rwObj Is Nothing Then Continue For

            'if this an insert or update?
            Dim exists As Boolean = FMS.Business.DataObjects.SimulatorSetting.GetAll.Where( _
                                    Function(x) x.SimulatorSettingID = rwObj.SimulatorSettingID). _
                                                                SingleOrDefault IsNot Nothing

            If exists Then
                FMS.Business.DataObjects.SimulatorSetting.Update(rwObj)
            Else
                FMS.Business.DataObjects.SimulatorSetting.Create(rwObj)
            End If
        Next


    End Sub


    Private Sub batchBtnCreate_Click(sender As Object, e As EventArgs) Handles batchBtnCreate.Click
        DeviceBatchCreate(False)
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        DeviceBatchCreate(True)
    End Sub


    Private Sub DeviceBatchCreate(justShowOutput As Boolean)

        Dim pattern As String = batchTxtPattern.Text
        Dim startNumber As Integer = CInt(batchTxtstartNo.Text)
        Dim batchSize As Integer = CInt(batchTxtBatchSize.Text)
        Dim applicationID As Guid = batchComboApplications.SelectedValue

        For i As Integer = startNumber To (startNumber + batchSize)

            Dim newDeviceName As String = String.Format(pattern, i)

            If Not justShowOutput Then


                Dim device As New FMS.Business.DataObjects.Device

                With device
                    .ApplicatinID = applicationID
                    .CreationDate = Now
                    .DeviceID = newDeviceName
                    .notes = "batch generated"
                End With

                FMS.Business.DataObjects.Device.Create(device)
                'create PI Points
                FMS.Business.SingletonAccess.CreateLatLongTagsForDevice(device.DeviceID)

                batchTxtMemoOutput.Text &= vbNewLine & String.Format("device {0} created", newDeviceName)


            Else

                batchTxtMemoOutput.Text &= vbNewLine & newDeviceName
            End If
        Next


    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click

        Dim diagResult As DialogResult = OpenFileDialog1.ShowDialog()

        txtPickleFileLocation.Text = OpenFileDialog1.FileName


    End Sub

    Public WithEvents pProcessor As New PickleProcessor


    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click

        btnStop.Visible = True
        Button10.Visible = False

        pProcessor.StopAllWorking()

        pProcessor = New PickleProcessor

        AddHandler pProcessor.MessageFired, AddressOf pProcessor_MessageFired

        pProcessor.ProcessFile(txtPickleFileLocation.Text, txtURLFormat.Text)

    End Sub

    Public Sub pProcessor_MessageFired(s As String)


        memoPickleProcess.Text &= vbNewLine & s

        memoPickleProcess.SelectionStart = Int32.MaxValue
        memoPickleProcess.ScrollToCaret()
    End Sub


    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click

        btnStop.Visible = False
        Button10.Visible = True

        pProcessor.StopAllWorking()

    End Sub

    Private Sub cbFMApplication_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbFMApplication.SelectedIndexChanged

        Try

            Dim selectedObject As FMS.Business.DataObjects.Application = cbFMApplication.SelectedItem

            Me.dgvApplicationFeature.DataSource = _
                FMS.Business.DataObjects.ApplicationFeature.GetAllFeatures(selectedObject.ApplicationID)


        Catch ex As Exception
            MessageBox.Show(ex.Message & vbNewLine & ex.StackTrace)
        End Try

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim appid As Guid = CType(cbFMApplication.SelectedItem, 
                                       FMS.Business.DataObjects.Application).ApplicationID
        FMS.Business.DataObjects.ApplicationFeature.delete(appid)
        For Each r As DataGridViewRow In dgvApplicationFeature.Rows
            Dim row = CType(r.DataBoundItem, FMS.Business.DataObjects.FeaturesForApplication)
            If row.IsInApplicationFeature = True Then
                Dim x = New FMS.Business.DataObjects.ApplicationFeature()
                x.ApplicationID = appid
                x.FeatureID = row.FeatureID
                FMS.Business.DataObjects.ApplicationFeature.insert(x)
            End If
        Next

        Me.dgvApplicationSettings.Refresh()
    End Sub
End Class

