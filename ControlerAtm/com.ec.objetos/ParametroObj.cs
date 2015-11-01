using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ControlerAtm.com.ec.objetos
{
    [DataContract]
    public class ParametroObj
    {
        [DataMember]
        public int id_parametro
        {
            set;
            get;
        }

        [DataMember]
        public string valor
        {
            set;
            get;
        }

        [DataMember]
        public string descripcion
        {
            set;
            get;
        }
        [DataMember]
        public bool estado
        {
            set;
            get;
        }
    }
}
