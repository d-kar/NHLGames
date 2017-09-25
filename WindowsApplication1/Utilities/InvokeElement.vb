Imports NHLGames.Controls
Imports NHLGames.Objects

Namespace Utilities
    Public Class InvokeElement

        ''' <summary>
        ''' Wrapper for LoadGames to stop UI locking and slow startup
        ''' </summary>
        ''' <param name="dateTime"></param>
        Public Shared Sub LoadGamesAsync(dateTime As DateTime, Optional refreshing As Boolean = False)
            Dim loadGamesFunc As New Action(Of DateTime, Boolean)(Sub(dt As DateTime, rf As Boolean) GameFetcher.LoadGames(dt, rf))
            loadGamesFunc.BeginInvoke(dateTime, refreshing, Nothing, Nothing)
        End Sub

        Public Shared Sub SetFormStatusLabel(msg As String)
            Dim form As NHLGamesMetro = NHLGamesMetro.FormInstance
            If form.InvokeRequired Then
                form.BeginInvoke(New Action(Of String)(AddressOf SetFormStatusLabel), msg)
            Else
                form.lblStatus.Text = msg
            End If
        End Sub

        Public Shared Sub ClearGamePanel()
            Dim form As NHLGamesMetro = NHLGamesMetro.FormInstance
            If form.InvokeRequired Then
                form.BeginInvoke(New Action(AddressOf ClearGamePanel))
            Else
                form.flpGames.Controls.Clear()
            End If
        End Sub

        Public Shared Sub NewGamesFound(gamesDict As Dictionary(Of String, Game))
            Dim form As NHLGamesMetro = NHLGamesMetro.FormInstance
            If form.InvokeRequired Then
                form.BeginInvoke(New Action(Of Dictionary(Of String, Game))(AddressOf NewGamesFound), gamesDict)
            Else
                form.flpGames.Controls.AddRange((From game In gamesDict Select New GameControl(
                    game.Value,
                    ApplicationSettings.Read(Of Boolean)(SettingsEnum.ShowScores),
                    ApplicationSettings.Read(Of Boolean)(SettingsEnum.ShowLiveScores),
                    ApplicationSettings.Read(Of Boolean)(SettingsEnum.ShowSeriesRecord))).ToArray())
            End If
        End Sub

        Public Shared Function MsgBoxRed(message As String, title As String, buttons As MessageBoxButtons) As DialogResult
            Dim form As NHLGamesMetro = NHLGamesMetro.FormInstance
            Dim result As DialogResult = New DialogResult()
            If form.InvokeRequired Then
                form.BeginInvoke(New Action(Of String, String, MessageBoxButtons)(AddressOf MsgBoxRed), message, title, buttons)
            Else
                form.tabMenu.SelectedIndex = 2
                result = MetroFramework.MetroMessageBox.Show(form,
                                                           message,
                                                           title, 
                                                           buttons,
                                                           MessageBoxIcon.Error)
            End If
            Return result
        End Function

        Public Shared Function MsgBoxBlue(message As String, title As String, buttons As MessageBoxButtons) As DialogResult
            Dim form As NHLGamesMetro = NHLGamesMetro.FormInstance
            Dim result As DialogResult = New DialogResult()
            If form.InvokeRequired Then
                form.BeginInvoke(New Action(Of String, String, MessageBoxButtons)(AddressOf MsgBoxBlue), message, title, buttons)
            Else
                result = MetroFramework.MetroMessageBox.Show(form,
                                                             message,
                                                             title, 
                                                             buttons,
                                                             MessageBoxIcon.Information)
            End If
            Return result
        End Function

    End Class
End Namespace
