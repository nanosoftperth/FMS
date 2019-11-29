Imports System.Net
Imports System.Web.Http
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net.Http
Imports FMS.Business.DataObjects
Imports System.Web.Security


Namespace Controllers

    Public Class UsersController
        Inherits ApiController


        ''' <summary>
        ''' this is by definition insecure, we need to enanble the session state here to allow the app to use the logged in user ID and not allow that to be dictated to.
        ''' </summary>
        ''' <param name="UserID"></param>
        ''' <param name="PreferenceName"></param>
        ''' <param name="Value"></param>
        ''' <returns>if the operation did not caus an exception (bool value)</returns>
        <HttpGet()>
        Public Function SetPreference(UserID As String, PreferenceName As String, Value As String)

            Dim OperationWasSuccessful As Boolean = True

            Try

                'Dim UserID As Guid = FMS.Business.ThisSession.User.UserId
                FMS.Business.DataObjects.UserPreference.SetUserPreference(Guid.Parse(UserID), PreferenceName, Value)

            Catch ex As Exception

                OperationWasSuccessful = False

            End Try

            Return OperationWasSuccessful

        End Function

        ' GET api/<controller>
        Public Function GetValues() As IEnumerable(Of String)
            Return New String() {"value1", "value2"}
        End Function

        ' GET api/<controller>/5
        Public Function GetValue(ByVal id As Integer) As String
            Return "value" & id
        End Function

        ' POST api/<controller>
        Public Sub PostValue(<FromBody()> ByVal value As String)

        End Sub

        ' PUT api/<controller>/5
        Public Sub PutValue(ByVal id As Integer, <FromBody()> ByVal value As String)

        End Sub

        ' DELETE api/<controller>/5
        Public Sub DeleteValue(ByVal id As Integer)

        End Sub
    End Class

End Namespace

