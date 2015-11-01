using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Drawing;
using controladorAtm.BeanObjetos;
using System.IO;
using ControlerAtm.com.ec.BaseDatos;
using ControlerAtm.com.ec.objetos;
using ControlerAtm.com.ec.Excepciones;

namespace controladorAtm
{
    public class ConexionTCP
    {
        private TcpClient cliente; /*Ciente tcp para atender las peticiones de los cajeros*/
        private NetworkStream stream; /* Buffer para enviar o recibir los datos del terminal*/
        private archivoRW terminalArchivo; /* Clase implementada para realizar la escritra de eventos que genera el cajero*/
        private archivoRW error; /*Escritura de archivos para almacenar los errores producidos por la aplicacion*/
        public event datoIngresoServicio datoIn; /*Variable para pasar la informacion entre clases en este caso datos entrada*/
        public event datoIngresoServicio datoResp; /*Variable para pasar la informacion entre clases en este caso datos salida*/
        private RichTextBox visor; /*Objeto para mostrar la informacion en la pantalla del cliente*/
        private ConfiguracionServicio configurarServicio; /*Configuracion del servicio para los cajeros*/
        private TramaTerminalComando comadoToATM; /*Trama de mensajeria hacia el terminal, se declara la interfaz porque puede manejar mas de un tipo de protocolo*/
        private bool sincronico; /*Bandera para indicar, si el flujo de datos es sincronico o asincronico*/
        private bool enviarDato; /*Bandera para validar si se puede enviar datos hacia el terminal*/

        private AtmObj terminal; /*Terminal ATM, parametros de valores de acceso*/
        private bool clienteConectado = true; /*Bandera para verificar si el terminal sigue conectado*/
        private BddSQLServer conBdd; /*Manejador de conexion a la base de datos*/
        private System.Timers.Timer verificacionConexion;
        /*Constructor para enviar los datos del servicio sin considerar el objeto richText Box */
        public ConexionTCP(TcpClient clie, ConfiguracionServicio serviceConf,AtmObj terminales,BddSQLServer conexion)

        {
            try
            {
                this.terminal = terminales;
                cliente = clie;
                stream = new NetworkStream(cliente.Client);
                stream.ReadTimeout = 3000;
                terminalArchivo = new archivoRW();
                error = new archivoRW();

                terminalArchivo.archivo_guardar("MENSAGE_TERMINAL", terminal.codigo);//Almacena en la carpeta MENSAGE_TERMINAL y en la sub carpeta codigo terminal
                error.archivo_guardar("ERROR", terminal.codigo);
                visor = new RichTextBox();
                configurarServicio = serviceConf;
                comadoToATM = new ComandoNdcTerminal();
                sincronico = true;
                enviarDato = false;
                
                error.escritura_archivo_string(configurarServicio.conexion +"\t"+ configurarServicio.pathLogServicio);
                //conBdd = new BddSQLServer(configurarServicio.conexion, configurarServicio.pathLogServicio);
                conBdd = conexion;
                verificacionConexion = new System.Timers.Timer();
                verificacionConexion.Elapsed += new System.Timers.ElapsedEventHandler(verificarConexion);
                verificacionConexion.Interval = 300000;
                verificacionConexion.Enabled = true;
                verificacionConexion.Start();
            }
            catch (Exception ex) {
                error.escritura_archivo_string(ex.Message);
                mensaje_error_sistema(ex.Message,Color.Red);
            }
        }

        /*Constructor para enviar los datos del servicio considerando el objeto richText Box */

        public ConexionTCP(TcpClient clie, ConfiguracionServicio serviceConf, RichTextBox visor,AtmObj terminal,BddSQLServer conexion)
        {
            try
            {
                this.terminal = terminal;
                cliente = clie;
                stream = new NetworkStream(cliente.Client);
                sincronico = true;
                stream.ReadTimeout = 3000;
                terminalArchivo = new archivoRW();
                error = new archivoRW();
                terminalArchivo.archivo_guardar("MENSAGE_TERMINAL", terminal.codigo);
                error.archivo_guardar("ERROR", terminal.codigo);
                configurarServicio = serviceConf;
                this.visor = visor;
                comadoToATM = new ComandoNdcTerminal();
                sincronico = true;
                enviarDato = false;
                conBdd = conexion;
                verificacionConexion = new System.Timers.Timer();
                verificacionConexion.Elapsed += new System.Timers.ElapsedEventHandler(verificarConexion);
                verificacionConexion.Interval = 1000;
                verificacionConexion.Enabled = true;
                verificacionConexion.Start();
            }
            catch (Exception ex)
            {
                error.escritura_archivo_string(ex.Message);
                mensaje_error_sistema(ex.Message,Color.Red);
            }
        }

