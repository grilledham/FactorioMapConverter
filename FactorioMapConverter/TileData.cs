using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FactorioMapConverter
{
    public class TileData : ObservableObject
    {
        private string name;
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        public int Number { get; set; }

        private Color color;
        public Color Color
        {
            get => color;
            set => Set(ref color, value);
        }

        private bool enabled;
        public bool Enabled
        {
            get => enabled;
            set => Set(ref enabled, value);
        }

        public static List<TileData> Tiles { get; } = new List<TileData>
        {
            new TileData() {name = "false", Number = 1, color = Color.FromArgb(255,0,0,0), enabled = false},
            new TileData() {name = "true", Number = 2, color = Color.FromArgb(255,255,255,255), enabled = false},
            new TileData() {name = "concrete", Number = 3, color = Color.FromArgb(255,105,104,102), enabled = true},
            new TileData() {name = "deepwater", Number = 4, color = Color.FromArgb(255,24,71,86), enabled = true},
            new TileData() {name = "deepwater-green", Number = 5, color = Color.FromArgb(255,24,38,14), enabled = true},
            new TileData() {name = "dirt", Number = 6, color = Color.FromArgb(255,154,112,43), enabled = true},
            new TileData() {name = "dirt-dark", Number = 7, color = Color.FromArgb(255,130,92,34), enabled = true},
            new TileData() {name = "grass", Number = 8, color = Color.FromArgb(255,77,64,22), enabled = true},
            new TileData() {name = "grass-dry", Number = 9, color = Color.FromArgb(255,75,56,25), enabled = true},
            new TileData() {name = "grass-medium", Number = 10, color = Color.FromArgb(255,65,57,20), enabled = true},
            new TileData() {name = "lab-dark-1", Number = 11, color = Color.FromArgb(255,44,44,44), enabled = true},
            new TileData() {name = "lab-dark-2", Number = 12, color = Color.FromArgb(255,63,63,63), enabled = true},
            new TileData() {name = "out-of-map", Number = 13, color = Color.FromArgb(255,0,0,0), enabled = true},
            new TileData() {name = "red-desert", Number = 14, color = Color.FromArgb(254,125,90,68), enabled = true},
            new TileData() {name = "red-desert-dark", Number = 15, color = Color.FromArgb(255,116,74,59), enabled = true},
            new TileData() {name = "sand", Number = 16, color = Color.FromArgb(255,177,144,65), enabled = true},
            new TileData() {name = "sand-dark", Number = 17, color = Color.FromArgb(255,161,126,49), enabled = true},
            new TileData() {name = "stone-path", Number = 18, color = Color.FromArgb(255,76,77,74), enabled = true},
            new TileData() {name = "water", Number = 19, color = Color.FromArgb(255,23,90,107), enabled = true},
            new TileData() {name = "water-green", Number = 20, color = Color.FromArgb(255,29,47,15), enabled = true},
        };
    }
}
