using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlerAtm.com.ec.Excepciones
{
    public class ExActualizarRegistro : Exception
    {
        public ExActualizarRegistro() : base() { }
        public ExActualizarRegistro(string mensaje) : base(mensaje.Replace(Convert.ToChar("'"), ' ').Replace('\n', ' ')) { }
        public ExActualizarRegistro(string mensaje, Exception union) : base(mensaje, union) { }

        // Permite que la excepcion se propague en en todas las clases del sistema
        protected ExActualizarRegistro(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
