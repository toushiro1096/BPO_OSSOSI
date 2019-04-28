using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSC.Entity
{
    public class OCRD
    {
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string Address { get; set; }
        public string LicTradNum { get; set; }
        public string Notes { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string MailCity { get; set; }
        public string MailCounty { get; set; }
        public string MailCountr { get; set; }
        public string E_Mail { get; set; }
        public string Tipo { get; set; }
        public string TipoPersona { get; set; }
        public int TipDoc { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public List<CRD1> LstCRD1 { get; set; }
        public string U_GS_EsExt { get; set; }
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        public string JrnlMemo { get; set; }
        public string CodigoExterno { get; set; }
        public int GroupCode { get; set; }
        public string CardType { get; set; }
        public OCRD()
        {
            this.CardCode = string.Empty;
            this.CardName = string.Empty;
            this.Address = string.Empty;
            this.LicTradNum = string.Empty;
            this.Notes = string.Empty;
            this.City = string.Empty;
            this.County = string.Empty;
            this.Country = string.Empty;
            this.MailCity = string.Empty;
            this.MailCounty = string.Empty;
            this.MailCountr = string.Empty;
            this.E_Mail = string.Empty;
            this.Tipo = string.Empty;
            this.TipDoc = 0;
            this.Nombre1 = string.Empty;
            this.Nombre2 = string.Empty;
            this.ApellidoP = string.Empty;
            this.ApellidoM = string.Empty;
            this.LstCRD1 = new List<CRD1>();
            this.U_GS_EsExt = string.Empty;
            this.LstCampoUsuario = new List<CampoUsuario>();
            this.JrnlMemo = string.Empty;
            this.CodigoExterno = string.Empty;
            this.GroupCode = 0;
            this.CardType = string.Empty;
        }

    }

}
