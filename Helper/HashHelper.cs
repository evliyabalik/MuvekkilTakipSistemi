using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MuvekkilTakipSistemi.Helper
{
    public class HashHelper
    {
        public static string GetMd5Hash(string password)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string GenerateToken(string email)
        {
            // Güvenlik anahtarı
            var securityKey = "YOUR_LONGER_SECURITY_KEY_WITH_AT_LEAST_32_CHARACTERS"; 

            // Token'ın süresini ayarlıyor.
            var expiryTime = DateTime.UtcNow.AddMinutes(15); // 15 dakikalık token süresi

            // Kimlik bilgilerini içeren bir ClaimsIdentity 
            var claimsIdentity = new ClaimsIdentity(new[] {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, expiryTime.ToString("yyyy-MM-ddTHH:mm:ssZ"))
            });

            // Token'ı oluşturmak için bir JwtSecurityTokenHandler Kullanıyor
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = expiryTime,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Oluşturulan token'ı döndür
            return tokenHandler.WriteToken(token);
        }

        public static bool VerifyToken(string token)
        {
            // Token'ı doğrulama işlemi...
            var handler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true, //Token imzalama anahtarını doğrula
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YOUR_LONGER_SECURITY_KEY_WITH_AT_LEAST_32_CHARACTERS")), 
                ValidateIssuer = false, //token kaynağı doğrula
                ValidateAudience = false, //token alıcısı doğrulama
                ValidateLifetime = true, // Token'ın süresini kontrol et
                ClockSkew = TimeSpan.FromHours(1) // Zaman sapması için süre
            };

            try
            {
                var validatedToken = handler.ValidateToken(token, validationParameters, out _);

                // Token'ın son kullanma tarihini al
                var expClaim = validatedToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);
                if (expClaim != null && long.TryParse(expClaim.Value, out var expUnix))
                {
                    var expDateTime = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;
                    return expDateTime > DateTime.UtcNow;
                }

                // Geçerli bir son kullanma tarihi bulunamazsa veya dönüşüm başarısız olursa token geçersiz kabul edilir.
                return false;
            }
            catch (SecurityTokenException)
            {
                // Token doğrulaması başarısız olursa
                return false;
            }
        }

    }
}
