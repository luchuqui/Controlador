using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace controladorAtm
{
    public class ErrorConexionTerminal : Exception
    {
        public ErrorConexionTerminal(string mensaje) : base(mensaje) {            
        }
        public ErrorConexionTerminal() : base() { 
        }
        public ErrorConexionTerminal(string message, System.Exception inner) : base(message, inner) { }

        protected ErrorConexionTerminal(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) { }
    }
}
