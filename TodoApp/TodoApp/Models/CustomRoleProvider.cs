using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace TodoApp.Models
{
    public class CustomRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        // ログインユーザのロールを検索し返す
        public override string[] GetRolesForUser(string username)
        {
            // DBアクセス
            using (var db = new TodoContext())
            {
                // ユーザ名を検索し、変数に格納する
                var user = db.Users.Where(u => u.UserName == username).FirstOrDefault();
                if(user != null)
                { // ユーザの所属するロールを返す
                    return user.Roles.Select(role => role.RoleName).ToArray(); // ToArray()配列型に変換する必要がある
                }
            }
            return new string[] { "Users" };
            //if ("administrator".Equals(username))
            //{
            //    return new string[] { "Administrator" };
            //}
            //return new string[] { "Users" };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        // ログインしたユーザがロールに当てはまるかのチェック
        public override bool IsUserInRole(string username, string roleName)
        {
            string[] roles = this.GetRolesForUser(username);
            return roles.Contains(roleName);
            //if("administrator".Equals(username) 
            //    && "Administrator".Equals(roleName))
            //{ // ユーザ名・ロールともに管理者である
            //    return true;
            //}
            //// 管理者でない
            //return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}