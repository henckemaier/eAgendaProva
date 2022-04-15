using eAgendaProva.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaProva.ConsoleApp.ModuloTarefas
{
    public class RepositorioTarefas : RepositorioBase<Tarefa>
    {
        public RepositorioTarefas()
        {
        }

        public override string Inserir(Tarefa tarefa)
        {
            tarefa.numero = ++contadorNumero;

            Console.Write("Digite a prioridade(alta [1] | normal [2] | baixa [3]): ");
            tarefa.prioridade = Console.ReadLine();

            if (tarefa.prioridade == "1")
            {
                tarefa.prioridade = "alta";
            }
            else if (tarefa.prioridade == "2")
            {
                tarefa.prioridade = "normal";
            }
            else if (tarefa.prioridade == "3")
            {
                tarefa.prioridade = "baixa";
            }

            Console.Write("Digite o titulo da tarefa: ");
            tarefa.titulo = Console.ReadLine();

            tarefa.Abrir();

            tarefa.itens.RegistrarCompromisso(tarefa);

            registros.Add(tarefa);

            return "REGISTRO_VALIDO";
        }

        public bool RegistrarDevolucao(Tarefa tarefa)
        {
            tarefa.Fechar();

            return true;
        }

        public List<Tarefa> SelecionarEmprestimosAbertos()
        {
            List<Tarefa> emprestimosAbertos = new List<Tarefa>();

            foreach (Tarefa emprestimo in registros)
            {
                if (emprestimo.estaAberto)
                    emprestimosAbertos.Add(emprestimo);
            }

            return emprestimosAbertos;
        }
    }
}
