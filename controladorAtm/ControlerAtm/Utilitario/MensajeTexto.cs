using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlerAtm.com.ec.Interfaces;
using System.IO.Ports;

namespace ControlerAtm.Utilitario
{
    public class MensajeTexto : NotificacionDao
    {
        private string puetoCom;
        private int velocidadTx;
        private int bitDatos;
        private SerialPort puertoSerial;
        private LecturaEscrituraArchivo errorSMS;
        private int tiempoEspera;
        private int formatoNumeroFono;
        private string[] telefonos;
        public MensajeTexto(string pathGuardar) {
            errorSMS = new LecturaEscrituraArchivo();
            errorSMS.set_path_guardar(pathGuardar);
            puetoCom = "COM23";//puerto Compor defecto
            velocidadTx = 9600;//9600 bps
            bitDatos = 8;//8 bit
            tiempoEspera = 1000;//Por defecto 1 segundo
            formatoNumeroFono = 1; // Formato nummero telefonico
            puertoSerial = new SerialPort(puetoCom);
            puertoSerial.NewLine = "\r\n";
            puertoSerial.BaudRate = velocidadTx;
            puertoSerial.Parity = Parity.None;
            puertoSerial.DataBits = bitDatos;
            puertoSerial.StopBits = StopBits.One;
            puertoSerial.Handshake = Handshake.None;
            puertoSerial.DtrEnable = true;
            puertoSerial.RtsEnable = true;
            puertoSerial.ReadTimeout = tiempoEspera;
            puertoSerial.WriteTimeout = tiempoEspera;
            puertoSerial.DataReceived += new SerialDataReceivedEventHandler(recepcion_Respueta);
            errorSMS.archivo_guardar("LOG_SMS");
            
        }

        public MensajeTexto(string numeroPuerto,int velocidad,int dataBit,int timeOut)
        {
            errorSMS = new LecturaEscrituraArchivo();
            errorSMS.archivo_guardar("LOG_SMS");
            puetoCom = numeroPuerto;//puerto Compor defecto
            velocidadTx = velocidad;//9600 bps
            bitDatos = dataBit;//8 bit
            tiempoEspera = timeOut*1000;//Ingresa en segundos y se multiplica por 1000
            puertoSerial = new SerialPort(puetoCom);
            puertoSerial.NewLine = "\r\n";
            puertoSerial.BaudRate = velocidadTx;
            puertoSerial.Parity = Parity.None;
            puertoSerial.DataBits = bitDatos;
            puertoSerial.StopBits = StopBits.One;
            puertoSerial.Handshake = Handshake.None;
            puertoSerial.DtrEnable = true;
            puertoSerial.RtsEnable = true;
            puertoSerial.ReadTimeout = tiempoEspera;
            puertoSerial.WriteTimeout = tiempoEspera;
            puertoSerial.DataReceived += new SerialDataReceivedEventHandler(recepcion_Respueta);
        }

        public void recepcion_Respueta(object o, EventArgs e) { 
        
        }
        #region Miembros de NotificacionDao

        public void abrir_conexion()
        {
            try
            {
                puertoSerial.Open();
            }
            catch (Exception e)
            {
                errorSMS.escritura_archivo_string(e.Message + "\r" + e.StackTrace);
            }
        }

        public void cerrar_conexion()
        {
            try
            {
                puertoSerial.Close();
            }
            catch (InvalidOperationException e) {
                errorSMS.escritura_archivo_string(e.Message + "\t" + e.StackTrace);
            }
        }

        public void configurar_parametros(string[] parametros)
        {

            try
            {
                puetoCom = parametros[0];//Config parametro
                velocidadTx = Int32.Parse(parametros[1]);
                bitDatos = Int32.Parse(parametros[2]);
                tiempoEspera = Int32.Parse(parametros[3]);
                formatoNumeroFono = Int32.Parse(parametros[4]);

                puertoSerial.PortName = puetoCom;
                puertoSerial.BaudRate = velocidadTx;
                puertoSerial.DataBits = bitDatos;
                puertoSerial.ReadTimeout = tiempoEspera;
                puertoSerial.WriteTimeout = tiempoEspera;
            }
            catch (FormatException e)
            {
                errorSMS.escritura_archivo_string(e.Message + "error en " + e.StackTrace);
            }
            catch (IndexOutOfRangeException e) {
                errorSMS.escritura_archivo_string(e.Message + "error en " + e.StackTrace);
            }
        }

        public void asignar_destinatarios(string[] destinatarios)
        {
            telefonos = destinatarios;
            int i = 0;
            foreach (string numero in telefonos) {
                if (formatoNumeroFono == 1) {
                    //Formato telefonica
                    if (numero.StartsWith("0")) {
                        
                        telefonos[i] = "593"+numero.Substring(1,numero.Length-1);
                    }
                }else if (formatoNumeroFono == 2){
                    //Formato claro
                    if (numero.StartsWith("593")) {
                        telefonos[i] = "0" + numero.Substring(3, numero.Length - 3);
                    }
                }
            }
        }

        public int enviar_notificacion(string mensajeEnviar, string titulo)
        {
            try
            {
                // Comando de comprobación                
                puertoSerial.WriteLine("AT");
                System.Threading.Thread.Sleep(tiempoEspera*1000);
                puertoSerial.WriteLine("ATZ");
                System.Threading.Thread.Sleep(tiempoEspera*1000);
                // Pasamo a modo SMS Texto
                puertoSerial.WriteLine("AT+CMGF=1");
                System.Threading.Thread.Sleep(tiempoEspera * 1000);
                foreach (string numeroFono in telefonos)
                {
                    if (!string.IsNullOrEmpty(numeroFono))
                    {
                        // Enviamos el numero al que queremos enviar el SMS
                        puertoSerial.WriteLine("AT+CMGS=\"" + numeroFono + "\"\r\n");
                        //puerto.WriteLine("AT+CMGS=\"" + NumTel.Trim() + "\"\r\n" + Mensaje.Trim() + (char)26);
                        System.Threading.Thread.Sleep(tiempoEspera*1000);
                        // El texto del mensaje, se termina con Control+Z
                        puertoSerial.WriteLine(mensajeEnviar.Trim() + '\x001a');
                        System.Threading.Thread.Sleep(tiempoEspera * 1000);
                        // Si todo sale bien devuelve true
                        puertoSerial.WriteLine("\"\r\n");
                        System.Threading.Thread.Sleep(tiempoEspera * 1000);
                    }
                }
                return 0;// 0 envio sin error
            }
            catch (Exception e)
            {
                // si hay algún error devuelve false
                errorSMS.escritura_archivo_string(e.Message + "\t" + e.StackTrace);
                return 1; // envia mensaje con error
            }
        }

        #endregion
    }
}
