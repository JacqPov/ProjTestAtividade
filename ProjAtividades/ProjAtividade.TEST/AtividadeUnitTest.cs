using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ProjAtividade.API.Controllers;
using ProjAtividade.API.Data;
using ProjAtividade.API.Model;
using Xunit;

namespace ProjAtividade.TEST
{
    public class AtividadeUnitTest
    {
        private DbContextOptions<AtividadesContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<AtividadesContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new AtividadesContext(options))
            {
                context.Atividade.Add(new Atividade { Id = 1, Descricao = "Descrição 1", DataInicio = DateTime.ParseExact("04/07/2021", "d", CultureInfo.InvariantCulture), DataFim = DateTime.ParseExact("04/10/2021", "d", CultureInfo.InvariantCulture) });
                context.Atividade.Add(new Atividade { Id = 2, Descricao = "Descrição 2", DataInicio = DateTime.ParseExact("04/09/2021", "d", CultureInfo.InvariantCulture), DataFim = DateTime.ParseExact("04/12/2021", "d", CultureInfo.InvariantCulture) });
                context.Atividade.Add(new Atividade { Id = 3, Descricao = "Descrição 3", DataInicio = DateTime.ParseExact("04/11/2021", "d", CultureInfo.InvariantCulture), DataFim = DateTime.ParseExact("04/14/2021", "d", CultureInfo.InvariantCulture) });
                context.SaveChanges();
            }

            using (var context = new AtividadesContext(options))
            {
                context.Responsavel.Add(new Responsavel { Id = 1, Nome = "José"});
                context.Responsavel.Add(new Responsavel { Id = 2, Nome = "Maria"});
                context.Responsavel.Add(new Responsavel { Id = 3, Nome = "João"});
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AtividadesContext(options))
            {
                AtividadeController atividadeController = new AtividadeController(context);
                IEnumerable<Atividade> atividades = atividadeController.GetAtividade().Result.Value;

                Assert.Equal(3, atividades.Count());
            }

            using (var context = new AtividadesContext(options))
            {
                ResponsavelController responsavelController = new ResponsavelController(context);
                IEnumerable<Responsavel> responsaveis = responsavelController.GetResponsavel().Result.Value;

                Assert.Equal(3, responsaveis.Count());
            }

        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AtividadesContext(options))
            {
                int atividadeId = 2;
                AtividadeController atividadeController = new AtividadeController(context);
                Atividade ativ = atividadeController.GetAtividade(atividadeId).Result.Value;
                Assert.Equal(2, ativ.Id);
            }

            using (var context = new AtividadesContext(options))
            {
                int responsavelId = 2;
                ResponsavelController responsavelController = new ResponsavelController(context);
                Responsavel respon = responsavelController.GetResponsavel(responsavelId).Result.Value;
                Assert.Equal(2, respon.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            Atividade atividade = new Atividade()
            {
                Id = 4,
                Descricao = "Descrição 4",
                DataInicio = DateTime.ParseExact("04/12/2021", "d", CultureInfo.InvariantCulture),
                DataFim = DateTime.ParseExact("04/15/2021", "d", CultureInfo.InvariantCulture)
            };

            // Use a clean instance of the context to run the test
            using (var context = new AtividadesContext(options))
            {
                AtividadeController atividadeController = new AtividadeController(context);
                Atividade ativ = atividadeController.PostAtividade(atividade).Result.Value;
                Assert.Equal(4, ativ.Id);
            }

            Responsavel responsavel = new Responsavel()
            {
                Id = 4,
                Nome = "Jorge"
               
            };

            // Use a clean instance of the context to run the test
            using (var context = new AtividadesContext(options))
            {
                ResponsavelController responsavelController = new ResponsavelController(context);
                Responsavel respon = responsavelController.PostResponsavel(responsavel).Result.Value;
                Assert.Equal(4, respon.Id);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            Atividade atividade = new Atividade()
            {
                Id = 3,
                Descricao = "Descrição 3",
                DataInicio = DateTime.ParseExact("04/11/2021", "d", CultureInfo.InvariantCulture),
                DataFim = DateTime.ParseExact("04/14/2021", "d", CultureInfo.InvariantCulture)
            };

            // Use a clean instance of the context to run the test
            using (var context = new AtividadesContext(options))
            {
                AtividadeController atividadeController = new AtividadeController(context);
                Atividade ativ = atividadeController.PutAtividade(3, atividade).Result.Value;
                Assert.Equal("Descrição 3", ativ.Descricao);
            }

            Responsavel responsavel = new Responsavel()
            {
                Id = 3,
                Nome = "João"
               
            };

            // Use a clean instance of the context to run the test
            using (var context = new AtividadesContext(options))
            {
                ResponsavelController responsavelController = new ResponsavelController(context);
                Responsavel respon = responsavelController.PutResponsavel(3, responsavel).Result.Value;
                Assert.Equal("João", respon.Nome);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new AtividadesContext(options))
            {
                AtividadeController atividadeController = new AtividadeController(context);
                Atividade atividade = atividadeController.DeleteAtividade(2).Result.Value;
                Assert.Null(atividade);
            }

            using (var context = new AtividadesContext(options))
            {
                ResponsavelController responsavelController = new ResponsavelController(context);
                Responsavel responsavel = responsavelController.DeleteResponsavel(2).Result.Value;
                Assert.Null(responsavel);
            }
        }
    }
}
