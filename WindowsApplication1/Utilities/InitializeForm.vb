Imports System.IO
Imports System.Net
Imports NHLGames.My.Resources
Imports NHLGames.Objects

Namespace Utilities
    Public Class InitializeForm
        Private ReadOnly Shared Form As NHLGamesMetro = NHLGamesMetro.FormInstance

        Public Shared Sub VersionCheck()
            Dim strLatest = Downloader.DownloadApplicationVersion()
            Dim versionFromSettings = ApplicationSettings.Read(Of String)(SettingsEnum.Version, "")

            If strLatest > versionFromSettings Then
                Form.lnkDownload.Text = String.Format(
                    NHLGamesMetro.RmText.GetString("msgNewVersionText"), 
                    strLatest)
                Form.lnkDownload.Width = 700
                Dim strChangeLog = Downloader.DownloadChangelog()
                InvokeElement.MsgBoxBlue(String.Format(NHLGamesMetro.RmText.GetString("msgChangeLog"), strLatest, vbCrLf, vbCrLf, strChangeLog),
                                         NHLGamesMetro.RmText.GetString("msgNewVersionAvailable"),
                                         MessageBoxButtons.OK)
            End If
            Form.lblVersion.Text = String.Format("v{0}", ApplicationSettings.Read(Of String)(SettingsEnum.Version))
        End Sub

        Public Shared Sub SetLanguage()
            'Main
            Form.tabMenu.TabPages.Item(0).Text = NHLGamesMetro.RmText.GetString("tabGames")
            Form.tabMenu.TabPages.Item(1).Text = NHLGamesMetro.RmText.GetString("tabSettings")
            Form.tabMenu.TabPages.Item(2).Text = NHLGamesMetro.RmText.GetString("tabConsole")
            Form.tabMenu.TabPages.Item(3).Text = NHLGamesMetro.RmText.GetString("tabModules")

            Form.lblNoGames.Text = NHLGamesMetro.RmText.GetString("lblNoGames")
       
            'Games

            'Settings
            Form.lblShowScores.Text = NHLGamesMetro.RmText.GetString("lblShowScores")
            Form.lblPlayer.Text = NHLGamesMetro.RmText.GetString("lblPlayer")
            Form.lblQuality.Text = NHLGamesMetro.RmText.GetString("lblQuality")
            Form.lblCdn.Text = NHLGamesMetro.RmText.GetString("lblCdn")
            Form.lblHostname.Text = NHLGamesMetro.RmText.GetString("lblHostname")
            Form.lblHosts.Text = NHLGamesMetro.RmText.GetString("lblHosts")
            Form.lblVlcPath.Text = NHLGamesMetro.RmText.GetString("lblVlcPath")
            Form.lblMpcPath.Text = NHLGamesMetro.RmText.GetString("lblMpcPath")
            Form.lblMpvPath.Text = NHLGamesMetro.RmText.GetString("lblMpvPath")
            Form.lblSlPath.Text = NHLGamesMetro.RmText.GetString("lblSlPath")
            Form.lblOutput.Text = NHLGamesMetro.RmText.GetString("lblOutput")
            Form.lblPlayerArgs.Text = NHLGamesMetro.RmText.GetString("lblPlayerArgs")
            Form.lblStreamerArgs.Text = NHLGamesMetro.RmText.GetString("lblStreamerArgs")
            Form.lblLanguage.Text = NHLGamesMetro.RmText.GetString("lblLanguage")
            Form.lblNoteCdn.Text = NHLGamesMetro.RmText.GetString("lblNoteCdn")

            Form.chkShowFinalScores.Text = NHLGamesMetro.RmText.GetString("chkShowFinalScores")
            Form.chkShowLiveScores.Text = NHLGamesMetro.RmText.GetString("chkShowLiveScores")
            Form.chkShowSeriesRecord.Text = NHLGamesMetro.RmText.GetString("chkShowSeriesRecord")

            Form.btnTestHosts.Text = NHLGamesMetro.RmText.GetString("btnTestHosts")
            Form.btnOpenHostsFile.Text = NHLGamesMetro.RmText.GetString("btnOpenHostsFile")
            Form.btnAddHosts.Text = NHLGamesMetro.RmText.GetString("btnAddHosts")
            Form.btnCleanHosts.Text = NHLGamesMetro.RmText.GetString("btnCleanHosts")

            Form.SettingsToolTip.SetToolTip(Form.lnkDiySteps, NHLGamesMetro.RmText.GetString("msgDiySteps"))
            Form.SettingsToolTip.SetToolTip(Form.lnkGetVlc, NHLGamesMetro.RmText.GetString("lblGetVlc"))
            Form.SettingsToolTip.SetToolTip(Form.lnkGetMpc, NHLGamesMetro.RmText.GetString("lblGetMpc"))
            Form.SettingsToolTip.SetToolTip(Form.btnTestHosts, NHLGamesMetro.RmText.GetString("msgTestHosts"))
            Form.SettingsToolTip.SetToolTip(Form.btnOpenHostsFile, NHLGamesMetro.RmText.GetString("msgViewHosts"))
            Form.SettingsToolTip.SetToolTip(Form.btnAddHosts, NHLGamesMetro.RmText.GetString("msgAddHost"))
            Form.SettingsToolTip.SetToolTip(Form.btnCleanHosts, NHLGamesMetro.RmText.GetString("msgRemoveHost"))
        
            'Console
            Form.btnCopyConsole.Text = NHLGamesMetro.RmText.GetString("btnCopyConsole")
            Form.btnClearConsole.Text = NHLGamesMetro.RmText.GetString("btnClearConsole")

            'Calendar
            Form.flpCalender.Controls.Clear()
            Form.flpCalender.Controls.Add(New Controls.CalenderControl)
        
        End Sub

        Public Shared Sub SetSettings()

            Form.SettingsToolTip.SetToolTip(Form.rbQual1, "300" & NHLGamesMetro.RmText.GetString("formatMbHour"))
            Form.SettingsToolTip.SetToolTip(Form.rbQual2, "500" & NHLGamesMetro.RmText.GetString("formatMbHour"))
            Form.SettingsToolTip.SetToolTip(Form.rbQual3, "700" & NHLGamesMetro.RmText.GetString("formatMbHour"))
            Form.SettingsToolTip.SetToolTip(Form.rbQual4, "950" & NHLGamesMetro.RmText.GetString("formatMbHour"))
            Form.SettingsToolTip.SetToolTip(Form.rbQual5, "1.3" & NHLGamesMetro.RmText.GetString("formatGbHour"))
            Form.SettingsToolTip.SetToolTip(Form.rbQual6, "1.8" & NHLGamesMetro.RmText.GetString("formatGbHour"))
            Form.SettingsToolTip.SetToolTip(Form.chk60, "+700" & NHLGamesMetro.RmText.GetString("formatMbHour") &" (+40%)")

            Dim mpcPath As String = ApplicationSettings.Read(Of String)(SettingsEnum.MpcPath, String.Empty)
            Dim mpcPathCurrent As String = PathFinder.GetPathOfMpc
            If mpcPath.Equals(String.Empty) And Not mpcPathCurrent.Equals(String.Empty) Then
                ApplicationSettings.SetValue(SettingsEnum.MpcPath, mpcPathCurrent)
                mpcPath = mpcPathCurrent
            ElseIf mpcPath <> mpcPathCurrent And Not mpcPathCurrent.Equals(String.Empty) Then
                ApplicationSettings.SetValue(SettingsEnum.MpcPath, mpcPathCurrent)
                mpcPath = mpcPathCurrent
            End If
            If Not File.Exists(mpcPath) Then
                mpcPath = String.Empty
                ApplicationSettings.SetValue(SettingsEnum.MpcPath, mpcPath)
            End If
            Form.txtMPCPath.Text = mpcPath

            Dim vlcPath As String = ApplicationSettings.Read(Of String)(SettingsEnum.VlcPath, String.Empty)
            Dim vlcPathCurrent As String = PathFinder.GetPathOfVlc
            If vlcPath.Equals(String.Empty) And Not vlcPathCurrent.Equals(String.Empty) Then
                ApplicationSettings.SetValue(SettingsEnum.VlcPath, vlcPathCurrent)
                vlcPath = vlcPathCurrent
            ElseIf vlcPath <> vlcPathCurrent And Not vlcPathCurrent.Equals(String.Empty) Then
                ApplicationSettings.SetValue(SettingsEnum.VlcPath, vlcPathCurrent)
                vlcPath = vlcPathCurrent
            End If
            If Not File.Exists(vlcPath) Then
                vlcPath = String.Empty
                ApplicationSettings.SetValue(SettingsEnum.VlcPath, vlcPath)
            End If
            Form.txtVLCPath.Text = vlcPath

            Dim mpvPath As String = ApplicationSettings.Read(Of String)(SettingsEnum.MpvPath, String.Empty)
            Dim mpvPathCurrent As String = Path.Combine(Application.StartupPath, "mpv\mpv.exe")
            If mpvPath.Equals(String.Empty) Then
                ApplicationSettings.SetValue(SettingsEnum.MpvPath, mpvPathCurrent)
                mpvPath = mpvPathCurrent
            ElseIf mpvPath <> mpvPathCurrent Then
                If File.Exists(mpvPathCurrent) Then
                    ApplicationSettings.SetValue(SettingsEnum.MpvPath, mpvPathCurrent)
                    mpvPath = mpvPathCurrent
                End If
            End If
            If Not File.Exists(mpvPath) Then
                mpvPath = String.Empty
                ApplicationSettings.SetValue(SettingsEnum.MpvPath, mpvPath)
            End If
            Form.txtMpvPath.Text = mpvPath

            Dim streamlinkPath As String = ApplicationSettings.Read(Of String)(SettingsEnum.StreamlinkPath, String.Empty)
            Dim streamlinkPathCurrent As String = Path.Combine(Application.StartupPath, "streamlink-0.8.1\streamlink.exe")
            If streamlinkPath.Equals(String.Empty) Then
                ApplicationSettings.SetValue(SettingsEnum.StreamlinkPath, streamlinkPathCurrent)
                streamlinkPath = streamlinkPathCurrent
            ElseIf streamlinkPath <> streamlinkPathCurrent Then
                If File.Exists(streamlinkPathCurrent) Then
                    ApplicationSettings.SetValue(SettingsEnum.StreamlinkPath, streamlinkPathCurrent)
                    streamlinkPath = streamlinkPathCurrent
                End If
            End If
            If Not File.Exists(streamlinkPath) Then
                streamlinkPath = String.Empty
                ApplicationSettings.SetValue(SettingsEnum.StreamlinkPath, streamlinkPath)
            End If
            Form.txtStreamlinkPath.Text = streamlinkPath

            Form.chkShowFinalScores.Checked = ApplicationSettings.Read(Of Boolean)(SettingsEnum.ShowScores, True)
            Form.chkShowLiveScores.Checked = ApplicationSettings.Read(Of Boolean)(SettingsEnum.ShowLiveScores, True)
            Form.chkShowSeriesRecord.Checked = ApplicationSettings.Read(Of Boolean)(SettingsEnum.ShowSeriesRecord, True)

            Dim playersPath As String() = New String() {mpvPath, mpcPath, vlcPath}
            Dim watchArgs As GameWatchArguments = ApplicationSettings.Read(Of GameWatchArguments)(SettingsEnum.DefaultWatchArgs)

            If ValidWatchArgs(watchArgs, playersPath, streamlinkPath) Then
                Player.RenewArgs(True)
                watchArgs = ApplicationSettings.Read(Of GameWatchArguments)(SettingsEnum.DefaultWatchArgs)
            End If

            Dim languageListFromConfig As String = ApplicationSettings.Read(Of String)(SettingsEnum.LanguageList, String.Empty)
            Dim languageList As String() = languageListFromConfig.Split(";")
            For Each lang In languageList
                Form.cbLanguage.Items.Add(lang)
            Next
            Form.cbLanguage.SelectedItem = ApplicationSettings.Read(Of String)(SettingsEnum.SelectedLanguage, String.Empty)
            If Form.cbLanguage.SelectedItem Is Nothing Then
                Form.cbLanguage.SelectedItem = Form.cbLanguage.Items(0)
            End If

            Dim serverListFromConfig As String = ApplicationSettings.Read(Of String)(SettingsEnum.ServerList, String.Empty)
            Dim serverList As String() = serverListFromConfig.Split(";")
            For Each server In serverList
                Form.cbServers.Items.Add(server)
            Next
            Form.cbServers.SelectedItem = ApplicationSettings.Read(Of String)(SettingsEnum.SelectedServer, String.Empty)
            If Form.cbServers.SelectedItem Is Nothing Then
                Form.cbServers.SelectedItem = Form.cbServers.Items(0)
            End If
            NHLGamesMetro.ServerIp = Dns.GetHostEntry(Form.cbServers.SelectedItem.ToString()).AddressList.First.ToString()
            NHLGamesMetro.HostName = Form.cbServers.SelectedItem.ToString()

            BindWatchArgsToForm(watchArgs)

            If (HostsFile.TestEntry( NHLGamesMetro.DomainName, NHLGamesMetro.ServerIp) = False) Then
                If InvokeElement.MsgBoxBlue(NHLGamesMetro.RmText.GetString("msgHostnameSet"), 
                                            NHLGamesMetro.RmText.GetString("msgAddHost"), 
                                            MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    HostsFile.AddEntry(NHLGamesMetro.ServerIp,  NHLGamesMetro.DomainName)
                Else
                    Form.tabMenu.SelectedIndex = 1
                    NHLGamesMetro.DiySteps()
                End If
            End If

            Form.lblNoGames.Location = New Point(
                ((Form.flpGames.Width - Form.lblNoGames.Width) / 2), 
                Form.flpGames.Location.Y + 175)
            Form.spnLoading.Location = New Point( 
                ((Form.flpGames.Width - Form.lblNoGames.Width) / 2) + 42, 
                Form.flpGames.Location.Y + 150)

            Form.spnLoading.Value = NHLGamesMetro.ProgressValue
            Form.spnLoading.Maximum = NHLGamesMetro.ProgressMaxValue
            Form.spnStreaming.Value = NHLGamesMetro.ProgressValue
            Form.spnStreaming.Maximum = NHLGamesMetro.ProgressMaxValue
            Form.lblDate.Text = DateHelper.GetFormattedDate(NHLGamesMetro.GameDate)
            
            NHLGamesMetro.LabelDate = Form.lblDate
            NHLGamesMetro.GamesDownloadedTime = Now

            'remove wip tab modules
            Form.tabMenu.TabPages.RemoveAt(4)

        End Sub

        Private Shared Function ValidWatchArgs(watchArgs As GameWatchArguments, playersPath As String(), streamLinkPath As String) As Boolean
            If watchArgs Is Nothing Then Return True

            Dim hasPlayerSet As Boolean = playersPath.Any(Function(x) x = watchArgs.PlayerPath)
            If Not watchArgs.StreamlinkPath.Equals(streamlinkPath) Then Return True
            If Not hasPlayerSet Then
                watchArgs.PlayerType = PlayerTypeEnum.None
                watchArgs.PlayerPath = String.Empty
                Form.rbMPC.Enabled = False
                Form.rbVLC.Enabled = False
                Form.rbMpv.Enabled = False
                Return True
            End If

            Return False
        End Function

        Private Shared Sub BindWatchArgsToForm(watchArgs As GameWatchArguments)

            If watchArgs IsNot Nothing Then
                Form.chk60.Checked = watchArgs.Is60Fps

                Select Case watchArgs.Quality
                    Case "720p"
                        Form.rbQual6.Checked = True
                    Case "540p"
                        Form.rbQual5.Checked = True
                    Case "504p"
                        Form.rbQual4.Checked = True
                    Case "360p"
                        Form.rbQual3.Checked = True
                    Case "288p"
                        Form.rbQual2.Checked = True
                    Case "224p"
                        Form.rbQual1.Checked = True
                End Select

                If watchArgs.Cdn = "akc" Then
                    Form.rbAkamai.Checked = True
                ElseIf watchArgs.Cdn = "l3c" Then
                    Form.rbLevel3.Checked = True
                End If

                Form.rbVLC.Checked = watchArgs.PlayerType = PlayerTypeEnum.Vlc
                Form.rbMPC.Checked = watchArgs.PlayerType = PlayerTypeEnum.Mpc
                Form.rbMpv.Checked = watchArgs.PlayerType = PlayerTypeEnum.Mpv

                If Form.rbVLC.Checked AndAlso watchArgs.PlayerPath <> Form.txtVLCPath.Text Then
                    Player.RenewArgs()
                ElseIf Form.rbMPC.Checked AndAlso watchArgs.PlayerPath <> Form.txtMPCPath.Text Then
                    Player.RenewArgs()
                ElseIf Form.rbMpv.Checked AndAlso watchArgs.PlayerPath <> Form.txtMpvPath.Text Then
                    Player.RenewArgs()
                End If

                Form.tgPlayer.Checked = watchArgs.UsePlayerArgs
                Form.txtPlayerArgs.Enabled = watchArgs.UsePlayerArgs
                Form.txtPlayerArgs.Text = watchArgs.PlayerArgs

                Form.tgStreamer.Checked = watchArgs.UsestreamlinkArgs
                Form.txtStreamerArgs.Enabled = watchArgs.UsestreamlinkArgs
                Form.txtStreamerArgs.Text = watchArgs.StreamlinkArgs

                Form.txtOutputArgs.Text = watchArgs.PlayerOutputPath
                Form.txtOutputArgs.Enabled = watchArgs.UseOutputArgs
                Form.tgOutput.Checked = watchArgs.UseOutputArgs
            End If
        End Sub

    End Class
End Namespace
