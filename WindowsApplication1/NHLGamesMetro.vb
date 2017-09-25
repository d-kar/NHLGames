Imports System.IO
Imports System.Security.Permissions
Imports System.Threading
Imports System.Resources
Imports System.Runtime.InteropServices
Imports NHLGames.AdDetection
Imports NHLGames.Controls
Imports NHLGames.My.Resources
Imports NHLGames.Objects
Imports NHLGames.Utilities

Public Class NHLGamesMetro

    Public Shared ServerIp As String
    Public Const DomainName As String = "mf.svc.nhl.com"
    Public Shared HostName As String
    Public Shared FormInstance As NHLGamesMetro = Nothing
    Private _adDetectorViewModel As AdDetectorViewModel = Nothing
    Public Shared ProgressValue As Integer = 0
    Public Shared ProgressMaxValue As Integer = 1000
    Public Shared FlpCalendar As FlowLayoutPanel
    Public Shared StreamStarted As Boolean = False
    Public Shared ProgressVisible As Boolean = False
    Public Shared GamesDownloadedTime As Date
    Public Shared LabelDate As Label
    Public Shared DownloadLink As String = "https://www.reddit.com/r/nhl_games/"
    Public Shared GameDate As Date = DateHelper.GetPacificTime()
    Private _resizeDirection As Integer = -1
    Private Const ResizeBorderWidth As Integer = 3
    Public Shared RmText As ResourceManager = English.ResourceManager
    Public LstGameControls As List(Of GameControl) = New List(Of GameControl)
    Public Shared LstThreads As List(Of Thread) = New List(Of Thread)()
    Public Shared FormLoaded As Boolean = False

    <DllImport("user32.dll")>
    Public Shared Function ReleaseCapture() As Boolean
    End Function

    <DllImport("user32.dll")>
    Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function

    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const HTBOTTOM As Integer = 15
    Private Const HTBOTTOMLEFT As Integer = 16
    Private Const HTBOTTOMRIGHT As Integer = 17
    Private Const HTLEFT As Integer = 10
    Private Const HTRIGHT As Integer = 11
    Private Const HTTOP As Integer = 12
    Private Const HTTOPLEFT As Integer = 13
    Private Const HTTOPRIGHT As Integer = 14

    <SecurityPermission(SecurityAction.Demand, Flags:=SecurityPermissionFlag.ControlAppDomain)>
    Public Shared Sub Main()
        ' Add the event handler for handling UI thread exceptions to the event.
        AddHandler Application.ThreadException, AddressOf Form1_UIThreadException
        ' Set the unhandled exception mode to force all Windows Forms errors to go through our handler.
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)
        ' Add the event handler for handling non-UI thread exceptions to the event. 
        AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf CurrentDomain_UnhandledException
        Dim form As New NHLGamesMetro()
        FormInstance = form
        'Setup redirecting console.out to 
        Dim writer = New ConsoleRedirectStreamWriter(form.RichTextBox)
        Console.SetOut(writer)
        ' Runs the application.
        Application.Run(form)
    End Sub

    ' Handle the UI exceptions by showing a dialog box, and asking the user whether
    ' or not they wish to abort execution.
    Private Shared Sub Form1_UIThreadException(ByVal sender As Object, ByVal t As ThreadExceptionEventArgs)
        Console.WriteLine(English.errorGeneral, t.Exception.ToString())
    End Sub

    Private Shared Sub CurrentDomain_UnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
        Console.WriteLine(e.ExceptionObject.ToString())
    End Sub

    Public Sub HandleException(e As Exception)
        Console.WriteLine(e.ToString())
    End Sub

    Private Sub NHLGames_Load(sender As Object, e As EventArgs) Handles Me.Load
        SuspendLayout()

        Common.GetLanguage()

        If Not File.Exists("NHLGames.exe.Config") then
            Console.WriteLine(English.errorConfigFile)
            FatalError(RmText.GetString("errorConfigFile"))
        End If

        tabMenu.SelectedIndex = 0

        Try
            _adDetectorViewModel = New AdDetectorViewModel()
            AdDetectionSettingsElementHost.Child = _adDetectorViewModel.SettingsControl
        catch ex As Exception
           Console.WriteLine(English.errorSetAdModule, ex.Message)
        end Try

        FlpCalendar = flpCalender
        flpGames.Controls.AddRange(lstGameControls.ToArray())
        InitializeForm.SetLanguage()
        InitializeForm.SetSettings()
        InitializeForm.VersionCheck()

        ResumeLayout()
        FormLoaded = True

    End Sub

    Private Sub FatalError(message As String)
        If InvokeElement.MsgBoxRed(message,RmText.GetString("msgFailure"), MessageBoxButtons.OK) = DialogResult.OK Then
            Me.Close
        End If
    End Sub

    Private Shared Sub _writeToConsoleSettingsChanged(key As String, value As String)
        If FormLoaded Then Console.WriteLine(String.Format(English.msgSettingUpdated, key, value))
    End Sub

    Private Shared Sub tmrAnimate_Tick(sender As Object, e As EventArgs) Handles tmrAnimate.Tick
        If StreamStarted Then
            GameFetcher.StreamingProgress
        Else 
            GameFetcher.LoadingProgress
        End If
        
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        InvokeElement.LoadGamesAsync(GameDate, True)
        flpGames.Focus()
    End Sub

    Private Sub RichTextBox_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox.TextChanged
        RichTextBox.SelectionStart = RichTextBox.Text.Length
        RichTextBox.ScrollToCaret()
    End Sub

    Private Sub btnOpenHostsFile_Click(sender As Object, e As EventArgs) Handles btnOpenHostsFile.Click 
        Dim hostsFilePath As String = Environment.SystemDirectory & "\drivers\etc\hosts"
        Process.Start("NOTEPAD", hostsFilePath)
    End Sub

    Private Sub btnVLCPath_Click(sender As Object, e As EventArgs) Handles btnVLCPath.Click 
        OpenFileDialog.Filter = $"VLC|vlc.exe|All files (*.*)|*.*"
        OpenFileDialog.Multiselect = False
        OpenFileDialog.InitialDirectory = If(txtVLCPath.Text.Equals(String.Empty), "C:\", Path.GetDirectoryName(txtVLCPath.Text))

        If OpenFileDialog.ShowDialog() = DialogResult.OK Then
            If String.IsNullOrEmpty(OpenFileDialog.FileName) = False And txtVLCPath.Text <> OpenFileDialog.FileName Then
                ApplicationSettings.SetValue(SettingsEnum.VlcPath, OpenFileDialog.FileName)
                txtVLCPath.Text = OpenFileDialog.FileName
            End If
        End If
    End Sub

    Private Sub btnMPCPath_Click(sender As Object, e As EventArgs) Handles btnMPCPath.Click 
        OpenFileDialog.Filter = $"MPC|mpc-hc64.exe;mpc-hc.exe|All files (*.*)|*.*"
        OpenFileDialog.Multiselect = False
        OpenFileDialog.InitialDirectory = If(txtMPCPath.Text.Equals(String.Empty), "C:\", Path.GetDirectoryName(txtMPCPath.Text))

        If OpenFileDialog.ShowDialog() = DialogResult.OK Then

            If String.IsNullOrEmpty(OpenFileDialog.FileName) = False And txtMPCPath.Text <> OpenFileDialog.FileName Then
                ApplicationSettings.SetValue(SettingsEnum.MpcPath, OpenFileDialog.FileName)
                txtMPCPath.Text = OpenFileDialog.FileName
            End If

        End If
    End Sub

    Private Sub btnMpvPath_Click(sender As Object, e As EventArgs) Handles btnMpvPath.Click 
        OpenFileDialog.Filter = $"mpv|mpv.exe|All files (*.*)|*.*"
        OpenFileDialog.Multiselect = False
        OpenFileDialog.InitialDirectory = If(txtMpvPath.Text.Equals(String.Empty), "C:\", Path.GetDirectoryName(txtMpvPath.Text))

        If OpenFileDialog.ShowDialog() = DialogResult.OK Then

            If String.IsNullOrEmpty(OpenFileDialog.FileName) = False And txtMpvPath.Text <> OpenFileDialog.FileName Then
                ApplicationSettings.SetValue(SettingsEnum.MpvPath, OpenFileDialog.FileName)
                txtMpvPath.Text = OpenFileDialog.FileName
            End If

        End If
    End Sub

    Private Sub btnstreamlinkPath_Click(sender As Object, e As EventArgs) Handles btnstreamlinkPath.Click 
        OpenFileDialog.Filter = $"streamlink|streamlink.exe|All files (*.*)|*.*"
        OpenFileDialog.Multiselect = False
        OpenFileDialog.InitialDirectory = If(txtStreamlinkPath.Text.Equals(String.Empty), "C:\", Path.GetDirectoryName(txtStreamlinkPath.Text))

        If OpenFileDialog.ShowDialog() = DialogResult.OK Then

            If String.IsNullOrEmpty(OpenFileDialog.FileName) = False And txtStreamlinkPath.Text <> OpenFileDialog.FileName Then
                ApplicationSettings.SetValue(SettingsEnum.StreamlinkPath, OpenFileDialog.FileName)
                txtStreamlinkPath.Text = OpenFileDialog.FileName
            End If

        End If
    End Sub

    Private Sub chkShowFinalScores_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowFinalScores.CheckedChanged 
        ApplicationSettings.SetValue(SettingsEnum.ShowScores, chkShowFinalScores.Checked)
        For each game As GameControl In flpGames.Controls
            game.UpdateGame(GameManager.GamesDict(game.GameId), chkShowFinalScores.Checked, chkShowLiveScores.Checked, chkShowSeriesRecord.Checked)
        Next
    End Sub

    Private Sub btnClearConsole_Click(sender As Object, e As EventArgs) Handles btnClearConsole.Click
        RichTextBox.Clear()
    End Sub

    Private Sub btnHosts_Click(sender As Object, e As EventArgs) Handles btnTestHosts.Click 
        If HostsFile.TestEntry(DomainName, ServerIp) Then
            InvokeElement.MsgBoxBlue(RmText.GetString("msgHostsSuccess"), RmText.GetString("msgSuccess"), MessageBoxButtons.OK)
        Else
            InvokeElement.MsgBoxBlue(RmText.GetString("msgHostsFailure"), RmText.GetString("msgFailure"), MessageBoxButtons.OK)
        End If
    End Sub

    Private Sub chk60_CheckedChanged(sender As Object, e As EventArgs) Handles chk60.CheckedChanged 
        If chk60.Checked Then
            rbQual6.Checked = True
            _writeToConsoleSettingsChanged(lblQuality.Text, rbQual6.Text & " @ " & chk60.Text)
        ElseIf rbQual6.Checked Then
            _writeToConsoleSettingsChanged(lblQuality.Text, rbQual6.Text)
        End If
        Player.RenewArgs()
    End Sub

    Private Sub txtVLCPath_TextChanged(sender As Object, e As EventArgs) Handles txtVLCPath.TextChanged 
        rbVLC.Enabled = True
        Player.RenewArgs()
    End Sub

    Private Sub txtMPCPath_TextChanged(sender As Object, e As EventArgs) Handles txtMPCPath.TextChanged 
        rbMPC.Enabled = True
        Player.RenewArgs()
    End Sub

    Private Sub txtMpvPath_TextChanged(sender As Object, e As EventArgs) Handles txtMpvPath.TextChanged 
        rbMpv.Enabled = True
        Player.RenewArgs()
    End Sub

    Private Sub txtStreamlinkPath_TextChanged(sender As Object, e As EventArgs) Handles txtStreamlinkPath.TextChanged 
        Player.RenewArgs()
    End Sub

    Private Sub quality_CheckedChanged(sender As Object, e As EventArgs) Handles rbQual6.CheckedChanged, rbQual5.CheckedChanged, rbQual4.CheckedChanged, rbQual3.CheckedChanged, rbQual2.CheckedChanged, rbQual1.CheckedChanged 
        Player.RenewArgs()
        Dim rb As RadioButton = sender
        If (Not chk60.Checked And rb.Checked) Then _writeToConsoleSettingsChanged(lblQuality.Text, rb.Text)
    End Sub

    Private Sub player_CheckedChanged(sender As Object, e As EventArgs) Handles rbVLC.CheckedChanged, rbMpv.CheckedChanged, rbMPC.CheckedChanged 
        Dim rb As RadioButton = sender
        If (rb.Checked) Then 
            Player.RenewArgs()
            _writeToConsoleSettingsChanged(lblPlayer.Text, rb.Text)
        End If
    End Sub

    Private Sub rbCDN_CheckedChanged(sender As Object, e As EventArgs) Handles rbLevel3.CheckedChanged, rbAkamai.CheckedChanged 
        Dim rb As RadioButton = sender
        If (rb.Checked) Then 
            Player.RenewArgs()
            _writeToConsoleSettingsChanged(lblCdn.Text, rb.Text)
        End If
    End Sub

    Private Sub txtOutputPath_TextChanged(sender As Object, e As EventArgs) Handles txtOutputArgs.TextChanged 
        Player.RenewArgs()
        _writeToConsoleSettingsChanged(lblOutput.Text, txtOutputArgs.Text)
    End Sub

    Private Sub txtPlayerArgs_TextChanged(sender As Object, e As EventArgs) Handles txtPlayerArgs.TextChanged 
        Player.RenewArgs()
        _writeToConsoleSettingsChanged(lblPlayerArgs.Text, txtPlayerArgs.Text)
    End Sub

    Private Sub txtStreamerArgs_TextChanged(sender As Object, e As EventArgs) Handles txtStreamerArgs.TextChanged 
        Player.RenewArgs()
        _writeToConsoleSettingsChanged(lblStreamerArgs.Text, txtStreamerArgs.Text)
    End Sub

    Private Sub btnOuput_Click(sender As Object, e As EventArgs) Handles btnOuput.Click 
        SaveFileDialog.CheckPathExists = True

        If txtOutputArgs.Text.Count > 0 Then
            SaveFileDialog.InitialDirectory = Path.GetDirectoryName(txtOutputArgs.Text)
            SaveFileDialog.FileName = Path.GetFileName(txtOutputArgs.Text)
        Else
            SaveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)
            SaveFileDialog.FileName = "(DATE)_(HOME)_vs_(AWAY)_(TYPE)_(QUAL)"
        End If

        SaveFileDialog.Filter = $"MP4 Files (*.mp4)|*.MP4"
        SaveFileDialog.DefaultExt = "mp4"
        SaveFileDialog.AddExtension = True

        If SaveFileDialog.ShowDialog() = DialogResult.OK Then
            txtOutputArgs.Text = SaveFileDialog.FileName
            Player.RenewArgs()
        End If
    End Sub

    Private Sub btnYesterday_Click(sender As Object, e As EventArgs) Handles btnYesterday.Click
        GameDate = GameDate.AddDays(-1)
        lblDate.Text = DateHelper.GetFormattedDate(GameDate)
    End Sub

    Private Sub btnTomorrow_Click(sender As Object, e As EventArgs) Handles btnTomorrow.Click
        GameDate = GameDate.AddDays(1)
        lblDate.Text = DateHelper.GetFormattedDate(GameDate)
    End Sub


    Private Sub btnClean_Click(sender As Object, e As EventArgs) Handles btnCleanHosts.Click 
        HostsFile.CleanHosts(DomainName)
    End Sub

    Private Sub lnkVLCDownload_Click(sender As Object, e As EventArgs) Handles lnkGetVlc.Click 
        Dim sInfo As ProcessStartInfo = New ProcessStartInfo("http://www.videolan.org/vlc/download-windows.html")
        Process.Start(sInfo)
    End Sub

    Private Sub lnkMPCDownload_Click(sender As Object, e As EventArgs) Handles lnkGetMpc.Click 
        Dim sInfo As ProcessStartInfo = New ProcessStartInfo("https://mpc-hc.org/downloads/")
        Process.Start(sInfo)
    End Sub

    Private Sub btnAddHosts_Click(sender As Object, e As EventArgs) Handles  btnAddHosts.Click 
        HostsFile.AddEntry(ServerIp, DomainName)
    End Sub

    Private Sub btnDate_Click(sender As Object, e As EventArgs) Handles btnDate.Click
        Dim val = Not flpCalender.Visible
        flpCalender.Visible = val
    End Sub

    Private Sub lblDate_TextChanged(sender As Object, e As EventArgs) Handles lblDate.TextChanged
        InvokeElement.LoadGamesAsync(GameDate)
        flpGames.Focus()
    End Sub

    Private Sub chkShowLiveScores_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowLiveScores.CheckedChanged 
        ApplicationSettings.SetValue(SettingsEnum.ShowLiveScores, chkShowLiveScores.Checked)
        For each game As GameControl In flpGames.Controls
            game.UpdateGame(GameManager.GamesDict(game.GameId), chkShowFinalScores.Checked, chkShowLiveScores.Checked, chkShowSeriesRecord.Checked)
        Next
    End Sub

    Private Sub lnkDownload_Click(sender As Object, e As EventArgs) Handles lnkDownload.Click
        Dim sInfo As ProcessStartInfo = New ProcessStartInfo(DownloadLink)
        Process.Start(sInfo)
    End Sub

    Private Sub TabControl_MouseClick(sender As Object, e As MouseEventArgs) Handles tabMenu.MouseClick
        ProgressVisible = False
        flpCalender.Visible = False
    End Sub

    Private Sub GamesTab_Click(sender As Object, e As EventArgs) Handles tabGames.Click
        flpCalender.Visible = False
    End Sub

    Private Sub FlowLayoutPanel_Click(sender As Object, e As EventArgs) Handles flpGames.Click
        flpCalender.Visible = False
    End Sub

    Private Sub tgStreamer_CheckedChanged(sender As Object, e As EventArgs) Handles tgStreamer.CheckedChanged 
        txtStreamerArgs.Enabled = tgStreamer.Checked
        Player.RenewArgs()
        _writeToConsoleSettingsChanged(String.Format(English.msgThisEnable, lblStreamerArgs.Text), 
                                       if(tgStreamer.Checked, English.msgOn, English.msgOff))
    End Sub

    Private Sub tgPlayer_CheckedChanged(sender As Object, e As EventArgs) Handles tgPlayer.CheckedChanged 
        txtPlayerArgs.Enabled = tgPlayer.Checked
        Player.RenewArgs()
        _writeToConsoleSettingsChanged(String.Format(English.msgThisEnable,lblPlayerArgs.Text), 
                                       if(tgPlayer.Checked, English.msgOn, English.msgOff))
    End Sub

    Private Sub tgOutput_CheckedChanged(sender As Object, e As EventArgs) Handles tgOutput.CheckedChanged 
        txtOutputArgs.Enabled = tgOutput.Checked
        Player.RenewArgs()
        _writeToConsoleSettingsChanged(String.Format(English.msgThisEnable,lblOutput.Text),
                                       if(tgOutput.Checked, English.msgOn, English.msgOff))
    End Sub

    Private Sub chkShowSeriesRecord_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowSeriesRecord.CheckedChanged 
        ApplicationSettings.SetValue(SettingsEnum.ShowSeriesRecord, chkShowSeriesRecord.Checked)
        For each game As GameControl In flpGames.Controls
            game.UpdateGame(GameManager.GamesDict(game.GameId), chkShowFinalScores.Checked, chkShowLiveScores.Checked, chkShowSeriesRecord.Checked)
        Next
    End Sub

    Private Shared Sub btnHelp_Click(sender As Object, e As EventArgs) Handles btnHelp.Click
        Dim sInfo As ProcessStartInfo = New ProcessStartInfo("https://github.com/NHLGames/NHLGames#nhlgames")
        Process.Start(sInfo)
    End Sub

    Private Sub btnMaximize_Click(sender As Object, e As EventArgs) Handles btnMaximize.Click
        WindowState = FormWindowState.Maximized
        btnMaximize.Visible = False
        btnNormal.Visible = True
        RefreshFocus()
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
        WindowState = FormWindowState.Minimized
        RefreshFocus()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub NHLGamesMetro_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        Refresh()
        RefreshFocus()
    End Sub

    Private Sub btnNormal_Click(sender As Object, e As EventArgs) Handles btnNormal.Click
        WindowState = FormWindowState.Normal
        btnMaximize.Visible = True
        btnNormal.Visible = False
        RefreshFocus()
    End Sub

    Private Sub RefreshFocus()
        If tabGames.Visible Then 
            flpGames.Focus()
        ElseIf tabSettings.Visible Then
            tlpSettings.Focus()
        ElseIf tabConsole.Visible Then
            RichTextBox.Focus()
        ElseIf tabAdDetectionModules.Visible Then
            flpAdDetector.Focus()
        Else 
            tabMenu.Focus()
        End If
    End Sub

    Private Sub TabControl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabMenu.SelectedIndexChanged
        RefreshFocus()
    End Sub

    Private Sub NHLGamesMetro_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        'Calculate which direction to resize based on mouse position
        _resizeDirection = -1
        If e.Location.X < ResizeBorderWidth And e.Location.Y < ResizeBorderWidth Then
            Cursor = Cursors.SizeNWSE
            _resizeDirection = HTTOPLEFT
        ElseIf e.Location.X < ResizeBorderWidth And e.Location.Y > Height - ResizeBorderWidth Then
            Cursor = Cursors.SizeNESW
            _resizeDirection = HTBOTTOMLEFT
        ElseIf e.Location.X > Width - ResizeBorderWidth And e.Location.Y > Height - ResizeBorderWidth Then
            Cursor = Cursors.SizeNWSE
            _resizeDirection = HTBOTTOMRIGHT
        ElseIf e.Location.X > Width - ResizeBorderWidth And e.Location.Y < ResizeBorderWidth Then
            Cursor = Cursors.SizeNESW
            _resizeDirection = HTTOPRIGHT
        ElseIf e.Location.X < ResizeBorderWidth Then
            Cursor = Cursors.SizeWE
            _resizeDirection = HTLEFT
        ElseIf e.Location.X > Width - ResizeBorderWidth Then
            Cursor = Cursors.SizeWE
            _resizeDirection = HTRIGHT
        ElseIf e.Location.Y < ResizeBorderWidth Then
            Cursor = Cursors.SizeNS
            _resizeDirection = HTTOP
        ElseIf e.Location.Y > Height - ResizeBorderWidth Then
            Cursor = Cursors.SizeNS
            _resizeDirection = HTBOTTOM
        Else
            Cursor = Cursors.Default
        End If
    End Sub

    Private Sub NHLGamesMetro_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        If e.Button = MouseButtons.Left And WindowState <> FormWindowState.Maximized Then
            ResizeForm()
        End If
    End Sub

    Private Sub ResizeForm()
        If _resizeDirection <> -1 Then
            ReleaseCapture()
            SendMessage(Handle, WM_NCLBUTTONDOWN, _resizeDirection, 0)
        End If
    End Sub

    Private Sub NHLGamesMetro_MouseLeave(sender As Object, e As EventArgs) Handles MyBase.MouseLeave
        Cursor = Cursors.Default
    End Sub

    Private Sub cbServers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbServers.SelectedIndexChanged 
        tlpSettings.Focus()
        ApplicationSettings.SetValue(SettingsEnum.SelectedServer, cbServers.SelectedItem.ToString())
    End Sub

    Private Sub btnCopyConsole_Click(sender As Object, e As EventArgs) Handles btnCopyConsole.Click
        dim player As String = if (rbMpv.Checked,"MPV",If(rbMPC.Checked,"MPC",If(rbVLC.Checked,"VLC","none")))
        Dim x64 As String = if(Environment.Is64BitOperatingSystem,"64 Bits","32 Bits")
        Clipboard.SetText(String.Format(English.textCopyConsole,
                                        vbCrLf,lblVersion.Text,
                                        My.Computer.Info.OSFullName.ToString(), x64.ToString(), 
                                        (Not String.IsNullOrEmpty(lblDate.Text)).ToString(),
                                        HostsFile.TestEntry(DomainName,ServerIp).ToString(),
                                        My.Computer.Network.Ping(ServerIp).ToString(),
                                        cbServers.SelectedItem.ToString(),
                                        player.ToString(),
                                        ApplicationSettings.Read(Of String)(SettingsEnum.StreamlinkPath, String.Empty).ToString(),
                                        (ApplicationSettings.Read(Of String)(SettingsEnum.StreamlinkPath, String.Empty)=txtStreamlinkPath.Text).ToString(),
                                        ApplicationSettings.Read(Of String)(SettingsEnum.VlcPath, String.Empty).ToString(),
                                        (ApplicationSettings.Read(Of String)(SettingsEnum.VlcPath, String.Empty)=txtVLCPath.Text).ToString(),
                                        ApplicationSettings.Read(Of String)(SettingsEnum.MpcPath, String.Empty).ToString(),
                                        (ApplicationSettings.Read(Of String)(SettingsEnum.MpcPath, String.Empty)=txtMPCPath.Text).ToString(),
                                        ApplicationSettings.Read(Of String)(SettingsEnum.MpvPath, String.Empty).ToString(),
                                        (ApplicationSettings.Read(Of String)(SettingsEnum.MpvPath, String.Empty)=txtMpvPath.Text).ToString(),
                                        RichTextBox.Text))
    End Sub



    Private Sub cbLanguage_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbLanguage.SelectedIndexChanged 
        tlpSettings.Focus()
        ApplicationSettings.SetValue(SettingsEnum.SelectedLanguage, cbLanguage.SelectedItem.ToString())
        Common.GetLanguage()
        InitializeForm.SetLanguage()
    End Sub

    Private Sub lnkDiySteps_Click(sender As Object, e As EventArgs) Handles lnkDiySteps.Click 
        DiySteps()
    End Sub

    Public sub DiySteps()
        If InvokeElement.MsgBoxBlue(String.Format(RmText.GetString("msgDiyStepsText"), vbCrLf, ServerIp, DomainName),
                                    RmText.GetString("msgDiySteps"),
                                    MessageBoxButtons.YesNo) = DialogResult.Yes Then
            Clipboard.SetText(ServerIp & vbTab & DomainName)
        End If
    End sub

    Private Sub NHLGamesMetro_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Common.WaitForGameThreads()
    End Sub

End Class
