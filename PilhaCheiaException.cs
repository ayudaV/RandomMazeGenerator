using System;
using System.Collections.Generic;
using System.Text;
class PilhaCheiaException : Exception
{
    public PilhaCheiaException(string mensagem) : base(mensagem)
    {
    }
}
