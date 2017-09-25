Imports System.IO
Imports System.Net
Imports System.Security.Principal
Imports NHLGames.My.Resources

Namespace Utilities

    Public Class HostsFile

        Public Shared ReadOnly Property HostsFilePath As String = String.Format("{0}\drivers\etc\hosts", Environment.SystemDirectory)

        Public Shared Function TestEntry(domain As String, ip As String) As Boolean
            Dim resolvedIp As String = ""
            Try
                resolvedIp = Dns.GetHostAddresses(domain)(0).ToString()
            Catch ex As Exception
            End Try
            Return ip = resolvedIp
        End Function

        Private Shared Function RemoveOldEntries(host As String, contents As String) As String
            Dim newContents As String = String.Empty

            Console.WriteLine(English.msgCleanHostsFile)

            Dim hostsFile = contents.Replace(vbCr, String.Empty).Split(vbLf)

            For lineCount As Integer = 0 To hostsFile.Length - 1
                If hostsFile(lineCount).Contains(host) = False Then
                    newContents &= hostsFile(lineCount).Replace(vbLf, String.Empty)
                    If lineCount < hostsFile.Length - 1 Then
                        newContents &= vbCrLf
                    End If
                End If
            Next

            newContents = newContents.TrimEnd()

            Return newContents
        End Function

        Public Shared Sub CleanHosts(host As String)

            If FileAccess.HasAccess(HostsFilePath, false, true) Then
                Dim fileIsReadonly As Boolean = FileAccess.IsFileReadonly(HostsFilePath)

                If fileIsReadonly Then
                    FileAccess.RemoveReadOnly(HostsFilePath)
                End If

                Console.WriteLine(English.msgBackingHostsFile, HostsFilePath)
                File.Copy(HostsFilePath, HostsFilePath & ".bak", True)

                Dim input As String
                Using sr As New StreamReader(HostsFilePath)
                    input = sr.ReadToEnd()
                End Using

                Dim output As String = RemoveOldEntries(host, input)

                Using sw As New StreamWriter(HostsFilePath)
                    sw.Write(output)
                    sw.Close()
                End Using

                If fileIsReadonly Then
                    FileAccess.AddReadonly(HostsFilePath)
                End If

                MessageOpenHostsFile()
            End If

        End Sub

        Private Shared Sub MessageOpenHostsFile()
            If InvokeElement.MsgBoxBlue(
                NHLGamesMetro.RmText.GetString("msgViewHostsText"),
                NHLGamesMetro.RmText.GetString("msgViewHosts"),
                MessageBoxButtons.YesNo) = DialogResult.Yes AndAlso FileAccess.HasAccess(HostsFilePath, false, true) Then
                Process.Start("NOTEPAD", HostsFilePath)
            End If
        End Sub

        Public Shared Sub AddEntry(ip As String, host As String)

            If EnsureAdmin() Then
                Dim fileIsReadonly As Boolean = FileAccess.IsFileReadonly(HostsFilePath)

                If fileIsReadonly Then
                    FileAccess.RemoveReadOnly(HostsFilePath)
                End If

                Console.WriteLine(English.msgBackingHostsFile, HostsFilePath)
                File.Copy(HostsFilePath, HostsFilePath & ".bak", True)

                Dim input As String
                Using sr As New StreamReader(HostsFilePath)
                    input = sr.ReadToEnd()
                End Using

                Dim output As String = RemoveOldEntries(host, input)

                output = output & vbNewLine & ip & vbTab & host

                Using sw As New StreamWriter(HostsFilePath)
                    sw.Write(output)
                    sw.Close()
                End Using

                If fileIsReadonly Then
                    FileAccess.AddReadonly(HostsFilePath)
                End If

                MessageOpenHostsFile()
            End If

        End Sub

        Public Shared Function EnsureAdmin() As Boolean

            If FileAccess.HasAccess(HostsFilePath, false, true) Then
                If IsAdministrator() Then
                    If InvokeElement.MsgBoxBlue(
                        NHLGamesMetro.RmText.GetString("msgRunAsAdminText"), 
                        NHLGamesMetro.RmText.GetString("msgRunAsAdmin"),
                        MessageBoxButtons.YesNo) = DialogResult.Yes Then

                        ' Restart program And run as admin
                        Dim exeName = Process.GetCurrentProcess().MainModule.FileName
                        Dim startInfo As ProcessStartInfo = New ProcessStartInfo(exeName)
                        startInfo.Verb = "runas"
                        startInfo.UseShellExecute = True
                        Try
                            Process.Start(startInfo)
                            Application.Exit()
                        Catch ex As Exception
                        End Try

                    End If
                    Return False
                End If
                Return True
            Else 
                Return False
            End If

        End Function

        Public Shared Function IsAdministrator() As Boolean
            Dim identity As WindowsIdentity = WindowsIdentity.GetCurrent()
            Dim principal As WindowsPrincipal = New WindowsPrincipal(identity)
            Return Not principal.IsInRole(WindowsBuiltInRole.Administrator)
        End Function

    End Class
End Namespace
