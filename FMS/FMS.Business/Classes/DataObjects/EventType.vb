Namespace DataObjects
    Public Class EventType
        Inherits CAN_MessageDefinition

        Public Property CanValue As String
        Public Property SendMail As Boolean
        Public Property SendText As Boolean

#Region "SHARED FUNCTIONS"
        Public Shared Function GetAllVehicleNames(ByVal applicationId As Guid) As Object
            Dim retobj As Object = SingletonAccess.FMSDataContextNew.ApplicationVehicles.Where(Function(y) y.ApplicationID = applicationId).OrderBy(Function(m) m.DeviceID).Select( _
                                                                            Function(x) New DataObjects.ApplicationVehicle(x)).Select(Function(col) col.Name).ToList()
            Return retobj
        End Function
        Public Shared Function GetAllForVehicleSPN(ByVal VehicleName As String, ByVal Comparison As String, ByVal TextSearch As String) As List(Of DataObjects.EventType)
            Dim dblCanValue As Double = 0
            Dim dblCanValue2 As Double = 0
            Dim intValue As Integer = 0
            If Comparison = Nothing Then Comparison = ""
            If TextSearch = Nothing Then TextSearch = ""
            Dim obj = DataObjects.ApplicationVehicle.GetFromName(VehicleName).GetAvailableCANTagsValue().ToList() _
                           .GroupBy(Function(xx) New With {Key xx.MessageDefinition.Standard, Key xx.MessageDefinition.PGN, Key xx.MessageDefinition.SPN}).ToList().Select(Function(xxx) xxx.First()).ToList()

            Dim objx As New List(Of EventType)
            For Each elem In obj
                Dim objElem As New EventType()
                If Not Comparison.Equals("") Then
                    CompareSearchedCanValues(objx, elem, TextSearch, Comparison)
                    CompareSearchedMessageDefinitions(objx, elem, TextSearch, Comparison)
                Else
                    objx.Add(SetCanMessageValues(elem))
                End If
            Next

            Return objx.ToList().GroupBy(Function(xx) New With {Key xx.Standard, Key xx.SPN}).ToList().Select(Function(xxx) xxx.First()).ToList()
        End Function
#End Region

#Region "CRUD"

#End Region

#Region "Private Shared Methods"
        Private Shared Sub CompareSearchedMessageDefinitions(ByRef objx As List(Of EventType), elem As CanValueMessageDefinition, TextSearch As String, stringCompare As String)
            Dim intValue As Integer = 0
            If (stringCompare.Equals("=") And elem.MessageDefinition.SPN.Equals(IIf(Integer.TryParse(IIf(TextSearch.Equals(""), "0", TextSearch), intValue), intValue, 0))) Or
                (stringCompare.Equals("<") And elem.MessageDefinition.SPN < IIf(Integer.TryParse(IIf(TextSearch.Equals(""), "0", TextSearch), intValue), intValue, 0)) Or
                (stringCompare.Equals(">") And elem.MessageDefinition.SPN > IIf(Integer.TryParse(IIf(TextSearch.Equals(""), "0", TextSearch), intValue), intValue, 0)) Or
                (stringCompare.Equals("<=") And elem.MessageDefinition.SPN <= IIf(Integer.TryParse(IIf(TextSearch.Equals(""), "0", TextSearch), intValue), intValue, 0)) Or
                (stringCompare.Equals(">=") And elem.MessageDefinition.SPN >= IIf(Integer.TryParse(IIf(TextSearch.Equals(""), "0", TextSearch), intValue), intValue, 0)) Or
                (stringCompare.Equals("!=") And elem.MessageDefinition.SPN <> IIf(Integer.TryParse(IIf(TextSearch.Equals(""), "0", TextSearch), intValue), intValue, 0)) Or
                (stringCompare.Equals("Like") And elem.MessageDefinition.SPN Like IIf(Integer.TryParse(IIf(TextSearch.Equals(""), "0", TextSearch), intValue), intValue, 0)) Then
                objx.Add(SetCanMessageValues(elem))
            End If
        End Sub
        Private Shared Sub CompareSearchedCanValues(ByRef objx As List(Of EventType), elem As CanValueMessageDefinition, TextSearch As String, stringCompare As String)
            Dim dblCanValue As Double = 0
            Dim dblCanValue2 As Double = 0
            If Not elem.CanValues.Count.Equals(0) Then
                If Double.TryParse(elem.CanValues(0).Value, dblCanValue) Then
                    If Double.TryParse(TextSearch, dblCanValue2) Then
                        If (stringCompare.Equals("=") And dblCanValue.Equals(dblCanValue2)) Or
                            (stringCompare.Equals("<") And dblCanValue < dblCanValue2) Or
                            (stringCompare.Equals(">") And dblCanValue > dblCanValue2) Or
                            (stringCompare.Equals("<=") And dblCanValue <= dblCanValue2) Or
                            (stringCompare.Equals(">=") And dblCanValue >= dblCanValue2) Or
                            (stringCompare.Equals("!=") And dblCanValue <> dblCanValue2) Then
                            objx.Add(SetCanMessageValues(elem))
                        End If
                    End If
                Else
                    If Not elem.CanValues(0).Value Is Nothing Then
                        If stringCompare.Equals("Like") Then
                            If elem.CanValues(0).Value.ToString().ToUpper.Contains(TextSearch.ToUpper()) Then
                                objx.Add(SetCanMessageValues(elem))
                            End If
                        Else
                            If elem.CanValues(0).Value.ToString().ToUpper.Equals(TextSearch.ToUpper()) Then
                                objx.Add(SetCanMessageValues(elem))
                            End If
                        End If
                    End If
                End If
            End If
        End Sub
        Private Shared Function SetCanMessageValues(ByVal canMessage As CanValueMessageDefinition) As EventType
            Dim objElem As New EventType()

            objElem.Acronym = canMessage.MessageDefinition.Acronym
            objElem.Data_Range = canMessage.MessageDefinition.Data_Range
            objElem.Description = canMessage.MessageDefinition.Description
            objElem.offset = canMessage.MessageDefinition.offset
            objElem.PGN = canMessage.MessageDefinition.PGN
            objElem.PGN_Length = canMessage.MessageDefinition.PGN_Length
            objElem.pos = canMessage.MessageDefinition.pos
            objElem.Resolution = canMessage.MessageDefinition.Resolution
            objElem.SPN = canMessage.MessageDefinition.SPN
            objElem.SPN_Length = canMessage.MessageDefinition.SPN_Length
            objElem.Standard = canMessage.MessageDefinition.Standard
            objElem.Units = canMessage.MessageDefinition.Units
            For Each canElem In canMessage.CanValues
                objElem.CanValue = canElem.Value
                Exit For
            Next
            objElem.SendMail = False
            objElem.SendText = False
            Return objElem
        End Function
#End Region
    End Class
End Namespace

