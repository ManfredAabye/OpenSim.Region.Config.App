using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RegionConfigApp.Services
{
    public class IniWriter
    {
        public void WriteRegionsToFile(string filename, List<RegionData> regions)
        {
            var sb = new StringBuilder();

            foreach (var region in regions)
            {
                sb.AppendLine($"[{region.RegionName}]");
                WriteRegionContent(sb, region);
                sb.AppendLine();
            }

            File.WriteAllText(filename, sb.ToString());
        }

        public void WriteRegionsToSeparateFiles(string directoryPath, List<RegionData> regions)
        {
            // Erstelle Verzeichnis falls nicht vorhanden
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            foreach (var region in regions)
            {
                var fileName = region.RegionName.ToLower().Replace(" ", "_") + ".ini";
                var filePath = Path.Combine(directoryPath, fileName);
                
                var sb = new StringBuilder();
                sb.AppendLine($"[{region.RegionName}]");
                WriteRegionContent(sb, region);
                
                File.WriteAllText(filePath, sb.ToString());
            }
        }

        private void WriteRegionContent(StringBuilder sb, RegionData region)
        {
            sb.AppendLine($"RegionUUID = {region.RegionUuid}");
            sb.AppendLine($"Location = {region.Location}");
            sb.AppendLine($"SizeX = {region.Size}");
            sb.AppendLine($"SizeY = {region.Size}");
            sb.AppendLine($"SizeZ = {region.Size}");
            sb.AppendLine($"InternalPort = {region.InternalPort}");
            sb.AppendLine($"InternalAddress = {region.InternalAddress}");
            sb.AppendLine($"AllowAlternatePorts = {region.AllowAlternatePorts}");
            sb.AppendLine($"ExternalHostName = {region.ExternalHost}");
            sb.AppendLine($"MaxPrims = {region.MaxPrims}");
            sb.AppendLine($"MaxAgents = {region.MaxAgents}");
            sb.AppendLine($"MaxPrimsPerUser = {region.MaxPrimsPerUser}");
            
            // Kommentierte Optionen (mit Semikolon)
            sb.AppendLine($";MaptileStaticUUID = {region.MaptileUuid}");
            sb.AppendLine($";NonPhysicalPrimMax = {region.NonPhysicalPrimMax}");
            sb.AppendLine($";PhysicalPrimMax = {region.PhysicalPrimMax}");
            sb.AppendLine($";ClampPrimSize = {region.ClampPrimSize}");
            sb.AppendLine($";ScopeID = {region.ScopeId}");
            sb.AppendLine($";RegionType = {region.RegionType}");
            sb.AppendLine($";RenderMinHeight = {region.RenderMinHeight}");
            sb.AppendLine($";RenderMaxHeight = {region.RenderMaxHeight}");
            sb.AppendLine($";MaptileStaticFile = {region.MaptileStaticFile}");
            sb.AppendLine($";MasterAvatarFirstName = {region.MasterAvatarFirstName}");
            sb.AppendLine($";MasterAvatarLastName = {region.MasterAvatarLastName}");
            sb.AppendLine($";MasterAvatarSandboxPassword = {region.MasterAvatarSandboxPassword}");
        }
    }

    public class RegionData
    {
        public string RegionName { get; set; } = string.Empty;
        public string RegionUuid { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Size { get; set; }
        public int InternalPort { get; set; }
        public string InternalAddress { get; set; } = string.Empty;
        public bool AllowAlternatePorts { get; set; }
        public string ExternalHost { get; set; } = string.Empty;
        public int MaxPrims { get; set; }
        public int MaxAgents { get; set; }
        public int MaxPrimsPerUser { get; set; }
        public string MaptileUuid { get; set; } = string.Empty;
        public int NonPhysicalPrimMax { get; set; }
        public int PhysicalPrimMax { get; set; }
        public bool ClampPrimSize { get; set; }
        public string ScopeId { get; set; } = string.Empty;
        public string RegionType { get; set; } = string.Empty;
        public int RenderMinHeight { get; set; }
        public int RenderMaxHeight { get; set; }
        public string MaptileStaticFile { get; set; } = string.Empty;
        public string MasterAvatarFirstName { get; set; } = string.Empty;
        public string MasterAvatarLastName { get; set; } = string.Empty;
        public string MasterAvatarSandboxPassword { get; set; } = string.Empty;
    }
}
