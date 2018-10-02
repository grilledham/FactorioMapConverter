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
            new TileData() {name = "concrete", Number = 3, color = Color.FromArgb(255,99,101,99), enabled = true},
            new TileData() {name = "deepwater-green", Number = 4, color = Color.FromArgb(255,16,36,11), enabled = true},
            new TileData() {name = "deepwater", Number = 5, color = Color.FromArgb(255,33,65,74), enabled = true},
            new TileData() {name = "dirt-1", Number = 6, color = Color.FromArgb(255,115,105,82), enabled = true},
            new TileData() {name = "dirt-2", Number = 7, color = Color.FromArgb(255,107,93,74), enabled = true},
            new TileData() {name = "dirt-3", Number = 8, color = Color.FromArgb(255,99,85,66), enabled = true},
            new TileData() {name = "dirt-4", Number = 9, color = Color.FromArgb(255,90,73,57), enabled = true},
            new TileData() {name = "dirt-5", Number = 10, color = Color.FromArgb(255,99,81,66), enabled = true},
            new TileData() {name = "dirt-6", Number = 11, color = Color.FromArgb(255,44,39,29), enabled = true},
            new TileData() {name = "dirt-7", Number = 12, color = Color.FromArgb(255,57,48,41), enabled = true},
            new TileData() {name = "dry-dirt", Number = 13, color = Color.FromArgb(255,107,93,66), enabled = true},
            new TileData() {name = "grass-1", Number = 14, color = Color.FromArgb(255,49,52,24), enabled = true},
            new TileData() {name = "grass-2", Number = 15, color = Color.FromArgb(255,57,52,33), enabled = true},
            new TileData() {name = "grass-3", Number = 16, color = Color.FromArgb(255,57,56,41), enabled = true},
            new TileData() {name = "grass-4", Number = 17, color = Color.FromArgb(255,49,44,33), enabled = true},
            new TileData() {name = "hazard-concrete-left", Number = 18, color = Color.FromArgb(255,123,125,0), enabled = true},
            new TileData() {name = "hazard-concrete-right", Number = 19, color = Color.FromArgb(255,123,125,0), enabled = true},
            new TileData() {name = "lab-dark-1", Number = 20, color = Color.FromArgb(255,0,0,0), enabled = true},
            new TileData() {name = "lab-dark-2", Number = 21, color = Color.FromArgb(255,29,29,29), enabled = true},
            new TileData() {name = "lab-white", Number = 22, color = Color.FromArgb(255,255,255,255), enabled = true},
            new TileData() {name = "out-of-map", Number = 23, color = Color.FromArgb(255,0,0,0), enabled = true},
            new TileData() {name = "red-desert-0", Number = 24, color = Color.FromArgb(255,90,73,57), enabled = true},
            new TileData() {name = "red-desert-1", Number = 25, color = Color.FromArgb(255,107,85,66), enabled = true},
            new TileData() {name = "red-desert-2", Number = 26, color = Color.FromArgb(255,123,93,74), enabled = true},
            new TileData() {name = "red-desert-3", Number = 27, color = Color.FromArgb(255,115,85,74), enabled = true},
            new TileData() {name = "sand-1", Number = 28, color = Color.FromArgb(255,148,130,107), enabled = true},
            new TileData() {name = "sand-2", Number = 29, color = Color.FromArgb(255,132,113,90), enabled = true},
            new TileData() {name = "sand-3", Number = 30, color = Color.FromArgb(255,123,109,82), enabled = true},
            new TileData() {name = "stone-path", Number = 31, color = Color.FromArgb(255,29,29,29), enabled = true},
            new TileData() {name = "water-green", Number = 32, color = Color.FromArgb(255,24,48,16), enabled = true},
            new TileData() {name = "water", Number = 33, color = Color.FromArgb(255,49,81,90), enabled = true},
        };
    }
}
