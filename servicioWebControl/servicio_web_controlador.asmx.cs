using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using ControlerAtm.com.ec.BaseDatos;
using ControlerAtm.com.ec.objetos;
using ControlerAtm.com.ec.Excepciones;
using ControlerAtm.Utilitario;
using ControlerAtm.LogicaNegocio;
namespace servicioWebControl
{
    /// <summary>
    /// Descripción breve de Service1
    /// </summary>
    [WebService(Namespace = "http://controladorAtm.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Service1 : System.Web.Services.WebService
    {
        private string conexionSql = string.Empty;// almacena la cadena de conexion SQL
        private ControlSistema controlUsr;
        public Service1()
        {
            conexionSql = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
            controlUsr = new ControlSistema(conexionSql);
        }

        [WebMethod]
        public UsuarioObj login_usuario(string nick_usuario,string contrase)
        {
            return controlUsr.control_login_usuario(nick_usuario,contrase);
        }

        [WebMethod]
      public List<UsuarioObj> buscar_usuario_by(string busqueda,bool opcion)
        {
            return controlUsr.control_buscar_usuario_by(busqueda,opcion);
        }
      
        [WebMethod]
        public List <AtmObj> buscar_terminal(string busqueda, bool tipo_busqueda)
        {
            return controlUsr.control_buscar_terminal(busqueda, tipo_busqueda);
        }

        [WebMethod]
        public List<AtmObj> obtenerTerminalPorUsuario(UsuarioObj usuario)
        {
            return controlUsr.control_buscar_terminalByUsr(usuario);
        }
        [WebMethod]
        public List<AtmObj> obtenerTerminalPorUsuario_NoAsignado(UsuarioObj usuario)
        {
            return controlUsr.control_buscar_terminalByUsr_NoAsignado(usuario);
        }

        [WebMethod]
        public List<PerfilObj> obtener_perfil(bool estado)
        {
            return controlUsr.control_obtener_perfil(estado);
        }

        [WebMethod]
        public string guardar_actualizar_usuario(UsuarioObj usuario,bool actualizar) {
            return controlUsr.control_guardar_actualizar_usuario(usuario,actualizar);
        }

        [WebMethod]
        public string guardar_actualizar_pass_usuario(UsuarioObj usuario,string passwordNuevo, bool generar)
        {
            return controlUsr.control_cambio_contrasenia(usuario,passwordNuevo,generar);
        }

        [WebMethod]
        public List<MenuObj> obtenerMenuUsuario(UsuarioObj u) {
            return controlUsr.control_obtenerMenuUsuario(u);
        }

        [WebMethod]
        public string guardar_actualizar_Perfil(PerfilObj perfil,List<MenuObj> menus, bool actualizar)
        {
            return controlUsr.control_guardar_actualizar_perfil(perfil,menus,actualizar);
        }

        [WebMethod]
        public string guardar_actualizar_perfil_(PerfilObj perfil, MenuObj menu, bool insertar)
        {
            return controlUsr.control_guardar_actualizar_perfil_menu(perfil, menu, insertar);
        }

        [WebMethod]
        public List<MenuObj> obtener_menu_codigo(int codigo)
        {
            return controlUsr.control_obtener_menu(codigo);
        }

        [WebMethod]
        public List<ModeloObj> obtener_modelo_terminal(bool estadoTerminal)
        {
            return controlUsr.control_obtener_modelo(estadoTerminal);
        }

        [WebMethod]
        public string guardar_actualizar_terminal(AtmObj terminal, bool actualizar)
        {
            return controlUsr.control_guardar_actualizar_terminal(terminal, actualizar);
        }

        [WebMethod]
        public string guardar_actualizar_responsable_terminal(List<AtmObj> terminales, UsuarioObj usuario)
        {
            return controlUsr.control_insertar_terminales_usuario(terminales, usuario);
        }

        [WebMethod]
        public List<AvanceObj> obtener_avance_by_usuario(UsuarioObj usuario)
        {
            return controlUsr.control_buscar_avance_by_usuario(usuario);
        }

        [WebMethod]
        public string actualizar_avance_by_usuario(UsuarioObj usuario,AvanceObj avance)
        {
            return controlUsr.control_insertar_actualizar_avance(avance,usuario,true);
        }

        [WebMethod]
        public List<AlarmasObj> obtener_alarma_atm(AlarmasObj alarma)
        {
            return controlUsr.control_buscar_alarma_atm(alarma);
        }

        [WebMethod]
        public UsuarioObj obtener_usuario_por_id(UsuarioObj usr)
        {
            return controlUsr.buscar_usuario_by_id(usr);
        }

        [WebMethod]
        public List<BeanMenuPerfil> obtener_menu_usuario_perfil(PerfilObj perfil)
        {
            return controlUsr.control_obtener_menu_perfil(perfil);
        }

        [WebMethod]
        public List<MonitoreoDispositivos> obtener_monitoreo_dipositivos(UsuarioObj usuario)
        {
            return controlUsr.control_monitoreo_dispositivos(usuario);
        }

        [WebMethod]
        public string enviarComandoTerminal(AtmObj terminal,string comando)
        {
            string ipMonitoreo = ConfigurationManager.AppSettings["servidorMonitoreo"];
            string mensaje = string.Empty;
            if (!string.IsNullOrEmpty(ipMonitoreo)) {
                mensaje = controlUsr.control_envioComando(terminal, comando, ipMonitoreo);
            }else{
                mensaje = "No esta configurado para enviar los comandos, revise su configuracion";
            }
            return mensaje;
        }

        [WebMethod]
        public List<DetalleDescripcionObj> obtener_detalle_alarma_terminal(AtmObj atm)
        {
            /*AtmObj atm = new AtmObj();
            atm.id_atm = 1;
            atm.codigo = "0932";*/
            try
            {
                return controlUsr.controlObtencionDescripcion(atm);
            }
            catch (Exception e) {
                List<DetalleDescripcionObj> ls = new List<DetalleDescripcionObj>();
                ls.Add(new DetalleDescripcionObj());
                ls[0].descripcion_mensaje = e.Message + " _ " + e.StackTrace;
                return ls;
            }
        }

    }
}
