using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace controladorAtm
{
    /*Clase utilizada para almacenar los parametros del servicio
     TCP del cajero, aca esta toda la configuracion del servidor ATM*/
    public class ConfiguracionServicio
    {

        /*Nombre del servicio que representa ejm servicio atm*/
        public string nombreServicio
        {
            get;
            set;
        }
        /*Número del sercio a que corresponde*/
        public string numero
        {
            get;
            set;
        }
        /*Direccion Ip por la cual va atender las peticiones
         * 0.0.0.0 atiende a todas las interfaces de red que tiene
         * la maquina, por seguridad se recomienda colocar la ip
         * de la máquina
         */
        public string ip
        {
            get;
            set;
        }

        /*Nombre de la DLL que va a utilizar el servicio. en la
         DLL debe estar la lógica para procesar la solicitud
         */
        public string dll
        {
            get;
            set;
        }

        /*Puerto de comunicación que va a atender las
         * peticiones de los clientes o terminales 
         */
        public int puerto
        {
            get;
            set;

        }

        /*carpeta o ruta donde se van a guardar los archivos de errores */
        public string pathLogServicio
        {
            get;
            set;
        }

        

        /*Estado del servicio inciado o parado*/
        public bool estado
        {
            get;
            set;
        }

        /*Nombre de usuario para conectarse a la base*/
        public string nombreUsuario
        {
            get;
            set;
        }

        /*cadena de conexion de base de datos*/
        public string conexion
        {
            get;
            set;
        }

    }
}