        public void cliente_servicio(){
            try
            {
                sincronico = true;
                //conBdd.abrir_conexion_base();
                AlarmasObj mensajeEnvioRecep = new AlarmasObj();
                mensajeEnvioRecep.id_atm = terminal.id_atm;
                this.comadoToATM.setPonerEnServicio();
                string datoEnvio = this.comadoToATM.getTramaComandoTerminal();
                string datoRespuesta = "";
                //envio_string(datoEnvio);
                datoIn("CONEXION NUEVA TERMINAL :" + terminal.codigo);
                enviarDato = true;
                while (sincronico)
                {
                    //verificarConexion();
                    
                    if (enviarDato) {
                        clienteConectado = true;
                        datoEnvio = comadoToATM.getTramaComandoTerminal();
                        envio_string(datoEnvio);
                        
                        mensajeEnvioRecep.fecha_registro = System.DateTime.Now;
                        mensajeEnvioRecep.envio_recepcion = 0; //cero envio, uno recibo
                        
                        terminalArchivo.escritura_archivo_string(">>>[" + datoEnvio.Length + "] : " + datoEnvio);
                        enviarDato = false;
                        datoIn(datoEnvio.Substring(2,datoEnvio.Length-2));
                        mensajeEnvioRecep.mensaje = datoEnvio.Substring(2, datoEnvio.Length - 2);
                        conBdd.insertar_alarmas(mensajeEnvioRecep);
                        datoEnvio = "";
                    }
                    datoRespuesta = recepcion_de_datos();
                    if (!string.IsNullOrEmpty(datoRespuesta) && datoRespuesta.Length > 2)
                    {
                        terminalArchivo.escritura_archivo_string("<<<[" + datoRespuesta.Length + "] : " + datoRespuesta);
                        datoResp(datoRespuesta.Substring(2, datoRespuesta.Length - 2));
                        mensajeEnvioRecep.mensaje = datoRespuesta.Substring(2, datoRespuesta.Length - 2);
                        mensajeEnvioRecep.fecha_registro = System.DateTime.Now;
                        mensajeEnvioRecep.envio_recepcion = 1; //cero envio, uno recibo
                        conBdd.insertar_alarmas(mensajeEnvioRecep);
                        datoRespuesta = "";
                    }
                    /*else {
                        comadoNDC.setSolicitarInformacionConfiguracion();
                        envio_string(comadoNDC.getTramaComandoTerminal() + "");// Envio para verifficar si esta conectado el terminal
                    }*/
                }
            }catch (SocketException ex)
            {
                error.escritura_archivo_string(ex.StackTrace);
                mensaje_error_sistema(ex.Message, Color.Red);
                mensaje_error_sistema(ex.StackTrace,Color.Red);
                sincronico = false;
                clienteConectado = false;
            }catch (ErrorConexionTerminal ex)
            {
                mensaje_error_sistema(ex.Message, Color.Green);
                sincronico = false;
                clienteConectado = false;
            }
            catch (Exception ex)
            {   
               error.escritura_archivo_string(ex.StackTrace);
               mensaje_error_sistema(ex.Message, Color.Red);
               mensaje_error_sistema(ex.StackTrace,Color.Red);
               sincronico = false;
                clienteConectado = false;
            }finally
            {
                cerrar_conexion();

            }
        }

        public string recepcion_de_datos()
                {
              string datos = "";//Mesaje de datos para mostrar.
                byte[] bytes = new byte[1024];// bufer para realizar la recepcion de flujo
                int i;
            
                if (stream.DataAvailable)
                {
                    i = stream.Read(bytes, 0, bytes.Length);
                    datos = System.Text.Encoding.UTF8.GetString(bytes, 0, i);
                    return datos;
                }
                return "";
        }

        public void verificarConexion(object sender, EventArgs e)
        {
            this.comadoToATM.setSolicitarInformacionConfiguracion();
            envio_string(this.comadoToATM.getTramaComandoTerminal());
            if (cliente == null) {
                throw new ErrorConexionTerminal("Terminal desconectado " +terminal.codigo);
            }
        }

        public void envio_string(string datoIn)
        {
            try
            {
                byte[] msg = new byte[datoIn.Length];
                msg = System.Text.Encoding.UTF8.GetBytes(datoIn);
                if (stream.CanWrite)
                {
                    stream.Write(msg, 0, msg.Length);
                }
            }
            catch (ObjectDisposedException ex)
            {
                error.escritura_archivo_string(ex.StackTrace);
                mensaje_error_sistema(ex.Message, Color.Red);
            }
            catch (ArgumentNullException ex)
            {
                error.escritura_archivo_string(ex.StackTrace);
                mensaje_error_sistema(ex.Message, Color.Red);
            }
            catch (IOException ex) {
                error.escritura_archivo_string(ex.StackTrace);
                cerrar_conexion();
            }
            catch (Exception ex)
            {
                error.escritura_archivo_string(ex.StackTrace);
                
            }
        }

        public void cerrar_conexion()
        {
            try
            {
                terminal.conexion = false;
                conBdd.actualizar_terminal(terminal);
                verificacionConexion.Enabled = false;
                verificacionConexion.Stop();
                sincronico = false;
                clienteConectado = false;
                cliente.Close();
                stream.Close();
            }
            catch (NullReferenceException ex)
            {
                error.escritura_archivo_string("Error al cerrar conexion :"+ex.StackTrace);
            }
            catch (Exception ex)
            {
                error.escritura_archivo_string("Error al cerrar conexion"+ex.StackTrace);
            }
            /*try
            {
                conBdd.cerrar_conexion_base();
            }
            catch (ExConexionBase ex)
            {
                error.escritura_archivo_string("Error al cerrar conexion base datos" + ex.StackTrace);
            }*/
        }

        public void mensaje_error_sistema(string mensaje,Color color)
        {
            limpiar_lineas_visor();
            visor.SelectionColor = color;
            visor.AppendText(System.DateTime.Now.ToString("hh:mm:ss") +" " +mensaje + "\r");
        }

        public void limpiar_lineas_visor()
        {
            try
            {
                if (visor.Lines.Length > 70)
                {
                    visor.Clear();
                }
            }
            catch (Exception ex) {
                visor.AppendText(ex.Message);
            }
        }

        public AtmObj get_Terminal_Atm() {
            return terminal;
        }

        public bool get_clienteConectado() {
            return clienteConectado;
        }

        public ServidorEscucha ServidorEscucha
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }

}
