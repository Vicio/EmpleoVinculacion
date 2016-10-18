<%@ Page Title="" Language="C#" MasterPageFile="~/Principal/MasterPage.master" AutoEventWireup="true" CodeFile="Busqueda.aspx.cs" Inherits="Principal_Busqueda" EnableEventValidation="false" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="contenidoCuerpo" Runat="Server">
    <asp:ScriptManager ID="scriptActualizacion" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="panelActualizacion" runat="server">
        <ContentTemplate>
            <div class="divTablas">

                <asp:TextBox runat="server" ID="TextBoxBusqueda" CssClass="cajasTexto"></asp:TextBox>
                <asp:Button runat="server" ID="ButtonBusqueda" CssClass="botones" Text="Buscar" OnClick="ButtonBusqueda_Click" />
                <asp:Button runat="server" ID="ButtonExportarExcel" CssClass="botones" Text="Exportar a Excel" OnClick="ButtonExportarExcel_Click" />
                <asp:GridView runat="server" ID="GridViewBusqueda" AutoGenerateColumns="False" CssClass="tablas" HeaderStyle-CssClass="tablasEncabezados" DataKeyNames="ID" AllowSorting="true" OnSorting="GridViewBusqueda_Sorting" DataSourceID="datosBusqueda" OnRowCommand="GridViewBusqueda_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
                        <asp:BoundField DataField="Nombre de la vacante" HeaderText="Nombre de la vacante" SortExpression="Nombre de la vacante" />
                        <asp:BoundField DataField="Nivel de estudios" HeaderText="Nivel de estudios" SortExpression="Nivel de estudios" />
                        <asp:BoundField DataField="Edad Requerida" HeaderText="Edad Requerida" ReadOnly="True" SortExpression="Edad Requerida" />
                        <asp:BoundField DataField="Género" HeaderText="Género" ReadOnly="True" SortExpression="Género" />
                        <asp:BoundField DataField="Horario" HeaderText="Horario" ReadOnly="True" SortExpression="Horario" />
                        <asp:BoundField DataField="Vigencia" DataFormatString="{0:d}" HeaderText="Vigencia" SortExpression="Vigencia" />
                        <asp:BoundField DataField="Fecha de alta" DataFormatString="{0:d}" HeaderText="Fecha de alta" SortExpression="Fecha de alta" />
                        <asp:CheckBoxField DataField="Publicada" HeaderText="Publicada" SortExpression="Publicada" />
                        <asp:ButtonField ControlStyle-CssClass="botonesTablas" ButtonType="Button" HeaderText="" CommandName="ModificarVacante" Text="Editar" />
                    </Columns>

                </asp:GridView>
                <asp:SqlDataSource ID="datosBusqueda" runat="server" ConnectionString="<%$ ConnectionStrings:empleoConnectionString %>"  SelectCommand="SELECT * FROM [vista_vacantes_administradores]"></asp:SqlDataSource>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ButtonExportarExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

