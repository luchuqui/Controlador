<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile ="~/Contenido/MenuUsuario.Master" CodeBehind="MensajeBienvenida.aspx.cs" Inherits="AdministradorTerminal.Contenido.MensajeBienvenida" %>

<asp:Content ID = "contenidoTitulo" ContentPlaceHolderID ="tituloPagina" runat = "server">
    <asp:Panel style = "text-align : center " runat = "server">
            <asp:Label ID = "tituloPagina" Text = "BIENVENIDO AL SISTEMA DE ADMINISTRACION Y MONITOREO" runat = "server" style = "color: #0033CC" Font-Size = "X-Large"></asp:Label>
    </asp:Panel>
</asp:Content>
<asp:Content ID = "contenidoPagina" runat = "server" ContentPlaceHolderID = "contenidoPagina">
    <asp:Table runat = "server">
        <asp:TableRow>
        <asp:TableCell ColumnSpan = "2">
            <asp:Label ID = "derechos" Text = "Derechos reservados" runat = "server"></asp:Label>
        </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
            <asp:Label ID="idMensajeSistema" Text = "Consejos de seguridad; no se olvide de cambiar su clave periodicamente" runat = "server"></asp:Label> 
            </asp:TableCell>
            <asp:TableCell>
                <asp:Image runat = "server" ImageUrl = "~/Imagenes/Administracion.jpg" ImageAlign="Middle" 
                    Width="50%"/>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>