using System;

namespace DapperTransactionWithUoW.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string SobreNome { get; set; }

        public string Empresa { get; set; }

        public DateTime DataNascimento { get; set; }

        public bool Ativo { get; set; }

        public decimal Valor { get; set; }

    }
}
