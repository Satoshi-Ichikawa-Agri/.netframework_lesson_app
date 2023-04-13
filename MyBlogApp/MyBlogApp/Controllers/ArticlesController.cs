using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyBlogApp.Models;

namespace MyBlogApp.Controllers
{
    [CategoryFilter]
    public class ArticlesController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Articles
        [AllowAnonymous] // 誰でもアクセス可能
        public ActionResult Index()
        {
            return View(db.Articles.ToList());
        }

        // GET: Articles/Details/5
        [AllowAnonymous] // 誰でもアクセス可能
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        [Authorize(Roles = "Owners")] // 管理者のみ
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 をご覧ください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owners")] // 管理者のみ
        public ActionResult Create([Bind(Include = "Id,Title,Body,CategoryName")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.Created = DateTime.Now;  // 投稿日は現在日時を取得
　　　　　　　　article.Modified = DateTime.Now; // 更新日は現在日時を取得

            　　// DBからカテゴリー名を取得する
            　　var category = db.Categories
                　　.Where(item => item.CategoryName.Equals(article.CategoryName))
                　　.FirstOrDefault();

            　　if(category == null)
            　　{ // カテゴリー名がない場合、カテゴリーを登録する
                　　category = new Category()
                　　{
                      CategoryName = article.CategoryName,
                      Count = 1
                　　};
                    db.Categories.Add(category);
            　　}
            　　else
            　　{ // カテゴリー名がある場合、記事の件数を更新する
            　　    category.Count++;
            　　    db.Entry(category).State = EntityState.Modified;
            　　}

            　　// ナビゲーションプロパティにカテゴリーをセット
            　　article.Category = category;

                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(article);
        }

        // GET: Articles/Edit/5
        [Authorize(Roles = "Owners")] // 管理者のみ
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }

            article.CategoryName = article.Category.CategoryName;

            return View(article);
        }

        // POST: Articles/Edit/5
        // 過多ポスティング攻撃を防止するには、バインド先とする特定のプロパティを有効にしてください。
        // 詳細については、https://go.microsoft.com/fwlink/?LinkId=317598 をご覧ください。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owners")] // 管理者のみ
        public ActionResult Edit([Bind(Include = "Id,Title,Body,CategoryName")] Article article)
        {
            if (ModelState.IsValid)
            {
                // DBから該当の記事データを取得する
                var dbArticle = db.Articles.Find(article.Id);

                if(dbArticle == null)
                { // 該当の記事データがない
                    return HttpNotFound();
                }

                // DBから取得したデータに画面の内容を反映する
                dbArticle.Title = article.Title;
                dbArticle.Body = article.Body;
                dbArticle.Modified = DateTime.Now;
                dbArticle.CategoryName = article.CategoryName;

                // カテゴリーの更新
                var beforeCategory = dbArticle.Category; // 変更前のカテゴリーを取得

                if (!beforeCategory.CategoryName.Equals(article.CategoryName))
                { /* 変更前と画面入力値を比較し、異なるとき
                   前のカテゴリーから削除し、件数を更新する*/
                    beforeCategory.Articles.Remove(dbArticle);
                    beforeCategory.Count--;
                    db.Entry(beforeCategory).State = EntityState.Modified;

                    // DBからカテゴリーを取得する
                    var category = db.Categories
                        .Where(item => item.CategoryName.Equals(article.CategoryName))
                        .FirstOrDefault();

                    if (category == null)
                    { // カテゴリーがない場合、カテゴリーを登録する
                        category = new Category()
                        {
                            CategoryName = article.CategoryName,
                            Count = 1
                        };
                        db.Categories.Add(category); // 件数の更新
                    }
                    else
                    { // カテゴリー名がある場合、記事の件数を更新する
                        category.Count++;
                        db.Entry(category).State = EntityState.Modified;
                    }

                    // 記事と新しいカテゴリーの紐づけ
                    dbArticle.Category = category;
                }


                db.Entry(dbArticle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        [Authorize(Roles = "Owners")] // 管理者のみ
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owners")] // 管理者のみ
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);

            // 記事を削除する前に、記事の件数を[-1]する
            Category category = article.Category; // カテゴリーの取得
            if(category != null)
            { // カテゴリーがあれば、[-1]する
                category.Count--;
                db.Entry(category).State = EntityState.Modified;
            }

            // 記事に紐づくコメントを削除する
            article.Comments.Clear();

            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Articles/CreateComment
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous] // 誰でもアクセス可能
        public ActionResult CreateComment([Bind(Include = "ArticleId,Body")] Comment comment)
        { // コメントの登録処理
            // 画面で入力されたArticleIdを基に、DBから記事のデータを取得する
            var article = db.Articles.Find(comment.ArticleId);

            if(article == null)
            {  // 記事が無ければエラーとして終了
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            // コメントの作成日時をセット
            comment.Created = DateTime.Now;
            // コメントのナビゲーションプロパティにDBから取得した記事をセット
            comment.Article = article;

            // コメントをDBに登録する
            db.Comments.Add(comment);
            db.SaveChanges();

            // コメント登録後、画面遷移する
            return RedirectToAction("Details", new { id = comment.ArticleId });
        }

        // GET: Articles/DeleteComment/5
        [Authorize(Roles = "Owners")] // 管理者のみ
        public ActionResult DeleteComment(int? id)
        { // コメントの削除時の確認画面を表示するメソッド
            if (id == null)
            { // idが見つからない場合、エラーを返す
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // DBから該当のコメントを取得する
            var comment = db.Comments.Find(id);

            if(comment == null)
            { // コメントが見つからなけば、NotFound
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Articles/DeleteComment/5
        [HttpPost, ActionName("DeleteComment")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owners")] // 管理者のみ
        public ActionResult DeleteCommentConfirmed(int id)
        { // コメントの削除処理
            // DBから該当のコメントを取得する
            var comment = db.Comments.Find(id);
            // ArticleのIDを取得する
            int articleId = comment.Article.Id;

            db.Comments.Remove(comment);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = articleId });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
