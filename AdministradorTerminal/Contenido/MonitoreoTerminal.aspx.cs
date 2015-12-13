using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdministradorTerminal.WSControlador;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace AdministradorTerminal.Contenido
{
    public partial class MonitoreoTerminal : System.Web.UI.Page
    {
        private MonitoreoDispositivos[] terminalesUsuario;
        //private int filaSeleccionada;

        protected void Page_Load(object sender, EventArgs e)
        {
            // se cargan los terminales que tiene el usuario
            if (tb_terminales != null) {
                tb_terminales.Rows.Clear();
            }
            setAutoRefresh();
            HtmlTableRow fila = new HtmlTableRow();
            HtmlTableCell celdaNum = new HtmlTableCell();
            HtmlTableCell celdaTer = new HtmlTableCell();
            HtmlTableCell celdaGab = new HtmlTableCell();
            HtmlTableCell celdaLec = new HtmlTableCell();
            HtmlTableCell celdaImp = new HtmlTableCell();
            HtmlTableCell celdaMod = new HtmlTableCell();
            HtmlTableCell celdaEst = new HtmlTableCell();
            HtmlTableCell celdaLlave = new HtmlTableCell();
            HtmlTableCell celdaProceso = new HtmlTableCell();
            celdaNum.InnerText = "#";
            celdaTer.InnerText = "Terminal";
            celdaEst.InnerText = "Conexión";
            celdaGab.InnerText = "Gavetas";
            celdaGab.ColSpan = 6;
            celdaLlave.InnerText = "Llave";
            celdaLec.InnerText = "Lectora";
            celdaImp.InnerText = "Impresora";
            celdaMod.InnerText = "Modo";
            celdaProceso.InnerText = "Eventos";
            
            fila.Cells.Add(celdaNum);
            fila.Cells.Add(celdaTer);
            fila.Cells.Add(celdaEst);
            fila.Cells.Add(celdaGab);
            fila.Cells.Add(celdaLec);
            fila.Cells.Add(celdaImp);
            fila.Cells.Add(celdaMod);
            fila.Cells.Add(celdaLlave);
            fila.Cells.Add(celdaProceso);
            fila.BgColor = "4E4545";
            fila.Style.Value = "color: #FFFFFF";
            tb_terminales.Rows.Add(fila);

            HtmlTableRow filaE = new HtmlTableRow();

            HtmlTableCell celdaNume = new HtmlTableCell();
            HtmlTableCell celdaHora = new HtmlTableCell();
            HtmlTableCell celdaDato = new HtmlTableCell();
            HtmlTableCell celdaTipoError = new HtmlTableCell();
            HtmlTableCell celdaDescripcion = new HtmlTableCell();
            
            celdaNume.InnerText = "#";
            celdaHora.InnerText = "Hora hh:mm:ss";
            celdaDato.InnerText = "Mensaje Terminal";
            celdaTipoError.InnerText = "Tipo Error";
            celdaDescripcion.InnerText = "Descripcion Error";
            filaE.Cells.Add(celdaHora);
            filaE.Cells.Add(celdaDato);
            filaE.Cells.Add(celdaTipoError);
            filaE.Cells.Add(celdaDescripcion);
            filaE.BgColor = "4E4545";
            filaE.Style.Value = "color: #FFFFFF";
            tb_evento.Rows.Add(filaE);
            UsuarioObj usrSesion = (UsuarioObj)Session["usuario"];
            if (usrSesion != null)
            {
                terminalesUsuario = Globales.servicio.obtener_monitoreo_dipositivos(usrSesion);
                if (terminalesUsuario != null)
                {
                    cargar_datos_tabla(terminalesUsuario);
                    
                }
            }
        }


        private void setAutoRefresh(){

            int intMinutes = int.Parse(ConfigurationSettings.AppSettings["minutesRefresh"].ToString());
            string strTime = Convert.ToString((intMinutes * 60) * 1000);
            if (ConfigurationSettings.AppSettings["autoRefresh"].ToString().Trim().ToUpper().Equals("TRUE"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "refresh", "timedRefresh(" + strTime.Trim() + ");", true);
                
            }
        }

        private void cargar_datos_eventos() {
            HtmlTableRow filaE = new HtmlTableRow();
            
            HtmlTableCell celdaNum = new HtmlTableCell();
            HtmlTableCell celdaHora = new HtmlTableCell();
            HtmlTableCell celdaDato = new HtmlTableCell();
            HtmlTableCell celdaTipoError = new HtmlTableCell();
            HtmlTableCell celdaDescripcion = new HtmlTableCell();
            celdaNum.InnerText = "#";
            celdaHora.InnerText = "Hora hh:mm:ss";
            celdaDato.InnerText = "Mensaje Terminal";
            celdaTipoError.InnerText = "Tipo Error";
            celdaDescripcion.InnerText = "Descripcion Error";

        }

        private void cargar_datos_tabla(MonitoreoDispositivos[] terminales)
        {
            int i = 1;
            foreach (MonitoreoDispositivos terminal in terminales)
            {
                if (!terminal.tipo_estado.Equals("S")) {
                    continue;
                }
                HtmlTableRow fila = new HtmlTableRow();
                HtmlTableCell celdaNum = new HtmlTableCell();
                HtmlTableCell celdaTer = new HtmlTableCell();
                HtmlTableCell celdaGab1 = new HtmlTableCell();
                HtmlTableCell celdaGab2 = new HtmlTableCell();
                HtmlTableCell celdaGab3 = new HtmlTableCell();
                HtmlTableCell celdaGab4 = new HtmlTableCell();
                HtmlTableCell celdaGab5 = new HtmlTableCell();
                HtmlTableCell celdaRecha = new HtmlTableCell();
                HtmlTableCell celdaLec = new HtmlTableCell();
                HtmlTableCell celdaMod = new HtmlTableCell();
                HtmlTableCell celdaImp = new HtmlTableCell();
                HtmlTableCell celdaEst = new HtmlTableCell();
                HtmlTableCell celdaProceso = new HtmlTableCell();
                HtmlTableCell celdaLlave = new HtmlTableCell();
                celdaNum.InnerText = i + "";
                celdaTer.InnerText = terminal.codigo_atm;
                Image estadoConexion = new Image();
                estadoConexion.Width = 40;
                Image supervisor = new Image();
                Image llave = new Image();
                supervisor.Width = 40;
                llave.Width = 40;
                if (terminal.estado_conexio)
                {
                    
                    estadoConexion.ImageUrl = "~/Imagenes/connect_creating.png";
                    estadoConexion.ToolTip = "Terminal Conectado";
                    
                    if (terminal.modo_supervisor)
                    {
                        supervisor.ImageUrl = "~/Imagenes/estado_azul_elec.png";
                        supervisor.ToolTip = "Modo supervisor";
                    }
                    else
                    {
                        supervisor.ImageUrl = "~/Imagenes/estado_azul.png";
                        supervisor.ToolTip = "Modo normal";
                    }
                    if (terminal.llave_terminal)
                    {
                        llave.ImageUrl = "~/Imagenes/estado_rojo.png";
                        llave.ToolTip = "Revisar la llave maestra y terminal";
                    }
                    else
                    {
                        llave.ImageUrl = "~/Imagenes/estado_verde.png";
                        llave.ToolTip = "Configurcion correcta de llaves";
                    }

                }
                else{
                    estadoConexion.ImageUrl = "~/Imagenes/connect_no.png";
                    estadoConexion.ToolTip = "Terminal desconectado";
                    supervisor.ImageUrl = "~/Imagenes/connect_no.png";
                    supervisor.ToolTip = "No determinado";
                    llave.ImageUrl = "~/Imagenes/connect_no.png";
                    llave.ToolTip = "No determinado";
                    terminal.estado_gaveta1 = "10:No determinado";
                    terminal.estado_gaveta2 = "10:No determinado";
                    terminal.estado_gaveta3 = "10:No determinado";
                    terminal.estado_gaveta4 = "10:No determinado";
                    terminal.estado_gaveta5 = "10:No determinado";
                    //terminal.estado_lectora = 10; revisar no hay estado determinado de la letora
                    terminal.estado_impresora = "10:No determinado";
                }
                
                Image gaveta1 = obtener_imagen(terminal.estado_gaveta1, "Gaveta 1");
                Image gaveta2 = obtener_imagen(terminal.estado_gaveta2, "Gaveta 2");
                Image gaveta3 = obtener_imagen(terminal.estado_gaveta3, "Gaveta 3");
                Image gaveta4 = obtener_imagen(terminal.estado_gaveta4, "Gaveta 4");
                Image gaveta5 = obtener_imagen(terminal.estado_gaveta5, "Gaveta 5");
                Image gavetaR = obtener_imagen(terminal.estado_gaveta5, "Gaveta Rechazo");
                Image lectora = obtener_imagen(terminal.estado_gaveta5, "Lectora");
                Image impresora = obtener_imagen(terminal.estado_impresora, "Impresora");
                
                celdaEst.Controls.Add(estadoConexion);
                celdaGab1.Controls.Add(gaveta1);
                celdaGab2.Controls.Add(gaveta2);
                celdaGab3.Controls.Add(gaveta3);
                celdaGab4.Controls.Add(gaveta4);
                celdaGab5.Controls.Add(gaveta5);
                celdaRecha.Controls.Add(gavetaR);
                celdaLec.Controls.Add(lectora);
                celdaImp.Controls.Add(impresora);
                celdaMod.Controls.Add(supervisor);
                celdaLlave.Controls.Add(llave);

                Button btnEl = new Button();
                btnEl.Text = "Sucesos";
                btnEl.ToolTip = "Visualizar eventos del Terminal";
                btnEl.Click += new EventHandler(this.eventoBtnSucesos);
                celdaProceso.Align = "Center";
                btnEl.ID = "btnEl_" + i;
                celdaProceso.Controls.Add(btnEl);
                fila.Cells.Add(celdaNum);
                fila.Cells.Add(celdaTer);
                fila.Cells.Add(celdaEst);
                fila.Cells.Add(celdaGab1);
                fila.Cells.Add(celdaGab2);
                fila.Cells.Add(celdaGab3);
                fila.Cells.Add(celdaGab4);
                fila.Cells.Add(celdaGab5);
                fila.Cells.Add(celdaRecha);
                fila.Cells.Add(celdaLec);
                fila.Cells.Add(celdaImp);
                fila.Cells.Add(celdaMod);
                fila.Cells.Add(celdaLlave);
                fila.Cells.Add(celdaProceso);
                tb_terminales.Rows.Add(fila);
                i++;
            }
            Session["terminalSistemaUsr"] = terminales;
        }
        private void eventoBtnSucesos(object sender, EventArgs e)
        {
            string [] nombre = ((Button)sender).ID.Split('_');
            int posicion = int.Parse(nombre[1]) - 1;
            MonitoreoDispositivos tm = terminalesUsuario[posicion];
            this.lbl_codigoTerminal.Text = tm.codigo_atm;
            this.lbl_fechaEvento.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
            //Globales.servicio.obtener_alarma_atm();
        }


        private Image obtener_imagen(string valor, string dispostivo) {
            string[] datos = valor.Split(':');
            Image img = new Image();
            img.ToolTip = dispostivo + " " + datos[1];
            img.Width = 40;
            img.ImageUrl = "~/Imagenes/editclear.png";
            if (datos[0].Equals("0"))
            {
                img.ImageUrl = "~/Imagenes/estado_verde.png";
            } else if (datos[0].Equals("1"))
            {
                img.ImageUrl = "~/Imagenes/estado_marron.png";
            }
            else if (datos[0].Equals("2"))
            {
                img.ImageUrl = "~/Imagenes/estado_amarillo.png";
            }
            else if (datos[0].Equals("3"))
            {
                img.ImageUrl = "~/Imagenes/estado_rosado.png";
            }
            else if (datos[0].Equals("4"))
            {
               img.ImageUrl = "~/Imagenes/estado_rojo.png";
            }
            
            return img;

        }
    }
}
