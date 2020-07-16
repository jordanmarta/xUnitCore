using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoOnline.Dominio.PublicosAlvo
{
    public class ConversorDePublicoAlvo : IConversorDePublicoAlvo
    {
        public EPublicoAlvo Converter(string publicoAlvo)
        {
            ValidadorDeRegra.Novo()
                .Quando(!Enum.TryParse<EPublicoAlvo>(publicoAlvo, out var publicoAlvoConvertido), Resource.PUBLICO_ALVO_INVALIDO)
                .DispararExcecaoSeExistir();

            return publicoAlvoConvertido;
        }
    }
}
