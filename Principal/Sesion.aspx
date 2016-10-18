<%@ Page Title="" Language="C#" MasterPageFile="~/Principal/MasterPage.master" AutoEventWireup="true" CodeFile="Sesion.aspx.cs" Inherits="Principal_Sesion" %>

<asp:Content ID="Contentido" ContentPlaceHolderID="contenidoCuerpo" Runat="Server">
    <asp:ScriptManager ID="scriptActualizacion" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="panelActualizacion" runat="server">
        <ContentTemplate>
            <div runat="server" id="divAcceso" class="divModificacion">
                <asp:Label id="textoError" runat="server" style="color:red"></asp:Label>
                <br />
                <label>Correo:</label>
                <br />
                <asp:TextBox CssClass="cajasTexto" ID="TextBoxCorreo" runat="server"></asp:TextBox>
                <br />
                <label>Contraseña:</label>
                <br />
                <asp:TextBox CssClass="cajasTexto" TextMode="Password" ID="TextBoxPassword" runat="server"></asp:TextBox>
                <br />
                <asp:LinkButton runat="server" ID="LinkButtonOlvidePassword" Text="Olvidé la contraseña" OnClick="LinkButtonOlvidePassword_Click"></asp:LinkButton>
                <br />
                <br />
                <asp:Button CssClass="botones" ID="buttonAceptar" Text="Aceptar" runat="server" OnClick="buttonAceptar_Click" />
            </div>
            <div runat="server" id="divPass" visible="false" class="divModificacion">
                <label>Escriba su correo, le llegará una notificación:</label>
                <br />
                <asp:Label id="LabelMensaje" runat="server" style="color:red"></asp:Label>
                <asp:TextBox CssClass="cajasTexto" ID="TextBoxCorreoVerificacion" runat="server"></asp:TextBox>
                <br />
                <asp:Button CssClass="botones" ID="buttonCambiarPass" Text="Aceptar" runat="server" OnClick="buttonCambiarPass_Click" />
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

