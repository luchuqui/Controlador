using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace controladorAtm
{
    /*Implementa la interfaz TramaTerminalComando, esta clase 
     * Especifica todos los campos que posee la trama para enviar
     * el comando hacia el cajero
     */
    public class ComandoNdcTerminal : TramaTerminalComando
    {
        private string cabecera;
        private string claseMensaje;
        private string banderaRespuesta;
        private string separador;
        private string luno;
        private string numeroSecuencia;
        private string codigoComando;
        private string modificadorComando;
        private string trailer;

        public ComandoNdcTerminal() {
            cabecera = Char.ConvertFromUtf32(0).ToString()
                + Char.ConvertFromUtf32(7).ToString(); //BELL
            separador = Char.ConvertFromUtf32(28).ToString();
            claseMensaje = "1";
            banderaRespuesta = "";
            luno = "";
            numeroSecuencia = "";
            codigoComando = "";
            modificadorComando = "";
            trailer = "";
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

        #region Miembros de TramaTerminalComando

        public string getTramaComandoTerminal()
        {
            return cabecera
            + claseMensaje
            + banderaRespuesta
            + separador
            + luno
            + separador
            + numeroSecuencia
            + separador
            + codigoComando
            + modificadorComando
            + trailer;
        }

        public void setPonerEnServicio()
        {
            codigoComando = "1";
        }

        public void setPonerFueraServicio()
        {
            codigoComando = "2";
            modificadorComando = "0";// 0 poner pantalla fuera de servicio

        }

        public void setEnviarConfiguracion(string trama)
        {
            codigoComando = "3";
        }

        public void setSolicitarInformacionConfiguracion()
        {
            codigoComando = "7";
        }

        public void setEnviarInformacionFechaHora()
        {
            codigoComando = "8";
            modificadorComando = "4";// Se solicita el estadod sensores 
 
        }

        public void setEnviarSolicitudContadores() {
            codigoComando = "4";
            modificadorComando = "1"; // 1 solicita mensaje basico contadores
            // 2 solicita mensaje extendido
        }
        #endregion
    }
}
