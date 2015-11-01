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

    }

}
