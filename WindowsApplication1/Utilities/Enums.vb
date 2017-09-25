Namespace Utilities

    Public Enum GameStateEnum
        Scheduled = 1
        Pregame = 2
        InProgress = 3
        Ending = 4
        Final = 5
    End Enum

    Public Enum GameTypeEnum
        Preseason = 1
        Season = 2
        Series = 3
    End Enum

    Public Enum StreamType
        Away
        Home
        National
        French
        MultiCam1
        MultiCam2
        RefCam
        EndzoneCam1
        EndzoneCam2
    End Enum

    Public Enum PlayerTypeEnum
        None = 0
        Vlc = 1
        Mpc = 2
        Mpv = 3
    End Enum

    Public Enum SettingsEnum
        Version = 1
        DefaultWatchArgs = 2
        VlcPath = 3
        MpcPath = 4
        MpvPath = 5
        StreamlinkPath = 6
        ServerList = 7
        ShowScores = 8
        SelectedServer = 9
        SelectedLanguage = 10
        ShowLiveScores = 11
        ShowSeriesRecord = 12
        LanguageList = 13
    End Enum

    Public Enum OutputType
        Normal = 0
        Status = 1
        [Error] = 2
        Cli = 3
    End Enum

End Namespace
