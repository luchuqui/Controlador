using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections;

namespace controladorAtm
{
    public class archivoRW
    {
        private String pathAbrir; // direccion donde se va a abrir o guardar el archivo
        private String pathGuardar;
        private string sLine;
        StreamReader objReader; //realiza la lectura del archivo
        StreamWriter objWrite;  //realiza la escritura del archivo
        ArrayList arrText;
        private XmlDocument xml;
        string logs;
        private ArrayList configSrv;

        public archivoRW()
        {
            pathGuardar = "c:\\" + cambiar_fecha(System.DateTime.Now.Date.ToString()) + ".txt";
            pathAbrir = @"D:\Mis Documentos\TESIS\Programa\Config.cfg";
            arrText = new ArrayList();
            sLine = "";
            xml = new XmlDocument();
            configSrv = new ArrayList();
        }

        public string get_path_abrir()
        {
            return pathAbrir;
        }

        public string get_path_guardar()
        {
            return pathGuardar;
        }

        public archivoRW(string tmp)
        {
            pathGuardar = "c:\\" + cambiar_fecha(System.DateTime.Now.Date.ToString()) + ".txt";
            pathAbrir = @"D:\Mis Documentos\TESIS\Programa\Config.cfg";
            arrText = new ArrayList();
            sLine = "";
            logs = tmp;
            xml = new XmlDocument();
        }

        public void cerrar_archivo()
        {
            objReader.Close();
            objWrite.Close();
        }

        public void cambiar_path(string path)
        {
            pathAbrir = path;
        }

        public ArrayList obtener_datos_array()
        {
            return arrText;
        }

        public void lectura_archivo()
        {
            try
            {
                objReader = new StreamReader(pathAbrir);
                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        arrText.Add(sLine);
                }
                objReader.Close();
            }
            catch (FileNotFoundException ex)
            {
                throw ex;
            }
            catch (UnauthorizedAccessException ex)
            {
                throw ex;
            }

        }

        public void escritura_archivo(string sOutput)
        {
            objWrite = new StreamWriter(pathGuardar, true);
            objWrite.WriteLine(sOutput);
            objWrite.Close();
        }

        public void escritura_archivo_txbx()
        {
            string nameArchivo;
            nameArchivo = pathGuardar + @"\" + cambiar_fecha(System.DateTime.Now.Date.ToString()) + ".txt";
            objWrite = new StreamWriter(nameArchivo, true);
            objWrite.WriteLine(nameArchivo);
            objWrite.Close();
        }

        public void escritura_archivo_string(string mensaje)
        {

            String tmpath;
            tmpath = pathGuardar + @"\" + cambiar_fecha(System.DateTime.Now.Date.ToString()) + ".txt";
            try
            {
                objWrite = new StreamWriter(tmpath, true);
                objWrite.WriteLine(System.DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + " " + mensaje.Trim() + "\r");
                objWrite.Close();
            }
            catch (IOException ex)
            {
                try
                {
                    System.IO.Directory.CreateDirectory(pathGuardar);
                    escritura_archivo_string(ex.Message);
                }
                catch (UnauthorizedAccessException exPath)
                {
                    //string tmp = exPath.Message;
                    throw new Exception(exPath.Message);
                }
            }
        }

        public string cambiar_fecha(string date)
        {
            String fecha = "";
            String temp;
            for (int i = 0; i < date.Length; i++)
            {
                temp = date[i].ToString();
                if (temp.Equals("/") || temp.Equals(":"))
                {

                }
                else
                {
                    if (temp.Equals(" "))
                    {
                        break;
                    }
                    else
                    {
                        fecha = fecha + date[i];
                    }

                }
            }
            return fecha;
        }

        public ArrayList procesar_datos_configuracion()
        {
            ArrayList datos_conecxion = new ArrayList();
            String datoConf;
            String temp;
            int conta; ;
            for (int i = 0; i < arrText.Count; i++)
            {
                datoConf = arrText[i].ToString();
                conta = 0; ;
                for (int j = 0; j < datoConf.Length; j++)
                {
                    //saco los datos desde que llega el primer simbolo = o :
                    temp = datoConf[j].ToString();
                    if (temp.Equals("=") || temp.Equals(":"))
                    {
                        conta = (datoConf.Length) - 1;
                        conta = conta - j;
                        datos_conecxion.Add(datoConf.Substring(j + 1, conta));
                        break;

                    }
                }
            }
            return datos_conecxion;
        }

