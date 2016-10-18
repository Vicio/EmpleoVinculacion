using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSetFuncsTableAdapters;
using DataSetProcsTableAdapters;
using System.Data;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class Principal_Busqueda : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ID"] == null)
            Response.Redirect("./Inicio.aspx");
        Session["IDVacanteEditada"] = null;
        SQLInjects inject = new SQLInjects();
        buscar_datosTableAdapter datosBusqueda = new buscar_datosTableAdapter();
        GridViewBusqueda.DataSourceID = null;
        GridViewBusqueda.DataSource = datosBusqueda.GetData(inject.Remover(TextBoxBusqueda.Text));
        GridViewBusqueda.DataBind();
    }
    protected void ButtonBusqueda_Click(object sender, EventArgs e)
    {
        BuscarDatos();
    }

    protected void BuscarDatos()
    {
        SQLInjects inject = new SQLInjects();

        buscar_datosTableAdapter datosBusqueda = new buscar_datosTableAdapter();
        DataSetFuncs.buscar_datosDataTable datos = new DataSetFuncs.buscar_datosDataTable();

        GridViewBusqueda.DataSourceID = null;
        string[] palabrasClave = TextBoxBusqueda.Text.Split(' ');
        GridViewBusqueda.DataSource = "";
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

        GridViewBusqueda.DataSource = temporalDatos;
        GridViewBusqueda.DataBind();

    }
    protected void GridViewBusqueda_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName.Equals("PublicarVacante"))
        {
            ProcsTableAdapter procesos = new ProcsTableAdapter();

            int indice = Convert.ToInt32(e.CommandArgument);
            
            GridViewRow fila = GridViewBusqueda.Rows[indice];
            
            int idVacante = Convert.ToInt32(fila.Cells[0].Text);
            procesos.publicar_vacante(idVacante);

            SQLInjects inject = new SQLInjects();

            buscar_datosTableAdapter datosBusqueda = new buscar_datosTableAdapter();

            GridViewBusqueda.DataSourceID = null;
            GridViewBusqueda.DataSource = datosBusqueda.GetData(inject.Remover(TextBoxBusqueda.Text));
            GridViewBusqueda.DataBind();
        }

        if(e.CommandName.Equals("OcultarVacante"))
        {
            ProcsTableAdapter procesos = new ProcsTableAdapter();
            
            int indice = Convert.ToInt32(e.CommandArgument);
            
            GridViewRow fila = GridViewBusqueda.Rows[indice];
            
            int idVacante = Convert.ToInt32(fila.Cells[0].Text);
            procesos.ocultar_vacante(idVacante);

            SQLInjects inject = new SQLInjects();

            buscar_datosTableAdapter datosBusqueda = new buscar_datosTableAdapter();

            GridViewBusqueda.DataSourceID = null;
            GridViewBusqueda.DataSource = datosBusqueda.GetData(inject.Remover(TextBoxBusqueda.Text));
            GridViewBusqueda.DataBind();
        }
        if(e.CommandName.Equals("ModificarVacante"))
        {
            int indice = Convert.ToInt32(e.CommandArgument);
            
            GridViewRow fila = GridViewBusqueda.Rows[indice];
            
            Session["IDVacanteEditada"] = fila.Cells[0].Text;
            Response.Redirect("Agregar.aspx");
        }
    }
    protected void GridViewBusqueda_Sorting(object sender, GridViewSortEventArgs e)
    {
        SQLInjects inject = new SQLInjects();

        buscar_datosTableAdapter datosBusqueda = new buscar_datosTableAdapter();

        GridViewBusqueda.DataSourceID = null;
        DataTable tabla = datosBusqueda.GetData(inject.Remover(TextBoxBusqueda.Text));
        tabla.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
        GridViewBusqueda.DataSource = tabla;
        GridViewBusqueda.DataBind();
        
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
    protected void ButtonExportarExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string NombreArchivo = "Reporte" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + NombreArchivo);
        BuscarDatos();
        GridViewBusqueda.Columns[8].Visible = false;
        GridViewBusqueda.Columns[9].Visible = false;
        GridViewBusqueda.AllowPaging = false;
        GridViewBusqueda.AllowSorting = false;
        GridViewBusqueda.DataBind();
        GridViewBusqueda.GridLines = GridLines.Both;
        GridViewBusqueda.HeaderStyle.Font.Bold = true;
        GridViewBusqueda.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End(); 
        

    }
}