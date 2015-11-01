<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile = "~/Contenido/MenuUsuario.Master" CodeBehind="ObservacionEvento.aspx.cs" Inherits="AdministradorTerminal.Contenido.ObservacionEvento" %>
<asp:Content ID = "tituloCambio" ContentPlaceHolderID ="tituloPagina" runat = "server" >
        <asp:Panel ID = "panelTitulo" runat = "server" style = "text-align : center">
            <asp:Label ID="Label1" Text = "ATENDER ALARMA" runat ="server" style = "color: #0033CC"></asp:Label>
        </asp:Panel>
</asp:Content>
<asp:Content ID = "ContenidoPag" ContentPlaceHolderID ="contenidoPagina" runat = "server" >
    <asp:Panel runat = "server">
        <asp:Table runat = "server" ID = "tablaContenido">
            <asp:TableRow>
                <asp:TableCell ColumnSpan = "2">
                    <asp:Label runat = "server" Text = "ATENCIÓN EVENTO USUARIO"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat = "server" Text = "# Incidencia:"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat = "server" ID = "lblNumeroIncidencia"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat = "server" Text = "Fecha Atención :"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat = "server" ID = "lblFechaAtencion"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
           
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat = "server" Text = "Fecha Registro :"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat = "server" ID = "lblFechaRegistro"></asp:Label>
                </asp:TableCell>
           </asp:TableRow>
           
           <asp:TableRow>
            <asp:TableCell>
                <asp:Label runat = "server" Text = "Estado Notificación :"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:CheckBox runat = "server" ID = "chbxNotificacion" Enabled = "false"/>
            </asp:TableCell>
           </asp:TableRow>
           
           <asp:TableRow>
            <asp:TableCell>
                <asp:Label runat = "server" Text = "Estado Atención :"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:CheckBox runat = "server" ID = "chbxAtencion" Enabled = "false"/>
            </asp:TableCell>
           </asp:TableRow>
           
           <asp:TableRow>
            <asp:TableCell>
            <asp:Label runat = "server" Text = "Usuario Atiende :"></asp:Label>
            </asp:TableCell>
            <asp:TableCell> 
                <asp:Label runat = "server" ID = "lblUsuarioAtiendo"></asp:Label>
            </asp:TableCell>
           </asp:TableRow>
           
           <asp:TableRow>
            <asp:TableCell>
                <asp:Label runat = "server" Text = "Usuario Notifica :"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label runat = "server" ID = "lblUsuarioNotifica"></asp:Label>
            </asp:TableCell>
           </asp:TableRow>
           
           <asp:TableRow>
            <asp:TableCell>
                <asp:Label runat = "server" Text = "Observaciones :"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat = "server" ID = "txbxObservacion" TextMode="MultiLine" Width = "180px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="txbxIngresoValidador" runat="server" 
                        ControlToValidate="txbxObservacion" 
                        ErrorMessage="Ingrese una descripcion." 
                        ToolTip="Campo descripcion es obligatorio." 
                        ValidationGroup="panelRegistro">*</asp:RequiredFieldValidator>
            </asp:TableCell>
           </asp:TableRow>
           
           <asp:TableRow>
            <asp:TableCell>
                <asp:Button Text = "Guardar" runat = "server" ID = "btnGuardar" Width = "100px" ValidationGroup="panelRegistro" OnClick = "guardar_observacion_usuario"/>
            </asp:TableCell>
            <asp:TableCell >
                <asp:Button Text = "Cancelar" runat = "server" ID = "btnCancelar" Width = "100px"/>
            </asp:TableCell>
           </asp:TableRow>
           
        </asp:Table>
    </asp:Panel>
</asp:Content>

