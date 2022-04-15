using eAgendaProva.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaProva.ConsoleApp.ModuloContato
{
    public class RepositorioContato : RepositorioBase<Contato>
    {
        public bool ContatoJaUtilizado(string nomeInformado)
        {
            foreach (Contato contato in registros)
                if (contato.Nome == nomeInformado)
                    return true;

            return false;
        }
    }
}
