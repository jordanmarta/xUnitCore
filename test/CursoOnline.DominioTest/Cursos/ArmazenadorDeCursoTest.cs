using Bogus;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using Moq;
using System;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private readonly CursoDto _cursoDto;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;
        private Mock<IConversorDePublicoAlvo> _conversorDePublicoAlvo;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;

        public ArmazenadorDeCursoTest()
        {
            var fake = new Faker();

            _cursoDto = new CursoDto
            {
                Nome = fake.Random.Words(),
                Descricao = fake.Lorem.Paragraph(),
                CargaHoraria = fake.Random.Double(50, 1000),
                PublicoAlvo = "Estudante",
                Valor = fake.Random.Double(1000, 2000)
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _conversorDePublicoAlvo = new Mock<IConversorDePublicoAlvo>();
            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object, _conversorDePublicoAlvo.Object);
        }

        // Método usando mock
        // mock = utilizado para verificar alguma coisa
        [Fact]
        public void DeveAdicionarCurso()
        {
            _armazenadorDeCurso.Armazenar(_cursoDto);

            // Verify é similar ao Assert
            _cursoRepositorioMock.Verify(r => r.Adicionar(
                It.Is<Curso>(
                    c => c.Nome == _cursoDto.Nome &&
                    c.Descricao == _cursoDto.Descricao
                )
            ));
        }

        // Método usando stub
        //stub = utilizado somente para setupear/dar comportamento
        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {

            var cursoJaSalvo = CursoBuilder.Novo().ComId(323).ComNome(_cursoDto.Nome).Build();
            _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem(Resource.NOME_CURSO_EXISTENTE);
        }

        [Fact]
        public void DeveAlterarDadosDoCurso()
        {
            _cursoDto.Id = 323;
            var curso = CursoBuilder.Novo().Build();
            _cursoRepositorioMock.Setup(r => r.ObterPorId(_cursoDto.Id)).Returns(curso);

            _armazenadorDeCurso.Armazenar(_cursoDto);

            Assert.Equal(_cursoDto.Nome, curso.Nome);
            Assert.Equal(_cursoDto.Valor, curso.Valor);
            Assert.Equal(_cursoDto.CargaHoraria, curso.CargaHoraria);
        }

        [Fact]
        public void NaoDeveAdicionarNoRepositorioQuandoCursoJaExiste()
        {
            _cursoDto.Id = 323;
            var curso = CursoBuilder.Novo().Build();
            _cursoRepositorioMock.Setup(r => r.ObterPorId(_cursoDto.Id)).Returns(curso);

            _armazenadorDeCurso.Armazenar(_cursoDto);

            _cursoRepositorioMock.Verify(r => r.Adicionar(It.IsAny<Curso>()), Times.Never);
        }
    }
 }
