using Bogus;
using Bogus.Extensions.Brazil;
using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.Alunos;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.PublicosAlvo;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Util;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    public class ArmazenadorDeAlunoTest
    {
        private readonly Faker _faker;
        private readonly AlunoDto _alunoDto;
        private readonly ArmazenadorDeAluno _armazenadorDeAluno;
        private readonly Mock<IAlunoRepositorio> _alunoRepositorio;
        private Mock<IConversorDePublicoAlvo> _conversorDePublicoAlvo;

        public ArmazenadorDeAlunoTest()
        {
            _faker = new Faker();
            _alunoDto = new AlunoDto
            {
                Nome = _faker.Person.FullName,
                Email = _faker.Person.Email,
                Cpf = _faker.Person.Cpf(),
                PublicoAlvo = EPublicoAlvo.Empregado.ToString(),
            };

            _alunoRepositorio = new Mock<IAlunoRepositorio>();
            _conversorDePublicoAlvo = new Mock<IConversorDePublicoAlvo>();
            _armazenadorDeAluno = new ArmazenadorDeAluno(_alunoRepositorio.Object, _conversorDePublicoAlvo.Object);
        }


        [Fact]
        public void DeveAdicionarAluno()
        {
            _armazenadorDeAluno.Armazenar(_alunoDto);

            _alunoRepositorio.Verify(r => r.Adicionar(It.Is<Aluno>(c => c.Nome == _alunoDto.Nome)));
        }


        [Fact]
        public void NaoDeveAdicionarAlunoQuandoCpfJaFoiCadastrado()
        {
            var alunoComMesmoCpf = AlunoBuilder.Novo().ComId(34).Build();

            _alunoRepositorio.Setup(r => r.ObterPeloCpf(_alunoDto.Cpf)).Returns(alunoComMesmoCpf);

            Assert.Throws<ExcecaoDeDominio>(() => _armazenadorDeAluno.Armazenar(_alunoDto))
                .ComMensagem(Resource.CPF_EXISTENTE);
        }


        [Fact]
        public void DeveEditarNomeAluno()
        {
            _alunoDto.Id = 35;
            _alunoDto.Nome = _faker.Person.FullName;

            var alunoJaSalvo = AlunoBuilder.Novo().Build();
            _alunoRepositorio.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(alunoJaSalvo);

            _armazenadorDeAluno.Armazenar(_alunoDto);

            Assert.Equal(_alunoDto.Nome, alunoJaSalvo.Nome);
        }

        [Fact]
        public void NaoDeveEditarDemaisInformacoes()
        {
            _alunoDto.Id = 35;
            var alunoJaSalvo = AlunoBuilder.Novo().Build();
            var cpfEsperado = alunoJaSalvo.Cpf;

            _alunoRepositorio.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(alunoJaSalvo);
            _armazenadorDeAluno.Armazenar(_alunoDto);

            Assert.Equal(cpfEsperado, alunoJaSalvo.Cpf);
        }

        [Fact]
        public void NaoDeveAdicionarQuandoForEdicao()
        {
            _alunoDto.Id = 35;
            var alunoJaSalvo = AlunoBuilder.Novo().Build();
            _alunoRepositorio.Setup(r => r.ObterPorId(_alunoDto.Id)).Returns(alunoJaSalvo);

            _armazenadorDeAluno.Armazenar(_alunoDto);

            _alunoRepositorio.Verify(r => r.Adicionar(It.IsAny<Aluno>()), Times.Never);
        }
    }
}
