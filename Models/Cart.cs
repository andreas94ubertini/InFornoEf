using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InForno.Models
{
    public class Cart
    {
        public int Quantita { get; set; }

        public string Nome { get; set; }
        public decimal CostoProdotto { get; set; }

        public decimal CostoAggiunzione { get; set; }

        public int IdProdotto { get; set; }

        public Cart() { }
        public Cart(int quantita, string nome, decimal costoProdotto, int idProdotto)
        {
            Quantita = quantita;
            Nome = nome;
            CostoProdotto = costoProdotto;
            IdProdotto = idProdotto;
        }

        public static decimal CalcoloCostoTotale(List<Cart> cart)
        {
            decimal tot = 0;
            foreach (Cart item in cart)
            {
                tot += item.Quantita * item.CostoProdotto;
            }
            return tot;
        }
    }
}