using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSC.Service.MODULOS
{
    public class ErrorLog
    {
        public static void RegistrarLog(string metodo, string company, string key, string obj, int type = 0, string error = "")
        {
        subir:
            string xRuta = System.Web.Hosting.HostingEnvironment.MapPath("~");
            string xArchivo = @"Log/" + metodo + ".log";
            if (type == 1)
                xArchivo = @"LogErrors/" + metodo + ".log";
            string msterror_three = DateTime.Now.ToString();
            string Path = string.Concat(xRuta.ToString(), xArchivo.ToString());

            if (!System.IO.File.Exists(Path))
            {
                string pathString = xRuta.ToString();
                System.IO.Directory.CreateDirectory(pathString);
                pathString = pathString + xArchivo;
                System.IO.StreamWriter archivo = System.IO.File.AppendText(pathString);
                archivo.Close();
                goto subir;
            }
            try
            {
                System.IO.File.SetAttributes(Path, System.IO.FileAttributes.Normal);
                System.IO.StreamWriter text = System.IO.File.AppendText(Path);
                text.WriteLine("---------------------------------------------------------------------------------");
                text.WriteLine("company : " + company);
                text.WriteLine("key : " + key);
                text.WriteLine("datetime : " + msterror_three);
                if (type == 1)
                    text.WriteLine("error : " + error);
                text.WriteLine("object : " + obj);
                text.WriteLine("---------------------------------------------------------------------------------");
                text.Close();
                text.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}