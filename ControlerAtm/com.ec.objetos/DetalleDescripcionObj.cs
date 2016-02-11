using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ControlerAtm.com.ec.objetos
{
    /*Descripcion de mensajes NDC filtrados para el usuaario*/
    [DataContract]
    public class DetalleDescripcionObj
    {
        [DataMember]
        public string tipo_mensaje
        {
            set;
            get;
        }

        [DataMember]
        public string mensaje_ndc
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
        public string tipo_estado
        {
            set;
            get;
        }

        [DataMember]
        public string descripcion_mensaje
        {
            set;
            get;
        }

        [DataMember]
        public string tipo_dispositivo
        {
            set;
            get;
        }

        [DataMember]
        public string detalle_descripcion        {
            set;
            get;
        }


    }
}
