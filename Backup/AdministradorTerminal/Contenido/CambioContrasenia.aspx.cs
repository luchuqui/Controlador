using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdministradorTerminal.WSControlador;

namespace AdministradorTerminal.Contenido
{
    public partial class CambioContrasenia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void cambiar_contrasenia(Object sender,EventArgs e)
        {
            UsuarioObj usuario = (UsuarioObj)Session["usuario"];
            usuario.contrasenia = txbxPassActual.Text;
            string mensaje = string.Empty;
            if (txbxNueva.Text.Equals(txbxConfirmar.Text))
            {
                mensaje = Globales.servicio.guardar_actualizar_pass_usuario(usuario, txbxNueva.Text, false);
            }
            else {
                mensaje = "Confirmacion de contraseña incorrecta";
            }
            Response.Write("<script language=javascript> alert('" + mensaje + "'); </script>");
        }

        public void limpiar_pantalla(Object sender, EventArgs e)
        {

        }
    }
}
