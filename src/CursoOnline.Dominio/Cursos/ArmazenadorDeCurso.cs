﻿using CursoOnline.Dominio._Base;
using CursoOnline.Dominio.PublicosAlvo;
using System;

namespace CursoOnline.Dominio.Cursos
{
    public class ArmazenadorDeCurso
    {
        private ICursoRepositorio _cursoRepositorio;
        private IConversorDePublicoAlvo _conversorDePublicoAlvo;

        public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio, IConversorDePublicoAlvo conversorDePublicoAlvo)
        {
            _cursoRepositorio = cursoRepositorio;
            _conversorDePublicoAlvo = conversorDePublicoAlvo;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            var cursoJaSalvo = _cursoRepositorio.ObterPeloNome(cursoDto.Nome);

            ValidadorDeRegra.Novo()
                .Quando(cursoJaSalvo != null && cursoJaSalvo.Id != cursoDto.Id, Resource.NOME_CURSO_EXISTENTE)
                //.Quando(!Enum.TryParse<EPublicoAlvo>(cursoDto.PublicoAlvo, out var publicoAlvo), Resource.PUBLICO_ALVO_INVALIDO)
                .DispararExcecaoSeExistir();

            var publicoAlvo = _conversorDePublicoAlvo.Converter(cursoDto.PublicoAlvo);

            var curso = new Curso(cursoDto.Nome, cursoDto.Descricao, cursoDto.CargaHoraria,
                (EPublicoAlvo)publicoAlvo, cursoDto.Valor);

            if(cursoDto.Id > 0)
            {
                curso = _cursoRepositorio.ObterPorId(cursoDto.Id);
                curso.AlterarNome(cursoDto.Nome);
                curso.AlterarValor(cursoDto.Valor);
                curso.AlterarCargaHoraria(cursoDto.CargaHoraria);
            }

            if(cursoDto.Id == 0)    
                _cursoRepositorio.Adicionar(curso);
        }
    }


}
