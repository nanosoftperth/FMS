
Imports System.Linq

Namespace DataObjects

    <Serializable()>
    Public Class Preference


        Public Property PreferenceID As Guid
        Public Property Name As String
        Public Property Description As String
        Public Property ValueType As String
        Public Property DefaultValue As String



        Public Sub New(p As FMS.Business.Preference)


            With p

                Me.PreferenceID = .PreferenceID
                Me.Name = .Name
                Me.Description = .Description
                Me.ValueType = .ValueType
                Me.DefaultValue = .DefaultValue

            End With

        End Sub


        Public Shared Function GetAllPreferences() As List(Of FMS.Business.DataObjects.Preference)

            Return (From x In SingletonAccess.FMSDataContextContignous.Preferences
                    Select New DataObjects.Preference(x)
                        ).ToList

        End Function

    End Class



End Namespace


