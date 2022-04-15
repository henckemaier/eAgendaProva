using eAgendaProva.ConsoleApp.Compartilhado;
using eAgendaProva.ConsoleApp.ModuloTarefas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaProva.ConsoleApp.ModuloItens
{
    public class Itens : EntidadeBase
    {
        private readonly string descricao;
        private readonly string status;

        private readonly Tarefa[] historicoTarefa = new Tarefa[10];

        public Itens(string descricao, string status)
        {
            this.descricao = descricao;
            this.status = status;
        }


        public string Descricao => descricao;
        public string Status => status;

        public override string Validar()
        {
            throw new NotImplementedException();
        }

        public void RegistrarCompromisso(Tarefa tarefa)
        {
            historicoTarefa[ObtemPosicaoVazia()] = tarefa;
        }

        private int ObtemPosicaoVazia()
        {
            for (int i = 0; i < historicoTarefa.Length; i++)
            {
                if (historicoTarefa[i] == null)
                    return i;
            }

            return -1;
        }
    }
}
