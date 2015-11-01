using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ControlerAtm.com.ec.objetos
{
    [DataContract]
    public class PerfilObj
    {
        [DataMember]
        public string nombre
        {
            get;
            set;
        }

        [DataMember]
        public int id
        {
            get;
            set;
        }
 
        [DataMember]
        public string descripcion
        {
            get;
            set;
        }



    }
}
