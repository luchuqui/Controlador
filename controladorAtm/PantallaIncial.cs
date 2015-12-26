using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ControlerAtm.com.ec.BaseDatos;
using ControlerAtm.com.ec.objetos;
using System.Collections;

namespace controladorAtm
{
    public partial class PantallaIncial : Form
    {
        public Thread hiloPrincipal;
        private BddSQLServer conBdd;
        private ServidorEscucha serv;
        private archivoRW configuracion;
        private archivoRW aux;
        private ConfiguracionServicio servicio;
        private ArrayList  terminales;
        private string down = "SIN CONEXION";
        public PantallaIncial()
        {
            configuracion = new archivoRW();
            aux = new archivoRW();
            configuracion.archivo_abrir("Config\\configServicio.xml");
            aux.archivo_abrir("");
            servicio = configuracion.obtenerDatosXml()[0];
            
            InitializeComponent();
            conBdd = new BddSQLServer(servicio.conexion,aux.get_path_abrir());
            try
            {
                conBdd.abrir_conexion_base();
                cargar_terminales();
                CheckForIllegalCrossThreadCalls = false;
                this.imagenProceso.Image = Properties.Resources.Error;
                ConfiguracionServicio miConfiguracion = new ConfiguracionServicio();
                miConfiguracion.ip = servicio.ip;
                miConfiguracion.puerto = servicio.puerto;
                miConfiguracion.conexion = servicio.conexion;
                miConfiguracion.pathLogServicio = aux.get_path_abrir();
                serv = new ServidorEscucha(miConfiguracion, txbx_visor_evento, terminales, dataGridMonitorDispositivos, conBdd);
                hiloPrincipal = new Thread((ThreadStart)serv.aceptar_conexion);
            }
            catch (Exception e) {
                txbx_visor_evento.SelectionColor = Color.Red;
                txbx_visor_evento.AppendText(e.Message);
                txbx_visor_evento.AppendText("\nRevise su configuración");
                btn_iniciar.Enabled = false;
                btn_parar.Enabled = false;
            }
        }

        public void cargar_terminales() {
            
            UsuarioObj u = new UsuarioObj();
            terminales = new ArrayList();
            u.id = 0;
            //conBdd.abrir_conexion_base();
            dataGridMonitorDispositivos.Rows.Clear();
            List<AtmObj> atms = conBdd.obtener_terminalByUsuario_NoAsignados(u);
            int i = 0;
            
            foreach (AtmObj atm in atms)
            {
                //TerminalAtm ter = new TerminalAtm(atm.ip, atm.ubicacion, atm.codigo);
                //ter.id_terminal  = atm.id_atm;
                //terminales.Add(ter);
                terminales.Add(atm);
                string[] str = { atm.codigo + " - " + atm.ip, down };
                dataGridMonitorDispositivos.Rows.Add(str);
                //dataGridMonitorDispositivos.Rows[i].Cells[0].Value = atm.ip;
                cambio_color(i, 1, Color.Red);
                i++;
            }
            //conBdd.cerrar_conexion_base();
        }

        public string relleno_caracteres(string datos,int longitud) { 
        string spacios = "";
        for (int i = datos.Length; i < longitud; i++)
        {
            spacios += " ";
        }
        datos = spacios +datos;
        return datos;
        }

        public void cambio_color(int fila, int columna, Color tmp)
        {
            dataGridMonitorDispositivos.Rows[fila].Cells[columna].Style.ForeColor = tmp;
        }
        public void iniciar_servicio()
        {
            
            //ServidorEscuchaUno serv = new ServidorEscuchaUno(miConfiguracion,txbx_visor_evento);
            
            //var prueba = new Thread((ThreadStart)serv.inicio_escucha);
            if (!hiloPrincipal.IsAlive)
            {
                hiloPrincipal = new Thread((ThreadStart)serv.aceptar_conexion);
                serv.set_visor(this.txbx_visor_evento);
                hiloPrincipal.Start();
                this.imagenProceso.Image = Properties.Resources.Animation;
            }
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            detener_servicio();
        }

        private void detener_servicio() {
            if (hiloPrincipal != null)
            {
                if (hiloPrincipal.IsAlive)
                {
                    serv.detener();
                    hiloPrincipal.Abort();
                    this.imagenProceso.Image = Properties.Resources.Error;
                    cargar_terminales();
                }
            }
        }

        private void btn_iniciar_Click(object sender, EventArgs e)
        {
            iniciar_servicio();
        }

        private void btn_parar_Click(object sender, EventArgs e)
        {
            detener_servicio();
        }
    }

 
}
