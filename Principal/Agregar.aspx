<%@ Page Title="" Language="C#" MasterPageFile="~/Principal/MasterPage.master" AutoEventWireup="true" CodeFile="Agregar.aspx.cs" Inherits="Principal_Agregar" %>

<asp:Content ID="Contenido" ContentPlaceHolderID="contenidoCuerpo" Runat="Server">
    <asp:ScriptManager ID="scriptActualizacion" EnablePartialRendering="true" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="panelActualizacion" runat="server">
        <ContentTemplate>
            <div runat="server" id="divAgregarVacantes" class="divModificacion">
                <label runat="server" id="labelIDVacante" visible="false">ID: </label>
                <label runat="server" id="labelIDNumVacante" visible="false"></label>
                <br />
                <label>Nombre de la vacante:</label>
                <br />
                <asp:TextBox runat="server" ID="TextBoxNombreVacante" CssClass="cajasTexto"></asp:TextBox>
                <br />
                <label>Nivel de estudios requerido:</label>
                <br />
                <asp:DropDownList runat="server" ID="DropDownListEstudios" CssClass="listas" DataSourceID="datosEstudios" DataTextField="Nivel de estudios" DataValueField="ID"></asp:DropDownList>
                <asp:SqlDataSource ID="datosEstudios" runat="server" ConnectionString="<%$ ConnectionStrings:empleoConnectionString %>" SelectCommand="SELECT * FROM [vista_estudios]"></asp:SqlDataSource>
                <br />
                <asp:Button runat="server" ID="ButtonAgregarNivelEstudios" CssClass="botones" Text="Agregar" OnClick="ButtonAgregarNivelEstudios_Click" />
                <br />
                <asp:TextBox runat="server" ID="TextboxAgregarNivelEstudios" CssClass="cajasTexto" Visible="false" ></asp:TextBox>
                <asp:Button runat="server" ID="ButtonAceptarAgregarNivelEstudios" CssClass="botones" Text="Aceptar" OnClick="ButtonAceptarAgregarNivelEstudios_Click" Visible="false" />
                <br />
                <br />
                <label>Edad Requerida:</label>
                <table>
                    <tr>
                        <td>
                            <label>Mínima:</label>
                            <div style="width:40%;">
                                <asp:TextBox runat="server" ID="TextBoxEdadMinima" ValidationGroup="edadMinima" Text="25" CssClass="cajasTextoNumerico" ReadOnly="true"></asp:TextBox>
                                <asp:Button runat="server" ID="ButtonSubirEdadMinima" ValidationGroup="edadMinima" CssClass="botonesFlechas" Text="^" OnClick="ButtonSubirEdadMinima_Click" />
                                <asp:Button runat="server" ID="ButtonBajarEdadMinima" ValidationGroup="edadMinima" CssClass="botonesFlechas" Text="v" OnClick="ButtonBajarEdadMinima_Click" />
                            </div>
                        </td>
                        <td>
                            <label>Máxima:</label>
                            <div style="width:40%;">
                                <asp:TextBox runat="server" ID="TextBoxEdadMaxima" ValidationGroup="edadMaxima" Text="30" CssClass="cajasTextoNumerico" ReadOnly="true"></asp:TextBox>
                                <asp:Button runat="server" ID="ButtonSubirEdadMaxima" ValidationGroup="edadMaxima" CssClass="botonesFlechas" Text="^" OnClick="ButtonSubirEdadMaxima_Click" />
                                <asp:Button runat="server" ID="ButtonBajarEdadMaxima" ValidationGroup="edadMaxima" CssClass="botonesFlechas" Text="v" OnClick="ButtonBajarEdadMaxima_Click" />
                            </div>
                        </td>
                    </tr>
                </table>
                <br />
                <label>Género:</label>
                <asp:RadioButtonList runat="server" RepeatLayout="Table" RepeatDirection="Horizontal" ID="RadioButtonGenero">
                    <asp:ListItem Value="F" Text="Femenino" Selected="True">
                    </asp:ListItem>
                    <asp:ListItem Value="M" Text="Masculino">
                    </asp:ListItem>
                    <asp:ListItem Value="I" Text="Indistinto">
                    </asp:ListItem>
                </asp:RadioButtonList>
                <br />
                <label>Horario:</label>
                <table>
                    <tr>
                        <td>
                            <label>Entrada:</label>
                            <div style="width:80%;">
                                <asp:TextBox runat="server" ID="TextBoxHoraEntrada" ValidationGroup="horaEntrada" CssClass="cajasTextoNumerico" ReadOnly="true" Text="8:00 AM"></asp:TextBox>
                                <asp:Button runat="server" ID="ButtonSubirHoraEntrada" ValidationGroup="horaEntrada" CssClass="botonesFlechas" Text="^" OnClick="ButtonSubirHoraEntrada_Click" />
                                <asp:Button runat="server" ID="ButtonBajarHoraEntrada" ValidationGroup="horaEntrada" CssClass="botonesFlechas" Text="v" OnClick="ButtonBajarHoraEntrada_Click" />
                            </div>
                        </td>
                        <td>
                            <label>Salida:</label>
                            <div style="width:80%;">
                                <asp:TextBox runat="server" ID="TextBoxHoraSalida" ValidationGroup="horaSalida" CssClass="cajasTextoNumerico" ReadOnly="true" Text="4:00 PM"></asp:TextBox>
                                <asp:Button runat="server" ID="ButtonSubirHoraSalida" ValidationGroup="horaSalida" CssClass="botonesFlechas" Text="^" OnClick="ButtonSubirHoraSalida_Click" />
                                <asp:Button runat="server" ID="ButtonBajarHoraSalida" ValidationGroup="horaSalida" CssClass="botonesFlechas" Text="v" OnClick="ButtonBajarHoraSalida_Click" />
                            </div>
                        </td>
                        <td>
                            <label>Días:</label>
                            <br />
                            <asp:CheckBoxList RepeatLayout="Table" RepeatDirection="Horizontal" ID="CheckBoxListDias" runat="server">
                                <asp:ListItem Text="Lunes" Value="Lunes"></asp:ListItem>
                                <asp:ListItem Text="Martes" Value ="Martes"></asp:ListItem>
                                <asp:ListItem Text="Miércoles" Value="Miércoles"></asp:ListItem>
                                <asp:ListItem Text="Jueves" Value="Jueves"></asp:ListItem>
                                <asp:ListItem Text="Viernes" Value="Viernes"></asp:ListItem>
                                <asp:ListItem Text="Sábado" Value="Sábado"></asp:ListItem>
                                <asp:ListItem Text="Domingo" Value="Domingo"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
                <br />
                <br />
                <label>Experiencia requerida:</label>
                <br />
                <asp:TextBox runat="server" ID="TextBoxExperiencia" TextMode="MultiLine" CssClass="cajasTextoMultilinea"></asp:TextBox>
                <br />
                <label>Actividades a realizar:</label>
                <br />
                <asp:TextBox runat="server" ID="TextBoxActividades" TextMode="MultiLine" CssClass="cajasTextoMultilinea"></asp:TextBox>
                <br />
                <label>Habilidades requeridas:</label>
                <br />
                <asp:TextBox runat="server" ID="TextBoxHabilidades" TextMode="MultiLine" CssClass="cajasTextoMultilinea"></asp:TextBox>
                <br />
                <label>Otros Requerimientos:</label>
                <br />
                <asp:TextBox runat="server" ID="TextBoxOtrosRequerimientos" TextMode="MultiLine" CssClass="cajasTextoMultilinea"></asp:TextBox>
                <br />
                <label>Vigencia:</label>
                <br />
                    <asp:Calendar ID="Calendario" runat="server" Visible="true" WeekendDayStyle-BackColor="#DEDEDE"
                        BorderStyle="None" CellPadding="10" Font-Size="16px" BackColor="#FFFFFF" ShowGridLines="true" CssClass="calendario"
                        DayStyle-BorderColor="#111547" DayStyle-ForeColor="#111547" DayStyle-BorderWidth="2px"
                        DayHeaderStyle-BorderColor="#111547" DayHeaderStyle-ForeColor="#111547" DayHeaderStyle-BorderWidth="2px"
                        TitleStyle-BackColor="#111547" TitleStyle-ForeColor="#FFFFFF" TitleStyle-BorderColor="#111547" TitleStyle-BorderWidth="2px"
                        OtherMonthDayStyle-Font-Size="0px"
                        NextPrevStyle-ForeColor="#FFFFFFF" NextMonthText="Sig" PrevMonthText="Ant"
                        SelectedDayStyle-BackColor="#6cb33f">
                    </asp:Calendar>
                <br />
                <asp:CheckBoxList runat="server" ID="CheckBoxPublicar">
                    <asp:ListItem Text="Publicar Vacante?"></asp:ListItem>
                </asp:CheckBoxList>
                <%--<asp:CheckBox runat="server" ID="CheckBoxPublicar" Text="Publicar Vacante?" />--%>
                <br />
                <asp:Button runat="server" ID="ButtonAgregar" CssClass="botones" Text="Agregar" OnClick="ButtonAgregar_Click" />
                <label runat="server" id="labelMensaje"></label>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

