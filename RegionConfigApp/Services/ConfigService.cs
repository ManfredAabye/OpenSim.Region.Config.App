using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using RegionConfigApp.Models;

namespace RegionConfigApp.Services
{
    public class ConfigService
    {
        private readonly Random _random = new();
        private AppConfig? _config;
        private readonly HashSet<string> _usedNames = new();

        public AppConfig LoadConfig()
        {
            try
            {
                var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
                if (File.Exists(configPath))
                {
                    var json = File.ReadAllText(configPath);
                    _config = JsonConvert.DeserializeObject<AppConfig>(json);
                    return _config ?? new AppConfig();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading config: {ex.Message}");
            }

            return new AppConfig();
        }

        public string GenerateRandomName()
        {
            if (_config == null || _config.NamePrefixes.Count == 0 || _config.NameSuffixes.Count == 0)
            {
                return GenerateUniqueFallbackName();
            }

            // Versuche bis zu 100 mal einen eindeutigen Namen zu generieren
            for (int attempt = 0; attempt < 100; attempt++)
            {
                var prefix = _config.NamePrefixes[_random.Next(_config.NamePrefixes.Count)];
                var suffix = _config.NameSuffixes[_random.Next(_config.NameSuffixes.Count)];
                var name = prefix + suffix;

                // Prüfe ob der Name bereits verwendet wurde
                if (!_usedNames.Contains(name))
                {
                    _usedNames.Add(name);
                    return name;
                }
            }

            // Falls nach 100 Versuchen kein eindeutiger Name gefunden wurde,
            // füge eine Nummer hinzu
            return GenerateUniqueFallbackName();
        }

        private string GenerateUniqueFallbackName()
        {
            int counter = 1;
            string baseName = "Region";
            string name;

            do
            {
                name = $"{baseName}{counter:D4}";
                counter++;
            } while (_usedNames.Contains(name));

            _usedNames.Add(name);
            return name;
        }

        public void ResetUsedNames()
        {
            _usedNames.Clear();
        }

        public int GetUsedNamesCount()
        {
            return _usedNames.Count;
        }

        public string GetTooltip(string key)
        {
            if (_config?.Tooltips != null && _config.Tooltips.ContainsKey(key))
            {
                return _config.Tooltips[key];
            }
            return string.Empty;
        }

        public List<int> GetValidSizes()
        {
            return _config?.ValidSizes ?? new List<int> 
            { 
                256, 512, 768, 1024, 1280, 1536, 1792, 2048, 
                2304, 2560, 2816, 3072, 3328, 3584, 3840, 4096 
            };
        }

        public int GetNearestValidSize(int size)
        {
            var validSizes = GetValidSizes();
            var validSize = validSizes.Where(s => s <= size).DefaultIfEmpty(256).Max();
            return validSize;
        }

        public List<string> GetSpiralTypes()
        {
            return _config?.SpiralTypes ?? new List<string> 
            { 
                "archimedean_spiral1", "archimedean_spiral2", "circle1", "circle2", 
                "fibonacci_spiral", "flower", "grid1", "grid2", "logarithmic_spiral", 
                "logistic", "random1", "random2", "star", "square", "rectangle_3_2", "rectangle_2_3"
            };
        }

        public DefaultSettings GetDefaultSettings()
        {
            return _config?.DefaultSettings ?? new DefaultSettings();
        }
    }
}
