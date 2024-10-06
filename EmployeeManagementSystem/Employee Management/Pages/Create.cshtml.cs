using Employee_Management.Models;
using Employee_Management.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Employee_Management.Pages
{
    public class CreateModel : PageModel
    {
        const double expires_in = 5;
        [BindProperty]
        public Employee? Employee { get; set; }
        public void OnGet()
        {
        }

        public ActionResult OnPost()
        {
            var token = "";
            token = ValidateToken();
            if (token != "")
            {
                token = GenerateToken();
                HttpContext.Response.Cookies.Append("token", token,
                new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddMinutes(expires_in) });
            }

            var employee = Employee;
            EmsServices.PostRequest(employee, token);
            return RedirectToPage("index");
            
        }

        private string GenerateToken()
        {
            AccountService accountService = new AccountService();
            return accountService.GenerateAuthTokenAsync(new UserLogin { Username = "test", Password = "test" }).Result;
        }

        private string? ValidateToken()
        {
            var token = "";

            if (HttpContext.Request.Cookies.TryGetValue("token", out token))
            {
                return token;
            }
            return token;
        }
    }
}
