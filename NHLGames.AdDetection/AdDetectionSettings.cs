using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace NHLGames.AdDetection
{
    [DataContract]
    public class AdDetectionSettings
    {
        private const string FileName = @"./AdDetectionSettings.xml";

        private static AdDetectionSettings _settings;

        private AdDetectionSettings()
        {
            IsEnabled = false;
            EnabledModules = new List<string>();
            EngineType = AdDetectionEngineType.PlayerSystemVolume;
        }

        private static AdDetectionSettings Default => new AdDetectionSettings();

        [DataMember]
        public bool IsEnabled { get; set; }

        [DataMember]
        public List<string> EnabledModules { get; set; }

        [DataMember]
        public AdDetectionEngineType EngineType { get; set; }


        public static AdDetectionSettings Load()
        {
            if (_settings != null)
            {
                return _settings;
            }

            try
            {
                if (File.Exists(FileName))
                {
                    var s = new DataContractSerializer(typeof (AdDetectionSettings));
                    using (var fs = File.Open(FileName, FileMode.Open))
                    {
                        using (var reader = XmlReader.Create(fs))
                        {
                            _settings = (AdDetectionSettings) s.ReadObject(reader);
                            return _settings;
                        }
                    }
                }
            }
            catch
            {
                Console.WriteLine($@"Unable to load {FileName}. Using default config.");
            }

            Save(Default);
            return _settings;
        }

        public static void Save(AdDetectionSettings settings)
        {
            try
            {
                var s = new DataContractSerializer(typeof (AdDetectionSettings));
                using (var fs = File.Open(FileName, FileMode.Create))
                {
                    s.WriteObject(fs, settings);
                    _settings = settings;
                }
            }
            catch
            {
                Console.WriteLine($@"Unable to save {FileName}");
            }
        }
    }
}