using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Drawing;
using System.Collections;
using ControlerAtm.com.ec.BaseDatos;
using ControlerAtm.com.ec.objetos;

namespace controladorAtm
{
    public class ServidorEscucha
    {
        private ConfiguracionServicio parametrosconfig;/*Parametros de configuracion del servicio*/
        private TcpListener servidor; /*Servidor TCP para escuchar las peticiones*/
        private IPAddress IPlocal; /*Objeto para almacenar la direccion IP del confi servicio*/
        private TcpClient cliente; /*Cliente TCP, este va a ser un proceso para atender las peticiones*/
        private bool opc;/*Bandera para verficar si ingresar al bucle para realizar repeticiones a la lectura de escucha*/
        private RichTextBox visor; /*Objeto Txbx para mostrar mensajes al usuario*/
        private DataGridView visorTerminales /*Objeto para almacenar los datos de los terminales*/;
        private Thread hiloServidor; /*Hilo padre para atender las peticiones de los terminales*/
        private ArrayList terminalesConectadas; /*Almacenamiento de terminales conectadas*/
        private ArrayList atmsAutorizados; /*Almacenamiento para terminales autorizados en BDD*/
        private Thread hiloMonitor; /*Hilo de ejecucion para verificar el estado de la conexion entre el servidor y terminal*/
        private string down = "SIN CONEXION";
        private string up = "CONECTADO";
        private BddSQLServer conBdd;
        private string ipMonitoreo;

        public ServidorEscucha(ConfiguracionServicio servicio) {
            parametrosconfig = servicio;
            visor = new RichTextBox();
            terminalesConectadas = new ArrayList();
            atmsAutorizados = new ArrayList();
            ActualizarEstadoConexionTerminal();
            ipMonitoreo = parametrosconfig.dll;
        }

        public ServidorEscucha(ConfiguracionServicio servicio, RichTextBox visor, ArrayList atms,DataGridView terminalesView,BddSQLServer conexion)
        {
            parametrosconfig = servicio;
            this.visor = visor;
            terminalesConectadas = new ArrayList();
            atmsAutorizados = atms;
            visorTerminales = terminalesView;
            conBdd = conexion;
            ActualizarEstadoConexionTerminal();
            ipMonitoreo = parametrosconfig.dll;
            //conBdd.abrir_conexion_base();
        }

        public ConfiguracionServicio ConfiguracionServicio
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public void set_visor(RichTextBox visor) {
            this.visor = visor;
        }

        public void cambio_color(int fila, int columna, Color tmp)
        {
           visorTerminales.Rows[fila].Cells[columna].Style.ForeColor = tmp;
        }

        public void aceptar_conexion()
        {
            try
            {
                IPlocal = IPAddress.Parse(parametrosconfig.ip);
                servidor = new TcpListener(IPlocal, parametrosconfig.puerto);
                servidor.Start();
                opc = true;
                /*Monitorea el estado de la conexion*/
                hiloMonitor = new Thread((ThreadStart)verificarEstadoConexion);
                hiloMonitor.Start();
                /*Ingresa a un lazo repetitivo para validar esperar varias conexiones*/
                while (opc)
                {
                    cliente = servidor.AcceptTcpClient();
                    mensaje_error_sistema("Conexion ip "+cliente.Client.RemoteEndPoint.ToString().Split(':')[0] +" remota",Color.Blue);
                    AtmObj atmIng = verificarIpAutorizada(cliente.Client.RemoteEndPoint.ToString().Split(':')[0]);
                    if (atmIng != null)
                    {
                        //conBdd.abrir_conexion_base();
                        //atmIng.conexion = true;
                        //conBdd.actualizar_terminal(atmIng);
                        //conBdd.cerrar_conexion_base();
                        mensaje_error_sistema("Ip " + cliente.Client.RemoteEndPoint.ToString().Split(':')[0] + " remota - aceptada ", Color.Green);
                        ConexionTCP clien = new ConexionTCP(cliente, parametrosconfig, this.visor,atmIng,conBdd);
                        clien.datoIn += new datoIngresoServicio(obtener_tramaIn);
                        clien.datoResp += new datoIngresoServicio(obtener_tramaRespuesta);
                        terminalesConectadas.Add(clien);
                        if (opc)
                        {
                            try
                            {
                                hiloServidor = new Thread((ThreadStart)clien.cliente_servicio); /*creacion del hilo hijo*/
                                hiloServidor.Start();
                            }
                            catch (Exception excli)
                            {
                                mensaje_error_sistema(excli.Message, Color.Red);
                            }
                        }
                    }
                    else {
                        
                        string ipSolicitud = cliente.Client.RemoteEndPoint.ToString().Split(':')[0];
                        if (ipMonitoreo.Equals(ipSolicitud))
                        {
                            envioComandoTerminal(cliente);
                            
                        }
                        else {
                            mensaje_error_sistema("No autorizada para conectarse ...", Color.Red);
                        }
                        cliente.Close();
                    }
                }
            }
            catch (SocketException ex)
            {
                mensaje_error_sistema(" "+ex.Message, Color.Red);
            }
            finally
            {
                servidor.Stop();
            }
        }

