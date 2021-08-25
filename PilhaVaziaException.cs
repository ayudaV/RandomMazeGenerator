using System;
using System.Collections.Generic;
using System.Text;
class PilhaVaziaException : Exception
{
    public PilhaVaziaException(string mensagem) : base(mensagem)
    {
    }
}
