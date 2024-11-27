Imports System.Data.SqlClient
Imports System
Imports System.Reflection
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Text
Imports System.IO

Namespace Provider
    Partial Public Class SqlDataProvider

#Region "Function"

        Public Function GetNull(ByVal Field As DateTime) As Object

            Dim TuNgay As DateTime = DateTime.ParseExact("31/12/1900", "dd/MM/yyyy", Globalization.CultureInfo.CreateSpecificCulture("vi-VN"))
            If TuNgay <= Field Then 'AndAlso Field <= DenNgay Then
                Return Field
            Else
                Return DBNull.Value
            End If
        End Function

        Public Shared Function ConnectDB() As Boolean
            Try
                Dim sql As SqlConnection = New Connections().SqlConn
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

        Public Function GetNull(ByVal Field As Object) As Object

            If Field Is Nothing Then
                Return DBNull.Value
            Else
                Return Field
            End If

        End Function

        Public Shared Function FillObject(ByVal dr As IDataReader, ByVal objType As Type) As Object
            Return FillObject(dr, objType, True)
        End Function

        Public Shared Function FillObject(ByVal dr As IDataReader, ByVal objType As Type, ByVal ManageDataReader As Boolean) As Object

            Dim objFillObject As Object

            ' get properties for type
            Dim objProperties As ArrayList = GetPropertyInfo(objType)

            ' get ordinal positions in datareader
            Dim arrOrdinals As Integer() = GetOrdinals(objProperties, dr)

            Dim [Continue] As Boolean
            If ManageDataReader Then
                [Continue] = False
                ' read datareader
                If dr.Read() Then
                    [Continue] = True
                End If
            Else
                [Continue] = True
            End If

            If [Continue] Then
                ' create custom business object
                objFillObject = CreateObject(objType, dr, objProperties, arrOrdinals)
            Else
                objFillObject = Nothing
            End If

            If ManageDataReader Then
                ' close datareader
                If Not dr Is Nothing Then
                    dr.Close()
                End If
            End If

            Return objFillObject

        End Function


        Public Shared Function GetPropertyInfo(ByVal objType As Type) As ArrayList

            'Modified by HoaTV
            'Không sử dụng được cache để lưu cấu trúc sử dụng
            Dim objProperties As ArrayList

            objProperties = New ArrayList
            Dim objProperty As PropertyInfo
            For Each objProperty In objType.GetProperties()
                objProperties.Add(objProperty)
            Next
            Return objProperties
        End Function


        Private Shared Function GetOrdinals(ByVal objProperties As ArrayList, ByVal dr As IDataReader) As Integer()

            Dim arrOrdinals(objProperties.Count) As Integer
            Dim intProperty As Integer

            If Not dr Is Nothing Then
                For intProperty = 0 To objProperties.Count - 1
                    arrOrdinals(intProperty) = -1
                    Try
                        arrOrdinals(intProperty) = dr.GetOrdinal(CType(objProperties(intProperty), PropertyInfo).Name)
                    Catch
                        ' property does not exist in datareader
                    End Try
                Next intProperty
            End If
            Return arrOrdinals
        End Function

        Private Shared Function CreateObject(ByVal objType As Type, ByVal dr As IDataReader, ByVal objProperties As ArrayList, ByVal arrOrdinals As Integer()) As Object

            Dim objPropertyInfo As PropertyInfo
            Dim objValue As Object
            Dim objPropertyType As Type = Nothing
            Dim intProperty As Integer

            Dim objObject As Object = Activator.CreateInstance(objType)

            ' fill object with values from datareader
            For intProperty = 0 To objProperties.Count - 1
                objPropertyInfo = CType(objProperties(intProperty), PropertyInfo)
                If objPropertyInfo.CanWrite Then
                    If arrOrdinals(intProperty) <> -1 Then
                        objValue = dr.GetValue(arrOrdinals(intProperty))
                        If IsDBNull(objValue) Then
                            ' translate Null value
                            objPropertyInfo.SetValue(objObject, Null.SetNull(objPropertyInfo), Nothing)
                        Else
                            Try
                                ' try implicit conversion first
                                objPropertyInfo.SetValue(objObject, objValue, Nothing)
                            Catch
                                ' business object info class member data type does not match datareader member data type
                                objPropertyType = objPropertyInfo.PropertyType
                                Try
                                    'need to handle enumeration conversions differently than other base types
                                    If objPropertyType.BaseType.Equals(GetType(System.Enum)) Then
                                        ' check if value is numeric and if not convert to integer ( supports databases like Oracle )
                                        If IsNumeric(objValue) Then
                                            CType(objProperties(intProperty), PropertyInfo).SetValue(objObject, System.Enum.ToObject(objPropertyType, Convert.ToInt32(objValue)), Nothing)
                                        Else
                                            CType(objProperties(intProperty), PropertyInfo).SetValue(objObject, System.Enum.ToObject(objPropertyType, objValue), Nothing)
                                        End If
                                    Else
                                        ' try explicit conversion
                                        objPropertyInfo.SetValue(objObject, Convert.ChangeType(objValue, objPropertyType), Nothing)
                                    End If
                                Catch
                                    If objPropertyType.Name = "Boolean" Then
                                        objPropertyInfo.SetValue(objObject, CType(objValue, Boolean), Nothing)
                                    Else
                                        objPropertyInfo.SetValue(objObject, Convert.ChangeType(objValue, objPropertyType), Nothing)
                                    End If
                                End Try
                            End Try
                        End If
                    Else
                        ' property does not exist in datareader
                    End If
                End If
            Next intProperty
            Return objObject
        End Function

#End Region

    End Class

End Namespace