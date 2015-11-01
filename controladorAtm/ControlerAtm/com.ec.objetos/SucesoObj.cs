using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ControlerAtm.com.ec.objetos
{
    [DataContract]
    public class SucesoObj
    {
        [DataMember]
        public int id_suceso
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
        public string fecha
        {
            set;
            get;
        }

        [DataMember]
        public string severidad
        {
            set;
            get;
        }

    }
}
