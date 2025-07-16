using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace locadoraVeiculos.DAO
    {
    public class LocacaoDAO
        {
        private MySqlConnection conexao;

        public LocacaoDAO()
            {
            conexao = new MySqlConnection("server=localhost;database=locadoraVeiculos;uid=root;pwd=root;");
            }

        public void Cadastrar(Locacao locacao)
            {
            try
                {
                conexao.Open();
                string sql = @"INSERT INTO Locacao (id_loc, data_inicio_loc, data_fim_prevista_loc, data_fim_real_loc,
                               valor_diaria_loc, valor_total_loc, situacao_loc, id_cliente_loc, id_veiculo_loc, id_funcionario_loc)
                               VALUES (@id, @dataInicio, @dataFimPrevista, @dataFimReal, @valorDiaria, @valorTotal,
                               @situacao, @idCliente, @idVeiculo, @idFuncionario)";
                MySqlCommand cmd = new MySqlCommand(sql , conexao);
                cmd.Parameters.AddWithValue("@id" , locacao.Id);
                cmd.Parameters.AddWithValue("@dataInicio" , locacao.DataInicio);
                cmd.Parameters.AddWithValue("@dataFimPrevista" , locacao.DataFimPrevista);
                cmd.Parameters.AddWithValue("@dataFimReal" , locacao.DataFimReal);
                cmd.Parameters.AddWithValue("@valorDiaria" , locacao.ValorDiaria);
                cmd.Parameters.AddWithValue("@valorTotal" , locacao.ValorTotal);
                cmd.Parameters.AddWithValue("@situacao" , locacao.Situacao);
                cmd.Parameters.AddWithValue("@idCliente" , locacao.IdCliente);
                cmd.Parameters.AddWithValue("@idVeiculo" , locacao.IdVeiculo);
                cmd.Parameters.AddWithValue("@idFuncionario" , locacao.IdFuncionario);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Locação cadastrada com sucesso!");
                }
            catch (Exception ex)
                {
                Console.WriteLine("Erro ao cadastrar locação: " + ex.Message);
                }
            finally
                {
                conexao.Close();
                }
            }

        public void Atualizar(Locacao locacao)
            {
            try
                {
                conexao.Open();
                string sql = @"UPDATE Locacao SET data_inicio_loc=@dataInicio, data_fim_prevista_loc=@dataFimPrevista,
                               data_fim_real_loc=@dataFimReal, valor_diaria_loc=@valorDiaria, valor_total_loc=@valorTotal,
                               situacao_loc=@situacao, id_cliente_loc=@idCliente, id_veiculo_loc=@idVeiculo,
                               id_funcionario_loc=@idFuncionario WHERE id_loc=@id";
                MySqlCommand cmd = new MySqlCommand(sql , conexao);
                cmd.Parameters.AddWithValue("@dataInicio" , locacao.DataInicio);
                cmd.Parameters.AddWithValue("@dataFimPrevista" , locacao.DataFimPrevista);
                cmd.Parameters.AddWithValue("@dataFimReal" , locacao.DataFimReal);
                cmd.Parameters.AddWithValue("@valorDiaria" , locacao.ValorDiaria);
                cmd.Parameters.AddWithValue("@valorTotal" , locacao.ValorTotal);
                cmd.Parameters.AddWithValue("@situacao" , locacao.Situacao);
                cmd.Parameters.AddWithValue("@idCliente" , locacao.IdCliente);
                cmd.Parameters.AddWithValue("@idVeiculo" , locacao.IdVeiculo);
                cmd.Parameters.AddWithValue("@idFuncionario" , locacao.IdFuncionario);
                cmd.Parameters.AddWithValue("@id" , locacao.Id);
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Locação atualizada com sucesso!" : "Locação não encontrada.");
                }
            catch (Exception ex)
                {
                Console.WriteLine("Erro ao atualizar locação: " + ex.Message);
                }
            finally
                {
                conexao.Close();
                }
            }

        public void Deletar(int id)
            {
            try
                {
                conexao.Open();

                // Excluir registros dependentes em Locacao_Veiculo
                string sqlDeleteLocacaoVeiculo = "DELETE FROM Locacao_Veiculo WHERE id_locacao_lv = @id";
                MySqlCommand cmdDeleteLocacaoVeiculo = new MySqlCommand(sqlDeleteLocacaoVeiculo , conexao);
                cmdDeleteLocacaoVeiculo.Parameters.AddWithValue("@id" , id);
                cmdDeleteLocacaoVeiculo.ExecuteNonQuery();

                // Excluir registros dependentes em Recebimento
                string sqlDeleteRecebimento = "DELETE FROM Recebimento WHERE id_locacao_rec = @id";
                MySqlCommand cmdDeleteRecebimento = new MySqlCommand(sqlDeleteRecebimento , conexao);
                cmdDeleteRecebimento.Parameters.AddWithValue("@id" , id);
                cmdDeleteRecebimento.ExecuteNonQuery();

                // Excluir Locacao
                string sqlDeleteLocacao = "DELETE FROM Locacao WHERE id_loc = @id";
                MySqlCommand cmdDeleteLocacao = new MySqlCommand(sqlDeleteLocacao , conexao);
                cmdDeleteLocacao.Parameters.AddWithValue("@id" , id);
                int rows = cmdDeleteLocacao.ExecuteNonQuery();

                Console.WriteLine(rows > 0 ? "Locação e registros dependentes deletados com sucesso!" : "Locação não encontrada.");
                }
            catch (Exception ex)
                {
                Console.WriteLine("Erro ao deletar locação: " + ex.Message);
                }
            finally
                {
                conexao.Close();
                }
            }

        public List<Locacao> Listar()
            {
            List<Locacao> lista = new List<Locacao>();

            try
                {
                conexao.Open();
                string sql = "SELECT * FROM Locacao";
                MySqlCommand cmd = new MySqlCommand(sql , conexao);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    {
                    Locacao locacao = new Locacao
                        {
                        Id = reader.GetInt32("id_loc") ,
                        DataInicio = reader.GetDateTime("data_inicio_loc") ,
                        DataFimPrevista = reader.IsDBNull(reader.GetOrdinal("data_fim_prevista_loc")) ? (DateTime?) null : reader.GetDateTime("data_fim_prevista_loc") ,
                        DataFimReal = reader.IsDBNull(reader.GetOrdinal("data_fim_real_loc")) ? (DateTime?) null : reader.GetDateTime("data_fim_real_loc") ,
                        ValorDiaria = reader.GetDecimal("valor_diaria_loc") ,
                        ValorTotal = reader.IsDBNull(reader.GetOrdinal("valor_total_loc")) ? (decimal?) null : reader.GetDecimal("valor_total_loc") ,
                        Situacao = reader.GetString("situacao_loc") ,
                        IdCliente = reader.GetInt32("id_cliente_loc") ,
                        IdVeiculo = reader.GetInt32("id_veiculo_loc") ,
                        IdFuncionario = reader.GetInt32("id_funcionario_loc")
                        };
                    lista.Add(locacao);
                    }
                }
            catch (Exception ex)
                {
                Console.WriteLine("Erro ao listar locações: " + ex.Message);
                }
            finally
                {
                conexao.Close();
                }

            return lista;
            }
        }
    }
