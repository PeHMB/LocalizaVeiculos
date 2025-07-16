using System;

namespace locadoraVeiculos.Entidades
    {
    public class Veiculo
        {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Renavam { get; set; }
        public string Chassi { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public int Ano { get; set; }
        public string Cor { get; set; }
        public int Quilometragem { get; set; }
        public string Status { get; set; }

        public Veiculo()
            {
            }

        public Veiculo(string placa , string renavam , string chassi , string modelo , string marca , int ano , string cor , int quilometragem , string status)
            {
            Placa = placa;
            Renavam = renavam;
            Chassi = chassi;
            Modelo = modelo;
            Marca = marca;
            Ano = ano;
            Cor = cor;
            Quilometragem = quilometragem;
            Status = status;
            }
        }
    }
