﻿Imports System.Globalization
Imports NHLGames.Objects
Imports NHLGames.Utilities

Namespace Controls

    Public Class GameControl: Implements IDisposable
        Private _game As Game
        Private ReadOnly _showLiveScores As Boolean
        Private ReadOnly _showScores As Boolean
        Private ReadOnly _showSeriesRecord As Boolean
        Private ReadOnly _showTeamCityAbr As Boolean
        Private ReadOnly _broadcasters As Dictionary(Of String, String)

        Public ReadOnly Property GameId() As String
            Get
                Return _game.GameId
            End Get
        End Property

        Public Sub UpdateGame(showScores As Boolean, showLiveScores As Boolean, showSeriesRecord As Boolean, showTeamCityAbr As Boolean, Optional game As Game = Nothing)
            If game IsNot Nothing Then 
                _game = game
                game.Dispose()
            End If

            lblPeriod.Text = ""
            lblGameStatus.Text = ""
            lblNotInSeason.Text = ""

            If _game.IsLive Then
                picLive.Visible = True
                lblGameStatus.Visible = Not showLiveScores
                lblHomeScore.Visible = showLiveScores
                lblAwayScore.Visible = showLiveScores
                lblPeriod.BackColor = Color.FromArgb(255, 0, 170, 210)
                lblPeriod.ForeColor = Color.White

                lblPeriod.Text = $"{_game.GamePeriod.
                    Replace($"1st",NHLGamesMetro.RmText.GetString("gamePeriod1")).
                    Replace($"2nd",NHLGamesMetro.RmText.GetString("gamePeriod2")).
                    Replace($"3rd",NHLGamesMetro.RmText.GetString("gamePeriod3")).
                    Replace($"OT", NHLGamesMetro.RmText.GetString("gamePeriodOt")).
                    Replace($"SO", NHLGamesMetro.RmText.GetString("gamePeriodSo")).
                    ToUpper()}          {_game.GameTimeLeft.ToLower().
                    Replace("final",NHLGamesMetro.RmText.GetString("gamePeriodFinal")).
                    Replace("end", "00:00")}".
                    ToUpper() '1st 2nd 3rd OT SO... Final, 12:34, 20:00 

                If _game.GamePeriod.Contains(NHLGamesMetro.RmText.GetString("gamePeriodOt")) And IsNumeric(_game.GamePeriod(0)) Then
                    lblPeriod.Text = String.Format(NHLGamesMetro.RmText.GetString("gamePeriodOtMore"), _game.GamePeriod(0)).ToUpper() '2OT..
                End If
                
                If Not showLiveScores Then
                    lblGameStatus.Text = String.Format("{0}{1}{2}",
                                                       _game.GameDate.ToLocalTime().ToString("h:mm tt"),
                                                       vbCrLf,
                                                       NHLGamesMetro.RmText.GetString("enuminprogress").ToUpper())
                End If

                If (Not showLiveScores OrElse Not showSeriesRecord) And _game.GameType.Equals(GameTypeEnum.Series) Then
                    lblNotInSeason.Text = NHLGamesMetro.RmText.GetString("lblPlayoffs").ToUpper()
                End If

                If Not showLiveScores Then lblPeriod.Text = String.Empty
            ElseIf _game.GameState.Equals(GameStateEnum.Final) Then
                lblHomeScore.Visible = showScores
                lblAwayScore.Visible = showScores
                lblGameStatus.Visible = Not showScores

                If _game.HomeScore < _game.AwayScore Then
                    lblHomeScore.ForeColor = Color.Gray
                Else 
                    lblAwayScore.ForeColor = Color.Gray
                End If

                If showScores Then
                    lblPeriod.Text = NHLGamesMetro.RmText.GetString("enumfinal").ToUpper()
                    If Not [String].Equals(_game.GamePeriod, $"3rd", StringComparison.CurrentCultureIgnoreCase) And _game.GamePeriod <> "" Then
                        lblPeriod.Text =  (NHLGamesMetro.RmText.GetString($"enumfinal") & 
                            $"/" & _game.GamePeriod.Replace($"OT",NHLGamesMetro.RmText.GetString("gamePeriodOt")).
                            Replace($"SO", NHLGamesMetro.RmText.GetString("gamePeriodSo"))).ToUpper() 'FINAL/SO.. OT.. 2OT
                    End If
                Else
                    lblGameStatus.Text = String.Format("{0}{1}{2}",
                                                       _game.GameDate.ToLocalTime().ToString("h:mm tt"),
                                                       vbCrLf,
                                                       NHLGamesMetro.RmText.GetString("enumfinal").ToUpper())
                    If lblPeriod.Text.Contains(NHLGamesMetro.RmText.GetString("gamePeriodOt")) Then
                        lblGameStatus.Text = String.Format("{0}{1}{2}",
                                                           _game.GameDate.ToLocalTime().ToString("h:mm tt"),
                                                           vbCrLf,
                                                           NHLGamesMetro.RmText.GetString("gamePeriodFinal").ToUpper())
                    End If
                End If

                If Not showScores Then lblPeriod.Text = String.Empty
            ElseIf _game.GameState <= GameStateEnum.Pregame Then
                lblDivider.Visible = False
                lblPeriod.Text = String.Empty
                lblGameStatus.Visible = True
                lblGameStatus.Text = _game.GameDate.ToLocalTime().ToString("h:mm tt")
                If _game.GameState.Equals(GameStateEnum.Pregame) Then
                    lblPeriod.BackColor = Color.FromArgb(255, 0, 170, 210)
                    If showLiveScores Then
                        lblPeriod.ForeColor = Color.White
                        lblPeriod.Text = NHLGamesMetro.RmText.GetString("enumpregame").ToUpper()
                    Else
                        lblGameStatus.Text &= String.Format("{0}{1}", vbCrLf, NHLGamesMetro.RmText.GetString("enumpregame").ToUpper())
                    End If
                End If
            End If

            If _game.GameType.Equals(GameTypeEnum.Preseason) Then 
                lblNotInSeason.Text = NHLGamesMetro.RmText.GetString("lblPreseason").ToUpper()
            Else If _game.GameType.Equals(GameTypeEnum.Series) Then
                Dim seriesStatusShort =  String.Format(NHLGamesMetro.RmText.GetString("lblGame"),
                                                       _game.SeriesGameNumber.ToString()).ToUpper() 'Game 1
                Dim seriesStatusLong = If(
                    _game.SeriesGameNumber <> 1,
                    String.Format(NHLGamesMetro.RmText.GetString("lblGameAbv"),
                                  _game.SeriesGameNumber.ToString(), 'Game 2.. 7
                                  _game.SeriesGameStatus.ToString().ToLower().
                                  Replace("tied",NHLGamesMetro.RmText.GetString("gameSeriesTied")).
                                  Replace("wins",NHLGamesMetro.RmText.GetString("gameSeriesWin")).
                                  Replace("leads",NHLGamesMetro.RmText.GetString("gameSeriesLead"))).ToUpper(),  'G3: Team wins 4-2, Tied 2-2, Team leads 1-0
                                  seriesStatusShort)

                lblNotInSeason.Text = If (showSeriesRecord, seriesStatusLong, seriesStatusShort)
            End If

            If Not _game.AreAnyStreamsAvailable OrElse Not NHLGamesMetro.HostNameResolved Then
                If _game.GameDate.AddMinutes(15).ToLocalTime() > Date.UtcNow.ToLocalTime() And _game.GameState < GameStateEnum.InProgress Then
                    lblStreamStatus.Text = NHLGamesMetro.RmText.GetString("lblStreamAvailableAtGameTime")
                Else
                    lblStreamStatus.Text = NHLGamesMetro.RmText.GetString("lblNoStreamAvailable")
                End If
                If Not NHLGamesMetro.HostNameResolved Then
                    lblStreamStatus.Text = NHLGamesMetro.RmText.GetString("lblHostFileNotWorking")
                End If
                flpStreams.Visible = False
            End If

            lblHomeTeam.Visible = showTeamCityAbr
            lblAwayTeam.Visible = showTeamCityAbr

            tt.SetToolTip(picAway, String.Format(NHLGamesMetro.RmText.GetString("lblAwayTeam"), _game.Away, _game.AwayTeam))
            tt.SetToolTip(picHome, String.Format(NHLGamesMetro.RmText.GetString("lblHomeTeam"), _game.Home, _game.HomeTeam))
        End Sub

        Public Sub New(game As Game, showScores As Boolean, showLiveScores As Boolean, showSeriesRecord As Boolean, showTeamCityAbr As Boolean)

            InitializeComponent()
            _broadcasters = New Dictionary(Of String, String)() From {
                {"ALT", "ALT"},
                {"CBC", "CBC"},
                {"CSN", "CSN"},
                {"ESPN", "ESPN"},
                {"FS", "FS"},
                {"MSG", "MSG"},
                {"NBC", "NBC"},
                {"NESN", "NESN"},
                {"RDS", "RDS"},
                {"ROOT", "ROOT"},
                {"SN", "SN"},
                {"TSN", "TSN"},
                {"TVAS", "TVAS"},
                {"SUN", "FS"},
                {"CITY", "CBC"},
                {"WGN", "WGN"},
                {"PRIM", "FS"},
                {"CNBC", "NBC"},
                {"KCOP", "FS"},
                {"TCN", "CSN"},
                {"USA", "NBC"},
                {"ATT", "ATT"}}
            _showScores = showScores
            _showLiveScores = showLiveScores
            _showSeriesRecord = showSeriesRecord
            _showTeamCityAbr = showTeamCityAbr
            _game = game

            SetWholeGamePanel()
        End Sub

        Private Sub UpdateGameStreams()
            lblHomeScore.Text = _game.HomeScore
            lblHomeTeam.Text = _game.HomeAbbrev

            lblAwayScore.Text = _game.AwayScore
            lblAwayTeam.Text = _game.AwayAbbrev
   
            lnkAway.Visible = _game.IsStreamDefined(StreamType.Away)
            lnkHome.Visible = _game.IsStreamDefined(StreamType.Home)
            lnkFrench.Visible = _game.IsStreamDefined(StreamType.French)
            lnkNational.Visible = _game.IsStreamDefined(StreamType.National)
            lnkThree.Visible = _game.IsStreamDefined(StreamType.MultiCam1)
            lnkSix.Visible = _game.IsStreamDefined(StreamType.MultiCam2)
            lnkRef.Visible = _game.IsStreamDefined(StreamType.RefCam)
            lnkStar.Visible = _game.IsStreamDefined(StreamType.StarCam)
            lnkEnd1.Visible = _game.IsStreamDefined(StreamType.EndzoneCam1)
            lnkEnd2.Visible = _game.IsStreamDefined(StreamType.EndzoneCam2)

            If _game.GameState < GameStateEnum.Final And _game.GameDate.ToLocalTime() <= Date.Today.AddDays(1) Then
                bpGameControl.BorderColour = Color.FromArgb(255, 0, 170, 210)
            Else
                bpGameControl.BorderColour = Color.DarkGray
            End If

            UpdateGame(_showScores, _showLiveScores, _showSeriesRecord, _showTeamCityAbr)
        End Sub

        Private Shared Function RemoveDiacritics(text As String) As String
            Dim normalizedString = text.Normalize(System.Text.NormalizationForm.FormD)
            Dim stringBuilder = New Text.StringBuilder()

            For Each c As String In normalizedString
                Dim unicodeCategory1 = CharUnicodeInfo.GetUnicodeCategory(c)
                If unicodeCategory1 <> UnicodeCategory.NonSpacingMark Then
                    stringBuilder.Append(c)
                End If
            Next

            Return stringBuilder.ToString().Normalize(System.Text.NormalizationForm.FormC)
        End Function 

        Public Sub SetWholeGamePanel()
            
            picAway.SizeMode = PictureBoxSizeMode.Zoom
            If String.IsNullOrEmpty(_game.HomeTeam) = False Then
                Dim img As Bitmap = ImageFetcher.GetEmbeddedImage(RemoveDiacritics(_game.AwayTeam).Replace(" ", "").Replace(".", ""))
                If Not img Is Nothing Then picAway.BackgroundImage = img
            End If

            picHome.SizeMode = PictureBoxSizeMode.Zoom
            If String.IsNullOrEmpty(_game.AwayTeam) = False Then
                Dim img As Bitmap = ImageFetcher.GetEmbeddedImage(RemoveDiacritics(_game.HomeTeam).Replace(" ", "").Replace(".", ""))
                If Not img Is Nothing Then picHome.BackgroundImage = img
            End If
            
            SetStreamButtonLink(StreamType.Away, lnkAway, String.Format(NHLGamesMetro.RmText.GetString("lblTeamStream"), _game.AwayAbbrev))
            SetStreamButtonLink(StreamType.Home, lnkHome, String.Format(NHLGamesMetro.RmText.GetString("lblTeamStream"), _game.HomeAbbrev))
            SetStreamButtonLink(StreamType.French, lnkFrench, NHLGamesMetro.RmText.GetString("lblFrenchNetwork"))
            SetStreamButtonLink(StreamType.National, lnkNational, NHLGamesMetro.RmText.GetString("lblNationalNetwork"))

            SetStreamButtonLink(StreamType.MultiCam1, lnkThree, String.Format( NHLGamesMetro.RmText.GetString("lblCamViews"), 3))
            SetStreamButtonLink(StreamType.MultiCam2, lnkSix, String.Format(NHLGamesMetro.RmText.GetString("lblCamViews"), 6))
            SetStreamButtonLink(StreamType.EndzoneCam1, lnkEnd1, String.Format(NHLGamesMetro.RmText.GetString("lblEndzoneCam"), _game.AwayAbbrev))
            SetStreamButtonLink(StreamType.EndzoneCam2, lnkEnd2,  String.Format(NHLGamesMetro.RmText.GetString("lblEndzoneCam"), _game.HomeAbbrev))
            SetStreamButtonLink(StreamType.RefCam, lnkRef, NHLGamesMetro.RmText.GetString("lblRefCam"))
            SetStreamButtonLink(StreamType.StarCam, lnkStar, NHLGamesMetro.RmText.GetString("lblStarCam"))
            
            UpdateGameStreams()
        End Sub

        Private Function _getBroadcasterPicFor(network As String)
            Dim value As String = _broadcasters.Where(Function(kvp) network.ToUpper().StartsWith(kvp.Key.ToString())).Select(Function(kvp) kvp.Value).FirstOrDefault()
            Return If(value <> Nothing, value.ToLower, "")
        End Function

        Private Function WatchArgs() As GameWatchArguments
            Dim args = ApplicationSettings.Read(Of GameWatchArguments)(SettingsEnum.DefaultWatchArgs)
            args.GameTitle = _game.AwayAbbrev & " @ " & _game.HomeAbbrev
            Return args
        End Function

        Private Sub lnkAway_Click(sender As Object, e As EventArgs) Handles lnkAway.Click
            Dim args = WatchArgs()
            args.Stream = _game.GetStream(StreamType.Away)
            Player.Watch(args)
        End Sub

        Private Sub lnkFrench_Click(sender As Object, e As EventArgs) Handles lnkFrench.Click 
            Dim args = WatchArgs()
            args.Stream = _game.GetStream(StreamType.French)
            Player.Watch(args)
        End Sub

        Private Sub lnkNational_Click(sender As Object, e As EventArgs) Handles lnkNational.Click 
            Dim args = WatchArgs()
            args.Stream = _game.GetStream(StreamType.National)
            Player.Watch(args)
        End Sub

        Private Sub lnkHome_Click(sender As Object, e As EventArgs) Handles lnkHome.Click 
            Dim args = WatchArgs()
            args.Stream = _game.GetStream(StreamType.Home)
            Player.Watch(args)
        End Sub

        Private Sub lnkThree_Click(sender As Object, e As EventArgs) Handles lnkThree.Click 
            Dim args = WatchArgs()
            args.Stream = _game.GetStream(StreamType.MultiCam1)
            Player.Watch(args)
        End Sub

        Private Sub lnkSix_Click(sender As Object, e As EventArgs) Handles lnkSix.Click 
            Dim args = WatchArgs()
            args.Stream = _game.GetStream(StreamType.MultiCam2)
            Player.Watch(args)
        End Sub

        Private Sub lnkEnd1_Click(sender As Object, e As EventArgs) Handles lnkEnd1.Click 
            Dim args = WatchArgs()
            args.Stream = _game.GetStream(StreamType.EndzoneCam1)
            Player.Watch(args)
        End Sub

        Private Sub lnkEnd2_Click(sender As Object, e As EventArgs) Handles lnkEnd2.Click 
            Dim args = WatchArgs()
            args.Stream = _game.GetStream(StreamType.EndzoneCam2)
            Player.Watch(args)
        End Sub

        Private Sub lnkRef_Click(sender As Object, e As EventArgs) Handles lnkRef.Click 
            Dim args = WatchArgs()
            args.Stream = _game.GetStream(StreamType.RefCam)
            Player.Watch(args)
        End Sub

        Private Sub lnkStar_Click(sender As Object, e As EventArgs) Handles lnkStar.Click
            Dim args = WatchArgs()
            args.Stream = _game.GetStream(StreamType.StarCam)
            Player.Watch(args)
        End Sub

        Private Sub SetStreamButtonLink(streamType As StreamType, btnLink As Button, tooltip As String)
            If _game.IsStreamDefined(streamType) Then
                Dim stream = _game.GetStream(streamType)
                btnLink.Enabled = Not stream.IsBroken

                If stream.IsBroken Then
                    btnLink.BackgroundImage = ImageFetcher.GetEmbeddedImage("broken")
                    tt.SetToolTip(btnLink, String.Format(NHLGamesMetro.RmText.GetString("tipBrokenStream")))
                Else 
                    If streamType < StreamType.EndzoneCam1 Then
                        If _game.GetStream(streamType).Network <> String.Empty Then
                            Dim img As String = _getBroadcasterPicFor(stream.Network)
                            If img <> "" Then btnLink.BackgroundImage = ImageFetcher.GetEmbeddedImage(img)
                            tooltip &= String.Format(NHLGamesMetro.RmText.GetString("lblOnNetwork"), stream.Network)
                        End If
                    End If
                    tt.SetToolTip(btnLink, tooltip)
                End If

            End If
        End Sub

    End Class
End Namespace
