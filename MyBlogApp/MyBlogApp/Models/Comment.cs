using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBlogApp.Models
{
    // ブログへの閲覧コメントを管理するクラス
    public class Comment
    {
        public int Id { get; set; } // 主キー

        [Required]
        [DisplayName("コメント")]
        public string Body { get; set; }

        [DisplayName("投稿日")]
        public DateTime Created { get; set; }

        // コメントは必ず1つの記事に紐づくためのプロパティ
        public virtual Article Article { get; set; }

        // 記事のIdを保持する
        [NotMapped]
        public int ArticleId { get; set; }
    }
}