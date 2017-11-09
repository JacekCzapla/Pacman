using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Packman
{
    public partial class Form1 : Form
    {

        int PacPosX = 9;
        int PacPosY = 6;

        private int[,] WallsData;

        private int[,] SpotsData;

        public Form1()
        {
            InitializeComponent();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog();
            if (f.ShowDialog() == DialogResult.Cancel) {
                return;
            }

            var lines = File.ReadAllLines(f.FileName);
            GenerateWallsData(lines);



        }

        private void GenerateWallsData(string[] lines)
        {
            WallsData = new int[lines[0].Length, lines.Length];
            SpotsData = new int[lines[0].Length, lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int y = 0; y < lines[i].Length; y++)
                {
                    var s = lines[i][y];
                    
                    WallsData[y, i] = s == '#' ? 0 : 1;
                    SpotsData[y, i] = s == '#' ? 0 : 1;

                }
            }

            SpotsData[PacPosX, PacPosY] = 0;

            panel1.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel;
            if (p == null)
                return;

            if (WallsData == null || WallsData.Length <= 0)
                return;

            //int xsize = WallsData.
            int xs = WallsData.GetLength(0);
            int ys = WallsData.GetLength(1);


            // labirynt
            for (int x = 0; x < xs; x++) {
                for (int y = 0; y < ys; y++)
                {
                    Pen blackPen = new Pen(Color.Black, 1);
                    Brush b = WallsData[x,y] == 0 ? Brushes.Black : Brushes.White;

                    // labirynt
                    e.Graphics.FillRectangle(b, x * 20 + 20, y * 20 + 20, 20, 20);
                    // kropka
                    if (SpotsData[x, y] == 1) {
                        Brush spotBrush = Brushes.Blue;
                        e.Graphics.FillRectangle(spotBrush, x * 20 + 27, y * 20 + 27, 6, 6);
                    }
                    
                }
            }

            // kropki



            // rysowanie pacmana
            Brush bred = Brushes.Red;
            e.Graphics.FillRectangle(bred, PacPosX * 20 + 20 +3, PacPosY * 20 + 20 + 3, 14, 14);

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) {
                case Keys.Left:
                    PacPosX = PacPosX - WallsData[PacPosX - 1, PacPosY];
                    break;
                case Keys.Right:
                    PacPosX = PacPosX + WallsData[PacPosX + 1, PacPosY];
                    break;
                case Keys.Up:
                    int s1 = WallsData[PacPosX, PacPosY - 1];
                    PacPosY = PacPosY - s1;
                    break;
                case Keys.Down:
                    int s2 = WallsData[PacPosX, PacPosY + 1];
                    PacPosY = PacPosY + s2;

                    break;
            }
            SpotsData[PacPosX, PacPosY] = 0;

            panel1.Refresh();

        }
    }
}
