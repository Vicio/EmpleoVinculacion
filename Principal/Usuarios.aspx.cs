using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSetProcsTableAdapters;
using DataSetFuncsTableAdapters;

public partial class Principal_Usuarios : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ID"] == null)
            Response.Redirect("./Inicio.aspx");

    }
    protected void ButtonAgregar_Click(object sender, EventArgs e)
    {
        ProcsTableAdapter procesos = new ProcsTableAdapter();
        FuncsTableAdapter funciones = new FuncsTableAdapter();
        SQLInjects injects = new SQLInjects();

        try
        {
            string correoUsuario = injects.Remover(TextBoxCorreo.Text);
            string nombreUsuario = injects.Remover(TextBoxNombre.Text);
            string hash = PasswordHash.PasswordHash.CreateHash(correoUsuario + nombreUsuario);

            if((bool)funciones.verificar_correo_registrado(correoUsuario))
            {
                labelMensajeModificacion.InnerHtml = "Este correo ya está registrado";
            }
            else
            {
                labelMensajeModificacion.InnerHtml = "Se está procesando su petición, espere por favor...";
                procesos.agregar_usuario(nombreUsuario, correoUsuario, hash);
                Correo correo = new Correo();


                correo.enviarMailCambioPass(correoUsuario, nombreUsuario, hash);
                labelMensaje.InnerHtml = "Se le ha enviado un correo para crear una contraseña para su cuenta." + DateTime.Now;
                divModificacionUsuarios.Visible = false;
                divUsuarios.Visible = true;
                GridViewUsuarios.DataBind();
            }

        }
        catch(Exception err)
        {
            labelMensajeModificacion.InnerHtml = "Ha ocurrido un error en el sistema, favor de contactar al administrador.";
        }
    }
    protected void ButtonCancelar_Click(object sender, EventArgs e)
    {
        divModificacionUsuarios.Visible = false;
        divUsuarios.Visible = true;
    }
    protected void ButtonAgregarUsuario_Click(object sender, EventArgs e)
    {
        divUsuarios.Visible = false;
        divModificacionUsuarios.Visible = true;
        labelMensajeModificacion.InnerHtml = "";
    }
    protected void GridViewUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName.Equals("EliminarUsuario"))
        {

        }
        if(e.CommandName.Equals("CambiarPassUsuario"))
        {
            try
            {
                int indice = Convert.ToInt32(e.CommandArgument);
                GridViewRow fila = GridViewUsuarios.Rows[indice];
                DataSetFuncsTableAdapters.FuncsTableAdapter funciones = new DataSetFuncsTableAdapters.FuncsTableAdapter();
                string nombreUsuario = HttpUtility.HtmlDecode(fila.Cells[1].Text);
                string hash = funciones.obtener_hash_usuario(HttpUtility.HtmlDecode(fila.Cells[2].Text));
                string correoUsuario = HttpUtility.HtmlDecode(fila.Cells[2].Text);
                Correo correo = new Correo();

                correo.enviarMailCambioPass(correoUsuario, nombreUsuario, hash);
                labelMensaje.InnerHtml = "Se ha enviado un correo al usuario";
            }
            catch(Exception err)
            {

            }
        }
    }
}