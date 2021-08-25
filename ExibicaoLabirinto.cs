using System;
using System.Drawing;
using System.Windows.Forms;
namespace MazeRandomGenerator
{
    public partial class ExibicaoLabirinto : Form
    {
        public ExibicaoLabirinto()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Labirinto lab = new Labirinto(40, "quebrado");
            Bitmap bmp = new Bitmap(lab.Largura * 32, lab.Altura * 32);

            for (int y = 0; y < lab.Altura; y++)
            {
                for (int x = 0; x < lab.Largura; x++)
                {
                    Bitmap image = Impressor.GetSprite(lab.Matriz[y, x]);

                    for (byte i = 0; i < image.Height; i++)
                    {
                        for (byte j = 0; j < image.Width; j++)
                        {
                            bmp.SetPixel(x * 32 + j, y * 32 + i, image.GetPixel(j, i));
                        }
                    }
                }
            }

            //load bmp in picturebox1
            pictureBox1.Image = bmp;

            //save (write) random pixel image
            bmp.Save("C:\\Images\\MazeImage.png");
        }
    }
}