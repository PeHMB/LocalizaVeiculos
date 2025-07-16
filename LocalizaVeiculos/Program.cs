using LocalizaVeiculos.DAOs;
using LocalizaVeiculos.Entidades;
ClienteDAO dao = new ClienteDAO();

// Teste de cadastro
Cliente novo = new Cliente("Maria Souza" , "987.654.321-00" , "7654321" , "98888-8888" , "Rua B, 456" , "98765432100" , "maria@email.com");
dao.Cadastrar(novo);

// Teste de atualização
Cliente atualizar = new Cliente();
atualizar.Id = 1; // Supondo que cliente com ID 1 existe
atualizar.Telefone = "97777-7777";
dao.Atualizar(atualizar);

// Teste de deletar
dao.Deletar(2); // Deleta cliente com ID 2

// Teste de listar todos
var lista = dao.ListarTodos();
foreach (var c in lista)
    {
    Console.WriteLine($"{c.Id} - {c.Nome} - {c.Cpf}");
    }

Console.ReadLine();
        }