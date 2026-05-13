namespace MesaListo.Application.Interfaces
{
    public interface IDatabaseConnectionTester
    {
        Task<bool> CanConnectAsync();
    }
}