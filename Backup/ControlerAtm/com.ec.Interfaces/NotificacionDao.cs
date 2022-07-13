using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlerAtm.com.ec.Interfaces
{
    public interface NotificacionDao
    {
        void abrir_conexion();
        void cerrar_conexion();
        void configurar_parametros(string [] parametros);
        void asignar_destinatarios(string[] destinatarios);
        void asignar_destinatario(string destinatario);
        void limpiarDestinatarios();
        int enviar_notificacion(string mensajeEnviar,string titulo);
    }
}
