using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services.Protocols;
using AdministradorTerminal.WSControlador;
using System.Data;


namespace AdministradorTerminal
{
    public partial class MenuUsuario : System.Web.UI.MasterPage
    {
        private UsuarioObj usesion;
        public MenuObj [] menusItems;
        public int conmenu = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.rptAccordian.DataSource = table;
            //this.rptAccordian.DataBind();
            //if (!IsPostBack)
            //{
            //    usesion = new UsuarioObj();
            //    usesion = (UsuarioObj)Session["usuario"];
            //    menusItems = Globales.servicio.obtenerMenuUsuario(usesion);
            //    conmenu++;
            //    menuPrincipal.Items.Clear();
            //    if (menusItems.Length == 0)
            //    {
            //        Session.RemoveAll();
            //        Response.Redirect("Default.aspx");
            //    }
            //    this.lblNick.Text = usesion.nombre + " " + usesion.apellido;
            //    foreach (MenuObj mprincipal in menusItems)
            //    {
            //        if (mprincipal.codigo_menu_padre == 0)
            //        {
            //            MenuItem padre = new MenuItem();
            //            //padre.NavigateUrl = mprincipal.url;
            //            padre.Value = mprincipal.id_menu.ToString();
            //            padre.Text = mprincipal.nombre;
            //            menuPrincipal.Items.Add(padre);
            //        }
            //        else
            //        {
            //            foreach (MenuItem pm in menuPrincipal.Items)
            //            {
            //                if (pm.Value.Equals(mprincipal.codigo_menu_padre.ToString()))
            //                {
            //                    MenuItem hijo = new MenuItem();
            //                    hijo.NavigateUrl = mprincipal.url;
            //                    hijo.Value = mprincipal.id_menu.ToString();
            //                    hijo.Text = mprincipal.nombre;
            //                    pm.ChildItems.Add(hijo);
            //                }
            //            }
            //        }
            //    }

            //    if (usesion.cambio_contrasenia)
            //    {
            //        usesion.cambio_contrasenia = false;
            //        Session["usuario"] = usesion;
            //        Response.Redirect("CambioContrasenia.aspx");
            //    }
            //}
        }

        public void cerrar_sesion(Object sender,
                                   EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("Default.aspx");
        }
    }
}
