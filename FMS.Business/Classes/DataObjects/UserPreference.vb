Namespace DataObjects


    ''' <summary>
    ''' There is another table in SQL which details the 
    ''' different possible preferences. To add a new preference, this needs to be added manually using SQLMS
    ''' values taken from a stored procedure 
    ''' </summary>
    <Serializable()>
    Public Class UserPreference

        'taken from the userpreference table
        Public Property UserPreferenceID As Guid?
        Public Property UserID As Guid
        Public Property PreferenceID As Guid
        Public Property Value As String

        'taken from the preference table
        Public Property Name As String
        Public Property Description As String


        Public Sub New(sp As FMS.Business.usp_GetUserPreferencesResult)

            With sp

                Me.UserPreferenceID = .UserPreferenceID
                Me.UserID = .UserID.Value ' for some reason this is determined to be nullable by linq to sql, so we just always take the value as it should be there.
                Me.PreferenceID = .PreferenceID
                Me.Value = .Value.Trim
                Me.Name = .Name.Trim
                Me.Description = .Description.Trim

            End With


        End Sub

        Public Shared Function GetForUser(UserID As Guid) As List(Of FMS.Business.DataObjects.UserPreference)

            Using dbContext As New FMS.Business.LINQtoSQLClassesDataContext

                Return (From x In dbContext.usp_GetUserPreferences(UserID)
                        Select New FMS.Business.DataObjects.UserPreference(x)).ToList

            End Using


        End Function


        Public Shared Sub Update(up As DataObjects.UserPreference)

            With up
                SetUserPreference(.UserID, .Name, .Value)
            End With

        End Sub


        Public Shared Sub SetUserPreference(userid As Guid, PreferenceName As String, value As String)


            'create databse connection instancen 
            Using dbContext As New FMS.Business.LINQtoSQLClassesDataContext


                'get the prefence by name 
                Dim preference As FMS.Business.DataObjects.Preference =
                    (From p In dbContext.Preferences
                     Where p.Name.ToLower().Equals(PreferenceName.ToLower)
                     Select New FMS.Business.DataObjects.Preference(p)).Single

                Dim preferenceID As Guid = preference.PreferenceID

                Dim userPref As FMS.Business.UserPreference

                userPref = dbContext.UserPreferences.Where(Function(x) x.PreferenceID = preferenceID And x.UserID = userid).SingleOrDefault

                If userPref Is Nothing Then
                    userPref = New FMS.Business.UserPreference
                    dbContext.UserPreferences.InsertOnSubmit(userPref)
                End If

                With userPref
                    .PreferenceID = preferenceID
                    .UserID = userid
                    .UserPreferenceID = IIf(.UserPreferenceID = Guid.Empty, Guid.NewGuid, .UserPreferenceID)
                    .Value = value
                End With

                dbContext.SubmitChanges()

            End Using



        End Sub

    End Class


End Namespace


