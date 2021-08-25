using System;
using System.Drawing;
using System.Text;

class Impressor
{
    private static readonly Image[] imagens = new Image[] { Image.FromFile(@"..\..\..\Images\path.png"),
                                        Image.FromFile(@"..\..\..\Images\wall.png"),
                                        Image.FromFile(@"..\..\..\Images\start.png"),
                                        Image.FromFile(@"..\..\..\Images\end.png")};

    public static Bitmap GetSprite(byte id)
    {
        return new Bitmap(imagens[id]);
    }
}
