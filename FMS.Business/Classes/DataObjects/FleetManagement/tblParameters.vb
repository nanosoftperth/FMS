Namespace DataObjects
    Public Class tblParameters

#Region "Properties / enums"
        Public Property ApplicationId As Guid
        Public Property ParId As String
        Public Property Field1 As String
        Public Property Field2 As String
        Public Property Field3 As String
        Public Property Field4 As String
        Public Property Field5 As String
        
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTbl As FMS.Business.tblParameter)
            With objTbl
                Me.ParId = .ParId
                Me.Field1 = .Field1
                Me.Field2 = .Field2
                Me.Field3 = .Field3
                Me.Field4 = .Field4
                Me.Field5 = .Field5
                Me.ApplicationId = .ApplicationId
            End With
        End Sub
#End Region

#Region "CRUD"
        Public Shared Sub Create(param As DataObjects.tblParameters)
            Dim AppID = ThisSession.ApplicationID

            Dim objParam As New FMS.Business.tblParameter
            With objParam
                .ParameterID = Guid.NewGuid
                .ParId = param.ParId
                .Field1 = param.Field1
                .ApplicationId = AppID

            End With
            SingletonAccess.FMSDataContextContignous.tblParameters.InsertOnSubmit(objParam)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub

        Public Shared Sub Update(param As DataObjects.tblParameters)
            Dim AppID = ThisSession.ApplicationID

            Dim objParam As FMS.Business.tblParameter = (From p In SingletonAccess.FMSDataContextContignous.tblParameters
                                                         Where p.ParId.Equals(param.ParId) And p.ApplicationId = AppID).Single
            With objParam
                .ParId = param.ParId
                .Field1 = param.Field1
                .Field2 = param.Field2
                .Field3 = param.Field3
                .Field4 = param.Field4
                .ApplicationId = AppID

            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblParameters)
            Dim AppID = ThisSession.ApplicationID

            Dim objParam = (From p In SingletonAccess.FMSDataContextContignous.tblParameters
                            Where p.ApplicationId = AppID
                            Select New DataObjects.tblParameters(p)).ToList()
            Return objParam
        End Function
#End Region


    End Class

End Namespace
