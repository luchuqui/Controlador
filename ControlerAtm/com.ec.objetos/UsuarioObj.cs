using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ControlerAtm.com.ec.objetos
{
    [DataContract]
    public class UsuarioObj
    {
        [DataMember]
        public int id
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
        public string apellido
        {
            set;
            get;
        }
        
        [DataMember]
        public string cedula
        {
            set;
            get;
        }
        
        [DataMember]
        public string correo
        {
            set;
            get;
        }
        
        [DataMember]
        public int id_perfil
        {
            set;
            get;
        }
        
        [DataMember]
        public string telefono
        {
            set;
            get;
        }
        
        [DataMember]
        public string contrasenia
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
        public int numero_intentos
        {
            set;
            get;
        }

        [DataMember]
        public bool cambio_contrasenia
        {
            set;
            get;
        }

    }
}
