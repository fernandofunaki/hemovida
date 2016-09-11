using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    public class Contato
    {
        private int _id;
        public virtual int Id { get { return _id; } }
        public virtual string Nome { get; set; }
        public virtual string Sobrenome { get; set; }
    }

    public interface IContatoRepositorio
    {
        void Salvar(Contato contato);
        void Excluir(Contato contato);
        void Alterar(Contato contato);
        Contato ObterPor(int id);
        IQueryable<T> ObterTodos<T>();
    }
}
