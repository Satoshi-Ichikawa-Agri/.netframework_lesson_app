using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TodoApp.Models
{
    // 編集画面用のモデル
    public class UserEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(256)]
        [DisplayName("ユーザー名")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "新しいパスワード")]
        public string Password { get; set; }

        [DisplayName("ロール")]
        public List<int> RoleIds { get; set; }

    }
}