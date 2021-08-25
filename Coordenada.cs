using System;
using System.Collections.Generic;
using System.Text;

class Coordenada
{
    int x, y;
    public Coordenada(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int X { get => x; set => x = value; }
    public int Y { get => y; set => y = value; }

    public override String ToString()
    {
        //Retorna as cordenadas x e y
        return "[" + this.x + "; " + this.y + "]";
    }

    public override bool Equals(Object o)
    {
        //Compara as cordenadas 
        if (this == o) return true;
        if (o == null || GetType() != o.GetType()) return false;
        Coordenada coordenada = (Coordenada)o;
        return x == coordenada.x && y == coordenada.y;
    }

    //Método obrigatório hashCode()
    public override int GetHashCode()
    {
        int ret = 666/*qualquer positivo*/;

        ret = ret * 7/*primo*/ + this.x;
        ret = ret * 7/*primo*/ + this.y;

        if (ret < 0)
            ret = -ret;
        return ret;
    }

    //Construtor de copia
    public Coordenada(Coordenada cord)
    {
        //Define o valor de x e y
        this.x = cord.x;
        this.y = cord.y;
    }

    //Metodo Clone
    public Object Clone()
    {
        Coordenada ret = null;

        try
        {
            ret = new Coordenada(this);
        }
        catch (Exception)
        { }

        return ret;
    }
}
