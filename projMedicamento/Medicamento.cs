using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projMedicamento
{
    class Medicamento
    {
        int id;
        string nome;
        string laboratorio;
        public Queue<Lote> lotes;

        public Medicamento()
        {
            this.id = 0;
            this.nome = "";
            this.laboratorio = "";
            this.lotes = new Queue<Lote>();
        }

        public Medicamento(int i) { this.id = i; }

        public Medicamento(int id, string nome, string laboratorio)
        {
            this.id = id;
            this.nome = nome;
            this.laboratorio = laboratorio;

            this.lotes = new Queue<Lote>();
        }

        public int qtdeDisponivel()
        {
            int qtde = 0;
            foreach (Lote lote in lotes)
            {
                qtde += lote.qtde;
            }
            return qtde;
        }

        public void comprar(Lote lote)
        {
            this.lotes.Enqueue(lote);
        }

        public bool vender(int qtde)
        {
            do
            {
                if (qtdeDisponivel() >= qtde && qtde > 0)
                {
                    if (lotes.Peek().qtde > qtde)
                    {
                        lotes.Peek().qtde -= qtde;
                        return true;
                    }
                    else if (lotes.Peek().qtde <= qtde)
                    {
                        qtde -= lotes.Peek().qtde;
                        lotes.Dequeue();
                    }
                }
                else
                {
                    return false;
                }
            } while (qtde != 0);

            return true;
        }

        public string toString()
        {
            return id + "-" +nome + "-" +laboratorio + "-" + qtdeDisponivel();
        }

        public bool adicionarLote(Lote lote)
        {
            if (pesquisar(lote) == null)
            {
                lotes.Enqueue(lote);
                return true;
            }
            return false;
        }
        public Lote pesquisar(Lote lote)
        {
            foreach (Lote l in lotes)
            {
                if (l.Equals(lote))
                    return l;
            }
            return null;
        }
        public override bool Equals(object obj)
        {
            var medicamento = obj as Medicamento;
            return medicamento != null && id == medicamento.id;
        }
    }
}
