Imports MetroFramework.Controls
Imports Newtonsoft.Json.Linq
Imports NHLGames.My.Resources
Imports NHLGames.Objects

Namespace Utilities
    Public Class GameFetcher
        Public Shared Sub StreamingProgress
            Dim form As NHLGamesMetro = NHLGamesMetro.FormInstance
            form.spnStreaming.Visible = NHLGamesMetro.ProgressVisible
            form.flpGames.Enabled = False
            form.flpGames.Focus()
            Progress(form.spnStreaming)
        End Sub

        Public Shared Sub LoadingProgress
            Dim form As NHLGamesMetro = NHLGamesMetro.FormInstance
            form.spnLoading.Visible = NHLGamesMetro.ProgressVisible
            form.flpGames.Enabled = True
            Progress(form.spnLoading)
        End Sub

        Private Shared Sub Progress(spinner As MetroProgressSpinner)
            Dim form As NHLGamesMetro = NHLGamesMetro.FormInstance

            If NHLGamesMetro.ProgressValue < spinner.Maximum And NHLGamesMetro.ProgressValue >= 0 Then
                spinner.Value = NHLGamesMetro.ProgressValue
            End If

            If spinner.Visible Then
                form.btnDate.Enabled = False
                form.btnTomorrow.Enabled = False
                form.btnYesterday.Enabled = False
                form.lblNoGames.Visible = False
            Else
                spinner.Value = 0
                form.btnDate.Enabled = True
                form.btnTomorrow.Enabled = True
                form.btnYesterday.Enabled = True
                If (form.flpGames.Controls.Count = 0) Then
                    form.lblNoGames.Visible = True
                Else
                    form.lblNoGames.Visible = False
                End If
            End If

        End Sub

        Public Shared Sub LoadGames(dateTime As DateTime, refreshing As Boolean)
            Try
                NHLGamesMetro.ProgressVisible = True
                NHLGamesMetro.ProgressValue = 0
                InvokeElement.SetFormStatusLabel(NHLGamesMetro.RmText.GetString("msgLoadingGames"))

                If Not refreshing Then
                    GameManager.ClearGames()
                End If

                InvokeElement.ClearGamePanel()

                Dim jsonSchedule As JObject = Downloader.DownloadJsonSchedule(dateTime, refreshing)
                If jsonSchedule.HasValues Then
                    GameManager.GetGames(dateTime, jsonSchedule, refreshing)
                    Common.WaitForGameThreads()
                    NHLGamesMetro.ProgressValue = NHLGamesMetro.ProgressMaxValue - 1
                    Threading.Thread.Sleep(100)
                    InvokeElement.NewGamesFound(GameManager.GamesDict)
                    InvokeElement.SetFormStatusLabel(String.Format(NHLGamesMetro.RmText.GetString("msgGamesFound"),GameManager.GamesList.Count.ToString()))
                    NHLGamesMetro.ProgressVisible = False
                Else 
                    Console.WriteLine(English.errorFetchingGames)
                End If
            Catch ex As Exception
                Console.WriteLine(ex.ToString())
            End Try
        End Sub
    End Class
End Namespace
