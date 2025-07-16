using locadoraVeiculos.Entidades;
using locadoraVeiculos.DAO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace locadoraVeiculos.DAO
    {
    public class FuncionarioDAO
        {
        private MySqlConnection conexao;

        public FuncionarioDAO()
            {
            conexao = new MySqlConnection("server=localhost;database=locadoraVeiculos;uid=root;pwd=root;");
            }

        public void Cadastrar(Funcionario funcionario)
            {
            try
                {
                conexao.Open();
                string sql = @"INSERT INTO Funcionario (id_fun, nome_fun, cpf_fun, cargo_fun, data_admissao_fun, telefone_fun)
                               VALUES (@id, @nome, @cpf, @cargo, @dataAdmissao, @telefone)";
                MySqlCommand cmd = new MySqlCommand(sql , conexao);
                cmd.Parameters.AddWithValue("@id" , funcionario.Id);
                cmd.Parameters.AddWithValue("@nome" , funcionario.Nome);
                cmd.Parameters.AddWithValue("@cpf" , funcionario.Cpf);
                cmd.Parameters.AddWithValue("@cargo" , funcionario.Cargo);
                cmd.Parameters.AddWithValue("@dataAdmissao" , funcionario.DataAdmissao);
                cmd.Parameters.AddWithValue("@telefone" , funcionario.Telefone);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Funcionário cadastrado com sucesso!");
                }
            catch (Exception ex)
                {
                Console.WriteLine("Erro ao cadastrar funcionário: " + ex.Message);
                }
            finally
                {
                conexao.Close();
                }
            }

        public void Atualizar(Funcionario funcionario)
            {
            try
                {
                conexao.Open();
                string sql = @"UPDATE Funcionario SET nome_fun=@nome, cpf_fun=@cpf, cargo_fun=@cargo, data_admissao_fun=@dataAdmissao, telefone_fun=@telefone WHERE id_fun=@id";
                MySqlCommand cmd = new MySqlCommand(sql , conexao);
                cmd.Parameters.AddWithValue("@nome" , funcionario.Nome);
                cmd.Parameters.AddWithValue("@cpf" , funcionario.Cpf);
                cmd.Parameters.AddWithValue("@cargo" , funcionario.Cargo);
                cmd.Parameters.AddWithValue("@dataAdmissao" , funcionario.DataAdmissao);
                cmd.Parameters.AddWithValue("@telefone" , funcionario.Telefone);
                cmd.Parameters.AddWithValue("@id" , funcionario.Id);
                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Funcionário atualizado com sucesso!" : "Funcionário não encontrado.");
                }
            catch (Exception ex)
                {
                Console.WriteLine("Erro ao atualizar funcionário: " + ex.Message);
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

                // Excluir registros dependentes em Locacao
                string sqlDeleteLocacao = "DELETE FROM Locacao WHERE id_funcionario_loc = @id";
                MySqlCommand cmdDeleteLocacao = new MySqlCommand(sqlDeleteLocacao , conexao);
                cmdDeleteLocacao.Parameters.AddWithValue("@id" , id);
                cmdDeleteLocacao.ExecuteNonQuery();

                // Excluir registros dependentes em Caixa
                string sqlDeleteCaixa = "DELETE FROM Caixa WHERE id_funcionario_cai = @id";
                MySqlCommand cmdDeleteCaixa = new MySqlCommand(sqlDeleteCaixa , conexao);
                cmdDeleteCaixa.Parameters.AddWithValue("@id" , id);
                cmdDeleteCaixa.ExecuteNonQuery();

                // Excluir o Funcionario
                string sqlDeleteFuncionario = "DELETE FROM Funcionario WHERE id_fun = @id";
                MySqlCommand cmdDeleteFuncionario = new MySqlCommand(sqlDeleteFuncionario , conexao);
                cmdDeleteFuncionario.Parameters.AddWithValue("@id" , id);
                int rows = cmdDeleteFuncionario.ExecuteNonQuery();

                Console.WriteLine(rows > 0 ? "Funcionário e registros dependentes deletados com sucesso!" : "Funcionário não encontrado.");
                }
            catch (Exception ex)
                {
                Console.WriteLine("Erro ao deletar funcionário: " + ex.Message);
                }
            finally
                {
                conexao.Close();
                }
            }

        public List<Funcionario> Listar()
            {
            List<Funcionario> lista = new List<Funcionario>();

            try
                {
                conexao.Open();
                string sql = "SELECT * FROM Funcionario";
                MySqlCommand cmd = new MySqlCommand(sql , conexao);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    {
                    Funcionario funcionario = new Funcionario
                        {
                        Id = reader.GetInt32("id_fun") ,
                        Nome = reader.GetString("nome_fun") ,
                        Cpf = reader.GetString("cpf_fun") ,
                        Cargo = reader.GetString("cargo_fun") ,
                        DataAdmissao = reader.GetDateTime("data_admissao_fun") ,
                        Telefone = reader.GetString("telefone_fun")
                        };
                    lista.Add(funcionario);
                    }
                }
            catch (Exception ex)
                {
                Console.WriteLine("Erro ao listar funcionários: " + ex.Message);
                }
            finally
                {
                conexao.Close();
                }

            return lista;
            }
        }
    }
