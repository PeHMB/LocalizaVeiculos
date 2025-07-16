using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaVeiculos.Entidades
    {
    internal class Cliente
        {
        private int _id;
        public int Id
            {
            get { return _id; }
            set { _id = value; }
            }

        private string _nome;
        public string Nome
            {
            get { return _nome; }
            set { _nome = value; }
            }

        private string _cpf;
        public string Cpf
            {
            get { return _cpf; }
            set
                {
                string cpfLimpo = value.Replace("-" , "").Replace("." , "");
                if (cpfLimpo.Length != 11)
                    {
                    throw new Exception("CPF inválido");
                    }
                _cpf = value;
                }
            }

        private string _rg;
        public string Rg
            {
            get { return _rg; }
            set { _rg = value; }
            }

        private string _telefone;
        public string Telefone
            {
            get { return _telefone; }
            set { _telefone = value; }
            }

        private string _endereco;
        public string Endereco
            {
            get { return _endereco; }
            set { _endereco = value; }
            }

        private string _cnh;
        public string Cnh
            {
            get { return _cnh; }
            set { _cnh = value; }
            }

        private string _email;
        public string Email
            {
            get { return _email; }
            set { _email = value; }
            }

        // Construtor vazio
        public Cliente()
            {

            }

        // Construtor completo
        public Cliente(string nome , string cpf , string rg , string telefone , string endereco , string cnh , string email)
            {
            Nome = nome;
            Cpf = cpf;
            Rg = rg;
            Telefone = telefone;
            Endereco = endereco;
            Cnh = cnh;
            Email = email;
            }


        }
    }