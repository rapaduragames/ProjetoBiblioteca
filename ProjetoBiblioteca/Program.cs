
using System;

namespace ProjetoBiblioteca
{
    internal class Program
    {
        static Livros acervo = new Livros();
        static int proximoTombo = 1;

        static void Main(string[] args)
        {
            int opcao;

            do
            {
                Console.Clear();
                Console.WriteLine("===== BIBLIOTECA =====");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("1 - Adicionar livro");
                Console.WriteLine("2 - Pesquisar livro (sintético)");
                Console.WriteLine("3 - Pesquisar livro (analítico)");
                Console.WriteLine("4 - Adicionar exemplar");
                Console.WriteLine("5 - Registrar empréstimo");
                Console.WriteLine("6 - Registrar devolução");
                Console.Write("\nEscolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                    opcao = -1;

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Encerrando a biblioteca...");
                        break;

                    case 1:
                        AdicionarLivro();
                        break;

                    case 2:
                        PesquisarLivro(false);
                        break;

                    case 3:
                        PesquisarLivro(true);
                        break;

                    case 4:
                        AdicionarExemplar();
                        break;

                    case 5:
                        RegistrarEmprestimo();
                        break;

                    case 6:
                        RegistrarDevolucao();
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione ENTER para continuar...");
                    Console.ReadLine();
                }

            } while (opcao != 0);
        }

        static Livro BuscarPorISBN()
        {
            Console.Write("ISBN do livro: ");

            if (!int.TryParse(Console.ReadLine(), out int isbn))
            {
                Console.WriteLine("ISBN inválido.");
                return null;
            }

            return acervo.pesquisar(new Livro(isbn, "", "", ""));
        }

        static void AdicionarLivro()
        {
            Console.WriteLine("\n--- ADICIONAR LIVRO ---");

            Console.Write("ISBN: ");

            if (!int.TryParse(Console.ReadLine(), out int isbn))
            {
                Console.WriteLine("ISBN inválido.");
                return;
            }

            if (acervo.pesquisar(new Livro(isbn, "", "", "")) != null)
            {
                Console.WriteLine("Já existe um livro com esse ISBN.");
                return;
            }

            Console.Write("Título: ");
            string titulo = Console.ReadLine() ?? "";

            Console.Write("Autor: ");
            string autor = Console.ReadLine() ?? "";

            Console.Write("Editora: ");
            string editora = Console.ReadLine() ?? "";

            Livro livro = new Livro(isbn, titulo, autor, editora);
            acervo.adicionar(livro);

            Console.WriteLine("Livro cadastrado com sucesso!");
        }

        static void PesquisarLivro(bool analitico)
        {
            Console.WriteLine(analitico
                ? "\n--- PESQUISA ANALÍTICA ---"
                : "\n--- PESQUISA SINTÉTICA ---");

            Livro livro = BuscarPorISBN();

            if (livro == null)
            {
                Console.WriteLine("Livro não encontrado.");
                return;
            }

            Console.WriteLine("\n" + livro);

            if (analitico)
            {
                foreach (Exemplar exemplar in livro.getExemplares())
                {
                    Console.WriteLine("\n" + exemplar);

                    foreach (Emprestimo emprestimo in exemplar.getEmprestimos())
                    {
                        Console.WriteLine("  " + emprestimo);
                    }
                }
            }
        }

        static void AdicionarExemplar()
        {
            Console.WriteLine("\n--- ADICIONAR EXEMPLAR ---");

            Livro livro = BuscarPorISBN();

            if (livro == null)
            {
                Console.WriteLine("Livro não encontrado.");
                return;
            }

            Exemplar exemplar = new Exemplar(proximoTombo++);
            livro.adicionarExemplar(exemplar);

            Console.WriteLine("Exemplar adicionado com sucesso!");
            Console.WriteLine(exemplar);
        }

        static void RegistrarEmprestimo()
        {
            Console.WriteLine("\n--- REGISTRAR EMPRÉSTIMO ---");

            Livro livro = BuscarPorISBN();

            if (livro == null)
            {
                Console.WriteLine("Livro não encontrado.");
                return;
            }

            Console.WriteLine("Exemplares disponíveis:");

            foreach (Exemplar exemplar in livro.getExemplares())
            {
                if (exemplar.disponivel())
                    Console.WriteLine(exemplar);
            }

            Console.Write("Número do tombo: ");

            if (!int.TryParse(Console.ReadLine(), out int tombo))
            {
                Console.WriteLine("Tombo inválido.");
                return;
            }

            foreach (Exemplar exemplar in livro.getExemplares())
            {
                if (exemplar.getTombo() == tombo)
                {
                    if (exemplar.emprestar())
                        Console.WriteLine("Empréstimo registrado!");
                    else
                        Console.WriteLine("Esse exemplar já está emprestado.");

                    return;
                }
            }

            Console.WriteLine("Exemplar não encontrado nesse livro.");
        }

        static void RegistrarDevolucao()
        {
            Console.WriteLine("\n--- REGISTRAR DEVOLUÇÃO ---");

            Livro livro = BuscarPorISBN();

            if (livro == null)
            {
                Console.WriteLine("Livro não encontrado.");
                return;
            }

            Console.Write("Número do tombo: ");

            if (!int.TryParse(Console.ReadLine(), out int tombo))
            {
                Console.WriteLine("Tombo inválido.");
                return;
            }

            foreach (Exemplar exemplar in livro.getExemplares())
            {
                if (exemplar.getTombo() == tombo)
                {
                    if (exemplar.devolver())
                        Console.WriteLine("Devolução registrada!");
                    else
                        Console.WriteLine("Esse exemplar não possui empréstimo ativo.");

                    return;
                }
            }

            Console.WriteLine("Exemplar não encontrado nesse livro.");
        }
    }
}
