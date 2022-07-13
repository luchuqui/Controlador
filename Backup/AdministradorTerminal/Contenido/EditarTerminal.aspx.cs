using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdministradorTerminal.WSControlador;

namespace AdministradorTerminal.Contenido
{
    public partial class EditarTerminal : System.Web.UI.Page
    {
        private AtmObj[] terminal;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (cboxModelo.Items.Count == 0) {
                cargar_datos();
            }
        }

        public void actualizar_datos(object sender, EventArgs e) { 
        
        }

        public void btn_buscar_datos(object sender, EventArgs e)
        {
            terminal = Globales.servicio.buscar_terminal(txbxBusqueda.Text,true);
            if (terminal[0].codigo== null)
            {
                Response.Write("<script language=javascript> alert('" + terminal[0].ubicacion + "'); </script>");
            }
            else {
                txbxCodigoTerminal.Text = terminal[0].codigo;
                txbxDescripcion.Text = terminal[0].ubicacion;
                txbxDirIP.Text = terminal[0].ip;
                cboxModelo.SelectedValue = terminal[0].id_modelo.ToString();
                labelIdCodigo.Text = terminal[0].id_atm.ToString();
            }
        }

        public void limpiar_datos(object sender, EventArgs e) { 
        
        }
        private void cargar_datos(){
            cboxModelo.Items.Clear();
            ModeloObj [] modelos = Globales.servicio.obtener_modelo_terminal(true);
            foreach(ModeloObj modelo in modelos){
                ListItem item = new ListItem(modelo.nombre, modelo.id_modelo.ToString());
                cboxModelo.Items.Add(item);
            }
        }
    }
}
