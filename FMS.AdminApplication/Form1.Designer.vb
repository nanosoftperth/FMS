﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.btnCreateApp = New System.Windows.Forms.Button()
        Me.txtApplicationName = New System.Windows.Forms.TextBox()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.btnUpdateSetting = New System.Windows.Forms.Button()
        Me.dgvApplicationSettings = New System.Windows.Forms.DataGridView()
        Me.NameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ValueDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SettingBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.cbSelectedApplication = New System.Windows.Forms.ComboBox()
        Me.ApplicationBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.txtFeatureDesc = New System.Windows.Forms.TextBox()
        Me.txtFeatureName = New System.Windows.Forms.TextBox()
        Me.txtCreateApplication = New System.Windows.Forms.TextBox()
        Me.txtCreateSetting = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnCreateFeature = New System.Windows.Forms.Button()
        Me.btnCreateApplication = New System.Windows.Forms.Button()
        Me.btnCreateSetting = New System.Windows.Forms.Button()
        Me.TabPage4 = New System.Windows.Forms.TabPage()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.dgvDevices = New System.Windows.Forms.DataGridView()
        Me.DeviceIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IMEIDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NotesDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PhoneNumberDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ApplicatinIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CreationDateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DeviceBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.TabPage5 = New System.Windows.Forms.TabPage()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.ApplicationNameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ApplicationIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.TabPage6 = New System.Windows.Forms.TabPage()
        Me.dgvSimulationSettings = New System.Windows.Forms.DataGridView()
        Me.SimulatorSettingBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SimulatorSettingIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SourceDeviceIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DestinationDeviceIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StartTimeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EndTimeDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.btnSimulationSettingsLoad = New System.Windows.Forms.Button()
        Me.btnSimulationSettingsSave = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.dgvApplicationSettings, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SettingBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ApplicationBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.dgvDevices, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DeviceBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        CType(Me.dgvSimulationSettings, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SimulatorSettingBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Controls.Add(Me.TabPage5)
        Me.TabControl1.Controls.Add(Me.TabPage6)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(721, 420)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.btnCreateApp)
        Me.TabPage1.Controls.Add(Me.txtApplicationName)
        Me.TabPage1.Controls.Add(Me.Button7)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(713, 394)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Application List"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'btnCreateApp
        '
        Me.btnCreateApp.Location = New System.Drawing.Point(260, 167)
        Me.btnCreateApp.Name = "btnCreateApp"
        Me.btnCreateApp.Size = New System.Drawing.Size(139, 23)
        Me.btnCreateApp.TabIndex = 2
        Me.btnCreateApp.Text = "Create Application"
        Me.btnCreateApp.UseVisualStyleBackColor = True
        '
        'txtApplicationName
        '
        Me.txtApplicationName.Location = New System.Drawing.Point(74, 171)
        Me.txtApplicationName.Name = "txtApplicationName"
        Me.txtApplicationName.Size = New System.Drawing.Size(154, 20)
        Me.txtApplicationName.TabIndex = 1
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(375, 55)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(277, 23)
        Me.Button7.TabIndex = 0
        Me.Button7.Text = "search main log and put into seperate"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.btnUpdateSetting)
        Me.TabPage2.Controls.Add(Me.dgvApplicationSettings)
        Me.TabPage2.Controls.Add(Me.cbSelectedApplication)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(713, 394)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Manage Settings"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btnUpdateSetting
        '
        Me.btnUpdateSetting.Location = New System.Drawing.Point(486, 27)
        Me.btnUpdateSetting.Name = "btnUpdateSetting"
        Me.btnUpdateSetting.Size = New System.Drawing.Size(125, 23)
        Me.btnUpdateSetting.TabIndex = 2
        Me.btnUpdateSetting.Text = "Update"
        Me.btnUpdateSetting.UseVisualStyleBackColor = True
        '
        'dgvApplicationSettings
        '
        Me.dgvApplicationSettings.AllowUserToAddRows = False
        Me.dgvApplicationSettings.AllowUserToDeleteRows = False
        Me.dgvApplicationSettings.AutoGenerateColumns = False
        Me.dgvApplicationSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvApplicationSettings.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NameDataGridViewTextBoxColumn, Me.ValueDataGridViewTextBoxColumn})
        Me.dgvApplicationSettings.DataSource = Me.SettingBindingSource
        Me.dgvApplicationSettings.Location = New System.Drawing.Point(9, 61)
        Me.dgvApplicationSettings.Name = "dgvApplicationSettings"
        Me.dgvApplicationSettings.Size = New System.Drawing.Size(701, 327)
        Me.dgvApplicationSettings.TabIndex = 1
        '
        'NameDataGridViewTextBoxColumn
        '
        Me.NameDataGridViewTextBoxColumn.DataPropertyName = "Name"
        Me.NameDataGridViewTextBoxColumn.HeaderText = "Name"
        Me.NameDataGridViewTextBoxColumn.Name = "NameDataGridViewTextBoxColumn"
        '
        'ValueDataGridViewTextBoxColumn
        '
        Me.ValueDataGridViewTextBoxColumn.DataPropertyName = "Value"
        Me.ValueDataGridViewTextBoxColumn.HeaderText = "Value"
        Me.ValueDataGridViewTextBoxColumn.Name = "ValueDataGridViewTextBoxColumn"
        '
        'SettingBindingSource
        '
        Me.SettingBindingSource.DataSource = GetType(FMS.Business.DataObjects.Setting)
        '
        'cbSelectedApplication
        '
        Me.cbSelectedApplication.DataSource = Me.ApplicationBindingSource
        Me.cbSelectedApplication.DisplayMember = "ApplicationName"
        Me.cbSelectedApplication.FormattingEnabled = True
        Me.cbSelectedApplication.Location = New System.Drawing.Point(31, 27)
        Me.cbSelectedApplication.Name = "cbSelectedApplication"
        Me.cbSelectedApplication.Size = New System.Drawing.Size(171, 21)
        Me.cbSelectedApplication.TabIndex = 0
        Me.cbSelectedApplication.ValueMember = "ApplicationID"
        '
        'ApplicationBindingSource
        '
        Me.ApplicationBindingSource.DataSource = GetType(FMS.Business.DataObjects.Application)
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Button3)
        Me.TabPage3.Controls.Add(Me.Button2)
        Me.TabPage3.Controls.Add(Me.TextBox1)
        Me.TabPage3.Controls.Add(Me.txtFeatureDesc)
        Me.TabPage3.Controls.Add(Me.txtFeatureName)
        Me.TabPage3.Controls.Add(Me.txtCreateApplication)
        Me.TabPage3.Controls.Add(Me.txtCreateSetting)
        Me.TabPage3.Controls.Add(Me.Button1)
        Me.TabPage3.Controls.Add(Me.btnCreateFeature)
        Me.TabPage3.Controls.Add(Me.btnCreateApplication)
        Me.TabPage3.Controls.Add(Me.btnCreateSetting)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(713, 394)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Test tab"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(85, 211)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(212, 23)
        Me.Button3.TabIndex = 3
        Me.Button3.Text = "create test geofence"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(320, 227)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(152, 23)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "btnSettings"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(46, 137)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(251, 20)
        Me.TextBox1.TabIndex = 1
        '
        'txtFeatureDesc
        '
        Me.txtFeatureDesc.Location = New System.Drawing.Point(303, 27)
        Me.txtFeatureDesc.Name = "txtFeatureDesc"
        Me.txtFeatureDesc.Size = New System.Drawing.Size(251, 20)
        Me.txtFeatureDesc.TabIndex = 1
        Me.txtFeatureDesc.Text = "feature desc"
        '
        'txtFeatureName
        '
        Me.txtFeatureName.Location = New System.Drawing.Point(46, 30)
        Me.txtFeatureName.Name = "txtFeatureName"
        Me.txtFeatureName.Size = New System.Drawing.Size(251, 20)
        Me.txtFeatureName.TabIndex = 1
        Me.txtFeatureName.Text = "feature name"
        '
        'txtCreateApplication
        '
        Me.txtCreateApplication.Location = New System.Drawing.Point(46, 97)
        Me.txtCreateApplication.Name = "txtCreateApplication"
        Me.txtCreateApplication.Size = New System.Drawing.Size(251, 20)
        Me.txtCreateApplication.TabIndex = 1
        '
        'txtCreateSetting
        '
        Me.txtCreateSetting.Location = New System.Drawing.Point(46, 59)
        Me.txtCreateSetting.Name = "txtCreateSetting"
        Me.txtCreateSetting.Size = New System.Drawing.Size(251, 20)
        Me.txtCreateSetting.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(303, 137)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(120, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnCreateFeature
        '
        Me.btnCreateFeature.Location = New System.Drawing.Point(576, 27)
        Me.btnCreateFeature.Name = "btnCreateFeature"
        Me.btnCreateFeature.Size = New System.Drawing.Size(120, 23)
        Me.btnCreateFeature.TabIndex = 0
        Me.btnCreateFeature.Text = "Create Feature"
        Me.btnCreateFeature.UseVisualStyleBackColor = True
        '
        'btnCreateApplication
        '
        Me.btnCreateApplication.Location = New System.Drawing.Point(303, 97)
        Me.btnCreateApplication.Name = "btnCreateApplication"
        Me.btnCreateApplication.Size = New System.Drawing.Size(120, 23)
        Me.btnCreateApplication.TabIndex = 0
        Me.btnCreateApplication.Text = "Create Application"
        Me.btnCreateApplication.UseVisualStyleBackColor = True
        '
        'btnCreateSetting
        '
        Me.btnCreateSetting.Location = New System.Drawing.Point(303, 59)
        Me.btnCreateSetting.Name = "btnCreateSetting"
        Me.btnCreateSetting.Size = New System.Drawing.Size(120, 23)
        Me.btnCreateSetting.TabIndex = 0
        Me.btnCreateSetting.Text = "Create Setting"
        Me.btnCreateSetting.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Panel2)
        Me.TabPage4.Controls.Add(Me.Panel1)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(713, 394)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Device Management"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.dgvDevices)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(3, 62)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(707, 329)
        Me.Panel2.TabIndex = 2
        '
        'dgvDevices
        '
        Me.dgvDevices.AutoGenerateColumns = False
        Me.dgvDevices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvDevices.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DeviceIDDataGridViewTextBoxColumn, Me.IMEIDataGridViewTextBoxColumn, Me.NotesDataGridViewTextBoxColumn, Me.PhoneNumberDataGridViewTextBoxColumn, Me.ApplicatinIDDataGridViewTextBoxColumn, Me.CreationDateDataGridViewTextBoxColumn})
        Me.dgvDevices.DataSource = Me.DeviceBindingSource
        Me.dgvDevices.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvDevices.Location = New System.Drawing.Point(0, 0)
        Me.dgvDevices.Name = "dgvDevices"
        Me.dgvDevices.Size = New System.Drawing.Size(707, 329)
        Me.dgvDevices.TabIndex = 0
        '
        'DeviceIDDataGridViewTextBoxColumn
        '
        Me.DeviceIDDataGridViewTextBoxColumn.DataPropertyName = "DeviceID"
        Me.DeviceIDDataGridViewTextBoxColumn.HeaderText = "DeviceID"
        Me.DeviceIDDataGridViewTextBoxColumn.Name = "DeviceIDDataGridViewTextBoxColumn"
        '
        'IMEIDataGridViewTextBoxColumn
        '
        Me.IMEIDataGridViewTextBoxColumn.DataPropertyName = "IMEI"
        Me.IMEIDataGridViewTextBoxColumn.HeaderText = "IMEI"
        Me.IMEIDataGridViewTextBoxColumn.Name = "IMEIDataGridViewTextBoxColumn"
        '
        'NotesDataGridViewTextBoxColumn
        '
        Me.NotesDataGridViewTextBoxColumn.DataPropertyName = "notes"
        Me.NotesDataGridViewTextBoxColumn.HeaderText = "notes"
        Me.NotesDataGridViewTextBoxColumn.Name = "NotesDataGridViewTextBoxColumn"
        '
        'PhoneNumberDataGridViewTextBoxColumn
        '
        Me.PhoneNumberDataGridViewTextBoxColumn.DataPropertyName = "PhoneNumber"
        Me.PhoneNumberDataGridViewTextBoxColumn.HeaderText = "PhoneNumber"
        Me.PhoneNumberDataGridViewTextBoxColumn.Name = "PhoneNumberDataGridViewTextBoxColumn"
        '
        'ApplicatinIDDataGridViewTextBoxColumn
        '
        Me.ApplicatinIDDataGridViewTextBoxColumn.DataPropertyName = "ApplicatinID"
        Me.ApplicatinIDDataGridViewTextBoxColumn.HeaderText = "ApplicatinID"
        Me.ApplicatinIDDataGridViewTextBoxColumn.Name = "ApplicatinIDDataGridViewTextBoxColumn"
        '
        'CreationDateDataGridViewTextBoxColumn
        '
        Me.CreationDateDataGridViewTextBoxColumn.DataPropertyName = "CreationDate"
        Me.CreationDateDataGridViewTextBoxColumn.HeaderText = "CreationDate"
        Me.CreationDateDataGridViewTextBoxColumn.Name = "CreationDateDataGridViewTextBoxColumn"
        '
        'DeviceBindingSource
        '
        Me.DeviceBindingSource.DataSource = GetType(FMS.Business.DataObjects.Device)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Button5)
        Me.Panel1.Controls.Add(Me.Button4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(707, 59)
        Me.Panel1.TabIndex = 1
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(5, 15)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(132, 23)
        Me.Button5.TabIndex = 0
        Me.Button5.Text = "Load Data"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(558, 15)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(132, 23)
        Me.Button4.TabIndex = 0
        Me.Button4.Text = "Save"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.Panel4)
        Me.TabPage5.Controls.Add(Me.Panel3)
        Me.TabPage5.Location = New System.Drawing.Point(4, 22)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(713, 394)
        Me.TabPage5.TabIndex = 4
        Me.TabPage5.Text = "Applications"
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.DataGridView2)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(3, 68)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(707, 323)
        Me.Panel4.TabIndex = 1
        '
        'DataGridView2
        '
        Me.DataGridView2.AllowUserToAddRows = False
        Me.DataGridView2.AllowUserToDeleteRows = False
        Me.DataGridView2.AutoGenerateColumns = False
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ApplicationNameDataGridViewTextBoxColumn, Me.ApplicationIDDataGridViewTextBoxColumn})
        Me.DataGridView2.DataSource = Me.ApplicationBindingSource
        Me.DataGridView2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView2.Location = New System.Drawing.Point(0, 0)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.ReadOnly = True
        Me.DataGridView2.Size = New System.Drawing.Size(707, 323)
        Me.DataGridView2.TabIndex = 0
        '
        'ApplicationNameDataGridViewTextBoxColumn
        '
        Me.ApplicationNameDataGridViewTextBoxColumn.DataPropertyName = "ApplicationName"
        Me.ApplicationNameDataGridViewTextBoxColumn.HeaderText = "ApplicationName"
        Me.ApplicationNameDataGridViewTextBoxColumn.Name = "ApplicationNameDataGridViewTextBoxColumn"
        Me.ApplicationNameDataGridViewTextBoxColumn.ReadOnly = True
        Me.ApplicationNameDataGridViewTextBoxColumn.Width = 250
        '
        'ApplicationIDDataGridViewTextBoxColumn
        '
        Me.ApplicationIDDataGridViewTextBoxColumn.DataPropertyName = "ApplicationID"
        Me.ApplicationIDDataGridViewTextBoxColumn.HeaderText = "ApplicationID"
        Me.ApplicationIDDataGridViewTextBoxColumn.Name = "ApplicationIDDataGridViewTextBoxColumn"
        Me.ApplicationIDDataGridViewTextBoxColumn.ReadOnly = True
        Me.ApplicationIDDataGridViewTextBoxColumn.Width = 200
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Button6)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(3, 3)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(707, 65)
        Me.Panel3.TabIndex = 0
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(22, 21)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(75, 23)
        Me.Button6.TabIndex = 0
        Me.Button6.Text = "Load Applications"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'TabPage6
        '
        Me.TabPage6.Controls.Add(Me.btnSimulationSettingsSave)
        Me.TabPage6.Controls.Add(Me.btnSimulationSettingsLoad)
        Me.TabPage6.Controls.Add(Me.dgvSimulationSettings)
        Me.TabPage6.Location = New System.Drawing.Point(4, 22)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage6.Size = New System.Drawing.Size(713, 394)
        Me.TabPage6.TabIndex = 5
        Me.TabPage6.Text = "SimulationSettings"
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'dgvSimulationSettings
        '
        Me.dgvSimulationSettings.AutoGenerateColumns = False
        Me.dgvSimulationSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvSimulationSettings.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SimulatorSettingIDDataGridViewTextBoxColumn, Me.SourceDeviceIDDataGridViewTextBoxColumn, Me.DestinationDeviceIDDataGridViewTextBoxColumn, Me.StartTimeDataGridViewTextBoxColumn, Me.EndTimeDataGridViewTextBoxColumn})
        Me.dgvSimulationSettings.DataSource = Me.SimulatorSettingBindingSource
        Me.dgvSimulationSettings.Location = New System.Drawing.Point(21, 91)
        Me.dgvSimulationSettings.Name = "dgvSimulationSettings"
        Me.dgvSimulationSettings.Size = New System.Drawing.Size(667, 284)
        Me.dgvSimulationSettings.TabIndex = 0
        '
        'SimulatorSettingBindingSource
        '
        Me.SimulatorSettingBindingSource.DataSource = GetType(FMS.Business.DataObjects.SimulatorSetting)
        '
        'SimulatorSettingIDDataGridViewTextBoxColumn
        '
        Me.SimulatorSettingIDDataGridViewTextBoxColumn.DataPropertyName = "SimulatorSettingID"
        Me.SimulatorSettingIDDataGridViewTextBoxColumn.HeaderText = "SimulatorSettingID"
        Me.SimulatorSettingIDDataGridViewTextBoxColumn.Name = "SimulatorSettingIDDataGridViewTextBoxColumn"
        '
        'SourceDeviceIDDataGridViewTextBoxColumn
        '
        Me.SourceDeviceIDDataGridViewTextBoxColumn.DataPropertyName = "SourceDeviceID"
        Me.SourceDeviceIDDataGridViewTextBoxColumn.HeaderText = "SourceDeviceID"
        Me.SourceDeviceIDDataGridViewTextBoxColumn.Name = "SourceDeviceIDDataGridViewTextBoxColumn"
        '
        'DestinationDeviceIDDataGridViewTextBoxColumn
        '
        Me.DestinationDeviceIDDataGridViewTextBoxColumn.DataPropertyName = "DestinationDeviceID"
        Me.DestinationDeviceIDDataGridViewTextBoxColumn.HeaderText = "DestinationDeviceID"
        Me.DestinationDeviceIDDataGridViewTextBoxColumn.Name = "DestinationDeviceIDDataGridViewTextBoxColumn"
        '
        'StartTimeDataGridViewTextBoxColumn
        '
        Me.StartTimeDataGridViewTextBoxColumn.DataPropertyName = "StartTime"
        Me.StartTimeDataGridViewTextBoxColumn.HeaderText = "StartTime"
        Me.StartTimeDataGridViewTextBoxColumn.Name = "StartTimeDataGridViewTextBoxColumn"
        '
        'EndTimeDataGridViewTextBoxColumn
        '
        Me.EndTimeDataGridViewTextBoxColumn.DataPropertyName = "EndTime"
        Me.EndTimeDataGridViewTextBoxColumn.HeaderText = "EndTime"
        Me.EndTimeDataGridViewTextBoxColumn.Name = "EndTimeDataGridViewTextBoxColumn"
        '
        'btnSimulationSettingsLoad
        '
        Me.btnSimulationSettingsLoad.Location = New System.Drawing.Point(31, 35)
        Me.btnSimulationSettingsLoad.Name = "btnSimulationSettingsLoad"
        Me.btnSimulationSettingsLoad.Size = New System.Drawing.Size(119, 23)
        Me.btnSimulationSettingsLoad.TabIndex = 1
        Me.btnSimulationSettingsLoad.Text = "Load"
        Me.btnSimulationSettingsLoad.UseVisualStyleBackColor = True
        '
        'btnSimulationSettingsSave
        '
        Me.btnSimulationSettingsSave.Location = New System.Drawing.Point(171, 35)
        Me.btnSimulationSettingsSave.Name = "btnSimulationSettingsSave"
        Me.btnSimulationSettingsSave.Size = New System.Drawing.Size(119, 23)
        Me.btnSimulationSettingsSave.TabIndex = 1
        Me.btnSimulationSettingsSave.Text = "Save"
        Me.btnSimulationSettingsSave.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(721, 420)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Form1"
        Me.Text = "FMS Administration"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.dgvApplicationSettings, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SettingBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ApplicationBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.dgvDevices, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DeviceBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.TabPage5.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        CType(Me.dgvSimulationSettings, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SimulatorSettingBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents txtCreateSetting As System.Windows.Forms.TextBox
    Friend WithEvents btnCreateSetting As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCreateApplication As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnCreateApplication As System.Windows.Forms.Button
    Friend WithEvents cbSelectedApplication As System.Windows.Forms.ComboBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents dgvApplicationSettings As System.Windows.Forms.DataGridView
    Friend WithEvents ApplicationBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ApplciationNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ValueDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SettingBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents btnUpdateSetting As System.Windows.Forms.Button
    Friend WithEvents txtFeatureDesc As System.Windows.Forms.TextBox
    Friend WithEvents txtFeatureName As System.Windows.Forms.TextBox
    Friend WithEvents btnCreateFeature As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents dgvDevices As System.Windows.Forms.DataGridView
    Friend WithEvents DeviceIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents IMEIDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents NotesDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PhoneNumberDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ApplicatinIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CreationDateDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DeviceBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents ApplicationNameDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ApplicationIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents btnCreateApp As System.Windows.Forms.Button
    Friend WithEvents txtApplicationName As System.Windows.Forms.TextBox
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents dgvSimulationSettings As System.Windows.Forms.DataGridView
    Friend WithEvents btnSimulationSettingsSave As System.Windows.Forms.Button
    Friend WithEvents btnSimulationSettingsLoad As System.Windows.Forms.Button
    Friend WithEvents SimulatorSettingIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SourceDeviceIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DestinationDeviceIDDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StartTimeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EndTimeDataGridViewTextBoxColumn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SimulatorSettingBindingSource As System.Windows.Forms.BindingSource

End Class