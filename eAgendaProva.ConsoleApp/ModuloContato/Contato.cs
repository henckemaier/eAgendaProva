using eAgendaProva.ConsoleApp.Compartilhado;
using eAgendaProva.ConsoleApp.ModuloCompromisso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaProva.ConsoleApp.ModuloContato
{
    public class Contato : EntidadeBase
    {
        private readonly string nome;
        private readonly string email;
        private readonly string telefone;
        private readonly string empresa;
        private readonly string cargo;

        private readonly Compromisso[] historicoCompromissos = new Compromisso[10];

        public string Nome => nome;

        public string Email => email;

        public string Telefone => telefone;

        public string Empresa => empresa;

        public string Cargo => cargo;

        public Contato(string nome, string email, string telefone, string empresa, string cargo)
        {
            this.nome = nome;
            this.email = email;
            this.telefone = telefone;
            this.empresa = empresa;
            this.cargo = cargo;
        }

        public void RegistrarCompromisso(Compromisso compromisso)
        {
            historicoCompromissos[ObtemPosicaoVazia()] = compromisso;
        }

        #region Métodos privados
        public override string Validar()
        {
            throw new System.NotImplementedException();
        }

        private int ObtemPosicaoVazia()
        {
            for (int i = 0; i < historicoCompromissos.Length; i++)
            {
                if (historicoCompromissos[i] == null)
                    return i;
            }

            return -1;
        }
        #endregion
    }
}

