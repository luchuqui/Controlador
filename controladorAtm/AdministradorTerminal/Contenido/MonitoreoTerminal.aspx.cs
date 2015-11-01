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
    public partial class MonitoreoTerminal : System.Web.UI.Page
    {
        private AtmObj[] terminalesUsuario;
        //private int filaSeleccionada;

        protected void Page_Load(object sender, EventArgs e)
        {
            // se cargan los terminales que tiene el usuario
            if (tb_terminales != null) {
                tb_terminales.Rows.Clear();
            }
            
            HtmlTableRow fila = new HtmlTableRow();
            HtmlTableCell celdaNum = new HtmlTableCell();
            HtmlTableCell celdaTer = new HtmlTableCell();
            HtmlTableCell celdaGab = new HtmlTableCell();
            HtmlTableCell celdaLec = new HtmlTableCell();
            HtmlTableCell celdaMod = new HtmlTableCell();
            HtmlTableCell celdaEst = new HtmlTableCell();
            HtmlTableCell celdaLlave = new HtmlTableCell();
            HtmlTableCell celdaProceso = new HtmlTableCell();
            celdaNum.InnerText = "#";
            celdaTer.InnerText = "Terminal";
            celdaEst.InnerText = "Conexión";
            celdaGab.InnerText = "Gavetas";
            celdaGab.ColSpan = 5;
            celdaLlave.InnerText = "Llave";
            celdaLec.InnerText = "Lectora";
            
            celdaMod.InnerText = "Modo";
            celdaProceso.InnerText = "Eventos";
            
            fila.Cells.Add(celdaNum);
            fila.Cells.Add(celdaTer);
            fila.Cells.Add(celdaEst);
            fila.Cells.Add(celdaGab);
            fila.Cells.Add(celdaLec);
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
                terminalesUsuario = Globales.servicio.obtenerTerminalPorUsuario(usrSesion);
                if (terminalesUsuario != null)
                {
                    cargar_datos_tabla(terminalesUsuario);
                    
                }
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

        private void cargar_datos_tabla(AtmObj[] terminales)
        {
            int i = 1;
            foreach (AtmObj terminal in terminales)
            {
                HtmlTableRow fila = new HtmlTableRow();
                HtmlTableCell celdaNum = new HtmlTableCell();
                HtmlTableCell celdaTer = new HtmlTableCell();
                HtmlTableCell celdaGab1 = new HtmlTableCell();
                HtmlTableCell celdaGab2 = new HtmlTableCell();
                HtmlTableCell celdaGab3 = new HtmlTableCell();
                HtmlTableCell celdaGab4 = new HtmlTableCell();
                HtmlTableCell celdaGab5 = new HtmlTableCell();
                HtmlTableCell celdaLec = new HtmlTableCell();
                HtmlTableCell celdaMod = new HtmlTableCell();
                HtmlTableCell celdaEst = new HtmlTableCell();
                HtmlTableCell celdaProceso = new HtmlTableCell();
                HtmlTableCell celdaLlave = new HtmlTableCell();
                celdaNum.InnerText = i + "";
                celdaTer.InnerText = terminal.codigo;
                Image estado = new Image();
                estado.ImageUrl = "~/Imagenes/connect_no.png";
                Image desc = new Image();
                desc.ImageUrl = "~/Imagenes/editclear.png";
                Image desc1 = new Image();
                desc1.ImageUrl = "~/Imagenes/editclear.png";
                Image desc2 = new Image();
                desc2.ImageUrl = "~/Imagenes/editclear.png";
                Image desc3 = new Image();
                desc3.ImageUrl = "~/Imagenes/editclear.png";
                Image desc4 = new Image();
                desc4.ImageUrl = "~/Imagenes/editclear.png";
                Image desc5 = new Image();
                desc5.ImageUrl = "~/Imagenes/editclear.png";
                Image desc6 = new Image();
                desc6.ImageUrl = "~/Imagenes/editclear.png";
                Image desc7 = new Image();
                desc7.ImageUrl = "~/Imagenes/editclear.png";

                estado.Width = 40;
                desc.Width = 30;
                desc1.Width = 30;
                desc2.Width = 30;
                desc3.Width = 30;
                desc4.Width = 30;
                desc5.Width = 30;
                desc6.Width = 30;
                desc7.Width = 30;
                //celdaEst.InnerText = "desconectado";
                estado.ToolTip = "Estado de conexion del terminal";
                celdaEst.Controls.Add(estado);
                
                desc.ToolTip = "Estado de la gaveta Uno";
                celdaGab1.Controls.Add(desc);
                
                desc1.ToolTip = "Estado de la gaveta Dos";
                celdaGab2.Controls.Add(desc1);
                
                desc2.ToolTip = "Estado de la gaveta Tres";
                celdaGab3.Controls.Add(desc2);
                
                desc3.ToolTip = "Estado de la gaveta Cuatro";
                celdaGab4.Controls.Add(desc3);
                
                desc4.ToolTip = "Estado de la gaveta Cinco";
                celdaGab5.Controls.Add(desc4);
                
                desc5.ToolTip = "Estado de Lectora de Tarjeta";
                celdaLec.Controls.Add(desc5);
                
                desc6.ToolTip = "Modo de operacion del atm";
                celdaMod.Controls.Add(desc6);

                desc7.ToolTip = "Estado de llave de encriptacion tipo B";
                celdaLlave.Controls.Add(desc7);

                Button btnEl = new Button();
                btnEl.Text = "Sucesos";
                btnEl.ToolTip = "Visualizar eventos del Terminal";
                //btnEl.Click += new EventHandler(this.eventoBtnSucesos);
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
                fila.Cells.Add(celdaLec);
                fila.Cells.Add(celdaMod);
                fila.Cells.Add(celdaLlave);
                fila.Cells.Add(celdaProceso);
                tb_terminales.Rows.Add(fila);
                i++;
            }
            Session["terminalSistemaUsr"] = terminales;
        }
        
    }
}
