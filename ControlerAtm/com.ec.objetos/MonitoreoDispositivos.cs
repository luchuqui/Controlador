using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ControlerAtm.com.ec.objetos
{
    [DataContract]
    public class MonitoreoDispositivos
    {
        [DataMember]
        public int id_atm
        {
            set;
            get;
        }

        [DataMember]
        public int estado_lectora
        {
            set;
            get;
        }


        [DataMember]
        public string codigo_atm
        {
            set;
            get;
        }
        [DataMember]
        public bool estado_conexio
        {
            set;
            get;
        }
        [DataMember]
        public bool modo_supervisor
        {
            set;
            get;
        }
        [DataMember]
        public bool llave_terminal
        {
            set;
            get;
        }
        [DataMember]
        public string estado_gaveta1
        {
            set;
            get;
        }
        [DataMember]
        public string estado_gaveta2
        {
            set;
            get;
        }
        [DataMember]
        public string estado_gaveta3
        {
            set;
            get;
        }
        [DataMember]
        public string estado_gaveta4
        {
            set;
            get;
        }
        [DataMember]
        public string estado_gaveta5
        {
            set;
            get;
        }
        [DataMember]
        public string estado_impresora
        {
            set;
            get;
        }
        [DataMember]
        public string estado_impresora_jrnl
        {
            set;
            get;
        }
        [DataMember]
        public string estado_dispensador
        {
            set;
            get;
        }
        [DataMember]
        public string estado_encriptora
        {
            set;
            get;
        }

        [DataMember]
        public string tipo_estado
        {
            set;
            get;
        }

    }
}
