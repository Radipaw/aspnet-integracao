using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Npgsql;

namespace MeuApp.Tests
{
    public static class DatabaseService
    {

        private const string ConnectionString = "Host=trivially-integrated-phoebe.data-1.use1.tembo.io;Port=5432;Username=postgres;Password=lpnkh21ndlAIEuLl;Database=famous_paintings;SSL Mode=Require;Trust Server Certificate=true";


        public static async Task<List<string>> ObterTabelasAsync()
        {
            var tabelas = new List<string>();
            await using var conn = new NpgsqlConnection(ConnectionString);
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand(@"
                SELECT table_name
                FROM information_schema.tables
                WHERE table_schema = 'public'
                ORDER BY table_name;
            ", conn);

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                tabelas.Add(reader.GetString(0));
            }

            return tabelas;
        }

        
        public static async Task<List<Dictionary<string, object>>> ExecutarQueryAsync(string query)
        {
            var resultados = new List<Dictionary<string, object>>();
            await using var conn = new NpgsqlConnection(ConnectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var row = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = await reader.IsDBNullAsync(i) ? null : reader.GetValue(i);
                }

                resultados.Add(row);
            }

            return resultados;
        }
    }
}
