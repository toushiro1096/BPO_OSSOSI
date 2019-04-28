using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSC.Entity
{
    public class PCH1
    {
        public int DocEntry { get; set; }
        public string CodigoExterno { get; set; }
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        public PCH1()
        {
            this.DocEntry = 0;
            this.LstCampoUsuario = new List<CampoUsuario>();
            this.CodigoExterno = string.Empty;
        }
    }
}
