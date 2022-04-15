using eAgendaProva.ConsoleApp.Compartilhado;
using eAgendaProva.ConsoleApp.ModuloItens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaProva.ConsoleApp.ModuloTarefas
{
    internal class TelaCadastroTarefas : TelaBase
    {
        private readonly Notificador notificador;
        private readonly RepositorioTarefas repositorioTarefas;
        private readonly RepositorioItens repositorioItens;
        private readonly TelaCadastroItens telaCadastroItens;

        public TelaCadastroTarefas(
            Notificador notificador,
            RepositorioTarefas repositorioTarefas,
            RepositorioItens repositorioItens,
            TelaCadastroItens telaCadastroItens) : base("Cadastro de Tarefas")
        {
            this.notificador = notificador;
            this.repositorioTarefas = repositorioTarefas;
            this.repositorioItens = repositorioItens;
            this.telaCadastroItens = telaCadastroItens;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Registrar Tarefa");
            Console.WriteLine("Digite 2 para Editar Tarefa");
            Console.WriteLine("Digite 3 para Excluir Tarefa");
            Console.WriteLine("Digite 4 para Visualizar");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void RegistrarEmprestimo()
        {
            MostrarTitulo("Inserindo novo Compromisso");

            // Validação do Amigo
            Itens itemSelecionado = ObtemAmigo();

            if (itemSelecionado == null)
            {
                notificador.ApresentarMensagem("Nenhum contato selecionado", TipoMensagem.Erro);
                return;
            }

            Tarefa tarefa = ObtemEmprestimo(itemSelecionado);

            string statusValidacao = repositorioTarefas.Inserir(tarefa);

            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Compromisso cadastrado com sucesso!", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        public void RegistrarDevolucao()
        {
            MostrarTitulo("Devolvendo Empréstimo");

            bool temEmprestimos = VisualizarEmprestimosEmAberto("Pesquisando");

            if (!temEmprestimos)
            {
                notificador.ApresentarMensagem("Nenhum empréstimo disponível para devolução.", TipoMensagem.Atencao);
                return;
            }

            int numeroEmprestimo = ObterNumeroEmprestimo();

            Tarefa emprestimoParaDevolver = (Tarefa)repositorioTarefas.SelecionarRegistro(numeroEmprestimo);

            if (!emprestimoParaDevolver.estaAberto)
            {
                notificador.ApresentarMensagem("O empréstimo selecionado não está mais aberto.", TipoMensagem.Atencao);
                return;
            }

            repositorioTarefas.RegistrarDevolucao(emprestimoParaDevolver);

            notificador.ApresentarMensagem("Devolução concluída com sucesso!", TipoMensagem.Sucesso);
        }

        public void EditarEmprestimo()
        {
            MostrarTitulo("Editando Compromisso");

            bool temEmprestimosCadastrados = VisualizarEmprestimos("Pesquisando");

            if (temEmprestimosCadastrados == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhuma compromisso cadastrado para poder editar", TipoMensagem.Atencao);
                return;
            }
            int numeroEmprestimo = ObterNumeroEmprestimo();

            Itens itemSelecionado = ObtemAmigo();

            Tarefa emprestimoAtualizado = ObtemEmprestimo(itemSelecionado);

            repositorioTarefas.Editar(numeroEmprestimo, emprestimoAtualizado);

            notificador.ApresentarMensagem("Empréstimo editado com sucesso", TipoMensagem.Sucesso);
        }

        public void VerEmprestimo()
        {
            MostrarTitulo("Visualizando Compromisso");

            bool temEmprestimosCadastrados = VisualizarEmprestimos("Pesquisando");

            if (temEmprestimosCadastrados == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhuma compromisso cadastrado", TipoMensagem.Atencao);
                return;
            }

            int numeroEmprestimo = ObterNumeroEmprestimo();

        }

        public void ExcluirEmprestimo()
        {
            MostrarTitulo("Excluindo Compromisso");

            bool temEmprestimosCadastrados = VisualizarEmprestimos("Pesquisando");

            if (temEmprestimosCadastrados == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhuma compromisso cadastrado para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int numeroEmprestimo = ObterNumeroEmprestimo();

            repositorioTarefas.Excluir(numeroEmprestimo);

            notificador.ApresentarMensagem("Compromisso excluído com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarEmprestimos(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Compromisso");

            List<Tarefa> tarefas = repositorioTarefas.SelecionarTodos();

            if (tarefas.Count == 0)
                return false;


            for (int i = 0; i < tarefas.Count; i++)
            {
                Tarefa tarefa = (Tarefa)tarefas[i];

                string statusEmprestimo = tarefa.estaAberto ? "Aberto" : "Fechado";

                Console.WriteLine("=======================");
                Console.WriteLine("Número: " + tarefa.numero);
                Console.WriteLine("-----------------------");
                Console.WriteLine("Prioridade: " + tarefa.prioridade);
                Console.WriteLine("Assunto : " + tarefa.titulo);
                Console.WriteLine("-----------------------");
                Console.WriteLine("Data de criação: " + tarefa.dataCriacao);
                Console.WriteLine("Data de conclusão: " + tarefa.dataConcluido);
                Console.WriteLine("Percentual: " + tarefa.percentual);
                Console.WriteLine("==================================");
                Console.WriteLine();
            }

            return true;
        }

        public bool VisualizarEmprestimosEmAberto(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Empréstimos em Aberto");

            List<Tarefa> tarefas = repositorioTarefas.SelecionarEmprestimosAbertos();

            if (tarefas.Count == 0)
                return false;

            for (int i = 0; i < tarefas.Count; i++)
            {
                Tarefa tarefa = tarefas[i];

                Console.WriteLine("Número: " + tarefa.numero);
                Console.WriteLine("Nome do contato: " + tarefa.prioridade);
                Console.WriteLine("Assunto: " + tarefa.titulo);
                Console.WriteLine("Data do compromisso: " + tarefa.dataCriacao);
                Console.WriteLine("Data do compromisso: " + tarefa.dataCriacao);
                Console.WriteLine();
            }

            return true;
        }

        #region Métodos privados
        private Tarefa ObtemEmprestimo(Itens itens)
        {
            Tarefa novoEmprestimo = new Tarefa();

            novoEmprestimo.itens = itens;

            return novoEmprestimo;
        }

        private Itens ObtemAmigo()
        {
            bool temAmigosDisponiveis = telaCadastroItens.VisualizarRegistros("Pesquisando");

            if (!temAmigosDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhum contato disponível para cadastrar compromisso.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número do contato que irá estar no compromisso: ");
            int numeroAmigoEmprestimo = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Itens amigoSelecionado = (Itens)repositorioItens.SelecionarRegistro(numeroAmigoEmprestimo);

            return amigoSelecionado;
        }


        private int ObterNumeroEmprestimo()
        {
            int numeroCompromisso;
            bool numeroEmprestimoEncontrado;

            do
            {
                Console.Write("Digite o número do compromisso que deseja selecionar: ");
                numeroCompromisso = Convert.ToInt32(Console.ReadLine());

                numeroEmprestimoEncontrado = repositorioTarefas.ExisteRegistro(numeroCompromisso);

                if (numeroEmprestimoEncontrado == false)
                    notificador.ApresentarMensagem("Número de compromisso não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroEmprestimoEncontrado == false);
            return numeroCompromisso;
        }
        #endregion
    }
}
