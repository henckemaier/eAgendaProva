using eAgendaProva.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaProva.ConsoleApp.ModuloContato
{
    public class TelaCadastroContato : TelaBase, ICadastroBasico
    {
        private readonly Notificador notificador;
        private readonly RepositorioContato repositorioContato;

        public TelaCadastroContato(RepositorioContato repositorioContato, Notificador notificador)
            : base("Cadastro de Caixas")
        {
            this.repositorioContato = repositorioContato;
            this.notificador = notificador;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo novo Contato");

            Contato novaCaixa = ObterContato();

            string statusValidacao = repositorioContato.Inserir(novaCaixa);

            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Contato cadastrado com sucesso!", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Caixa");

            bool temContatosCadastrados = VisualizarRegistros("Pesquisando");

            if (temContatosCadastrados == false)
            {
                notificador.ApresentarMensagem("Nenhuma caixa cadastrada para poder editar", TipoMensagem.Atencao);
                return;
            }

            int numeroCaixa = ObterNumeroCaixa();

            Contato caixaAtualizada = ObterContato();

            repositorioContato.Editar(numeroCaixa, caixaAtualizada);

            notificador.ApresentarMensagem("Caixa editada com sucesso", TipoMensagem.Sucesso);
        }

        public int ObterNumeroCaixa()
        {
            int numeroCaixa;
            bool numeroCaixaEncontrado;

            do
            {
                Console.Write("Digite o número da caixa que deseja editar: ");
                numeroCaixa = Convert.ToInt32(Console.ReadLine());

                numeroCaixaEncontrado = repositorioContato.ExisteRegistro(numeroCaixa);

                if (numeroCaixaEncontrado == false)
                    notificador.ApresentarMensagem("Número de caixa não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroCaixaEncontrado == false);
            return numeroCaixa;
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Caixa");

            bool temCaixasCadastradas = VisualizarRegistros("Pesquisando");

            if (temCaixasCadastradas == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhum contato cadastrado para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int numeroCaixa = ObterNumeroCaixa();

            repositorioContato.Excluir(numeroCaixa);

            notificador.ApresentarMensagem("Contato excluído com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Caixas");

            List<Contato> contato = repositorioContato.SelecionarTodos();
            

            if (contato.Count == 0)
                return false;

            for (int i = 0; i < contato.Count; i++)
            {
                Contato c = (Contato)contato[i];

                Console.WriteLine("Número: " + c.numero);
                Console.WriteLine("Nome: " + c.Nome);
                Console.WriteLine("Email: " + c.Email);
                Console.WriteLine("Telefone: " + c.Telefone);
                Console.WriteLine("Empresa: " + c.Empresa);
                Console.WriteLine("Cargo: " + c.Cargo);

                Console.WriteLine();
            }

            Console.ReadLine();

            return true;
        }

        public Contato ObterContato()
        {
            Console.Write("Digite o nome: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o email: ");
            string email = Console.ReadLine();

            Console.Write("Digite o telefone: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite a empresa: ");
            string empresa = Console.ReadLine();

            Console.Write("Digite o cargo: ");
            string cargo = Console.ReadLine();


            bool etiquetaJaUtilizada;

            do
            {
                etiquetaJaUtilizada = repositorioContato.ContatoJaUtilizado(nome);

                if (etiquetaJaUtilizada)
                {
                    notificador.ApresentarMensagem("Nome já utilizado, por gentileza informe outro", TipoMensagem.Erro);

                    Console.Write("Digite a nome: ");
                    nome = Console.ReadLine();
                }

            } while (etiquetaJaUtilizada);

            Contato contato = new Contato(nome, email, telefone, empresa, cargo);

            return contato;
        }

    }
}