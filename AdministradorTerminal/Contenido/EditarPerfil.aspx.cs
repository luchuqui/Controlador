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
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (cboxPerfiles.Items.Count == 0)
            
            if (!this.IsPostBack)
            {
                //cargar_datos();
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
            int indice = permisoUsuario.EditIndex = e.RowIndex;
        }

        protected void RowCancelingEdit(object sender, GridViewCancelEditEventArgs e) { 
        //no se hace nada
            int indice = permisoUsuario.EditIndex = e.RowIndex;
            int otro = permisoUsuario.Rows[indice].Cells.Count;
            permisoUsuario.Rows[indice].Cells[0].Enabled = false;
            permisoUsuario.Rows[indice].Cells[1].Enabled = false;
            permisoUsuario.Rows[indice].Cells[2].Enabled = false;
            permisoUsuario.Rows[indice].Cells[3].Enabled = false;
        }

        protected void RowEditing(object sender, GridViewEditEventArgs e)
        {

            int indice = permisoUsuario.EditIndex = e.NewEditIndex;
            caragar_menu_items();
            permisoUsuario.Rows[indice].Cells[0].Enabled = false;
            permisoUsuario.Rows[indice].Cells[1].Enabled = false;
            permisoUsuario.Rows[indice].Cells[2].Enabled = false;
            permisoUsuario.Rows[indice].Cells[3].Enabled = false;

        }

        private void caragar_menu_items() {
            PerfilObj perfil = new PerfilObj();
            perfil.id = 1;
            BeanMenuPerfil [] lsList = Globales.servicio.obtener_menu_usuario_perfil(perfil);
            permisoUsuario.DataSource = lsList;
            permisoUsuario.DataBind();
            
            //if (Tablemenu == null)
            //{
            //    Tablemenu = new Table();
            //}

            //if (IsPostBack) {
            //    return;
            //}
            //foreach (MenuObj menu in menusPadres)
            //{
            //    TableRow fila = new TableRow();
            //    TableCell columnaPadre = new TableCell();
            //    columnaPadre.ColumnSpan = 3;
            //    fila.BackColor = Color.Gray;
            //    fila.ForeColor = Color.White;
            //    columnaPadre.Text = menu.nombre;
            //    fila.Cells.Add(columnaPadre);
            //    Tablemenu.Rows.Add(fila);
            //    menusHijos = Globales.servicio.obtener_menu_codigo(menu.id_menu);
            //    foreach (MenuObj menuHijo in menusHijos)
            //    {
            //        TableRow filaHijo = new TableRow();
            //        TableCell columnaEspacio = new TableCell();
            //        TableCell columnaNombre = new TableCell();
            //        TableCell columnaCheck = new TableCell();
            //        columnaNombre.Text = menuHijo.nombre;
            //        columnaEspacio.Text = menuHijo.id_menu.ToString();
            //        columnaEspacio.ForeColor = Color.White;
            //        columnaCheck.Controls.Add(new CheckBox());
            //        filaHijo.Cells.Add(columnaEspacio);
            //        filaHijo.Cells.Add(columnaNombre);
            //        filaHijo.Cells.Add(columnaCheck);
            //        Tablemenu.Rows.Add(filaHijo);
            //    }
            //}
            //Session["MenuOp"] = Tablemenu;
            
        }

        private void cargar_datos() {
        
            perfiles = Globales.servicio.obtener_perfil(true);
            foreach (PerfilObj perfil in perfiles)
            {
                ListItem ls = new ListItem(perfil.nombre, perfil.id.ToString());
                cboxPerfiles.Items.Add(ls);
            } 
        }

        public void Seleccion_perfil(object sender, EventArgs e)
        {
            //try
            //{
            //    if (cboxPerfiles.Items.Count > 0)
            //    {
            //        Tablemenu = (Table)Session["MenuOp"];
            //        foreach (PerfilObj perfil in perfiles)
            //        {
            //            if (cboxPerfiles.SelectedValue.Equals(perfil.id.ToString()))
            //            {
            //                txbxDescripcion.Text = perfil.descripcion;
            //                txbxNombrePerfil.Text = perfil.nombre;
            //                UsuarioObj usuario = new UsuarioObj();
            //                usuario.id_perfil = perfil.id;
            //                MenuObj[] menus = Globales.servicio.obtenerMenuUsuario(usuario);
            //                for (int i = 0; i < Tablemenu.Rows.Count; i++)
            //                {
            //                    if (Tablemenu.Rows[i].Cells.Count > 2)
            //                    {
            //                        CheckBox c = (CheckBox)Tablemenu.Rows[i].Cells[2].Controls[0];
            //                        c.CheckedChanged += new EventHandler(this.eventocambio);
            //                        c.ID = "c_" + i;
            //                        int idMenu = int.Parse(Tablemenu.Rows[i].Cells[0].Text);
            //                        foreach (MenuObj menu in menus)
            //                        {
            //                            if (idMenu == menu.id_menu)
            //                            {
            //                                c.Checked = true;
            //                                Tablemenu.Rows[i].Cells[2].Controls.Clear();
            //                                Tablemenu.Rows[i].Cells[2].Controls.Add(c);
            //                                break;
            //                            }
            //                        }
            //                    }

            //                }
            //                break;
            //            }
            //        }
            //        Session["MenuOp"] = Tablemenu;
            //    }
            //}
            //catch (IndexOutOfRangeException x) {
            //    string error = x.Message;
            //}
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


    