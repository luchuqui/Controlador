using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlerAtm.Utilitario;
using ControlerAtm.com.ec.BaseDatos;
using ControlerAtm.com.ec.objetos;
using ControlerAtm.com.ec.Excepciones;

namespace ControlerAtm.LogicaNegocio
{
    /* Logica para almacenar actualizar los datos del usuario */
    public class ControlSistema
    {
        private Seguridad seguridad;
        private CorreoElectronico email;
        private MensajeTexto smsSend;
        private BddSQLServer conBdd;
        private LecturaEscrituraArchivo logSistema;
        private int numeroIntentos;
        private int longitudClave;

        public ControlSistema(string conexionBase) {
            conBdd = new BddSQLServer(conexionBase, "C:\\TEMP");
            string pathGuardar = conBdd.obtenerParametro(5).valor;
            seguridad = new Seguridad();
            email = new CorreoElectronico(pathGuardar);
            smsSend = new MensajeTexto(pathGuardar);
            conBdd.set_archivo_path_guradar(pathGuardar);
            logSistema = new LecturaEscrituraArchivo();
            logSistema.set_path_guardar(pathGuardar);
            logSistema.archivo_guardar("\\LOG_SISTEMA");
            string configCorreo = conBdd.obtenerParametro(1).valor;
            string configSMS = conBdd.obtenerParametro(6).valor;
            numeroIntentos = int.Parse(conBdd.obtenerParametro(2).valor);
            longitudClave = int.Parse(conBdd.obtenerParametro(4).valor);
            email.configurar_parametros(configCorreo.Split(':'));
            smsSend.configurar_parametros(configSMS.Split(':'));
        }

        public List<UsuarioObj> control_buscar_usuario_by(string busqueda, bool opcion)
        {
            conBdd.abrir_conexion_base();
            List<UsuarioObj> u = null;
            try
            {
                u = conBdd.obtener_usuario_por(busqueda, opcion);
            }
            catch (ExpObtenerRegistro ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                u = new List<UsuarioObj>();
            }
            catch (Exception ex) {
                logSistema.escritura_archivo_string(ex.Message);
                u = new List<UsuarioObj>();
            }
            finally
            {
                conBdd.cerrar_conexion_base();
            }
            return u;
        }

        public List<PerfilObj> control_obtener_perfil(bool estado)
        {
            conBdd.abrir_conexion_base();
            List<PerfilObj> p = new List<PerfilObj>();
            try
            {
                p = conBdd.obtener_perfil(estado);
            }
            catch (ExpObtenerRegistro ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                p = new List<PerfilObj>();
            }
            catch (Exception ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
            }
            finally
            {
                conBdd.cerrar_conexion_base();
            }
            return p;
        }

        public string control_guardar_actualizar_usuario(UsuarioObj usuario, bool actualizar)
        {
            conBdd.abrir_conexion_base();
            string mensaje = string.Empty;
            try
            {
                if (actualizar)
                {
                    conBdd.actualizar_usuario(usuario);
                    mensaje = "Actualizacion de datos usuario correcta";
                }
                else
                {
                    GeneracionClave clave = new GeneracionClave(longitudClave);
                    String password = clave.generarClaveUsuario();
                    if (!string.IsNullOrEmpty(password))
                    {
                        usuario.cambio_contrasenia = true;
                        usuario.contrasenia = seguridad.encriptar_informacion(password);
                        actualizar = true;
                        usuario.numero_intentos = 0;
                        notificacion("Usuario : " + usuario.nombre +" "+usuario.nombre + ", Contraseña Acceso : " + password
                            , MensajeSistema.contrasenia_generada, usuario);
                        conBdd.insertar_usuario(usuario);
                        mensaje = "Ingreso de nuevo usuario correcta";
                    }
                    else {
                        mensaje = "No se pudo realizar el ingreso de nuevo usuario correcta";
                    }
                }
            }
            catch (ExInsertarRegistro ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                mensaje = ex.Message;
            }
            catch (ExActualizarRegistro ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                mensaje = ex.Message;
            }
            finally
            {
                conBdd.cerrar_conexion_base();
            }
            return mensaje;
        }


