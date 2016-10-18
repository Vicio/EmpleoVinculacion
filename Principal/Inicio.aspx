<%@ Page Title="" Language="C#" MasterPageFile="~/Principal/MasterPage.master" AutoEventWireup="true" CodeFile="Inicio.aspx.cs" Inherits="Principal_Inicio" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="contenidoCuerpo" Runat="Server">
    <asp:ScriptManager ID="scriptActualizacion" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="panelActualizacion" runat="server">
        <ContentTemplate>
            <div runat="server" id="divMensaje" class="divModificacionMensaje">
                En caso de estar interesado (a) en algún puesto, te solicitamos enviar a la dirección <a href="mailto:empleo@uaq.mx">empleo@uaq.mx</a> ,
                el número de la vacante anexando el puesto de interés y tu curriculum de no más de 2 páginas y preferentemente en español e inglés .<br />
                Anota de favor en Asunto : Puesto y el numero de vacante.<br />
                Agradecemos tu atención, para cualquier duda llamar al 1921200 ext. 7026 ó 3581
            </div>
            <div runat="server" id="divVacantes" class="divTablas">
                <asp:TextBox runat="server" ID="TextBoxBusqueda" CssClass="cajasTexto"></asp:TextBox>
                <asp:Button runat="server" ID="ButtonBusqueda" CssClass="botones" Text="Buscar" OnClick="ButtonBusqueda_Click" />
                <asp:GridView runat="server" ID="GridViewVacantes" OnPreRender="GridViewVacantes_PreRender" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="datosVacantes" OnRowCommand="GridViewVacantes_RowCommand" CssClass="tablas" HeaderStyle-CssClass="tablasEncabezados" AllowPaging="false" OnSorting="GridViewBusqueda_Sorting" AllowSorting="True">
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
                        <asp:BoundField DataField="Nombre de la vacante" HeaderText="Nombre de la vacante" SortExpression="Nombre de la vacante" />
                        <asp:BoundField DataField="Nivel de estudios" HeaderText="Nivel de estudios" SortExpression="Nivel de estudios" />
                        <asp:BoundField DataField="Edad Requerida" HeaderText="Edad Requerida" ReadOnly="True" SortExpression="Edad Requerida" />
                        <asp:BoundField DataField="Género" HeaderText="Género" SortExpression="Género" />
                        <asp:BoundField DataField="Horario" HeaderText="Horario" ReadOnly="True" SortExpression="Horario" />
                        <asp:BoundField DataField="Vigencia" DataFormatString="{0:d}" HeaderText="Vigencia" SortExpression="Vigencia" />
                        <asp:ButtonField ControlStyle-CssClass="botonesTablas" ButtonType="Button" HeaderText="" CommandName="VerDetalle" Text="Ver Detalle" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="datosVacantes" runat="server" ConnectionString="<%$ ConnectionStrings:empleoConnectionString %>" SelectCommand="SELECT * FROM [vista_vacantes]"></asp:SqlDataSource>
            </div>
            <div runat="server" id="divDetalleVacantes" class="divModificacion" visible="false">
                <label runat="server" id="labelID"></label>
                <label runat="server" id="labelNombreVacante"></label>
                <label runat="server" id="labelNivelEstudio"></label>
                <label runat="server" id="labelEdadRequerida"></label>
                <label runat="server" id="labelGenero"></label>
                <label runat="server" id="labelHorario"></label>
                <label runat="server" id="labelDias"></label>
                <label runat="server" id="labelExperiencia"></label>
                <label runat="server" id="labelActividades"></label>
                <label runat="server" id="labelHabilidades"></label>
                <label runat="server" id="labelOtrosRequisitos"></label>
                <label runat="server" id="labelVigencia"></label>
                <label runat="server" id="labelFechaAlta"></label>
                <asp:Button runat="server" ID="ButtonRegresar" Text="Regresar" OnClick="ButtonRegresar_Click" CssClass="botones" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GridViewVacantes" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

