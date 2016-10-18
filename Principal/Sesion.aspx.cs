using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataSetFuncsTableAdapters;
using DataSetProcsTableAdapters;

public partial class Principal_Sesion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void buttonAceptar_Click(object sender, EventArgs e)
    {
        SQLInjects injects = new SQLInjects();
        FuncsTableAdapter funciones = new FuncsTableAdapter();
        obtener_datos_validacion_usuarioTableAdapter sesion = new obtener_datos_validacion_usuarioTableAdapter();

        string correo = injects.Remover(TextBoxCorreo.Text);
        string hashUsuario = "";
        try
        {
            hashUsuario = funciones.obtener_hash_usuario(correo);
            if (hashUsuario.Length < 5)
                MostrarError("Los datos que ingresó son incorrectos, por favor inténtelo de nuevo.");
            else
                if (PasswordHash.PasswordHash.ValidatePassword
                    (TextBoxPassword.Text, hashUsuario))
                {
                    Session["ID"] = sesion.GetData(hashUsuario)[0].id_usuario;
                    Session["Nombre"] = sesion.GetData(hashUsuario)[0].nombre_usuario;
                    Response.Redirect("./Inicio.aspx");
                }
                else
                    MostrarError("Los datos que ingresó son incorrectos, por favor inténtelo de nuevo.");

        }
        catch (Exception err)
        {
            MostrarError("Usted no se ha registrado.");
        }




    }

    private void MostrarError(string error)
    {
        textoError.Text = error + "<br />";
    }

    protected void LinkButtonOlvidePassword_Click(object sender, EventArgs e)
    {
        divAcceso.Visible = false;
        divPass.Visible = true;
    }
    protected void buttonCambiarPass_Click(object sender, EventArgs e)
    {

        SQLInjects injects = new SQLInjects();

        string correoUsuario = injects.Remover(TextBoxCorreoVerificacion.Text);
        FuncsTableAdapter funciones = new FuncsTableAdapter();
        string hash = funciones.obtener_hash_usuario(correoUsuario);
        obtener_datos_validacion_usuarioTableAdapter datos = new obtener_datos_validacion_usuarioTableAdapter();
        string nombreUsuario = datos.GetData(hash)[0].nombre_usuario;

        Correo correo = new Correo();
        correo.enviarMailCambioPass(correoUsuario, nombreUsuario, hash);
        LabelMensaje.Text = "Se le ha enviado un correo para cambiar su contraseña." + DateTime.Now;
        try
        {
        }
        catch (Exception err)
        {
            LabelMensaje.Text = "Ocurrió un problema al enviar su correo, intente de nuevo más tarde.";
        }
    }
}