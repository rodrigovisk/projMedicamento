using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projMedicamento
{
    class Medicamentos
    {
        public List<Medicamento> listaMedicamentos;

        public Medicamentos()
        {
            listaMedicamentos = new List<Medicamento>();
        }

        public void adicionar(Medicamento medicamento)
        {
            if (pesquisar(medicamento) == null)
                listaMedicamentos.Add(medicamento);
        }

        public bool deletar(Medicamento medicamento)
        {
            Medicamento pesquisaMedicamento = pesquisar(medicamento);

            if (pesquisaMedicamento.lotes.Count == 0 || pesquisaMedicamento.qtdeDisponivel() == 0)
            {
                listaMedicamentos.Remove(medicamento);
                return true;
            }
            else return false;
        }
        public Medicamento pesquisar(Medicamento medicamento)
        {
            return this.listaMedicamentos.FirstOrDefault(item => item.Equals(medicamento));
        }
    }
}
