using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Identity;

namespace WebStore.WebAPI.Clients.Identity
{
   public static  class IdentityExtensions
    {
        public static IServiceCollection AddIdentityWebStoreWebApiClients(this IServiceCollection services)
        {
            services.AddHttpClient("WebStoreApiIdentity", (s, client) => client.BaseAddress = new Uri(s.GetRequiredService<IConfiguration>()["WebAPI"]))
                .AddTypedClient<IUserStore<User>, UsersClient>()
                .AddTypedClient<IUserRoleStore<User>, UsersClient>()
                .AddTypedClient<IUserPasswordStore<User>, UsersClient>()
                .AddTypedClient<IUserEmailStore<User>, UsersClient>()
                .AddTypedClient<IUserPhoneNumberStore<User>, UsersClient>()
                .AddTypedClient<IUserTwoFactorStore<User>, UsersClient>()
                .AddTypedClient<IUserClaimStore<User>, UsersClient>()
                .AddTypedClient<IUserLoginStore<User>, UsersClient>()
                .AddTypedClient<IRoleStore<Role>, RolesClient>();
            return services;
        }

        public static IdentityBuilder AddIdentityWebStoreWebApiClients(this IdentityBuilder builder)
        {
            builder.Services.AddIdentityWebStoreWebApiClients();
            return builder;
        }
    }
}