        public List<MenuObj> control_obtenerMenuUsuario(UsuarioObj usuario)
        {
            List<MenuObj> menus = new List<MenuObj>();
            conBdd.abrir_conexion_base();
            try
            {
                menus = conBdd.obtener_menu(usuario);
            }
            catch (ExRegistroNoExiste e)
            {
                logSistema.escritura_archivo_string(e.Message);
            }
            catch (Exception ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
            }
            finally
            {
                conBdd.cerrar_conexion_base();
            }
            return menus;
        }

        public UsuarioObj control_login_usuario(string nick_usuario, string contrase)
        {
            conBdd.abrir_conexion_base();
            UsuarioObj u;
            string mensajeError = string.Empty;
            try
            {
                u = conBdd.obtener_usuario(nick_usuario);
                contrase = seguridad.encriptar_informacion(contrase);
                if (!u.contrasenia.Equals(contrase))
                {
                    if (u.numero_intentos > (numeroIntentos - 1))
                    {
                        u.estado = "B";// Bloqueo usuario
                        mensajeError = MensajeSistema.usuario_bloqueado;
                        notificacion("Su usuario ha sido bloqueado " +
                            "por el sistema debido a 3 intentos fallidos", MensajeSistema.usuario_bloqueado, u);
                    }
                    else if (u.estado.Equals("A"))
                    {
                        u.numero_intentos = u.numero_intentos + 1;
                        mensajeError = MensajeSistema.pass_incorrecto + " Queda " + (numeroIntentos - u.numero_intentos) + " intentos";
                    }
                    else if (u.estado.Equals("A"))
                    {
                        mensajeError = MensajeSistema.usuario_bloqueado;
                    }else {
                        mensajeError = MensajeSistema.usuario_no_reg;
                    }
                    conBdd.actualizar_usuario(u);
                    u = new UsuarioObj();
                    u.correo = mensajeError;
                }else if (u.estado.Equals("B")){
                    mensajeError = MensajeSistema.usuario_bloqueado;
                    u = new UsuarioObj();
                    u.correo = mensajeError;
                }
                u.contrasenia = null;
            }
            catch (ExRegistroNoExiste ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                u = new UsuarioObj();
                u.correo = ex.Message;
            }
            catch (ExpObtenerRegistro ex) {
                logSistema.escritura_archivo_string(ex.Message);
                u = new UsuarioObj();
                u.correo = ex.Message;
            }
            catch (Exception ex)
            {
                u = new UsuarioObj();
                u.correo = MensajeSistema.error_sistema;
                logSistema.escritura_archivo_string(MensajeSistema.error_sistema + ":" + ex.Message);
            }
            finally
            {
                conBdd.cerrar_conexion_base();
            }
            return u;
        }

        public string control_cambio_contrasenia(UsuarioObj usuario,string conNueva,bool generar) {
            string mensaje = "";
            bool actualizar = false;
            try
            {
                conBdd.abrir_conexion_base();
                if (generar)
                { // True es para generar la clave aleatorea
                    GeneracionClave clave = new GeneracionClave(longitudClave);
                    conNueva = clave.generarClaveUsuario();
                    if (!string.IsNullOrEmpty(conNueva))
                    {
                        usuario.cambio_contrasenia = true;
                        usuario.contrasenia = seguridad.encriptar_informacion(conNueva);
                        actualizar = true;
                        usuario.numero_intentos = 0;
                        int [] isnotificado = notificacion("Usuario : " + usuario.nombre + " " + usuario.apellido+", Contraseña Reseteada : " + conNueva
                            , MensajeSistema.contrasenia_generada, usuario);
                        mensaje = "Contraseña de usuario generada correctamente, se ha enviado una notificación vía";
                        if (isnotificado[0] > 1) {
                            mensaje += ", No Configurado Correo";
                        }
                        else if (isnotificado[0] == 0)
                        {
                            mensaje += ", Correo [OK]";
                        }
                        else {
                            mensaje += ", Correo [FALLIDO]";
                        }
                        if (isnotificado[1] > 1)
                        {
                            mensaje += ", No Configurado Correo";
                        }
                        else if (isnotificado[1] == 0)
                        {
                            mensaje += ", SMS [OK]";
                        }
                        else
                        {
                            mensaje += ", SMS [FALLIDO]";
                        }

                    }else{
                        mensaje = MensajeSistema.contrasenia_no_generada;
                    }
                }else{
                    conNueva = seguridad.encriptar_informacion(conNueva);
                    string conAnt = seguridad.encriptar_informacion(usuario.contrasenia);
                    UsuarioObj utm = conBdd.obtener_usuario(usuario.cedula);
                    if (utm.contrasenia.Equals(conAnt))
                    {
                        usuario.contrasenia = conNueva;
                        usuario.cambio_contrasenia = false;
                        actualizar = true;
                        mensaje = "Contraseña de usuario actulizada correctamente";
                    }
                    else {
                        mensaje = MensajeSistema.error_contrasenia_ingresada;

                    }
                }
                if (actualizar)
                {
                    conBdd.actualizar_usuario(usuario);
                }
                else {
                    mensaje = "No se actulizo contraseña de usuario";
                }
            }
            catch (ExActualizarRegistro ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                mensaje = ex.Message;
            }
            catch (ExConexionBase ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                mensaje = MensajeSistema.error_Conexion;
            }
            catch (Exception ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                mensaje = MensajeSistema.error_sistema;
            }
            finally {
                conBdd.cerrar_conexion_base();
            }

            return mensaje;
        }

