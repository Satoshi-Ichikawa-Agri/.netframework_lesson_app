using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TodoApp.Models
{
    /*
     * ログインユーザの管理ロジック
     */
    public class User
    {
        public int Id { get; set; }

        [Required]               // 入力必須
        [Index(IsUnique = true)] // ユニーク
        [StringLength(256)]      // 桁数制限
        [DisplayName("ユーザ名")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)] // パスワード
        [DisplayName("パスワード")]
        public string Password { get; set; }

        // ナビゲーションプロパティ(ロール)
        public virtual ICollection<Role> Roles { get; set; }

        [NotMapped]
        [DisplayName("ロール")]
        public List<int> RoleIds { get; set; }


        // 1人のユーザは複数のTodoを持つので、Todoを所持できるコレクションを作成する。
        public virtual ICollection<Todo> Todoes { get; set; }
    }
}