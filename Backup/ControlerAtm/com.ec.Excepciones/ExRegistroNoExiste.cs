using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlerAtm.com.ec.Excepciones
{
    public class ExRegistroNoExiste : Exception
    {
        public ExRegistroNoExiste() : base() { }
        public ExRegistroNoExiste(string mensaje) : base(mensaje.Replace(Convert.ToChar("'"), ' ').Replace('\n', ' ')) { }
        public ExRegistroNoExiste(string mensaje, Exception union) : base(mensaje, union) { }

        // Permite que la excepcion se propague en en todas las clases del sistema
        protected ExRegistroNoExiste(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
