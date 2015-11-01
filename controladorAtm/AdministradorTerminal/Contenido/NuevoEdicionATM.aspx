<%@ Page Language="C#" MasterPageFile ="~/Contenido/MenuUsuario.Master" AutoEventWireup="true" CodeBehind="NuevoEdicionATM.aspx.cs" Inherits="AdministradorTerminal.Contenido.NuevoEdicionATM" %>

<asp:Content ID = "tituloCambio" ContentPlaceHolderID ="tituloPagina" runat = "server" >
        <asp:Panel ID = "panelTitulo" runat = "server" style = "text-align : center">
            <asp:Label ID="Label1" Text = "ADMINISTRACION TERMINAL" runat ="server" Font-Size="X-Large" style = "color: #0033CC"></asp:Label>
        </asp:Panel>
</asp:Content>
<asp:Content ID = "ContenidoPag" ContentPlaceHolderID ="contenidoPagina" runat = "server" >
    <asp:Panel runat = "server" >
        <asp:Table runat = "server" >
        <asp:TableRow runat = "server">
            <asp:TableCell runat = "server">
                <asp:TextBox  runat = "server" ID = "txbxIngreso"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="cRadios" runat = "server">
                    <asp:RadioButtonList runat = "server" RepeatDirection="Horizontal" ID = "rbGroup">
                    <asp:ListItem Text = "Codigo" Selected = "true" ></asp:ListItem>
                    <asp:ListItem Text = "Ip"></asp:ListItem>
                    </asp:RadioButtonList>
            </asp:TableCell>
            <asp:TableCell ID = "cBtnBusqueda">
            <asp:Button Text = "Buscar" runat = "server" OnClick = "btn_buscarATM"/>
            </asp:TableCell>
         
            <asp:TableCell ID = "TableCell1">
            <asp:Button ID="Button1" Text = "Nuevo" runat = "server" OnClick = "btn_NuevoATM"/>
            </asp:TableCell>  
        </asp:TableRow>
        </asp:Table>
        
        <table id = "tb_data" runat = "server" style = "z-index: 1" border = "2" width = "100%">
        </table>
    </asp:Panel>
</asp:Content>
