<%@ Page Language="C#" MasterPageFile = "~/Contenido/MenuUsuario.Master" AutoEventWireup="true" CodeBehind="MonitoreoTerminal.aspx.cs" Inherits="AdministradorTerminal.Contenido.MonitoreoTerminal" %>

<asp:Content ID = "titlo" ContentPlaceHolderID = "tituloPagina" runat =  "server">
    <asp:Panel ID = "panelTitulo" runat = "server" style = "text-align : center">
        <asp:Label ID = "lbltituloUsuario" Text = "Monitoreo Dispositivos" runat = "server"  Font-Size="X-Large" style = "color: #0033CC">
        </asp:Label>
    </asp:Panel>
</asp:Content>
<asp:Content ID="agregarPerfilPage" ContentPlaceHolderID="contenidoPagina" Runat="Server">
    <div>
    <asp:ScriptManager ID = "scriptPanelDatos" runat = "server">
    </asp:ScriptManager>
</div>

<asp:UpdatePanel ID = "panelActualizar" runat = "server" UpdateMode = "Conditional">
<ContentTemplate>
    <asp:Panel ID="Panel1" runat = "server" Width = "100%" ScrollBars = "Vertical" Height="200px">
        <table id = "tb_terminales" runat = "server" style = "z-index: 1; text-align :center; font-size: small; table-layout: fixed;" border = "2" width = "100%">
        </table>
        
    </asp:Panel>
    <asp:Panel ID="Panel3" runat = "server" Width = "70%" HorizontalAlign="Left">
        <asp:Table ID = "tablaDatos" runat = "server">
            <asp:TableRow>
                <asp:TableCell> 
                    <asp:Label Text = "Codigo Terminal :" Font-Bold = "true" runat = "server" ></asp:Label>
                
</asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat = "server" ID = "lbl_codigoTerminal"></asp:Label>
                
</asp:TableCell>
            </asp:TableRow>
            <asp:TableRow >
                <asp:TableCell> 
                    <asp:Label ID="lbl_fecha" Text = "Fecha :" Font-Bold = "true" runat = "server" ></asp:Label>
                
</asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat = "server" ID = "lbl_fechaEvento"></asp:Label>
                
</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        </asp:Panel>
    <asp:Panel runat = "server" ID = "detalleEventos" Width = "90%" ScrollBars = "Vertical" Height="200px">
        <table id = "tb_evento" runat = "server" style = "z-index: 1; font-size: small;" border = "2" width = "100%">
        </table>
    </asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgressBusqueda" runat="server">
        <ProgressTemplate>
        <asp:Panel ID="Panel2" runat = "server" Width = "100%">
            <table style = "text-align : center" width = "100%">
                <tr align = "center" >
                    <td>
                        <asp:Image ID="Image1" ImageUrl ="../Imagenes/procesar.gif" runat = "server" ImageAlign="AbsMiddle" Width= "10%"/>
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