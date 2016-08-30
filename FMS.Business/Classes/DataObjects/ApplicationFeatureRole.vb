
Namespace DataObjects

    Public Class ApplicationFeatureRole

        Public Property ApplicationFeatureRoleID As Guid
        Public Property ApplicationID As Guid
        Public Property FeatureID As Guid
        Public Property RoleID As Guid

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

    End Class

End Namespace
