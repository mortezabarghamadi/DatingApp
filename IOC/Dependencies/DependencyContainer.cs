using Application.Services.Implementation;
using Application.Services.Interfaces;
using Data.Repositories;
using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Convertors;
using Application.Security.Passwordhelper;
using Application.Senders;

namespace IOC.Dependencies
{
    public static class DependencyContainer
    {
        // محل اد کردن تمام سرویس ها و ریپازیتوری ها.
        public static void  RegisterServices(this IServiceCollection service)
        {
            #region Service
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IUserLikeService, UserLikeService>();

            service.AddScoped<ISendMail, SendMail>();
            service.AddScoped<IViewRender, RenderViewToString>();
            service.AddScoped<IPasswordHelper, PasswordHelper>();
            #endregion

            #region Repository
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IUserLikeRepository, UserLikeRepository>();

            #endregion

        }
    }
}
