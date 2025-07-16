using LocalizaVeiculos.Entidades;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalizaVeiculos.DAOs
    {
    internal class ClienteDAO
        {
        private MySqlConnection conexao;

        public ClienteDAO()
            {
            conexao = new MySqlConnection("server=localhost;database=locadora;uid=root;pwd=;");
            }

        public void Cadastrar(Cliente cliente)
            {
            try
                {
                conexao.Open();
                string sql = "INSERT INTO Cliente (nome_cli, cpf_cli, rg_cli, telefone_cli, endereco_cli, cnh_cli, email_cli) " +
                             "VALUES (@nome, @cpf, @rg, @telefone, @endereco, @cnh, @email)";

                MySqlCommand cmd = new MySqlCommand(sql , conexao);
                cmd.Parameters.AddWithValue("@nome" , cliente.Nome);
                cmd.Parameters.AddWithValue("@cpf" , cliente.Cpf);
                cmd.Parameters.AddWithValue("@rg" , cliente.Rg);
                cmd.Parameters.AddWithValue("@telefone" , cliente.Telefone);
                cmd.Parameters.AddWithValue("@endereco" , cliente.Endereco);
                cmd.Parameters.AddWithValue("@cnh" , cliente.Cnh);
                cmd.Parameters.AddWithValue("@email" , cliente.Email);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Cliente cadastrado com sucesso!");
                }
            catch (Exception ex)
                {
                Console.WriteLine("Erro ao cadastrar cliente: " + ex.Message);
                }
            finally
                {
                conexao.Close();
                }
            }

        public void Atualizar(Cliente cliente)
            {
            try
                {
                conexao.Open();

                string sql = "UPDATE Cliente SET ";
                List<string> updates = new List<string>();
                if (cliente.Nome != null)
                    updates.Add("nome_cli=@nome");
                if (cliente.Cpf != null)
                    updates.Add("cpf_cli=@cpf");
                if (cliente.Rg != null)
                    updates.Add("rg_cli=@rg");
                if (cliente.Telefone != null)
                    updates.Add("telefone_cli=@telefone");
                if (cliente.Endereco != null)
                    updates.Add("endereco_cli=@endereco");
                if (cliente.Cnh != null)
                    updates.Add("cnh_cli=@cnh");
                if (cliente.Email != null)
                    updates.Add("email_cli=@email");

                sql += string.Join(", " , updates) + " WHERE id_cli=@id";

                MySqlCommand cmd = new MySqlCommand(sql , conexao);
                cmd.Parameters.AddWithValue("@id" , cliente.Id);
                if (cliente.Nome != null)
                    cmd.Parameters.AddWithValue("@nome" , cliente.Nome);
                if (cliente.Cpf != null)
                    cmd.Parameters.AddWithValue("@cpf" , cliente.Cpf);
                if (cliente.Rg != null)
                    cmd.Parameters.AddWithValue("@rg" , cliente.Rg);
                if (cliente.Telefone != null)
                    cmd.Parameters.AddWithValue("@telefone" , cliente.Telefone);
                if (cliente.Endereco != null)
                    cmd.Parameters.AddWithValue("@endereco" , cliente.Endereco);
                if (cliente.Cnh != null)
                    cmd.Parameters.AddWithValue("@cnh" , cliente.Cnh);
                if (cliente.Email != null)
                    cmd.Parameters.AddWithValue("@email" , cliente.Email);

                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Cliente atualizado com sucesso!" : "Cliente não encontrado.");
                }
            catch (Exception ex)
                {
                Console.WriteLine("Erro ao atualizar cliente: " + ex.Message);
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
                string sql = "DELETE FROM Cliente WHERE id_cli=@id";
                MySqlCommand cmd = new MySqlCommand(sql , conexao);
                cmd.Parameters.AddWithValue("@id" , id);

                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine(rows > 0 ? "Cliente deletado com sucesso!" : "Cliente não encontrado.");
                }
            catch (Exception ex)
                {
                Console.WriteLine("Erro ao deletar cliente: " + ex.Message);
                }
            finally
                {
                conexao.Close();
                }
            }

        public List<Cliente> ListarTodos()
            {
            List<Cliente> lista = new List<Cliente>();

            try
                {
                conexao.Open();
                string sql = "SELECT * FROM Cliente";
                MySqlCommand cmd = new MySqlCommand(sql , conexao);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    {
                    Cliente cliente = new Cliente
                        {
                        Id = reader.GetInt32("id_cli") ,
                        Nome = reader.GetString("nome_cli") ,
                        Cpf = reader.GetString("cpf_cli") ,
                        Rg = reader.GetString("rg_cli") ,
                        Telefone = reader.GetString("telefone_cli") ,
                        Endereco = reader.GetString("endereco_cli") ,
                        Cnh = reader.GetString("cnh_cli") ,
                        Email = reader.GetString("email_cli")
                        };
                    lista.Add(cliente);
                    }
                }
            catch (Exception ex)
                {
                Console.WriteLine("Erro ao listar clientes: " + ex.Message);
                }
            finally
                {
                conexao.Close();
                }

            return lista;
            }
        }
    }
