using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlerAtm.com.ec.Interfaces;
using System.Net.Mail;
using System.Net;

namespace ControlerAtm.Utilitario
{
    public class CorreoElectronico : NotificacionDao
    {
        private MailMessage correo;
        private SmtpClient smtp;
        private LecturaEscrituraArchivo errorMail;
        //Parametros a enviar son los siguientes
        //1. Nombre de suceso
        //2. nombre o direccion del servidor SMTP
        //3. puerto de trabajo smtp
        //4. usuario
        //5. Contrasenia
        public CorreoElectronico(string pathGuardar) {
            correo = new MailMessage();
            smtp = new SmtpClient();
            errorMail = new LecturaEscrituraArchivo();
            errorMail.set_path_guardar(pathGuardar);
            errorMail.archivo_guardar("\\LOG_CORREO");
        }

        #region Miembros de NotificacionDao

        public void abrir_conexion()
        {
            throw new NotImplementedException();
        }

        public void cerrar_conexion()
        {
            throw new NotImplementedException();
        }

        public void configurar_parametros(string[] parametros)
        {
            correo.From = new MailAddress(parametros[2]);//Correo de origen
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            smtp.Host = parametros[0];//Servidor SMTP
            smtp.Port = Int16.Parse(parametros[1]);//Puerto de trabajo
            smtp.EnableSsl = parametros[4] == "1";
            //smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(parametros[2], parametros[3]);//cuenta de correo de origen con la contraseña.
        }

        public int enviar_notificacion(string mensajeEnviar, string titulo)
        {
            correo.Subject = titulo;
            correo.Body = "<p><b><font color=blue>" + mensajeEnviar + 
                "</font></b></p><p>CORREO GENERADO AUTOMATICAMENTE POR SERVICIO NOTIFICACIÓN, Versión 1.0 A LAS " + System.DateTime.Now + "</p>"
                + "<p>No responda a este correo, contactese con soporte técnico o el administrador de aplicación copyright ©, Derechos de autor</p>";
            try
            {
                smtp.Send(correo);
                correo.Dispose();
                return 0;
            }
            catch (Exception ex)
            {
                errorMail.escritura_archivo_string("No se pudo enviar la notificacion electrónica, debido al siguiente error: " + ex.Message);
                return 1;
            }
            
        }

        #endregion

        #region Miembros de NotificacionDao


        public void asignar_destinatarios(string[] destinatarios)
        {
            correo.To.Clear();
            foreach (string destinatario in destinatarios) {
                correo.To.Add(new MailAddress(destinatario));
            }
        }

        #endregion
    }
}
