# Icon-Anleitung

## Optional: Eigenes Icon hinzufügen

Wenn Sie ein eigenes Icon für die Anwendung verwenden möchten:

### Schritt 1: Icon-Datei erstellen

1. Erstellen Sie eine `icon.ico` Datei (32x32 oder 48x48 Pixel empfohlen)
2. Platzieren Sie die Datei im Projektverzeichnis: `RegionConfigApp/icon.ico`

### Schritt 2: Projekt-Datei anpassen

Öffnen Sie `RegionConfigApp.csproj` und fügen Sie in der `<PropertyGroup>` hinzu:

```xml
<ApplicationIcon>icon.ico</ApplicationIcon>
```

Die vollständige PropertyGroup sollte dann so aussehen:

```xml
<PropertyGroup>
  <OutputType>WinExe</OutputType>
  <TargetFramework>net8.0-windows</TargetFramework>
  <UseWPF>true</UseWPF>
  <Nullable>enable</Nullable>
  <ApplicationIcon>icon.ico</ApplicationIcon>
</PropertyGroup>
```

### Icon im Ausgabeverzeichnis

Das Icon wird automatisch ins Ausgabeverzeichnis kopiert (siehe `.csproj`):

```xml
<ItemGroup>
  <None Update="icon.ico">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

### Icon-Quellen

Kostenlose Icon-Generatoren:

- <https://favicon.io/>
- <https://www.icoconverter.com/>
- <https://convertio.co/de/png-ico/>

### Ohne Icon

Die Anwendung funktioniert auch ohne Icon-Datei. In diesem Fall wird das Standard-Windows-Icon verwendet.
