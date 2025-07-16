using System;

namespace locadoraVeiculos.Entidades
    {
    public class Funcionario
        {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Cargo { get; set; }
        public DateTime DataAdmissao { get; set; }
        public string Telefone { get; set; }

        public Funcionario()
            {
            }

        public Funcionario(string nome , string cpf , string cargo , DateTime dataAdmissao , string telefone)
            {
            Nome = nome;
            Cpf = cpf;
            Cargo = cargo;
            DataAdmissao = dataAdmissao;
            Telefone = telefone;
            }
        }
    }
