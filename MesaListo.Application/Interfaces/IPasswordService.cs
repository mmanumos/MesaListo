namespace MesaListo.Application.Interfaces
{
    public interface IPasswordService
    {
        string GenerarHash(string contrasena);

        bool ValidarContrasena(string contrasena, string hashGuardado);
    }
}