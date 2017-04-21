
Namespace DataObjects

    Public Class ApplicationFeatureRole

        Public Property ApplicationFeatureRoleID As Guid
        Public Property ApplicationID As Guid
        Public Property FeatureID As Guid
        Public Property RoleID As Guid
        Public Property FetaureName As String
        Public Property RoleName As String
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

            Dim retlst As List(Of DataObjects.ApplicationFeatureRole) = _
                            (From i In SingletonAccess.FMSDataContextNew.ApplicationFeatureRoles _
                                Where i.ApplicationID = appid
                                 Select New DataObjects.ApplicationFeatureRole(i)).ToList
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
    End Class

End Namespace
