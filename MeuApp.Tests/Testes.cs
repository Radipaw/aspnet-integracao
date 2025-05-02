using NUnit.Framework;
using System.Threading.Tasks;
using MeuApp.Tests;

namespace MeuApp.Tests
{
    public class Testes
    {
        [Test]
        public async Task Tables()
        {
            var tabelas = await DatabaseService.ObterTabelasAsync();

            Assert.IsNotNull(tabelas);
            Assert.IsNotEmpty(tabelas, "Nenhuma tabela foi retornada do banco.");

            foreach (var tabela in tabelas)
            {
                TestContext.WriteLine($"Tabela encontrada: {tabela}");
            }
        }

        [Test]
        public async Task WorkMuseumIdNull()
        {
            var resultado = await DatabaseService.ExecutarQueryAsync("SELECT * FROM work WHERE museum_id IS null ");

            Assert.IsNotNull(resultado);
            Assert.IsNotEmpty(resultado);

            foreach (var linha in resultado)
            {
                foreach (var coluna in linha)
                {
                    TestContext.WriteLine($"{coluna.Key}: {coluna.Value}");
                }
                TestContext.WriteLine("------------");
            }
        }

        

    }
}
