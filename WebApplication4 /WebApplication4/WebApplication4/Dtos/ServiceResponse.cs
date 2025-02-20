namespace WebApplication4.Dtos
{
    public class ServiceResponse
    {
        public bool Success { get; set; } = true; // Indicates success/failure
        public string Message { get; set; } = string.Empty; // Response message
    }
}
