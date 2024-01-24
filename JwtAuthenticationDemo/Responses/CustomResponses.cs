namespace JwtAuthenticationDemo.Responses
{
    public class CustomResponses
    {
        public record RegistrationResponse(bool Flag = false, string Message = null!);
        public record LoginResopnse(bool Flag = false, string Message = null! , string JWTToken = null! );
    }
}