        public void verificarEstadoConexion() {
            mensaje_error_sistema("Monitor de conexion inciado",Color.Blue);
            while (opc)
            {
                Thread.Sleep(1000);    
                ArrayList terminalesQuitar = new ArrayList();
                ArrayList indicesActivos = new ArrayList();
                ArrayList indicesInactivos = new ArrayList();
                foreach (ConexionTCP conexionEstablecidad in terminalesConectadas)
                {
                    if (conexionEstablecidad.get_clienteConectado())
                    {
                        for (int i = 0; i < visorTerminales.Rows.Count; i++)
                        {
                            if (visorTerminales.Rows[i].Cells[0].Value.ToString()
                                .EndsWith(conexionEstablecidad
                                .get_Terminal_Atm().ip))
                            {
                                indicesActivos.Add(i);
                            }

                        }
                    }
                    else if (!conexionEstablecidad.get_clienteConectado())
                    {
                        for (int i = 0; i < visorTerminales.Rows.Count; i++)
                        {
                            if (visorTerminales.Rows[i].Cells[0].Value.ToString()
                                .EndsWith(conexionEstablecidad
                                .get_Terminal_Atm().ip))
                            {
                                indicesInactivos.Add(i);
                                terminalesQuitar.Add(conexionEstablecidad);
                            }
                        }
                    }
                }
                
                foreach (int indiceInact in indicesInactivos)
                {
                    cambio_color(indiceInact, 1, Color.Red);
                    visorTerminales.Rows[indiceInact].Cells[1].Value = down;
                }

                foreach (int indiceAct in indicesActivos) {
                    cambio_color(indiceAct, 1, Color.Green);
                    visorTerminales.Rows[indiceAct].Cells[1].Value = up;
                }
                foreach (ConexionTCP conexionQuitar in terminalesQuitar) {
                    terminalesConectadas.Remove(conexionQuitar);
                }
            }
            mensaje_error_sistema("Monitor de conexion finalizado", Color.Blue);
        }

        public AtmObj verificarIpAutorizada(string ip) {
            AtmObj terminal = null;
            foreach(AtmObj atm in atmsAutorizados){
                if (atm.ip.Equals(ip)) {
                    terminal = atm;
                    break;
                }
            }
            foreach (ConexionTCP conexionEstablecidad in terminalesConectadas)
            {
                if (conexionEstablecidad.get_Terminal_Atm().ip.Equals(ip)) {
                    terminal = null;
                    mensaje_error_sistema("Error ya existe un terminal conectado :" , Color.Red);
                    break;
                }
            }
            return terminal;
        }

