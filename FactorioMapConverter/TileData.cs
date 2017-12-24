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
            new TileData() {name = "concrete", Number = 3, color = Color.FromArgb(255,80,81,80), enabled = true},
            new TileData() {name = "deepwater-green", Number = 4, color = Color.FromArgb(255,20,36,11), enabled = true},
            new TileData() {name = "deepwater", Number = 5, color = Color.FromArgb(255,22,70,86), enabled = true},
            new TileData() {name = "dirt-1", Number = 6, color = Color.FromArgb(255,99,78,47), enabled = true},
            new TileData() {name = "dirt-2", Number = 7, color = Color.FromArgb(255,93,72,44), enabled = true},
            new TileData() {name = "dirt-3", Number = 8, color = Color.FromArgb(255,85,63,36), enabled = true},
            new TileData() {name = "dirt-4", Number = 9, color = Color.FromArgb(255,69,52,32), enabled = true},
            new TileData() {name = "dirt-5", Number = 10, color = Color.FromArgb(255,64,47,29), enabled = true},
            new TileData() {name = "dirt-6", Number = 11, color = Color.FromArgb(255,62,45,29), enabled = true},
            new TileData() {name = "dirt-7", Number = 12, color = Color.FromArgb(255,61,44,26), enabled = true},
            new TileData() {name = "dry-dirt", Number = 13, color = Color.FromArgb(255,89,70,44), enabled = true},
            new TileData() {name = "grass-1", Number = 14, color = Color.FromArgb(255,51,45,16), enabled = true},
            new TileData() {name = "grass-2", Number = 15, color = Color.FromArgb(255,53,45,18), enabled = true},
            new TileData() {name = "grass-3", Number = 16, color = Color.FromArgb(255,52,43,22), enabled = true},
            new TileData() {name = "grass-4", Number = 17, color = Color.FromArgb(255,50,37,19), enabled = true},
            new TileData() {name = "hazard-concrete-left", Number = 18, color = Color.FromArgb(255,80,73,53), enabled = true},
            new TileData() {name = "hazard-concrete-right", Number = 19, color = Color.FromArgb(255,80,73,53), enabled = true},
            new TileData() {name = "lab-dark-1", Number = 20, color = Color.FromArgb(255,27,27,27), enabled = true},
            new TileData() {name = "lab-dark-2", Number = 21, color = Color.FromArgb(255,48,48,48), enabled = true},
            new TileData() {name = "out-of-map", Number = 22, color = Color.FromArgb(255,0,0,0), enabled = true},
            new TileData() {name = "red-desert-0", Number = 23, color = Color.FromArgb(255,77,55,29), enabled = true},
            new TileData() {name = "red-desert-1", Number = 24, color = Color.FromArgb(255,96,69,39), enabled = true},
            new TileData() {name = "red-desert-2", Number = 25, color = Color.FromArgb(255,99,73,41), enabled = true},
            new TileData() {name = "red-desert-3", Number = 26, color = Color.FromArgb(255,104,79,44), enabled = true},
            new TileData() {name = "sand-1", Number = 27, color = Color.FromArgb(255,142,114,71), enabled = true},
            new TileData() {name = "sand-2", Number = 28, color = Color.FromArgb(255,131,102,61), enabled = true},
            new TileData() {name = "sand-3", Number = 29, color = Color.FromArgb(255,114,91,55), enabled = true},
            new TileData() {name = "stone-path", Number = 30, color = Color.FromArgb(255,71,66,58), enabled = true},
            new TileData() {name = "water-green", Number = 31, color = Color.FromArgb(255,30,48,11), enabled = true},
            new TileData() {name = "water", Number = 32, color = Color.FromArgb(255,22,89,104), enabled = true},
        };
    }
}
