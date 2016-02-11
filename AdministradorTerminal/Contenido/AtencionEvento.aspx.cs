using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdministradorTerminal.WSControlador;
using System.Web.UI.HtmlControls;

namespace AdministradorTerminal.Contenido
{
    public partial class AtencionEvento : System.Web.UI.Page
    {
        private int filaSeleccionada = 0;
        private AvanceObj[] avancesUsuario;
        private UsuarioObj usuario;
        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = (UsuarioObj)Session["usuario"];
            if (!IsPostBack)
            {
                cargar_cabecera_datos();
            }
            else {
                avancesUsuario = (AvanceObj [])Session["lsAvances"];
            }
        }

        public void cargar_cabecera_datos() {
            avancesUsuario = Globales.servicio.obtener_avance_by_usuario(usuario);
            Session["lsAvances"] = avancesUsuario;
            GridViewIncidente.DataSource = avancesUsuario;
            GridViewIncidente.DataBind();
        }

        protected void edicion_fila_suceso(object sender, GridViewCommandEventArgs e)
        {
            string cmdName = e.CommandName;
            filaSeleccionada = Convert.ToInt16(e.CommandArgument.ToString());
            if (cmdName.Equals("Edicion"))
            {
                Session["AvanceEvento"] = avancesUsuario[filaSeleccionada];
                Response.Redirect("ObservacionEvento.aspx");   
            }

        }

        protected void GridViewIncidente_PageIndexChanged_(object sender, EventArgs e)
        {

        }

        protected void GridViewIncidente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewIncidente.PageIndex = e.NewPageIndex;
            cargar_cabecera_datos();
        }
        

    }
}
