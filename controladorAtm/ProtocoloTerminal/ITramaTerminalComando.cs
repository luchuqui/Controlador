using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace controladorAtm
{
    /*Interfaz para manejar los comandos que se envian al cajero
     Se ha creado esta interfaz debido a que el programa 
     * deberia de procesar cualquier tipo de trama sea NDC o 912(DIEBOLD)
     */
    interface ITramaTerminalComando
    {
        string getTramaComandoTerminal();

        void setPonerEnServicio();

        void setPonerFueraServicio();

        void setEnviarConfiguracion(string trama);

        void setSolicitarInformacionConfiguracion();

        void setEnviarInformacionFechaHora();

        void setEnviarSolicitudContadores();
    }
}
