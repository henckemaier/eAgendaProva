using eAgendaProva.ConsoleApp.Compartilhado;
using eAgendaProva.ConsoleApp.ModuloCompromisso;
using eAgendaProva.ConsoleApp.ModuloContato;
using eAgendaProva.ConsoleApp.ModuloTarefas;
using System;

namespace eAgendaProva.ConsoleApp
{
    internal class Program
    {
        static Notificador notificador = new Notificador();
        static TelaMenuPrincipal menuPrincipal = new TelaMenuPrincipal(notificador);

        static void Main(string[] args)
        {
            while (true)
            {
                TelaBase telaSelecionada = menuPrincipal.ObterTela();

                string opcaoSelecionada = telaSelecionada.MostrarOpcoes();

                if (telaSelecionada is ICadastroBasico)
                    GerenciarCadastroBasico(telaSelecionada, opcaoSelecionada);

                else if (telaSelecionada is TelaCadastravelCompromisso)
                    GerenciarTelaCadastroCompromisso(telaSelecionada, opcaoSelecionada);

                else if (telaSelecionada is TelaCadastroTarefas)
                    GerenciarTelaCadastroTarefa(telaSelecionada, opcaoSelecionada);
            }
        }

        public static void GerenciarCadastroBasico(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            ICadastroBasico telaCadastroBasico = (ICadastroBasico)telaSelecionada;

            if (opcaoSelecionada == "1")
                telaCadastroBasico.InserirRegistro();

            else if (opcaoSelecionada == "2")
                telaCadastroBasico.EditarRegistro();

            else if (opcaoSelecionada == "3")
                telaCadastroBasico.ExcluirRegistro();

            else if (opcaoSelecionada == "4")
            {
                bool temRegistros = telaCadastroBasico.VisualizarRegistros("Tela");

                if (!temRegistros)
                    notificador.ApresentarMensagem("Nenhum registro disponível!", TipoMensagem.Atencao);
            }
        }

        private static void GerenciarTelaCadastroCompromisso(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            TelaCadastravelCompromisso telaCadastravelCompromisso = (TelaCadastravelCompromisso)telaSelecionada;

            if (opcaoSelecionada == "1")
                telaCadastravelCompromisso.RegistrarEmprestimo();
            else if (opcaoSelecionada == "2")
                telaCadastravelCompromisso.EditarEmprestimo();
            else if (opcaoSelecionada == "3")
                telaCadastravelCompromisso.ExcluirEmprestimo();
            else if (opcaoSelecionada == "4")
            {
                telaCadastravelCompromisso.VerEmprestimo();
            }
        }

        private static void GerenciarTelaCadastroTarefa(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            TelaCadastroTarefas telaCadastroTarefas = (TelaCadastroTarefas)telaSelecionada;

            if (opcaoSelecionada == "1")
                telaCadastroTarefas.RegistrarEmprestimo();
            else if (opcaoSelecionada == "2")
                telaCadastroTarefas.EditarEmprestimo();
            else if (opcaoSelecionada == "3")
                telaCadastroTarefas.ExcluirEmprestimo();
            else if (opcaoSelecionada == "4")
            {
                telaCadastroTarefas.VerEmprestimo();
            }
        }
    }
}
