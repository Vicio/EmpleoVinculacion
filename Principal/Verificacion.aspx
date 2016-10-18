<%@ Page Title="" Language="C#" MasterPageFile="~/Principal/MasterPage.master" AutoEventWireup="true" CodeFile="Verificacion.aspx.cs" Inherits="Principal_Verificacion" %>

<asp:Content ID="Contentido" ContentPlaceHolderID="contenidoCuerpo" Runat="Server">
    <asp:ScriptManager ID="scriptActualizacion" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="panelActualizacion" runat="server">
        <ContentTemplate>
            <div class="divModificacion">
                <p runat="server" id="mensaje"></p>
                <div runat="server" id="divCambioPass" style="width:40%; margin-left:30%;" visible="false">
                    <label>Contraseña:</label>
                    <br />
                    <asp:TextBox runat="server" ID="TextBoxPassword" CssClass="cajasTexto" TextMode="Password" />
                    <br />
                    <label>Repetir Contraseña:</label>
                    <br />
                    <asp:TextBox runat="server" ID="TextBoxConfirmarPassword" CssClass="cajasTexto" TextMode="Password" />
                    <br />
                    <asp:Button runat="server" ID="ButtonConfirmar" Text="Confirmar" OnClick="ButtonConfirmar_Click" CssClass="botones" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

