using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace TodoApp.Models
{
    public class Todo
    {
        public int Id { get; set; }

        // 画面表記の変更　
        [DisplayName("概要")]
        public string Summary { get; set; }
        
        // 画面表記の変更　
        [DisplayName("詳細")]
        public string Detail { get; set; }
       
        // 画面表記の変更　
        [DisplayName("期限")]
        public DateTime Limit { get; set; }
        
        // 画面表記の変更　
        [DisplayName("完了")]
        public bool Done { get; set; }

        // プロパティを作成し、TodoクラスとUserクラスをつなぐ
        public virtual User User { get; set; }
    }
}