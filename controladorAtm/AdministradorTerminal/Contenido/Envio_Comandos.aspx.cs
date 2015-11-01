using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AdministradorTerminal.WSControlador;

namespace AdministradorTerminal.Contenido
{
    public partial class Envio_Comandos : System.Web.UI.Page
    {
        private UsuarioObj usesion;
        public void cargar_terminales_asignados()
        {
            usesion = new UsuarioObj();
            usesion = (UsuarioObj)Session["usuario"];
            AtmObj[] atms = Globales.servicio.obtenerTerminalPorUsuario(usesion);
            if (atms.Length > 0)
            {
                ATMs_Registrados.Items.Add(new ListItem("Seleccione un Terminal", "001"));
            }
            else {
                ATMs_Registrados.Items.Add(new ListItem("Asigne un Terminal", "001"));
            }
            foreach (AtmObj atm in atms)
            {
                ATMs_Registrados.Items.Add(new ListItem(atm.codigo, atm.id_atm.ToString()));
            }
        }


        public void seleccionar_terminal(Object sender, EventArgs e) 
        
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            cargar_terminales_asignados();
        }
    }
}
