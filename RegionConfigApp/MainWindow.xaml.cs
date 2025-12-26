using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.Win32;
using RegionConfigApp.Models;
using RegionConfigApp.Services;

namespace RegionConfigApp
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly ConfigService _configService;
        private readonly SpiralGenerator _spiralGenerator;
        private readonly IniWriter _iniWriter;
        private readonly List<RegionData> _regions = new();
        private int _regionCount = 0;
        private RegionConfig _currentRegion = null!;

        // Tooltip Properties
        private string _regionNameTooltip = string.Empty;
        private string _regionUuidTooltip = string.Empty;
        private string _locationTooltip = string.Empty;
        private string _sizeTooltip = string.Empty;
        private string _internalPortTooltip = string.Empty;
        private string _externalHostTooltip = string.Empty;
        private string _maxPrimsTooltip = string.Empty;
        private string _maxAgentsTooltip = string.Empty;
        private string _maptileUuidTooltip = string.Empty;
        private string _internalAddressTooltip = string.Empty;
        private string _allowAlternatePortsTooltip = string.Empty;
        private string _nonPhysicalPrimMaxTooltip = string.Empty;
        private string _physicalPrimMaxTooltip = string.Empty;
        private string _clampPrimSizeTooltip = string.Empty;
        private string _maxPrimsPerUserTooltip = string.Empty;
        private string _scopeIdTooltip = string.Empty;
        private string _regionTypeTooltip = string.Empty;
        private string _renderMinHeightTooltip = string.Empty;
        private string _renderMaxHeightTooltip = string.Empty;
        private string _maptileStaticFileTooltip = string.Empty;
        private string _masterAvatarFirstNameTooltip = string.Empty;
        private string _masterAvatarLastNameTooltip = string.Empty;
        private string _masterAvatarSandboxPasswordTooltip = string.Empty;
        private string _spiralTypeTooltip = string.Empty;

        public string RegionNameTooltip { get => _regionNameTooltip; set { _regionNameTooltip = value; OnPropertyChanged(); } }
        public string RegionUuidTooltip { get => _regionUuidTooltip; set { _regionUuidTooltip = value; OnPropertyChanged(); } }
        public string LocationTooltip { get => _locationTooltip; set { _locationTooltip = value; OnPropertyChanged(); } }
        public string SizeTooltip { get => _sizeTooltip; set { _sizeTooltip = value; OnPropertyChanged(); } }
        public string InternalPortTooltip { get => _internalPortTooltip; set { _internalPortTooltip = value; OnPropertyChanged(); } }
        public string ExternalHostTooltip { get => _externalHostTooltip; set { _externalHostTooltip = value; OnPropertyChanged(); } }
        public string MaxPrimsTooltip { get => _maxPrimsTooltip; set { _maxPrimsTooltip = value; OnPropertyChanged(); } }
        public string MaxAgentsTooltip { get => _maxAgentsTooltip; set { _maxAgentsTooltip = value; OnPropertyChanged(); } }
        public string MaptileUuidTooltip { get => _maptileUuidTooltip; set { _maptileUuidTooltip = value; OnPropertyChanged(); } }
        public string InternalAddressTooltip { get => _internalAddressTooltip; set { _internalAddressTooltip = value; OnPropertyChanged(); } }
        public string AllowAlternatePortsTooltip { get => _allowAlternatePortsTooltip; set { _allowAlternatePortsTooltip = value; OnPropertyChanged(); } }
        public string NonPhysicalPrimMaxTooltip { get => _nonPhysicalPrimMaxTooltip; set { _nonPhysicalPrimMaxTooltip = value; OnPropertyChanged(); } }
        public string PhysicalPrimMaxTooltip { get => _physicalPrimMaxTooltip; set { _physicalPrimMaxTooltip = value; OnPropertyChanged(); } }
        public string ClampPrimSizeTooltip { get => _clampPrimSizeTooltip; set { _clampPrimSizeTooltip = value; OnPropertyChanged(); } }
        public string MaxPrimsPerUserTooltip { get => _maxPrimsPerUserTooltip; set { _maxPrimsPerUserTooltip = value; OnPropertyChanged(); } }
        public string ScopeIdTooltip { get => _scopeIdTooltip; set { _scopeIdTooltip = value; OnPropertyChanged(); } }
        public string RegionTypeTooltip { get => _regionTypeTooltip; set { _regionTypeTooltip = value; OnPropertyChanged(); } }
        public string RenderMinHeightTooltip { get => _renderMinHeightTooltip; set { _renderMinHeightTooltip = value; OnPropertyChanged(); } }
        public string RenderMaxHeightTooltip { get => _renderMaxHeightTooltip; set { _renderMaxHeightTooltip = value; OnPropertyChanged(); } }
        public string MaptileStaticFileTooltip { get => _maptileStaticFileTooltip; set { _maptileStaticFileTooltip = value; OnPropertyChanged(); } }
        public string MasterAvatarFirstNameTooltip { get => _masterAvatarFirstNameTooltip; set { _masterAvatarFirstNameTooltip = value; OnPropertyChanged(); } }
        public string MasterAvatarLastNameTooltip { get => _masterAvatarLastNameTooltip; set { _masterAvatarLastNameTooltip = value; OnPropertyChanged(); } }
        public string MasterAvatarSandboxPasswordTooltip { get => _masterAvatarSandboxPasswordTooltip; set { _masterAvatarSandboxPasswordTooltip = value; OnPropertyChanged(); } }
        public string SpiralTypeTooltip { get => _spiralTypeTooltip; set { _spiralTypeTooltip = value; OnPropertyChanged(); } }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            _configService = new ConfigService();
            _spiralGenerator = new SpiralGenerator();
            _iniWriter = new IniWriter();

            var config = _configService.LoadConfig();
            LoadTooltips();
            InitializeSpiralTypes();
            InitializeWithDefaults();
        }

        private void LoadTooltips()
        {
            RegionNameTooltip = _configService.GetTooltip("RegionName");
            RegionUuidTooltip = _configService.GetTooltip("RegionUUID");
            LocationTooltip = _configService.GetTooltip("Location");
            SizeTooltip = _configService.GetTooltip("Size");
            InternalPortTooltip = _configService.GetTooltip("InternalPort");
            ExternalHostTooltip = _configService.GetTooltip("ExternalHost");
            MaxPrimsTooltip = _configService.GetTooltip("MaxPrims");
            MaxAgentsTooltip = _configService.GetTooltip("MaxAgents");
            MaptileUuidTooltip = _configService.GetTooltip("MaptileUUID");
            InternalAddressTooltip = _configService.GetTooltip("InternalAddress");
            AllowAlternatePortsTooltip = _configService.GetTooltip("AllowAlternatePorts");
            NonPhysicalPrimMaxTooltip = _configService.GetTooltip("NonPhysicalPrimMax");
            PhysicalPrimMaxTooltip = _configService.GetTooltip("PhysicalPrimMax");
            ClampPrimSizeTooltip = _configService.GetTooltip("ClampPrimSize");
            MaxPrimsPerUserTooltip = _configService.GetTooltip("MaxPrimsPerUser");
            ScopeIdTooltip = _configService.GetTooltip("ScopeID");
            RegionTypeTooltip = _configService.GetTooltip("RegionType");
            RenderMinHeightTooltip = _configService.GetTooltip("RenderMinHeight");
            RenderMaxHeightTooltip = _configService.GetTooltip("RenderMaxHeight");
            MaptileStaticFileTooltip = _configService.GetTooltip("MaptileStaticFile");
            MasterAvatarFirstNameTooltip = _configService.GetTooltip("MasterAvatarFirstName");
            MasterAvatarLastNameTooltip = _configService.GetTooltip("MasterAvatarLastName");
            MasterAvatarSandboxPasswordTooltip = _configService.GetTooltip("MasterAvatarSandboxPassword");
            SpiralTypeTooltip = _configService.GetTooltip("SpiralType");
        }

        private void InitializeSpiralTypes()
        {
            var spiralTypes = _configService.GetSpiralTypes();
            foreach (var type in spiralTypes)
            {
                cmbSpiralType.Items.Add(type);
            }
            cmbSpiralType.SelectedIndex = spiralTypes.IndexOf("flower");
        }

        private void InitializeWithDefaults()
        {
            _currentRegion = new RegionConfig();
            var defaults = _configService.GetDefaultSettings();

            _currentRegion.RegionName = _configService.GenerateRandomName();
            _currentRegion.RegionUuid = Guid.NewGuid().ToString();
            _currentRegion.Location = defaults.Location;
            _currentRegion.Size = defaults.Size;
            _currentRegion.InternalPort = defaults.InternalPort;
            _currentRegion.ExternalHost = defaults.ExternalHost;
            _currentRegion.MaxPrims = defaults.MaxPrims;
            _currentRegion.MaxAgents = defaults.MaxAgents;
            _currentRegion.InternalAddress = defaults.InternalAddress;
            _currentRegion.AllowAlternatePorts = defaults.AllowAlternatePorts;
            _currentRegion.NonPhysicalPrimMax = defaults.NonPhysicalPrimMax;
            _currentRegion.PhysicalPrimMax = defaults.PhysicalPrimMax;
            _currentRegion.ClampPrimSize = defaults.ClampPrimSize;
            _currentRegion.MaxPrimsPerUser = defaults.MaxPrimsPerUser;
            _currentRegion.RegionType = defaults.RegionType;
            _currentRegion.RenderMinHeight = defaults.RenderMinHeight;
            _currentRegion.RenderMaxHeight = defaults.RenderMaxHeight;
            _currentRegion.MaptileStaticFile = defaults.MaptileStaticFile;
            _currentRegion.MasterAvatarFirstName = defaults.MasterAvatarFirstName;
            _currentRegion.MasterAvatarLastName = defaults.MasterAvatarLastName;
            _currentRegion.MasterAvatarSandboxPassword = defaults.MasterAvatarSandboxPassword;

            UpdateUIFromModel();
        }

        private void UpdateUIFromModel()
        {
            txtRegionName.Text = _currentRegion.RegionName;
            txtRegionUuid.Text = _currentRegion.RegionUuid;
            txtLocation.Text = _currentRegion.Location;
            txtSize.Text = _currentRegion.Size.ToString();
            txtInternalPort.Text = _currentRegion.InternalPort.ToString();
            txtExternalHost.Text = _currentRegion.ExternalHost;
            txtMaxPrims.Text = _currentRegion.MaxPrims.ToString();
            txtMaxAgents.Text = _currentRegion.MaxAgents.ToString();
            txtMaptileUuid.Text = _currentRegion.MaptileUuid;
            txtInternalAddress.Text = _currentRegion.InternalAddress;
            chkAllowAlternatePorts.IsChecked = _currentRegion.AllowAlternatePorts;
            txtNonPhysicalPrimMax.Text = _currentRegion.NonPhysicalPrimMax.ToString();
            txtPhysicalPrimMax.Text = _currentRegion.PhysicalPrimMax.ToString();
            chkClampPrimSize.IsChecked = _currentRegion.ClampPrimSize;
            txtMaxPrimsPerUser.Text = _currentRegion.MaxPrimsPerUser.ToString();
            txtScopeId.Text = _currentRegion.ScopeId;
            txtRegionType.Text = _currentRegion.RegionType;
            txtRenderMinHeight.Text = _currentRegion.RenderMinHeight.ToString();
            txtRenderMaxHeight.Text = _currentRegion.RenderMaxHeight.ToString();
            txtMaptileStaticFile.Text = _currentRegion.MaptileStaticFile;
            txtMasterAvatarFirstName.Text = _currentRegion.MasterAvatarFirstName;
            txtMasterAvatarLastName.Text = _currentRegion.MasterAvatarLastName;
            txtMasterAvatarSandboxPassword.Text = _currentRegion.MasterAvatarSandboxPassword;
        }

        private void UpdateModelFromUI()
        {
            _currentRegion.RegionName = txtRegionName.Text;
            _currentRegion.RegionUuid = txtRegionUuid.Text;
            _currentRegion.Location = txtLocation.Text;
            
            if (int.TryParse(txtSize.Text, out int size))
                _currentRegion.Size = size;
            
            if (int.TryParse(txtInternalPort.Text, out int port))
                _currentRegion.InternalPort = port;
            
            _currentRegion.ExternalHost = txtExternalHost.Text;
            
            if (int.TryParse(txtMaxPrims.Text, out int maxPrims))
                _currentRegion.MaxPrims = maxPrims;
            
            if (int.TryParse(txtMaxAgents.Text, out int maxAgents))
                _currentRegion.MaxAgents = maxAgents;
            
            _currentRegion.MaptileUuid = txtMaptileUuid.Text;
            _currentRegion.InternalAddress = txtInternalAddress.Text;
            _currentRegion.AllowAlternatePorts = chkAllowAlternatePorts.IsChecked ?? false;
            
            if (int.TryParse(txtNonPhysicalPrimMax.Text, out int nonPhysical))
                _currentRegion.NonPhysicalPrimMax = nonPhysical;
            
            if (int.TryParse(txtPhysicalPrimMax.Text, out int physical))
                _currentRegion.PhysicalPrimMax = physical;
            
            _currentRegion.ClampPrimSize = chkClampPrimSize.IsChecked ?? false;
            
            if (int.TryParse(txtMaxPrimsPerUser.Text, out int maxPrimsPerUser))
                _currentRegion.MaxPrimsPerUser = maxPrimsPerUser;
            
            _currentRegion.ScopeId = txtScopeId.Text;
            _currentRegion.RegionType = txtRegionType.Text;
            
            if (int.TryParse(txtRenderMinHeight.Text, out int renderMin))
                _currentRegion.RenderMinHeight = renderMin;
            
            if (int.TryParse(txtRenderMaxHeight.Text, out int renderMax))
                _currentRegion.RenderMaxHeight = renderMax;
            
            _currentRegion.MaptileStaticFile = txtMaptileStaticFile.Text;
            _currentRegion.MasterAvatarFirstName = txtMasterAvatarFirstName.Text;
            _currentRegion.MasterAvatarLastName = txtMasterAvatarLastName.Text;
            _currentRegion.MasterAvatarSandboxPassword = txtMasterAvatarSandboxPassword.Text;
        }

        private void BtnGenerateRegions_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateModelFromUI();

                // Parse die Anzahl der zu generierenden Regionen
                if (!int.TryParse(txtRegionCount.Text, out int count) || count < 1 || count > 999)
                {
                    MessageBox.Show("Bitte geben Sie eine gültige Anzahl zwischen 1 und 999 ein.", 
                        "Ungültige Eingabe", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Bestätigung bei großen Mengen
                if (count > 50)
                {
                    var result = MessageBox.Show(
                        $"Sie sind dabei, {count} Regionen zu generieren. Dies kann einige Sekunden dauern. Fortfahren?",
                        "Bestätigung", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    
                    if (result != MessageBoxResult.Yes)
                        return;
                }

                // Validiere und korrigiere die Größe
                var validSize = _configService.GetNearestValidSize(_currentRegion.Size);
                _currentRegion.Size = validSize;

                // Setze die Regionsgröße im SpiralGenerator
                _spiralGenerator.SetRegionSize(validSize);

                var selectedSpiral = cmbSpiralType.SelectedItem?.ToString() ?? "flower";
                var startPort = _currentRegion.InternalPort;
                var generatedRegions = new List<string>();

                // Generiere die Regionen
                for (int i = 0; i < count; i++)
                {
                    // Berechne die nächste Position
                    var (x, y) = _spiralGenerator.GetNextLocation(selectedSpiral);
                    
                    // Stelle sicher, dass Koordinaten nicht negativ sind
                    x = Math.Max(0, x);
                    y = Math.Max(0, y);

                    _currentRegion.Location = $"{x},{y}";
                    _currentRegion.InternalPort = startPort + i;

                    // Speichere die Region
                    var regionData = new RegionData
                    {
                        RegionName = _currentRegion.RegionName,
                        RegionUuid = _currentRegion.RegionUuid,
                        Location = _currentRegion.Location,
                        Size = _currentRegion.Size,
                        InternalPort = _currentRegion.InternalPort,
                        InternalAddress = _currentRegion.InternalAddress,
                        AllowAlternatePorts = _currentRegion.AllowAlternatePorts,
                        ExternalHost = _currentRegion.ExternalHost,
                        MaxPrims = _currentRegion.MaxPrims,
                        MaxAgents = _currentRegion.MaxAgents,
                        MaxPrimsPerUser = _currentRegion.MaxPrimsPerUser,
                        MaptileUuid = _currentRegion.MaptileUuid,
                        NonPhysicalPrimMax = _currentRegion.NonPhysicalPrimMax,
                        PhysicalPrimMax = _currentRegion.PhysicalPrimMax,
                        ClampPrimSize = _currentRegion.ClampPrimSize,
                        ScopeId = _currentRegion.ScopeId,
                        RegionType = _currentRegion.RegionType,
                        RenderMinHeight = _currentRegion.RenderMinHeight,
                        RenderMaxHeight = _currentRegion.RenderMaxHeight,
                        MaptileStaticFile = _currentRegion.MaptileStaticFile,
                        MasterAvatarFirstName = _currentRegion.MasterAvatarFirstName,
                        MasterAvatarLastName = _currentRegion.MasterAvatarLastName,
                        MasterAvatarSandboxPassword = _currentRegion.MasterAvatarSandboxPassword
                    };

                    _regions.Add(regionData);
                    _regionCount++;
                    generatedRegions.Add($"{regionData.RegionName} ({x},{y})");

                    // Generiere neue Werte für die nächste Region
                    _currentRegion.RegionName = _configService.GenerateRandomName();
                    _currentRegion.RegionUuid = Guid.NewGuid().ToString();
                }

                // Aktualisiere die UI mit den letzten Werten
                UpdateUIFromModel();
                txtRegionCountDisplay.Text = $"📊 Regions: {_regionCount}";

                MessageBox.Show($"✨ {count} Region(en) wurden erfolgreich generiert!\n\n" +
                    $"📊 Gesamt-Regionen: {_regionCount}\n" +
                    $"🌀 Spiral-Typ: {selectedSpiral}\n" +
                    $"🔌 Port-Bereich: {startPort}-{startPort + count - 1}\n" +
                    $"📝 Verwendete Namen: {_configService.GetUsedNamesCount()} eindeutige Namen", 
                    "Regionen generiert", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fehler beim Generieren der Regionen: {ex.Message}", 
                    "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnSaveConfig_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_regions.Count == 0)
                {
                    MessageBox.Show("Keine Regionen zum Speichern vorhanden. Bitte fügen Sie zuerst Regionen hinzu.", 
                        "Warnung", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                bool separateFiles = chkSeparateFiles.IsChecked ?? false;

                if (separateFiles)
                {
                    // Ordner-Dialog für separate Dateien
                    var folderDialog = new System.Windows.Forms.FolderBrowserDialog
                    {
                        Description = "Wählen Sie einen Ordner für die Region-Dateien",
                        ShowNewFolderButton = true
                    };

                    if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        _iniWriter.WriteRegionsToSeparateFiles(folderDialog.SelectedPath, _regions);
                        
                        MessageBox.Show(
                            $"✅ {_regions.Count} Region-Dateien wurden erfolgreich gespeichert!\n\n" +
                            $"📁 Ordner: {folderDialog.SelectedPath}\n" +
                            $"📄 Dateien: [regionname].ini (kleingeschrieben)",
                            "Erfolgreich gespeichert", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    // Einzelne Datei (wie vorher)
                    var saveFileDialog = new SaveFileDialog
                    {
                        Filter = "INI files (*.ini)|*.ini|All files (*.*)|*.*",
                        DefaultExt = ".ini",
                        FileName = "Regions.ini"
                    };

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        _iniWriter.WriteRegionsToFile(saveFileDialog.FileName, _regions);
                        MessageBox.Show(
                            $"✅ Konfiguration wurde erfolgreich gespeichert!\n\n" +
                            $"📁 Datei: {saveFileDialog.FileName}\n" +
                            $"📊 Regionen: {_regions.Count}",
                            "Erfolgreich gespeichert", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Fehler beim Speichern der Konfiguration:\n\n{ex.Message}", 
                    "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
