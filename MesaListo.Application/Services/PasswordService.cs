using System.Security.Cryptography;
using MesaListo.Application.Interfaces;

namespace MesaListo.Application.Services
{
    public class PasswordService : IPasswordService
    {
        private const int Iteraciones = 100000;
        private const int TamanoSalt = 16;
        private const int TamanoHash = 32;

        public string GenerarHash(string contrasena)
        {
            try
            {
                byte[] salt = RandomNumberGenerator.GetBytes(TamanoSalt);

                byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                    contrasena,
                    salt,
                    Iteraciones,
                    HashAlgorithmName.SHA256,
                    TamanoHash
                );

                string saltBase64 = Convert.ToBase64String(salt);
                string hashBase64 = Convert.ToBase64String(hash);

                return $"{Iteraciones}.{saltBase64}.{hashBase64}";
            }
            catch (Exception ex)
            {
                throw new Exception("Error generando hash de contraseña.", ex);
            }
        }

        public bool ValidarContrasena(string contrasena, string hashGuardado)
        {
            try
            {
                string[] partes = hashGuardado.Split('.');

                if (partes.Length != 3)
                {
                    return false;
                }

                int iteraciones = Convert.ToInt32(partes[0]);
                byte[] salt = Convert.FromBase64String(partes[1]);
                byte[] hashOriginal = Convert.FromBase64String(partes[2]);

                byte[] hashCalculado = Rfc2898DeriveBytes.Pbkdf2(
                    contrasena,
                    salt,
                    iteraciones,
                    HashAlgorithmName.SHA256,
                    hashOriginal.Length
                );

                return CryptographicOperations.FixedTimeEquals(hashOriginal, hashCalculado);
            }
            catch
            {
                return false;
            }
        }
    }
}