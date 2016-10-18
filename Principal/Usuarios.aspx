<%@ Page Title="" Language="C#" MasterPageFile="~/Principal/MasterPage.master" AutoEventWireup="true" CodeFile="Usuarios.aspx.cs" Inherits="Principal_Usuarios" %>

<asp:Content ID="Contentido" ContentPlaceHolderID="contenidoCuerpo" Runat="Server">
    <asp:ScriptManager ID="scriptActualizacion" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="panelActualizacion" runat="server">
        <ContentTemplate>
            <div runat="server" id="divUsuarios" class="divTablas">
                <asp:GridView ID="GridViewUsuarios" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="datosUsuarios" CssClass="tablas" HeaderStyle-CssClass="tablasEncabezados" OnRowCommand="GridViewUsuarios_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                        <asp:BoundField DataField="Correo" HeaderText="Correo" SortExpression="Correo" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="datosUsuarios" runat="server" ConnectionString="<%$ ConnectionStrings:empleoConnectionString %>" SelectCommand="SELECT * FROM [vista_usuarios]"></asp:SqlDataSource>
                <asp:Button runat="server" ID="ButtonAgregarUsuario" CssClass="botones" OnClick="ButtonAgregarUsuario_Click" Text="Agregar Usuario" />
                <br />
                <label runat="server" id="labelMensaje" style="color:red;"></label>
            </div>
            <div runat="server" id="divModificacionUsuarios" visible="false" class="divModificacion">
                <asp:Label runat="server" ID="labelTextoID" Visible="false">ID:</asp:Label>
                <asp:Label runat="server" ID="labelID"></asp:Label>
                <br />
                <label>Nombre:</label>
                <br />
                <asp:TextBox runat="server" ID="TextBoxNombre" CssClass="cajasTexto"></asp:TextBox>
                <br />
                <br />
                <label runat="server" id="labelCorreo">Correo Electrónico:</label>
                <br />
                <asp:TextBox runat="server" ID="TextBoxCorreo" CssClass="cajasTexto"></asp:TextBox>
                <br />
                <br />
                <label runat="server" id="labelMensajeModificacion" style="color:red;"></label>
                <br />
                <asp:Button runat="server" ID="ButtonAgregar" Text="Aceptar" OnClick="ButtonAgregar_Click" CssClass="botones"/>
                <asp:Button runat="server" ID="ButtonCancelar" Text="Cancelar" OnClick="ButtonCancelar_Click" CssClass="botones"/>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

