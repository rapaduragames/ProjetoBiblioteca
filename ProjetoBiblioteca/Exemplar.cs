using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBiblioteca
{
    internal class Exemplar
    {
        private int tombo;
        private List<Emprestimo> emprestimos;

        public Exemplar(int tombo)
        {
            this.tombo = tombo;
            emprestimos = new List<Emprestimo>();
        }

        public int getTombo()
        {
            return tombo;
        }

        public bool emprestar()
        {
            if (!disponivel())
                return false;

            emprestimos.Add(new Emprestimo(DateTime.Now, DateTime.MinValue));
            return true;
        }

        public bool devolver()
        {
            foreach (Emprestimo emprestimo in emprestimos)
            {
                if (emprestimo.isAtivo())
                    return emprestimo.devolver();
            }

            return false;
        }

        public bool disponivel()
        {
            foreach (Emprestimo emprestimo in emprestimos)
            {
                if (emprestimo.isAtivo())
                    return false;
            }

            return true;
        }

        public int qtdeEmprestimos()
        {
            return emprestimos.Count;
        }

        public IReadOnlyList<Emprestimo> getEmprestimos()
        {
            return emprestimos.AsReadOnly();
        }

        public override string ToString()
        {
            string situacao = disponivel() ? "Disponível" : "Emprestado";

            return $"Tombo: {tombo} | Situação: {situacao}";
        }
    }
}