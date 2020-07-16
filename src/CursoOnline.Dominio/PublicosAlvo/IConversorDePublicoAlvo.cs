using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.PublicosAlvo
{
    public interface IConversorDePublicoAlvo
    {
        EPublicoAlvo Converter(string publicoAlvo);
    }
}
