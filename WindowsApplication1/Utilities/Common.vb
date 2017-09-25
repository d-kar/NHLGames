Imports System.Net
Imports NHLGames.My.Resources

Namespace Utilities
    Public Class Common

        Public Shared Function GetRandomString(ByVal intLength As Integer)
            Const s As String = "abcdefghijklmnopqrstuvwxyz0123456789"
            Dim r As New Random
            Dim sb As New Text.StringBuilder
            For i = 1 To intLength
                Dim idx As Integer = r.Next(0, 35)
                sb.Append(s.Substring(idx, 1))
            Next

            Return sb.ToString()
        End Function

        Public Shared Function CheckUrl(ByVal address As String, Optional httpWebRequest As HttpWebRequest = Nothing) As Boolean
            Try
                Dim myHttpWebRequest As HttpWebRequest
                If httpWebRequest Is Nothing Then
                    myHttpWebRequest = CType(WebRequest.Create(address), HttpWebRequest)
                    myHttpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, Like Gecko) Chrome/48.0.2564.82 Safari/537.36 Edge/14.14316"
                Else 
                    myHttpWebRequest = httpWebRequest
                End If
                Dim myHttpWebResponse As HttpWebResponse = CType(myHttpWebRequest.GetResponse(), HttpWebResponse)
                myHttpWebResponse.Close()

                If myHttpWebResponse.StatusCode = HttpStatusCode.OK Then
                    Return True
                End If
            Catch e As Exception
                Return False
            End Try
            Return False
        End Function

        Public Shared Sub WaitForGameThreads()
            For Each t As Threading.Thread In NHLGamesMetro.lstThreads
                t.Join()
            Next
            NHLGamesMetro.lstThreads.Clear()
        End Sub

        Public Shared Sub GetLanguage()
            Dim lang = ApplicationSettings.Read(Of String)(SettingsEnum.SelectedLanguage, String.Empty)
            If String.IsNullOrEmpty(lang) OrElse lang = NHLGamesMetro.RmText.GetString("lblEnglish") Then
                NHLGamesMetro.RmText = English.ResourceManager
            ElseIf lang = NHLGamesMetro.RmText.GetString("lblFrench") Then
                NHLGamesMetro.RmText = French.ResourceManager
            End If
        End Sub

    End Class
End Namespace
