using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSetProcsTableAdapters;
using DataSetFuncsTableAdapters;

public partial class Principal_Agregar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ID"] == null)
            Response.Redirect("./Inicio.aspx");
        if(Session["IDVacanteEditada"] != null)
        {
            obtener_vacanteTableAdapter vacante = new obtener_vacanteTableAdapter();
            int idVacante = Convert.ToInt32(Session["IDVacanteEditada"].ToString());

            DataSetFuncs.obtener_vacanteDataTable tablaVacantes = vacante.GetData(idVacante);

            labelIDNumVacante.InnerHtml = tablaVacantes[0].id_vacante + "";
            labelIDVacante.Visible = true;
            labelIDNumVacante.Visible = true;

            TextBoxNombreVacante.Text = tablaVacantes[0].nombre_vacante;

            DropDownListEstudios.SelectedValue = tablaVacantes[0].id_estudio + "";

            TextBoxEdadMinima.Text = tablaVacantes[0].edad_minima_requerida + "";
            TextBoxEdadMaxima.Text = tablaVacantes[0].edad_maxima_requerida + "";

            //RadioButtonGenero.SelectedValue = tablaVacantes[0].genero + "";

            DateTime hora = Convert.ToDateTime(tablaVacantes[0].hora_entrada.ToString());
            TextBoxHoraEntrada.Text = hora.ToShortTimeString();

            hora = Convert.ToDateTime(tablaVacantes[0].hora_salida.ToString());
            TextBoxHoraSalida.Text = hora.ToShortTimeString();

            TextBoxExperiencia.Text = tablaVacantes[0].experiencia + "";

            TextBoxActividades.Text = tablaVacantes[0].actividades + "";
            try
            {
                string[] dias = tablaVacantes[0].dias.Split(' ');
                for (int i = 0; i < dias.Length; i++)
                {
                    for (int j = 0; j < CheckBoxListDias.Items.Count; j++)
                    {
                        if (dias[i].Equals(CheckBoxListDias.Items[j].Value))
                            CheckBoxListDias.Items[j].Selected = true;
                    }
                }
            }
            catch(Exception err)
            {

            }


            TextBoxHabilidades.Text = tablaVacantes[0].habilidades_requeridas + "";

            TextBoxOtrosRequerimientos.Text = tablaVacantes[0].otros_requisitos + "";

            Calendario.SelectedDate = tablaVacantes[0].vigencia;

            //CheckBoxPublicar.Items[0].Selected = tablaVacantes[0].publicar;
        }
    }
    protected void ButtonSubirEdadMinima_Click(object sender, EventArgs e)
    {
        int edadMinima = (Convert.ToInt32(TextBoxEdadMinima.Text));
        edadMinima = (edadMinima < Convert.ToInt32(TextBoxEdadMaxima.Text)) ? edadMinima + 1 : edadMinima;
        TextBoxEdadMinima.Text = edadMinima.ToString();
    }
    protected void ButtonBajarEdadMinima_Click(object sender, EventArgs e)
    {
        int edadMinima = Convert.ToInt32(TextBoxEdadMinima.Text);
        edadMinima = (edadMinima > 15) ? edadMinima - 1 : edadMinima;
        TextBoxEdadMinima.Text = edadMinima.ToString();
    }
    protected void ButtonSubirEdadMaxima_Click(object sender, EventArgs e)
    {
        int edadMaxima = Convert.ToInt32(TextBoxEdadMaxima.Text);
        edadMaxima = (edadMaxima < 80) ? edadMaxima + 1 : edadMaxima;
        TextBoxEdadMaxima.Text = edadMaxima.ToString();

    }
    protected void ButtonBajarEdadMaxima_Click(object sender, EventArgs e)
    {
        int edadMaxima = Convert.ToInt32(TextBoxEdadMaxima.Text);
        edadMaxima = (edadMaxima > Convert.ToInt32(TextBoxEdadMinima.Text)) ? edadMaxima - 1 : edadMaxima;
        TextBoxEdadMaxima.Text = edadMaxima.ToString();

    }
    protected void ButtonSubirHoraEntrada_Click(object sender, EventArgs e)
    {
        DateTime hora = Convert.ToDateTime(TextBoxHoraEntrada.Text);
        hora = hora.AddMinutes(15);
        TextBoxHoraEntrada.Text = hora.ToShortTimeString();

    }
    protected void ButtonBajarHoraEntrada_Click(object sender, EventArgs e)
    {
        DateTime hora = Convert.ToDateTime(TextBoxHoraEntrada.Text);
        hora = hora.AddMinutes(-15);
        TextBoxHoraEntrada.Text = hora.ToShortTimeString();
    }
    protected void ButtonSubirHoraSalida_Click(object sender, EventArgs e)
    {
        DateTime hora = Convert.ToDateTime(TextBoxHoraSalida.Text);
        hora = hora.AddMinutes(15);
        TextBoxHoraSalida.Text = hora.ToShortTimeString();
    }
    protected void ButtonBajarHoraSalida_Click(object sender, EventArgs e)
    {
        DateTime hora = Convert.ToDateTime(TextBoxHoraSalida.Text);
        hora = hora.AddMinutes(-15);
        TextBoxHoraSalida.Text = hora.ToShortTimeString();
    }
    protected void ButtonAgregarNivelEstudios_Click(object sender, EventArgs e)
    {
        TextboxAgregarNivelEstudios.Visible = true;
        ButtonAceptarAgregarNivelEstudios.Visible = true;
    }
    protected void ButtonAceptarAgregarNivelEstudios_Click(object sender, EventArgs e)
    {
        ProcsTableAdapter procesos = new ProcsTableAdapter();

        procesos.agregar_nivel_estudio(TextboxAgregarNivelEstudios.Text);
        DropDownListEstudios.DataBind();
        TextboxAgregarNivelEstudios.Visible = false;
        ButtonAceptarAgregarNivelEstudios.Visible = false;
        TextboxAgregarNivelEstudios.Text = "";

    }
    protected void ButtonAgregar_Click(object sender, EventArgs e)
    {
        if(Session["IDVacanteEditada"] == null)
        {
            ProcsTableAdapter procesos = new ProcsTableAdapter();
            try
            {
                string dias="";
                for (int i = 0; i < CheckBoxListDias.Items.Count; i++)
                {
                    if (CheckBoxListDias.Items[i].Selected)
                    {
                        dias += CheckBoxListDias.Items[i].Value + " ";
                    }
                }
                procesos.agregar_vacante
                    (
                        Convert.ToByte(DropDownListEstudios.SelectedValue),
                        TextBoxNombreVacante.Text,
                        Convert.ToByte(TextBoxEdadMinima.Text),
                        Convert.ToByte(TextBoxEdadMaxima.Text),
                        RadioButtonGenero.SelectedValue,
                        DateTime.Parse(TextBoxHoraEntrada.Text).TimeOfDay,
                        DateTime.Parse(TextBoxHoraSalida.Text).TimeOfDay,
                        dias,
                        TextBoxExperiencia.Text,
                        TextBoxActividades.Text,
                        TextBoxHabilidades.Text,
                        TextBoxOtrosRequerimientos.Text,
                        Calendario.SelectedDate,
                        DateTime.Now,
                        CheckBoxPublicar.Items[0].Selected
                    );

                labelMensaje.InnerHtml = "Agregado Correctamente.";
            }
            catch (Exception err)
            {
                labelMensaje.InnerHtml = "Ocurrió un error al agregar el dato." + err.ToString();
            }
        }
        else
        {
           try
            {
                ProcsTableAdapter procesos = new ProcsTableAdapter();
                string dias = "";
                for (int i = 0; i < CheckBoxListDias.Items.Count; i++)
                {
                    if (CheckBoxListDias.Items[i].Selected)
                    {
                        dias += CheckBoxListDias.Items[i].Value + " ";
                    }
                }
                bool publicar = true;
                if (CheckBoxPublicar.Items[0].Selected == false)
                    publicar = false;
                procesos.modificar_vacante
                    (
                        Convert.ToInt32(labelIDNumVacante.InnerText),
                        Convert.ToByte(DropDownListEstudios.SelectedValue),
                        TextBoxNombreVacante.Text,
                        Convert.ToByte(TextBoxEdadMinima.Text),
                        Convert.ToByte(TextBoxEdadMaxima.Text),
                        RadioButtonGenero.SelectedValue,
                        DateTime.Parse(TextBoxHoraEntrada.Text).TimeOfDay,
                        DateTime.Parse(TextBoxHoraSalida.Text).TimeOfDay,
                        dias,
                        TextBoxExperiencia.Text,
                        TextBoxActividades.Text,
                        TextBoxHabilidades.Text,
                        TextBoxOtrosRequerimientos.Text,
                        Calendario.SelectedDate,
                        DateTime.Now,
                        publicar
                    );

                    labelMensaje.InnerHtml = "Editado Correctamente.";
                    Session["Repeticiones"] = 0;
                    Response.Redirect("./Busqueda.aspx");
            }
            catch (Exception err)
            {               
                
                labelMensaje.InnerHtml = "Ocurrió un error al agregar el dato." + err.ToString();
            }
        }
    }
    }
