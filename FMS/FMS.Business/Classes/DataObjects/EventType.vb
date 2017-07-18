Namespace DataObjects
    Public Class EventType
        Inherits CAN_MessageDefinition

        Public Property SendMail As Boolean
        Public Property SendText As Boolean

#Region "SHARED FUNCTIONS"

        Public Shared Function GetAllVehicleNames(ByVal applicationId As Guid) As Object
            Dim retobj As Object = SingletonAccess.FMSDataContextNew.ApplicationVehicles.Where(Function(y) y.ApplicationID = applicationId).OrderBy(Function(m) m.DeviceID).Select( _
                                                                            Function(x) New DataObjects.ApplicationVehicle(x)).Select(Function(col) col.Name).ToList()
            Return retobj
        End Function
        Public Shared Function GetAllForVehicleSPN(ByVal VehicleName As String, ByVal Comparison As String, ByVal TextSearch As String) As List(Of DataObjects.EventType)
            Dim intValue As Integer = 0
            If Comparison = Nothing Then Comparison = ""
            If TextSearch = Nothing Then TextSearch = ""
            Dim obj = DataObjects.ApplicationVehicle.GetFromName(VehicleName).GetAvailableCANTagsValue() _
                            .Where(Function(res) + _
                               IIf(Comparison.Equals("<"), res.SPN < IIf(Integer.TryParse(IIf(TextSearch.Equals(""), "0", TextSearch), intValue), intValue, 0), + _
                               IIf(Comparison.Equals(">"), res.SPN > IIf(Integer.TryParse(IIf(TextSearch.Equals(""), "0", TextSearch), intValue), intValue, 0), + _
                               IIf(Comparison.Equals("<="), res.SPN <= IIf(Integer.TryParse(IIf(TextSearch.Equals(""), "0", TextSearch), intValue), intValue, 0), + _
                               IIf(Comparison.Equals(">="), res.SPN >= IIf(Integer.TryParse(IIf(TextSearch.Equals(""), "0", TextSearch), intValue), intValue, 0), + _
                               IIf(Comparison.Equals("!="), res.SPN <> IIf(Integer.TryParse(IIf(TextSearch.Equals(""), "0", TextSearch), intValue), intValue, 0), + _
                               IIf(Comparison.Equals("Like"), res.Description.ToUpper.Contains(TextSearch.ToUpper), + _
                               IIf(Comparison.Equals("<"), res.SPN < IIf(Integer.TryParse(IIf(TextSearch.Equals(""), "0", TextSearch), intValue), intValue, 0), + _
                               IIf(Comparison.Equals("="), res.SPN = IIf(Integer.TryParse(IIf(TextSearch.Equals(""), "0", TextSearch), intValue), intValue, 0) Or res.Description.ToUpper = TextSearch.ToUpper, Not res.Description.Equals("")))))))))).ToList() _
                           .GroupBy(Function(xx) New With {Key xx.Standard, Key xx.SPN}).ToList().Select(Function(xxx) xxx.First()).ToList()

            Dim objx As New List(Of EventType)
            For Each elem In obj
                Dim objElem As New EventType()
                objElem.Acronym = elem.Acronym
                objElem.Data_Range = elem.Data_Range
                objElem.Description = elem.Description
                objElem.offset = elem.offset
                objElem.PGN = elem.PGN
                objElem.PGN_Length = elem.PGN_Length
                objElem.pos = elem.pos
                objElem.Resolution = elem.Resolution
                objElem.SPN = elem.SPN
                objElem.SPN_Length = elem.SPN_Length
                objElem.Standard = elem.Standard
                objElem.Units = elem.Units
                objElem.SendMail = False
                objElem.SendText = False
                objx.Add(objElem)
            Next


            Return objx
        End Function
#End Region

#Region "CRUD"

#End Region
    End Class
End Namespace

