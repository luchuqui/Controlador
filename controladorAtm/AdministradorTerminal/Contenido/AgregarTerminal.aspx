<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile ="~/Contenido/MenuUsuario.Master"  CodeBehind="AgregarTerminal.aspx.cs" Inherits="AdministradorTerminal.Contenido.AgregarTerminal" %>
<asp:Content ID = "titulo" ContentPlaceHolderID = "tituloPagina" runat = "server">
    <asp:Panel ID = "panelTitulo" runat = "server" style = "text-align : center">
        <asp:Label ID = "lbltitulo" runat = "server" Text = "AGREGAR TERMINAL" Font-Size="X-Large" style = "color: #0033CC">
        </asp:Label>
    </asp:Panel>
</asp:Content>
<asp:Content ID="agregarTerminalPage" ContentPlaceHolderID="contenidoPagina" Runat="Server">
    <asp:Panel runat = "server">
        <asp:Table runat = "server">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID = "labelCodigo" Text = "Codigo Terminal :" runat = "server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID = "txbxCodigoTerminal" runat = "server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID = "labelDirIp" Text = "Dirección IP :" runat = "server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox ID = "txbxDirIP" runat = "server"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID = "labelDescripcion" Text = "Descripción :" runat = "server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:TextBox id = "txbxDescripcion" runat = "server" TextMode="MultiLine"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID = "labelModelo" Text = "Modelo Terminal :" runat = "server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID = "cboxModelo" runat = "server"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Button ID = "btnGuardar" runat = "server" Text = "Guardar" OnClick = "guardar_datos"/>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Button ID = "btnLimpiar" runat = "server" Text = "Limpiar" OnClick = "limpiar_datos"/>
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

