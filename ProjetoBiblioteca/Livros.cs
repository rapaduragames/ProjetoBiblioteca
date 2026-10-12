using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBiblioteca
{
    internal class Livros
    {
        private List<Livro> acervo;

        public Livros()
        {
            acervo = new List<Livro>();
        }

        public void adicionar(Livro livro)
        {
            if (livro == null)
                return;

            if (pesquisar(livro) != null)
                return;

            acervo.Add(livro);
        }

        public Livro pesquisar(Livro livro)
        {
            if (livro == null)
                return null;

            foreach (Livro existente in acervo)
            {
                if (existente.getIsbn() == livro.getIsbn())
                    return existente;
            }

            return null;
        }

        public IReadOnlyList<Livro> getAcervo()
        {
            return acervo.AsReadOnly();
        }
    }
}