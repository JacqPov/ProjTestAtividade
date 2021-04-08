using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjAtividade.API.Model;

namespace ProjAtividade.API.Data
{
    public class AtividadesContext : DbContext
    {
        public AtividadesContext(DbContextOptions<AtividadesContext> options)
           : base(options)
        {
        }

        public DbSet<Atividade> Atividade { get; set; }
        public DbSet<Responsavel> Responsavel { get; set; }
    }
}