        public int[] notificacion(string mensaje,string titulo,UsuarioObj u) {
            string[] tipoNotificacion = conBdd.obtenerParametro(3).valor.Split(':');
            bool correo = (tipoNotificacion[0] == "1");
            bool celular = (tipoNotificacion[1] == "1");
            int[] isEnvio = { 2, 2 };
            if (correo)
            {
                string[] destinatario = { u.correo };
                email.asignar_destinatarios(destinatario);
                isEnvio[0] = email.enviar_notificacion(mensaje, titulo);
            }
            if (celular) {
                string[] numero = { u.telefono };
                smsSend.asignar_destinatarios(numero);
                smsSend.abrir_conexion();
                isEnvio[1] = smsSend.enviar_notificacion(mensaje, "");
                smsSend.cerrar_conexion();
            }
            return isEnvio;
        }

        public string control_guardar_actualizar_perfil(PerfilObj perfil,List<MenuObj> menus, bool actualizar)
        {
            conBdd.abrir_conexion_base();
            string mensaje = string.Empty;
            try
            {
                if (actualizar)
                {
                    //conBdd.borrar_perfil_usuario(perfil.id);
                    conBdd.actualizar_perfil(perfil, menus);
                    mensaje = "Actualizacion de datos usuario correcta";
                    
                }
                else
                {
                    conBdd.insertar_perfil(perfil,menus);
                    mensaje = "Ingreso de nuevo perfil - Menu correcto";
                }
            }
            catch (ExInsertarRegistro ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                mensaje = ex.Message;
            }
            catch (ExActualizarRegistro ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                mensaje = ex.Message;
            }
            finally
            {
                conBdd.cerrar_conexion_base();
            }
            return mensaje;
        }

        public string control_guardar_actualizar_perfil_menu(PerfilObj perfil, MenuObj menu, bool insertar)
        {
            conBdd.abrir_conexion_base();
            string mensaje = string.Empty;
            try
            {
                if (insertar)
                {
                    conBdd.insertar_menu_perfil(menu, perfil);
                    mensaje = "Ingreso de datos usuario correcta";

                }
                else // quita el elemento
                {
                    conBdd.borrar_perfil_usuario(perfil,menu);
                    mensaje = "Eliminacion Menu correcto";
                }
            }
            catch (ExInsertarRegistro ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                mensaje = ex.Message;
            }
            catch (ExActualizarRegistro ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                mensaje = ex.Message;
            }
            finally
            {
                conBdd.cerrar_conexion_base();
            }
            return mensaje;
        }


        public List<MenuObj> control_obtener_menu(bool estadoMenu)
        {
            conBdd.abrir_conexion_base();
            List<MenuObj> menus = new List<MenuObj>();
            MenuObj menu = new MenuObj();
            try
            {
               menus = conBdd.obtener_menu_estado(estadoMenu);
            }
            catch (ExInsertarRegistro ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                menu.url = ex.Message;
            }
            catch (ExActualizarRegistro ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                menu.url = ex.Message;
            }
            catch (Exception ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                menu.url = ex.Message;
            }
            finally
            {
                conBdd.cerrar_conexion_base();
            }
            return menus;
        }

