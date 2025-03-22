using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/protected")]
[ApiController]
public class ProtectedController : ControllerBase
{
    [HttpGet]
    [Authorize] // Buraya sadece token'ı olanlar erişebilir
    public IActionResult GetSecureData()
    {
        return Ok(new { message = "Bu veriyi sadece giriş yapanlar görebilir!" });
    }
}
