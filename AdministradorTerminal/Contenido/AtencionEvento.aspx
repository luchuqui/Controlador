<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile = "~/Contenido/MenuUsuario.Master" CodeBehind="AtencionEvento.aspx.cs" Inherits="AdministradorTerminal.Contenido.AtencionEvento" %>

<asp:Content ID = "titulo" ContentPlaceHolderID = "tituloPagina" runat =  "server">
    <asp:Panel ID = "panelTitulo" runat = "server" style = "text-align : center">
        <asp:Label ID = "lbltituloUsuario" runat = "server"  Font-Size="X-Large" style = "color: #0033CC">
        </asp:Label>
    </asp:Panel>
</asp:Content>
<asp:Content ID="agregarPerfilPage" ContentPlaceHolderID="contenidoPagina" Runat="Server">
<asp:Panel runat = "server">
    <asp:Table runat = "server">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID ="lblEror" runat = "server" Text = "Codigo Suceso : "></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox runat = "server" ID = "txbxCodigoSuceso"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Button runat = "server" ID = "btnBusqueda" ToolTip = "Busca el codigo suceso en el sistema" Text = "Buscar"/>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <table id = "tb_evento" runat = "server" style = "z-index: 1" border = "2" width = "100%">
    </table>
</asp:Panel>
</asp:Content>