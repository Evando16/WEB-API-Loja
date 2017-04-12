using Loja.DataLayer;
using Loja.DataLayer.Migrations;
using Loja.DomainClasses;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;

namespace Loja
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Faz a Migração do banco de dados adicionando ou removendo propriedades nas tabelas.
             * OBS: Para fazer a remoção é necessário permitir 
             * a perda de dados na classe de Configuration dentro da pasta de Migrations
             */
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<LojaModelContext, Configuration>());

            /*
             * Dropa o banco de dados e cria novamente
             */
            //Database.SetInitializer(new DropCreateDatabaseAlways<LojaModelContext>());

            GetProdutos();

            //InserirPessoa("Evando", "evando66.ea@gmail.com","Rua Lignito");
            GetPessoas();

            Console.ReadKey();

        }

        private static void GetPessoas()
        {
            using(var context = new LojaModelContext())
            {
                var pessoas = context.Pessoas.ToList();

                if (pessoas.Any())
                {
                    foreach (var objPessoa in pessoas)
                    {
                        Console.WriteLine("|{0}|{1}|{2}|", objPessoa.Id, objPessoa.Nome, objPessoa.Email);
                    }
                }
                else
                {
                    Console.WriteLine("Sem pessoas cadastradas.");
                }
            }
        }

        private static void InserirPessoa(string nome, string email, string endereco)
        {
            using (var context = new LojaModelContext())
            {
                if (context.InserirPessoa(new Pessoa {Nome = nome, Email = email, Endereco = endereco }))
                {
                    context.SaveChanges();
                    Console.WriteLine("Pessoa inserida");
                }
            }
        }

        private static void GetProdutos()
        {
            using (var context = new LojaModelContext())
            {
                var produtos = context.Produtos.ToList();

                if (produtos.Any())
                {
                    foreach (var objProduto in produtos)
                    {
                        Console.WriteLine(objProduto.Nome);
                    }
                }else
                {
                    Console.WriteLine("Sem produtos cadastrado.");
                }
            }
        }
    }
}
