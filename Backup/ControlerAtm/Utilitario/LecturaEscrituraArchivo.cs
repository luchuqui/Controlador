using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using controladorAtm;

namespace ControlerAtm.Utilitario
{
    /*Permite realizar la escritura, lectura de archivo en formato txt y xml*/
    public class LecturaEscrituraArchivo
    {
    private string pathAbrir; // direccion donde se va a abrir o guardar el archivo
        private string pathGuardar;
        private StreamWriter objWrite;  //realiza la escritura del archivo
        private XmlDocument xml; // Archivo xml configuracion

        public LecturaEscrituraArchivo() {
            pathGuardar = "c:\\" + cambiar_fecha(System.DateTime.Now.Date.ToString()) + ".txt";
            pathAbrir = @"D:\Mis Documentos\NDC_TESIS\Programa\Config.xml";
            xml = new XmlDocument();
        }

        public void cerrar_archivo()
        {
            objWrite.Close();
        }

        public void cambiar_path(string path)
        {
            pathAbrir = path;
        }

        public void set_path_guardar(string pathSave) {
            pathGuardar = pathSave;
        }

        public string cambiar_fecha(string date)
        {
            string fecha = "";
            string temp;
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
        /*Escritura de archivo en formato txt*/
        public void escritura_archivo_string(string mensaje)
        {
            mensaje = mensaje + "\r";
            string tmpath;
            tmpath = pathGuardar + @"\" + cambiar_fecha(System.DateTime.Now.Date.ToString()) + ".txt";
            try
            {
                objWrite = new StreamWriter(tmpath, true);
                objWrite.WriteLine(System.DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + " " + mensaje.Trim());
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
        
        public void archivo_guardar(string carpeta)
        {
            pathGuardar = pathGuardar + carpeta;
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
                XmlNodeList ndll = nodo.GetElementsByTagName("ipServicioAdmin");
                ConfiguracionServicio datosConfig = new ConfiguracionServicio();
                datosConfig.conexion = conBDD[0].InnerText;
                datosConfig.numero = num.ToString();
                datosConfig.ip = nipServidor[0].InnerText;
                datosConfig.puerto = int.Parse(npuertoServidor[0].InnerText);
                try
                {
                    datosConfig.dll = ndll[0].InnerText;
                }
                catch (NullReferenceException e)
                {
                    datosConfig.dll = "127.0.0.1";
                }
                datosConfig.nombreServicio = nNombre[0].InnerText;
                datosConfig.estado = false;
                datosServicio.Add(datosConfig);

            }
            return datosServicio;
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

        public string get_path_abrir()
        {
            return pathAbrir;
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

        public void escritura_archivo_strSplit(string[] mensaje)
        {

            String tmpath;
            tmpath = pathGuardar + @"\" + cambiar_fecha(System.DateTime.Now.Date.ToString()) + ".txt";
            try
            {
                objWrite = new StreamWriter(tmpath, true);
                foreach (string msg in mensaje)
                {
                    objWrite.WriteLine(System.DateTime.Now.TimeOfDay.ToString().Substring(0, 8) + " " + msg.Trim());
                }
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


    }
}
