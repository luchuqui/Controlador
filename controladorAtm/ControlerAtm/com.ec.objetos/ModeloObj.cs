using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ControlerAtm.com.ec.objetos
{
    [DataContract]
    public class ModeloObj
    {
        [DataMember]
        public int id_modelo
        {
            set;
            get;
        }

        [DataMember]
        public string nombre
        {
            set;
            get;
        }

        [DataMember]
        public string fabricante
        {
            set;
            get;
        }

   

    }

}
