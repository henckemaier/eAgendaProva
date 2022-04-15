using eAgendaProva.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaProva.ConsoleApp.ModuloCompromisso
{
    public class RepositorioCompromisso : RepositorioBase<Compromisso>
    {
        public RepositorioCompromisso()
        {
        }

        public override string Inserir(Compromisso compromisso)
        {
            compromisso.numero = ++contadorNumero;

            Console.WriteLine("Digite o assunto: ");
            compromisso.assunto = Console.ReadLine();

            Console.WriteLine("Digite a data (dd/mm/aaaa): ");
            compromisso.dataAssunto = Console.ReadLine();
            DateTime dt;
            while (!DateTime.TryParseExact(compromisso.dataAssunto, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt))
            {
                Console.WriteLine("Data invalida, tente novamente");
                compromisso.dataAssunto = Console.ReadLine();
            }

            Console.WriteLine("Digite o horario de inicio (hh:mm): ");
            compromisso.horarioInicio = Console.ReadLine();
            DateTime dh;
            while (!DateTime.TryParseExact(compromisso.horarioInicio, "t", null, System.Globalization.DateTimeStyles.None, out dh))
            {
                Console.WriteLine("Data invalida, tente novamente");
                compromisso.dataAssunto = Console.ReadLine();
            }

            Console.WriteLine("Digite o horario de termino (hh:mm): ");
            compromisso.horarioFim = Console.ReadLine();
            DateTime dhf;
            while (!DateTime.TryParseExact(compromisso.horarioFim, "t", null, System.Globalization.DateTimeStyles.None, out dhf))
            {
                Console.WriteLine("Data invalida, tente novamente");
                compromisso.horarioFim = Console.ReadLine();
            }

            compromisso.Abrir();

            compromisso.contato.RegistrarCompromisso(compromisso);

            registros.Add(compromisso);

            return "REGISTRO_VALIDO";
        }

        public bool RegistrarDevolucao(Compromisso emprestimo)
        {
            emprestimo.Fechar();

            return true;
        }

        public List<Compromisso> SelecionarEmprestimosAbertos()
        {
            List<Compromisso> emprestimosAbertos = new List<Compromisso>();

            foreach (Compromisso emprestimo in registros)
            {
                if (emprestimo.estaAberto)
                    emprestimosAbertos.Add(emprestimo);
            }

            return emprestimosAbertos;
        }
    }
}
