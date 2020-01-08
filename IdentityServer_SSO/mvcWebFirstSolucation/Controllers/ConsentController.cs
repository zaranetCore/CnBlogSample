using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvcWebFirstSolucation.Models;
using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Models;

namespace mvcWebFirstSolucation.Controllers
{
    public class ConsentController : Controller
    {
        private readonly IClientStore _clientStore;
        private readonly IResourceStore _resourceStore;
        private readonly IIdentityServerInteractionService _identityServerInteractionService;
        public ConsentController(
          IClientStore clientStore,
          IResourceStore resourceStore, 
          IIdentityServerInteractionService identityServerInteractionService)
        {
            _clientStore = clientStore;
            _resourceStore = resourceStore;
            _identityServerInteractionService = identityServerInteractionService;
        }
        /// <summary>
        /// 返回一个consent对象
        /// </summary>
        private async Task<ConsentVm> BuildConsentViewModel(string returlUrl)
        {
            //获取验证上下文
            var request = await _identityServerInteractionService.GetAuthorizationContextAsync(returlUrl);
            if (request == null)
                return null;
            //根据上下文获取client的信息以及资源Api的信息
            var client = await _clientStore.FindEnabledClientByIdAsync(request.ClientId);
            var resources = await _resourceStore.FindEnabledResourcesByScopeAsync(request.ScopesRequested);
            //创建consent对象
            var vm =  CreateConsentViewModel(request,client,resources);
            vm.ReturnUrl = returlUrl;
            return vm;
        }
        /// <summary>
        /// 创建consent对象
        /// </summary>
        private ConsentVm CreateConsentViewModel(AuthorizationRequest request,Client client,Resources resources)
        {
            var vm = new ConsentVm(); 
            vm.ClientId = client.ClientId;
            vm.Logo = client.LogoUri;
            vm.ClientName = client.ClientName;
            vm.ClientUrl = client.ClientUri;//客户端url
            vm.RemeberConsent = client.AllowRememberConsent;//是否记住信息
            vm.IdentityScopes = resources.IdentityResources.Select(i=>CreateScopeViewModel(i));
            vm.ResourceScopes = resources.ApiResources.SelectMany(u => u.Scopes).Select(x => CreatesScoreViewModel(x));
            return vm;
        }
        public ScopeVm CreatesScoreViewModel(Scope scope)
        {
            return new ScopeVm
            {
                name = scope.Name,
                DisplayName = scope.DisplayName,
                Description = scope.Description,
                Required = scope.Required,
                Checked = scope.Required,
                Emphasize = scope.Emphasize
            };
        }
        private ScopeVm CreateScopeViewModel(IdentityResource identityResource)
        {
            return new ScopeVm
            {
                name = identityResource.Name,
                DisplayName = identityResource.DisplayName,
                Description = identityResource.Description,
                Required = identityResource.Required,
                Checked = identityResource.Required,
                Emphasize = identityResource.Emphasize
            };
        }
        public async Task<IActionResult> Index(string returnUrl)
        {
            var model =await BuildConsentViewModel(returnUrl);

            if (model == null)
            {

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(InputConsentViewModel viewmodel)
        {
            // viewmodel.ReturlUrl
            ConsentResponse consentResponse = null;
            if (viewmodel.Button =="no")
            {
                consentResponse = ConsentResponse.Denied;
            }
            else
            {
                if (viewmodel.ScopesConsented !=null && viewmodel.ScopesConsented.Any()) 
                {
                    consentResponse = new ConsentResponse
                    {
                        RememberConsent = viewmodel.RemeberConsent,
                        ScopesConsented = viewmodel.ScopesConsented
                    };
                }
            }
            if (consentResponse != null)
            {
                var request = await _identityServerInteractionService.GetAuthorizationContextAsync(viewmodel.ReturnUrl);
                await _identityServerInteractionService.GrantConsentAsync(request, consentResponse);
                return Redirect(viewmodel.ReturnUrl);
            }
            return View(await BuildConsentViewModel(viewmodel.ReturnUrl));
        }
    }
}