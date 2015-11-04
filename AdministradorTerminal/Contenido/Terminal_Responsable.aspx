<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile ="~/Contenido/MenuUsuario.Master" CodeBehind="Terminal_Responsable.aspx.cs" Inherits="AdministradorTerminal.Contenido.Termina_Responsable" %>

<asp:Content ID = "titulo" ContentPlaceHolderID = "tituloPagina" runat = "server">
    <asp:Panel ID = "panelTitulo" runat = "server" style = "text-align : center">
        <asp:Label ID = "lbltitulo" runat = "server" Text = "ADMINISTRACION TERMINAL" Font-Size="X-Large" style = "color: #0033CC">
        </asp:Label>
    </asp:Panel>
</asp:Content>

<asp:Content ID="agregarTerminalPage" ContentPlaceHolderID="contenidoPagina" Runat="Server">
    <asp:Panel ID="Panel2" runat = "server">
    
        <asp:Table ID="Table3" runat = "server" CellSpacing = "3" Width = "80%" CellPadding="3">
        <asp:TableRow>
    <asp:TableCell ID="cRadios" runat = "server" ColumnSpan="4" HorizontalAlign="Left" Width = "100%">
                    <asp:RadioButtonList runat = "server" RepeatDirection="Horizontal" ID = "rbGroup">
                    <asp:ListItem Text = "Documento" Selected = "true" ></asp:ListItem>
                    <asp:ListItem Text = "Nombre"></asp:ListItem>
                    </asp:RadioButtonList>
    </asp:TableCell>
    </asp:TableRow>
        <asp:TableRow ID="TableRow1" runat = "server">
            <asp:TableCell ID="TableCell1" runat = "server" ColumnSpan ="1" HorizontalAlign = "Left">
                <asp:TextBox  runat = "server" ID = "txbxIngreso" Width = "100%"></asp:TextBox>
            </asp:TableCell>
            <asp:TableCell ColumnSpan="3" ID = "cBtnBusqueda" HorizontalAlign = "Left">
            <asp:Button ID="Button1" Text = "Buscar" runat = "server" OnClick = "btn_buscarUsuario" ToolTip="Si no hay datos, se muestra todos los datos de los usuarios" /> 
         </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell HorizontalAlign="Left">
                    <asp:Label runat = "server" Text = "Usuarios"></asp:Label>
                </asp:TableCell>
                
                <asp:TableCell ColumnSpan = "2" HorizontalAlign = "Left">
                    <asp:Label ID="Label1" runat = "server" Text = "Terminales Por Asignar"></asp:Label>
                </asp:TableCell>
                
                <asp:TableCell HorizontalAlign = "Left">
                    <asp:Label ID="Label2" runat = "server" Text = "Terminales Asignados"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell Width="30%">
                    <asp:DropDownList ID="lista_Usuarios" runat="server" AutoPostBack= "True" OnSelectedIndexChanged= "cargar_terminales_no_asignados" Width = "100%">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell Width = "30%">
                    <asp:ListBox ID="lista_Atm_no_asignados" runat="server" AutoPostBack="True" OnSelectedIndexChanged = "seleccionar_terminales_usuario" EnableViewState = "true" Width = "100%"></asp:ListBox>
                </asp:TableCell>
                 <asp:TableCell Width = "5%">
                 <asp:Table runat="server" Width = "10%">
                 <asp:TableRow>
                 <asp:TableCell Width="50%">
                 <asp:Button ID="btnAgregar" Text= " > " runat="server" OnClick="cargar_guardar_terminales_asignados"/> 
                 </asp:TableCell>
                 <asp:TableCell Width="50%">
                 <asp:Button ID="Button2" Text= ">>" runat="server" OnClick="agregar_terminales_usuario_todos"/> 
                 </asp:TableCell>
                     </asp:TableRow>
                  <asp:TableRow>
                 <asp:TableCell Width="50%">
                 <asp:Button ID="btnQuitar" Text=" < " runat="server" OnClick="quitar_guardar_terminales_asignados"/> 
                 </asp:TableCell>
                  <asp:TableCell Width="50%">
                 <asp:Button ID="Button3" Text="<<" runat="server" OnClick="quitar_terminales_usuario_todos"/> 
                 </asp:TableCell>
                     </asp:TableRow>
                 </asp:Table>
                </asp:TableCell>
                <asp:TableCell Width= "30%">
                    <asp:ListBox ID="lista_Atm_asignados" runat="server" OnSelectedIndexChanged = "quitar_terminales_ls_usuario" EnableViewState = "true" Width = "100%"></asp:ListBox>
                </asp:TableCell> 
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="4" HorizontalAlign = "Center" ID = "cBtnGuardar" Width="100%">
            <asp:Button ID="BtnLimpiar" Text = "Limpiar" runat = "server" OnClick = "limpiar_datos" Width ="20%"/>
                </asp:TableCell>
            </asp:TableRow>

        </asp:Table> 
    </asp:Panel>
</asp:Content>

