
Namespace DataObjects

    Public Class PaidFeature

        Public Property ApplicationPaidFeaturesID As Guid
        Public Property ApplicationID As Guid
        Public Property PaidFeatureDescription As String


#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(dbobj As FMS.Business.PaidFeature)

            With dbobj

                ApplicationPaidFeaturesID = .ApplicationPaidFeaturesID
                ApplicationID = .ApplicationID
                PaidFeatureDescription = .PaidFeatureDescription

            End With

        End Sub

#End Region

#Region "CRUD"

        'Public Shared Function Insert(at As DataObjects.PaidFeature) As Guid

        '    Dim newObj As New FMS.Business.PaidFeature

        '    Try

        '        If at.ApplicationID = Guid.Empty Then Throw New Exception("the applicationID was left blank.")

        '        With newObj
        '            .ApplicationPaidFeaturesID = If(at.ApplicationPaidFeaturesID = Guid.Empty, Guid.NewGuid, at.ApplicationPaidFeaturesID)
        '            .ApplicationID = at.ApplicationID
        '            .PaidFeatureDescription = at.PaidFeatureDescription
        '        End With

        '        SingletonAccess.FMSDataContextContignous.PaidFeatures.InsertOnSubmit(newObj)
        '        SingletonAccess.FMSDataContextContignous.SubmitChanges()

        '        Return newObj.ApplicationPaidFeaturesID

        '    Catch ex As Exception
        '        Throw
        '    End Try
        'End Function

        'Public Shared Sub Update(at As DataObjects.PaidFeature)

        '    Dim foundObj As FMS.Business.PaidFeature

        '    foundObj = SingletonAccess.FMSDataContextContignous.PaidFeatures. _
        '                        Where(Function(x) x.ApplicationPaidFeaturesID = at.ApplicationPaidFeaturesID).SingleOrDefault

        '    If foundObj Is Nothing Then Throw New Exception( _
        '                        "Could not find related ""PaidFeature"" object in database")

        '    With foundObj

        '        .ApplicationPaidFeaturesID = at.ApplicationPaidFeaturesID
        '        .PaidFeatureDescription = at.PaidFeatureDescription

        '    End With

        '    SingletonAccess.FMSDataContextContignous.SubmitChanges()

        'End Sub

        'Public Shared Sub Delete(at As DataObjects.PaidFeature)

        '    Dim foundObj As FMS.Business.PaidFeature

        '    foundObj = SingletonAccess.FMSDataContextContignous.PaidFeatures. _
        '                        Where(Function(x) x.ApplicationPaidFeaturesID = at.ApplicationPaidFeaturesID).SingleOrDefault

        '    If foundObj Is Nothing Then Throw New Exception( _
        '                        "Could not find related ""PaidFeature"" object in database")


        '    SingletonAccess.FMSDataContextContignous.PaidFeatures.DeleteOnSubmit(foundObj)
        '    SingletonAccess.FMSDataContextContignous.SubmitChanges()

        'End Sub

#End Region
#Region "Get & Misc"
        Public Shared Function GetAllPaidFeatures() As List(Of String)
            Return New List(Of String)({
                                       "Bookings"
                                   })
        End Function
        Public Shared Function HasAccessToPaidFeatures(applicationid As Guid, feature As String) As Boolean
            Return (From x In SingletonAccess.FMSDataContextNew.PaidFeatures _
                     Where x.ApplicationID = applicationid _
                     Select x.PaidFeatureDescription).ToList.Contains(feature)
        End Function
#End Region
    End Class

End Namespace