Imports System.IO

Namespace Utilities

    Public Class ImageFetcher

        Public Shared Function GetEmbeddedImage(fileName As String) As Image
            Dim myAssembly As Reflection.Assembly = Reflection.Assembly.GetExecutingAssembly()
            Dim myStream As Stream = myAssembly.GetManifestResourceStream("NHLGames." & fileName & ".gif")
            If myStream IsNot Nothing Then
                Return Image.FromStream(myStream)
            Else
                Return Nothing
            End If
        End Function

    End Class
End Namespace
