<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Contenido/MenuUsuario.Master" CodeBehind="CambioContrasenia.aspx.cs" Inherits="AdministradorTerminal.Contenido.CambioContrasenia" %>

<asp:Content ID = "tituloCambio" ContentPlaceHolderID ="tituloPagina" runat = "server" >
        <asp:Panel ID = "panelTitulo" runat = "server" style = "text-align : center">
            <asp:Label ID="Label1" Text = "Cambio Contraseña" runat ="server" style = "color: #0033CC"></asp:Label>
        </asp:Panel>
</asp:Content>
<asp:Content ID = "ContenidoPag" ContentPlaceHolderID ="contenidoPagina" runat = "server" >
    <asp:Panel runat = "server">
        <asp:Table runat = "server">
            <asp:TableRow>
                <asp:TableCell ColumnSpan = "2">
                <asp:Label runat = "server" Text ="Por su seguridad debe cambiar de contraseña antes de empezar"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label runat = "server" Text = "Contraseña Actual :"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat ="server" ID = "txbxPassActual" TextMode = "Password"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblnuevo" runat = "server" Text = "Contraseña Nueva :"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat ="server" ID = "txbxNueva" TextMode = "Password"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblConfirmar" runat = "server" Text = "Contraseña Nueva Confirmar :"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox runat ="server" ID = "txbxConfirmar" TextMode ="Password" ></asp:TextBox>
                    
                </asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID = "btnGuardar" runat = "server" Text = "Cambiar" OnClick = "cambiar_contrasenia"/>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID = "btnLimpiar" runat = "server" Text = "Limpiar" OnClick = "limpiar_pantalla"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
</asp:Content>