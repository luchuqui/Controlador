<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile ="~/Contenido/MenuUsuario.Master" CodeBehind="Terminal_Responsable.aspx.cs" Inherits="AdministradorTerminal.Contenido.Termina_Responsable" %>

<asp:Content ID = "titulo" ContentPlaceHolderID = "tituloPagina" runat = "server">
    <asp:Panel ID = "panelTitulo" runat = "server" style = "text-align : center">
        <asp:Label ID = "lbltitulo" runat = "server" Text = "ADMINISTRACION TERMINAL" Font-Size="X-Large" style = "color: #0033CC">
        </asp:Label>
    </asp:Panel>
</asp:Content>

<asp:Content ID="agregarTerminalPage" ContentPlaceHolderID="contenidoPagina" Runat="Server">
    <asp:Panel ID="Panel2" runat = "server">
    
        <asp:Table ID="Table3" runat = "server">
        <asp:TableRow >
    <asp:TableCell ID="cRadios" runat = "server" ColumnSpan="4" HorizontalAlign="Left">
                    <asp:RadioButtonList runat = "server" RepeatDirection="Horizontal" ID = "rbGroup">
                    <asp:ListItem Text = "Documento" Selected = "true" ></asp:ListItem>
                    <asp:ListItem Text = "Nombre"></asp:ListItem>
                    </asp:RadioButtonList>
    </asp:TableCell>
    </asp:TableRow>
        <asp:TableRow ID="TableRow1" runat = "server">
            <asp:TableCell ID="TableCell1" runat = "server">
                <asp:TextBox  runat = "server" ID = "txbxIngreso" Width = "180px"></asp:TextBox>
            </asp:TableCell>
         <%--   <asp:TableCell ID="cRadios" runat = "server">
                    <asp:RadioButtonList runat = "server" RepeatDirection="Horizontal" ID = "rbGroup">
                    <asp:ListItem Text = "Documento" Selected = "true" ></asp:ListItem>
                    <asp:ListItem Text = "Nombre"></asp:ListItem>
                    </asp:RadioButtonList>
            </asp:TableCell>--%>
            <asp:TableCell ColumnSpan="2" ID = "cBtnBusqueda" >
            <asp:Button ID="Button1" Text = "Buscar" runat = "server" OnClick = "btn_buscarUsuario" ToolTip="Si no hay datos, se muestra todos los datos de los usuarios" /> 
         </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
            
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell >
                    <asp:Label runat = "server" Text = "Usuarios"></asp:Label>
                </asp:TableCell>
                
                <asp:TableCell ColumnSpan = "2">
                    <asp:Label ID="Label1" runat = "server" Text = "Terminales Por Asignar"></asp:Label>
                </asp:TableCell>
                
                <asp:TableCell >
                    <asp:Label ID="Label2" runat = "server" Text = "Terminales Asignados"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow>
                <asp:TableCell>
                    <asp:DropDownList ID="lista_Usuarios" runat="server" AutoPostBack= "True" OnSelectedIndexChanged= "cargar_terminales_no_asignados" Width ="180px">
                    </asp:DropDownList>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:ListBox ID="lista_Atm_no_asignados" runat="server" AutoPostBack="True" OnSelectedIndexChanged = "seleccionar_terminales_usuario" EnableViewState = "true" Width = "180px"></asp:ListBox>
                </asp:TableCell>
                 <asp:TableCell>
                 <asp:Table runat="server">
                 <asp:TableRow>
                 <asp:TableCell>
                 <asp:Button ID="btnAgregar" Text= ">" runat="server" OnClick="cargar_guardar_terminales_asignados"/> 
                 </asp:TableCell>
                 <asp:TableCell>
                 <asp:Button ID="Button2" Text= ">>" runat="server" OnClick="agregar_terminales_usuario_todos"/> 
                 </asp:TableCell>
               </asp:TableRow>
                  <asp:TableRow>
                 <asp:TableCell>
                 <asp:Button ID="btnQuitar" Text="<" runat="server" OnClick="quitar_guardar_terminales_asignados"/> 
                 </asp:TableCell>
                  <asp:TableCell>
                 <asp:Button ID="Button3" Text="<<" runat="server" OnClick="quitar_terminales_usuario_todos"/> 
                 </asp:TableCell>
               </asp:TableRow>
                 </asp:Table>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:ListBox ID="lista_Atm_asignados" runat="server" OnSelectedIndexChanged = "quitar_terminales_ls_usuario" EnableViewState = "true" Width = "180px"></asp:ListBox>
                </asp:TableCell> 
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign = "Center" ID = "cBtnGuardar">
            <asp:Button ID="btnGuardar" Text = "Guardar" runat = "server" OnClick = "btn_guardar_datos"/> 
                </asp:TableCell>
                
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" ID = "cBtnLimpiar">
            <asp:Button ID="BtnLimpiar" Text = "Limpiar" runat = "server" OnClick = "limpiar_datos"/>
                </asp:TableCell>
            
            </asp:TableRow>

        </asp:Table> 
    </asp:Panel>
</asp:Content>

