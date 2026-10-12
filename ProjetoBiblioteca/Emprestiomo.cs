using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBiblioteca
{
    internal class Emprestimo
    {
        private DateTime dtEmprestimo;
        private DateTime dtDevolucao;

        public Emprestimo(DateTime dtEmprestimo, DateTime dtDevolucao)
        {
            this.dtEmprestimo = DateTime.Now;
            this.dtDevolucao = DateTime.MinValue;
        }

        public DateTime getDtEmprestimo()
        {
            return dtEmprestimo;
        }

        public DateTime getDtDevolucao()
        {
            return dtDevolucao;
        }

        public bool devolver()
        {
            if (dtDevolucao != DateTime.MinValue)
            {
                return false;
            }
            else
            {
                dtDevolucao = DateTime.Now;
                return true;
            }
        }

        public bool isAtivo()
        {
            return dtDevolucao == DateTime.MinValue;
        }

        public override string ToString()
        {
            string devolucao = isAtivo() ? "Ainda não devolvido" : dtDevolucao.ToString("dd/MM/yyyy HH:mm");
            return $"Empréstimo: {dtEmprestimo.ToString("dd/MM/yyyy HH:mm")}, Data da devolução: {devolucao}";

        }
    }
}
