using eAgendaProva.ConsoleApp.Compartilhado;
using eAgendaProva.ConsoleApp.ModuloItens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaProva.ConsoleApp.ModuloTarefas
{
    public class Tarefa : EntidadeBase
    {
        public Itens itens;
        public string prioridade;
        public string titulo;
        public DateTime dataCriacao;
        public DateTime dataConcluido;
        public string percentual;

        public string Prioridade => prioridade;
        public string Titulo => titulo;

        public bool estaAberto;

        public void Abrir()
        {
            if (!estaAberto)
            {
                estaAberto = true;
                dataCriacao = DateTime.Today;
                percentual = "0%";
            }
        }

        public void Fechar()
        {
            if (estaAberto)
            {
                estaAberto = false;

                dataConcluido = DateTime.Today;
                percentual = "100%";
            }
        }

        public override string Validar()
        {
            throw new NotImplementedException();
        }
    }
}
