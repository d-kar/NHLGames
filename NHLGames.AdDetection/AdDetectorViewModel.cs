using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using NHLGames.AdDetection.AdDetectors;
using NHLGames.AdDetection.Common;
using Prism.Commands;
using Prism.Mvvm;

namespace NHLGames.AdDetection
{
    public class AdDetectorViewModel : BindableBase
    {
        private bool _adDetectingEnabled;

        private IAdDetectionEngineDescriptor _selectedAdDetectionEngineDescriptor;

        private ObservableCollection<IAdDetectionEngineDescriptor> _adDetectionEngines;

        private ObservableCollection<IAdModule> _disabledModules;

        private DelegateCommand<IAdModule> _disableModuleCommand;

        private ObservableCollection<IAdModule> _enabledModules;

        private DelegateCommand<IAdModule> _enableModuleCommand;

        private AdDetectionEngineType _engineType;

        private Dictionary<string, IAdModule> _modules;


        private IAdModule _selectedModule;

        private AdDetectionSettings _settings;

        public UserControl SettingsControl;

        public AdDetectorViewModel()
        {
            AdDetectionEngineBase currentEngine;
            EnableModuleCommand.ObservesProperty(() => SelectedModule);
            DisableModuleCommand.ObservesProperty(() => SelectedModule);

            AdDetectionEngines = new ObservableCollection<IAdDetectionEngineDescriptor>
            {
                new VolumeAdDetectionDescriptor(),
                new ScreenAdDetectionDescriptor()
            };

            ImportModules();

            LoadConfig();

            RemoveUninstalledModulesFromConfig();


            SettingsControl = new AdDetectorUserControl(this);

            switch (_engineType)
            {
                case AdDetectionEngineType.FullScreenImage:
                    currentEngine = new ScreenAdDetectionEngine();
                    currentEngine.Start(_modules.Where(x => _settings.EnabledModules.Contains(x.Key)).Select(x => x.Value).ToList());
                    break;
                case AdDetectionEngineType.PlayerSystemVolume:
                    currentEngine = new VolumeAdDetectionEngine();
                    currentEngine.Start(_modules.Where(x => _settings.EnabledModules.Contains(x.Key)).Select(x => x.Value).ToList());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public IAdDetectionEngineDescriptor SelectedAdDetectionEngineDescriptor
        {
            get { return _selectedAdDetectionEngineDescriptor; }
            set
            {
                if (SetProperty(ref _selectedAdDetectionEngineDescriptor, value))
                {
                    AdDetectionEngineChanged();
                }
            }
        }

        public ObservableCollection<IAdDetectionEngineDescriptor> AdDetectionEngines
        {
            get { return _adDetectionEngines; }
            set { SetProperty(ref _adDetectionEngines, value); }
        }

        public DelegateCommand<IAdModule> EnableModuleCommand
            =>
                _enableModuleCommand =
                    _enableModuleCommand ?? new DelegateCommand<IAdModule>(EnableModule, CanEnableModuleExecute);

        public DelegateCommand<IAdModule> DisableModuleCommand
            =>
                _disableModuleCommand =
                    _disableModuleCommand ?? new DelegateCommand<IAdModule>(DisableModule, CanDisableModuleExecute);


        public AdDetectionEngineType EngineType
        {
            get { return _engineType; }
            set { SetProperty(ref _engineType, value); }
        }

        public IAdModule SelectedModule
        {
            get { return _selectedModule; }
            set
            {
                SetProperty(ref _selectedModule, null);

                SetProperty(ref _selectedModule, value);
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<IAdModule> EnabledModules
        {
            get { return _enabledModules; }
            set { SetProperty(ref _enabledModules, value); }
        }

        public ObservableCollection<IAdModule> DisabledModules
        {
            get { return _disabledModules; }
            set { SetProperty(ref _disabledModules, value); }
        }

        public bool AdDetectingEnabled
        {
            get { return _adDetectingEnabled; }
            set
            {
                if (SetProperty(ref _adDetectingEnabled, value))
                {
                    EnableChanged();
                }
            }
        }

        private void AdDetectionEngineChanged()
        {
            if (SelectedAdDetectionEngineDescriptor != null &&
                SelectedAdDetectionEngineDescriptor.Type != _settings.EngineType)
            {
                _settings.EngineType = SelectedAdDetectionEngineDescriptor.Type;

                AdDetectionSettings.Save(_settings);
            }
        }

        public void LoadConfig()
        {
            _settings = AdDetectionSettings.Load();

            _adDetectingEnabled = _settings.IsEnabled;

            _enabledModules =
                new ObservableCollection<IAdModule>(
                    _modules.Where(x => _settings.EnabledModules.Contains(x.Key)).Select(x => x.Value));
            _disabledModules =
                new ObservableCollection<IAdModule>(
                    _modules.Where(x => !_settings.EnabledModules.Contains(x.Key)).Select(x => x.Value));

            _selectedAdDetectionEngineDescriptor = AdDetectionEngines.FirstOrDefault(x => x.Type == _settings.EngineType);
        }


        private bool CanDisableModuleExecute(IAdModule x)
        {
            return x != null && _enabledModules.Contains(x);
        }

        private bool CanEnableModuleExecute(IAdModule x)
        {
            return x != null && _disabledModules.Contains(x);
        }

        private void EnableModule(IAdModule module)
        {
            DisabledModules.Remove(module);
            EnabledModules.Add(module);

            _settings.EnabledModules = EnabledModules.Select(x => x.Title).ToList();
            AdDetectionSettings.Save(_settings);
        }

        private void DisableModule(IAdModule module)
        {
            EnabledModules.Remove(module);
            DisabledModules.Add(module);

            _settings.EnabledModules = EnabledModules.Select(x => x.Title).ToList();
            AdDetectionSettings.Save(_settings);
        }

        private void RemoveUninstalledModulesFromConfig()
        {
            var removedModules =
                _settings.EnabledModules.Where(enabledModule => !_modules.ContainsKey(enabledModule)).ToList();

            _settings.EnabledModules.RemoveAll(x => removedModules.Contains(x));
            AdDetectionSettings.Save(_settings);
        }

        private void ImportModules()
        {
            var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            var modulesDirectory = Directory.GetDirectories(assemblyDirectory + @"\AdModules");


            var catalog = new AggregateCatalog();
            foreach (var module in modulesDirectory)
            {
                catalog.Catalogs.Add(
                    new DirectoryCatalog(module));
            }

            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
            _modules = container.GetExportedValues<IAdModule>().ToDictionary(k => k.Title, v => v);
           
        }

        private void EnableChanged()
        {
            if (_settings.IsEnabled == AdDetectingEnabled) return;

            _settings.IsEnabled = AdDetectingEnabled;

            AdDetectionSettings.Save(_settings);
        }
    }
}