using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.DominioTest._Util;
using System;
using Xunit;

namespace CursoOnline.DominioTest.PublicosAlvo
{
    public class ConversorDePublicoAlvoTest
    {
        private ConversorDePublicoAlvo _conversor = new ConversorDePublicoAlvo();

        [Theory]
        [InlineData(EPublicoAlvo.Empregado, "Empregado")]
        [InlineData(EPublicoAlvo.Empreendedor, "Empreendedor")]
        [InlineData(EPublicoAlvo.Estudante, "Estudante")]
        [InlineData(EPublicoAlvo.Universitário, "Universitário")]
        public void DeveConverterPublicoAlvo(EPublicoAlvo publicoAlvoEsperado, string publicoAlvoEmString)
        {
            var publicoAlvoConvertido = _conversor.Converter(publicoAlvoEmString);

            Assert.Equal(publicoAlvoEsperado, publicoAlvoConvertido);
        }

        [Fact]
        public void NaoDeveConverterQuandoPublicoAlvoInvalido()
        {
            const string publicoAlvoInvalido = "Invalido";

            Assert.Throws<ExcecaoDeDominio>(() =>
                    _conversor.Converter(publicoAlvoInvalido))
               .ComMensagem(Resource.PUBLICO_ALVO_INVALIDO); 
        }
    }
}