        /*Este metodo se va a encarga de realizar el envio y recepcion de datos*/
        public void envioComandoTerminal(TcpClient clienteCmd)
        {
            string ipSolicitud = clienteCmd.Client.RemoteEndPoint.ToString().Split(':')[0];
            string mensajeCmd = string.Empty;
            /*1.- debo de receptar lo solicitado*/
            NetworkStream ncomand = new NetworkStream(clienteCmd.Client);
                    try
                    {
                    string datosCmd = string.Empty;
                    byte[] bytes = new byte[1024];// bufer para realizar la recepcion de flujo
                    int i;
                    i = ncomand.Read(bytes, 0, bytes.Length);
                    datosCmd = System.Text.Encoding.UTF8.GetString(bytes, 0, i);/*Datos recibidos para el comando*/
                    string[] procesoCmd = datosCmd.Split(':');
                    mensaje_error_sistema("Envio comando ip Terminal :"+procesoCmd[0], Color.Green);    
                    foreach (ConexionTCP connAtm in terminalesConectadas)
                    {
                        if (connAtm.get_Terminal_Atm().ip.Equals(procesoCmd[0]))
                        {
                            connAtm.envio_string(procesoCmd[1]);
                            //mensajeCmd=connAtm.envioRecepcionString(procesoCmd[1]);
                            mensajeCmd = "220009";
                            break;
                        }
                    }
                    /*Envia la respuesta al cliente que solicito*/
                    if (mensajeCmd.EndsWith("220009"))
                    {
                        mensajeCmd = "Envio comando exitoso";
                    }
                    else if (string.IsNullOrEmpty(mensajeCmd)) {
                        mensajeCmd = "Respuesta no registrada";
                    }
                    byte[] msg = new byte[mensajeCmd.Length];
                    msg = System.Text.Encoding.UTF8.GetBytes(mensajeCmd);
                    ncomand.Write(msg, 0, msg.Length);
                }
            catch (Exception e)
            {
                mensaje_error_sistema(e.Message, Color.Green);    
            }
            finally {
                try
                {
                    ncomand.Close();
                    //clienteCmd.Close();
                }
                catch (Exception e) {
                    mensaje_error_sistema(e.Message, Color.Green);    
                }
            }

                
        }

        public void ActualizarEstadoConexionTerminal()
        {
            foreach (AtmObj atm in atmsAutorizados)
            {
                atm.conexion = false;
                conBdd.actualizar_terminal(atm);
            }
        }

        public void detener()
        {
            try
            {
                //if (hiloServidor != null)
                //{
                    opc = false;
                    //if (hiloServidor.IsAlive) {
                        foreach(ConexionTCP terminal in terminalesConectadas){
                            terminal.cerrar_conexion();
                        }
                        servidor.Stop();
                        terminalesConectadas.Clear();
                        //hiloServidor.Interrupt();
                    //}
                //}
                //opc = false;
                //TcpClient sockcli = new TcpClient(parametrosconfig.ip, parametrosconfig.puerto);
                //sockcli.Close();
            }
            catch (SocketException ex)
            {
                string msg = ex.Message;
                mensaje_error_sistema("Detener el servicio :"+ex.Message, Color.Red);
            }
        }
        
        public void obtener_tramaIn(string tramaIn) {
                mensaje_error_sistema(">>>" + tramaIn, Color.Green);
            
        }

        public void obtener_tramaRespuesta(string tramaOut)
        {
            mensaje_error_sistema("<<<" + tramaOut,Color.Green);
            
        }

        public void mensaje_error_sistema(string mensaje, Color colorFuente)
        {
            limpiar_lineas_visor();
            visor.SelectionColor = colorFuente;
            visor.AppendText(System.DateTime.Now.ToString("hh:mm:ss") + " " + mensaje + "\r");
        }

        public void limpiar_lineas_visor()
        {
            if (visor.Lines.Length > 70)
            {
                visor.Clear();
            }
        }
    }
    public delegate void datoIngresoServicio(string datoTrama);
}
