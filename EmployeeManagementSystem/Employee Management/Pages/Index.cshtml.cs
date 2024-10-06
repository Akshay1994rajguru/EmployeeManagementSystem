using Employee_Management.Models;
using Employee_Management.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Employee_Management.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        const double expires_in = 5;
        public List<Employee?> EmployeeList { get; set; }
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            var token = "";
            token= ValidateToken();
            if (token == "")
            {
                token= GenerateToken();
                HttpContext.Response.Cookies.Append("token", token,
                new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddMinutes(expires_in) });
            }

            var data = EmsServices.GetAll(token);

            EmployeeList = data.Result;
        }

        public ActionResult OnGetDelete(int? id)
        {
            var token = "";
            token = ValidateToken();
            if (token == "")
            {
                token = GenerateToken();
                HttpContext.Response.Cookies.Append("token", token,
                new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.Now.AddMinutes(expires_in) });
            }
            if (id != null)
            {
                if(EmsServices.DeleteEmployee(id?.ToString(), token).Result)
                {
                    return RedirectToPage("Index");
                }
            }
            return null;
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
