using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ControlerAtm.com.ec.objetos
{
    [DataContract]
    public class BeanMenuPerfil
    {
        [DataMember]
        public string nombreMenu
        {
            set;
            get;
        }

        [DataMember]
        public string urlMenuOpciones
        {
            set;
            get;
        }

        [DataMember]
        public string menuPadre
        {
            set;
            get;
        }

        [DataMember]
        public int idMenuOPciones
        {
            set;
            get;
        }

        [DataMember]
        public bool isMenuActivo
        {
            set;
            get;
        }
    
    }
}
