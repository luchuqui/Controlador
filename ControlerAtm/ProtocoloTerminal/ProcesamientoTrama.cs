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
        private AtmObj terminal;
        public ProcesamientoTrama(AtmObj terminal) {
            errorNDC = new archivoRW();
            errorNDC.archivo_guardar("MENSAGE_NDC", terminal.codigo);//Almacena en la carpeta MENSAGE_TERMINAL y en la sub carpeta codigo terminal
            this.terminal = terminal;
        }
        #region Miembros de IProcesarTrama
        /* Metodo para procesar las tramas de alarmas del cajero automatico*/
        public AlarmasObj parseaTramaIngreso(string tramaIng)
        {
            AlarmasObj alarma = new AlarmasObj();
            alarma.mensaje = tramaIng;
            alarma.id_atm = terminal.id_atm;
            string[] campos = tramaIng.Split((char)28);// es el FS  caracter 28
            alarma.id_mensaje = campos[0];
            alarma.fecha_registro = System.DateTime.Now;
            try
            {
                /*PARA MENSAJES NO SOLICITADOS DE ALARMAS*/
                if (campos[0].Equals("12")) 
                {
                    alarma.tipo_alarma = "A";
                    try
                    {
                        //obtenemos el tipo de dispositivo
                        alarma.id_tipo_dispositivo = campos[3].Substring(0, 1);
                        if (alarma.id_tipo_dispositivo.Equals("P"))
                        {
                            
                            alarma.estado_dispositivo = campos[3].Substring(1, 1);// estado de nivel P
                            if (alarma.estado_dispositivo.Equals("2"))
                            {
                                alarma.error_severidad = campos[3].Substring(2, campos[3].Length - 2); // datos adicionales
                                // si es 2 llega el 0 modo supervisor exit, 1 modo supervisor ingreso
                            }
                            else {
                                alarma.estado_suministro = campos[3].Substring(2, campos[3].Length - 2); // aca obtiene datos de los dispositivos
                            }
                            
                        }
                        else {
                            alarma.estado_dispositivo = campos[3].Substring(1, campos[3].Length-1);
                            alarma.error_severidad = campos[4];
                            alarma.estado_diagnostico = campos[5];
                            alarma.estado_suministro = campos[6];
                        }
                    }
                    catch (IndexOutOfRangeException e) {
                        errorNDC.escritura_archivo_string(e.Message);
                        errorNDC.escritura_archivo_strSplit(campos);
                    }
                }
                /*PARA MENSAJES SOLICITADOS DE ALARMAS*/
                else if (campos[0].Equals("22"))
                {
                    /*CODIGO TIPO 8 SIGNIFICA QUE ES UNA ALARMA SOLICITADA,
                      CODIGO F SOLICITUD DE CONTADORES LEER SI SE VALID*/
                    alarma.descriptor = campos[3];
                    if (campos[3].Equals("8"))
                    {
                        alarma.tipo_alarma = "A";
                        try
                        {
                            alarma.id_tipo_dispositivo = campos[4].Substring(0, 1);
                            alarma.estado_dispositivo = campos[5].Substring(1, campos[5].Length-1);
                            alarma.error_severidad = campos[6];
                            alarma.estado_diagnostico = campos[7];
                            if (campos[8].Length > 8)
                            {
                                alarma.estado_suministro = campos[8].Substring(0,8);
                            }
                            else {
                                alarma.estado_suministro = campos[8];
                            }
                        }
                        catch (IndexOutOfRangeException e) {
                            errorNDC.escritura_archivo_string(e.Message);
                            errorNDC.escritura_archivo_strSplit(campos);
                        }
                    }
                        /* ESTADOS DE DISPOSITIVOS */
                    else if(campos[3].Equals("F")){
                        //alarma.descriptor = campos[3];
                        alarma.estado_suministro = campos[5];
                        alarma.estado_diagnostico = campos[6];
                        alarma.estado_dispositivo = campos[7];
                        
                    }
                }
                else if (campos[0].Equals("1"))// comando a terminal 
                {
                    alarma.tipo_comando = campos[3];
                }
            }
            catch (IndexOutOfRangeException e) {
                errorNDC.escritura_archivo_string(e.Message);
                errorNDC.escritura_archivo_strSplit(campos);
                //alarma = null;
            }
            return alarma;
        }

        #endregion

        #region Miembros de IProcesarTrama


        public List<MonitoreoDispositivos> parseaTramaAlarmaDispositivo(AlarmasObj alarma)
        {
            List<MonitoreoDispositivos> alarmas = new List<MonitoreoDispositivos>();
            MonitoreoDispositivos monitoreoC = new MonitoreoDispositivos();
            MonitoreoDispositivos monitoreoS = new MonitoreoDispositivos();
            int[] conf = new int[alarma.estado_suministro.Length];
            int[] sum = new int[alarma.estado_dispositivo.Length];
            /* En este for se saca el estado de cada uno de los dispositivos referenciados en el
             * manual en el capitulo 9 */
            for (int i = 0; i < alarma.estado_suministro.Length-1; i++) {
                conf[i] = int.Parse( alarma.estado_suministro[i].ToString());
            }
            /*Suministros*/
            for (int i = 0; i < alarma.estado_dispositivo.Length - 1; i++)
            {
                sum[i] = int.Parse(alarma.estado_dispositivo[i].ToString());
            }
            monitoreoC.id_atm = terminal.id_atm;
            monitoreoC.estado_lectora = conf[3].ToString();// En esta posición se encuentra el estado de lectora
            monitoreoC.estado_dispensador = conf[4].ToString();
            monitoreoC.estado_impresora = conf[6].ToString();
            monitoreoC.estado_impresora_jrnl = conf[7].ToString();
            monitoreoC.estado_encriptora = conf[11].ToString();
            monitoreoC.estado_gaveta1 = conf[15].ToString();
            monitoreoC.estado_gaveta2 = conf[16].ToString();
            monitoreoC.estado_gaveta3 = conf[17].ToString();
            monitoreoC.estado_gaveta4 = conf[18].ToString();
            monitoreoC.estado_gaveta5 = "0";
            monitoreoC.tipo_estado = "C";
            /*Ver estados severidad en pagina 445 NDC tabla 9-13*/

            monitoreoS.id_atm = terminal.id_atm;
            monitoreoS.estado_lectora = sum[3].ToString();// En esta posición se encuentra el estado de lectora
            monitoreoS.estado_dispensador = sum[4].ToString();
            monitoreoS.estado_impresora = sum[6].ToString();
            monitoreoS.estado_impresora_jrnl = sum[7].ToString();
            monitoreoS.estado_encriptora = "0";
            monitoreoS.estado_gaveta1 = sum[15].ToString();
            monitoreoS.estado_gaveta2 = sum[16].ToString();
            monitoreoS.estado_gaveta3 = sum[17].ToString();
            monitoreoS.estado_gaveta4 = sum[18].ToString();
            monitoreoS.estado_gaveta5 = "0";
            monitoreoS.tipo_estado = "S";
            /*Ver estados en pagina 454 NDC tabla 9-16*/
            alarmas.Add(monitoreoC);
            alarmas.Add(monitoreoS);
            return alarmas;
        }

        #endregion
    }
}
