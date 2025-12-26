using System;
using System.Collections.Generic;

namespace RegionConfigApp.Services
{
    public class SpiralGenerator
    {
        private int _index = 0;
        private double _angle = 0;
        private double _radius = 1;
        private readonly Random _random = new();
        private int _regionSize = 256; // Aktuelle Regionsgröße

        public void Reset()
        {
            _index = 0;
            _angle = 0;
            _radius = 1;
        }

        public void SetRegionSize(int size)
        {
            _regionSize = size;
        }

        private int GetGridUnits()
        {
            // Berechne wie viele 256m-Einheiten die Region belegt
            return _regionSize / 256;
        }

        public (int X, int Y) GetNextLocation(string spiralType)
        {
            return spiralType switch
            {
                "flower" => NextFlowerSpiralLocation(),
                "fibonacci_spiral" => NextFibonacciSpiralLocation(),
                "archimedean_spiral1" => NextArchimedeanSpiral1Location(),
                "archimedean_spiral2" => NextArchimedeanSpiral2Location(),
                "logarithmic_spiral" => NextLogarithmicSpiralLocation(),
                "random1" => NextRandom1Location(),
                "random2" => NextRandom2Location(),
                "grid1" => NextGrid1Location(),
                "grid2" => NextGrid2Location(),
                "circle1" => NextCircle1Location(),
                "circle2" => NextCircle2Location(),
                "star" => NextStarLocation(),
                "logistic" => NextLogisticLocation(),
                "square" => NextSquareLocation(),
                "rectangle_3_2" => NextRectangle32Location(),
                "rectangle_2_3" => NextRectangle23Location(),
                _ => (1000, 1000)
            };
        }

        // Flower Spiral (Golden Angle)
        private (int X, int Y) NextFlowerSpiralLocation()
        {
            _angle += 137.5; // Golden angle in degrees
            var radians = _angle * Math.PI / 180.0;
            int gridUnits = GetGridUnits();
            var locationX = 1000 + (int)(_radius * Math.Cos(radians)) * gridUnits;
            var locationY = 1000 + (int)(_radius * Math.Sin(radians)) * gridUnits;
            _radius += 1; // Increment the radius gradually
            return (locationX, locationY);
        }

        // Fibonacci Spiral
        private (int X, int Y) NextFibonacciSpiralLocation()
        {
            _angle += 137.5; // Golden angle in degrees
            var radians = _angle * Math.PI / 180.0;
            _radius *= 1.618; // Fibonacci increment (Golden Ratio)
            int gridUnits = GetGridUnits();
            var locationX = 1000 + (int)(_radius * Math.Cos(radians)) * gridUnits;
            var locationY = 1000 + (int)(_radius * Math.Sin(radians)) * gridUnits;
            return (locationX, locationY);
        }

        // Archimedean Spiral 1
        private (int X, int Y) NextArchimedeanSpiral1Location()
        {
            _angle += 10; // Change the step size for tighter or looser spirals
            var radians = _angle * Math.PI / 180.0;
            
            // Archimedean spiral equation: r = a + b * theta
            double a = 5; // Initial distance from the center
            double b = 5; // Spacing between spiral arms
            _radius = a + b * _angle;

            int gridUnits = GetGridUnits();
            var locationX = 1000 + (int)(_radius * Math.Cos(radians)) * gridUnits;
            var locationY = 1000 + (int)(_radius * Math.Sin(radians)) * gridUnits;
            return (locationX, locationY);
        }

        // Archimedean Spiral 2
        private (int X, int Y) NextArchimedeanSpiral2Location()
        {
            _angle += 10; // Increment angle by a fixed value
            var radians = _angle * Math.PI / 180.0;
            int gridUnits = GetGridUnits();
            var locationX = 1000 + (int)(_radius * Math.Cos(radians)) * gridUnits;
            var locationY = 1000 + (int)(_radius * Math.Sin(radians)) * gridUnits;
            _radius += 10; // Increment the radius linearly for a smooth spiral
            return (locationX, locationY);
        }

        // Logarithmic Spiral
        private (int X, int Y) NextLogarithmicSpiralLocation()
        {
            _angle += 10; // Increase angle gradually
            var radians = _angle * Math.PI / 180.0;
            _radius *= 1.1; // Exponentially increase the radius
            int gridUnits = GetGridUnits();
            var locationX = 1000 + (int)(_radius * Math.Cos(radians)) * gridUnits;
            var locationY = 1000 + (int)(_radius * Math.Sin(radians)) * gridUnits;
            return (locationX, locationY);
        }

        // Random Location 1
        private (int X, int Y) NextRandom1Location()
        {
            int gridUnits = GetGridUnits();
            var locationX = _random.Next(0, 2001 / gridUnits) * gridUnits;
            var locationY = _random.Next(0, 2001 / gridUnits) * gridUnits;
            return (locationX, locationY);
        }

        // Random Location 2
        private (int X, int Y) NextRandom2Location()
        {
            int gridUnits = GetGridUnits();
            var locationX = _random.Next(800 / gridUnits, 1201 / gridUnits) * gridUnits;
            var locationY = _random.Next(800 / gridUnits, 1201 / gridUnits) * gridUnits;
            return (locationX, locationY);
        }

        // Grid Location 1
        private (int X, int Y) NextGrid1Location()
        {
            int gridUnits = GetGridUnits();
            var locationX = 1000 + (_index % 10) * 100 * gridUnits; // Move horizontally
            var locationY = 1000 + (_index / 10) * 100 * gridUnits; // Move vertically
            _index++;
            return (locationX, locationY);
        }

