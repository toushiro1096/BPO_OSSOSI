using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSC.Entity
{
    public class CRD1
    {

        public string Address { get; set; }
        public string Street { get; set; }
        public string Block { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string StreetNo { get; set; }
        public string BuildingFloorRoom { get; set; }
        public string Tipo { get; set; }
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        public CRD1()
        {
            this.Address = string.Empty;
            this.Street = string.Empty;
            this.Block = string.Empty;
            this.City = string.Empty;
            this.ZipCode = string.Empty;
            this.County = string.Empty;
            this.StreetNo = string.Empty;
            this.BuildingFloorRoom = string.Empty;
            this.Tipo = string.Empty;
            this.LstCampoUsuario = new List<CampoUsuario>();
        }
    }
}
