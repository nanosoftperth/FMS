Namespace DataObjects


    Public Class AuthenticationToken

#Region "properties"

        Public Property TokenID As Guid
        Public Property ApplicationID As Guid
        Public Property UserID As Guid
        Public Property ExpiryDate As Date
        Public Property StartDate As Date
        Public Property TokenType As String
        Public Property isUsedForChangePassword As Boolean
#End Region

#Region "constructors"

        Public Sub New(x As Business.AuthenticationToken)

            With x

                Me.TokenId = x.TokenId
                Me.ApplicationID = x.ApplicationID
                Me.UserID = x.UserID
                Me.ExpiryDate = x.ExpiryDate.timezoneToClient
                Me.StartDate = x.StartDate.timezoneToClient
                Me.TokenType = x.TokenType
                Me.isUsedForChangePassword = If(x.isUsedForChangePassword.HasValue, x.isUsedForChangePassword.Value, False)
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

            With New LINQtoSQLClassesDataContext
                With dbToken
                    .ApplicationID = t.ApplicationID
                    .ExpiryDate = t.ExpiryDate.timezoneToPerth
                    .StartDate = t.StartDate.timezoneToPerth
                    .TokenId = t.TokenID
                    .UserID = t.UserID
                    .TokenId = If(t.TokenID = Guid.Empty, Guid.NewGuid, t.TokenID)
                    .TokenType = t.TokenType
                    .isUsedForChangePassword = t.isUsedForChangePassword
                End With

                .AuthenticationTokens.InsertOnSubmit(dbToken)
                .SubmitChanges()
            End With

            Return dbToken.TokenId

        End Function
        Public Shared Function Update(t As DataObjects.AuthenticationToken) As Guid

            Dim dbToken As FMS.Business.AuthenticationToken

            With New LINQtoSQLClassesDataContext
                dbToken = .AuthenticationTokens. _
                                Where(Function(x) x.TokenId = t.TokenID).SingleOrDefault

                With dbToken
                    .ApplicationID = t.ApplicationID
                    .ExpiryDate = t.ExpiryDate.timezoneToPerth
                    .StartDate = t.StartDate.timezoneToPerth
                    .TokenId = t.TokenID
                    .UserID = t.UserID
                    .TokenType = t.TokenType
                    .isUsedForChangePassword = t.isUsedForChangePassword
                End With

                .SubmitChanges()

            End With

            Return dbToken.TokenId

        End Function

#End Region

#Region "gets & sets"

        Public Shared Function GetTokenFromID(tokenID As Guid) As AuthenticationToken


            Dim t As Business.AuthenticationToken = SingletonAccess.FMSDataContextNew. _
                            AuthenticationTokens.Where(Function(x) x.TokenId = tokenID).SingleOrDefault()

            Return If(t Is Nothing, Nothing, New DataObjects.AuthenticationToken(t))

        End Function
        Public Shared Function GetFPTokenFromID(tokenID As Guid) As AuthenticationToken


            Dim t As Business.AuthenticationToken = SingletonAccess.FMSDataContextNew. _
                            AuthenticationTokens.Where(Function(x) x.TokenId = tokenID And
                                                           x.ExpiryDate > Now And
                                                           x.TokenType = "CP" And
                                                           x.isUsedForChangePassword = True).SingleOrDefault()

            Return If(t Is Nothing, Nothing, New DataObjects.AuthenticationToken(t))

        End Function
        Public Shared Function GetExistingTokenIdForUser(userID As Guid) As Guid
            Dim t As Business.AuthenticationToken = SingletonAccess.FMSDataContextNew. _
                            AuthenticationTokens.Where(Function(x) x.UserID = userID And
                                                           x.ExpiryDate > Now And
                                                           x.TokenType = "CP" And
                                                           x.isUsedForChangePassword = True).SingleOrDefault()

            Return If(t Is Nothing, Nothing, t.TokenId)

        End Function
#End Region

    End Class

End Namespace



