namespace WebApplication1.DTOs
{
    public record LoginResponse
    (
        string token,
        int? status
        );
}
