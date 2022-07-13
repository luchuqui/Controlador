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
                ATMs_Registrados.Items.Add(new ListItem("Seleccione un Terminal", "999901"));
            }
            else {
                ATMs_Registrados.Items.Add(new ListItem("Asigne un Terminal", "999901"));
            }
            foreach (AtmObj atm in atms)
            {
                ATMs_Registrados.Items.Add(new ListItem(atm.codigo +" - "+atm.ubicacion, atm.ip));
            }
        }


        public void seleccionar_terminal(Object sender, EventArgs e) 
        
        {
        }

        public void envioComando(Object sender, EventArgs e)
        {
            if (ATMs_Registrados.SelectedItem.Value.Equals("999901")) {
                this.lblMenEnv.Text = "Seleccione un terminal";
                return;
            }
            string ipAtm = ATMs_Registrados.SelectedValue;
            string cmd = this.rbGroup.SelectedValue;
            AtmObj ter = new AtmObj();
            ter.ip = ipAtm;
            ter.id_atm = 1;
            this.lblMenEnv.Text = rbGroup.SelectedItem.Text;
            this.lblMensConf.Text = Globales.servicio.enviarComandoTerminal(ter,cmd);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargar_terminales_asignados();
            }
        }
    }
}
