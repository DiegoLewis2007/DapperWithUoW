using Dapper;
using DapperTransactionWithUoW.Domain.Entities;
using DapperTransactionWithUoW.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;

namespace DapperTransactionWithUoW.Data.Repositories
{
    public sealed class UserRepository : CockpitContext, IRepository
    {
        public UserRepository(IConfiguration configuration,IDbTransaction transaction = null)
            : base(configuration, transaction)
        {
        }

        public void Add(User entity)
        {
            this.Connection.Execute("Insert into [dbo].[Tabela] ([Nome],[SobreNome],[DataNascimento],[ativo],[Valor],[Empresa]) VALUES (@Nome,@SobreNome,@DataNascimento,@Ativo,@Valor,@Empresa)", param: entity, transaction: Transaction);
        }

        public IEnumerable<User> GetAll()
        {
            return base.Connection.Query<User>("Select [Nome],[SobreNome],[DataNascimento],[ativo],[Valor],[Empresa] from [dbo].[Tabela]");
        }

        public void Update(User entity)
        {
            this.Connection.Execute("UPDATE [dbo].[Tabela] set[Nome] = @Nome, [SobreNome] = @SobreNome, [ativo] = @Ativo where  Empresa = @Empresa", param: entity, transaction: Transaction);
        }
    }
}
