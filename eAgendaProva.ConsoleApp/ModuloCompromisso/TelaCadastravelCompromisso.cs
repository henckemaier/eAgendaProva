using eAgendaProva.ConsoleApp.Compartilhado;
using eAgendaProva.ConsoleApp.ModuloContato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaProva.ConsoleApp.ModuloCompromisso
{
    public class TelaCadastravelCompromisso : TelaBase
    {
        private readonly Notificador notificador;
        private readonly RepositorioCompromisso repositorioCompromisso;
        private readonly RepositorioContato repositorioContato;
        private readonly TelaCadastroContato telaCadastroContato;

        public TelaCadastravelCompromisso(
            Notificador notificador,
            RepositorioCompromisso repositorioCompromisso,
            RepositorioContato repositorioContato,
            TelaCadastroContato telaCadastroContato) : base("Cadastro de Empréstimos")
        {
            this.notificador = notificador;
            this.repositorioCompromisso = repositorioCompromisso;
            this.repositorioContato = repositorioContato;
            this.telaCadastroContato = telaCadastroContato;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Registrar Compromisso");
            Console.WriteLine("Digite 2 para Editar Compromisso");
            Console.WriteLine("Digite 3 para Excluir Compromisso");
            Console.WriteLine("Digite 4 para Visualizar");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void RegistrarEmprestimo()
        {
            MostrarTitulo("Inserindo novo Compromisso");

            // Validação do Amigo
            Contato contatoSelecionado = ObtemAmigo();

            if (contatoSelecionado == null)
            {
                notificador.ApresentarMensagem("Nenhum contato selecionado", TipoMensagem.Erro);
                return;
            }

            Compromisso compromisso = ObtemEmprestimo(contatoSelecionado);

            string statusValidacao = repositorioCompromisso.Inserir(compromisso);

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

            Compromisso emprestimoParaDevolver = (Compromisso)repositorioCompromisso.SelecionarRegistro(numeroEmprestimo);

            if (!emprestimoParaDevolver.estaAberto)
            {
                notificador.ApresentarMensagem("O empréstimo selecionado não está mais aberto.", TipoMensagem.Atencao);
                return;
            }

            repositorioCompromisso.RegistrarDevolucao(emprestimoParaDevolver);

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

            Contato contatoSelecionado = ObtemAmigo();

            Compromisso emprestimoAtualizado = ObtemEmprestimo(contatoSelecionado);

            repositorioCompromisso.Editar(numeroEmprestimo, emprestimoAtualizado);

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

            repositorioCompromisso.Excluir(numeroEmprestimo);

            notificador.ApresentarMensagem("Compromisso excluído com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarEmprestimos(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Compromisso");

            List<Compromisso> compromissos = repositorioCompromisso.SelecionarTodos();

            if (compromissos.Count == 0)
                return false;

            
            for (int i = 0; i < compromissos.Count; i++)
            {
                Compromisso compromisso = (Compromisso)compromissos[i];

                string statusEmprestimo = compromisso.estaAberto ? "Aberto" : "Fechado";

                Console.WriteLine("=======================");
                Console.WriteLine("Número: " + compromisso.numero);
                Console.WriteLine("-----------------------");
                Console.WriteLine("Nome do contato: " + compromisso.contato.Nome);
                Console.WriteLine("Assunto : " + compromisso.Assunto);
                Console.WriteLine("-----------------------");
                Console.WriteLine("Data: " + compromisso.dataAssunto);
                Console.WriteLine("Horario de Inicio: " + compromisso.horarioInicio);
                Console.WriteLine("Horario de Termino: " + compromisso.horarioFim);
                Console.WriteLine("==================================");
                Console.WriteLine();
            }

            return true;
        }

        public bool VisualizarEmprestimosEmAberto(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Empréstimos em Aberto");

            List<Compromisso> compromissos = repositorioCompromisso.SelecionarEmprestimosAbertos();

            if (compromissos.Count == 0)
                return false;

            for (int i = 0; i < compromissos.Count; i++)
            {
                Compromisso compromisso = compromissos[i];

                Console.WriteLine("Número: " + compromisso.numero);
                Console.WriteLine("Nome do contato: " + compromisso.contato.Nome);
                Console.WriteLine("Assunto: " + compromisso.Assunto);
                Console.WriteLine("Data do compromisso: " + compromisso.dataEmprestimo);
                Console.WriteLine();
            }

            return true;
        }

        #region Métodos privados
        private Compromisso ObtemEmprestimo(Contato contato)
        {
            Compromisso novoEmprestimo = new Compromisso();

            novoEmprestimo.contato = contato;

            return novoEmprestimo;
        }

        private Contato ObtemAmigo()
        {
            bool temAmigosDisponiveis = telaCadastroContato.VisualizarRegistros("Pesquisando");

            if (!temAmigosDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhum contato disponível para cadastrar compromisso.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número do contato que irá estar no compromisso: ");
            int numeroAmigoEmprestimo = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Contato amigoSelecionado = (Contato)repositorioContato.SelecionarRegistro(numeroAmigoEmprestimo);

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

                numeroEmprestimoEncontrado = repositorioCompromisso.ExisteRegistro(numeroCompromisso);

                if (numeroEmprestimoEncontrado == false)
                    notificador.ApresentarMensagem("Número de compromisso não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroEmprestimoEncontrado == false);
            return numeroCompromisso;
        }
       #endregion
    }
}
