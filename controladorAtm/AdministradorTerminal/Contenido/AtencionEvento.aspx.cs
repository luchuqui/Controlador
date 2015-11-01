using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdministradorTerminal.WSControlador;
using System.Web.UI.HtmlControls;

namespace AdministradorTerminal.Contenido
{
    public partial class AtencionEvento : System.Web.UI.Page
    {
        private int filaSeleccionada;
        protected void Page_Load(object sender, EventArgs e)
        {
            cargar_cabecera_datos();
        }

        public void cargar_cabecera_datos() {
            if (tb_evento != null)
            {
                tb_evento.Rows.Clear();
            }

            HtmlTableRow fila = new HtmlTableRow();
            HtmlTableCell celdaNum = new HtmlTableCell();
            HtmlTableCell celdaEven = new HtmlTableCell();
            HtmlTableCell celdaTer = new HtmlTableCell();
            HtmlTableCell celdaFEv = new HtmlTableCell();
            HtmlTableCell celdaFAv = new HtmlTableCell();
            HtmlTableCell celdaNot = new HtmlTableCell();
            HtmlTableCell celdaEst = new HtmlTableCell();
            HtmlTableCell celdaProceso = new HtmlTableCell();
            celdaNum.InnerText = "#";
            celdaEven.InnerText = "Código Incidencia";
            celdaEst.InnerText = "Evento";
            celdaTer.InnerText = "Dispositivo";
            celdaFEv.InnerText = "Fecha Evento";
            celdaFAv.InnerText = "Estado Atención";
            celdaNot.InnerText = "Estado Notificación";
            celdaProceso.InnerText = "Atender Notificacion";

            fila.Cells.Add(celdaNum);
            fila.Cells.Add(celdaEven);
            fila.Cells.Add(celdaEst);
            fila.Cells.Add(celdaTer);
            fila.Cells.Add(celdaFEv);
            fila.Cells.Add(celdaFAv);
            fila.Cells.Add(celdaNot);
            fila.Cells.Add(celdaProceso);
            fila.BgColor = "4E4545";
            fila.Style.Value = "color: #FFFFFF";
            tb_evento.Rows.Add(fila);
            if (!IsCallback) {
                UsuarioObj us = (UsuarioObj)Session["usuario"];
                cargar_eventos_usuario(us);
            }

        }

        public void cargar_eventos_usuario(UsuarioObj usr) {
            AvanceObj[] avances = Globales.servicio.obtener_avance_by_usuario(usr);
            Session["avancesSistema"] = avances;
            int filaNum = 1;
            foreach(AvanceObj avance in avances){
                HtmlTableRow fila = new HtmlTableRow();
                HtmlTableCell celdaNum = new HtmlTableCell();
                HtmlTableCell celdaEven = new HtmlTableCell();
                HtmlTableCell celdaTer = new HtmlTableCell();
                HtmlTableCell celdaFEv = new HtmlTableCell();
                HtmlTableCell celdaDes = new HtmlTableCell();
                HtmlTableCell celdaNot = new HtmlTableCell();
                HtmlTableCell celdaEst = new HtmlTableCell();
                HtmlTableCell celdaProceso = new HtmlTableCell();
                celdaNum.InnerText = filaNum + "";
                
                AlarmasObj a = new AlarmasObj();
                a.id_atm = 0;
                a.id_alarma = avance.id_alarma;
                celdaEven.InnerText = avance.id_alarma+"";
                try
                {
                    AlarmasObj[] alarma = Globales.servicio.obtener_alarma_atm(a);
                    celdaDes.InnerText = alarma[0].mensaje;
                    celdaFEv.InnerText = alarma[0].fecha_registro.ToString("yyyy-MM-dd");
                    string busqueda = alarma[0].id_atm + ":" + "1";
                    Session["alarmasSistema"] = alarma;
                    AtmObj [] terminales = Globales.servicio.buscar_terminal(busqueda,true);
                    celdaTer.InnerText = terminales[0].codigo;
                    
                }
                catch (IndexOutOfRangeException e) {
                    string s = e.Message;
                    celdaEven.InnerText = "Sin mensaje a mostrar";
                }
                celdaEven.InnerText = avance.id_alarma+"";
                
                CheckBox cbxAtencio = new CheckBox();
                cbxAtencio.Checked = avance.atendido;
                celdaEst.Controls.Add(cbxAtencio);
                cbxAtencio.Enabled = false;
                CheckBox cbxNotifi = new CheckBox();
                cbxNotifi.Checked = avance.notificacion;
                celdaNot.Controls.Add(cbxNotifi);
                cbxNotifi.Enabled = false;
                Button atender = new Button();
                atender.Text = "ingresar Atencion";
                atender.ID = "btnAtender_" + filaNum;
                celdaProceso.Controls.Add(atender);
                atender.Click += new EventHandler(this.eventoBtnEditar);
                fila.Cells.Add(celdaNum);
                fila.Cells.Add(celdaEven);
                fila.Cells.Add(celdaDes);
                fila.Cells.Add(celdaTer);
                fila.Cells.Add(celdaFEv);
                fila.Cells.Add(celdaEst);
                fila.Cells.Add(celdaNot);
                fila.Cells.Add(celdaProceso); 
                tb_evento.Rows.Add(fila);
                filaNum++;
            }
        }

        public void eventoBtnEditar(Object sender, EventArgs e)
        {
            string sr = ((Button)sender).ID;
            string sel = sr.Split('_')[1];
            filaSeleccionada = int.Parse(sel) - 1;
            AvanceObj[] alarmas = (AvanceObj[])Session["avancesSistema"];
            Session["AvanceEvento"] = alarmas[filaSeleccionada];
            Response.Redirect("ObservacionEvento.aspx");

        }

    }
}
