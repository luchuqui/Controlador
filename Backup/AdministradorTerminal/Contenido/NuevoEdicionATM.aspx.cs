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
    public partial class NuevoEdicionATM : System.Web.UI.Page
    {
        private int filaSeleccionada;
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlTableRow fila = new HtmlTableRow();
            HtmlTableCell celdaCod = new HtmlTableCell();
            HtmlTableCell celdaIp = new HtmlTableCell();
            HtmlTableCell celdaUbi = new HtmlTableCell();
            HtmlTableCell celdaEst = new HtmlTableCell();
            HtmlTableCell celdaProceso = new HtmlTableCell();
            celdaCod.InnerText = "Codigo ATM";
            celdaIp.InnerText = "Ip ATM";
            celdaUbi.InnerText = "Ubicación";
            celdaEst.InnerText = "Estado";
            celdaProceso.InnerText = "Tarea";
            celdaProceso.ColSpan = 2;
            fila.Cells.Add(celdaCod);
            fila.Cells.Add(celdaIp);
            fila.Cells.Add(celdaUbi);
            fila.Cells.Add(celdaEst);
            fila.Cells.Add(celdaProceso);
            fila.BgColor = "4E4545";
            fila.Style.Value = "color: #FFFFFF";
            tb_data.Rows.Add(fila);
            CuadroMensaje mensajeNotificacion = new CuadroMensaje(sender, this.GetType());
            if (IsPostBack)
            {
                AtmObj[] terminales = (AtmObj[])Session["terminalesSistema"];
                if (terminales != null)
                {
                    cargar_datos_tabla(terminales,mensajeNotificacion);
                }
            }           
        }
        public void btn_buscarATM(object sender, EventArgs e)
        {

            string seleccionado = this.rbGroup.SelectedItem.Text;
            AtmObj[] lsatm = { };
            if (seleccionado.Equals("Codigo"))
            {
                lsatm = Globales.servicio.buscar_terminal(this.txbxIngreso.Text, true);
            }
            else if (seleccionado.Equals("Ip"))
            {
                lsatm = Globales.servicio.buscar_terminal(this.txbxIngreso.Text, false);
            }
            CuadroMensaje mensajeNotificacion = new CuadroMensaje(sender, this.GetType());
            cargar_datos_tabla(lsatm, mensajeNotificacion);
        }

        public void buscar_datos() {
            string seleccionado = this.rbGroup.SelectedItem.Text;
            AtmObj[] lsatm = { };
            if (seleccionado.Equals("Codigo"))
            {
                if (string.IsNullOrEmpty(this.txbxIngreso.Text))
                {
                    this.txbxIngreso.Text = " :0";
                }
                else {
                    this.txbxIngreso.Text += ":0";
                }
                lsatm = Globales.servicio.buscar_terminal(this.txbxIngreso.Text, true);
            }
            else if (seleccionado.Equals("Ip"))
            {
                lsatm = Globales.servicio.buscar_terminal(this.txbxIngreso.Text, false);
            }
            CuadroMensaje mensajeNotificacion = new CuadroMensaje(new object(), this.GetType());
            cargar_datos_tabla(lsatm, mensajeNotificacion);
        }

        public void cargar_datos_tabla(AtmObj[] terminales, CuadroMensaje mensajeNotificacion)
        {
            limpiar_tabla_datos();
            int i = 1;
            
            foreach (AtmObj atm in terminales)
            {
                if (string.IsNullOrEmpty(atm.estado)) {
                    mensajeNotificacion.mostrar_mensaje_alerta("No existen terminales");
                    break;
                }
                HtmlTableRow fila = new HtmlTableRow();
                HtmlTableCell celdaCod = new HtmlTableCell();
                HtmlTableCell celdaIp = new HtmlTableCell();
                HtmlTableCell celdaUbi = new HtmlTableCell();
                HtmlTableCell celdaEst = new HtmlTableCell();
                HtmlTableCell celdaProceso = new HtmlTableCell();
                celdaCod.InnerText = atm.codigo;
                celdaIp.InnerText = atm.ip;
                celdaUbi.InnerText = atm.ubicacion;
                string strEstado = "";
                if (atm.estado.Equals("A"))
                {
                    strEstado = "Activo";
                }
                else
                {
                    strEstado = "Bloqueado";
                }
                celdaEst.InnerText = strEstado;
                Button btnEd = new Button();
                btnEd.Text = "Editar";
                btnEd.ID = "btEd_" + i;
                btnEd.Click += new EventHandler(this.eventoBtnEditar);
                Button btnEl = new Button();
                btnEl.Text = "Eliminar";
                btnEl.Click += new EventHandler(this.eventoBtnEliminar);
                celdaProceso.Align = "Center";
                btnEl.ID = "btnEl_" + i;
                btnEl.OnClientClick = "return confirm('¿Desea elminar registro?');";
                celdaProceso.Controls.Add(btnEd);
                celdaProceso.Controls.Add(btnEl);
                fila.Cells.Add(celdaCod);
                fila.Cells.Add(celdaIp);
                fila.Cells.Add(celdaUbi);
                fila.Cells.Add(celdaEst);
                fila.Cells.Add(celdaProceso);
                tb_data.Rows.Add(fila);
                i++;
            }
            //buscar_datos();
             Session["terminalesSistema"] = terminales;
        }

        public void limpiar_tabla_datos() {
            if (tb_data.Rows.Count > 1) {
                int filas = tb_data.Rows.Count-1;
                for (int i = filas; i > 0; i--) {
                    tb_data.Rows.RemoveAt(i);
                }
            }
        }

        public void eventoBtnEditar(Object sender,EventArgs e) {
            string sr = ((Button)sender).ID;
            string sel = sr.Split('_')[1];
            filaSeleccionada = int.Parse(sel)-1;
            AtmObj[] terminales = (AtmObj[])Session["terminalesSistema"];
            Session["terSeleccionado"] = terminales[filaSeleccionada];
            Response.Redirect("ATM.aspx");
            
        }

        public void eventoBtnNuevo(Object sender, EventArgs e)
        {
            Session["terSeleccionado"] = null;
            Response.Redirect("ATM.aspx");
        }

        public void eventoBtnEliminar(Object sender, EventArgs e)
        {
            string sr = ((Button)sender).ID;
            string sel = sr.Split('_')[1];
            filaSeleccionada = int.Parse(sel) - 1;
            AtmObj[] lsterminales = (AtmObj[])Session["terminalesSistema"];
            AtmObj terminal = lsterminales[filaSeleccionada];
            terminal.estado = "E";
            string mensaje = Globales.servicio.guardar_actualizar_terminal(terminal, true);
            if (mensaje.StartsWith("Actualizacion de"))
            {
                mensaje = "Terminal Eliminado";
                tb_data.Rows.RemoveAt(filaSeleccionada);
            }
            CuadroMensaje mensajeNotificacion = new CuadroMensaje(sender, this.GetType());
            mensajeNotificacion.mostrar_mensaje_alerta(mensaje);
        }

        public void btn_NuevoATM(object sender, EventArgs e)
        {
            Session["terSeleccionado"] = null;
            Response.Redirect("ATM.aspx");
        }
    }
 }

    

