using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlerAtm.com.ec.Excepciones
{
    public class ExpObtenerRegistro : Exception
    {
        public ExpObtenerRegistro() : base() { }
        public ExpObtenerRegistro(string mensaje) : base(mensaje.Replace(Convert.ToChar("'"), ' ').Replace('\n', ' ')) { }
        public ExpObtenerRegistro(string mensaje, Exception union) : base(mensaje, union) { }

        // Permite que la excepcion se propague en en todas las clases del sistema
        protected ExpObtenerRegistro(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }
}
