using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlerAtm.com.ec.objetos;
namespace ControlerAtm.com.ec.Interfaces
{
    interface BaseDatosDao
    {
        void abrir_conexion_base();
        void cerrar_conexion_base();
        void cambiar_cadena_conexion(string urlNueva);
        void insertar_usuario(UsuarioObj usuario);
        UsuarioObj obtener_usuario(string nick_usuario);
        void insertar_perfil(PerfilObj perfil);
        List<MenuObj> obtener_menu(UsuarioObj usuario);
        void insertar_modelo(ModeloObj modelo);
        void insertar_alarmas(AlarmasObj alarmas);
        void insertar_atm(AtmObj atm);
        void insertar_suceso(SucesoObj suceso);
        void insertar_menu(MenuObj menu);
        void insertar_responsable_atm(AtmObj atm, UsuarioObj usuario);
        void insertar_menu_perfil(MenuObj menu, PerfilObj perfil);
        void insertar_log_suceso(SucesoObj suceso, UsuarioObj usuario);
        List<UsuarioObj> obtener_usuario_por(string datoEntrada, bool tipo_busqueda);
        List<PerfilObj> obtener_perfil(bool estado);
        void actualizar_usuario(UsuarioObj usuario);
        ParametroObj obtenerParametro(int id_parametro);
        void actualizar_perfil(PerfilObj obj,List<MenuObj> menus);
        void insertar_perfil(PerfilObj obj, List<MenuObj> menus);
        int obtener_clave_tabla(string nombreTabla,string nombreColumna);
        int borrar_perfil_usuario(PerfilObj perfil, MenuObj menu);
        List<MenuObj> obtener_menu_estado(bool estado);
        List<MenuObj> obtener_menu_padre();
        List<MenuObj> obtener_menu_hijo(int id_menu_padre);
        List<ModeloObj> obtener_modelo_terminal(bool estado);
        void actualizar_terminal(AtmObj terminal);
        List <AtmObj> obtener_terminal(string busqueda, bool tipo_busqueda);
        List<AtmObj> obtener_terminalByUsuario(UsuarioObj usuario);
        List<AtmObj> obtener_terminalByUsuario_NoAsignados(UsuarioObj usuario);
        string guardar_terminales_asignados(List<AtmObj> terminales,UsuarioObj responsable);
        string borrar_datos_terminales_usuario(UsuarioObj responsable);
        List<AvanceObj> buscar_evento_by_usuario(UsuarioObj usuario);
        string actualizar_evento_by_usuario(UsuarioObj usuarioCambia,AvanceObj evento);
        string insertar_avance_terminal(AvanceObj evento);
        List<AlarmasObj> obtener_alarma(AlarmasObj alarma);
        UsuarioObj obtener_usuario_id(UsuarioObj usr);
        List<BeanMenuPerfil> obtener_perfil_menu(int codigoPerfil);
        List<MonitoreoDispositivos> obtener_alarmaByUsuario(UsuarioObj usuario);
        void insertar_actualizar_monitoreo_dispositivos(MonitoreoDispositivos monitoreo);
    }
}
