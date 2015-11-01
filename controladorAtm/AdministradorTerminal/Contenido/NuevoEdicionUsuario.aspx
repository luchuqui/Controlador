<%@ Page Language="C#" MasterPageFile ="~/Contenido/MenuUsuario.Master" AutoEventWireup="true" CodeBehind="NuevoEdicionUsuario.aspx.cs" Inherits="AdministradorTerminal.Contenido.NuevoEdicionUsuario" %>
<asp:Content ID = "tituloCambio" ContentPlaceHolderID ="tituloPagina" runat = "server" >
        <asp:Panel ID = "panelTitulo" runat = "server" style = "text-align : center">
            <asp:Label ID="Label1" Text = "Administrador Usuario" runat ="server" style = "color: #0033CC"></asp:Label>
        </asp:Panel>
</asp:Content>
<asp:Content ID = "ContenidoPag" ContentPlaceHolderID ="contenidoPagina" runat = "server" >
<div>
    <asp:ScriptManager ID = "scriptPanelDatos" runat = "server">
    </asp:ScriptManager>
</div>
<asp:UpdatePanel ID = "panelActualizar" runat = "server" UpdateMode = "Conditional" >
<ContentTemplate>
    <asp:Panel runat = "server">
        <asp:Table runat = "server">
        <asp:TableRow runat = "server">
            <asp:TableCell runat = "server">
                <asp:TextBox  runat = "server" ID = "txbxIngreso"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ID="cRadios" runat = "server">
                    <asp:RadioButtonList runat = "server" RepeatDirection="Horizontal" ID = "rbGroup">
                    <asp:ListItem Text = "Documento" Selected = "true" ></asp:ListItem>
                    <asp:ListItem Text = "Nombre"></asp:ListItem>
                    </asp:RadioButtonList>
            </asp:TableCell>
            <asp:TableCell ID = "cBtnBusqueda">
            <asp:Button Text = "Buscar" runat = "server" OnClick = "btn_buscarUsuario"/>
            </asp:TableCell>
            <asp:TableCell ID = "cBtnNuevo">
            <asp:Button ID="btnNuevo" Text = "Nuevo" runat = "server" OnClick = "eventoBtnNuevo"/>
            </asp:TableCell>
        </asp:TableRow>
        </asp:Table>
        
        <table id = "tb_data" runat = "server" style = "z-index: 1" border = "2" width = "100%">
        </table>
    </asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgressBusqueda" runat="server">
        <ProgressTemplate>
        <asp:Panel runat = "server" Width = "100%">
            <table style = "text-align : center" width = "100%">
                <tr align = "center" >
                    <td>
                        <asp:Image ImageUrl ="../Imagenes/procesar.gif" runat = "server" ImageAlign="AbsMiddle"/>
                    </td>
                </tr>
                <tr>
                    <td align = "center" >
                        Procesando...
                    </td>
                </tr>
            </table>
                </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
