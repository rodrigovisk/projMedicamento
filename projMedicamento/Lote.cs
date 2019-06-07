using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projMedicamento
{
    class Lote
    {
        int id;
        public int qtde;
        DateTime venc;

        public Lote()
        {
            id = 0;
            qtde = 0;
            venc = DateTime.Now;
        }

        public Lote(int id, int qtde, DateTime venc)
        {
            this.id = id;
            this.qtde = qtde;
            this.venc = venc;
        }

        public string toString()
        {
            return id + "-" +qtde + "-" + venc;
        }
    }
}
