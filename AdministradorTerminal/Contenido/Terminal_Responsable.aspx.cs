using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AdministradorTerminal.WSControlador;

namespace AdministradorTerminal.Contenido
{
    public partial class Termina_Responsable : System.Web.UI.Page
    {
        private int usuarioSeleccionado;
        private static string nombre_terminal;
        private static string terminal_seleccionado;
        private static string nombre_terminal_quitar;
        private static string terminal_seleccionado_quitar;
        public void btn_buscarUsuario(Object sender, EventArgs e)
        {
            string seleccionado = this.rbGroup.SelectedItem.Text;
            UsuarioObj[] lsusr = { };
            Session["usuariosSistema"] = null;
            if (seleccionado.Equals("Documento"))
            {
                lsusr = Globales.servicio.buscar_usuario_by(this.txbxIngreso.Text, true);
            }
            else if (seleccionado.Equals("Nombre"))
            {
                lsusr = Globales.servicio.buscar_usuario_by(this.txbxIngreso.Text, false);
            }
            cargar_datos_usuario(lsusr);
        }

        public void btn_guardar_datos(object sender,EventArgs e) {
            AtmObj[] terminales = new AtmObj[lista_Atm_asignados.Items.Count];
            int i = 0;
            usuarioSeleccionado = int.Parse(lista_Usuarios.SelectedValue);
            UsuarioObj u = new UsuarioObj();
            u.id = usuarioSeleccionado;
            foreach (ListItem terminal in lista_Atm_asignados.Items) {
                AtmObj atm = new AtmObj();
                atm.id_atm = int.Parse(terminal.Value);
                terminales[i] = atm;
                i++;
            }
            string respuesta = Globales.servicio.guardar_actualizar_responsable_terminal(terminales,u);
            CuadroMensaje mensajeNotificacion = new CuadroMensaje(sender, this.GetType());
            labelUpdt.Text = respuesta;
            //mensajeNotificacion.mostrar_mensaje_alerta(respuesta);
        }

        public void limpiar_datos(object sender,EventArgs e) {
            lista_Atm_asignados.Items.Clear();
            lista_Atm_no_asignados.Items.Clear();
            txbxIngreso.Text = "";
            lista_Usuarios.Items.Clear();
            usuarioSeleccionado = 0;
        }

        public void cargar_datos_usuario(UsuarioObj[] lsusr)
        {
            lista_Atm_asignados.Items.Clear();
            lista_Atm_no_asignados.Items.Clear();
            if (lsusr != null)
            {
                lista_Usuarios.Items.Clear();
                lista_Usuarios.Items.Add(new ListItem("Seleccione Usuario", "0"));
                foreach (UsuarioObj usr in lsusr)
                {
                    lista_Usuarios.Items.Add(new ListItem(usr.apellido + " " + usr.nombre, usr.id.ToString()));
                }

            }
        
        }

        public void cargar_terminales_no_asignados(Object sender, EventArgs o)

        {
            lista_Atm_no_asignados.Items.Clear();
            lista_Atm_asignados.Items.Clear();
            usuarioSeleccionado = int.Parse(lista_Usuarios.SelectedValue);
            UsuarioObj usr=new UsuarioObj ();
            usr.id = usuarioSeleccionado;
            AtmObj[]atms = Globales.servicio.obtenerTerminalPorUsuario_NoAsignado(usr);
            foreach (AtmObj atm in atms)
            {
                lista_Atm_no_asignados.Items.Add(new ListItem(atm.codigo, atm.id_atm.ToString()));
            }
            cargar_terminales_asignados();
        }

        public void cargar_terminales_asignados()
        {
           
            usuarioSeleccionado = int.Parse(lista_Usuarios.SelectedValue);
            UsuarioObj usr = new UsuarioObj();
            usr.id = usuarioSeleccionado;
            AtmObj[] atms = Globales.servicio.obtenerTerminalPorUsuario(usr);
            foreach (AtmObj atm in atms)
            {
                lista_Atm_asignados.Items.Add(new ListItem(atm.codigo, atm.id_atm.ToString()));
            }
        }

