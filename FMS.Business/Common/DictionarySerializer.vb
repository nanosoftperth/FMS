Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Xml
Imports System.Xml.Schema
Imports System.Xml.Serialization
Public Class DictionarySerializer
    Implements IXmlSerializable
    ''' <summary>
    ''' Dictonary serializer
    ''' </summary>
    Public Dictionary As Dictionary(Of String, Object)
    Public Sub New()
        MyBase.New()
        Me.Dictionary = New Dictionary(Of String, Object)
    End Sub
    Public Sub New(ByVal dictionary As Dictionary(Of String, Object))
        MyBase.New()
        Me.Dictionary = dictionary
    End Sub
    Public Function GetSchema() As XmlSchema Implements IXmlSerializable.GetSchema
        Return Nothing
    End Function
    Public Sub ReadXml(reader As XmlReader) Implements IXmlSerializable.ReadXml
        Dim wasEmpty As Boolean = reader.IsEmptyElement
        reader.Read()
        If wasEmpty Then
            Return
        End If
        While (reader.NodeType <> XmlNodeType.EndElement)
            reader.ReadStartElement("item")
            Dim key As String = reader.ReadElementString("key")
            Dim value As String = reader.ReadElementString("value")
            Me.Dictionary.Add(key, value)
            reader.ReadEndElement()
            reader.MoveToContent()
        End While
        reader.ReadEndElement()
    End Sub
    Public Sub WriteXml(writer As XmlWriter) Implements IXmlSerializable.WriteXml
        If Not Me.Dictionary.Any Then
            Return
        End If

        For Each key In Me.Dictionary.Keys
            writer.WriteStartElement("item")
            writer.WriteElementString("key", key)
            Dim value = Me.Dictionary(key)
            'please note that we use ToString() for objects here
            'of course, we can Serialize them
            'but let's keep it simple and leave it for developers to handle it
            'just put required serialization into ToString method of your object(s)
            'because some objects don't implement ISerializable
            'the question is how should we deserialize null values?
            writer.WriteElementString("value", value.ToString)
            'TODO: Warning!!!, inline IF is not supported ?
            '(Not (value) Is Nothing)
            'Nothing
            writer.WriteEndElement()
        Next
    End Sub
End Class
