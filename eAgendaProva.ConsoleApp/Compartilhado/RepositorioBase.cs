using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eAgendaProva.ConsoleApp.Compartilhado
{
    public abstract class RepositorioBase<T> where T : EntidadeBase
    {
        protected readonly List<T> registros;

        protected int contadorNumero;

        public RepositorioBase()
        {
            registros = new List<T>();
        }

        public virtual string Inserir(T entidade)
        {
            entidade.numero = ++contadorNumero;

            registros.Add(entidade);

            return "REGISTRO_VALIDO";
        }

        public void Editar(int numeroSelecionado, T entidade)
        {
            for (int i = 0; i < registros.Count; i++)
            {
                if (registros[i].numero == numeroSelecionado)
                {
                    entidade.numero = numeroSelecionado;
                    registros[i] = entidade;

                    break;
                }
            }
        }

        public bool Excluir(int numeroSelecionado)
        {
            T entidadeSelecionada = SelecionarRegistro(numeroSelecionado);

            if (entidadeSelecionada == null)
                return false;

            registros.Remove(entidadeSelecionada);

            return true;
        }

        public T SelecionarRegistro(int numeroRegistro)
        {
            foreach (T registro in registros)
                if (numeroRegistro == registro.numero)
                    return registro;

            return null;
        }

        public List<T> SelecionarTodos()
        {
            return registros;
        }

        public bool ExisteRegistro(int numeroRegistro)
        {
            foreach (T registro in registros)
                if (registro.numero == numeroRegistro)
                    return true;

            return false;
        }
    }
}
