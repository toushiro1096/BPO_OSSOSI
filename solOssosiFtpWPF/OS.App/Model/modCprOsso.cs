using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS.App.Model
{
    public class modCprOsso
    {
        // Cab
        public string Ruc { get; set; }
        public string Supplier { get; set; }
        public string Location { get; set; }
        public string ReceiptName { get; set; }
        public string ReceiptDate { get; set; }
        public string InvoiceName { get; set; }
        public string InvoiceDate { get; set; }
        public string State { get; set; }
        // Det
        //public string Article { get; set; }
        public double NetValue { get; set; }
        public double GrossValue { get; set; }
        //public string MajorGroup { get; set; }
        public string FamilyGroup { get; set; }
    }
}
