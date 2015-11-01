using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ControlerAtm.com.ec.Interfaces;
using System.Data.SqlClient;
using ControlerAtm.com.ec.Excepciones;
using ControlerAtm.com.ec.objetos;
using System.Data;
using ControlerAtm.Utilitario;
namespace ControlerAtm.com.ec.BaseDatos
{
    public class BddSQLServer : BaseDatosDao
    {
        private string url = string.Empty;
        private SqlConnection conn; //Establecimiento de conexion a la base de datos.
        private LecturaEscrituraArchivo logs;
        public BddSQLServer(string url,string pathGuardar)
        {
            this.url = url;
            conn = new SqlConnection(this.url);
            logs = new LecturaEscrituraArchivo();
            logs.set_path_guardar(pathGuardar);
            logs.archivo_guardar("LOG_BDD");
        }

        public void set_archivo_path_guradar(string path) {
            logs.set_path_guardar(path);
        }

        #region Miembros de BaseDatosDao

        public void abrir_conexion_base()
        {
            try
            {
                conn.Open();

            }
            catch (SqlException e)
            {
                logs.escritura_archivo_string(e.Message);
                //logs.cerrar_archivo();
                throw new ExConexionBase(e.Message);
            }
        }

        public void cerrar_conexion_base()
        {
            try
            {
                conn.Close();
            }
            catch (SqlException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExConexionBase(ex.Message);
            }
        }

        public void cambiar_cadena_conexion(string urlNueva)
        {
            url = urlNueva;
            try
            {
                conn.Dispose();
                conn.Close();
                conn.ConnectionString = url;
            }
            catch (SqlException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExConexionBase(ex.Message);
            }
        }

