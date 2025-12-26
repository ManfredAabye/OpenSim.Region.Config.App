using System.Collections.Generic;

namespace RegionConfigApp.Models
{
    public class AppConfig
    {
        public List<string> NamePrefixes { get; set; } = new();
        public List<string> NameSuffixes { get; set; } = new();
        public DefaultSettings DefaultSettings { get; set; } = new();
        public List<string> SpiralTypes { get; set; } = new();
        public List<int> ValidSizes { get; set; } = new();
        public Dictionary<string, string> Tooltips { get; set; } = new();
    }

    public class DefaultSettings
    {
        public string Location { get; set; } = "1000,1000";
        public int Size { get; set; } = 256;
        public int InternalPort { get; set; } = 9050;
        public string ExternalHost { get; set; } = "SYSTEMIP";
        public int MaxPrims { get; set; } = 100000;
        public int MaxAgents { get; set; } = 99;
        public string InternalAddress { get; set; } = "0.0.0.0";
        public bool AllowAlternatePorts { get; set; } = false;
        public int NonPhysicalPrimMax { get; set; } = 256;
        public int PhysicalPrimMax { get; set; } = 64;
        public bool ClampPrimSize { get; set; } = false;
        public int MaxPrimsPerUser { get; set; } = -1;
        public string RegionType { get; set; } = "Main";
        public int RenderMinHeight { get; set; } = -1;
        public int RenderMaxHeight { get; set; } = 100;
        public string MaptileStaticFile { get; set; } = "SomeFile.png";
        public string MasterAvatarFirstName { get; set; } = "John";
        public string MasterAvatarLastName { get; set; } = "Doe";
        public string MasterAvatarSandboxPassword { get; set; } = "passwd";
    }
}
