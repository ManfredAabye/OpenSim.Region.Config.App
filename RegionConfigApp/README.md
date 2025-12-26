# RegionConfigApp - OpenSim Region Konfigurator

Eine WPF-Anwendung zur Generierung von OpenSimulator Region-Konfigurationsdateien mit verschiedenen Platzierungsmustern.

## Features

### ✨ Hauptfunktionen

- **Automatische Namensgenerierung**: Generiert deutsche Regionsnamen aus konfigurierbaren Vor- und Nachsilben
- **14 Spiral-Algorithmen**: Verschiedene Muster für die automatische Region-Platzierung
- **JSON-Konfiguration**: Alle Einstellungen und Vorlagen sind in `config.json` anpassbar
- **UUID-Management**: Automatische Generierung von Region- und Maptile-UUIDs
- **Tooltips**: Hilfreiche Tooltips für alle Felder (aus Version 12)
- **INI-Export**: Speichert Konfigurationen im OpenSim-kompatiblen INI-Format

### 🌀 Verfügbare Spiral-Muster

1. **flower** - Blumenspirale (Golden Angle)
2. **fibonacci_spiral** - Fibonacci-Spirale
3. **archimedean_spiral1** - Archimedische Spirale (Variante 1)
4. **archimedean_spiral2** - Archimedische Spirale (Variante 2)
5. **logarithmic_spiral** - Logarithmische Spirale
6. **circle1** - Kreis (10 Punkte)
7. **circle2** - Kreis (20 Punkte)
8. **grid1** - Gitter (10x10)
9. **grid2** - Gitter (anpassbar)
10. **star** - Sternmuster
11. **logistic** - Logistische Funktion
12. **random1** - Zufällig (0-2000)
13. **random2** - Zufällig (800-1200)

## Konfiguration

### config.json Struktur

```json
{
  "NamePrefixes": ["Alt", "Augs", "Ber", ...],
  "NameSuffixes": ["markt", "burg", "lin", ...],
  "DefaultSettings": {
    "Location": "1000,1000",
    "Size": 256,
    "InternalPort": 9050,
    ...
  },
  "Tooltips": {
    "RegionName": "Name der Region...",
    ...
  }
}
```

### Anpassbare Einstellungen

- **NamePrefixes/NameSuffixes**: Für die Namensgenerierung
- **DefaultSettings**: Standardwerte für neue Regionen
- **ValidSizes**: Erlaubte Regiongrößen (256, 512, 768, 1024, 1280, 1536, 1792, 2048)
- **Tooltips**: Hilfetexte für alle UI-Elemente

## Verwendung

### 1. Region hinzufügen

1. Wählen Sie ein Spiral-Muster aus dem Dropdown
2. Passen Sie bei Bedarf die Standardwerte an
3. Klicken Sie auf "Add Region"
4. Die Region wird automatisch mit der nächsten Position im Muster erstellt

### 2. Konfiguration speichern

1. Fügen Sie eine oder mehrere Regionen hinzu
2. Klicken Sie auf "Save Config"
3. Wählen Sie einen Dateinamen (z.B. `Regions.ini`)
4. Die Datei kann direkt in OpenSim verwendet werden

### 3. Ausgabe-Format

Die generierte INI-Datei enthält:

- Aktive Einstellungen (ohne Semikolon)
- Kommentierte optionale Einstellungen (mit Semikolon)

Beispiel:

```ini
[Ostburg]
RegionUUID = 12345678-1234-1234-1234-123456789abc
Location = 1000,1000
SizeX = 256
SizeY = 256
SizeZ = 256
InternalPort = 9050
InternalAddress = 0.0.0.0
AllowAlternatePorts = False
ExternalHostName = SYSTEMIP
MaxPrims = 100000
MaxAgents = 99
MaxPrimsPerUser = -1
;MaptileStaticUUID = 12345678-1234-1234-1234-123456789abc
;NonPhysicalPrimMax = 256
;PhysicalPrimMax = 64
...
```

## Systemanforderungen

- .NET 8.0 oder höher
- Windows (WPF-Anwendung)
- Optional: icon.ico im Ausgabeverzeichnis

## Build

```powershell
cd RegionConfigApp
dotnet restore
dotnet build
dotnet run
```

## Unterschiede zur Python-Version

### Vorteile der C#-Version

✅ **Keine Python-Installation erforderlich**
✅ **Native Windows-Performance**
✅ **JSON-basierte Konfiguration** - Änderungen ohne Neukompilierung
✅ **Typsicherheit** - Weniger Laufzeitfehler
✅ **IntelliSense-Unterstützung** in Visual Studio/VS Code
✅ **Moderne WPF-UI** mit Data Binding

### Portierte Features

- ✅ Alle 14 Spiral-Algorithmen aus Version 11
- ✅ Tooltip-System aus Version 12
- ✅ Automatische Namensgenerierung
- ✅ UUID-Management
- ✅ INI-Export mit Case-Preservation
- ✅ Validierung der Region-Größen

## Projektstruktur

```bash
RegionConfigApp/
├── Models/
│   ├── AppConfig.cs          - Konfigurationsmodell
│   └── RegionConfig.cs       - Region-Datenmodell
├── Services/
│   ├── ConfigService.cs      - Konfigurationsverwaltung
│   ├── SpiralGenerator.cs    - Alle Spiral-Algorithmen
│   └── IniWriter.cs          - INI-Datei-Export
├── MainWindow.xaml           - UI-Definition
├── MainWindow.xaml.cs        - UI-Logik
├── App.xaml                  - Anwendungsressourcen
├── App.xaml.cs               - Anwendungs-Entry Point
├── config.json               - Konfigurationsdatei
└── RegionConfigApp.csproj    - Projektdatei
```

## Lizenz

Portiert von RegionConfigApp_11.py und RegionConfigApp_12.py
Mit erweiterten Features und JSON-Konfiguration

## Version

**1.0.0** - C# Port von Python Version 11 + Version 12 Features

- Basis: RegionConfigApp_11.py (stabile Version)
- Features von RegionConfigApp_12.py (Tooltips)
- Neu: JSON-basierte Konfiguration
