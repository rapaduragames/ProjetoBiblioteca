
using System;
using System.Collections.Generic;

namespace ProjetoBiblioteca
{
    internal class Livro
    {
        private int isbn;
        private string titulo;
        private string autor;
        private string editora;
        private List<Exemplar> exemplares;

        public Livro(int isbn, string titulo, string autor, string editora)
        {
            this.isbn = isbn;
            this.titulo = titulo;
            this.autor = autor;
            this.editora = editora;
            exemplares = new List<Exemplar>();
        }

        public int getIsbn()
        {
            return isbn;
        }

        public string getTitulo()
        {
            return titulo;
        }

        public string getAutor()
        {
            return autor;
        }

        public string getEditora()
        {
            return editora;
        }

        public void adicionarExemplar(Exemplar exemplar)
        {
            if (exemplar == null)
                return;

            foreach (Exemplar existente in exemplares)
            {
                if (existente.getTombo() == exemplar.getTombo())
                    return;
            }

            exemplares.Add(exemplar);
        }

        public int qtdeExemplares()
        {
            return exemplares.Count;
        }

        public int qtdeDisponiveis()
        {
            int quantidade = 0;

            foreach (Exemplar exemplar in exemplares)
            {
                if (exemplar.disponivel())
                    quantidade++;
            }

            return quantidade;
        }

        public int qtdeEmprestimos()
        {
            int quantidade = 0;

            foreach (Exemplar exemplar in exemplares)
                quantidade += exemplar.qtdeEmprestimos();

            return quantidade;
        }

        public double percDisponibilidade()
        {
            if (qtdeExemplares() == 0)
                return 0;

            return (double)qtdeDisponiveis() / qtdeExemplares() * 100;
        }

        public IReadOnlyList<Exemplar> getExemplares()
        {
            return exemplares.AsReadOnly();
        }

        public override string ToString()
        {
            return $"ISBN: {isbn}\n" +
                   $"Título: {titulo}\n" +
                   $"Autor: {autor}\n" +
                   $"Editora: {editora}\n" +
                   $"Total de exemplares: {qtdeExemplares()}\n" +
                   $"Exemplares disponíveis: {qtdeDisponiveis()}\n" +
                   $"Total de empréstimos: {qtdeEmprestimos()}\n" +
                   $"Disponibilidade: {percDisponibilidade():F2}%";
        }
    }
}