        public void cargar_guardar_terminales_asignados(Object sender, EventArgs e)
        {
                    agregar_terminales_usuario(sender,e);
                    btn_guardar_datos(sender, e);
        }

        public void quitar_guardar_terminales_asignados(Object sender, EventArgs e)
        {
            quitar_terminales_usuario(sender, e);
            btn_guardar_datos(sender, e);
        }

        public void cargar_todos_terminales_asignados(Object sender, EventArgs e)

        {

            agregar_terminales_usuario(sender, e);
            btn_guardar_datos(sender, e);
        
        }

        public void agregar_terminales_usuario(Object sender, EventArgs e)
        {
            try
            {
                if (nombre_terminal != null && terminal_seleccionado != null)
                {
                    lista_Atm_asignados.Items.Add(new ListItem(nombre_terminal, terminal_seleccionado));
                    lista_Atm_no_asignados.Items.Remove(new ListItem(nombre_terminal, terminal_seleccionado));
                    nombre_terminal = null;
                    terminal_seleccionado = null;
                }
            }
            catch (NullReferenceException ex)
            {
              string m=ex.Message;
            }
        }

        public void agregar_terminales_usuario_todos(Object sender, EventArgs e)
        {
            try
            {
                foreach(ListItem  terminal in lista_Atm_no_asignados.Items){
                    lista_Atm_asignados.Items.Add(new ListItem(terminal.Text, terminal.Value));
                }
                if(lista_Atm_no_asignados.Items.Count>0){
                lista_Atm_no_asignados.Items.Clear();
                btn_guardar_datos(sender, e);
                }else{
                    // aqui el mensaje que no hay terminales para colocar
                }

            }
            catch (InvalidOperationException ex)
            {
                string m = ex.Message;
            }
            catch (NullReferenceException ex)
            {
                string m = ex.Message;
            }
        }

        public void quitar_terminales_usuario(Object sender, EventArgs e)
        {
            try
            {
                if (nombre_terminal_quitar != null && terminal_seleccionado_quitar != null)
                {
                    lista_Atm_no_asignados.Items.Add(new ListItem(nombre_terminal_quitar, terminal_seleccionado_quitar));
                    lista_Atm_asignados.Items.Remove(new ListItem(nombre_terminal_quitar, terminal_seleccionado_quitar));
                    nombre_terminal_quitar = null;
                    terminal_seleccionado_quitar = null;
                }
            }
            catch (NullReferenceException ex)
            {
                string m = ex.Message;
            }
        }
        public void quitar_terminales_usuario_todos(Object sender, EventArgs e)
        {
            try
            {
                foreach (ListItem terminal in lista_Atm_asignados.Items)
                {
                    lista_Atm_no_asignados.Items.Add(new ListItem(terminal.Text, terminal.Value));
                }
                if (lista_Atm_asignados.Items.Count > 0)
                {
                    lista_Atm_asignados.Items.Clear();
                    btn_guardar_datos(sender, e);
                }
                else
                {
                    // aqui el mensaje que no hay terminales para colocar
                }

            }
            catch (InvalidOperationException ex)
            {
                string m = ex.Message;
            }
            catch (NullReferenceException ex)
            {
                string m = ex.Message;
            }
        }

        public void seleccionar_terminales_usuario(Object sender, EventArgs e)
        {
            try
            {
                terminal_seleccionado = lista_Atm_no_asignados.SelectedValue;
                nombre_terminal = lista_Atm_no_asignados.SelectedItem.Text;
                
            }
            catch (NullReferenceException ex)
            {
                string m = ex.Message;
            }
        }

        public void quitar_terminales_ls_usuario(Object sender, EventArgs e)
        {
            try
            {
                terminal_seleccionado_quitar = lista_Atm_asignados.SelectedValue;
                nombre_terminal_quitar = lista_Atm_asignados.SelectedItem.Text;
            }
            catch (NullReferenceException ex)
            {
                string m = ex.Message;
            }
        }
    
    }
}
