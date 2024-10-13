using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Advert.Services.Extensions
{
    public static class LoggedInUserExtensions
    {
        //ClaimsPrincipal kullanıcı kimlik doğrulaması yaprıltan sonra kullanıcının bilgileri CLAİMSORİNCİOAL nesnesinde tutulur
        //Bu extension metotlar, ASP.NET Core kimlik doğrulama sistemi ile çalışan uygulamalarda oturum açmış kullanıcının ID'sini veya e-posta adresini almayı kolaylaştırır.
        //Böylece her seferinde ClaimsPrincipal üzerinden manuel olarak bu bilgileri çekmek yerine, bu metotları çağırarak daha temiz ve okunabilir kod yazılabilir.




        //Bu metot, ClaimTypes.NameIdentifier ile kullanıcının ID'sini alır
        //FindFirstValue() metodu, belirtilen claim tipine göre ilk değeri döner.
        public static Guid GetLoggedInUserId(this ClaimsPrincipal principal)
        {
            return Guid.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));
        }


        //ClaimTypes.Email, kullanıcı oturum açarken e-posta adresi ile doğrulandıysa, bu claim içinde saklanır.
        public static string GetLoggedInEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Email);
        }
    }
}
