using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Drawing;
using System.Threading;

namespace controladorAtm
{
    class ServidorEscuchaUno // Clase administra conexion ATM controlador
    {
        // COmentario agregado
    //    // se declara too privado al ser dependiente de la presente clase
    //    private ConfiguracionServicio parametrosconfig; // parametros de configuracion para una maquina
    //    private TcpListener servidor;// permite la conexion via TCP
    //    private IPAddress IPlocal;// clase que guarda la ip local del servidor escucha por todas 0.0.0.0
    //    private TcpClient cliente; //clase definida en el visual q maneja conexiones TCP
    //    private bool opc; //indica el esta de inicio del servicio
    //    private RichTextBox visor; // clase visual q muestra mensajes en pantalla del usuario
    //    private TramaComandoNdc ndcComando; // definida para creacion y procesamiento de mensajes NDC entre ATM y controlador
    //    private bool envioComandoNDC; //flag incador si se va enviar un comando
    //    private archivoRW logMensajesNdc; // archivo con informacion de envio y Rx de la coexion

    //    public ServidorEscuchaUno(ConfiguracionServicio servicio) // constructor primera sentencia q se ejeecuta al inicio de la creacion de un objeto del servicio modo escucha
    //    {
    //        parametrosconfig = servicio;
    //        visor = new RichTextBox();
    //        ndcComando = new TramaComandoNdc();
    //        logMensajesNdc = new archivoRW();
    //        logMensajesNdc.archivo_guardar("NDCTRAMA","TRAMANDC");
    //    }

    //    public ServidorEscuchaUno(ConfiguracionServicio servicio, RichTextBox visor)
    //    {
    //        parametrosconfig = servicio;
    //        this.visor = visor;
    //        ndcComando = new TramaComandoNdc();
    //        logMensajesNdc = new archivoRW();
    //        logMensajesNdc.archivo_guardar("NDCTRAMA", "TRAMANDC");
    //    }

    //    public void set_visor(RichTextBox visor) {
    //        this.visor = visor;
    //    }
        
        
    //    public void inicio_escucha() // metodo que maneja la comunicacion entre ATM y controlaador
    //    { 
    //    // Data buffer for incoming data.
    //    byte[] bytes = new Byte[1024];
    //    opc = true;
    //    // Establish the local endpoint for the socket.
    //    // Dns.GetHostName returns the name of the 
    //    // host running the application.
    //    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
    //    IPAddress ipAddress = ipHostInfo.AddressList[0];
    //    IPEndPoint localEndPoint = new IPEndPoint(ipAddress, parametrosconfig.puerto);

    //    // Crea una conexion socket  TCP/IP .
    //    Socket listener = new Socket(AddressFamily.InterNetwork,
    //        SocketType.Stream, ProtocolType.Tcp );

    //    // escucha todas conexiones entrantes
    //    Socket handler = null;
    //    string data = null;
    //    try
    //    {
    //        listener.Bind(localEndPoint);
    //        listener.Listen(10);
    //        envioComandoNDC = true;
    //        // Start listening for connections.
    //        while (opc)
    //        {
    //            //Console.WriteLine("Waiting for a connection...");
    //            visor.AppendText("Esperando una conexion cliente ATM...\n");
    //            // Program is suspended while waiting for an incoming connection.
    //            handler = listener.Accept();
    //            handler.ReceiveTimeout = 3000;
    //            data = null;
    //            bytes = new byte[1024];
    //            // An incoming connection needs to be processed.
    //            ndcComando.ponerServicioAtm();// setea el valor para enviar el comando para poner en servicio
    //            bytes = Encoding.ASCII.GetBytes(ndcComando.getTramaNDC());
    //            //logMensajesNdc.escritura_archivo_string(">>>" + ndcComando.getTramaNDC());// ALMACENANDO EN EL ARCHIVO LO QUE SE VA ENVIAR
    //            while (opc)
    //            {
    //                if (envioComandoNDC)
    //                {
    //                    visor.AppendText(ndcComando.getTramaNDC() + "\n");
    //                    logMensajesNdc.escritura_archivo_string(">>>" + ndcComando.getTramaNDC());
    //                    handler.Send(bytes);
    //                    envioComandoNDC = false;// Se envia el comando por primera vez luego se pone en off para luego colocar otro comando
    //                }
    //                bytes = new byte[1024];
    //                int bytesRec = 0;
    //                try
    //                {
    //                    bytesRec = handler.Receive(bytes);
    //                    data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
    //                    if (!string.IsNullOrEmpty(data))
    //                    {
    //                        visor.AppendText("\nRespuesta Atm : " + data.Substring(2, data.Length - 2));
    //                        if (data != null)
    //                        {
    //                            logMensajesNdc
    //                                .escritura_archivo_string("<<<" + data);
    //                        }
    //                    }
    //                }
    //                catch (Exception ex) {
    //                    //visor.AppendText("No se recibio ningun dato se procede a parasar\n");
    //                    string m = ex.Message;
    //                }
    //            }
    //        }

