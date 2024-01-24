namespace JwtAuthenticationDemo.DTOs
{
    public class CustomUserClaim
    {
        public CustomUserClaim()
        {
        }

        public string Name { get; set; }
        public string Email { get; set; }

        public CustomUserClaim(string value1, string value2)
        {
            Name = value1;
            Email = value2;
        }

    }
}
