Namespace DataObjects


    Public Class AuthenticationToken

#Region "properties"

        Public Property TokenID As Guid
        Public Property ApplicationID As Guid
        Public Property UserID As Guid
        Public Property ExpiryDate As Date
        Public Property StartDate As Date

#End Region

#Region "constructors"

        Public Sub New(x As Business.AuthenticationToken)

            With x

                Me.TokenId = x.TokenId
                Me.ApplicationID = x.ApplicationID
                Me.UserID = x.UserID
                Me.ExpiryDate = x.ExpiryDate.timezoneToClient
                Me.StartDate = x.StartDate.timezoneToClient

            End With

        End Sub

        ''' <summary>
        ''' for serialisation only
        ''' </summary>
        Public Sub New()

        End Sub


#End Region


#Region "CRUD"

        Public Shared Function Create(t As DataObjects.AuthenticationToken) As Guid

            Dim dbToken As New Business.AuthenticationToken

            With dbToken
                .ApplicationID = t.ApplicationID
                .ExpiryDate = t.ExpiryDate.timezoneToPerth
                .StartDate = t.StartDate.timezoneToPerth
                .TokenId = t.TokenID
                .UserID = t.UserID
                .TokenId = If(t.TokenID = Guid.Empty, Guid.NewGuid, t.TokenID)
            End With

            SingletonAccess.FMSDataContextContignous.AuthenticationTokens.InsertOnSubmit(dbToken)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

            Return dbToken.TokenId

        End Function

#End Region

#Region "gets & sets"

        Public Shared Function GetTokenFromID(tokenID As Guid) As AuthenticationToken


            Dim t As Business.AuthenticationToken = SingletonAccess.FMSDataContextNew. _
                            AuthenticationTokens.Where(Function(x) x.TokenId = tokenID).SingleOrDefault()

            Return If(t Is Nothing, Nothing, New DataObjects.AuthenticationToken(t))

        End Function

#End Region

    End Class

End Namespace



