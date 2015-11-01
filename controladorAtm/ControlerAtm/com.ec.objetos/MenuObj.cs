using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ControlerAtm.com.ec.objetos
{
    [DataContract]
    public class MenuObj
    {
        [DataMember]
        public int id_menu
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
        public string descripcion
        {
            set;
            get;
        }

        [DataMember]
        public string url
        {
            set;
            get;
        }

        [DataMember]
        public int codigo_menu_padre
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
