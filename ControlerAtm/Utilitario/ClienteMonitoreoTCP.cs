using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using controladorAtm;

namespace ControlerAtm.Utilitario
{
    /*este cliente va ser asincrono*/
    public class ClienteMonitoreoTCP
    {
        private TcpClient cliente; /*Ciente tcp para enviar las peticiones de comandos al cajero*/
        private NetworkStream stream; /* Buffer para enviar o recibir los datos de la aplicacion*/
        
        public ClienteMonitoreoTCP(string configuracion) {
            string[] cfg = configuracion.Split(':');
            cliente = new TcpClient(cfg[0],int.Parse(cfg[1]));
            stream = new NetworkStream(cliente.Client);
        }

        public string envioRecepcionString(string mensajeEnvio){
            byte[] msg = new byte[mensajeEnvio.Length];
            msg = System.Text.Encoding.UTF8.GetBytes(mensajeEnvio);
            if (stream.CanWrite)
            {
                stream.Write(msg, 0, msg.Length);
            }
            /*Proceso de respuesta*/
            string datos = string.Empty;
            byte[] bytes = new byte[1024];// bufer para realizar la recepcion de flujo
            int i;
            i = stream.Read(bytes, 0, bytes.Length);
                    datos = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
                    stream.Close();
                    cliente.Close();
            return datos;
        }

        public void cerrarConexion() {
            stream.Close();
            cliente.Close();
        }
    }
}