        public void insertar_usuario(UsuarioObj usuario)
        {
            SqlCommand cmd = new SqlCommand("insertar_usuario_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre_usuario", usuario.nombre);
            cmd.Parameters.AddWithValue("@apellido_usuario", usuario.apellido);
            cmd.Parameters.AddWithValue("@cedula_usuario", usuario.cedula);
            cmd.Parameters.AddWithValue("@correo_usuario", usuario.correo);
            cmd.Parameters.AddWithValue("@telefono_usuario", usuario.telefono);
            cmd.Parameters.AddWithValue("@contrasenia_usuario", usuario.contrasenia);
            cmd.Parameters.AddWithValue("@numero_intentos", usuario.numero_intentos);
            cmd.Parameters.AddWithValue("@id_perfil", usuario.id_perfil);
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i == 0)
                {
                    throw new ExInsertarRegistro(MensajeSistema.ingreso_error + " Usuario");
                }
            }
            catch (ArgumentException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExInsertarRegistro(MensajeSistema.ingreso_error);
            }
            catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExConexionBase(MensajeSistema.error_Conexion);
            }
            catch (SqlException ex) {
                logs.escritura_archivo_string(ex.Message);
                if (ex.Message.Contains("IX_USUARIO_1")) {
                    throw new ExConexionBase("Documento ya ingresado");
                }
                throw new ExConexionBase(MensajeSistema.ingreso_error);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExConexionBase(MensajeSistema.ingreso_error);
            }
        }

        public UsuarioObj obtener_usuario(string cedula)
        {
            SqlCommand cmd = new SqlCommand("obtencion_usuario_por_documento_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@numero_documento", cedula);
            UsuarioObj usuario = new UsuarioObj();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("usuarioObj");
                da.Fill(tb);
                usuario.id = Convert.ToInt16(tb.Rows[0][0].ToString());
                usuario.nombre = tb.Rows[0][1].ToString();
                usuario.apellido = tb.Rows[0][2].ToString();
                usuario.cedula = tb.Rows[0][3].ToString();
                usuario.correo = tb.Rows[0][4].ToString();
                usuario.telefono = tb.Rows[0][5].ToString();
                usuario.estado = tb.Rows[0][6].ToString();
                usuario.contrasenia = tb.Rows[0][7].ToString();
                usuario.id_perfil = int.Parse(tb.Rows[0][8].ToString());
                usuario.cambio_contrasenia = bool.Parse(tb.Rows[0][9].ToString());
                usuario.numero_intentos = int.Parse(tb.Rows[0][10].ToString());
                usuario.estado = tb.Rows[0][11].ToString();
                return usuario;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new Exception(MensajeSistema.reg_no_existe);
            }
        }

        public UsuarioObj obtener_usuario_id(UsuarioObj usr)
        {
            SqlCommand cmd = new SqlCommand("obtencion_usuario_por_id_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", usr.id);
            UsuarioObj usuario = new UsuarioObj();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("usuarioObj");
                da.Fill(tb);
                usuario.id = Convert.ToInt16(tb.Rows[0][0].ToString());
                usuario.nombre = tb.Rows[0][1].ToString();
                usuario.apellido = tb.Rows[0][2].ToString();
                usuario.cedula = tb.Rows[0][3].ToString();
                usuario.correo = tb.Rows[0][4].ToString();
                usuario.telefono = tb.Rows[0][5].ToString();
                usuario.estado = tb.Rows[0][6].ToString();
                usuario.contrasenia = tb.Rows[0][7].ToString();
                usuario.id_perfil = int.Parse(tb.Rows[0][8].ToString());
                usuario.cambio_contrasenia = bool.Parse(tb.Rows[0][9].ToString());
                usuario.numero_intentos = int.Parse(tb.Rows[0][10].ToString());
                usuario.estado = tb.Rows[0][11].ToString();
                return usuario;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new Exception(MensajeSistema.reg_no_existe);
            }
        }


        public void insertar_perfil(PerfilObj perfil)
        {
            SqlCommand cmd = new SqlCommand("insertar_perfil_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre_perfil", perfil.nombre);
            cmd.Parameters.AddWithValue("@descrpcion_perfil", perfil.descripcion);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (ArgumentException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExInsertarRegistro(MensajeSistema.ingreso_error);
            }
            catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExConexionBase(MensajeSistema.error_Conexion);
            }
        }

        public List<MenuObj> obtener_menu(UsuarioObj usuario)
        {
            SqlCommand cmd;

                cmd = new SqlCommand("obtener_menu_perfil_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo_perfil", usuario.id_perfil);
                List<MenuObj> lista = new List<MenuObj>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("lsMenu");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    MenuObj menu = new MenuObj();
                    menu.id_menu = int.Parse(tb.Rows[i][0].ToString());
                    menu.nombre = tb.Rows[i][1].ToString().ToUpper();
                    menu.descripcion = tb.Rows[i][2].ToString();
                    menu.url = tb.Rows[i][3].ToString();
                    menu.codigo_menu_padre = int.Parse(tb.Rows[i][4].ToString());
                    menu.estado = bool.Parse(tb.Rows[i][5].ToString());
                    lista.Add(menu);
                }
                return lista;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new Exception(MensajeSistema.error_Conexion);
            }
        }

        #endregion

        #region Miembros de BaseDatosDao


        public void cambiar_cadena_conexion()
        {
            throw new NotImplementedException();
        }

        public void insertar_modelo(ModeloObj modelo)
        {
            SqlCommand cmd = new SqlCommand("insertar_modelo_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre_modelo", modelo.nombre);
            cmd.Parameters.AddWithValue("@fabricante_modelo", modelo.fabricante);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (ArgumentException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExInsertarRegistro(MensajeSistema.reg_no_existe);
            }
            catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExConexionBase(MensajeSistema.error_Conexion);
            }
        }

        public void insertar_alarmas(AlarmasObj alarmas)
        {
            SqlCommand cmd = new SqlCommand("insertar_alarmas_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@mensaje_alarma", alarmas.mensaje);
            //cmd.Parameters.AddWithValue("@fecha_registro", alarmas.fecha_registro);
            cmd.Parameters.AddWithValue("@id_atm", alarmas.id_atm);
            cmd.Parameters.AddWithValue("@envioRecepcion", alarmas.envio_recepcion);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (ArgumentException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExInsertarRegistro(MensajeSistema.reg_no_existe);
            }
            catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExConexionBase(MensajeSistema.error_Conexion);
            }
        }

        public void insertar_atm(AtmObj atm)
        {
            SqlCommand cmd = new SqlCommand("insertar_atm_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ip_atm", atm.ip);
            cmd.Parameters.AddWithValue("@codigo_atm", atm.codigo);
            cmd.Parameters.AddWithValue("@ubicacion_atm", atm.ubicacion);
            cmd.Parameters.AddWithValue("@estado_atm", atm.estado);
            cmd.Parameters.AddWithValue("@id_modelo", atm.id_modelo);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (ArgumentException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExInsertarRegistro(MensajeSistema.reg_no_existe);
            }
            catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExConexionBase(MensajeSistema.error_Conexion);
            }
            catch (SqlException ex) {
                logs.escritura_archivo_string(ex.Message);
                throw new ExInsertarRegistro(ex.Message);
            }
        }

        public void insertar_suceso(SucesoObj suceso)
        {
            SqlCommand cmd = new SqlCommand("insertar_suceso_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@descripcion_suceso", suceso.descripcion);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (ArgumentException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExInsertarRegistro(MensajeSistema.ingreso_error);
            }
            catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExConexionBase(MensajeSistema.error_Conexion);
            }
        }

        public void insertar_menu(MenuObj menu)
        {
            SqlCommand cmd = new SqlCommand("insertar_menu_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre_menu", menu.nombre);
            cmd.Parameters.AddWithValue("@descripcion_menu", menu.descripcion);
            cmd.Parameters.AddWithValue("@url_menu", menu.url);
            cmd.Parameters.AddWithValue("@codigo_menu_padre", menu.codigo_menu_padre);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (ArgumentException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExInsertarRegistro(MensajeSistema.ingreso_error);
            }
            catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExConexionBase(MensajeSistema.error_Conexion);
            }
        }

        public void insertar_responsable_atm(AtmObj atm, UsuarioObj usuario)
        {
            SqlCommand cmd = new SqlCommand("insertar_responsable_atm_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id_usuario", usuario.id);
            cmd.Parameters.AddWithValue("@id_atm", atm.id_atm);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (ArgumentException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExInsertarRegistro(MensajeSistema.ingreso_error);
            }
            catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExConexionBase(MensajeSistema.error_Conexion);
            }
        }

        public void insertar_menu_perfil(MenuObj menu, PerfilObj perfil)
        {
            SqlCommand cmd = new SqlCommand("insertar_menu_perfil_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id_menu", menu.id_menu);
            cmd.Parameters.AddWithValue("@id_perfil", perfil.id);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (ArgumentException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExInsertarRegistro(MensajeSistema.ingreso_error);
            }
            catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExConexionBase(MensajeSistema.error_Conexion);
            }
        }

        public void insertar_log_suceso(SucesoObj suceso, UsuarioObj usuario)
        {
            SqlCommand cmd = new SqlCommand("insertar_log_suceso_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id_suceso", suceso.id_suceso);
            cmd.Parameters.AddWithValue("@id_usuario", usuario.id);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (ArgumentException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExInsertarRegistro(MensajeSistema.ingreso_error);
            }
            catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExConexionBase(MensajeSistema.ingreso_error);
            }
        }

        #endregion

        #region Miembros de BaseDatosDao


        public List<UsuarioObj> obtener_usuario_por(string datoEntrada, bool tipo_busqueda)
        {
            SqlCommand cmd;
            if (tipo_busqueda)// True es por documento
            {
                cmd = new SqlCommand("obtencion_usuario_por_documento_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@numero_documento", datoEntrada);
            }
            else {// false es por nick
                string[] datosUsuario = datoEntrada.Split(' ');
                cmd = new SqlCommand("obtencion_usuario_por_nombre_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                if (datosUsuario.Length == 2)
                {
                    cmd.Parameters.AddWithValue("@nombre", datosUsuario[0]);
                    cmd.Parameters.AddWithValue("@apellido", datosUsuario[1]);
                }
                else if (datosUsuario.Length == 1)
                {
                    cmd.Parameters.AddWithValue("@nombre", datosUsuario[0]);
                    cmd.Parameters.AddWithValue("@apellido", "");
                }
                else if(datosUsuario.Length > 2 ){
                    cmd.Parameters.AddWithValue("@nombre", datosUsuario[0]);
                    cmd.Parameters.AddWithValue("@apellido", datosUsuario[datosUsuario.Length-1]);
                }
            }
            List<UsuarioObj> usuarios = new List<UsuarioObj>();
            try
            {
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("usuarioObj");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    UsuarioObj usuario = new UsuarioObj();
                    usuario.id = Convert.ToInt16(tb.Rows[i][0].ToString());
                    usuario.nombre = tb.Rows[i][1].ToString();
                    usuario.apellido = tb.Rows[i][2].ToString();
                    usuario.cedula = tb.Rows[i][3].ToString();
                    usuario.correo = tb.Rows[i][4].ToString();
                    usuario.telefono = tb.Rows[i][5].ToString();
                    usuario.estado = tb.Rows[i][6].ToString();
                    usuario.contrasenia = tb.Rows[i][7].ToString();
                    usuario.id_perfil = int.Parse(tb.Rows[i][8].ToString());
                    usuario.cambio_contrasenia = bool.Parse(tb.Rows[i][9].ToString());
                    usuarios.Add(usuario);
                }
                return usuarios;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new Exception(MensajeSistema.error_Conexion);
            }
        }

        #endregion

        #region Miembros de BaseDatosDao


        public List<PerfilObj> obtener_perfil(bool estado)
        {
            SqlCommand cmd = new SqlCommand("obtener_perfil_usuario_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@estado_perfil", estado);
                List<PerfilObj> perfiles = new List<PerfilObj>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("PerfilObj");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    PerfilObj perfil = new PerfilObj();
                    perfil.id = int.Parse(tb.Rows[i][0].ToString());
                    perfil.nombre = tb.Rows[i][1].ToString().ToUpper();
                    perfil.descripcion = tb.Rows[i][2].ToString();
                    perfiles.Add(perfil);
                }
                return perfiles;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExpObtenerRegistro(MensajeSistema.ingreso_error);
            }
            catch (ArgumentNullException ex)
            {

                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExpObtenerRegistro(MensajeSistema.ingreso_error);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new Exception(MensajeSistema.error_Conexion);
            }

        }

        #endregion

        #region Miembros de BaseDatosDao


        public void actualizar_usuario(UsuarioObj usuario)
        {
            SqlCommand cmd = new SqlCommand("actualizar_usuario_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id_usuario", usuario.id);
            cmd.Parameters.AddWithValue("@nombre_usuario", usuario.nombre);
            cmd.Parameters.AddWithValue("@apellido_usuario", usuario.apellido);
            cmd.Parameters.AddWithValue("@cedula_usuario", usuario.cedula);
            cmd.Parameters.AddWithValue("@correo_usuario", usuario.correo);
            cmd.Parameters.AddWithValue("@telefono_usuario", usuario.telefono);
            cmd.Parameters.AddWithValue("@constrasenia_usuario", usuario.contrasenia);
            cmd.Parameters.AddWithValue("@estado_usuario", usuario.estado);
            cmd.Parameters.AddWithValue("@numero_intentos", usuario.numero_intentos);
            cmd.Parameters.AddWithValue("@id_perfil", usuario.id_perfil);
            cmd.Parameters.AddWithValue("@cambio_contraseña", usuario.cambio_contrasenia);
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i == 0) {
                    throw new ExActualizarRegistro(MensajeSistema.actualizacion_error + " Usuario");
                }
            }catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExConexionBase(MensajeSistema.error_Conexion);
            }
            catch (SqlException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                if (ex.Message.Contains("IX_USUARIO_1"))
                {
                    throw new ExConexionBase("Documento ya ingresado");
                }
                throw new ExConexionBase(MensajeSistema.ingreso_error);
            }
        }

        #endregion

        #region Miembros de BaseDatosDao


        public ParametroObj obtenerParametro(int id_parametro)
        {
            SqlCommand cmd = new SqlCommand("obtener_parametro_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id_parametro", id_parametro);
            ParametroObj parametro = new ParametroObj();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("ParametroObj");
                da.Fill(tb);
                parametro.id_parametro = int.Parse(tb.Rows[0][0].ToString());
                parametro.valor = tb.Rows[0][1].ToString();
                parametro.descripcion = tb.Rows[0][2].ToString();
                parametro.estado = (tb.Rows[0][2].ToString()=="1");
                return parametro;
                
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExpObtenerRegistro(MensajeSistema.ingreso_error);
            }
            catch (ArgumentNullException ex)
            {

                logs.escritura_archivo_string(ex.Message);
                throw new ExpObtenerRegistro(MensajeSistema.ingreso_error);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new Exception(MensajeSistema.error_Conexion);
            }

        }

        #endregion

        #region Miembros de BaseDatosDao


        public void actualizar_perfil(PerfilObj obj, List<MenuObj> menus)
        {
            SqlCommand cmd = new SqlCommand("actualizar_perfil_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre", obj.nombre);
            cmd.Parameters.AddWithValue("@id_perfil", obj.id);
            cmd.Parameters.AddWithValue("@descripcion", obj.descripcion);

            try
            {
                if (cmd.ExecuteNonQuery() == 0)
                {
                    throw new ExInsertarRegistro(MensajeSistema.ingreso_ok);
                }
                
                foreach (MenuObj menu in menus)
                {
                    if (menu != null)
                    {
                        SqlCommand cmdUP = new SqlCommand("insertar_menu_perfil_sp", conn);
                        cmdUP.CommandType = CommandType.StoredProcedure;
                        cmdUP.Parameters.AddWithValue("@id_menu", menu.id_menu);
                        cmdUP.Parameters.AddWithValue("@id_perfil", obj.id);
                        cmdUP.ExecuteNonQuery();
                    }
                }
            }
            catch (ArgumentException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExInsertarRegistro(MensajeSistema.ingreso_error);
                // CONSULTAR COMO HACER UN ROLL BACK
            }
            catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExConexionBase(MensajeSistema.error_Conexion);
            }
            catch (NullReferenceException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExInsertarRegistro(ex.Message);
            }    
        }

        public void insertar_perfil(PerfilObj obj, List<MenuObj> menus)
        {
            SqlCommand cmd = new SqlCommand("insertar_perfil_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre_perfil", obj.nombre);
            cmd.Parameters.AddWithValue("@descripcion_perfil", obj.descripcion);

            try
            {
                if (cmd.ExecuteNonQuery() == 0)
                {
                    throw new ExInsertarRegistro(MensajeSistema.ingreso_ok);
                }
                obj.id = obtener_clave_tabla("PERFIL", "id_perfil");

                foreach (MenuObj menu in menus)
                {
                    if (menu != null)
                    {
                        SqlCommand cmdUP = new SqlCommand("insertar_menu_perfil_sp", conn);
                        cmdUP.CommandType = CommandType.StoredProcedure;
                        cmdUP.Parameters.AddWithValue("@id_menu", menu.id_menu);
                        cmdUP.Parameters.AddWithValue("@id_perfil", obj.id);
                        cmdUP.ExecuteNonQuery();
                    }
                }
            }
            catch (ArgumentException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExInsertarRegistro(MensajeSistema.ingreso_error);
                // CONSULTAR COMO HACER UN ROLL BACK
            }
            catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExConexionBase(MensajeSistema.error_Conexion);
            }
            catch (NullReferenceException ex) {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExInsertarRegistro(ex.Message);
            }            
        }

        #endregion

        #region Miembros de BaseDatosDao


        public int obtener_clave_tabla(string nombreTabla, string nombreColumna)
        {
            SqlCommand cmd = new SqlCommand("select MAX("+nombreColumna+") from "+nombreTabla, conn);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("reporte");
                da.Fill(tb);
                return int.Parse(tb.Rows[0][0].ToString());
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExRegistroNoExiste("Identificador no encontrado");
            }
            catch (Exception ex) {
                logs.escritura_archivo_string(ex.Message);
                throw new ExConexionBase(MensajeSistema.error_sistema);
            }
        }

        #endregion

        #region Miembros de BaseDatosDao


        public int borrar_perfil_usuario(int id_perfil)
        {
            SqlCommand cmd = new SqlCommand("delete MENU_PERFIL where id_perfil = "+id_perfil, conn);
            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExRegistroNoExiste("Identificador no encontrado");
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExConexionBase(MensajeSistema.error_sistema);
            }

        }

        #endregion

        #region Miembros de BaseDatosDao


        public List<MenuObj> obtener_menu_estado(bool estado)
        {
            SqlCommand cmd;
            int estadoM = 0;
            if (estado) {
                estadoM = 1;
            }
            cmd = new SqlCommand("select * "+
                "from Menu m where m.estado_menu = "+estadoM, conn);
            List<MenuObj> lista = new List<MenuObj>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("lsMenu");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    MenuObj menu = new MenuObj();
                    menu.id_menu = int.Parse(tb.Rows[i][0].ToString());
                    menu.nombre = tb.Rows[i][1].ToString().ToUpper();
                    menu.descripcion = tb.Rows[i][2].ToString();
                    menu.url = tb.Rows[i][3].ToString();
                    menu.codigo_menu_padre = int.Parse(tb.Rows[i][4].ToString());
                    menu.estado = bool.Parse(tb.Rows[i][5].ToString());
                    lista.Add(menu);
                }
                return lista;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new Exception(MensajeSistema.error_Conexion);
            }
        }

        #endregion

        #region Miembros de BaseDatosDao


        public List<MenuObj> obtener_menu_padre()
        {
            SqlCommand cmd;
            cmd = new SqlCommand("select * " +
                "where m.codigo_menu_padre = 0 ", conn);
            List<MenuObj> lista = new List<MenuObj>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("lsMenu");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    MenuObj menu = new MenuObj();
                    menu.id_menu = int.Parse(tb.Rows[i][0].ToString());
                    menu.nombre = tb.Rows[i][1].ToString().ToUpper();
                    menu.descripcion = tb.Rows[i][2].ToString();
                    menu.url = tb.Rows[i][3].ToString();
                    menu.codigo_menu_padre = int.Parse(tb.Rows[i][4].ToString());
                    menu.estado = bool.Parse(tb.Rows[i][5].ToString());
                    lista.Add(menu);
                }
                return lista;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new Exception(MensajeSistema.error_Conexion);
            }

        }

        public List<MenuObj> obtener_menu_hijo(int id_menu_padre)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("select * from MENU m" +
                " where m.codigo_menu_padre = " + id_menu_padre +
                " and m.estado_menu = 1", conn);
            List<MenuObj> lista = new List<MenuObj>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("lsMenu");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    MenuObj menu = new MenuObj();
                    menu.id_menu = int.Parse(tb.Rows[i][0].ToString());
                    menu.nombre = tb.Rows[i][1].ToString().ToUpper();
                    menu.descripcion = tb.Rows[i][2].ToString();
                    menu.url = tb.Rows[i][3].ToString();
                    menu.codigo_menu_padre = int.Parse(tb.Rows[i][4].ToString());
                    menu.estado = bool.Parse(tb.Rows[i][5].ToString());
                    lista.Add(menu);
                }
                return lista;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new Exception(MensajeSistema.error_Conexion);
            }

        }

        #endregion

        #region Miembros de BaseDatosDao


        public List<ModeloObj> obtener_modelo_terminal(bool estado)
        {
            SqlCommand cmd;
            int estadoM = 0;
            if (estado)
            {
                estadoM = 1;
            }
            cmd = new SqlCommand("obtener_modelo_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@estado", estadoM);
            List<ModeloObj> lista = new List<ModeloObj>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("lsMenu");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    ModeloObj modelo = new ModeloObj();
                    modelo.id_modelo = int.Parse(tb.Rows[i][0].ToString());
                    modelo.nombre = tb.Rows[i][1].ToString();
                    modelo.fabricante = tb.Rows[i][2].ToString();
                    lista.Add(modelo);
                }
                return lista;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new Exception(MensajeSistema.error_Conexion);
            }
        }

        #endregion

        #region Miembros de BaseDatosDao


        public void actualizar_terminal(AtmObj terminal)
        {
            SqlCommand cmd = new SqlCommand("actualizar_terminal_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id_atm", terminal.id_atm);
            cmd.Parameters.AddWithValue("@ip", terminal.ip);
            cmd.Parameters.AddWithValue("@codigo", terminal.codigo);
            cmd.Parameters.AddWithValue("@ubicacion", terminal.ubicacion);
            cmd.Parameters.AddWithValue("@estado", terminal.estado);
            cmd.Parameters.AddWithValue("@modelo", terminal.id_modelo);
            cmd.Parameters.AddWithValue("@estado_conexion", terminal.conexion);
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i == 0)
                {
                    throw new ExActualizarRegistro(MensajeSistema.actualizacion_error + " Terminal");
                }
            }
            catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExConexionBase(MensajeSistema.error_Conexion);
            }
        }

        #endregion

        #region Miembros de BaseDatosDao


        public List <AtmObj> obtener_terminal(string busqueda, bool tipo_busqueda)
        {
            SqlCommand cmd = null;
            string[] tipoOpcion = busqueda.Split(':');
            int valorbusqueda = 0;
            if (tipoOpcion.Length > 1) {
                busqueda = tipoOpcion[0];
                valorbusqueda = Int16.Parse(tipoOpcion[1]);
            }
            if (tipo_busqueda)
            { //Cuando es true busca por codigo terminal
                cmd = new SqlCommand("obtener_terminal_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo_terminal", busqueda);
                cmd.Parameters.AddWithValue("@busqueda_por", valorbusqueda);
            }
            else {
                cmd = new SqlCommand("obtener_terminal_by_Ip_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ip_atm", busqueda);
            }
            List <AtmObj> terminales = new List<AtmObj>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("TerminalObj");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    AtmObj terminal = new AtmObj();
                    terminal.id_atm = Convert.ToInt16(tb.Rows[i][0].ToString());
                    terminal.ip = tb.Rows[i][1].ToString();
                    terminal.codigo = tb.Rows[i][2].ToString();
                    terminal.ubicacion = tb.Rows[i][3].ToString();
                    terminal.estado = tb.Rows[i][4].ToString();
                    terminal.id_modelo = int.Parse(tb.Rows[i][5].ToString());
                    terminal.conexion = bool.Parse(tb.Rows[i][6].ToString());
                    terminales.Add(terminal);
                } return terminales;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new Exception(MensajeSistema.reg_no_existe);
            }
        }

        #endregion



        #region Miembros de BaseDatosDao


        public List<AtmObj> obtener_terminalByUsuario(UsuarioObj usuario)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand("obtener_terminal_by_usuario_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo_usuario", usuario.id);
            List<AtmObj> terminales = new List<AtmObj>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("TerminalObj");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    AtmObj terminal = new AtmObj();
                    terminal.id_atm = Convert.ToInt16(tb.Rows[i][0].ToString());
                    terminal.ip = tb.Rows[i][1].ToString();
                    terminal.codigo = tb.Rows[i][2].ToString();
                    terminal.ubicacion = tb.Rows[i][3].ToString();
                    terminal.estado = tb.Rows[i][4].ToString();
                    terminal.id_modelo = int.Parse(tb.Rows[i][5].ToString());
                    terminales.Add(terminal);
                } return terminales;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new Exception(MensajeSistema.reg_no_existe);
            }

        }

        #endregion

        #region Miembros de BaseDatosDao


        public List<AtmObj> obtener_terminalByUsuario_NoAsignados(UsuarioObj usuario)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand("obtener_terminal_by_usuario_NoAsignados_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codigo_usuario", usuario.id);
            List<AtmObj> terminales = new List<AtmObj>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("TerminalObj");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    AtmObj terminal = new AtmObj();
                    terminal.id_atm = Convert.ToInt16(tb.Rows[i][0].ToString());
                    terminal.ip = tb.Rows[i][1].ToString();
                    terminal.codigo = tb.Rows[i][2].ToString();
                    terminal.ubicacion = tb.Rows[i][3].ToString();
                    terminal.estado = tb.Rows[i][4].ToString();
                    terminal.id_modelo = int.Parse(tb.Rows[i][5].ToString());
                    terminales.Add(terminal);
                } return terminales;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new Exception(MensajeSistema.reg_no_existe);
            }

        }

        #endregion

        #region Miembros de BaseDatosDao


        public string guardar_terminales_asignados(List<AtmObj> terminales, UsuarioObj responsable)
        {
            
            foreach(AtmObj terminal in terminales){
                SqlCommand cmd = new SqlCommand("insertar_responsable_atm_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_usuario", responsable.id);
                cmd.Parameters.AddWithValue("@id_atm", terminal.id_atm);
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i == 0)
                {
                    throw new ExInsertarRegistro(MensajeSistema.ingreso_error + " Usuario");
                }
            }
            catch (ArgumentException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExInsertarRegistro(MensajeSistema.ingreso_error);
            }
            catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExConexionBase(MensajeSistema.error_Conexion);
            }
            catch (SqlException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                if (ex.Message.Contains("IX_RESPONSABLE_ATM"))
                {
                    throw new ExConexionBase("ya se encuentra asignado a terminal");
                }
                throw new ExConexionBase(MensajeSistema.ingreso_error);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExConexionBase(MensajeSistema.ingreso_error);
            }
            }
            return "Datos guardados con exito";
        }

        #endregion

        #region Miembros de BaseDatosDao


        public string borrar_datos_terminales_usuario(UsuarioObj responsable)
        {
            SqlCommand cmd = new SqlCommand("borrar_terminal_responsable_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id_responsable", responsable.id);
            
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (ArgumentException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExInsertarRegistro(MensajeSistema.reg_no_existe);
            }
            catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExConexionBase(MensajeSistema.error_Conexion);
            }
            catch (SqlException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExInsertarRegistro(ex.Message);
            }
            return "000";
        }

        #endregion

        #region Miembros de BaseDatosDao


        public List<AvanceObj> buscar_evento_by_usuario(UsuarioObj usuario)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand("buscar_avance_by_usuario_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id_usuario", usuario.id);
            List<AvanceObj> avances = new List<AvanceObj>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("AvanceObj");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    AvanceObj avance = new AvanceObj();
                    avance.id_avance = Convert.ToInt16(tb.Rows[i][0].ToString());
                    if(tb.Rows[i][1] != null){
                        avance.fecha_atencion = tb.Rows[i][1].ToString();
                    }
                    avance.notificacion = bool.Parse(tb.Rows[i][2].ToString());
                    avance.atendido = bool.Parse(tb.Rows[i][3].ToString());
                    avance.observacion = tb.Rows[i][4].ToString();
                    avance.id_alarma = Convert.ToInt16(tb.Rows[i][5].ToString());
                    try
                    {
                        avance.usuario_atiende = Convert.ToInt16(tb.Rows[i][6].ToString());
                    }catch(FormatException e){
                        string msa = e.Message;
                        avance.usuario_atiende = 0;
                    }
                    avance.usuario_notifica = Convert.ToInt16(tb.Rows[i][7].ToString());
                    avance.fecha_registro = tb.Rows[i][8].ToString();
                    avances.Add(avance);
                } 
                return avances;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string(ex.Message + "\n" + ex.StackTrace);
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string(ex.Message + "\n" + ex.StackTrace);
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string(ex.Message+"\n"+ex.StackTrace);
                throw new Exception(MensajeSistema.reg_no_existe);
            }            
        }

        public string actualizar_evento_by_usuario(UsuarioObj usuarioCambia, AvanceObj evento)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand("actualizar_avance_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@observacion", evento.observacion);
            cmd.Parameters.AddWithValue("@id_avance", evento.id_avance);
            cmd.Parameters.AddWithValue("@id_usuario_atiende", usuarioCambia.id);
            string mensaje = "Actualizacion correcta de evento";
            try
            {
                int filas = cmd.ExecuteNonQuery();
                if(filas==0){
                    mensaje = "No se actualizo ningun registro";
                }
                return mensaje;
            }
            catch (ArgumentException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExInsertarRegistro(MensajeSistema.reg_no_existe);
            }
            catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExConexionBase(MensajeSistema.error_Conexion);
            }
            catch (SqlException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExInsertarRegistro(ex.Message);
            }
        }

        #endregion

        #region Miembros de BaseDatosDao


        public string insertar_avance_terminal(AvanceObj evento)
        {
            SqlCommand cmd = new SqlCommand("insertar_avance_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@notificacion", evento.notificacion);
            cmd.Parameters.AddWithValue("@id_alarma", evento.id_alarma);
            cmd.Parameters.AddWithValue("@id_usuario_notifica", evento.usuario_notifica);
            string mensaje = "Ingreso exitoso";
            try
            {
                int fila = cmd.ExecuteNonQuery();
                if(fila == 0){
                mensaje = "No se inserto ningun registro";
                }
                return mensaje;
            }
            catch (ArgumentException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExInsertarRegistro(MensajeSistema.reg_no_existe);
            }
            catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExConexionBase(MensajeSistema.error_Conexion);
            }
        }

        #endregion

        #region Miembros de BaseDatosDao


        public List<AlarmasObj> obtener_alarma(AlarmasObj alarma)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("obtener_alarma_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id_alarma", alarma.id_alarma);
            cmd.Parameters.AddWithValue("@id_atm", alarma.id_atm);
            List<AlarmasObj> lista = new List<AlarmasObj>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("lsAlarma");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    AlarmasObj alarmatmp = new AlarmasObj();
                    alarmatmp.id_alarma = int.Parse(tb.Rows[i][0].ToString());
                    alarmatmp.mensaje = tb.Rows[i][1].ToString();
                    alarmatmp.id_atm = int.Parse(tb.Rows[i][2].ToString());
                    alarmatmp.fecha_registro = DateTime.Parse(tb.Rows[i][3].ToString());
                    alarmatmp.envio_recepcion = int.Parse(tb.Rows[i][4].ToString());
                    lista.Add(alarmatmp);
                }
                return lista;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new ExpObtenerRegistro(MensajeSistema.reg_no_existe);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string(ex.Message);
                throw new Exception(MensajeSistema.error_Conexion);
            }

        }

        #endregion

    }
}
