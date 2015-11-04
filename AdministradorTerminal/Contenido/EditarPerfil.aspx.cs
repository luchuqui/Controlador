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
using System.Drawing;
using System.Collections.Generic;

namespace AdministradorTerminal.Contenido
{
    public partial class EditarPerfil : System.Web.UI.Page
    {

        private static PerfilObj[] perfiles;
        private static int contMenuItem;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (cboxPerfiles.Items.Count == 0)
            
            if (!this.IsPostBack)
            {
                cargar_datos();
                caragar_menu_items();
                //Tablemenu = (Table)Session["MenuOp"];
            }
            /*if ( !IsPostBack ||Tablemenu == null)
            {
                //if (Tablemenu.Rows.Count == 0) {
                menusPadres = Globales.servicio.obtener_menu_codigo(0);
                caragar_menu_items();
            }*/
            /*else {
                tablatmp =  (Table)Session["MenuOp"];
                for (int i = 0; i < tablatmp.Rows.Count; i++) {
                    Tablemenu.Rows.Add(tablatmp.Rows[i]);
                }
            }*/
        }

        protected void RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int indice = e.RowIndex;
            CheckBox cb = (CheckBox)permisoUsuario.Rows[indice].Cells[3].Controls[0];
            PerfilObj perfil = new PerfilObj();
            perfil.id = int.Parse(cboxPerfiles.SelectedValue);
            MenuObj menu =  new MenuObj();
            menu.id_menu = int.Parse(((TextBox)(permisoUsuario.Rows[indice].Cells[1].Controls[0])).Text);
            string mensaje = Globales.servicio.guardar_actualizar_perfil_(perfil, menu,cb.Checked);
            CuadroMensaje ms = new CuadroMensaje(sender, this.GetType());
            ms.mostrar_mensaje_alerta(mensaje);
            caragar_menu_items();
            permisoUsuario.EditIndex = -1;
            /*permisoUsuario.Rows[indice].Cells[0].Enabled = false;
            permisoUsuario.Rows[indice].Cells[1].Enabled = false;
            permisoUsuario.Rows[indice].Cells[2].Enabled = false;
            permisoUsuario.Rows[indice].Cells[3].Enabled = false;*/
        }

        protected void RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        { 
        //no se hace nada
            //int indice = permisoUsuario.EditIndex = e.RowIndex;
            //int otro = permisoUsuario.Rows[indice].Cells.Count;
            permisoUsuario.EditIndex = -1;
            caragar_menu_items();
            //e.Cancel = true;
            /*permisoUsuario.Rows[indice].Cells[0].Enabled = false;
            permisoUsuario.Rows[indice].Cells[1].Enabled = false;
            permisoUsuario.Rows[indice].Cells[2].Enabled = false;
            permisoUsuario.Rows[indice].Cells[3].Enabled = false;*/
        }

        protected void RowEditing(object sender, GridViewEditEventArgs e)
        {

            caragar_menu_items();
            int indice = e.NewEditIndex;
                permisoUsuario.EditIndex = e.NewEditIndex;
            permisoUsuario.Rows[indice].Cells[0].Enabled = false;
            permisoUsuario.Rows[indice].Cells[1].Enabled = false;
            permisoUsuario.Rows[indice].Cells[2].Enabled = false;
            permisoUsuario.Rows[indice].Cells[3].Enabled = true;
            
        }

        private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
        {
            if (groupName == "menuPadre")
            {
                row.Cells[0].Font.Bold = true;
                row.ForeColor = Color.White;
                row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#A4A4A4");
                row.Cells[0].Text = string.Format("{0}", values[0].ToString());
            }
        }

        private void caragar_menu_items() {
            PerfilObj perfil = new PerfilObj();
            if (perfiles.Length > 0)
            {
                perfil.id = int.Parse( cboxPerfiles.SelectedValue);
            }
            else
            {
                perfil.id = 0;
            }
            BeanMenuPerfil [] lsList = Globales.servicio.obtener_menu_usuario_perfil(perfil);
            contMenuItem = lsList.Length;
            permisoUsuario.DataSource = lsList;
            permisoUsuario.DataBind();

            GridViewHelper helper = new GridViewHelper(this.permisoUsuario);
            helper.RegisterGroup("menuPadre", true, true);
            helper.GroupHeader += new GroupEvent(helper_GroupHeader);
            helper.ApplyGroupSort();
            permisoUsuario.DataBind();

        }

        private void cargar_datos() {
        
            perfiles = Globales.servicio.obtener_perfil(true);
            foreach (PerfilObj perfil in perfiles)
            {
                ListItem ls = new ListItem(perfil.nombre, perfil.id.ToString());
                cboxPerfiles.Items.Add(ls);
            }
            try
            {
                txbxDescripcion.Text = perfiles[0].descripcion;
                txbxNombrePerfil.Text = perfiles[0].nombre;
            }
            catch (Exception e) {
                string ms = e.Message;
            }
        }

        public void Seleccion_perfil(object sender, EventArgs e)
        {
            try
            {
                if (cboxPerfiles.Items.Count > 0)
                {foreach (PerfilObj perfil in perfiles)
                    {
                        if (cboxPerfiles.SelectedValue.Equals(perfil.id.ToString()))
                        {
                            txbxDescripcion.Text = perfil.descripcion;
                            txbxNombrePerfil.Text = perfil.nombre;
                            caragar_menu_items();
                            break;
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException x)
            {
                string error = x.Message;
            }
        }

        private void eventocambio(Object sender, EventArgs e) {
            string sr = ((CheckBox)sender).ID;
            string sel = sr.Split('_')[1];
            int filaSeleccionada = int.Parse(sel);
        }

        public void btn_guardar_datos(Object sender, EventArgs e)
        {
            //Tablemenu = (Table)Session["MenuOp"];
            //CuadroMensaje mensajeNotificacion = new CuadroMensaje(sender, this.GetType());
            //menusSeleccionado = new MenuObj[Tablemenu.Rows.Count - menusPadres.Length];
            //string mensaje = "";
            //int indice = 0;
            //bool guardar = false;
            //PerfilObj perfil = new PerfilObj();
            //perfil.nombre = txbxNombrePerfil.Text;
            //perfil.descripcion = txbxDescripcion.Text;
            //for (int i = 0; i < Tablemenu.Rows.Count; i++)
            //{
            //    string s = string.Empty;
            //    if (Tablemenu.Rows[i].Cells.Count > 2)
            //    {
            //        CheckBox c = (CheckBox)Tablemenu.Rows[i].Cells[2].Controls[0];
            //        if (c.Checked)
            //        {
            //            MenuObj m = new MenuObj();
            //            m.id_menu = int.Parse(Tablemenu.Rows[i].Cells[0].Text);
            //            menusSeleccionado[indice] = m;
            //            guardar = true;
            //            indice++;
            //        }
            //    }
            //}
            //if (guardar)
            //{
            //    perfil.id = int.Parse( cboxPerfiles.SelectedValue);
            //    mensaje = Globales.servicio.guardar_actualizar_Perfil(perfil, menusSeleccionado, true);
            //    mensajeNotificacion.mostrar_mensaje_alerta(mensaje);
                
            //}
            //else
            //{
            //    mensajeNotificacion.mostrar_mensaje_alerta("Seleccione al menos un menu");
            //}

        }

        public void btn_limpiar_datos(Object sender, EventArgs e)
        {
            txbxDescripcion.Text = "";
            txbxNombrePerfil.Text = "";
        }
    }
}


    