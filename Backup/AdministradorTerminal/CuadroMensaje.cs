using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace AdministradorTerminal
{
    public class CuadroMensaje
    {
        private Type tipo;
        private Object sender;

        public CuadroMensaje(Object envio,Type tipo) {
            this.tipo = tipo;
            sender = envio;
        }

        public void mostrar_mensaje_alerta(string mensaje) {
            mensaje = mensaje.Replace('\n', ' ');
            mensaje = "alert('" + mensaje + "')";
            //ScriptManager.RegisterStartupScript((sender as Control), tipo, "alert", mensaje, true);
            ScriptManager.RegisterClientScriptBlock((sender as Control), tipo, "alert", mensaje, true);
        }
    }
}
