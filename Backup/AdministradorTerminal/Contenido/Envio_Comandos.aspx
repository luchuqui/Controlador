<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile ="~/Contenido/MenuUsuario.Master" CodeBehind="Envio_Comandos.aspx.cs" Inherits="AdministradorTerminal.Contenido.Envio_Comandos" %>



<asp:Content ID="envioComandosPage" ContentPlaceHolderID="contenidoPagina" Runat="Server">
    <asp:Panel ID="Panel2" runat = "server">
      <asp:Table ID="Table3" runat = "server" CellSpacing="20">
        <asp:TableRow >
        <asp:TableCell ColumnSpan="2" HorizontalAlign ="Center">
        <asp:Label ID = "lbltitulo" runat = "server" Text = "ENVIO DE COMANDOS :" Font-Size="X-Large" style = "color: #0033CC; text-align :center">
        </asp:Label>
        </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow >
         <asp:TableCell ID="TableCell1" runat = "server" ColumnSpan="4" HorizontalAlign="Center" BorderStyle="Solid" text= "TERMINALES :" BackColor="#4E4545" ForeColor="#FFFFFF">
          </asp:TableCell>
          <asp:TableCell ID="TableCell2" runat = "server" >
              <asp:DropDownList ID="ATMs_Registrados" runat="server" AutoPostBack= "True" OnSelectedIndexChanged= "seleccionar_terminal" Width ="180px">
                    </asp:DropDownList>
          </asp:TableCell> 
          </asp:TableRow>
          <asp:TableRow >
            <asp:TableCell ID="lblDireccion" runat="server" ColumnSpan="20" HorizontalAlign="Center">
                <asp:Label runat="server" Text= "Dirección Terminal" BorderStyle="Groove" Width="70%" >
                </asp:Label>
            </asp:TableCell>
          </asp:TableRow>
          
           <asp:TableRow >
          <asp:TableCell ID="cRadios" runat = "server" ColumnSpan="2"  HorizontalAlign="Left" BorderStyle="Solid" >
                    <asp:RadioButtonList runat = "server" RepeatDirection="Vertical" ID = "rbGroup" CellSpacing="4" >
                    <asp:ListItem Text = "ATM en servicio" Selected = "true" Value ="1" ></asp:ListItem>
                    <asp:ListItem Text = "ATM fuera de servicio" Value = "2"></asp:ListItem>
                    <asp:ListItem Text = "Contadores" Value = "4"></asp:ListItem>
                    <asp:ListItem Text = "Sincronización" Value = "8"></asp:ListItem>
                    </asp:RadioButtonList>
          </asp:TableCell>
        </asp:TableRow>
       </asp:Table>
       <asp:Table ID="Table2" runat = "server" Width="360px">
        <asp:TableRow >
          <asp:TableCell ID="TableCell3" runat = "server" ColumnSpan="4" HorizontalAlign="Center">
                 <asp:Button ID="Enviar" Text= "Enviar" runat="server" Width ="100px" OnClick = "envioComando"/> 
          </asp:TableCell>
        </asp:TableRow>
       </asp:Table>        
        
        
        <asp:Table ID="Table5" runat = "server" Width="360px" CellSpacing ="6">
        <asp:TableRow >
          <asp:TableCell ID="TableCell4" runat = "server" ColumnSpan="4" HorizontalAlign="Center">
          <asp:Label ID="lblMenEnv" runat="server" Text= "Mensaje Enviado" BorderStyle="Groove" Width="70%" Visible ="true" >
                </asp:Label>
                  </asp:TableCell>
              </asp:TableRow>
                  <asp:TableRow>
          <asp:TableCell ID="TableCell5" runat = "server" ColumnSpan="4" HorizontalAlign="Center">
          <asp:Label ID="lblMensConf" runat="server" Text="Respuesta de Cajero"  BorderStyle="Groove" Width="70%" Visible ="true" >
                </asp:Label>
                  </asp:TableCell>
                  </asp:TableRow>
                 
       </asp:Table>        


     </asp:Panel>
</asp:Content>        
          
          