        public void archivo_abrir(string carpeta)
        {
            String tmpco = "";
            int tcont = 0;
            //MessageBox.Show(pathAbrir);
            if (string.IsNullOrEmpty(carpeta))
            {
                pathAbrir = Path.GetFullPath("LOG");
            }
            else
            {
                pathAbrir = Path.GetFullPath(carpeta);
            }
            for (int i = pathAbrir.Length - 1; i > 0; i--)
            {
                tmpco = pathAbrir[i].ToString();
                if (tmpco.Equals("\\"))
                {
                    tcont++;
                }
                if (tcont == 3)
                {
                    tcont = i;
                    break;
                }
            }
            pathAbrir = pathAbrir.Substring(0, tcont) + @"\" + carpeta;
            //MessageBox.Show(pathAbrir);
        }

        public void archivo_guardar(string carpeta)
        {
            String tmpco = "";
            int tcont = 0;
            pathGuardar = Path.GetFullPath(carpeta);
            for (int i = pathGuardar.Length - 1; i > 0; i--)
            {
                tmpco = pathGuardar[i].ToString();
                if (tmpco.Equals("\\"))
                {
                    tcont++;
                }
                if (tcont == 2)
                {
                    tcont = i;
                    break;
                }
            }
            pathGuardar = pathGuardar.Substring(0, tcont) + @"\" + carpeta;
        }

        public void archivo_guardar(string carpeta, string nombreServicioCarpeta)
        {
            String tmpco = "";
            int tcont = 0;
            pathGuardar = Path.GetFullPath(carpeta);
            for (int i = pathGuardar.Length - 1; i > 0; i--)
            {
                tmpco = pathGuardar[i].ToString();
                if (tmpco.Equals("\\"))
                {
                    tcont++;
                }
                if (tcont == 2)
                {
                    tcont = i;
                    break;
                }
            }
            pathGuardar = pathGuardar.Substring(0, tcont) + @"\" + carpeta + @"\" + nombreServicioCarpeta;
        }

        public void escribir_cfg(ArrayList tmpar)
        {

            if (System.IO.File.Exists(pathAbrir))
            {
                System.IO.File.Delete(pathAbrir);
            }
            objWrite = new StreamWriter(pathAbrir, true);
            //objWrite.WriteLine("[PortSwitch]="+tmpar[0]);
            objWrite.WriteLine("[modoOpeInter]=" + tmpar[0]);
            objWrite.WriteLine("[HostCoac]=" + tmpar[1]);
            objWrite.WriteLine("[PuertoCoac]=" + tmpar[2]);
            objWrite.WriteLine("[PuertoRtc]=" + tmpar[3]);
            objWrite.WriteLine("[UrlWS]=" + tmpar[4]);
            objWrite.Close();
        }

        //public List<string> lectura_de_archivo_configuracion(string nombreArchivo)
        public List<string> lectura_de_archivo_configuracion()
        {
            string line;
            //string path = System.AppDomain.CurrentDomain.BaseDirectory + nombreArchivo;
            List<string> lineas = new List<string>();
            StreamReader sr = null;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                sr = new StreamReader(pathAbrir);

                //Read the first line of text
                line = sr.ReadLine();

                //Continue to read until you reach end of file
                while (line != null)
                {
                    lineas.Add(line.Split('=')[1]);
                    line = sr.ReadLine();
                }

                sr.Close();

            }
            catch (FileNotFoundException ex)
            {
                string message = ex.StackTrace;
                List<string> datosConf = new List<string>();
                if (pathAbrir.ToLower().Contains("config"))
                {
                    datosConf.Add("modo_operacion=1");
                    datosConf.Add("host_coac=127.0.0.1");
                    datosConf.Add("puerto_host=2001");
                    datosConf.Add("puerto_rtc=2001");
                    datosConf.Add("urlWs=http://localhost/test.asmx");
                    datosConf.Add("tamanio_trama=659");
                    datosConf.Add("codigo_usuario_servicio=test");
                    datosConf.Add("contrase_servicio=12345");
                    datosConf.Add("ip_autorizadora=127.0.0.1");
                    datosConf.Add("host_name_autorizador=localhost");
                }
                lineas = escribir_archivo(datosConf, pathAbrir);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return lineas;

        }

        public void cargar_archivo_configuracionXml()
        {
            try
            {
                xml.Load(pathAbrir);
            }
            catch (FileNotFoundException ex)
            {
                string message = ex.StackTrace;
                crearArchivoXml();
                xml.Load(pathAbrir);

            }
            catch (DirectoryNotFoundException ex)
            {
                string mse = ex.Message;
                crearArchivoXml();
                //throw new Exception(ex.Message);
                
            }
        }

