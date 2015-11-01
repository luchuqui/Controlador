using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdministradorTerminal.WSControlador;


namespace AdministradorTerminal
{
    public partial class _Default : System.Web.UI.Page
    {
        UsuarioObj usuariow;
  //      private bool mostrarMensaje;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            usuariow = new UsuarioObj();
            Session.Clear();
            string usuario = LoginUsuario.UserName;
            string pass = LoginUsuario.Password;

            usuariow = Globales.servicio.login_usuario(usuario, pass);
            if (!string.IsNullOrEmpty(usuariow.cedula))
            {
                //Page.Master.FindControl("");
                Session["usuario"] = usuariow;
                Response.Redirect("MensajeBienvenida.aspx");
            }
            else
            {
                this.LoginUsuario.FailureText = usuariow.correo;
            }
        }

    }
}
