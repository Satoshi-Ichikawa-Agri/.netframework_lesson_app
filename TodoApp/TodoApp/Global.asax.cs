using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TodoApp.Migrations;
using TodoApp.Models;

namespace TodoApp
{
    // TodoApp���N�������Ƃ��ɍŏ��ɌĂ΂��N���X�E���\�b�h
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TodoContext, Configuration>());
        }
    }
}
