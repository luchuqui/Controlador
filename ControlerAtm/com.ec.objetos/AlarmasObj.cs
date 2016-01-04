using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ControlerAtm.com.ec.objetos
{
    [DataContract]
    public class AlarmasObj
    {
        [DataMember]
        public int id_alarma
        {
            set;
            get;
        }

        [DataMember]
        public string mensaje
        {
            set;
            get;
        }

        [DataMember]
        public int id_atm
        {
            set;
            get;
        }

        [DataMember]
        public DateTime fecha_registro
        {
            set;
            get;
        }

        [DataMember]
        public int envio_recepcion
        {
            set;
            get;
        }

        [DataMember]
        public string id_mensaje
        {
            set;
            get;
        }

        [DataMember]
        public string id_tipo_dispositivo
        {
            set;
            get;
        }

        [DataMember]
        public string estado_dispositivo
        {
            set;
            get;
        }

        [DataMember]
        public string error_severidad
        {
            set;
            get;
        }

        [DataMember]
        public string estado_diagnostico
        {
            set;
            get;
        }

        [DataMember]
        public string estado_suministro
        {
            set;
            get;
        }

        [DataMember]
        public string tipo_mensaje
        {
            set;
            get;
        }

        [DataMember]
        public string tipo_comando
        {
            set;
            get;
        }
        [DataMember]
        public string tipo_alarma
        {
            set;
            get;
        }
        
        [DataMember]
        public string descriptor
        {
            set;
            get;
        }
    }
}
