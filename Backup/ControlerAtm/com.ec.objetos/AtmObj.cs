using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ControlerAtm.com.ec.objetos
{
    [DataContract]
    public class AtmObj
    {
        [DataMember]
        public int id_atm
        {
            set;
            get;
        }

        [DataMember]
        public string ip
        {
            set;
            get;
        }

        [DataMember]
        public string codigo
        {
            set;
            get;
        }

        [DataMember]
        public string ubicacion
        {
            set;
            get;
        }

        [DataMember]
        public string estado
        {
            set;
            get;
        }

        [DataMember]
        public int id_modelo
        {
            set;
            get;
        }
        
        [DataMember]
        public bool conexion
        {
            set;
            get;
        }
        [DataMember]
        public bool modoSupervisor
        {
            set;
            get;
        }

    }

}
