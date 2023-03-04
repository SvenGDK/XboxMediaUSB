Imports System.Runtime.InteropServices
Imports System.Text

Namespace INI

    Public Class IniFile
        Public IniPath As String

        <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> Private Shared Function WritePrivateProfileString(ByVal lpAppName As String, _
                        ByVal lpKeyName As String, _
                        ByVal lpString As String, _
                        ByVal lpFileName As String) As Boolean
        End Function

        <DllImport("kernel32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> Private Shared Function GetPrivateProfileString(ByVal lpAppName As String, _
                        ByVal lpKeyName As String, _
                        ByVal lpDefault As String, _
                        ByVal lpReturnedString As StringBuilder, _
                        ByVal nSize As Integer, _
                        ByVal lpFileName As String) As Integer
        End Function

        Public Sub New(ByVal INIPathValue As String)
            IniPath = INIPathValue
        End Sub

        Public Sub WriteValue(ByVal Section As String, ByVal Key As String, ByVal Value As String)
            WritePrivateProfileString(Section, Key, Value, IniPath)
        End Sub

        Public Function ReadValue(ByVal Section As String, ByVal Key As String) As String
            Dim res As Integer
            Dim sb As New StringBuilder(255)
            res = GetPrivateProfileString(Section, Key, "", sb, sb.Capacity, IniPath)
            Return sb.ToString()
        End Function
    End Class

End Namespace