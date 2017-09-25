Imports System.IO
Imports NHLGames.My.Resources
Imports NHLGames.Utilities

Namespace Objects

    Public Class GameWatchArguments

        Public Property Quality As String = ""
        Public Property Is60Fps As Boolean = True
        Public Property Cdn As String = ""
        Public Property Stream As GameStream = Nothing
        Public Property IsVod As Boolean = False
        Public Property GameTitle As String = ""
        Public Property PlayerPath As String = ""
        Public Property PlayerType As PlayerTypeEnum = PlayerTypeEnum.None
        Public Property StreamlinkPath As String = ""
        Public Property UsestreamlinkArgs As Boolean = False
        Public Property StreamlinkArgs As String = ""
        Public Property UsePlayerArgs As Boolean = False
        Public Property PlayerArgs As String = ""
        Public Property UseOutputArgs As Boolean = False
        Public Property PlayerOutputPath As String = ""

        Public Overrides Function ToString() As String
            Return OutputArgs(False)
        End Function

        Public Overloads Function ToString(ByVal safeOutput As Boolean)
            Return OutputArgs(safeOutput)
        End Function

        Private Function OutputArgs(ByVal safeOutput As Boolean)
            Dim returnValue As String = ""
            Const dblQuot As String = """"
            Const dblQuot2 As String = """"""
            Const space As String = " "
            Dim literalPlayerArgs As String = String.Empty

            If UsePlayerArgs Then
                literalPlayerArgs = PlayerArgs
            End If

            'DEBUG
            'Returnvalue &= "-v -l debug -h "

            Dim titleArg As String = literalPlayerArgs

            If PlayerType = PlayerTypeEnum.Vlc Then
                titleArg = String.Format("--meta-title{0}{1}{2}{1}{0}", space, dblQuot2, GameTitle)
            ElseIf PlayerType = PlayerTypeEnum.Mpv Then
                titleArg = String.Format("--title{0}{1}{2}{1}{0}--user-agent=User-Agent={1}Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, Like Gecko) Chrome/48.0.2564.82 Safari/537.36 Edge/14.14316{1}{0}", space, dblQuot2, GameTitle)
            End If

            If String.IsNullOrEmpty(PlayerPath) = False Then
                returnValue &= String.Format("--player{0}{1}{2}{0}{3}{4}{0}{1}{0}", space, dblQuot, PlayerPath, titleArg, literalPlayerArgs)
            Else
                Console.WriteLine(English.errorPlayerPathEmpty)
            End If

            If PlayerType = PlayerTypeEnum.Mpv Then
                returnValue &= String.Format("--player-passthrough=hls{0}", space)
            End If

            If safeOutput = False Then
                returnValue &= String.Format("--http-cookie={0}mediaAuth={1}{2}{0}{2}", dblQuot, Common.GetRandomString(240), space)
            End If

            returnValue &= String.Format("--http-header={0}User-Agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, Like Gecko) Chrome/48.0.2564.82 Safari/537.36 Edge/14.14316{0}{1}", dblQuot, space)

            If safeOutput = False Then
                returnValue &= String.Format("{0}hlsvariant://", dblQuot)

                If IsVod Then
                    returnValue &= Stream.VODURL
                Else
                    returnValue &= Stream.GameURL
                End If

                returnValue = returnValue.Replace("CDN", Cdn) & space
            Else
                returnValue &= String.Format("{0}hlsvariant://--URL CENSORED--{1}", dblQuot, space)
            End If

            If Is60Fps Then
                returnValue &= String.Format("name_key=bitrate{0}{1}", dblQuot, space)
            Else
                returnValue &= dblQuot & space
            End If

            If Is60Fps Then
                returnValue &= String.Format("best{0}", space)
            Else
                returnValue &= Quality & space
            End If

            returnValue &= String.Format("--http-no-ssl-verify{0}", space)

            If UseOutputArgs Then
                Dim outputPath As String = PlayerOutputPath.
                        Replace("(DATE)", DateHelper.GetPacificTime(Stream.Game.GameDate).ToString("yyyy-MM-dd")).
                        Replace("(HOME)", Stream.Game.HomeAbbrev).
                        Replace("(AWAY)", Stream.Game.AwayAbbrev).
                        Replace("(TYPE)", Stream.Type.ToString()).
                        Replace("(QUAL)", If(Is60Fps, "720p60", Quality))
                Dim suffix As Integer = 1
                Dim originalName = Path.GetFileNameWithoutExtension(outputPath)
                Dim originalExt = Path.GetExtension(outputPath)
                While (My.Computer.FileSystem.FileExists(outputPath))
                    outputPath = Path.ChangeExtension(Path.Combine(Path.GetDirectoryName(outputPath), originalName & "_" & suffix), originalExt)
                    suffix += 1
                End While

                returnValue &= "-f -o" & space & dblQuot & outputPath & dblQuot & space
            End If

            If UsestreamlinkArgs Then
                returnValue &= StreamlinkArgs
            End If

            Return returnValue
        End Function

End Class
End Namespace
