
Namespace DataObjects

    Public Class ApplicationFeature

        Public Property ApplicationFeatureID As Guid
        Public Property ApplicationID As Guid
        Public Property FeatureID As Guid

        Public Sub New(apr As FMS.Business.ApplicationFeature)

            With apr
                Me.ApplicationFeatureID = .ApplicationFeatureID
                Me.ApplicationID = .ApplicationID
                Me.FeatureID = .FeatureID
            End With

        End Sub

        Public Sub New()

        End Sub

        Public Shared Sub insert(afr As DataObjects.ApplicationFeature)

            Dim x As New FMS.Business.ApplicationFeature

            x.ApplicationFeatureID = Guid.NewGuid
            x.ApplicationID = afr.ApplicationID
            x.FeatureID = afr.FeatureID

            SingletonAccess.FMSDataContextContignous.ApplicationFeatures.InsertOnSubmit(x)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub update(afr As DataObjects.ApplicationFeature)

            Dim x As FMS.Business.ApplicationFeature = _
                            (From i In SingletonAccess.FMSDataContextContignous.ApplicationFeatures _
                             Where i.ApplicationFeatureID = afr.ApplicationFeatureID).Single

            x.FeatureID = afr.FeatureID

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub delete(afr As DataObjects.ApplicationFeature)

            Dim x As FMS.Business.ApplicationFeature = _
                           (From i In SingletonAccess.FMSDataContextContignous.ApplicationFeatures _
                            Where i.ApplicationFeatureID = afr.ApplicationFeatureID).Single

            SingletonAccess.FMSDataContextContignous.ApplicationFeatures.DeleteOnSubmit(x)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub
        Public Shared Sub delete(appid As Guid)

            Dim x = (From i In SingletonAccess.FMSDataContextContignous.ApplicationFeatures _
                            Where i.ApplicationID = appid)

            SingletonAccess.FMSDataContextContignous.ApplicationFeatures.DeleteAllOnSubmit(x)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Function GetAllApplicationFeatures(appid As Guid) As List(Of ApplicationFeature)

            Dim retlst As List(Of DataObjects.ApplicationFeature) = _
                            (From i In SingletonAccess.FMSDataContextNew.ApplicationFeatures _
                                Where i.ApplicationID = appid
                                 Select New DataObjects.ApplicationFeature(i)).ToList
            Return retlst
        End Function
        Public Shared Function GetAllFeatures(appid As Guid)

            Dim ApplicationFeatureList = (From i In SingletonAccess.FMSDataContextNew.ApplicationFeatures _
                                Where i.ApplicationID = appid
                                 Select i.FeatureID).ToList
            Dim retlst = _
                            (From i In SingletonAccess.FMSDataContextNew.Features _
                                 Select New FeaturesForApplication() With {
                                     .IsInApplicationFeature = (ApplicationFeatureList.Contains(i.FeatureID)),
                                     .FeatureID = i.FeatureID,
                                     .FeatureName = i.FeatureName,
                                     .FeatureDescription = i.FeatureDescription
                                            }).ToList
            Return retlst
        End Function
    End Class
    Public Class FeaturesForApplication

        Sub New()
            ' TODO: Complete member initialization 
        End Sub

        Public Property IsInApplicationFeature As Boolean
        Public Property FeatureID As Guid
        Public Property FeatureName As String
        Public Property FeatureDescription As String
    End Class
End Namespace