        // Grid Location 2
        private (int X, int Y) NextGrid2Location()
        {
            // Define grid parameters
            int gridSize = 100; // Distance between grid points
            int numColumns = 10; // Number of columns in the grid
            int gridUnits = GetGridUnits();

            // Calculate row and column based on the current index
            int row = _index / numColumns;
            int column = _index % numColumns;

            // Calculate the x, y position
            var locationX = 1000 + column * gridSize * gridUnits;
            var locationY = 1000 + row * gridSize * gridUnits;

            _index++;
            return (locationX, locationY);
        }

        // Circle Location 1
        private (int X, int Y) NextCircle1Location()
        {
            _angle += 36; // Divide 360 degrees by 10 points
            var radians = _angle * Math.PI / 180.0;
            int radius = 500; // Constant radius
            int gridUnits = GetGridUnits();
            var locationX = 1000 + (int)(radius * Math.Cos(radians)) * gridUnits;
            var locationY = 1000 + (int)(radius * Math.Sin(radians)) * gridUnits;
            return (locationX, locationY);
        }

        // Circle Location 2
        private (int X, int Y) NextCircle2Location()
        {
            int numPoints = 20; // Total number of points on the circle
            int radius = 200; // Fixed radius of the circle
            int gridUnits = GetGridUnits();

            // Calculate the angle for the current point
            double angle = (2 * Math.PI / numPoints) * _index;
            var locationX = 1000 + (int)(radius * Math.Cos(angle)) * gridUnits;
            var locationY = 1000 + (int)(radius * Math.Sin(angle)) * gridUnits;

            _index++;
            return (locationX, locationY);
        }

        // Star Location
        private (int X, int Y) NextStarLocation()
        {
            _angle += 144; // Star angle (5 points)
            var radians = _angle * Math.PI / 180.0;
            int gridUnits = GetGridUnits();
            var locationX = 1000 + (int)(_radius * Math.Cos(radians)) * gridUnits;
            var locationY = 1000 + (int)(_radius * Math.Sin(radians)) * gridUnits;
            return (locationX, locationY);
        }

        // Logistic Function Location
        private (int X, int Y) NextLogisticLocation()
        {
            _angle += 10;
            var radians = _angle * Math.PI / 180.0;
            double K = 2000; // Carrying capacity (limit of growth)
            var factor = K / (1 + Math.Exp(-0.1 * _angle));
            int gridUnits = GetGridUnits();
            var locationX = 1000 + (int)(_radius * factor * Math.Cos(radians) / 1000) * gridUnits;
            var locationY = 1000 + (int)(_radius * factor * Math.Sin(radians) / 1000) * gridUnits;
            return (locationX, locationY);
        }

        // Square Pattern - Perfektes Quadrat
        private (int X, int Y) NextSquareLocation()
        {
            int gridUnits = GetGridUnits();
            int layer = (int)Math.Sqrt(_index);
            int sideLength = layer * 2 + 1;
            int posInLayer = _index - layer * layer;
            
            int x = 0, y = 0;
            
            if (posInLayer < sideLength)
            {
                // Untere Seite (links nach rechts)
                x = -layer + posInLayer;
                y = -layer;
            }
            else if (posInLayer < 2 * sideLength - 1)
            {
                // Rechte Seite (unten nach oben)
                x = layer;
                y = -layer + (posInLayer - sideLength + 1);
            }
            else if (posInLayer < 3 * sideLength - 2)
            {
                // Obere Seite (rechts nach links)
                x = layer - (posInLayer - 2 * sideLength + 2);
                y = layer;
            }
            else
            {
                // Linke Seite (oben nach unten)
                x = -layer;
                y = layer - (posInLayer - 3 * sideLength + 3);
            }
            
            _index++;
            return (1000 + x * gridUnits, 1000 + y * gridUnits);
        }

        // Rectangle 3:2 Pattern - Breiter als hoch
        private (int X, int Y) NextRectangle32Location()
        {
            int gridUnits = GetGridUnits();
            int cols = 3; // 3 Spalten
            int rows = 2; // 2 Zeilen
            
            int x = _index % cols;
            int y = _index / cols;
            
            // Zentriere das Muster um (1000, 1000)
            int offsetX = -(cols - 1) / 2;
            int offsetY = -(rows - 1) / 2;
            
            _index++;
            
            // Wenn eine Ebene vollständig ist, wechsle zur nächsten
            if (_index % (cols * rows) == 0)
            {
                cols += 3;
                rows += 2;
            }
            
            return (1000 + (x + offsetX) * gridUnits, 1000 + (y + offsetY) * gridUnits);
        }

        // Rectangle 2:3 Pattern - Höher als breit
        private (int X, int Y) NextRectangle23Location()
        {
            int gridUnits = GetGridUnits();
            int cols = 2; // 2 Spalten
            int rows = 3; // 3 Zeilen
            
            int x = _index % cols;
            int y = _index / cols;
            
            // Zentriere das Muster um (1000, 1000)
            int offsetX = -(cols - 1) / 2;
            int offsetY = -(rows - 1) / 2;
            
            _index++;
            
            // Wenn eine Ebene vollständig ist, wechsle zur nächsten
            if (_index % (cols * rows) == 0)
            {
                cols += 2;
                rows += 3;
            }
            
            return (1000 + (x + offsetX) * gridUnits, 1000 + (y + offsetY) * gridUnits);
        }
    }
}
