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
    public partial class NuevoEdicionUsuario : System.Web.UI.Page
    {
        private int filaSeleccionada;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (tb_data != null)
            {
                tb_data.Rows.Clear();
            }
                HtmlTableRow fila = new HtmlTableRow();
                HtmlTableCell celdaNum = new HtmlTableCell();
                HtmlTableCell celdaDoc = new HtmlTableCell();
                HtmlTableCell celdaNom = new HtmlTableCell();
                HtmlTableCell celdaEst = new HtmlTableCell();
                HtmlTableCell celdaProceso = new HtmlTableCell();
                celdaNum.InnerText = "#";
                celdaDoc.InnerText = "Documento";
                celdaNom.InnerText = "Nombre";
                celdaEst.InnerText = "Estado";
                celdaProceso.InnerText = "Tarea";
                celdaProceso.ColSpan = 2;
                fila.Cells.Add(celdaNum);
                fila.Cells.Add(celdaNom);
                fila.Cells.Add(celdaDoc);
                fila.Cells.Add(celdaEst);
                fila.Cells.Add(celdaProceso);
                fila.BgColor = "4E4545";
                fila.Style.Value = "color: #FFFFFF";
                tb_data.Rows.Add(fila);
                if (IsPostBack) { 
                  UsuarioObj []  usuarios = (UsuarioObj [])Session["usuariosSistema"];
                  if (usuarios != null)
                  {
                      cargar_datos_tabla(usuarios);
                  }
                }
        }

        public void btn_buscarUsuario(Object sender, EventArgs e)
        {
            string seleccionado = this.rbGroup.SelectedItem.Text;
            UsuarioObj[] lsusr = {};
            Session["usuariosSistema"] = null;
            if (seleccionado.Equals("Documento")) {
                lsusr = Globales.servicio.buscar_usuario_by(this.txbxIngreso.Text,true);
            }
            else if (seleccionado.Equals("Nombre"))
            {
                lsusr = Globales.servicio.buscar_usuario_by(this.txbxIngreso.Text, false);
            }
            cargar_datos_tabla(lsusr);
        }

        public void cargar_datos_tabla(UsuarioObj[] usuarios) {
            limpiar_tabla_datos();
            int i = 1;
            foreach (UsuarioObj usr in usuarios) {
                HtmlTableRow fila = new HtmlTableRow();
                HtmlTableCell celdaNum = new HtmlTableCell();
                HtmlTableCell celdaDoc = new HtmlTableCell();
                HtmlTableCell celdaNom = new HtmlTableCell();
                HtmlTableCell celdaEst = new HtmlTableCell();
                HtmlTableCell celdaProceso = new HtmlTableCell();
                celdaNum.InnerText = i+"";
                celdaDoc.InnerText = usr.cedula;
                celdaNom.InnerText = usr.nombre + " " + usr.apellido;
                string strEstado = "";
                if (usr.estado.Equals("A"))
                {
                    strEstado = "Activo";
                }
                else if (usr.estado.Equals("B"))
                {
                    strEstado = "Bloqueado";
                }
                else if (usr.estado.Equals("E"))
                {
                    strEstado = "Eliminado";
                }
                celdaEst.InnerText = strEstado;

                Button btnEd = new Button();
                btnEd.Text = "Editar";
                btnEd.ID = "btEd_"+i;
                btnEd.Click += new EventHandler(this.eventoBtnEditar);
                Button btnEl = new Button();
                btnEl.Text = "Eliminar";
                btnEl.Click += new EventHandler(this.eventoBtnEliminar);
                celdaProceso.Align = "Center";
                btnEl.ID = "btnEl_"+i;
                btnEl.OnClientClick = "return confirm('¿Desea elminar registro de usuario?');";
                celdaProceso.Controls.Add(btnEd);
                celdaProceso.Controls.Add(btnEl);
                fila.Cells.Add(celdaNum);
                fila.Cells.Add(celdaNom);
                fila.Cells.Add(celdaDoc);
                fila.Cells.Add(celdaEst);
                fila.Cells.Add(celdaProceso);
                
                tb_data.Rows.Add(fila);
                i++;                
            }
            Session["usuariosSistema"] = usuarios;
        }

        public void limpiar_tabla_datos() {
            if (tb_data.Rows.Count > 1) {
                int filas = tb_data.Rows.Count-1;
                for (int i = filas; i > 0; i--) {
                    tb_data.Rows.RemoveAt(i);
                }
            }
        }

        public void eventoBtnEditar(Object sender,EventArgs e) {
            string sr = ((Button)sender).ID;
            string sel = sr.Split('_')[1];
            filaSeleccionada = int.Parse(sel)-1;
            UsuarioObj[] usuarios = (UsuarioObj[])Session["usuariosSistema"];
            Session["usrSeleccionado"] = usuarios[filaSeleccionada];
            Response.Redirect("Usuario.aspx");
            
        }

        public void eventoBtnNuevo(Object sender, EventArgs e)
        {
            Session["usrSeleccionado"] = null;
            Response.Redirect("Usuario.aspx");
        }

        public void eventoBtnEliminar(Object sender, EventArgs e)
        {
            string sr = ((Button)sender).ID;
            string sel = sr.Split('_')[1];
            filaSeleccionada = int.Parse(sel)-1;
            UsuarioObj[] usuarios = (UsuarioObj[])Session["usuariosSistema"];
            UsuarioObj usr = usuarios[filaSeleccionada];
            usr.estado = "E";
            string mensaje = Globales.servicio.guardar_actualizar_usuario(usr,true);
            if (mensaje.StartsWith("Actualizacion de datos usuario correcta"))
            {
                mensaje = "Usuario Eliminado";
                tb_data.Rows.RemoveAt(filaSeleccionada);
            }
            CuadroMensaje mensajeNotificacion = new CuadroMensaje(sender, this.GetType());
            mensajeNotificacion.mostrar_mensaje_alerta(mensaje);
        }
    }
}
