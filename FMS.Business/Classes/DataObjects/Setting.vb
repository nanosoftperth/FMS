
Namespace DataObjects

    Public Class Setting

#Region "properties"

        Public Property ApplicationID As Guid
        Public Property SettingID As Guid
        Public Property Value As String

        Public Property ApplicatiopnSettingValueID As Guid
       
        Public Property ValueObj() As Byte()

        Public Property ApplicationName As String

        Public Property Name As String

#End Region

#Region "constructors"
        ''' <summary>
        ''' for serialisation only
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub

        Public Sub New(x As FMS.Business.usp_GetSettingsForApplicationResult)

            Try

                Me.ApplicationID = x.ApplicationId
                Me.ApplicatiopnSettingValueID = If(x.ApplicationSettingValueID Is Nothing, Guid.NewGuid, x.ApplicationSettingValueID)
                Me.SettingID = x.SettingID
                Me.Value = x.Value
                Me.ApplicationName = x.ApplicationName
                Me.Name = x.Name

                'present the image
                If x.ValueObj IsNot Nothing Then Me.ValueObj = x.ValueObj.ToArray

            Catch ex As Exception
                Throw
            End Try

        End Sub


        ''' <summary>
        ''' Anti-pattern, sould not be used. This has been added as to avoid future confusion.
        ''' All settings are shoes without values when the application is first created (on the website)
        '''only the update function should be used to edit any of the default blank values. 
        ''' </summary>
        Public Shared Function Insert(x As FMS.Business.DataObjects.Setting)

            Throw New NotImplementedException("you cannot ""insert"" a new setting this way, it needs to be added to the setting table first so it can then be edited via the website.")

        End Function

        Public Shared Sub Update(x As FMS.Business.DataObjects.Setting)

            'check if the object already exists in the database, 
            'the primary key may have been "made up" as to give the 
            'devexpress datagridview a keyfield for binding.
            Dim dbobj As FMS.Business.ApplicationSettingValue = _
                                    SingletonAccess.FMSDataContextContignous.ApplicationSettingValues _
                                                .Where(Function(y) y.SettingID = x.SettingID And y.ApplicationID = x.ApplicationID _
                                                                                                                ).SingleOrDefault

            'If it does not, create it
            If dbobj Is Nothing Then

                dbobj = New FMS.Business.ApplicationSettingValue

                dbobj.ApplicationID = x.ApplicationID
                dbobj.ApplicationSettingValueID = Guid.NewGuid
                dbobj.SettingID = x.SettingID
                'edit the value to = what we want it to be
                dbobj.Value = x.Value
                'also, change the image binary (if needed)
                If x.ValueObj IsNot Nothing Then dbobj.ValueObj = New System.Data.Linq.Binary(x.ValueObj)

                SingletonAccess.FMSDataContextContignous.ApplicationSettingValues.InsertOnSubmit(dbobj)

            Else

                'also, change the image binary (if needed)
                If x.ValueObj IsNot Nothing Then dbobj.ValueObj = New System.Data.Linq.Binary(x.ValueObj)
                'edit the value to = what we want it to be
                dbobj.Value = x.Value

            End If

            'send changes to the db
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Sub New(name As String, id As Guid)


        End Sub

#End Region


        Public Sub SaveValueToDB(appID As Guid)

            'get setting from data context
            'Dim d As ApplicationSettingValue = (From i In SingletonAccess.FMSDataContextContignous.ApplicationSettingValues
            '                    Where i.SettingID = Me.ID And i.ApplicationID = appID).SingleOrDefault
            ''if does not exist, then create it
            'If d Is Nothing Then
            '    d = New ApplicationSettingValue()
            '    d.ApplicationID = appID
            '    d.SettingID = Me.ID
            '    d.ApplicationSettingValueID = Guid.NewGuid
            '    SingletonAccess.FMSDataContextContignous.ApplicationSettingValues.InsertOnSubmit(d)
            '    SingletonAccess.FMSDataContextContignous.SubmitChanges()
            'End If

            'd = (From i In SingletonAccess.FMSDataContextContignous.ApplicationSettingValues Where i.ApplicationSettingValueID = d.ApplicationSettingValueID).Single
            'd.Value = Me.Value

            'SingletonAccess.FMSDataContextContignous.SubmitChanges()


        End Sub

        Public Shared Sub SetLogoForApplication(applicationname As String, data As Byte())

            Dim setting As Setting = GetSettingsForApplication(applicationname) _
                                            .Where(Function(x) x.Name = "Logo").Single()

            setting.ValueObj = data

            Update(setting)

        End Sub

        Public Shared Function GetLogoForApplication(ApplicationName As String) As Byte()


            Dim uspr As List(Of DataObjects.Setting) = GetSettingsForApplication(ApplicationName)

            Dim x As DataObjects.Setting = (From y In uspr Where y.Name = "Logo").Single

            If x.ValueObj Is Nothing Then Return Nothing Else Return x.ValueObj

        End Function



        Public Shared Function GetSettingsForApplication_withoutImages(applicationid As Guid) As List(Of Setting)

            Return GetSettingsForApplication(applicationid, False)

        End Function

        Public Shared Function GetSettingsForApplication(applicationid As Guid, Optional returnLogoObj As Boolean = True) As List(Of Setting)

            Dim appName As String = DataObjects.Application.GetFromAppID(applicationid).ApplicationName

            Return GetSettingsForApplication(appName, returnLogoObj)

        End Function

        Public Shared Function GetSettingsForApplication(applicationName As String, Optional returnLogoObj As Boolean = True) As List(Of Setting)

            Using dataContext As New LINQtoSQLClassesDataContext


                Dim uspr As List(Of DataObjects.Setting) =
                      (From i In dataContext.usp_GetSettingsForApplication(applicationName)
                       Select New FMS.Business.DataObjects.Setting(i)).ToList

                If Not returnLogoObj Then uspr = (From s In uspr Where s.Name <> "Logo").ToList

                Return uspr

            End Using


        End Function

        ''' <summary>
        ''' Specific function to return the value of the "allowselfregistration" application setting
        ''' the default is always True, an admin has to explicitly switch it off using the application settings
        ''' after the application has been initially setup / configured
        ''' </summary>
        Public Shared Function GetAllowSelfRegistration(applicationName As String) As Boolean

            Using dataContext As New LINQtoSQLClassesDataContext


                Dim val = (From i In dataContext.usp_GetSettingsForApplication(applicationName)
                           Where i.Name.ToLower = "allowselfregistration").SingleOrDefault

                Return If(val Is Nothing OrElse String.IsNullOrEmpty(val.Value),
                                        True, CBool(val.Value))

            End Using

        End Function

    End Class


End Namespace


