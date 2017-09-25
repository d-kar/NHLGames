Imports System.IO
Imports NHLGames.My.Resources

Namespace Utilities

    Public Class FileAccess

        ''' <summary>
        ''' DirectoryInfo.GetFiles can cause AuthorizationException Errors, hence this function
        ''' </summary>
        ''' <param name="root">Root folder to search (eg. "C:\Program Files")</param>
        ''' <param name="searchPattern">Pattern to search for (eg. "vlc.exe" or "*.exe")</param>
        ''' <returns>List of paths</returns>
        Public Shared Function GetFiles(root As String, searchPattern As String) As List(Of String)
            Dim result As New List(Of String)()

            Dim pending As New Stack(Of String)()
            pending.Push(root)

            While pending.Count <> 0
                Dim path = pending.Pop()
                Dim nextDir As String() = Nothing
                Try
                    nextDir = Directory.GetFiles(path, searchPattern)
                Catch
                End Try
                If nextDir IsNot Nothing AndAlso nextDir.Length <> 0 Then
                    result.AddRange(nextDir)
                End If
                Try
                    nextDir = Directory.GetDirectories(path)
                    For Each subdir As String In nextDir
                        pending.Push(subdir)
                    Next
                Catch
                End Try
            End While

            Return result
        End Function


        Public Shared Function IsFileReadonly(path As String) As Boolean

            Dim attributes As FileAttributes = File.GetAttributes(path)
            Return (attributes And FileAttributes.[ReadOnly]) = FileAttributes.[ReadOnly]

        End Function


        Public Shared Sub RemoveReadOnly(path As String)

            Dim attributes As FileAttributes = File.GetAttributes(path)

            If (attributes And FileAttributes.[ReadOnly]) = FileAttributes.[ReadOnly] Then
                ' Make the file RW
                attributes = RemoveAttribute(attributes, FileAttributes.[ReadOnly])
                File.SetAttributes(path, attributes)
                Console.WriteLine(English.msgRemoveReadOnly, path)
            End If

        End Sub

        Public Shared Sub AddReadonly(path As String)
            File.SetAttributes(path, File.GetAttributes(path) Or FileAttributes.ReadOnly)
            Console.WriteLine(English.msgAddReadOnly, path)
        End Sub

        Public Shared Function RemoveAttribute(attributes As FileAttributes, attributesToRemove As FileAttributes) As FileAttributes
            Return attributes And Not attributesToRemove
        End Function

        Public Shared Function HasAccess(filePath As String, Optional createIt As Boolean = true, Optional reportException As Boolean = false)
            Try
                If createIt Then File.WriteAllText(filePath, English.msgTestTxtContent)

                Using inputstreamreader As New StreamReader(filePath)
                    inputstreamreader.Close()
                End Using
                Using inputStream As FileStream = File.Open(filePath, FileMode.Open, IO.FileAccess.ReadWrite, FileShare.None)
                    inputStream.Close()
                End Using

                If createIt Then File.Delete(filePath)
                Return True
            Catch ex As Exception
                If reportException Then 
                    Console.WriteLine(String.Format(English.errorGeneral, ex.Message))
                End If
                Return False
            End Try
        End Function

    End Class
End Namespace
