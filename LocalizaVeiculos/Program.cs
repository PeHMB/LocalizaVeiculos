using System;
using locadoraVeiculos.DAO;

namespace locadoraVeiculos
    {
    class Program
        {
        static void Main(string[] args)
            {
            LocacaoDAO locacaoDAO = new LocacaoDAO();

            // 1. Cadastrar nova locação
            Locacao novaLocacao = new Locacao
                {
                Id = 11 ,
                DataInicio = DateTime.Today ,
                DataFimPrevista = DateTime.Today.AddDays(5) ,
                DataFimReal = null ,
                ValorDiaria = 120.50m ,
                ValorTotal = null ,
                Situacao = "Reservada" ,
                IdCliente = 1 ,
                IdVeiculo = 3 ,
                IdFuncionario = 2
                };
            locacaoDAO.Cadastrar(novaLocacao);

            // 2. Listar todas as locações
            Console.WriteLine("\nLista de Locacoes:");
            foreach (var loc in locacaoDAO.Listar())
                {
                Console.WriteLine($"ID: {loc.Id}, Cliente: {loc.IdCliente}, Veiculo: {loc.IdVeiculo}, Situação: {loc.Situacao}, Início: {loc.DataInicio.ToShortDateString()}");
                }

            // 3. Atualizar locação
            novaLocacao.Situacao = "Em andamento";
            novaLocacao.ValorTotal = 602.50m;
            locacaoDAO.Atualizar(novaLocacao);

            // 4. Deletar locação cadastrada
            locacaoDAO.Deletar(novaLocacao.Id);

            Console.WriteLine("\nTeste de LocacaoDAO finalizado.");
            }
        }
    }
