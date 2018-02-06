Namespace DataObjects
    Public Class usp_GetIncludeServiceInformationReport

#Region "Properties / enums"
        Public Property ApplicationId As Guid
        Public Property CID As System.Nullable(Of Integer)
        Public Property ServiceCode As String
        Public Property ServiceDescription As String
        Public Property ServiceUnits As System.Nullable(Of Double)
        Public Property ServicePrice As System.Nullable(Of Double)
        Public Property PerAnnumCharge As System.Nullable(Of Double)
        Public Property CSid As System.Nullable(Of Integer)
        Public Property SiteCeaseDate As System.Nullable(Of Date)
        Public Property UnitsHaveMoreThanOneRun As System.Nullable(Of Boolean)
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(IncludeServiceInformation As FMS.Business.usp_GetIncludeServiceInformationReportResult)
            With IncludeServiceInformation
                Me.ApplicationId = .applicationid
                Me.CID = .Cid
                Me.ServiceCode = .ServiceCode
                Me.ServiceDescription = .ServiceDescription
                Me.ServiceUnits = .ServiceUnits
                Me.ServicePrice = .ServicePrice
                Me.PerAnnumCharge = .PerAnnumCharge
                Me.CSid = .CSid
                Me.SiteCeaseDate = .SiteCeaseDate
                Me.UnitsHaveMoreThanOneRun = .UnitsHaveMoreThanOneRun

            End With
        End Sub



#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.usp_GetIncludeServiceInformationReport)
            Try
                Dim objList As New List(Of DataObjects.usp_GetIncludeServiceInformationReport)
                With New LINQtoSQLClassesDataContext
                    .CommandTimeout = 180
                    objList = (From i In .usp_GetIncludeServiceInformationReport()
                               Select New DataObjects.usp_GetIncludeServiceInformationReport(i)).ToList
                    .Dispose()
                End With
                Return objList
            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Shared Function GetAllIncludeServiceInformationPerCustomer(Customer As Integer) As List(Of DataObjects.usp_GetIncludeServiceInformationReport)
            Try
                Dim objList As New List(Of DataObjects.usp_GetIncludeServiceInformationReport)
                With New LINQtoSQLClassesDataContext
                    .CommandTimeout = 180

                    objList = (From s In .usp_GetIncludeServiceInformationReport()
                               Where s.Customer = Customer
                               Select New DataObjects.usp_GetIncludeServiceInformationReport(s)).ToList
                    .Dispose()
                End With
                Return objList

            Catch ex As Exception
                Throw ex
            End Try

        End Function
        Public Shared Function GetAllIncludeServiceInformationPerCustomerAndCID(Customer As Integer, Optional CID As Integer = 0) As List(Of DataObjects.usp_GetIncludeServiceInformationReport)
            Try
                Dim appId = ThisSession.ApplicationID
                Dim objList As New List(Of DataObjects.usp_GetIncludeServiceInformationReport)
                With New LINQtoSQLClassesDataContext
                    If (Customer = 0) Then
                        If (CID = 0) Then
                            objList = (From s In .usp_GetIncludeServiceInformationReport()
                                       Where s.applicationid = appId
                                       Select New DataObjects.usp_GetIncludeServiceInformationReport(s)).ToList
                        End If
                    Else
                        If (CID = 0) Then
                            objList = (From s In .usp_GetIncludeServiceInformationReport()
                                       Where s.Customer = Customer And s.applicationid = appId
                                       Select New DataObjects.usp_GetIncludeServiceInformationReport(s)).ToList
                        Else
                            objList = (From s In .usp_GetIncludeServiceInformationReport()
                                       Where s.Customer = Customer And s.Cid = CID And s.applicationid = appId
                                       Select New DataObjects.usp_GetIncludeServiceInformationReport(s)).ToList
                        End If
                    End If
                    .Dispose()
                End With
                Return objList

            Catch ex As Exception
                Throw ex
            End Try

        End Function

#End Region

    End Class

End Namespace


