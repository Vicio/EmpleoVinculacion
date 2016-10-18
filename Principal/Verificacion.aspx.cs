using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSetFuncsTableAdapters;
using DataSetProcsTableAdapters;

public partial class Principal_Verificacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string validacion = Request.Url.AbsoluteUri.ToString();//se toma el string del url
        if (validacion.Contains("?CambiarPassword="))
        {
            try
            {
                divCambioPass.Visible = true;
                validacion = validacion.Substring(validacion.IndexOf('=') + 1);
                obtener_datos_validacion_usuarioTableAdapter datos = new obtener_datos_validacion_usuarioTableAdapter();
                Session["tempClaveUsuario"] = datos.GetData(validacion)[0].id_usuario;
            }
            catch (Exception err)
            {
                mensaje.InnerHtml = "Hubo un error al cambiar su contraseña";
                divCambioPass.Visible = false;
            }
        }
    }
    protected void ButtonConfirmar_Click(object sender, EventArgs e)
    {
        if (TextBoxPassword.Text.Equals(TextBoxConfirmarPassword.Text) && TextBoxPassword.Text != "")
        {
            try
            {
                ProcsTableAdapter procesos = new ProcsTableAdapter();
                string hash = PasswordHash.PasswordHash.CreateHash(TextBoxConfirmarPassword.Text);
                procesos.cambiar_pass((int)Session["tempClaveUsuario"], hash);
                mensaje.InnerHtml = "Contraseña cambiada exitosamente.";
                divCambioPass.Visible = false;
            }
            catch (Exception err)
            {
                mensaje.InnerHtml = "Ha ocurrido un problema con la comunicación al servidor, intente de nuevo más tarde" + err.ToString();
            }
        }
        else
            mensaje.InnerHtml = "Las contraseñas no coinciden";
    }
}