using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreinaWeb.MinhaApi.Dominio;

namespace TreinaWeb.MinhaApi.AcessoDados.Entity.Context
{
    public class MinhaApiDbContext : DbContext
    {
        public DbSet<Aluno> Alunos { get; set; }

        public MinhaApiDbContext()
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
    }
}
