
Namespace DataObjects

    Public Class ApplicationFeatureRole

        Public Property ApplicationFeatureRoleID As Guid
        Public Property ApplicationID As Guid
        Public Property FeatureID As Guid
        Public Property RoleID As Guid
        Public Property FetaureName As String
        Public Property RoleName As String
        Public Property Vehicles As Object      'for UW 226 (Add feature allowing filtering of vehicles on the map) - testing
        
        Public Sub New(apr As FMS.Business.ApplicationFeatureRole)
            With apr
                Me.ApplicationFeatureRoleID = .ApplicationFeatureRoledID
                Me.ApplicationID = .ApplicationID
                Me.FeatureID = .FeatureID
                Me.RoleID = .RoleID
            End With
        End Sub
        Public Sub New()

        End Sub

        Public Shared Sub insert(afr As DataObjects.ApplicationFeatureRole)

            Dim x As New FMS.Business.ApplicationFeatureRole

            x.ApplicationFeatureRoledID = Guid.NewGuid
            x.ApplicationID = afr.ApplicationID
            x.FeatureID = afr.FeatureID
            x.RoleID = afr.RoleID
            
            SingletonAccess.FMSDataContextContignous.ApplicationFeatureRoles.InsertOnSubmit(x)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub update(afr As DataObjects.ApplicationFeatureRole)

            Dim x As FMS.Business.ApplicationFeatureRole = _
                            (From i In SingletonAccess.FMSDataContextContignous.ApplicationFeatureRoles _
                             Where i.ApplicationFeatureRoledID = afr.ApplicationFeatureRoleID).Single

            x.FeatureID = afr.FeatureID
            x.RoleID = afr.RoleID
            
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Sub delete(afr As DataObjects.ApplicationFeatureRole)

            Dim x As FMS.Business.ApplicationFeatureRole = _
                           (From i In SingletonAccess.FMSDataContextContignous.ApplicationFeatureRoles _
                            Where i.ApplicationFeatureRoledID = afr.ApplicationFeatureRoleID).Single

            SingletonAccess.FMSDataContextContignous.ApplicationFeatureRoles.DeleteOnSubmit(x)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Function GetAllApplicationFeatureRoles(appid As Guid) As List(Of ApplicationFeatureRole)
            ' Old Code
            'Dim retlst As List(Of DataObjects.ApplicationFeatureRole) = _
            '                (From i In SingletonAccess.FMSDataContextNew.ApplicationFeatureRoles _
            '                    Where i.ApplicationID = appid
            '                    Select New DataObjects.ApplicationFeatureRole(i)).ToList

            ' Updated code for approval (this is to have value for Featurename and Rolename right away without going back to DB for another fetch)
            Dim retlst = (From afr In SingletonAccess.FMSDataContextContignous.ApplicationFeatureRoles
                         Join f In SingletonAccess.FMSDataContextContignous.Features
                         On afr.FeatureID Equals f.FeatureID
                         Join r In SingletonAccess.FMSDataContextContignous.aspnet_Roles
                         On afr.RoleID Equals r.RoleId
                         Where afr.ApplicationID = appid
                         Select New DataObjects.ApplicationFeatureRole With {
                             .ApplicationFeatureRoleID = afr.ApplicationFeatureRoledID,
                             .ApplicationID = afr.ApplicationID,
                             .FeatureID = afr.FeatureID,
                             .FetaureName = f.FeatureName,
                             .RoleID = afr.RoleID,
                             .RoleName = r.RoleName
                            }).OrderBy(Function(a) a.FetaureName).ToList()


            Return retlst


        End Function
        Public Shared Function GetAllApplicationFeatureRole(appid As Guid) As List(Of ApplicationFeatureRole)

            Dim retlst As New List(Of ApplicationFeatureRole)

            Dim retobj = _
                    (From alt In SingletonAccess.FMSDataContextContignous.ApplicationFeatureRoles _
                      Join ato In SingletonAccess.FMSDataContextContignous.Features _
                      On alt.FeatureID Equals ato.FeatureID _
                      Join rols In SingletonAccess.FMSDataContextContignous.aspnet_Roles _
                      On alt.RoleID Equals rols.RoleId
                      Where alt.ApplicationID = appid _
                      Select alt)

            For Each Item In retobj
                retlst.Add(New ApplicationFeatureRole() With
                                 {.FetaureName = Item.Feature.FeatureName,
                                   .RoleName = Item.aspnet_Role.RoleName
                                  })
            Next 
            Return retlst 

        End Function

        Public Shared Function GetVehiclesForRole(appid As Guid, featureID As Guid, roleID As Guid) As List(Of ApplicationFeatureRole)

            Dim retlst As List(Of DataObjects.ApplicationFeatureRole) = _
                            (From i In SingletonAccess.FMSDataContextNew.ApplicationFeatureRoles _
                                Where i.ApplicationID = appid And (i.FeatureID = featureID And i.RoleID = roleID)
                                 Select New DataObjects.ApplicationFeatureRole(i)).ToList
            Return retlst


        End Function


#Region "Vehicles pers role"
        'Public Shared Function FormatVehicles(vehicles As Object) As String
        '    Try
        '        Dim strValue As String = ""

        '        If (vehicles IsNot Nothing) Then

        '            Dim objType = vehicles.GetType()

        '            If (objType.Name() = "List`1") Then
        '                Dim oCtr = DirectCast(vehicles.Count, Integer)
        '                Dim vValue As Guid
        '                'Dim strValue As String = ""
        '                '(New System.Collections.Generic.Mscorlib_CollectionDebugView(Of Object)(businesslocation)).Items(0)
        '                If oCtr > 0 Then

        '                    For Each element As Object In vehicles
        '                        ' Avoid Nothing elements.
        '                        If element IsNot Nothing Then
        '                            vValue = element

        '                            If vValue.ToString() <> "00000000-0000-0000-0000-000000000000" Then
        '                                If strValue.Length > 0 Then
        '                                    strValue = strValue + "|" + vValue.ToString()
        '                                Else
        '                                    strValue = vValue.ToString()
        '                                End If
        '                            End If

        '                        End If
        '                    Next

        '                End If
        '            Else
        '                strValue = vehicles.ToString()
        '            End If

        '        End If

        '        Return strValue

        '    Catch ex As Exception
        '        Throw ex
        '    End Try
        'End Function
#End Region


    End Class

End Namespace
