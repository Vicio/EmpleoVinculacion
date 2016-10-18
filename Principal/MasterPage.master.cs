using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ID"] != null)
        {
            linkCierreSesion.Visible = true;
            LinkInicioSesion.Visible = false;
            mensajeBienvenido.Text = "Bienvenido " + Session["Nombre"].ToString();
            linkAgregar.Visible = true;
            linkBusqueda.Visible = true;
            linkUsuarios.Visible = true;
        }
    }
    protected void linkCierreSesion_Click(object sender, EventArgs e)
    {
        Session["ID"] = null;
        Session["Nombre"] = null;
        Response.Redirect("./Inicio.aspx");
    }
    protected void LinkInicioSesion_Click(object sender, EventArgs e)
    {
        Response.Redirect("./Sesion.aspx");
    }
}
