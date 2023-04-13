using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Security;

namespace TodoApp.Models
{
    public class CustomMembershipProvider : MembershipProvider
    {
        public override bool EnablePasswordRetrieval => throw new NotImplementedException();

        public override bool EnablePasswordReset => throw new NotImplementedException();

        public override bool RequiresQuestionAndAnswer => throw new NotImplementedException();

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override int MaxInvalidPasswordAttempts => throw new NotImplementedException();

        public override int PasswordAttemptWindow => throw new NotImplementedException();

        public override bool RequiresUniqueEmail => throw new NotImplementedException();

        public override MembershipPasswordFormat PasswordFormat => throw new NotImplementedException();

        public override int MinRequiredPasswordLength => throw new NotImplementedException();

        public override int MinRequiredNonAlphanumericCharacters => throw new NotImplementedException();

        public override string PasswordStrengthRegularExpression => throw new NotImplementedException();

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        // ユーザ名とパスワードを受け取り、ユーザ名とパスワードが正誤を返す
        public override bool ValidateUser(string username, string password)
        {
            using (var db = new TodoContext())
            { /* usingの間はtodoContextのインスタンスを使える。
               * 外に出るとdisposeされる
               */

                string hash = this.GeneratePasswordHash(username, password);
                
                // ユーザ名とパスワードが一致する情報を取得し、変数に格納する
                // 複数ある場合は先頭を返し、ない場合はNullを返す
                var user = db.Users.Where(u => u.UserName == username 
                && u.Password == hash).FirstOrDefault();

                if(user != null)
                { // ユーザ・パスワードがある
                    return true;
                }
            }
            // ユーザ・パスワードがない
            return false;

            //if("administrator".Equals(username)
            //    && "password".Equals(password))
            //{ // 管理者
            //    return true;
            //}
            //if ("user".Equals(username)
            //    && "password".Equals(password))
            //{ // ユーザ
            //    return true;
            //}
            //// ユーザ名とパスワードが誤りの場合
            //return false;
        }

        // ユーザ名とパスワード引数にとり、Hash化するメソッド
        public string GeneratePasswordHash(string username, string password)
        {
            string rawSalt = $"secret_{username}";
            var sha256 = new SHA256CryptoServiceProvider();
            var salt = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(rawSalt));

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            var hash = pbkdf2.GetBytes(32);

            return Convert.ToBase64String(hash);
        }
    }
}