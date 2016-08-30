Namespace DataObjects

    Public Class AlertTypeOccurance

#Region "properties"
        Public Property AlertTypeOccuranceID As Guid
        Public Property AlertTypeID As Guid
        Public Property GeoFenceCollisionID As Guid
        Public Property SubscriberTypeStr As String
        Public Property SubscriberTypeName As String
        Public Property SubscriberNativeID As Guid
        Public Property Emails As String
        Public Property Texts As String
        Public Property DateSent As String
        Public Property ApplicationGeoFenceID As Guid
        Public Property ApplicationGeoFenceName As String
        Public Property DriverName As String
        Public Property MessageContent As String

#End Region

#Region "constructors"


        Public Sub New(x As FMS.Business.AlertTypeOccurance)

            With x
                Me.AlertTypeID = .AlertTypeID
                Me.AlertTypeOccuranceID = .AlertTypeOccuranceID
                Me.DateSent = .DateSend
                Me.Emails = .Emails
                Me.GeoFenceCollisionID = .GeoFenceCollisionID
                Me.SubscriberNativeID = .SubscriberNativeID
                Me.SubscriberTypeName = .SubscriberTypeName
                Me.SubscriberTypeStr = .SubscriberTypeStr
                Me.Texts = .Texts
                Me.ApplicationGeoFenceID = .ApplicationGeoFenceID
                Me.ApplicationGeoFenceName = .ApplicationGeoFenceName
                Me.MessageContent = .MessageContent
                Me.DriverName = .DriverName
            End With

        End Sub

        ''' <summary>
        ''' for serialisation purposes
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub

#End Region

#Region "CRUD"


        Public Shared Function Create(newObj As DataObjects.AlertTypeOccurance) As Guid

            Dim dbObj As New Business.AlertTypeOccurance

            With newObj
                dbObj.AlertTypeID = .AlertTypeID
                dbObj.AlertTypeOccuranceID = If(.AlertTypeOccuranceID = Guid.Empty, New Guid, .AlertTypeOccuranceID)
                dbObj.DateSend = .DateSent
                dbObj.Emails = .Emails
                dbObj.GeoFenceCollisionID = .GeoFenceCollisionID
                dbObj.SubscriberNativeID = .SubscriberNativeID
                dbObj.SubscriberTypeName = .SubscriberTypeName
                dbObj.SubscriberTypeStr = .SubscriberTypeStr
                dbObj.Texts = .Texts
                dbObj.DriverName = .DriverName
                dbObj.ApplicationGeoFenceID = .ApplicationGeoFenceID
                dbObj.ApplicationGeoFenceName = .ApplicationGeoFenceName
                dbObj.MessageContent = .MessageContent

            End With

            SingletonAccess.FMSDataContextContignous.AlertTypeOccurances.InsertOnSubmit(dbObj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Function

        Public Shared Sub Update(obj As DataObjects.AlertTypeOccurance)
            Throw New Exception("""Update"" not implemented")
        End Sub

        Public Sub Delete(obj As DataObjects.AlertTypeOccurance)
            Throw New Exception("""Delete"" not implemented")
        End Sub

#End Region

#Region "get/sets"

        Public Shared Function GetAllForApplication(applicationID As Guid) As List(Of DataObjects.AlertTypeOccurance)

            'TODO: we should filter this by time , this will become slow eventually

            Dim retobj As List(Of DataObjects.AlertTypeOccurance) = _
                    (From alt In SingletonAccess.FMSDataContextContignous.AlertTypes _
                      Join ato In SingletonAccess.FMSDataContextContignous.AlertTypeOccurances _
                      On alt.ApplicationAlertTypeID Equals ato.AlertTypeID _
                      Where alt.ApplicationID = applicationID _
                      Select New DataObjects.AlertTypeOccurance(ato)).ToList


            Return retobj

        End Function



#End Region

    End Class


End Namespace


