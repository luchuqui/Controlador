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
            string[] campos = tramaIng.Split((char)28);
            alarma.tipo_mensaje = campos[0];
            alarma.fecha_registro = System.DateTime.Now;
            try
            {
                /*PARA MENSAJES NO SOLICITADOS DE ALARMAS*/
                if (campos[0].Equals("12")) 
                {
                    try
                    {
                        //obtenemos el tipo de dispositivo
                        alarma.id_tipo_dispositivo = campos[3].Substring(0, 1);
                        if (alarma.id_tipo_dispositivo.Equals("P"))
                        {
                            alarma.estado_dispositivo = campos[3].Substring(1, 2);// estado de nivel P
                            alarma.error_severidad = campos[3].Substring(2,campos[3].Length); // datos adicionales
                            // si es 2 llega el 0 modo supervisor exit, 1 modo supervisor ingreso
                            
                        }
                        else {
                            alarma.estado_dispositivo = campos[3].Substring(1, campos[3].Length);
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
                    if (campos[3].Equals("8"))
                    {
                        try
                        {
                            alarma.id_tipo_dispositivo = campos[4].Substring(0, 1);
                            alarma.estado_dispositivo = campos[5].Substring(1, campos[5].Length);
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
                        alarma.id_tipo_dispositivo = campos[3];
                        alarma.estado_suministro = campos[5];
                        alarma.estado_diagnostico = campos[6];

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


        public MonitoreoDispositivos parseaTramaAlarmaDispositivo(AlarmasObj alarma)
        {
            MonitoreoDispositivos monitoreo = new MonitoreoDispositivos();
            int[] disp = new int[alarma.estado_suministro.Length];
            int ini = 0,fin = 1;
            /* En este for se saca el estado de cada uno de los dispositivos referenciados en el
             * manual en el capitulo 9 */
            for (int i = 0; i < alarma.estado_suministro.Length; i++) {
                disp[i] = int.Parse( alarma.estado_suministro.Substring(ini,fin));
                ini = fin;
                fin++;
            }
            monitoreo.id_atm = terminal.id_atm;
            monitoreo.estado_lectora = disp[3];// En esta posición se encuentra el estado de lectora
            monitoreo.estado_dispensador = disp[4].ToString();
            monitoreo.estado_impresora = disp[6].ToString();
            monitoreo.estado_impresora_jrnl = disp[7].ToString();
            monitoreo.estado_encriptora = disp[11].ToString();
            monitoreo.estado_gaveta1 = disp[15].ToString();
            monitoreo.estado_gaveta2 = disp[16].ToString();
            monitoreo.estado_gaveta3 = disp[17].ToString();
            monitoreo.estado_gaveta4 = disp[18].ToString();
            monitoreo.estado_gaveta5 = "0";
            return monitoreo;
        }

        #endregion
    }
}
