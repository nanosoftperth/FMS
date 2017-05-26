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

