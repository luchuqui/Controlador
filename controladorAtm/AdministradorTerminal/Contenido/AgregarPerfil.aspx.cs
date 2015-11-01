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
using System.Collections.Generic;
using System.Drawing;

namespace AdministradorTerminal.Contenido
{
    public partial class AgregarPerfil : System.Web.UI.Page
    {

        private MenuObj [] menusPadres;
        private MenuObj[] menusSeleccionado;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            menusPadres = Globales.servicio.obtener_menu_codigo(0);
            Tablemenu.Rows.Clear ();
            foreach(MenuObj menu in menusPadres)
            {
                TableRow fila = new TableRow();
                TableCell columnaPadre = new TableCell();
                columnaPadre.ColumnSpan = 3;
                fila.BackColor = Color.Gray;
                fila.ForeColor = Color.White;
                columnaPadre.Text = menu.nombre;
                fila.Cells.Add(columnaPadre);
                Tablemenu.Rows.Add(fila);
                MenuObj[] menusHijos = Globales.servicio.obtener_menu_codigo(menu.id_menu);
                foreach (MenuObj menuHijo in menusHijos)
                {
                    TableRow filaHijo = new TableRow();
                    TableCell columnaEspacio = new TableCell();
                    TableCell columnaNombre = new TableCell();
                    TableCell columnaCheck = new TableCell();
                    columnaNombre.Text = menuHijo.nombre;
                    columnaEspacio.Text = menuHijo.id_menu.ToString();
                    columnaEspacio.ForeColor = Color.White;
                    columnaCheck.Controls.Add(new CheckBox());
                    filaHijo.Cells.Add(columnaEspacio);
                    filaHijo.Cells.Add(columnaNombre);
                    filaHijo.Cells.Add(columnaCheck);
                    Tablemenu.Rows.Add(filaHijo);
                }
            }
        }

        public void btn_guardar_datos(Object sender, EventArgs e) 
        {
            menusSeleccionado = new MenuObj[Tablemenu.Rows.Count-menusPadres.Length];
            string mensaje = "";
            int indice = 0;
            bool guardar = false;
            PerfilObj perfil = new PerfilObj();
            perfil.nombre = txbxNombrePerfil.Text;
            perfil.descripcion = txbxDescripcion.Text;
            for (int i = 0; i < Tablemenu.Rows.Count; i++) {
                string s = string.Empty;
                if (Tablemenu.Rows[i].Cells.Count > 2)
                {
                    CheckBox c = (CheckBox)Tablemenu.Rows[i].Cells[2].Controls[0];
                    if (c.Checked) { 
                        MenuObj m = new MenuObj();
                        m.id_menu = int.Parse(Tablemenu.Rows[i].Cells[0].Text);
                        menusSeleccionado[indice] = m;
                        guardar = true;
                        indice++;
                    }
                }
            }
            if (guardar)
            {
               mensaje =  Globales.servicio.guardar_actualizar_Perfil(perfil, menusSeleccionado, false);
               Response.Write("<script language=javascript> alert('"+mensaje+"'); </script>");
            }
            else {
                Response.Write("<script language=javascript> alert('Seleccione al menos un menu'); </script>");            
            }

        }

        public void btn_limpiar_datos(Object sender, EventArgs e)
        {
            txbxDescripcion.Text = "";
            txbxNombrePerfil.Text = "";
        }

       

    }
}
