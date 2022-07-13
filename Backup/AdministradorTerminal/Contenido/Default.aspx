<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AdministradorTerminal._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 192px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="Panel1" runat = "server" style="border: 2px solid #000">
        <asp:Table runat = "server" ID="TablaOPcion">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Image ID = "imagenPrincipal" runat = "server" 
                    ImageUrl = "~/Imagenes/sis_principal.png" ImageAlign="Middle" 
                    Width="30%"/>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </asp:Panel>
    <div style="text-align: center">
    
        <asp:Label ID="lblTitulo" runat="server" Text="Administrador de Terminal" 
            style="text-align: center; font-size: xx-large; color: #0033CC"></asp:Label>
    
    </div>
    <div>
    </div>
    <div>
    </div>
    <div style="text-align: center">
    
    <table border = "4" 
            style="text-align : center; margin : 0 auto;"  >
        <tr align ="center">
            <td class="style1">
                <asp:Image ID="imagenLogin" runat="server" Height="160px" ImageAlign="Middle" 
                    ImageUrl="~/Imagenes/acceso_sistema.jpg" Width="180px" />
            </td>
            <td>
                <asp:Login ID="LoginUsuario" runat="server" FailureText="">
                    <LayoutTemplate>
                        <table border="0" cellpadding="1" cellspacing="0" 
                            style="border-collapse:collapse;">
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0">
                                        <tr>
                                            <td align="center" colspan="2">
                                                Iniciar sesión</td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Nombre de usuario:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                                    ControlToValidate="UserName" 
                                                    ErrorMessage="El nombre de usuario es obligatorio." 
                                                    ToolTip="El nombre de usuario es obligatorio." 
                                                    ValidationGroup="LoginUsuario">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Contraseña:</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                                    ControlToValidate="Password" ErrorMessage="La contraseña es obligatoria."
                                                    ToolTip="La contraseña es obligatoria." ValidationGroup="LoginUsuario">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:CheckBox ID="RememberMe" runat="server" 
                                                    Text="Recordármelo la próxima vez." />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="2" style="color:Red;">
                                                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="2">
                                                <asp:Button ID="LoginButton" runat="server" CommandName="Login" 
                                                    onclick="LoginButton_Click" Text="Inicio de sesión" 
                                                    ValidationGroup="LoginUsuario" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:Login>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
