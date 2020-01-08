using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IdentityServer4.Test;
using mvcWebFirstSolucation.Models;
using Microsoft.AspNetCore.Authentication;

namespace mvcWebFirstSolucation.Controllers
{
    public class AccountController : Controller
    {
        private readonly TestUserStore _users;
        public AccountController(TestUserStore ussotre)
        {
            _users = ussotre;
        }
        [HttpGet]
        [Route("/Account/Login")]
        public IActionResult Index(string ReturnUrl = null)
        
{
            ViewData["returnUrl"] = ReturnUrl;
            return View();
        }
        private IActionResult RediretToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction(nameof(HomeController.Index),"Home");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm,string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                ViewData["returnUrl"] = returnUrl;
                var user =  _users.FindByUsername(vm.UserName);
                if (user==null)
                {
                    ModelState.AddModelError(nameof(LoginVM.UserName), "userName is exists");
                }
                else
                {
                    if(_users.ValidateCredentials(vm.UserName, vm.PassWord))
                    {
                        var props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30))
                        };
                        await Microsoft.AspNetCore.Http
                            .AuthenticationManagerExtensions
                                .SignInAsync( HttpContext, user.SubjectId, user.Username, props );

                        return RediretToLocal(returnUrl);
                    }

                    ModelState.AddModelError(nameof(LoginVM.PassWord), "Wrong Password");
                }
            }
            return View();
        }
    }
}