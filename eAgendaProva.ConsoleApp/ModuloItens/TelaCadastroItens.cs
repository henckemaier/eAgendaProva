using eAgendaProva.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaProva.ConsoleApp.ModuloItens
{
    public class TelaCadastroItens : TelaBase, ICadastroBasico
    {
        private readonly Notificador notificador;
        private readonly RepositorioItens repositorioItens;

        public TelaCadastroItens(RepositorioItens repositorioItens, Notificador notificador)
            : base("Cadastro de Itens")
        {
            this.repositorioItens = repositorioItens;
            this.notificador = notificador;
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo novo Contato");

            Itens novoItem = ObterContato();

            string statusValidacao = repositorioItens.Inserir(novoItem);

            if (statusValidacao == "REGISTRO_VALIDO")
                notificador.ApresentarMensagem("Contato cadastrado com sucesso!", TipoMensagem.Sucesso);
            else
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Caixa");

            bool temItensCadastrado = VisualizarRegistros("Pesquisando");

            if (temItensCadastrado == false)
            {
                notificador.ApresentarMensagem("Nenhuma caixa cadastrada para poder editar", TipoMensagem.Atencao);
                return;
            }

            int numeroCaixa = ObterNumeroCaixa();

            Itens caixaAtualizada = ObterContato();

            repositorioItens.Editar(numeroCaixa, caixaAtualizada);

            notificador.ApresentarMensagem("Caixa editada com sucesso", TipoMensagem.Sucesso);
        }

        public int ObterNumeroCaixa()
        {
            int numeroCaixa;
            bool numeroCaixaEncontrado;

            do
            {
                Console.Write("Digite o número do item que deseja editar: ");
                numeroCaixa = Convert.ToInt32(Console.ReadLine());

                numeroCaixaEncontrado = repositorioItens.ExisteRegistro(numeroCaixa);

                if (numeroCaixaEncontrado == false)
                    notificador.ApresentarMensagem("Número de item não encontrado, digite novamente", TipoMensagem.Atencao);

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
                    "Nenhum item cadastrado para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int numeroCaixa = ObterNumeroCaixa();

            repositorioItens.Excluir(numeroCaixa);

            notificador.ApresentarMensagem("Contato excluído com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Caixas");

            List<Itens> item = repositorioItens.SelecionarTodos();

            if (item.Count == 0)
                return false;

            for (int i = 0; i < item.Count; i++)
            {
                Itens c = (Itens)item[i];

                Console.WriteLine("Número: " + c.numero);
                Console.WriteLine("Descrição: " + c.Descricao);
                Console.WriteLine("Status: " + c.Status);


                Console.WriteLine();
            }

            Console.ReadLine();

            return true;
        }

        public Itens ObterContato()
        {
            Console.Write("Digite o a descrição: ");
            string descricao = Console.ReadLine();

            Console.Write("Digite o status(aberto [1] | fechado [2]): ");
            string status = Console.ReadLine();

            if(status == "1")
            {
                status = "aberto";
            }
            else if(status == "2")
            {
                status = "fechado";
            }
            else
            {
                Console.WriteLine("Valor Invalido");
                return ObterContato();
            }

            Itens item = new Itens(descricao, status);

            return item;
        }

    }
}
