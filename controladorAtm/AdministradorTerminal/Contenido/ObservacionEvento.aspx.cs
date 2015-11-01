using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdministradorTerminal.WSControlador;

namespace AdministradorTerminal.Contenido
{
    public partial class ObservacionEvento : System.Web.UI.Page
    {
        private AvanceObj alarma;
        protected void Page_Load(object sender, EventArgs e)
        {
            alarma = (AvanceObj)Session["AvanceEvento"];
            if (alarma != null)
            {
                cargar_datos_alarma(alarma);
            }
        }

        private void cargar_datos_alarma(AvanceObj alarmaLoad) {
            alarma = alarmaLoad;
            lblFechaRegistro.Text = alarmaLoad.fecha_registro;
            lblNumeroIncidencia.Text = alarmaLoad.id_alarma.ToString();
            UsuarioObj ua = new UsuarioObj();
            UsuarioObj un = new UsuarioObj();
            ua.id = alarmaLoad.usuario_atiende;
            un.id = alarmaLoad.usuario_notifica;
            ua = Globales.servicio.obtener_usuario_por_id(ua);
            un = Globales.servicio.obtener_usuario_por_id(un);
            lblUsuarioAtiendo.Text = ua.nombre + " " + ua.apellido;
            lblUsuarioNotifica.Text = un.nombre + " " + un.apellido;
            chbxAtencion.Checked = alarmaLoad.notificacion;
            chbxNotificacion.Checked = alarmaLoad.atendido;
            lblFechaAtencion.Text = alarmaLoad.fecha_atencion;
            if (IsCallback)
            {
                txbxObservacion.Text = alarmaLoad.observacion;
            }
        }

        public void guardar_observacion_usuario(object sender, EventArgs e)
        {
            alarma.observacion = txbxObservacion.Text;
            UsuarioObj u = (UsuarioObj)Session["usuario"];
            alarma.usuario_atiende = u.id;
            string mensaje = Globales.servicio.actualizar_avance_by_usuario(u, alarma);
            CuadroMensaje mensajeNotificacion = new CuadroMensaje(sender, this.GetType());
            mensajeNotificacion.mostrar_mensaje_alerta(mensaje);
        }
    }
}
