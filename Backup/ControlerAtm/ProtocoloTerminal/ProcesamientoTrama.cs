using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlerAtm.com.ec.objetos;
using ControlerAtm.Utilitario;

namespace controladorAtm.ProtocoloTerminal
{
    /*Permite realizar el precesamiento de tramas ndc, de alarmas*/
    public class ProcesamientoTrama:IProcesarTrama
    {
        private LecturaEscrituraArchivo errorNDC; /*Archivo para almacenar las alertas del cajero*/
        private AtmObj terminal;
        public ProcesamientoTrama(AtmObj terminal) {
            errorNDC = new LecturaEscrituraArchivo();
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
                      CODIGO F SOLICITUD DE CONTADORES Y ESTADOS DE DISPOSITIVOS GENERAL*/
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
                        if (campos[4].StartsWith("1"))// envio de configuracion de informacion
                        {
                            alarma.id_tipo_dispositivo = "1";
                            alarma.tipo_alarma = "A";
                            alarma.estado_suministro = campos[5];
                            alarma.estado_diagnostico = campos[6];
                            alarma.estado_dispositivo = campos[7];
                        }
                        else if (campos[4].StartsWith("2")) { // envio de estado de contadores
                            alarma.id_tipo_dispositivo = "2";
                            alarma.estado_suministro = campos[4];
                        }
                        else if (campos[4].StartsWith("7A"))// envio de contadores extendidos
                        { // envio de estado de contadores
                            alarma.id_tipo_dispositivo = "7";
                            string[] otrosValores = campos[4].Split((char)29);// char 29 GS
                            alarma.estado_suministro = otrosValores[1];
                            alarma.estado_diagnostico = otrosValores[2];
                        }

                        
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

        #region Miembros de IProcesarTrama


        public DetalleDescripcionObj procesamientoDescripcion(DetalleDescripcionObj detalle)
        {
            if (detalle.tipo_dispositivo.Equals("B")) {
                detalle.detalle_descripcion = "numero de secuencia de configuracion " + detalle.descripcion_mensaje;
            }
            else if (detalle.tipo_dispositivo.Equals("D"))
            {
                if (detalle.tipo_mensaje.Equals("0")) {
                    detalle.detalle_descripcion = "error de serveridad cambio de suministros ";
                }else if (detalle.tipo_mensaje.Equals("1"))
                {
                    detalle.detalle_descripcion = "Cliente no tomo la tarjeta en el tiempo determinado ";
                }
                else if (detalle.tipo_mensaje.Equals("2"))
                {
                    detalle.detalle_descripcion = "Mecanismo de ejeccion ha falla, revisar ";
                }
                else if (detalle.tipo_mensaje.Equals("3"))
                {
                    detalle.detalle_descripcion = "Mecanismo de actualizacion del track ha fallado ";
                }
                else if (detalle.tipo_mensaje.Equals("4"))
                {
                    detalle.detalle_descripcion = "Invalido track recibido por el central ";
                }
                else if (detalle.tipo_mensaje.Equals("7"))
                {
                    detalle.detalle_descripcion = "Error en lectura de track ";
                }
                else
                {
                    detalle.detalle_descripcion = "No registrada";
                }
            }
            else if (detalle.tipo_dispositivo.Equals("E"))
            {

                if (detalle.tipo_mensaje.StartsWith("0"))
                {
                    detalle.detalle_descripcion = "Operacion exitosa pero ha ocurrido un error ";
                }
                else if (detalle.tipo_mensaje.StartsWith("1"))
                {
                    detalle.detalle_descripcion = "Billetes adicionaciones se ha dispensado ";
                }
                else if (detalle.tipo_mensaje.StartsWith("2"))
                {
                    detalle.detalle_descripcion = "No ha dispensado dinero ";
                }
                else if (detalle.tipo_mensaje.StartsWith("3"))
                {
                    detalle.detalle_descripcion = "Dinero dispensado desconocido, el cliente pudo tener acceso a cualquier valor ";
                }
                else if (detalle.tipo_mensaje.StartsWith("4"))
                {
                    detalle.detalle_descripcion = "Dinero no dispensado o tarjeta no ejectada ";
                }
                else if (detalle.tipo_mensaje.StartsWith("5"))
                {
                    detalle.detalle_descripcion = "Algún dinero ha sido retraido a la gaveta de rechazo ";
                }
                else
                {
                    detalle.detalle_descripcion = "No registrada";
                }
                if (detalle.tipo_mensaje.Length > 8) {
                    detalle.detalle_descripcion += "\nBilletes entregados ";
                    detalle.detalle_descripcion += "\nGabeta 1 :" + detalle.tipo_mensaje.Substring(1,2);
                    detalle.detalle_descripcion += "\nGabeta 2 :" + detalle.tipo_mensaje.Substring(3, 2);
                    detalle.detalle_descripcion += "\nGabeta 3 :" + detalle.tipo_mensaje.Substring(5, 2);
                    detalle.detalle_descripcion += "\nGabeta 4 :" + detalle.tipo_mensaje.Substring(7, 2);
                }
            }
            else if (detalle.tipo_dispositivo.Equals("F"))
            {
                if (detalle.tipo_mensaje.Equals("0"))
                {
                    detalle.detalle_descripcion = "Operacion exitosa pero ha ocurrido un error ";
                }
                else if (detalle.tipo_mensaje.Equals("1"))
                {
                    detalle.detalle_descripcion = "Tiempo de espera ha caducado ";
                }
                else if (detalle.tipo_mensaje.Equals("2"))
                {
                    detalle.detalle_descripcion = "Falla para habilitar el mecanismo de deposito ";
                }
                else if (detalle.tipo_mensaje.Equals("3"))
                {
                    detalle.detalle_descripcion = "Deposito fallido, el cliente tiene acceso al dinero ";
                }
                else if (detalle.tipo_mensaje.Equals("4"))
                {
                    detalle.detalle_descripcion = "Deposito fallido, el cliente no tiene acceso al dinero ";
                }
                else {
                    detalle.detalle_descripcion = "No registrada";
                }
            }
            else if (detalle.tipo_dispositivo.Equals("G"))
            {
                if (detalle.tipo_mensaje.Equals("0"))
                {
                    detalle.detalle_descripcion = "Impresion exitosa ";
                }
                else if (detalle.tipo_mensaje.Equals("1"))
                {
                    detalle.detalle_descripcion = "Impresion no exitosa completada ";
                }
                else if (detalle.tipo_mensaje.Equals("2"))
                {
                    detalle.detalle_descripcion = "Dispositivo no configurado ";
                }
                else if (detalle.tipo_mensaje.Equals("4"))
                {
                    detalle.detalle_descripcion = "Cancelacion presionada durante la impresion ";
                }
                else if (detalle.tipo_mensaje.Equals("5"))
                {
                    detalle.detalle_descripcion = "Recibo retraido ";
                }
                else
                {
                    detalle.detalle_descripcion = "No registrada";
                }
            }
            else if (detalle.tipo_dispositivo.Equals("H"))
            {
                if (detalle.tipo_mensaje.Equals("0"))
                {
                    detalle.detalle_descripcion = "Impresion exitosa ";
                }
                else if (detalle.tipo_mensaje.Equals("1"))
                {
                    detalle.detalle_descripcion = "Impresion no exitosa completada ";
                }
                else if (detalle.tipo_mensaje.Equals("2"))
                {
                    detalle.detalle_descripcion = "Dispositivo no configurado ";
                }
                else if (detalle.tipo_mensaje.Equals("6"))
                {
                    detalle.detalle_descripcion = "Journal de respaldo activado ";
                }
                else if (detalle.tipo_mensaje.Equals("7"))
                {
                    detalle.detalle_descripcion = "Journal de respaldo y reimpresion finalizada ";
                }
                else if (detalle.tipo_mensaje.Equals("8"))
                {
                    detalle.detalle_descripcion = "Journal de respaldo y reimpresion iniciad ";
                }
                else if (detalle.tipo_mensaje.Equals("9"))
                {
                    detalle.detalle_descripcion = "Journal de respaldo detenido ";
                }
                else if (detalle.tipo_mensaje.Equals(":"))
                {
                    detalle.detalle_descripcion = "Journal de respaldo con error de seguridad ";
                }
                else if (detalle.tipo_mensaje.Equals(";"))
                {
                    detalle.detalle_descripcion = "Journal de respaldo y reimpresion detenida ";
                }
                else
                {
                    detalle.detalle_descripcion = "No registrada";
                }
            }
            else if (detalle.tipo_dispositivo.Equals("L"))
            {
                if (detalle.tipo_mensaje.Equals("1"))
                {
                    detalle.detalle_descripcion = "Encriptor con error ";
                }
                else if (detalle.tipo_mensaje.Equals("2"))
                {
                    detalle.detalle_descripcion = "Encriptor no configurado ";
                }
                else
                {
                    detalle.detalle_descripcion = "No registrada";
                }
            }
            else if (detalle.tipo_dispositivo.Equals("P"))
            {
                if (detalle.tipo_mensaje.Equals("1"))
                {
                    detalle.detalle_descripcion = "Cambio de modo supervisor ";
                }
                else
                {
                    detalle.detalle_descripcion = "No registrada";
                }
            }
            else if (detalle.tipo_dispositivo.Equals("R"))
            {
                detalle.detalle_descripcion = "Presion de número";
            }else if (detalle.tipo_dispositivo.Equals("F1")){
            //estados de dispositivos
                try
                {
                    detalle.detalle_descripcion = "Impresora :" + estado_dispositivo(detalle.tipo_mensaje[6]);
                    detalle.detalle_descripcion += "\nEncriptora :" + estado_dispositivo(detalle.tipo_mensaje[11]);
                    detalle.detalle_descripcion += "\nGaveta 1 :" + estado_dispositivo(detalle.tipo_mensaje[15]);
                    detalle.detalle_descripcion += "\nGaveta 2 :" + estado_dispositivo(detalle.tipo_mensaje[16]);
                    detalle.detalle_descripcion += "\nGaveta 3 :" + estado_dispositivo(detalle.tipo_mensaje[17]);
                    detalle.detalle_descripcion += "\nGaveta 4 :" + estado_dispositivo(detalle.tipo_mensaje[18]);
                }
                catch (IndexOutOfRangeException e) {
                    detalle.detalle_descripcion = "Error al procesar";
                }
            }
            else if (detalle.tipo_dispositivo.Equals("F2"))
            {
                //contadores
                if (!string.IsNullOrEmpty(detalle.tipo_mensaje))
                {
                    int inicial = 12;

                    detalle.detalle_descripcion = "# billetes en gavetas";
                    detalle.detalle_descripcion += "\nGaveta 1 :" + detalle.tipo_mensaje.Substring(inicial, 4);
                    inicial = inicial + 4;
                    detalle.detalle_descripcion += "\nGaveta 2 :" + detalle.tipo_mensaje.Substring(inicial, 4);
                    inicial = inicial + 4;
                    detalle.detalle_descripcion += "\nGaveta 3 :" + detalle.tipo_mensaje.Substring(inicial, 4);
                    inicial = inicial + 4;
                    detalle.detalle_descripcion += "\nGaveta 4 :" + detalle.tipo_mensaje.Substring(inicial, 4);
                    inicial = inicial + 4;

                    detalle.detalle_descripcion += "\n\n# billetes en rechazado";
                    detalle.detalle_descripcion += "\nGaveta 1 :" + detalle.tipo_mensaje.Substring(inicial, 4);
                    inicial = inicial + 4;
                    detalle.detalle_descripcion += "\nGaveta 2 :" + detalle.tipo_mensaje.Substring(inicial, 4);
                    inicial = inicial + 4;
                    detalle.detalle_descripcion += "\nGaveta 3 :" + detalle.tipo_mensaje.Substring(inicial, 4);
                    inicial = inicial + 4;
                    detalle.detalle_descripcion += "\nGaveta 4 :" + detalle.tipo_mensaje.Substring(inicial, 4);
                    inicial = inicial + 4;

                    detalle.detalle_descripcion += "\n\n# billetes dispensados";
                    detalle.detalle_descripcion += "\nGaveta 1 :" + detalle.tipo_mensaje.Substring(inicial, 4);
                    inicial = inicial + 4;
                    detalle.detalle_descripcion += "\nGaveta 2 :" + detalle.tipo_mensaje.Substring(inicial, 4);
                    inicial = inicial + 4;
                    detalle.detalle_descripcion += "\nGaveta 3 :" + detalle.tipo_mensaje.Substring(inicial, 4);
                    inicial = inicial + 4;
                    detalle.detalle_descripcion += "\nGaveta 4 :" + detalle.tipo_mensaje.Substring(inicial, 4);
                    inicial = inicial + 4;

                    detalle.detalle_descripcion += "\n\n# billetes entregados en la ultimatransaccion";
                    detalle.detalle_descripcion += "\nGaveta 1 :" + detalle.tipo_mensaje.Substring(inicial, 4);
                    inicial = inicial + 4;
                    detalle.detalle_descripcion += "\nGaveta 2 :" + detalle.tipo_mensaje.Substring(inicial, 4);
                    inicial = inicial + 4;
                    detalle.detalle_descripcion += "\nGaveta 3 :" + detalle.tipo_mensaje.Substring(inicial, 4);
                    inicial = inicial + 4;
                    detalle.detalle_descripcion += "\nGaveta 4 :" + detalle.tipo_mensaje.Substring(inicial, 4);
                    inicial = inicial + 4;
                }
                else {
                    detalle.detalle_descripcion = "sin descripción";
                }
            }
            else { 
                detalle.detalle_descripcion = "No existe descripción";
            }

            return detalle;
        }

        public string estado_dispositivo(char valor) {
            string mensaje = string.Empty;
            if (valor.Equals('0')) {
                mensaje = "Estado Correcto";
            }
            else if (valor.Equals('1'))
            {
                mensaje = "Estado Correcto";
            }
            else if (valor.Equals('2'))
            {
                mensaje = "Nivel medio";
            }
            else if (valor.Equals('3'))
            {
                mensaje = "Casi vacio";
            }
            else if (valor.Equals('4'))
            {
                mensaje = "Sin dinero";
            }
            else {
                mensaje = "sin descipcion";
            }

            return mensaje;
        }

        #endregion
    }
}
