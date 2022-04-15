using eAgendaProva.ConsoleApp.ModuloCompromisso;
using eAgendaProva.ConsoleApp.ModuloContato;
using eAgendaProva.ConsoleApp.ModuloItens;
using eAgendaProva.ConsoleApp.ModuloTarefas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaProva.ConsoleApp.Compartilhado
{
    public class TelaMenuPrincipal
    {
        private string opcaoSelecionada;

        private const int QUANTIDADE_REGISTROS = 10;

        private Notificador notificador;

        // Declaração de Contato
        private RepositorioContato repositorioContato;
        private TelaCadastroContato telaCadastroContato;

        // Declaração de Compromisso
        private RepositorioCompromisso repositorioCompromisso;
        private TelaCadastravelCompromisso telaCadastroCompromisso;

        // Declaração de Itens
        private RepositorioItens repositorioItens;
        private TelaCadastroItens telaCadastroItens;

        // Declaração de Tarefas
        private RepositorioTarefas repositorioTarefas;
        private TelaCadastroTarefas telaCadastroTarefas;


        public TelaMenuPrincipal(Notificador notificador)
        {
            this.notificador = notificador;

            repositorioContato = new RepositorioContato();
            telaCadastroContato = new TelaCadastroContato(repositorioContato, notificador);
            repositorioCompromisso = new RepositorioCompromisso();
            repositorioItens = new RepositorioItens();
            telaCadastroItens = new TelaCadastroItens(repositorioItens, notificador);
            repositorioTarefas = new RepositorioTarefas();

            telaCadastroTarefas = new TelaCadastroTarefas(
                notificador,
                repositorioTarefas,
                repositorioItens,
                telaCadastroItens
            );

            telaCadastroCompromisso = new TelaCadastravelCompromisso(
                notificador,
                repositorioCompromisso,
                repositorioContato,
                telaCadastroContato
            );
        }

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("eAgenda");

            Console.WriteLine();

            Console.WriteLine("Digite 1 para Cadastrar Contatos");
            Console.WriteLine("Digite 2 para Gerenciar Compromissos");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Digite 3 para Cadastrar Itens");
            Console.WriteLine("Digite 4 para Gerenciar Tarefas");

            Console.WriteLine("Digite s para sair");

            opcaoSelecionada = Console.ReadLine();

            return opcaoSelecionada;
        }

        public TelaBase ObterTela()
        {
            string opcao = MostrarOpcoes();

            TelaBase tela = null;

            if (opcao == "1")
                tela = telaCadastroContato;

            else if (opcao == "2")
                tela = telaCadastroCompromisso;

            else if (opcao == "3")
                tela = telaCadastroItens;

            else if (opcao == "4")
                tela = telaCadastroTarefas;

            return tela;
        }

    }
}