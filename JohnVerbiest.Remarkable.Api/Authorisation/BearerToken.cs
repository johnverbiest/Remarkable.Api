namespace JohnVerbiest.Remarkable.Api.Authorisation
{
    public class BearerToken
    {
        public string Token { get; set; }
        public override string ToString()
        {
            return $"Bearer {Token}";
        }
    }
}