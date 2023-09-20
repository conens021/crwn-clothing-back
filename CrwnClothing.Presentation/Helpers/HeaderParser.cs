namespace CrwnClothing.Presentation.Helpers
{
    public class HeaderParser
    {
        private readonly IHttpContextAccessor _contextAccesor;

        public HeaderParser(IHttpContextAccessor contextAccesor) 
        {
            _contextAccesor = contextAccesor;

        }

        public string? Get(string headerKey)
        {
            string? jwtTokenValue = _contextAccesor?.HttpContext?.Request.Headers[headerKey].ToString();


            return jwtTokenValue;
        }
    }
}
