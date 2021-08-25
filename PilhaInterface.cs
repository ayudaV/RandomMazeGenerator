using System;
using System.Collections.Generic;
using System.Text;
interface IStack<Dado> // Dado indica que trataaremos classes genéricas na pilha
{
    void Empilhar(Dado elemento); // empilha elemento, que é da classe genérica Dado
    Dado Desempilhar(); // desempilha e retorna o elemento da classe Dado
                        // que está no topo da pilha
    Dado OTopo(); // retorna o elemento do topo da pilha sem removê-lo
    int Tamanho { get; } // propriedade informa quantos Dados empilhados
    bool EstaVazia { get; } // propriedade informa se a pilha está ou não vazia
}