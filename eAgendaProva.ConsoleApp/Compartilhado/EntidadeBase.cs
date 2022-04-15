using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaProva.ConsoleApp.Compartilhado
{
    public abstract class EntidadeBase
    {
        public int numero;

        public abstract string Validar();
    }
}
