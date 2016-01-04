using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlerAtm.com.ec.objetos;

namespace ControlerAtm.Utilitario
{
    public class NotificacionEnviar
    {
        private CorreoElectronico email;
        private MensajeTexto msTexto;
        private string [] servicioHabilitado;
    
        public NotificacionEnviar(string [] configCorreo,
            string [] configTexto,
            string pathGuardar,string [] serviciosHabilitados) {
            email = new CorreoElectronico(pathGuardar);
            msTexto = new MensajeTexto(pathGuardar);
            email.configurar_parametros(configCorreo);
            msTexto.configurar_parametros(configTexto);
            this.servicioHabilitado = serviciosHabilitados;
        }

        public void enviarNotificacionUsuario(List<UsuarioObj> usuarios,string mensajeEnvio,string idAlarma) {
            string telefonos = string.Empty;
            string correos = string.Empty;
            email.limpiarDestinatarios();
            foreach (UsuarioObj u in usuarios) {
                /*Parametro almacenado en la base de datos, 1 Habilitado, 0 deshabilitado*/
                if (servicioHabilitado[0].Equals("1")) { 
                /*Si ingresa se procede a enviar correo*/
                    //correos += u.correo + ";";
                    email.asignar_destinatario(u.correo);
                }
                if (servicioHabilitado[1].Equals("1"))
                {
                /*Si ingresa se procede a enviar telefono*/
                    telefonos += u.telefono + ":";
                }

            }
            if (servicioHabilitado[0].Equals("1"))
            {
                //email.asignar_destinatario(correos.Substring(0,correos.Length-1));
                email.enviar_notificacion(mensajeEnvio, "#Alarma: " + idAlarma);
            }
            if (servicioHabilitado[1].Equals("1"))
            {
                msTexto.abrir_conexion();
                msTexto.asignar_destinatarios(telefonos.Split(':'));
                mensajeEnvio += "\n#Alarma :" + idAlarma;
                msTexto.enviar_notificacion(mensajeEnvio,"");
                msTexto.cerrar_conexion();
            }
            
        
        }
    }
}
