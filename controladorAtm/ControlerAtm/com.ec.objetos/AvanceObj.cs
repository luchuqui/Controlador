using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ControlerAtm.com.ec.objetos
{
    [DataContract]
    public class AvanceObj
    {
        [DataMember]
        public int id_avance
        {
            set;
            get;
        }
        
        [DataMember]
        public string fecha_atencion
        {
            set;
            get;
        }
        
        [DataMember]
        public bool notificacion
        {
            set;
            get;
        }
        
        [DataMember]
        public bool atendido
        {
            set;
            get;
        }

        [DataMember]
        public string observacion
        {
            set;
            get;
        }

        [DataMember]
        public int id_alarma
        {
            set;
            get;
        }

        [DataMember]
        public int usuario_atiende
        {
            set;
            get;
        }

        [DataMember]
        public int usuario_notifica
        {
            set;
            get;
        }

        [DataMember]
        public string fecha_registro
        {
            set;
            get;
        }
    }
}
