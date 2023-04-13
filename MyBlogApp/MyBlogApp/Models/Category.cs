using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyBlogApp.Models
{
    // カテゴリーを管理するクラス
    public class Category
    {
        public int Id { get; set; } // 主キー

        [Required]
        [Index(IsUnique = true)] // 一意にする
        [StringLength(200)]
        [DisplayName("カテゴリー")]
        public string CategoryName { get; set; }

        // カウントプロパティ
        // 件数をあらかじめ保持しておく
        [DisplayName("件数")]
        public int Count { get; set; }

        // コレクションのArticleプロパティ
        // 1つの記事には複数のカテゴリーを用意する
        public virtual ICollection<Article> Articles { get; set; }
    }
}