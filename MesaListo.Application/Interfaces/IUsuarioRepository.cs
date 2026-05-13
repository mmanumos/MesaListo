using MesaListo.Application.DTOs.Auth;

namespace MesaListo.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<ResultadoCrearUsuarioDbDto> CrearUsuarioAsync(CrearCuentaRequestDto request, string passwordHash);

        Task<UsuarioLoginDbDto?> ObtenerUsuarioPorCorreoAsync(string correo);
    }
}