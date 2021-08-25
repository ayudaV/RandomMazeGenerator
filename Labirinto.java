import java.util.ArrayList;

public class Labirinto {

    int largura, altura;
    Coordenada entrada, saida;
    char[][] labirinto;
    boolean quebrado = false;

    public Labirinto(int dificuldade, String tipo) throws Exception {
        if(dificuldade < 1)
            dificuldade = 1;

        largura = dificuldade * 4 - 1;
        altura  = dificuldade * 2 + 1;

        labirinto = new char[altura][largura];

        for(int i=0; i < altura; i++)
            for(int j=0; j < largura; j++)
                labirinto[i][j] = '#';

        switch (tipo) {
            case "fixo" -> {
                entrada = new Coordenada(largura / 2, 0);
                saida = new Coordenada(largura / 2, altura - 1);
            }
            case "randomico" -> {
                entrada = new Coordenada((int) (Math.random() * (largura / 2)) * 2 + 1, 0);
                saida = new Coordenada((int) (Math.random() * (largura / 2)) * 2 + 1, altura - 1);
            }
            case "quebrado" -> {
                entrada = new Coordenada((int) (Math.random() * (largura / 2)) * 2 + 1, 0);
                saida = new Coordenada((int) (Math.random() * (largura / 2)) * 2 + 1, altura - 1);
                quebrado = true;
            }
            default -> {
                entrada = new Coordenada(1, 0);
                saida = new Coordenada(largura - 2, altura - 1);
            }
        }

        Pilha<Coordenada> caminho = new Pilha<>(largura * altura);
        Coordenada atual = new Coordenada(entrada);

        caminho.guardeUmItem(atual);
        labirinto[entrada.getY()][entrada.getX()] = 'E'; //Abrir a entrada
        atual = new Coordenada(entrada.getX(),entrada.getY()+1);
        caminho.guardeUmItem(atual);
        labirinto[atual.getY()][atual.getX()] = ' ';

        do
        {
            ArrayList<Coordenada> fila = new ArrayList<>(3);

            if(cavavel(atual.getX() - 1, atual.getY()))
                fila.add(new Coordenada(atual.getX() - 1, atual.getY()));

            if(cavavel(atual.getX() + 1, atual.getY()))
                fila.add(new Coordenada(atual.getX() + 1, atual.getY()));

            if(cavavel(atual.getX(), atual.getY() - 1))
                fila.add(new Coordenada(atual.getX(), atual.getY() - 1));

            if(cavavel(atual.getX(), atual.getY() + 1))
                fila.add(new Coordenada(atual.getX(), atual.getY() + 1));

            for(;;)
            {
                if(fila.isEmpty())
                    break;

                int escolha = (int) (Math.random() * fila.size());

                Coordenada proximo = new Coordenada(atual.getX() + (fila.get(escolha).getX()-atual.getX()) * 2,
                                                    atual.getY() + (fila.get(escolha).getY()-atual.getY()) * 2);

                if(labirinto[proximo.getY()][proximo.getX()] == '#')
                    //Se a coordenada a 2 casa de distancia da direcao escolhida for parede ele cava para essa direcao
                {
                    cavar(fila.get(escolha));
                    cavar(proximo);

                    caminho.guardeUmItem(fila.get(escolha));
                    caminho.guardeUmItem(proximo);

                    atual = proximo;
                    break;
                }
                fila.remove(escolha);
            }

            if (fila.isEmpty())//Regressao
            {
                caminho.removaUmItem();
                caminho.removaUmItem();
                if(caminho.isVazia())
                    break;

                atual = caminho.recupereUmItem();
            }
        }while (true);
        labirinto[saida.getY()][saida.getX()] = 'S';
        if(quebrado) quebrar();
    }

    public boolean cavavel(int x, int y) {
        if(x <= 0 || x >= largura-1) return false;
        if(y <= 0 || y >= altura-1)  return false;
        return labirinto[y][x] == '#';
    }

    public void cavar(Coordenada cord) {
        labirinto[cord.getY()][cord.getX()] = ' ';

    }

    public void quebrar() { //Este metodo gera caminhos alternativos, já que só existe um caminho por padrao.
        for(int i = 0; i < altura; i++)
        {
            byte quebravel = 0;
            int x = (int) (Math.random() * (largura/ 2) - 1) * 2 + 2;
            int y = (int) (Math.random() * (altura / 2) - 1) * 2 + 2;

            if(labirinto[y][x-1] == '#') quebravel++;
            if(labirinto[y][x+1] == '#') quebravel++;
            if(labirinto[y-1][x] == '#') quebravel++;
            if(labirinto[y+1][x] == '#') quebravel++;

            if(quebravel == 2 && (labirinto[y][x-1] == '#' && labirinto[y][x+1] == '#' || labirinto[y-1][x] == '#' && labirinto[y+1][x] == '#'))
            {
                labirinto[y][x] = ' ';
                continue;
            }
            i--;
        }
    }

    public char[][] getLabirinto() {
        return labirinto;
    }

    @Override
    public String toString() {
        StringBuilder res = new StringBuilder();
        res.append(this.altura).append("\n").append(this.largura).append("\n");
        for(int i=0; i < altura; i++)
        {
            for(int j=0; j < largura; j++)
            {
                res.append(this.labirinto[i][j]);
            }
            res.append("\n");
        }
        return res.toString();
    }
}
