using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdministradorTerminal.WSControlador;

namespace AdministradorTerminal.Contenido
{
    public partial class Usuario : System.Web.UI.Page
    {
        private PerfilObj[] perfiles;
        private bool nuevo;
        protected string estadoSeleccionado;
        protected string perfilSeleccionado;
        protected UsuarioObj usuarioSelecionado;
        protected void Page_Load(object sender, EventArgs e)
        {
            UsuarioObj usr = (UsuarioObj)Session["usrSeleccionado"];
            cargar_datos(usr);
            cargarDatosUsuario(usr);
        }

        public void cargar_datos(UsuarioObj usr) {
            estadoSeleccionado = lsEstado.SelectedValue;
            perfilSeleccionado = lsPerfiles.SelectedValue;
            lsPerfiles.Items.Clear();
            lsEstado.Items.Clear();
            perfiles = Globales.servicio.obtener_perfil(true);
            foreach (PerfilObj perfil in perfiles )
            {
                ListItem item = new ListItem(perfil.nombre, perfil.id.ToString());
                lsPerfiles.Items.Add(item);
                
            }
            lsEstado.Items.Add(new ListItem("Bloqueado","B"));
            lsEstado.Items.Add(new ListItem("Activo", "A"));
            if (usr != null)
            {
                nuevo = true;
                if (!IsPostBack)
                {
                    perfilSeleccionado = usr.id_perfil.ToString();
                    estadoSeleccionado = usr.estado;
                    
                }
                lsPerfiles.SelectedValue = perfilSeleccionado;
                lsEstado.SelectedValue = estadoSeleccionado;            
                lsEstado.Enabled = true;
                btnResetear.Visible = true;
            }
            else {
                lsEstado.SelectedValue = "A";
                lsPerfiles.SelectedValue = perfilSeleccionado;
                lsEstado.Enabled = false;
                nuevo = false;
                btnResetear.Visible = false;
            }
        }

        public void cargarDatosUsuario(UsuarioObj usuario)
        {
            if (usuario != null) {
                if (!IsPostBack)
                {
                    txbxNombre.Text = usuario.nombre;
                    txbxApellido.Text = usuario.apellido;
                    txbxCorreo.Text = usuario.correo;
                    txbxDocumento.Text = usuario.cedula;
                    txbxTelefono.Text = usuario.telefono;
                    txbxIdentificadorUsuario.Text = usuario.id.ToString();
                }
            }
        }

        public void btn_cancelar(object sender, EventArgs e) {
            Response.Redirect("NuevoEdicionUsuario.aspx");
            Session["usrSeleccionado"] = null;
            Session["usuariosSistema"] = null;
        }

        public void btn_guardarActualizarDatos(object sender, EventArgs e)
        {
            UsuarioObj usuario = (UsuarioObj)Session["usrSeleccionado"];
            if (usuario == null) {
                usuario = new UsuarioObj();
            }
            usuario.nombre = txbxNombre.Text;
            usuario.apellido = txbxApellido.Text;
            usuario.correo = txbxCorreo.Text;
            usuario.cedula = txbxDocumento.Text;
            usuario.telefono = txbxTelefono.Text;
            usuario.id_perfil = int.Parse(lsPerfiles.SelectedValue);
            
            if (!string.IsNullOrEmpty(txbxIdentificadorUsuario.Text)) {
                usuario.id = int.Parse(txbxIdentificadorUsuario.Text);
            }
            string estado = "A";
            if (lsEstado.SelectedValue.Equals("B")) {
                estado = "B";
            }
            usuario.estado = estado;
            string mensaje = Globales.servicio.guardar_actualizar_usuario(usuario,nuevo);
            CuadroMensaje mensajeNotificacion = new CuadroMensaje(sender, this.GetType());
           // mensajeNotificacion.mostrar_mensaje_alerta(mensaje);
            lblmensaje.Text= mensaje;
        }

        public void cambiarContrasenia(object sender, EventArgs e)
        {
            UsuarioObj usuario = (UsuarioObj)Session["usrSeleccionado"];
            string mensaje = "";
            if (usuario != null) {
               mensaje = Globales.servicio.guardar_actualizar_pass_usuario(usuario, "", true);
               CuadroMensaje mensajeNotificacion = new CuadroMensaje(sender, this.GetType());
               mensajeNotificacion.mostrar_mensaje_alerta(mensaje);
            }
            
        }
    }

    
}
