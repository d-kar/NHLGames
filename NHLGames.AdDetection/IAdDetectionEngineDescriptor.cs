using System.ComponentModel;
using System.Windows.Controls;

namespace NHLGames.AdDetection
{
    public interface IAdDetectionEngineDescriptor : INotifyPropertyChanged
    {
        AdDetectionEngineType Type { get; }

        string Description { get; }

        string Name { get; }

        UserControl SettingsControl { get; }
    }
}
