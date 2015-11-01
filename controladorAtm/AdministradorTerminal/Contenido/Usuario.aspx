<%@ Page Language="C#" MasterPageFile ="~/Contenido/MenuUsuario.Master"  AutoEventWireup="true" CodeBehind="Usuario.aspx.cs" Inherits="AdministradorTerminal.Contenido.Usuario" %>
<asp:Content ID = "titlo" ContentPlaceHolderID = "tituloPagina" runat =  "server">
    <asp:Panel ID = "panelTitulo" runat = "server" style = "text-align : center">
        <asp:Label ID = "lbltituloUsuario" runat = "server"  Font-Size="X-Large" style = "color: #0033CC">
        </asp:Label>
    </asp:Panel>
</asp:Content>
<asp:Content ID="agregarPerfilPage" ContentPlaceHolderID="contenidoPagina" Runat="Server">
    <asp:Panel runat = "server" Width = "80%">
        <asp:Table runat = "server" ID = "datosUsuario" Width = "100%">
        
        <asp:TableRow ID="TableRow9" runat = "server">
                <asp:TableCell ID="TableCell17" runat = "server">
                    <asp:Label ID = "Label2" runat = "server" Text = "Identificador :"/>
                </asp:TableCell>
                <asp:TableCell ID="TableCell18" runat = "server">
                    <asp:TextBox ID = "txbxIdentificadorUsuario" runat = "server" Enabled = "false"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow runat = "server">
                <asp:TableCell runat = "server">
                    <asp:Label ID = "lblidUsuario" runat = "server" Text = "Número Documento :"/>
                </asp:TableCell>
                <asp:TableCell ID="celda" runat = "server">
                    <asp:TextBox ID = "txbxDocumento" runat = "server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                                ControlToValidate="txbxDocumento" 
                                                ErrorMessage="El numero documento es obligatorio." 
                                                ToolTip="El numero documento es obligatorio." 
                                                ValidationGroup="IngresoUsuario">*</asp:RequiredFieldValidator>
                    <asp:RangeValidator MaximumValue = "9" runat = "server" ControlToValidate="txbxDocumento" ValidationGroup="IngresoUsuario">longitud máxima 10 números</asp:RangeValidator>
                    
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow1" runat = "server">
                <asp:TableCell ID="TableCell1" runat = "server">
                    <asp:Label ID = "lblNombre" runat = "server" Text = "Nombres :"/>
                </asp:TableCell>
                <asp:TableCell ID="TableCell2" runat = "server">
                    <asp:TextBox ID = "txbxNombre" runat = "server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                ControlToValidate="txbxNombre" 
                                                ErrorMessage="El nombre usuario es obligatorio." 
                                                ToolTip="El nombre usuario es obligatorio." 
                                                ValidationGroup="IngresoUsuario">*</asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow2" runat = "server">
                <asp:TableCell ID="TableCell3" runat = "server">
                    <asp:Label ID = "lblApellido" runat = "server" Text = "Apellidos :"/>
                </asp:TableCell>
                <asp:TableCell ID="TableCell4" runat = "server">
                    <asp:TextBox ID = "txbxApellido" runat = "server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                ControlToValidate="txbxApellido" 
                                                ErrorMessage="El apellido usuario es obligatorio." 
                                                ToolTip="El apellido usuario es obligatorio." 
                                                ValidationGroup="IngresoUsuario">*</asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow3" runat = "server">
                <asp:TableCell ID="TableCell5" runat = "server">
                    <asp:Label ID = "lblCorreo" runat = "server" Text = "Correo :"/>
                </asp:TableCell>
                <asp:TableCell ID="TableCell6" runat = "server">
                    <asp:TextBox ID = "txbxCorreo" runat = "server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                ControlToValidate="txbxCorreo" 
                                                ErrorMessage="El correo usuario es obligatorio." 
                                                ToolTip="El correo usuario es obligatorio." 
                                                ValidationGroup="IngresoUsuario">*</asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow4" runat = "server">
                <asp:TableCell ID="TableCell7" runat = "server">
                    <asp:Label ID = "lblPerfil" runat = "server" Text = "Perfil :"/>
                </asp:TableCell>
                <asp:TableCell ID="TableCell8" runat = "server">
                    <asp:DropDownList ID = "lsPerfiles" runat = "server" AutoPostBack="True"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="TableRow5" runat = "server">
                <asp:TableCell ID="TableCell9" runat = "server">
                    <asp:Label ID = "lblTelefono" runat = "server" Text = "Telefono :"/>
                </asp:TableCell>
                <asp:TableCell ID="TableCell10" runat = "server">
                    <asp:TextBox ID = "txbxTelefono" runat = "server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                ControlToValidate="txbxTelefono" 
                                                ErrorMessage="El telefono usuario es obligatorio." 
                                                ToolTip="El telefono usuario es obligatorio." 
                                                ValidationGroup="IngresoUsuario">*</asp:RequiredFieldValidator>
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
                <asp:TableCell ID="TableCell13" runat = "server" >
                    <asp:Button ID = "btnGuardar" Text = "Guardar" runat = "server" OnClick = "btn_guardarActualizarDatos" ValidationGroup="IngresoUsuario" />
                </asp:TableCell>
                <asp:TableCell ID="TableCell14" runat = "server" ColumnSpan = "2">
                    <asp:Button ID = "btnCancelar" runat = "server" Text = "Cancelar" OnClick = "btn_cancelar"/>
                    <asp:Button ID = "btnResetear" runat = "server" Text = "Resetear Clave" OnClick = "cambiarContrasenia"/>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
            <asp:TableCell>
           <asp:Label ID = "lblmensaje" runat = "server" ForeColor ="Blue" > </asp:Label>
            </asp:TableCell>
            
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
</asp:Content>