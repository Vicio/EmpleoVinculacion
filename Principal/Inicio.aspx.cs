using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSetFuncsTableAdapters;
using System.Data;
using DataSetProcsTableAdapters;

public partial class Principal_Inicio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["IDVacanteEditada"] = null;
        SQLInjects inject = new SQLInjects();
        buscar_datos_inicioTableAdapter datosBusqueda = new buscar_datos_inicioTableAdapter();
        GridViewVacantes.DataSourceID = null;
        GridViewVacantes.DataSource = datosBusqueda.GetData(inject.Remover(TextBoxBusqueda.Text));
        GridViewVacantes.DataBind();
    }
    private void MergeRows(GridView gridView)
    {
        for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow row = gridView.Rows[rowIndex];
            GridViewRow previousRow = gridView.Rows[rowIndex + 1];

            for (int i = 0; i < row.Cells.Count; i++)
            {

                try
                {
                    if (((CheckBox)row.Cells[i].Controls[0]).Checked == ((CheckBox)previousRow.Cells[i].Controls[0]).Checked)
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 : previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }
                }
                catch (ArgumentOutOfRangeException err)
                {
                    if (row.Cells[i].Text == previousRow.Cells[i].Text)
                    {
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 : previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;
                    }
                }

            }
        }
    }
    protected void GridViewVacantes_PreRender(object sender, EventArgs e)
    {
    }
    protected void GridViewVacantes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName.Equals("VerDetalle"))
        {
            divDetalleVacantes.Visible = true;
            divVacantes.Visible = false;
            int indice = Convert.ToInt32(e.CommandArgument);

            GridViewRow fila = GridViewVacantes.Rows[indice];

            int idVacante = Convert.ToInt32(fila.Cells[0].Text);

            obtener_detalle_vacanteTableAdapter vacante = new obtener_detalle_vacanteTableAdapter();

            DataSetFuncs.obtener_detalle_vacanteDataTable tablaVacante = vacante.GetData(idVacante);

            labelID.InnerHtml = "ID: " + tablaVacante[0].id_vacante + "<br /><br />";

            labelNombreVacante.InnerHtml = "Nombre de la vacante: " + tablaVacante[0].nombre_vacante + "<br /><br />";

            labelNivelEstudio.InnerHtml = "Nivel de estudios requerido: " + tablaVacante[0].nivel_estudio + "<br /><br />";

            labelEdadRequerida.InnerHtml = "Edad requerida: " + tablaVacante[0].edad_requerida + " años<br /><br />";

            labelGenero.InnerHtml = "Sexo requerido: " + tablaVacante[0].genero + "<br /><br />";

            labelHorario.InnerHtml = "Horario: " + tablaVacante[0].horario + "<br /><br />";

            labelDias.InnerHtml = "Días: " + tablaVacante[0].dias + "<br /><br />";

            labelExperiencia.InnerHtml = "Experiencia mínima requerida:<br />" + tablaVacante[0].experiencia + "<br /><br />";

            labelActividades.InnerHtml = "Actividades a realizar:<br />" + tablaVacante[0].actividades + "<br /><br />";

            labelHabilidades.InnerHtml = "Habilidades requeridas:<br />" + tablaVacante[0].habilidades_requeridas + "<br /><br />";

            labelOtrosRequisitos.InnerHtml = "Otros requisitos adicionales:<br />" + tablaVacante[0].otros_requisitos + "<br /><br />";

            labelVigencia.InnerHtml = "Vigencia de la vacante: " + tablaVacante[0].vigencia.ToLongDateString() + "<br /><br />";

        }
    }
    protected void ButtonRegresar_Click(object sender, EventArgs e)
    {
        divDetalleVacantes.Visible = false;
        divVacantes.Visible = true;
    }
    protected void ButtonBusqueda_Click(object sender, EventArgs e)
    {
        BuscarDatos();
    }

    protected void BuscarDatos()
    {
        SQLInjects inject = new SQLInjects();

        buscar_datos_inicioTableAdapter datosBusqueda = new buscar_datos_inicioTableAdapter();
        DataSetFuncs.buscar_datosDataTable datos = new DataSetFuncs.buscar_datosDataTable();

        GridViewVacantes.DataSourceID = null;
        string[] palabrasClave = TextBoxBusqueda.Text.Split(' ');
        GridViewVacantes.DataSource = "";
        foreach (string palabraClave in palabrasClave)
        {
            datos.Merge(datosBusqueda.GetData(inject.Remover(palabraClave)));
        }

        bool[] palabrasEncontradas = new bool[palabrasClave.Length];


        for (int j = 0; j < datos.Rows.Count; j++)
        {
            for (int i = 0; i < palabrasClave.Count(); i++)
            {
                palabrasEncontradas[i] = false;
                for (int k = 0; k < datos.Columns.Count; k++)
                {
                    if (datos[j][k].ToString().ToLower().Contains(palabrasClave[i].ToLower()))
                    {
                        palabrasEncontradas[i] = true;
                    }
                }
            }

            for (int i = 0; i < palabrasEncontradas.Length; i++)
            {
                if (palabrasEncontradas[i] == false)
                    datos.Rows[j].Delete();
            }
        }

        DataTable temporalDatos = datos.DefaultView.ToTable(true);

        GridViewVacantes.DataSource = temporalDatos;
        GridViewVacantes.DataBind();

    }
    protected void GridViewBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("PublicarVacante"))
        {
            ProcsTableAdapter procesos = new ProcsTableAdapter();

            int indice = Convert.ToInt32(e.CommandArgument);

            GridViewRow fila = GridViewVacantes.Rows[indice];

            int idVacante = Convert.ToInt32(fila.Cells[0].Text);
            procesos.publicar_vacante(idVacante);

            SQLInjects inject = new SQLInjects();

            buscar_datos_inicioTableAdapter datosBusqueda = new buscar_datos_inicioTableAdapter();

            GridViewVacantes.DataSourceID = null;
            GridViewVacantes.DataSource = datosBusqueda.GetData(inject.Remover(TextBoxBusqueda.Text));
            GridViewVacantes.DataBind();
        }

        if (e.CommandName.Equals("OcultarVacante"))
        {
            ProcsTableAdapter procesos = new ProcsTableAdapter();

            int indice = Convert.ToInt32(e.CommandArgument);

            GridViewRow fila = GridViewVacantes.Rows[indice];

            int idVacante = Convert.ToInt32(fila.Cells[0].Text);
            procesos.ocultar_vacante(idVacante);

            SQLInjects inject = new SQLInjects();

            buscar_datos_inicioTableAdapter datosBusqueda = new buscar_datos_inicioTableAdapter();

            GridViewVacantes.DataSourceID = null;
            GridViewVacantes.DataSource = datosBusqueda.GetData(inject.Remover(TextBoxBusqueda.Text));
            GridViewVacantes.DataBind();
        }
        if (e.CommandName.Equals("ModificarVacante"))
        {
            int indice = Convert.ToInt32(e.CommandArgument);

            GridViewRow fila = GridViewVacantes.Rows[indice];

            Session["IDVacanteEditada"] = fila.Cells[0].Text;
            Response.Redirect("Agregar.aspx");
        }
    }
    protected void GridViewBusqueda_Sorting(object sender, GridViewSortEventArgs e)
    {
        SQLInjects inject = new SQLInjects();

        buscar_datos_inicioTableAdapter datosBusqueda = new buscar_datos_inicioTableAdapter();

        GridViewVacantes.DataSourceID = null;
        DataTable tabla = datosBusqueda.GetData(inject.Remover(TextBoxBusqueda.Text));
        tabla.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
        GridViewVacantes.DataSource = tabla;
        GridViewVacantes.DataBind();

    }
    private string GetSortDirection(string column)
    {

        // By default, set the sort direction to ascending.
        string sortDirection = "ASC";

        // Retrieve the last column that was sorted.
        string sortExpression = ViewState["SortExpression"] as string;

        if (sortExpression != null)
        {
            // Check if the same column is being sorted.
            // Otherwise, the default value can be returned.
            if (sortExpression == column)
            {
                string lastDirection = ViewState["SortDirection"] as string;
                if ((lastDirection != null) && (lastDirection == "ASC"))
                {
                    sortDirection = "DESC";
                }
            }
        }

        // Save new values in ViewState.
        ViewState["SortDirection"] = sortDirection;
        ViewState["SortExpression"] = column;

        return sortDirection;
    }
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
}