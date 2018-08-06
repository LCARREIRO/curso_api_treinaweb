using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TreinaWeb.Comum.Repositorios.Interfaces;
using TreinaWeb.MinhaApi.AcessoDados.Entity.Context;
using TreinaWeb.MinhaApi.Api.AutoMapper;
using TreinaWeb.MinhaApi.Api.DTOs;
using TreinaWeb.MinhaApi.Dominio;
using TreinaWeb.MinhaApi.Repositorios.Entity;

namespace TreinaWeb.MinhaApi.Api.Controllers
{
    public class AlunosController : ApiController
    {
        private IRepositorioTreinaWeb<Aluno, int> _repositorioAlunos
            = new RepositorioAlunos(new MinhaApiDbContext());

        /*
         * VERSÃO RECOMENDADA - Web API 2 - IHttpActionResult
         */

        public IHttpActionResult Get()
        {
            List<Aluno> alunos = _repositorioAlunos.Selecionar();
            List<AlunoDTO> dtos = AutoMapperManager.Instance.Mapper.Map<List<Aluno>, List<AlunoDTO>>(alunos);

            return Ok(dtos);
        }

        public IHttpActionResult Get(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            Aluno aluno = _repositorioAlunos.SelecionarPorId(id.Value);
            
            if (aluno == null)
            {
                return NotFound();
            }

            AlunoDTO dto = AutoMapperManager.Instance.Mapper.Map<Aluno, AlunoDTO>(aluno);

            return Content(HttpStatusCode.Found, dto);
        }

        public IHttpActionResult Post([FromBody] AlunoDTO dto)
        {
            if(!ModelState.IsValid)
            {
                try
                {
                    Aluno aluno = AutoMapperManager.Instance.Mapper.Map<AlunoDTO, Aluno>(dto);

                    _repositorioAlunos.Inserir(aluno);
                    return Created($"{Request.RequestUri}/{aluno.Id}", aluno);
                }
                catch (Exception ex)
                {

                    return InternalServerError(ex);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        public IHttpActionResult Put(int? id, [FromBody] Aluno aluno)
        {
            try
            {
                if (!id.HasValue)
                {
                    return BadRequest();
                }
                aluno.Id = id.Value;
                _repositorioAlunos.Atualizar(aluno);

                return Ok();

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult Delete(int? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    return BadRequest();
                }
                Aluno aluno = _repositorioAlunos.SelecionarPorId(id.Value);
                if (aluno == null)
                {
                    return NotFound();
                }
                _repositorioAlunos.ExcluirPorId(id.Value);

                return Ok();

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        /*VERSÃO ANTIGA - Web API 1 - HttpResponseMessage (CÓDIGO MAIS VERBOSO)
        
        public IEnumerable<Aluno> Get()
        {
            return _repositorioAlunos.Selecionar();
        }

        public HttpResponseMessage Get(int? id)
        {
            if(!id.HasValue)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            Aluno aluno =  _repositorioAlunos.SelecionarPorId(id.Value);

            if(aluno == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.Found, aluno);
        }

        public HttpResponseMessage Post([FromBody] Aluno aluno)
        {
            try
            {
                _repositorioAlunos.Inserir(aluno);

                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message );
            }           
        }
        */
    }
}
