Imports NHLGames.Game

Public Class GameControl

    Private _Game As Game
    Private _selectedDate As Date
    Private _PVR As Dictionary(Of String, Tuple(Of Boolean, Boolean))

    Public Sub New(Game As Game, showScores As Boolean, selectedDate As Date, PVR As Dictionary(Of String, Tuple(Of Boolean, Boolean)))
        InitializeComponent()

        _Game = Game
        _selectedDate = selectedDate
        _PVR = PVR

        SetInitialProperties(Game)
        UpdateGameStatusProperties(Game)

        lblHomeScore.Visible = showScores
        lblAwayScore.Visible = showScores

        AddHandler _Game.GameUpdated, AddressOf GameUpdatedHandler

    End Sub

    Private Sub UpdateGameStatusProperties(Game As Game)
        lblHomeScore.Text = Game.HomeScore
        lblHomeTeam.Text = Game.HomeAbbrev

        lblAwayScore.Text = Game.AwayScore
        lblAwayTeam.Text = Game.AwayAbbrev

        'btnWatch.Enabled = Game.Date <= DateTime.Now 'AndAlso Game.GameIsLive

        If Game.Date <= DateTime.Now.ToUniversalTime() AndAlso Game.GameIsLive Then
            BorderPanel1.BorderColour = Color.Green
            'lblVS.Visible = True
        ElseIf Game.Date <= DateTime.Now.ToUniversalTime() Then
            BorderPanel1.BorderColour = Color.LightGray
            'lblVS.Visible = True
        Else
            BorderPanel1.BorderColour = Color.DarkGray
            'lblVS.Visible = False
        End If
    End Sub

    Private Sub SetInitialProperties(Game As Game)
        picAway.SizeMode = PictureBoxSizeMode.StretchImage
        If String.IsNullOrEmpty(Game.HomeTeamLogo) = False Then
            picAway.Image = ImageFetcher.GetEmbeddedImage(Game.AwayTeamLogo)
            AwayTeamToolTip.SetToolTip(picAway, "Away Team: " & Game.AwayTeam)
        End If


        picHome.SizeMode = PictureBoxSizeMode.StretchImage
        If String.IsNullOrEmpty(Game.AwayTeamLogo) = False Then
            picHome.Image = ImageFetcher.GetEmbeddedImage(Game.HomeTeamLogo)
            HomeTeamToolTip.SetToolTip(picHome, "Home Team: " & Game.HomeTeam)
        End If

        lblTime.Text = Game.Date.ToLocalTime().ToString("h:mm tt")

        lblAwayStream.Visible = Game.AwayStream.IsAvailable
        If Game.AwayStream.Network <> String.Empty Then
            lblAwayStream.Text = lblAwayStream.Text & " (" & Game.AwayStream.Network & ")"
        End If
        lblHomeStream.Visible = Game.HomeStream.IsAvailable
        If Game.HomeStream.Network <> String.Empty Then
            lblHomeStream.Text = lblHomeStream.Text & " (" & Game.HomeStream.Network & ")"
        End If
        lblFrenchStream.Visible = Game.FrenchStream.IsAvailable
        If Game.FrenchStream.Network <> String.Empty Then
            lblFrenchStream.Text = lblFrenchStream.Text & " (" & Game.FrenchStream.Network & ")"
        End If
        lblNationalStream.Visible = Game.NationalStream.IsAvailable
        If Game.NationalStream.Network <> String.Empty Then
            lblNationalStream.Text = lblNationalStream.Text & " (" & Game.NationalStream.Network & ")"
        End If

        If (Not Game.AwayStream.IsAvailable And
            Not Game.HomeStream.IsAvailable And
            Not Game.FrenchStream.IsAvailable And
            Not Game.NationalStream.IsAvailable) Then
            lblPVRHome.Visible = True
            lblPVRAway.Visible = True

            Dim curVal As Tuple(Of Boolean, Boolean) = New Tuple(Of Boolean, Boolean)(False, False)
            If (_PVR.TryGetValue(_Game.GameID, curVal)) Then
                If (curVal.Item1) Then
                    lblPVRHome.LinkColor = Color.Orange
                End If
                If (curVal.Item2) Then
                    lblPVRAway.LinkColor = Color.Orange
                End If
            End If
        End If
    End Sub

    Private Sub GameUpdatedHandler(game As Game)
        _Game = game
        UpdateGameStatusProperties(game)
    End Sub

    Private Sub WatchGameStreamHandler(stream As GameStream, label As LinkLabel)
        Dim args = WatchArgs()

        If DateHelper.GetPacificTime(_Game.Date).ToShortDateString <> DateHelper.GetPacificTime().ToShortDateString() Then
            stream.CheckVOD(args.CDN)
            args.IsVOD = stream.IsVOD
        End If

        'If DateHelper.GetPacificTime(_Game.Date).ToShortDateString = _selectedDate.ToShortDateString() Then
        '    stream.CheckDuplicate(args.CDN)
        'End If

        _Game.Watch(stream, args)
        label.LinkColor = Color.Green
    End Sub

    Private Sub lblHomeStream_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblHomeStream.LinkClicked
        WatchGameStreamHandler(_Game.HomeStream, lblHomeStream)
    End Sub

    Private Sub lblAwayStream_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblAwayStream.LinkClicked
        WatchGameStreamHandler(_Game.AwayStream, lblAwayStream)
    End Sub

    Private Sub lblFrenchStream_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblFrenchStream.LinkClicked
        WatchGameStreamHandler(_Game.FrenchStream, lblFrenchStream)
    End Sub

    Private Sub lblNationalStream_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblNationalStream.LinkClicked
        WatchGameStreamHandler(_Game.NationalStream, lblNationalStream)
    End Sub

    Private Sub lblPVRHome_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblPVRHome.LinkClicked
        Dim curVal As Tuple(Of Boolean, Boolean) = New Tuple(Of Boolean, Boolean)(True, False)
        If (_PVR.TryGetValue(_Game.GameID, curVal)) Then
            _PVR(_Game.GameID) = New Tuple(Of Boolean, Boolean)(Not curVal.Item1, curVal.Item2)
            If (Not curVal.Item1) Then
                lblPVRHome.LinkColor = Color.Orange
            Else
                lblPVRHome.LinkColor = Color.Red
            End If
        Else
            _PVR.Add(_Game.GameID, New Tuple(Of Boolean, Boolean)(True, False))
            lblPVRHome.LinkColor = Color.Orange
        End If
    End Sub

    Private Sub lblPVRAway_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lblPVRAway.LinkClicked
        Dim curVal As Tuple(Of Boolean, Boolean) = New Tuple(Of Boolean, Boolean)(False, True)
        If (_PVR.TryGetValue(_Game.GameID, curVal)) Then
            _PVR(_Game.GameID) = New Tuple(Of Boolean, Boolean)(curVal.Item1, Not curVal.Item2)
            If (Not curVal.Item2) Then
                lblPVRAway.LinkColor = Color.Orange
            Else
                lblPVRAway.LinkColor = Color.Red
            End If
        Else
            _PVR.Add(_Game.GameID, New Tuple(Of Boolean, Boolean)(False, True))
            lblPVRAway.LinkColor = Color.Orange
        End If
    End Sub

    Private Function WatchArgs() As Game.GameWatchArguments
        Dim args = ApplicationSettings.Read(Of Game.GameWatchArguments)(ApplicationSettings.Settings.DefaultWatchArgs)
        args.GameTitle = _Game.AwayAbbrev & " @ " & _Game.HomeAbbrev
        Return args
    End Function
End Class

