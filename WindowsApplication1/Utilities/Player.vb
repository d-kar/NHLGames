Imports System.Threading
Imports NHLGames.My.Resources
Imports NHLGames.Objects

Namespace Utilities
    Public Class Player
        Public Shared Sub Watch(args As GameWatchArguments)
            Dim form As NHLGamesMetro = NHLGamesMetro.FormInstance

            If args.PlayerPath.Equals(String.Empty) OrElse args.StreamlinkPath.Equals(String.Empty) Then
                If form.txtStreamlinkPath.Text.Equals(String.Empty) Then
                    Console.WriteLine(English.errorStreamlinkExe)
                ElseIf form.txtMpvPath.Text.Equals(String.Empty) AndAlso form.txtVLCPath.Text.Equals(String.Empty) AndAlso form.txtMPCPath.Text.Equals(String.Empty) Then
                    Console.WriteLine(English.errorMpvExe)
                Else 
                    Console.WriteLine(English.errorPlayerPathEmpty)
                End If
                
                Return
            End If

            'current strings StreamLink shows into console, keep it updated to make sure the progress bar moves.
            Dim lstKeywords As New List(Of String) From {"Found matching plugin stream", "Available streams", "Opening stream", "Starting player"}
            Dim progressStep As Integer = (NHLGamesMetro.ProgressMaxValue) / (lstKeywords.Count +1)

            Dim taskLaunchingStream As Task = New Task(Sub()
                NHLGamesMetro.ProgressValue = 0
                NHLGamesMetro.StreamStarted = True
                NHLGamesMetro.ProgressVisible = True
                Console.WriteLine(English.msgStartingApp, args.StreamlinkPath, args.ToString(True))

                Dim procStreaming = New Process() With {.StartInfo =
                        New ProcessStartInfo With {
                        .FileName = args.StreamlinkPath,
                        .Arguments = args.ToString(),
                        .UseShellExecute = False,
                        .RedirectStandardOutput = True,
                        .CreateNoWindow = True}
                        }
                procStreaming.EnableRaisingEvents = True

                Dim taskPlayerWatcher As Task = New Task(Sub()
                    Dim processes As Process() = Process.GetProcesses()
                    Dim i As Integer = 0
                    While Not processes.Any(Function(p) p.ProcessName.ToLower().Contains(args.PlayerType.ToString().ToLower()) OrElse NHLGamesMetro.StreamStarted = False OrElse i = 10)
                        processes = Process.GetProcesses()
                        Thread.Sleep(1000)
                        i += 1
                    End While
                    NHLGamesMetro.ProgressValue = NHLGamesMetro.ProgressMaxValue-1
                    Thread.Sleep(1000)
                    NHLGamesMetro.ProgressVisible = False
                    Thread.Sleep(1000)
                    NHLGamesMetro.StreamStarted = False
                End Sub)

                Try
                    procStreaming.Start()
                    While (procStreaming.StandardOutput.EndOfStream = False)
                        Dim line = procStreaming.StandardOutput.ReadLine()
                        If line.Contains(lstKeywords(0)) Or line.Contains("Unable to open URL:") Then
                            line = line.Substring(0, line.IndexOf("http://", StringComparison.Ordinal)) & 
                                                     "--URL CENSORED--." & 
                                                     line.Substring(line.IndexOf("m3u8", StringComparison.Ordinal))
                        End If
                        If lstKeywords.Any(Function(x) line.Contains(x)) Then
                            NHLGamesMetro.ProgressValue += progressStep
                        End If
                        If line.Contains(lstKeywords(3)) Then
                            taskPlayerWatcher.Start()
                        End If
                        Console.WriteLine(line)
                        Thread.Sleep(100) 'to let some time for the progress bar to move
                    End While
                Catch ex As Exception
                    Console.WriteLine(English.errorGeneral, ex.Message.ToString())
                    NHLGamesMetro.ProgressVisible = False
                    Thread.Sleep(1000)
                    NHLGamesMetro.StreamStarted = False
                End Try
            End Sub)

            taskLaunchingStream.Start()

        End Sub

        Public Shared Sub RenewArgs(Optional forceSet As Boolean = False)

            Dim form As NHLGamesMetro = NHLGamesMetro.FormInstance

            If NHLGamesMetro.FormLoaded OrElse forceSet Then
                Dim watchArgs As New GameWatchArguments
                watchArgs.Is60Fps = form.chk60.Checked

                If form.rbQual6.Checked Then
                    watchArgs.Quality = "720p"
                ElseIf form.rbQual5.Checked Then
                    watchArgs.Quality = "540p"
                    form.chk60.Checked = False
                ElseIf form.rbQual4.Checked Then
                    watchArgs.Quality = "504p"
                    form.chk60.Checked = False
                ElseIf form.rbQual3.Checked Then
                    watchArgs.Quality = "360p"
                    form.chk60.Checked = False
                ElseIf form.rbQual2.Checked Then
                    watchArgs.Quality = "288p"
                    form.chk60.Checked = False
                ElseIf form.rbQual1.Checked Then
                    watchArgs.Quality = "224p"
                    form.chk60.Checked = False
                End If

                If form.rbMpv.Checked Then
                    watchArgs.PlayerType = PlayerTypeEnum.Mpv
                    watchArgs.PlayerPath = form.txtMpvPath.Text
                ElseIf form.rbMPC.Checked Then
                    watchArgs.PlayerType = PlayerTypeEnum.Mpc
                    watchArgs.PlayerPath = form.txtMPCPath.Text
                ElseIf form.rbVLC.Checked Then
                    watchArgs.PlayerType = PlayerTypeEnum.Vlc
                    watchArgs.PlayerPath = form.txtVLCPath.Text
                
                End If

                watchArgs.StreamlinkPath = form.txtStreamlinkPath.Text

                If form.rbAkamai.Checked Then
                    watchArgs.Cdn = "akc"
                ElseIf form.rbLevel3.Checked Then
                    watchArgs.Cdn = "l3c"
                End If

                watchArgs.UsePlayerArgs = form.tgPlayer.Checked
                watchArgs.PlayerArgs = form.txtPlayerArgs.Text

                watchArgs.UsestreamlinkArgs = form.tgStreamer.Checked
                watchArgs.StreamlinkArgs = form.txtStreamerArgs.Text

                watchArgs.UseOutputArgs = form.tgOutput.Checked
                watchArgs.PlayerOutputPath = form.txtOutputArgs.Text
                ApplicationSettings.SetValue(SettingsEnum.DefaultWatchArgs, Serialization.SerializeObject(Of GameWatchArguments)(watchArgs))
            End If
        End Sub
    End Class
End Namespace

