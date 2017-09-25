Imports System.Globalization
Imports System.IO
Imports System.Net
Imports Newtonsoft.Json.Linq
Imports NHLGames.My.Resources
Imports NHLGames.Utilities

Namespace Objects

    Public Class GameStream

        Public ReadOnly Property Type As StreamType
        Public ReadOnly Property Game As Game
        Public ReadOnly Property IsDefined As Boolean = False
        Public Property IsVod As Boolean = False
        Public ReadOnly Property Network As String
        Public ReadOnly Property PlayBackId As String
        Public Property GameUrl As String = ""
        Public Property Vodurl As String = ""
        Public ReadOnly Property IsAvailable As Boolean
            Get
                return GameUrl <> ""
            End Get
        End Property

        Public Sub New()
        End Sub

        Public Sub New(game As Game, stream As JObject, type As StreamType)
            Me.Game = game
            IsDefined = True
            Network = stream.Property("callLetters")
            PlayBackId = stream.Property("mediaPlaybackId").Value.ToString()
            Me.Type = type
        End Sub

        Public Sub CheckVod(ByVal strCdn As String)
            Try
                Dim myHttpWebRequest As HttpWebRequest = CType(WebRequest.Create(Vodurl.Replace("CDN", strCdn)), HttpWebRequest)
                myHttpWebRequest.CookieContainer = New CookieContainer()
                myHttpWebRequest.CookieContainer.Add(New Cookie("mediaAuth", Common.GetRandomString(240), String.Empty, "nhl.com"))
                myHttpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, Like Gecko) Chrome/48.0.2564.82 Safari/537.36 Edge/14.14316"
                IsVod = Common.CheckURL(Vodurl.Replace("CDN", strCdn), myHttpWebRequest)
            Catch e As Exception
                Console.WriteLine(English.msgVOD, e.Message)
            End Try
        End Sub


        Public Sub GetRightGameStream()
            Dim client = New WebClient()
            Dim reader As StreamReader

            client.Headers.Set("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, Like Gecko) Chrome/48.0.2564.82 Safari/537.36 Edge/14.14316")

            Dim args = ApplicationSettings.Read(Of GameWatchArguments)(SettingsEnum.DefaultWatchArgs)
            Dim address As String = String.Format("http://{0}/m3u8/{1}/{2}{3}", NHLGamesMetro.HostName, GameManager.GamesListDate.ToString("yyyy-MM-dd"), PlayBackId, args.Cdn)
            Dim legacyAddress As String = String.Format("http://{0}/m3u8/{1}/{2}", NHLGamesMetro.HostName, GameManager.GamesListDate.ToString("yyyy-MM-dd"), PlayBackId)

            Try
                reader = New StreamReader(client.OpenRead(address))
                GameUrl = reader.ReadToEnd()
            Catch ex As WebException
                Try
                    reader = New StreamReader(client.OpenRead(legacyAddress))
                    GameUrl = reader.ReadToEnd()
                Catch ex2 As Exception 
                    If Not ex2.Message.Contains("404") Then
                        Console.WriteLine(String.Format(NHLGamesMetro.RmText.GetString("errorGeneral"), ex.Message))
                    End If
                End Try
            Catch ex As Exception
                Console.WriteLine(String.Format(NHLGamesMetro.RmText.GetString("errorGeneral"), ex.Message))
            Finally
                client.Dispose()
                SetVideoOnDemandLink()
            End Try

        End Sub

        Private Sub SetVideoOnDemandLink()
            If GameUrl.Contains("http://hlslive") Then
                Dim spliter = GameUrl.Split("/")
                For Each split As String In spliter
                    If split.StartsWith("NHL_GAME_VIDEO_") Then
                        Vodurl = String.Format("http://hlsvod-akc.med2.med.nhl.com/ps01/nhl/{0}/{1}/master_wired60.m3u8", dateString, split)
                    End If
                Next
            End If
        End Sub
    End Class
End Namespace
