using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinaWeb.Comum.Repositorios.Entity;
using TreinaWeb.MinhaApi.AcessoDados.Entity.Context;
using TreinaWeb.MinhaApi.Dominio;

namespace TreinaWeb.MinhaApi.Repositorios.Entity
{
    public class RepositorioAlunos : RepositorioTreinaWeb<Aluno, int>
    {
        public RepositorioAlunos(MinhaApiDbContext context)    
            : base(context)
        {
        }
    }
}
