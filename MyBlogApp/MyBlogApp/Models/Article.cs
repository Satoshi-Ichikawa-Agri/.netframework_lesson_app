using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBlogApp.Models
{
    // ブログの記事を管理するクラス
    public class Article
    {
        // フィールド
        public int Id { get; set; } // 主キー

        [Required] // 入力必須(アノテーション)
        [DisplayName("タイトル")]
        public string Title { get; set; }

        [Required]
        [DisplayName("本文")]
        public string Body { get; set; }

        [DisplayName("投稿日")]
        public DateTime Created { get; set; }

        [DisplayName("更新日")]
        public DateTime Modified { get; set; }

        // ナビゲーションプロパティ：
        // 記事の投稿時にカテゴリーを選択できるようにする
        public virtual Category Category { get; set; }

        // ナビゲーションプロパティ：
        // 1記事に複数コメントを投稿できるようにする
        public virtual ICollection<Comment> Comments { get; set; }

        // カテゴリーネームプロパティ：
        // 画面でのカテゴリーを保持する
        [NotMapped] // DBに保存しない
        [DisplayName("カテゴリー")]
        public string CategoryName { get; set; }




    }
}