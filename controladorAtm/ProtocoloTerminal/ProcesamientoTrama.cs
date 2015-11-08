using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlerAtm.com.ec.objetos;

namespace controladorAtm.ProtocoloTerminal
{
    public class ProcesamientoTrama:IProcesarTrama
    {
        private archivoRW errorNDC; /*Archivo para almacenar las alertas del cajero*/
        public ProcesamientoTrama(AtmObj terminal) {
            errorNDC = new archivoRW();
            errorNDC.archivo_guardar("MENSAGE_NDC", terminal.codigo);//Almacena en la carpeta MENSAGE_TERMINAL y en la sub carpeta codigo terminal
        }
        #region Miembros de IProcesarTrama
        /* Metodo para procesar las tramas de alarmas del cajero automatico*/
        public AlarmasObj parseaTramaIngreso(string tramaIng)
        {
            AlarmasObj alarma = new AlarmasObj();
            string[] campos = tramaIng.Split((char)28);
            try
            {
                /*PARA MENSAJES NO SOLICITADOS DE ALARMAS*/
                if (campos[0].Equals("12")) 
                {

                }
                /*PARA MENSAJES SOLICITADOS DE ALARMAS*/
                else if (campos[0].Equals("22"))
                {

                }
            }
            catch (IndexOutOfRangeException e) {
                errorNDC.escritura_archivo_string(e.Message);
                errorNDC.escritura_archivo_strSplit(campos);
                alarma = null;
            }
            return alarma;
        }

        #endregion
    }
}
