using System;
using System.Collections;

class Labirinto
{
    private int largura = 0;
    private int altura = 0;
    private readonly Coordenada entrada, saida;
    byte[,] matriz;
    private readonly bool quebrado = false;
    private readonly Random rand = new Random();

    public byte[,] Matriz => matriz;

    public int Largura => largura;
    public int Altura => altura;

    public Labirinto(int dificuldade, string tipo)
    {
        if (dificuldade < 1)
            dificuldade = 1;

        largura = dificuldade * 4 - 1;
        altura = dificuldade * 2 + 1;

        matriz = new byte[altura, largura];

        for (int i = 0; i < altura; i++)
            for (int j = 0; j < largura; j++)
                matriz[i, j] = 1;

        switch (tipo)
        {
            case "fixo":
                entrada = new Coordenada(largura / 2, 0);
                saida = new Coordenada(largura / 2, altura - 1);
                break;

            case "randomico":
                entrada = new Coordenada(rand.Next(largura / 2) * 2 + 1, 0);
                saida = new Coordenada(rand.Next(largura / 2) * 2 + 1, altura - 1);
                break;

            case "quebrado":
                entrada = new Coordenada(rand.Next(largura / 2) * 2 + 1, 0);
                saida = new Coordenada(rand.Next(largura / 2) * 2 + 1, altura - 1);
                quebrado = true;
                break;

            default:
                entrada = new Coordenada(1, 0);
                saida = new Coordenada(largura - 2, altura - 1);
                break;
        }

        Pilha<Coordenada> caminho = new Pilha<Coordenada>(largura * altura);
        Coordenada atual = new Coordenada(entrada);

        caminho.Empilhar(atual);
        matriz[entrada.Y, entrada.X] = 2; //Abrir a entrada
        atual = new Coordenada(entrada.X, entrada.Y + 1);
        caminho.Empilhar(atual);
        matriz[atual.Y, atual.X] = 0;

        do
        {
            ArrayList fila = new ArrayList(3);

            if (Cavavel(atual.X - 2, atual.Y))
                fila.Add(new Coordenada(atual.X - 2, atual.Y));

            if (Cavavel(atual.X + 2, atual.Y))
                fila.Add(new Coordenada(atual.X + 2, atual.Y));

            if (Cavavel(atual.X, atual.Y - 2))
                fila.Add(new Coordenada(atual.X, atual.Y - 2));

            if (Cavavel(atual.X, atual.Y + 2))
                fila.Add(new Coordenada(atual.X, atual.Y + 2));

            if (fila.Count <= 0)//Regressao
            {
                caminho.Desempilhar();
                caminho.Desempilhar();
                if (caminho.EstaVazia)
                    break;

                atual = caminho.OTopo();
                continue;
            }

            int escolha = rand.Next(fila.Count);

            Coordenada entre = new Coordenada((atual.X + ((Coordenada)fila[escolha]).X) / 2,
                                                            (atual.Y + ((Coordenada)fila[escolha]).Y) / 2);
            Cavar(entre);
            Cavar((Coordenada)fila[escolha]);

            caminho.Empilhar(entre);
            caminho.Empilhar((Coordenada)fila[escolha]);

            atual = (Coordenada)fila[escolha];

        } while (true);

        matriz[saida.Y, saida.X] = 3;

        if (quebrado)
            Quebrar();
    }

    public bool Cavavel(int x, int y)
    {
        try { if (x <= 0 || x >= largura - 1) return false; }
        catch (IndexOutOfRangeException) { return false; }
        try { if (y <= 0 || y >= altura - 1) return false; }
        catch (IndexOutOfRangeException) { return false; }
        return matriz[y,x] == 1;
    }

    public void Cavar(Coordenada cord)
    {
        matriz[cord.Y, cord.X] = 0;
    }

    public void Quebrar()
    {
        for (int i = 0; i < altura; i++)
        {
            byte quebravel = 0;
            int x = rand.Next((largura - 2) / 2) * 2 + 2;
            int y = rand.Next((altura - 2) / 2) * 2 + 2;

            if (matriz[y, x - 1] == 1) quebravel++;
            if (matriz[y, x + 1] == 1) quebravel++;
            if (matriz[y - 1, x] == 1) quebravel++;
            if (matriz[y + 1, x] == 1) quebravel++;

            if (quebravel == 2
                && (matriz[y, x - 1] == 1 && matriz[y, x + 1] == 1 || matriz[y - 1, x] == 1 && matriz[y + 1, x] == 1))
            {
                matriz[y, x] = 0;
                continue;
            }
            i--;
        }
    }

    public override string ToString()
    {
        string res = altura + "\n" + largura;
        for (int i = 0; i < altura; i++)
        {
            for (int j = 0; j < largura; j++)
            {
                res += this.matriz[i, j];
            }
            res += "\n";
        }
        return res;

    }
}
