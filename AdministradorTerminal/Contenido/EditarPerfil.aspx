<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile ="~/Contenido/MenuUsuario.Master"     CodeBehind="EditarPerfil.aspx.cs" Inherits="AdministradorTerminal.Contenido.EditarPerfil" %>


<asp:Content ID = "titlo" ContentPlaceHolderID = "tituloPagina" runat =  "server">
    <asp:Panel ID = "panelTitulo" runat = "server" style = "text-align : center">
        <asp:Label ID = "lbltitulo" runat = "server" Text = "EDITAR PERFIL" Font-Size="X-Large" style = "color: #0033CC">
        </asp:Label>
    </asp:Panel>
</asp:Content>
<asp:Content ID="agregarPerfilPage" ContentPlaceHolderID="contenidoPagina" Runat="Server">
    <asp:ScriptManager ID="adminTareasAjax" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="panelTareaUpdateAjajex" runat="server">
    <ContentTemplate>
    <asp:Panel ID = "panelDatosPerfil" runat = "server" >
       <asp:Table ID = "tablaMuestra" runat = "server" Width = "70%" BorderStyle = "Dotted" BorderWidth ="1px">
        <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID = "labelModelo" Text = "Nombre Perfil :" runat = "server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID = "cboxPerfiles" runat = "server" OnSelectedIndexChanged = "Seleccion_perfil" AutoPostBack="True"></asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
       
       </asp:Table>
       <asp:Table ID = "tablaContenido" runat = "server" Width = "70%" BorderStyle = "Dotted" BorderWidth ="1px">
        <asp:TableRow ID = "fnombre" style = "border" >
                    <asp:TableCell ID = "cnombrelb" Text = "Nombre :" style = " text-align : left" ></asp:TableCell>
                    <asp:TableCell ID = "cnombretbx">
                    <asp:TextBox ID = "txbxNombrePerfil" runat = "server" Width = "90%"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="txbxNombrePerfilValidador" runat="server" 
                                    ControlToValidate="txbxNombrePerfil" 
                                    ErrorMessage="Campo descripcion es obligatorio." 
                                    ToolTip="Campo descripcion es obligatorio." 
                                    ValidationGroup="panelDatosPerfil">*</asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>
                <asp:TableRow ID = "fDescripcion" style = "border">
                <asp:TableCell ID = "cDescripcionlbl" Text = "Descripción :" style = "text-align : left"></asp:TableCell>
                <asp:TableCell ID = "cDescripcionxbx">
                <asp:TextBox ID = "txbxDescripcion" runat = "server" Width = "90%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="txbxDescripcionValidador" runat="server" 
                                    ControlToValidate="txbxDescripcion" 
                                    ErrorMessage="Campo descripcion es obligatorio." 
                                    ToolTip="Campo descripcion es obligatorio." 
                                    ValidationGroup="panelDatosPerfil">*</asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ColumnSpan = "2">
                <asp:Label ID="FailureText" runat="server" EnableViewState="False"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
   </asp:Table>         
     </asp:Panel>
     <div>
     <br />
     </div>
    <asp:Panel ID = "panelmenu" runat = "server" >
       <asp:GridView ID="permisoUsuario" runat="server" BackColor="White" 
            BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            AutoGenerateColumns="False" Width = "80%"
            OnRowEditing="RowEditing" OnRowCancelingEdit="RowCancelingEdit"
            OnRowUpdating="RowUpdating">
       
           <FooterStyle BackColor="White" ForeColor="#000000" />
           <RowStyle ForeColor="#000000" />
           <Columns>
               <asp:TemplateField AccessibleHeaderText="menuPadre" HeaderText="Menu Padre" 
                   Visible="False">
                   <ItemStyle HorizontalAlign="Left" />
               </asp:TemplateField>
               <asp:BoundField HeaderText="Menu Padre" DataField="menuPadre">
               </asp:BoundField>
               <asp:BoundField DataField="idMenuOPciones" HeaderText="# Perfil" />
               <asp:BoundField DataField="nombreMenu" HeaderText="Nombre Menu">
                   <HeaderStyle HorizontalAlign="Left" />
               </asp:BoundField>
               <asp:CheckBoxField HeaderText="Seleccionado" DataField="isMenuActivo"/>
               <asp:CommandField HeaderText="Edicion" ShowEditButton="True" />
           </Columns>
           <PagerStyle BackColor="White" ForeColor="#4E4545" HorizontalAlign="Left" />
           <SelectedRowStyle BackColor="#4E4545" Font-Bold="True" ForeColor="White" />
           <HeaderStyle BackColor="#4E4545" Font-Bold="True" ForeColor="White" />
       
       </asp:GridView>
       </asp:Panel>
      
       <asp:Panel ID = "panelBoton" runat = "server">
      <asp:table ID="Tabledatos" runat = "server">
      <asp:TableRow ID = "fOpciones" style = "border">
                <asp:TableCell ID = "cGuardar" style = "text-align : right">
                <asp:Button ID = "btnGuardar" runat = "server" Text = "Guardar" Width = "100px" OnClick="btn_guardar_datos" ValidationGroup="panelDatosPerfil"/>
                </asp:TableCell>
                <asp:TableCell ID = "cLimpiar" style = "text-align : left">
                <asp:Button ID = "btnLimpiar" runat = "server" Text = "Limpiar" Width = "100px" OnClick="btn_limpiar_datos"/>
                </asp:TableCell>
       </asp:TableRow> 
 </asp:table>
 
 </asp:Panel>
 </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdateProgress ID = "barraProgreso" runat ="server" >
<ProgressTemplate>
    <asp:Image Id = "imEspera" runat = "server" ImageUrl= "~/Imagenes/procesando.gif"/>
</ProgressTemplate>
</asp:UpdateProgress>
</asp:Content>