    //    }
    //    catch (Exception e)
    //    {
    //        //Console.WriteLine(e.ToString());
    //        visor.AppendText("ERROR : " + e.Message + "\n");
    //    }
    //    finally {
    //        if (handler != null)
    //        {
    //            handler.Shutdown(SocketShutdown.Both);
    //            handler.Close();
    //        }
    //    }
    //    //visor.AppendText("\nPress ENTER to continue...");
    //    //Console.WriteLine("\nPress ENTER to continue...");
    //    //Console.Read();
    //}

    //    public void servidorInicio() {
    //        IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
    //        IPAddress ipAddress = ipHostInfo.AddressList[0];
    //        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, parametrosconfig.puerto);

    //        // Crea una conexion socket  TCP/IP .
    //        Socket listener = new Socket(AddressFamily.InterNetwork,
    //            SocketType.Stream, ProtocolType.Tcp);
    //        listener.Bind(localEndPoint);
    //        listener.Listen(10);
    //        do
    //        {
    //            //Socket clientesocket = listener.Accept();
    //            //ConexionTCP cliente = new ConexionTCP(listener.Accept(), "ATM001");
    //            mensaje_error_sistema("Ingreso otra conexion.. ", Color.Green);
    //            ndcComando.ponerServicioAtm();
    //            //cliente.setEnviarDatos(true, ndcComando.getTramaNDC());
    //            //var clienteAtm = new Thread((ThreadStart) cliente.envioRecepcionDatos);
    //            //cliente.envioRecepcionDatos();
    //            //clienteAtm.Start();

    //        } while (opc);//Indica si debe seguir esperando mas clientes
    //                      //Caso contrario cerra todas las conexiones


    //    }

    //    public void detener()
    //    {
    //        try
    //        {
    //            opc = false;
    //            TcpClient sockcli = new TcpClient(parametrosconfig.ip, parametrosconfig.puerto);
    //            sockcli.Close();
    //        }
    //        catch (SocketException ex)
    //        {
    //            string msg = ex.Message;
    //            mensaje_error_sistema("Detener el servicio :"+ex.Message, Color.Red);
    //        }
    //    }
        
    //    public void obtener_tramaIn(string tramaIn) {
    //        mensaje_error_sistema(" ---> "+tramaIn, Color.Green);
    //    }

    //    public void obtener_tramaRespuesta(string tramaOut)
    //    {
    //        mensaje_error_sistema(" <--- "+tramaOut,Color.Green);
    //    }

    //    public void mensaje_error_sistema(string mensaje, Color colorFuente)
    //    {
    //        limpiar_lineas_visor();
    //        visor.SelectionColor = colorFuente;
    //        visor.AppendText(System.DateTime.Now.ToString("hh:mm:ss")+mensaje + "\n\r");
    //    }

    //    public void limpiar_lineas_visor()
    //    {
    //        if (visor.Lines.Length > 70)
    //        {
    //            visor.Clear();
    //        }
    //    }

    //    public void envioComandoATM(TramaComandoNdc comando) {
    //        envioComandoNDC = true;
    //        ndcComando = comando;
    //    }
    }
    //public delegate void datoIngresoDelegados(string datoTrama);
}

