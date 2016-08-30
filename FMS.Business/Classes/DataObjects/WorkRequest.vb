
Namespace DataObjects


    Class WorkRequest

#Region "properties"

        Public Property WorkRequestID As Integer
        Public Property isBug As Boolean
        Public Property Name As String
        Public Property Description As String
        Public Property LoggedDate As DateTime
        Public Property EstimateDeliveryDate As DateTime
        Public Property DeveloperComment As String
        Public Property Complete As Boolean

#End Region


#Region "constructors"

        ''' <summary>
        ''' for serialization purposes only
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub

        Public Sub New(x As FMS.Business.WorkRequest)


            Me.Complete = x.Complete
            Me.Description = x.Description
            Me.DeveloperComment = x.DeveloperComment
            Me.EstimateDeliveryDate = x.EstimateDeliveryDate
            Me.isBug = x.isBug
            Me.LoggedDate = x.LoggedDate
            Me.Name = x.Name
            Me.WorkRequestID = x.WorkRequestID

        End Sub

#End Region

#Region "CRUD"


        Public Shared Function Create(x As DataObjects.WorkRequest) As Integer

            Dim dbobj As New FMS.Business.WorkRequest

            With dbobj
                .Complete = x.Complete
                .Description = x.Description
                .DeveloperComment = x.DeveloperComment
                .EstimateDeliveryDate = x.EstimateDeliveryDate
                .isBug = x.isBug
                .LoggedDate = x.LoggedDate
                .Name = x.Name
                '.WorkRequestID (ignore this (PK set as identity field in SQL)
            End With

            SingletonAccess.FMSDataContextContignous.WorkRequests.InsertOnSubmit(dbobj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

            Return dbobj.WorkRequestID

        End Function

        Public Shared Sub Update(x As DataObjects.WorkRequest)

            Dim dbobj As FMS.Business.WorkRequest = _
                 SingletonAccess.FMSDataContextContignous.WorkRequests.Where(Function(y) _
                                                     y.WorkRequestID = x.WorkRequestID).Single

            With dbobj

                .Complete = x.Complete
                .Description = x.Description
                .DeveloperComment = x.DeveloperComment
                .EstimateDeliveryDate = x.EstimateDeliveryDate
                .isBug = x.isBug
                .LoggedDate = x.LoggedDate
                .Name = x.Name
            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub Delete(x As DataObjects.WorkRequest)

            Dim dbobj As FMS.Business.WorkRequest = _
                SingletonAccess.FMSDataContextContignous.WorkRequests.Where(Function(y) _
                                                    y.WorkRequestID = x.WorkRequestID).Single

            SingletonAccess.FMSDataContextContignous.WorkRequests.DeleteOnSubmit(dbobj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

#End Region


#Region "get/sets"


        Public Shared Function GetAllWorkRequests() As List(Of DataObjects.WorkRequest)

            Return SingletonAccess.FMSDataContextNew.WorkRequests.Select(Function(x) New DataObjects.WorkRequest(x)).ToList
        End Function

        Public Shared Function GetAllWorkRequests(appid As Guid) As List(Of DataObjects.WorkRequest)

            Throw New Exception("Not implemented yet")

        End Function

#End Region

    End Class


End Namespace