        public List<MenuObj> control_obtener_menu(int id_menu)
        {
            conBdd.abrir_conexion_base();
            List<MenuObj> menus = new List<MenuObj>();
            MenuObj menu = new MenuObj();
            try
            {
                menus = conBdd.obtener_menu_hijo(id_menu);
            }
            catch (ExInsertarRegistro ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                menu.url = ex.Message;
            }
            catch (ExActualizarRegistro ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                menu.url = ex.Message;
            }
            catch (Exception ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                menu.url = ex.Message;
            }
            finally
            {
                conBdd.cerrar_conexion_base();
            }
                return menus;
        }

        public List<ModeloObj> control_obtener_modelo(bool estado)
        {
            conBdd.abrir_conexion_base();
            List<ModeloObj> modelos = new List<ModeloObj>();
            ModeloObj modelo = new ModeloObj();
            try
            {
                modelos = conBdd.obtener_modelo_terminal(estado);
            }
            catch (ExInsertarRegistro ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                modelo.fabricante = ex.Message;
            }
            catch (ExActualizarRegistro ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                modelo.fabricante = ex.Message;
            }
            catch (Exception ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                modelo.fabricante = ex.Message;
            }
            finally
            {
                conBdd.cerrar_conexion_base();
            }
            return modelos;
        }

        public string control_guardar_actualizar_terminal(AtmObj terminal, bool actualizar)
        {
            conBdd.abrir_conexion_base();
            string mensaje = string.Empty;
            try
            {
                if (actualizar)
                {
                    conBdd.actualizar_terminal(terminal);
                    mensaje = "Actualizacion de datos terminal correcto";
                }
                else
                {
                    conBdd.insertar_atm(terminal);
                    mensaje = "Ingreso de nuevo terminal correcto";
                }
            }
            catch (ExInsertarRegistro ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                mensaje = ex.Message;
            }
            catch (ExActualizarRegistro ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                mensaje = ex.Message;
            }
            catch (Exception ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                mensaje = ex.Message;
            }
            finally
            {
                conBdd.cerrar_conexion_base();
            }
            return mensaje;
        }

        public List <AtmObj> control_buscar_terminal(string busqueda, bool tipo_busqueda)
        {
            conBdd.abrir_conexion_base();
            List <AtmObj> t = null;
            try
            {
                t = conBdd.obtener_terminal(busqueda, tipo_busqueda);
            }
            catch (ExpObtenerRegistro ex)
            {
                t = new List <AtmObj>();
                AtmObj tmp = new AtmObj();
                tmp.ubicacion = MensajeSistema.reg_no_existe + " Terminal ";
                t.Add(tmp);
                logSistema.escritura_archivo_string(ex.Message + "\n" + ex.StackTrace);
                
            }
            catch (Exception ex)
            {
                t = new List<AtmObj>();
                AtmObj tmp = new AtmObj();
                tmp.ubicacion = MensajeSistema.error_sistema + " Terminal ";
                t.Add(tmp);
                logSistema.escritura_archivo_string(ex.Message +"\n"+ ex.StackTrace);
            }
            finally
            {
                conBdd.cerrar_conexion_base();
            }
            return t;
        }

        public List<AtmObj> control_buscar_terminalByUsr(UsuarioObj usuario)
        {
            conBdd.abrir_conexion_base();
            List<AtmObj> t = null;
            try
            {
                t = conBdd.obtener_terminalByUsuario(usuario);
            }
            catch (ExpObtenerRegistro ex)
            {
                t = new List<AtmObj>();
                AtmObj tmp = new AtmObj();
                tmp.ubicacion = MensajeSistema.reg_no_existe + " Terminal ";
                t.Add(tmp);
                logSistema.escritura_archivo_string(ex.Message);

            }
            catch (Exception ex)
            {
                t = new List<AtmObj>();
                AtmObj tmp = new AtmObj();
                tmp.ubicacion = MensajeSistema.error_sistema + " Terminal ";
                t.Add(tmp);
                logSistema.escritura_archivo_string(ex.Message);
            }
            finally
            {
                conBdd.cerrar_conexion_base();
            }
            return t;
        }
        
