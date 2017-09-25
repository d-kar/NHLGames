Imports System.Configuration
Imports NHLGames.My.Resources

Namespace Utilities

    Public Class ApplicationSettings

        Public Shared Function Read(Of T)(key As SettingsEnum, Optional defaultReturnValue As Object = Nothing) As T
            Try
                Dim configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                Dim settings = configFile.AppSettings.Settings
                Dim result As Object = settings(key.ToString()).Value
                If String.IsNullOrEmpty(result) OrElse IsNothing(result) Then
                    Return defaultReturnValue
                End If

                If TypeOf result Is T Then
                    result = DirectCast(result, T)
                    Return result
                End If

                Dim boolReturn As Boolean
                If Boolean.TryParse(result, boolReturn) Then
                    Return CType(result, T)
                End If

                Try
                    Return Serialization.DeserializeObject(Of T)(result)
                Catch ex As Exception
                    Console.WriteLine(English.errorDeserialize, key.ToString(), GetType(T).ToString())
                    Return defaultReturnValue
                End Try

            Catch e As ConfigurationErrorsException
                Console.WriteLine(English.errorReadingSettings, key)
                Return defaultReturnValue
            End Try
        End Function


        Public Shared Sub SetValue(key As SettingsEnum, value As String)
            Try
                Dim configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                Dim settings = configFile.AppSettings.Settings
                If IsNothing(settings(key.ToString())) Then
                    settings.Add(key.ToString(), value)
                Else
                    settings(key.ToString()).Value = value
                End If

                If value.Length > 200 Then
                    value = English.msgValueTooLarge
                End If
                If key <> SettingsEnum.DefaultWatchArgs Then
                    Console.WriteLine(English.msgSettingUpdated, key.ToString(), value)
                End If
                configFile.Save(ConfigurationSaveMode.Modified)
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name)
            Catch e As ConfigurationErrorsException
                Console.WriteLine(English.errorWritingSettings)
            End Try
        End Sub

    End Class
End Namespace
