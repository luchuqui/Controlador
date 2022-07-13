<%@ Page Language="C#"  MasterPageFile ="~/Contenido/MenuUsuario.Master" AutoEventWireup="true" CodeBehind="ATM.aspx.cs" Inherits="AdministradorTerminal.Contenido.ATM" %>

<asp:Content ID = "titlo" ContentPlaceHolderID = "tituloPagina" runat =  "server">
    <asp:Panel ID = "panelTitulo" runat = "server" style = "text-align : center">
        <asp:Label ID = "lbltituloUsuario" runat = "server"  Text = "ADMINISTRACION TERMINAL" Font-Size="X-Large" style = "color: #0033CC">
        </asp:Label>
    </asp:Panel>
</asp:Content>
<asp:Content ID="agregarPerfilPage" ContentPlaceHolderID="contenidoPagina" Runat="Server">
    <asp:Panel runat = "server" Width = "80%">
        <asp:Table runat = "server" ID = "datosTerminal" Width = "100%">
        
        <asp:TableRow ID="TableRow9" runat = "server">
                <asp:TableCell ID="TableCell17" runat = "server">
                    <asp:Label ID = "Label2" runat = "server" Text = "Identificador :"/>
                </asp:TableCell>
                <asp:TableCell ID="TableCell18" runat = "server">
                    <asp:TextBox ID = "txbxIdentificadorTerminal" runat = "server" Enabled = "false"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow runat = "server">
                <asp:TableCell runat = "server">
                    <asp:Label ID = "lblidTerminal" runat = "server" Text = "Código :"/>
                </asp:TableCell>
                <asp:TableCell ID="celda" runat = "server">
                    <asp:TextBox ID = "txbxCodigo" runat = "server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                                ControlToValidate="txbxCodigo" 
                                                ErrorMessage="El código  es obligatorio." 
                                                ToolTip="El código es obligatorio." 
                                                ValidationGroup="IngresoTerminal">*</asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow1" runat = "server">
                <asp:TableCell ID="TableCell1" runat = "server">
                    <asp:Label ID = "lblIp" runat = "server" Text = "Dirección Ip :"/>
                </asp:TableCell>
                <asp:TableCell ID="TableCell2" runat = "server">
                    <asp:TextBox ID = "txbxIp" runat = "server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                ControlToValidate="txbxIp" 
                                                ErrorMessage="La direccion Ip es obligatoria." 
                                                ToolTip="La direccion Ip es obligatoria" 
                                                ValidationGroup="IngresoTerminal">*</asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow2" runat = "server">
                <asp:TableCell ID="TableCell3" runat = "server">
                    <asp:Label ID = "lblUbicaion" runat = "server" Text = "Ubicación :"/>
                </asp:TableCell>
                <asp:TableCell ID="TableCell4" runat = "server">
                    <asp:TextBox ID = "txbxUbicacion" runat = "server" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                ControlToValidate="txbxUbicacion" 
                                                ErrorMessage="La ubicación del terminal es obligatoria." 
                                                ToolTip="La ubicación del terminal es obligatoria" 
                                                ValidationGroup="IngresoTerminal">*</asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow ID="TableRow6" runat = "server">
                <asp:TableCell ID="TableCell11" runat = "server">
                    <asp:Label ID = "lblEstado" runat = "server" Text = "Estado :"/>
                </asp:TableCell>
                <asp:TableCell ID="TableCell12" runat = "server">
                    <asp:DropDownList ID = "lsEstado" runat = "server" AutoPostBack="True"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow7" runat = "server">
                <asp:TableCell ID="TableCell13" runat = "server">
                    <asp:Button ID = "btnGuardar" Text = "Guardar" runat = "server" OnClick = "btn_guardarActualizarDatos" ValidationGroup="IngresoTerminal"/>
                </asp:TableCell>
                <asp:TableCell ID="TableCell14" runat = "server">
                    <asp:Button ID = "btnCancelar" runat = "server" Text = "Cancelar" OnClick = "btn_cancelar"/>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow8" runat = "server">
              <asp:TableCell >
              <asp:Label ID = "lblmensaje" runat = "server" ForeColor ="Blue" >
              </asp:Label>
              </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
</asp:Content>