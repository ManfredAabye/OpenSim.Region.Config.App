using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RegionConfigApp.Models
{
    public class RegionConfig : INotifyPropertyChanged
    {
        private string _regionName = string.Empty;
        private string _regionUuid = string.Empty;
        private string _maptileUuid = string.Empty;
        private string _location = "1000,1000";
        private int _size = 256;
        private int _internalPort = 9050;
        private string _externalHost = "SYSTEMIP";
        private int _maxPrims = 100000;
        private int _maxAgents = 99;
        private string _internalAddress = "0.0.0.0";
        private bool _allowAlternatePorts = false;
        private int _nonPhysicalPrimMax = 256;
        private int _physicalPrimMax = 64;
        private bool _clampPrimSize = false;
        private int _maxPrimsPerUser = -1;
        private string _scopeId = string.Empty;
        private string _regionType = "Main";
        private int _renderMinHeight = -1;
        private int _renderMaxHeight = 100;
        private string _maptileStaticFile = "SomeFile.png";
        private string _masterAvatarFirstName = "John";
        private string _masterAvatarLastName = "Doe";
        private string _masterAvatarSandboxPassword = "passwd";

        public string RegionName
        {
            get => _regionName;
            set { _regionName = value; OnPropertyChanged(); }
        }

        public string RegionUuid
        {
            get => _regionUuid;
            set
            {
                _regionUuid = value;
                OnPropertyChanged();
                // Automatisch MaptileUuid aktualisieren
                MaptileUuid = value;
                ScopeId = value;
            }
        }

        public string MaptileUuid
        {
            get => _maptileUuid;
            set { _maptileUuid = value; OnPropertyChanged(); }
        }

        public string Location
        {
            get => _location;
            set { _location = value; OnPropertyChanged(); }
        }

        public int Size
        {
            get => _size;
            set { _size = value; OnPropertyChanged(); }
        }

        public int InternalPort
        {
            get => _internalPort;
            set { _internalPort = value; OnPropertyChanged(); }
        }

        public string ExternalHost
        {
            get => _externalHost;
            set { _externalHost = value; OnPropertyChanged(); }
        }

        public int MaxPrims
        {
            get => _maxPrims;
            set { _maxPrims = value; OnPropertyChanged(); }
        }

        public int MaxAgents
        {
            get => _maxAgents;
            set { _maxAgents = value; OnPropertyChanged(); }
        }

        public string InternalAddress
        {
            get => _internalAddress;
            set { _internalAddress = value; OnPropertyChanged(); }
        }

        public bool AllowAlternatePorts
        {
            get => _allowAlternatePorts;
            set { _allowAlternatePorts = value; OnPropertyChanged(); }
        }

        public int NonPhysicalPrimMax
        {
            get => _nonPhysicalPrimMax;
            set { _nonPhysicalPrimMax = value; OnPropertyChanged(); }
        }

        public int PhysicalPrimMax
        {
            get => _physicalPrimMax;
            set { _physicalPrimMax = value; OnPropertyChanged(); }
        }

        public bool ClampPrimSize
        {
            get => _clampPrimSize;
            set { _clampPrimSize = value; OnPropertyChanged(); }
        }

        public int MaxPrimsPerUser
        {
            get => _maxPrimsPerUser;
            set { _maxPrimsPerUser = value; OnPropertyChanged(); }
        }

        public string ScopeId
        {
            get => _scopeId;
            set { _scopeId = value; OnPropertyChanged(); }
        }

        public string RegionType
        {
            get => _regionType;
            set { _regionType = value; OnPropertyChanged(); }
        }

        public int RenderMinHeight
        {
            get => _renderMinHeight;
            set { _renderMinHeight = value; OnPropertyChanged(); }
        }

        public int RenderMaxHeight
        {
            get => _renderMaxHeight;
            set { _renderMaxHeight = value; OnPropertyChanged(); }
        }

        public string MaptileStaticFile
        {
            get => _maptileStaticFile;
            set { _maptileStaticFile = value; OnPropertyChanged(); }
        }

        public string MasterAvatarFirstName
        {
            get => _masterAvatarFirstName;
            set { _masterAvatarFirstName = value; OnPropertyChanged(); }
        }

        public string MasterAvatarLastName
        {
            get => _masterAvatarLastName;
            set { _masterAvatarLastName = value; OnPropertyChanged(); }
        }

        public string MasterAvatarSandboxPassword
        {
            get => _masterAvatarSandboxPassword;
            set { _masterAvatarSandboxPassword = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
