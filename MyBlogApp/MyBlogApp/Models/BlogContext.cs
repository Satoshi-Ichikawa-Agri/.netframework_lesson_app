using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyBlogApp.Models
{
    // DBとModelクラスの接続クラス
    public class BlogContext : DbContext
    {
        // Articleクラスと接続
        public DbSet<Article> Articles { get; set; }

        // Commentクラスと接続
        public DbSet<Comment> Comments { get; set; }

        // Categoryクラスと接続
        public DbSet<Category> Categories { get; set; }
    }
}