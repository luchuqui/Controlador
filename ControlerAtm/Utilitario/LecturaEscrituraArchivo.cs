using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ControlerAtm.Utilitario
{
    public class LecturaEscrituraArchivo
    {
        private string pathAbrir; // direccion donde se va a abrir o guardar el archivo
        private string pathGuardar;
        private StreamReader objReader; //reliaza la lectura del archivo
        private StreamWriter objWrite;  //realiza la escritura del archivo

        public LecturaEscrituraArchivo() {
            pathGuardar = "c:\\" + cambiar_fecha(System.DateTime.Now.Date.ToString()) + ".txt";
            pathAbrir = @"D:\Mis Documentos\TESIS BIOMETRIA\Programa\Config.cfg";

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

        public void escritura_archivo_string(string mensaje)
        {
            mensaje = mensaje + "\r";
            string tmpath;
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
        
        public void archivo_guardar(string carpeta)
        {
            pathGuardar = pathGuardar + carpeta;
        }

        public List<string> lectura_archivo()
        {
            string sLine = string.Empty;
            List<string> arrText = new List<string>();
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
                return arrText;
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
    }
}