        public List<AtmObj> control_buscar_terminalByUsr_NoAsignado(UsuarioObj usuario)
        {
            conBdd.abrir_conexion_base();
            List<AtmObj> t = null;
            try
            {
                t = conBdd.obtener_terminalByUsuario_NoAsignados(usuario);
            }
            catch (ExpObtenerRegistro ex)
            {
                t = new List<AtmObj>();
                AtmObj tmp = new AtmObj();
                tmp.ubicacion = MensajeSistema.reg_no_existe + " Terminal ";
                t.Add(tmp);
                logSistema.escritura_archivo_string(ex.Message);

            }
            catch (Exception ex)
            {
                t = new List<AtmObj>();
                AtmObj tmp = new AtmObj();
                tmp.ubicacion = MensajeSistema.error_sistema + " Terminal ";
                t.Add(tmp);
                logSistema.escritura_archivo_string(ex.Message);
            }
            finally
            {
                conBdd.cerrar_conexion_base();
            }
            return t;
        }

        public string control_insertar_terminales_usuario(List<AtmObj> terminales,UsuarioObj responsable) {
            conBdd.abrir_conexion_base();
            string respuesta = string.Empty;
            try
            {
                respuesta = conBdd.borrar_datos_terminales_usuario(responsable);
                if (respuesta.Equals("000"))
                {
                    respuesta = conBdd.guardar_terminales_asignados(terminales, responsable);
                }
                else
                {
                    respuesta = "Error al intentar registrar terminales a usuario";
                }
            }
            catch (Exception e)
            {
                logSistema.escritura_archivo_string(e.Message);
                respuesta = e.Message;
            }
            finally {
                conBdd.cerrar_conexion_base();
            }
            return respuesta;
        }

        public string control_insertar_actualizar_avance(AvanceObj avance,UsuarioObj usuario,bool actualizar) {
            conBdd.abrir_conexion_base();
            string respuesta = string.Empty;
            try
            {
                if (actualizar)
                {
                    respuesta = conBdd.actualizar_evento_by_usuario(usuario, avance);
                }
                else
                {
                    avance.usuario_notifica = usuario.id;
                    respuesta = conBdd.insertar_avance_terminal(avance);
                }
            }
            catch (Exception e)
            {
                logSistema.escritura_archivo_string(e.Message);
                respuesta = e.Message;
            }
            finally
            {
                conBdd.cerrar_conexion_base();
            }
            return respuesta;
        }

        public List<AvanceObj> control_buscar_avance_by_usuario(UsuarioObj usuario)
        {
            conBdd.abrir_conexion_base();
            List<AvanceObj> avancesUsr = new List<AvanceObj>();
            try
            {
                avancesUsr = conBdd.buscar_evento_by_usuario(usuario);
            }
            catch (Exception e)
            {
                logSistema.escritura_archivo_string(e.Message);
            }
            finally
            {
                conBdd.cerrar_conexion_base();
            }
            return avancesUsr;
        }

        public List<AlarmasObj> control_buscar_alarma_atm(AlarmasObj alarma) {
            conBdd.abrir_conexion_base();
            List<AlarmasObj> alarmas = new List<AlarmasObj>();
            try
            {
                alarmas = conBdd.obtener_alarma(alarma);
            }
            catch (Exception e)
            {
                logSistema.escritura_archivo_string(e.Message);
            }
            finally
            {
                conBdd.cerrar_conexion_base();
            }
            return alarmas;
        }

        public List<BeanMenuPerfil> control_obtener_menu_perfil(PerfilObj perfil)
        {
            conBdd.abrir_conexion_base();
            List<BeanMenuPerfil> menusPerfiles = new List<BeanMenuPerfil>();
            try
            {
                menusPerfiles = conBdd.obtener_perfil_menu(perfil.id);
            }
            catch (Exception e)
            {
                logSistema.escritura_archivo_string(e.Message);
            }
            finally
            {
                conBdd.cerrar_conexion_base();
            }
            return menusPerfiles;
        }


        public UsuarioObj buscar_usuario_by_id(UsuarioObj user) {
            conBdd.abrir_conexion_base();
            UsuarioObj u;
            try
            {
                u = conBdd.obtener_usuario_id(user);
            }
            catch (ExRegistroNoExiste ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                u = new UsuarioObj();
                u.correo = ex.Message;
            }
            catch (ExpObtenerRegistro ex)
            {
                logSistema.escritura_archivo_string(ex.Message);
                u = new UsuarioObj();
                u.correo = ex.Message;
            }
            catch (Exception ex)
            {
                u = new UsuarioObj();
                u.correo = MensajeSistema.error_sistema;
                logSistema.escritura_archivo_string(MensajeSistema.error_sistema + ":" + ex.Message);
            }
            finally
            {
                conBdd.cerrar_conexion_base();
            }
            return u;

        }

    }
}
