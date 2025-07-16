using locadoraVeiculos.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace locadoraVeiculos.DAO
    {
    public class VeiculoDAO
        {
        private MySqlConnection conexao;

        public VeiculoDAO()
            {
            conexao = new MySqlConnection("server=localhost;database=locadoraVeiculos;uid=root;pwd=root;");
            }

        public void Cadastrar(Veiculo veiculo)
            {
            try
                {
                conexao.Open();
                string sql = @"INSERT INTO Veiculo (placa_vei, renavam_vei, chassi_vei, modelo_vei, marca_vei, ano_vei, cor_vei, quilometragem_vei, status_vei)
                               VALUES (@placa, @renavam, @chassi, @modelo, @marca, @ano, @cor, @quilometragem, @status)";
                MySqlCommand cmd = new MySqlCommand(sql , conexao);
                cmd.Parameters.AddWithValue("@placa" , veiculo.Placa);
                cmd.Parameters.AddWithValue("@renavam" , veiculo.Renavam);
                cmd.Parameters.AddWithValue("@chassi" , veiculo.Chassi);
                cmd.Parameters.AddWithValue("@modelo" , veiculo.Modelo);
                cmd.Parameters.AddWithValue("@marca" , veiculo.Marca);
                cmd.Parameters.AddWithValue("@ano" , veiculo.Ano);
                cmd.Parameters.AddWithValue("@cor" , veiculo.Cor);
                cmd.Parameters.AddWithValue("@quilometragem" , veiculo.Quilometragem);
                cmd.Parameters.AddWithValue("@status" , veiculo.Status);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Veículo cadastrado com sucesso!");
                }
            catch (Exception ex)
                {
                Console.WriteLine("Erro ao cadastrar veículo: " + ex.Message);
                }
            finally
                {
                conexao.Close();
                }
            }

        public void Atualizar(Veiculo veiculo)
            {
            try
                {
                conexao.Open();
                string sql = @"UPDATE Veiculo SET placa_vei=@placa, renavam_vei=@renavam, chassi_vei=@chassi, modelo_vei=@modelo, marca_vei=@marca, ano_vei=@ano, cor_vei=@cor, quilometragem_vei=@quilometragem, status_vei=@status WHERE id_vei=@id";
                MySqlCommand cmd = new MySqlCommand(sql , conexao);
                cmd.Parameters.AddWithValue("@placa" , veiculo.Placa);
                cmd.Parameters.AddWithValue("@renavam" , veiculo.Renavam);
                cmd.Parameters.AddWithValue("@chassi" , veiculo.Chassi);
                cmd.Parameters.AddWithValue("@modelo" , veiculo.Modelo);
                cmd.Parameters.AddWithValue("@marca" , veiculo.Marca);
                cmd.Parameters.AddWithValue("@ano" , veiculo.Ano);
                cmd.Parameters.AddWithValue("@cor" , veiculo.Cor);
                cmd.Parameters.AddWithValue("@quilometragem" , veiculo.Quilometragem);
                cmd.Parameters.AddWithValue("@status" , veiculo.Status);
                cmd.Parameters.AddWithValue("@id" , veiculo.Id);
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Veículo atualizado com sucesso!" : "Veículo não encontrado.");
                }
            catch (Exception ex)
                {
                Console.WriteLine("Erro ao atualizar veículo: " + ex.Message);
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
                string sqlDeleteLocacaoVeiculo = "DELETE FROM Locacao_Veiculo WHERE id_veiculo_lv = @id";
                MySqlCommand cmdDeleteLocacaoVeiculo = new MySqlCommand(sqlDeleteLocacaoVeiculo , conexao);
                cmdDeleteLocacaoVeiculo.Parameters.AddWithValue("@id" , id);
                cmdDeleteLocacaoVeiculo.ExecuteNonQuery();

                // Excluir registros dependentes em Locacao
                string sqlDeleteLocacao = "DELETE FROM Locacao WHERE id_veiculo_loc = @id";
                MySqlCommand cmdDeleteLocacao = new MySqlCommand(sqlDeleteLocacao , conexao);
                cmdDeleteLocacao.Parameters.AddWithValue("@id" , id);
                cmdDeleteLocacao.ExecuteNonQuery();

                // Excluir registros dependentes em Manutencao
                string sqlDeleteManutencao = "DELETE FROM Manutencao WHERE id_veiculo_man = @id";
                MySqlCommand cmdDeleteManutencao = new MySqlCommand(sqlDeleteManutencao , conexao);
                cmdDeleteManutencao.Parameters.AddWithValue("@id" , id);
                cmdDeleteManutencao.ExecuteNonQuery();

                // Excluir o Veiculo
                string sqlDeleteVeiculo = "DELETE FROM Veiculo WHERE id_vei = @id";
                MySqlCommand cmdDeleteVeiculo = new MySqlCommand(sqlDeleteVeiculo , conexao);
                cmdDeleteVeiculo.Parameters.AddWithValue("@id" , id);
                int rows = cmdDeleteVeiculo.ExecuteNonQuery();

                Console.WriteLine(rows > 0 ? "Veículo e registros dependentes deletados com sucesso!" : "Veículo não encontrado.");
                }
            catch (Exception ex)
                {
                Console.WriteLine("Erro ao deletar veículo: " + ex.Message);
                }
            finally
                {
                conexao.Close();
                }
            }


        public List<Veiculo> Listar()
            {
            List<Veiculo> lista = new List<Veiculo>();

            try
                {
                conexao.Open();
                string sql = "SELECT * FROM Veiculo";
                MySqlCommand cmd = new MySqlCommand(sql , conexao);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    {
                    Veiculo veiculo = new Veiculo
                        {
                        Id = reader.GetInt32("id_vei") ,
                        Placa = reader.GetString("placa_vei") ,
                        Renavam = reader.GetString("renavam_vei") ,
                        Chassi = reader.GetString("chassi_vei") ,
                        Modelo = reader.GetString("modelo_vei") ,
                        Marca = reader.GetString("marca_vei") ,
                        Ano = reader.GetInt32("ano_vei") ,
                        Cor = reader.GetString("cor_vei") ,
                        Quilometragem = reader.GetInt32("quilometragem_vei") ,
                        Status = reader.GetString("status_vei")
                        };
                    lista.Add(veiculo);
                    }
                }
            catch (Exception ex)
                {
                Console.WriteLine("Erro ao listar veículos: " + ex.Message);
                }
            finally
                {
                conexao.Close();
                }

            return lista;
            }
        }
    }
