namespace pys.api.Services
{
    public interface IJwtService
    {
        string GenerateToken(string username);
    }
}
