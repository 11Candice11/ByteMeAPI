namespace ByteMeAPI.IDP
{
    public class AuthenticationResponseModel
    {
        public string AccessToken { get; set; }
        public long AccessTokenIssuedUtcDateTimeTicks { get; set; }
        public int AccessTokenValidityInSeconds { get; set; }
        public string RefreshToken { get; set; }
        public long RefreshTokenIssuedUtcDateTimeTicks { get; set; }
        public int RefreshTokenValidityInSeconds { get; set; }
    }
}
