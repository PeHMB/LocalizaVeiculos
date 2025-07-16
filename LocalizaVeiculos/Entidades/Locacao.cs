using System;

namespace locadoraVeiculos.DAO
    {
    public class Locacao
        {
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFimPrevista { get; set; }
        public DateTime? DataFimReal { get; set; }
        public decimal ValorDiaria { get; set; }
        public decimal? ValorTotal { get; set; }
        public string Situacao { get; set; }
        public int IdCliente { get; set; }
        public int IdVeiculo { get; set; }
        public int IdFuncionario { get; set; }

        public Locacao()
            {
            }

        public Locacao(DateTime dataInicio , DateTime? dataFimPrevista , DateTime? dataFimReal , decimal valorDiaria ,
                       decimal? valorTotal , string situacao , int idCliente , int idVeiculo , int idFuncionario)
            {
            DataInicio = dataInicio;
            DataFimPrevista = dataFimPrevista;
            DataFimReal = dataFimReal;
            ValorDiaria = valorDiaria;
            ValorTotal = valorTotal;
            Situacao = situacao;
            IdCliente = idCliente;
            IdVeiculo = idVeiculo;
            IdFuncionario = idFuncionario;
            }
        }
    }