        public void crearArchivoXml()
        {
            ConfiguracionServicio Stmp = new ConfiguracionServicio();
            Stmp.nombreServicio = "ServicioAtm";
            Stmp.puerto = 1000;
            //Stmp.urlWs = "http://localhost/wsTest.com.ec";
            Stmp.ip = "127.0.0.1";
            Stmp.dll = "ServicioAtms.dll";
            Stmp.nombreServicio = "Servicio Atms";
            XmlElement raiz = xml.CreateElement("", "Servicios", "");
            XmlElement nodo = xml.CreateElement("", "Servicio", "");

            XmlElement nombreServicio = xml.CreateElement("", "nombre", "");
            XmlText dato = xml.CreateTextNode(Stmp.nombreServicio);
            nombreServicio.AppendChild(dato);
            nodo.AppendChild(nombreServicio);

            XmlElement direccionIp = xml.CreateElement("", "DireccionIP", "");
            //XmlText dato = xml.CreateTextNode(Stmp.ip);
            dato = xml.CreateTextNode(Stmp.ip);
            direccionIp.AppendChild(dato);
            nodo.AppendChild(direccionIp);

            XmlElement puerto = xml.CreateElement("", "Puerto", "");
            dato = xml.CreateTextNode(Stmp.puerto.ToString());
            puerto.AppendChild(dato);
            nodo.AppendChild(puerto);

            //XmlElement ws = xml.CreateElement("", "ConexionHostCoac", "");
            XmlElement origen = xml.CreateElement("", "conexionBDD", "");
            dato = xml.CreateTextNode("Data Source=127.0.0.1;Initial Catalog=NDC_BDD;User ID=ControladorBDD;Password=123456");
            origen.AppendChild(dato);
            nodo.AppendChild(origen);

            XmlElement ensamblado = xml.CreateElement("", "dllServicio", "");
            dato = xml.CreateTextNode(Stmp.dll);
            ensamblado.AppendChild(dato);
            nodo.AppendChild(ensamblado);
            raiz.AppendChild(nodo);
            xml.AppendChild(raiz);
            XmlDeclaration xmldecl;
            xmldecl = xml.CreateXmlDeclaration("1.0", null, null);
            xml.InsertBefore(xmldecl, raiz);
            xml.Save(pathAbrir);
            //return xml.InnerXml; //devuelve en string el archivo xml
        }

        public List<ConfiguracionServicio> obtenerDatosXml()
        {
            cargar_archivo_configuracionXml();

            List<ConfiguracionServicio> datosServicio = new List<ConfiguracionServicio>();
            XmlNodeList servicios = xml.GetElementsByTagName("Servicios");
            XmlNodeList lista = ((XmlElement)servicios[0]).GetElementsByTagName("Servicio");
            int num = 0;
            foreach (XmlElement nodo in lista)
            {
                num++;
                XmlNodeList nNombre = nodo.GetElementsByTagName("nombre");
                XmlNodeList nipServidor = nodo.GetElementsByTagName("DireccionIP");
                XmlNodeList npuertoServidor = nodo.GetElementsByTagName("Puerto");
                XmlNodeList conBDD = nodo.GetElementsByTagName("conexionBDD");
                XmlNodeList ndll = nodo.GetElementsByTagName("dllServicio");
                ConfiguracionServicio datosConfig = new ConfiguracionServicio();
                datosConfig.conexion = conBDD[0].InnerText;
                datosConfig.numero = num.ToString();
                datosConfig.ip = nipServidor[0].InnerText;
                datosConfig.puerto = int.Parse(npuertoServidor[0].InnerText);
                //datosConfig.urlWs = nurlWs[0].InnerText;
                
                datosConfig.dll = ndll[0].InnerText;
                datosConfig.nombreServicio = nNombre[0].InnerText;
                datosConfig.estado = false;
                //ServidorEscuchaUno srv = new ServidorEscuchaUno(datosConfig);
                //datosConfig.servicioClase = "terminal";
                datosServicio.Add(datosConfig);
                
            }
            return datosServicio;
        }

        public List<string> escribir_archivo(List<string> dato, string ruta)
        {
            List<string> datos = new List<string>();
            try
            {

                //Pass the filepath and filename to the StreamWriter Constructor
                StreamWriter sw = new StreamWriter(ruta);
                //Write a line of text
                foreach (string tm in dato)
                {
                    sw.WriteLine(tm);
                    datos.Add(tm.Split('=')[1]);
                }
                //Close the file
                sw.Close();
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return datos;
        }

        public ConexionTCP ConexionTCP
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
