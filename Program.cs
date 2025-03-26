using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Transactions;
using System.IO;
using System.Security.Cryptography;



class Program
{

    static List<Transaction> transactionsList = LoadTransaction() ?? new List<Transaction>();

    static void Main()
    {
        while (true)
        {


            ShowSummary();

            Console.WriteLine("1 - Adicionar Transação");
            Console.WriteLine("2 - Listar Transações");
            Console.WriteLine("3 - Limpar transação");
            Console.WriteLine("4 - Sair");
            Console.WriteLine("Escolha uma opção:");
            if (int.TryParse(Console.ReadLine(), out int opcao))
            {

                if (opcao == 1)
                {
                    AdicionarTransacao();
                }
                else if (opcao == 2)
                {
                    ListarTransacoes();
                }
                else if (opcao == 3)
                {
                    RemoverTransacao();
                }
                else if (opcao == 4)
                {
                    SaveTransaction();
                    break;
                }
                else
                {
                    Console.WriteLine("Opção inválida");
                    Console.ReadLine();
                }
            }

        }
    }


    static void ShowSummary()
    {

        decimal Income = transactionsList.Where(t => !t.IsExpense).Sum(t => t.Amount);
        decimal Expense = transactionsList.Where(t => t.IsExpense).Sum(t => t.Amount);


        decimal Total = Income - Expense;

        Console.WriteLine("=== Controle de Gastos ===");
        Console.WriteLine($"Entrada: R${Income:F2}");
        Console.WriteLine($"Saída: R${Expense:F2}");
        Console.WriteLine($"Total: {(Total >= 0 ? "R$" : "-R$")}{Math.Abs(Total):F2}");
        Console.WriteLine("===========================");
    }

    static void AdicionarTransacao()
    {
        Console.Clear();
        Random random = new Random();
        int id = random.Next(1, 1000);

        Console.WriteLine("Informe seu nome:");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Informe sua idade:");
        if (!int.TryParse(Console.ReadLine(), out int idade))
        {
            Console.WriteLine("Idade inválida.");
            return;
        }else if(idade <= 0 || idade > 70)
        {
            Console.WriteLine("Idade inválida.");
            return;
        }

        Console.WriteLine("Descrição da transação:");
        string descricao = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Digite um valor: R$");
        if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
        {
            Console.WriteLine("Valor inválido.");
            return;
        }

        Console.WriteLine("É uma despesa? (s/n)");
        bool isExpense = (Console.ReadLine()?.Trim().ToLower() ?? "n") == "s";

        if (idade < 18 && !isExpense)
        {
            Console.WriteLine("Menorres de 18 não podem efetuar entradas");
            return;
        }

        transactionsList.Add(new Transaction
        {
            Id = id,
            Nome = nome,
            Idade = idade,
            Descricao = descricao,
            Amount = amount,
            IsExpense = isExpense
        });

        SaveTransaction();
        Console.WriteLine("Transação adicionada com sucesso!");
    }



    static void ListarTransacoes()
    {
        Console.Clear();



        if (transactionsList.Count == 0)
        {
            Console.WriteLine("Nenhuma transação registrada.");
        }
        else
        {
            Console.WriteLine("=== Lista de Transações ===");
            foreach (var transaction in transactionsList)
            {
                Console.WriteLine($"ID: {transaction.Id}");
                Console.WriteLine($"Nome: {transaction.Nome}");
                Console.WriteLine($"Idade: {transaction.Idade}");
                Console.WriteLine($"Descrição: {transaction.Descricao}");
                Console.WriteLine($"{(transaction.IsExpense ? "-" : "+")} R${transaction.Amount:F2} ");
                Console.WriteLine("----------------------------------------------\n");

            }
        }

        Console.ReadLine();
    }

    static void RemoverTransacao()
    {
        Console.Clear();

        if (transactionsList.Count == 0)
        {
            Console.WriteLine("Nenhuma transação registrada para remover.");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Digite o ID da transação que deseja remover:");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            Console.ReadLine();
            return;
        }

        var transactionToRemove = transactionsList.FirstOrDefault(t => t.Id == id);

        if (transactionToRemove != null)
        {
            transactionsList.Remove(transactionToRemove);
            SaveTransaction(); // Salva a lista após a remoção
            Console.WriteLine("Transação removida com sucesso!");
        }
        else
        {
            Console.WriteLine("ID não encontrado.");
        }

        Console.ReadLine();
    }

    static void SaveTransaction()
    {
        string json = JsonSerializer.Serialize(transactionsList);
        File.WriteAllText("transactions.json", json);

    }



    static List<Transaction> LoadTransaction()
    {
        if (File.Exists("transactions.json"))
        {
            string json = System.IO.File.ReadAllText("transactions.json");
            return JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
        }
        return new List<Transaction>();


    }
}
class Transaction
{
    public decimal Amount { get; set; }
    public bool IsExpense { get; set; }
    public int Id { get; internal set; }
    public int Idade { get; internal set; }
    public string? Nome { get; internal set; }
    public string? Descricao { get; internal set; }
}