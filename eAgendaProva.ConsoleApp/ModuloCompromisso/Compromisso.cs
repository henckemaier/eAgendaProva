using eAgendaProva.ConsoleApp.Compartilhado;
using eAgendaProva.ConsoleApp.ModuloContato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaProva.ConsoleApp.ModuloCompromisso
{
    public class Compromisso: EntidadeBase
    {
        public Contato contato;
        public string assunto;
        public DateTime dataEmprestimo;
        public DateTime dataDevolucao;
        public string dataAssunto;
        public string horarioInicio;
        public string horarioFim;

        public string Assunto => assunto;

        public bool estaAberto;

        public void Abrir()
        {
            if (!estaAberto)
            {
                estaAberto = true;
                dataEmprestimo = DateTime.Today;
            }
        }

        public void Fechar()
        {
            if (estaAberto)
            {
                estaAberto = false;

                DateTime dataRealDevolucao = DateTime.Today;
            }
        }

        public override string Validar()
        {
            throw new NotImplementedException();
        }
    }
}
