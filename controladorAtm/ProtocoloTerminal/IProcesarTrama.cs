﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlerAtm.com.ec.objetos;

namespace controladorAtm.ProtocoloTerminal
{
    interface IProcesarTrama
    {
        AlarmasObj parseaTramaIngreso(string tramaIng);
        MonitoreoDispositivos parseaTramaAlarmaDispositivo(AlarmasObj alarma);
    }
}
