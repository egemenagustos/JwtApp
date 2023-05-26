using JwtApp.Front.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace JwtApp.Front.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel request)
        {
            if (ModelState.IsValid)
            {
                /* İstemci oluşturdum.*/
                var client = _httpClientFactory.CreateClient();

                /* İstek atacağım adres için parametremden gelen dataları bir json dönüştürme işlemine tabi tuttum.*/
                var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

                /* Gideceğim adrese paramteremdeki dönüştürülmüş json tipindeki datalarımla birlikte isteği attım.*/
                var responseMessage = await client.PostAsync("https://localhost:7188/api/Auth/Login", content);

                /* Attığım requestten yani istekten gelen response u aldım ve bir koşula koydum.*/
                if (responseMessage.IsSuccessStatusCode)
                {
                    /* Bana gelen datanın içindekileri okudum.*/
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();

                    /* Okuduğum dataları kendi oluşturduğum modele dönüştürdüm.*/
                    var tokenModel = JsonSerializer.Deserialize<JwtTokenResponseModel>(jsonData, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

                    /* Yine aldığım dataları koşula koydum ki data geldiğinden emin olayım.*/
                    if (tokenModel != null)
                    {
                        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

                        /* Token bilgilerimi okudum.*/
                        var token = handler.ReadJwtToken(tokenModel.Token);

                        var claims = token.Claims.ToList();

                        if(tokenModel.Token != null)
                        {
                            claims.Add(new Claim("accessToken", tokenModel.Token));
                        }
                        
                        /* Token sayesinde gelen claim bilgilerimi okudum.*/
                        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);

                        /* Token için bitiş tarihini ve tarayıcının beni hatırlayıp hatırlamamasını belirttim. */
                        var authProp = new AuthenticationProperties
                        {
                            ExpiresUtc = tokenModel.ExpireDate, 
                            IsPersistent = true 
                        };

                        /* Veee giriş işlemini yaptım. */
                        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProp);

                        return RedirectToAction("Index","Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalıdır!");
                }
                return View();
            }
            return View(request);
        }
    }
}
