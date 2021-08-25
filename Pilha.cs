using System;
using System.Collections.Generic;
using System.Text;

class Pilha<Dado> : IStack<Dado>
{
    int maximoPosicoes;
    Dado[] p; // vetor onde serão armazenados os dados empilhados
    int topo; // índice da posição usada por último nesse vetor
    public Pilha(int posic)
    {
        p = new Dado[posic];
        maximoPosicoes = posic;
        topo = -1;
    }
    public Pilha() : this(500)
    { }
    public void Empilhar(Dado elemento)
    {
        if (topo == maximoPosicoes)
            throw new Exception("Pilha transbordou!");
        p[++topo] = elemento;
    }
    public Dado Desempilhar()
    {
        if (EstaVazia)
            throw new PilhaVaziaException("Pilha esvaziou!");
        var valor = p[topo--];
        return valor;
    }
    public Dado OTopo()
    {
        if (EstaVazia)
            throw new PilhaVaziaException("Pilha esvaziou!");
        return p[topo]; // devolve o valor armazenado na última posição em uso do vetor p
    }

    public int Tamanho { get => topo + 1; }
    public bool EstaVazia { get => topo < 0; }
}