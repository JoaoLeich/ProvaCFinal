using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class Token
{

    public static Boolean IsTokenPresenteEValido(HttpContext context){

        var ret = false;

        if (!context.Request.Headers.ContainsKey("Authorization"))
        {
          
            ret =  false;
            return ret;
        }

        var token = GetToken(context);

        var tokenReturn = isTokenValid(token);

        if(tokenReturn is not null){

            ret =  true;
            return ret;

        }

        return ret;
        
    }

    public static String GetToken(HttpContext context){

    var tokenResponse = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

        return tokenResponse;

    }

    public static String GenerateToken()
    {

        var TokenHandler = new JwtSecurityTokenHandler();

        var SecretyKey = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");


        var TokenDescriptor = new SecurityTokenDescriptor
        {


            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(SecretyKey),
                SecurityAlgorithms.HmacSha256Signature
            )

        };

        var token = TokenHandler.CreateToken(TokenDescriptor);

        return TokenHandler.WriteToken(token);

    }

    public static SecurityToken isTokenValid(String token)
    {

        var TokenHandler = new JwtSecurityTokenHandler();

        var Key = Encoding.ASCII.GetBytes("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

        var ValidationParam = new TokenValidationParameters
        {

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Key),
            ValidateIssuer = false,
            ValidateAudience = false

        };

        SecurityToken ValidateToken;

        try
        {

            TokenHandler.ValidateToken(token, ValidationParam, out ValidateToken);
            return ValidateToken;

        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);

        }

        return null;

    }
}
