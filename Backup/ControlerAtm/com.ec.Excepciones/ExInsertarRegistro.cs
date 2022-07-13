using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlerAtm.com.ec.Excepciones
{
    public class ExInsertarRegistro : Exception
    {
        public ExInsertarRegistro() : base() { }
        public ExInsertarRegistro(string mensaje) : base(mensaje.Replace(Convert.ToChar("'"), ' ').Split('\n')[0]) { }
        public ExInsertarRegistro(string mensaje, Exception union) : base(mensaje, union) { }

        // Permite que la excepcion se propague en en todas las clases del sistema
        protected ExInsertarRegistro(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
