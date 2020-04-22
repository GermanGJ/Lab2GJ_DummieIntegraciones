using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Lab2GJ_DI_ServiciosSOA
{
    /// <summary>
    /// Descripción breve de WS_ConsultasCliente
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WS_ConsultasCliente : System.Web.Services.WebService
    {
        [WebMethod]
        public Rta_WS_ConsultasCliente ConsultarCliente(string TipoId, string NoId)
        {
            Rta_WS_ConsultasCliente objRespuesta = new Rta_WS_ConsultasCliente();

            try
            {
                if (TipoId == "?" || NoId == "?")
                {
                    objRespuesta.Codigo = 100;
                    objRespuesta.Descripcion = "Parametros de entrada no validos (TipoId: " + TipoId + " NoId: " + NoId + ")";
                }
                else
                {
                    Cliente objCliente = new Cliente(TipoId, NoId);
                    objRespuesta.Cliente = objCliente;
                    objRespuesta.GetInformacionBasicaCliente();
                }
                return objRespuesta;
            }
            catch(Exception e) 
            {
                if (objRespuesta.Codigo == -1)
                {
                    objRespuesta.Descripcion = e.Message.ToString();
                }
                return objRespuesta;
            }
        }
    }

    public class Rta_WS_ConsultasCliente
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public Cliente Cliente { get; set; }

        public Rta_WS_ConsultasCliente()
        {
            this.Codigo = -1;
            this.Descripcion = null;
            this.Cliente = null;
        }

        public void GetInformacionBasicaCliente()
        {
            if (this.Cliente.ConsultarCliente() != true)
            { 
                this.Codigo = 1;
                this.Descripcion = "Cliente no existe. (Tipo Identificacion: " + this.Cliente.TipoId + " - Numero Identificacion: " + this.Cliente.NumeroId + ").";
                this.Cliente = null;
            }
            else
            {
                this.Codigo = 0;
            }
        }
    }

    public class Cliente
    {
        public string TipoId { get; set; }
        public string NumeroId { get; set; }

        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set;  }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }

        public Cliente ()
        {
            this.TipoId = null;
            this.NumeroId = null;
        }
        public Cliente (string TipoId, string NoId)
        {
            this.TipoId = TipoId;
            this.NumeroId = NoId;
        }

        public Boolean ConsultarCliente()
        {
            Boolean Respuesta = false;

            if (this.TipoId == "01" && this.NumeroId.Substring(this.NumeroId.Length - 1, 1) == "1") 
            {
                //Respuesta de usuario que no existe.
                Respuesta = false;
            }
            else
            {
                //Respuesta de usuario que si existe.
                Respuesta = true;
                this.PrimerNombre = "MARIANA";
                this.SegundoNombre = null;
                this.PrimerApellido = "FLOREZ";
                this.SegundoApellido = "RODRIGUEZ";
            }

            return Respuesta;
        }
    }
}
