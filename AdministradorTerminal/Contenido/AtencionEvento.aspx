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
    <div>
    <asp:GridView ID="GridViewIncidente" HeaderStyle-BackColor="#e94f31" HeaderStyle-ForeColor="White"  
                runat="server" AutoGenerateColumns="False" AllowPaging = "True" 
                CellSpacing = "1"  OnRowCommand="edicion_fila_suceso" 
                onpageindexchanged="GridViewIncidente_PageIndexChanged_" 
                onpageindexchanging="GridViewIncidente_PageIndexChanging" 
                Font-Size="Smaller" Width="100%">  
                <Columns>  
                    <asp:BoundField DataField="id_alarma" HeaderText="# Suceso" 
                        ItemStyle-Width="100" >  
                    <ItemStyle Width="11%"></ItemStyle>
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="fecha_registro" HeaderText="Fecha Incidente" 
                        ItemStyle-Width="150" DataFormatString="{0:MM-dd-yyyy HH:mm}" >
<ItemStyle Width="12%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="fecha_atencion" 
                        ItemStyle-Width="150" DataFormatString="{0:MM-dd-yyyy HH:mm}" 
                        HeaderText="Fecha Atencion" >
                    <ItemStyle Width="12%"></ItemStyle>
                    </asp:BoundField>
                    
                    
                    
                    <asp:BoundField DataField="atendido" HeaderText="Esta Atendido" 
                        ItemStyle-Width="150" >    
                    <ItemStyle Width="11%"></ItemStyle>
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="observacion" HeaderText="Trabajo Realizado" 
                        ItemStyle-Width="100" >  
                    <ItemStyle Width="40%"></ItemStyle>
                    </asp:BoundField>
                    
                    <asp:ButtonField ButtonType="Button" CommandName="Edicion" Text="Edición"> 
                        <ItemStyle Width="5%" />
                    </asp:ButtonField>
                </Columns>  

                <HeaderStyle BackColor="#4E4545" ForeColor="White"></HeaderStyle>
            </asp:GridView>
    </div>
</asp:Panel>
</asp:Content>