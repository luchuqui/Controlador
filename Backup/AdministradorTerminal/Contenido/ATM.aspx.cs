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
    public partial class ATM : System.Web.UI.Page
    {
        private bool nuevo;
        protected string estadoSeleccionado;
        //protected string perfilSeleccionado;

        protected void Page_Load(object sender, EventArgs e)
        {
             //UsuarioObj usr = (UsuarioObj)Session["usrSeleccionado"];
            AtmObj ter = (AtmObj)Session["terSeleccionado"];
            cargar_datos(ter);
            cargarDatosTerminal(ter);
        }

        public void cargar_datos(AtmObj ter)
        {
            estadoSeleccionado = lsEstado.SelectedValue;
            lsEstado.Items.Clear();
            lsEstado.Items.Add(new ListItem("Bloqueado", "B"));
            lsEstado.Items.Add(new ListItem("Activo", "A"));
            if (ter != null)
            {
                nuevo = true;
                if (!IsPostBack)
                {
                    estadoSeleccionado = ter.estado;
                }
                lsEstado.SelectedValue = estadoSeleccionado;
                lsEstado.Enabled = true;
            }
            else
            {
                lsEstado.SelectedValue = "A";
                lsEstado.Enabled = false;
                nuevo = false;
                 }
        }

        public void cargarDatosTerminal(AtmObj atm)
        {
            if (atm != null )
            {
                if (!IsPostBack)
                {
                    txbxCodigo.Text = atm.codigo;
                    txbxIp.Text = atm.ip;
                    txbxUbicacion.Text = atm.ubicacion;
                    //lsEstado.SelectedValue = atm.estado;
                    this.txbxIdentificadorTerminal.Text = atm.id_atm.ToString();
                }
            }
        }

        public void btn_guardarActualizarDatos(object sender, EventArgs e)
        {
            AtmObj atm = (AtmObj)Session["terSeleccionado"];
            if (atm == null)
                atm = new AtmObj();
        
            atm.codigo = txbxCodigo.Text;
            atm.ip = txbxIp.Text;
            atm.ubicacion = txbxUbicacion.Text;
            atm.estado = estadoSeleccionado;
            Session["terSeleccionado"] = atm;
            if (!string.IsNullOrEmpty(txbxIdentificadorTerminal.Text))
            {
                atm.id_atm = int.Parse(txbxIdentificadorTerminal.Text);
            }
            string estado = "A";
            if (lsEstado.SelectedValue.Equals("B"))
            {
                estado = "B";
            }
            atm.estado = estado;
            string mensaje = Globales.servicio.guardar_actualizar_terminal(atm , nuevo);
            CuadroMensaje mensajeNotificacion = new CuadroMensaje(sender, this.GetType());
            lblmensaje.Text = mensaje; 
            
            // mensajeNotificacion.mostrar_mensaje_alerta(mensaje);
        }

        public void btn_cancelar(object sender, EventArgs e)
        {
            Session["terSeleccionado"] = null;
            Session["terSeleccionado"] = null;
            Response.Redirect("NuevoEdicionATM.aspx");
        }

    }
}
