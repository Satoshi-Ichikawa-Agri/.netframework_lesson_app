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
    public class CategoriesController : Controller
    {
        private BlogContext db = new BlogContext();

        // GET: Categories
        [AllowAnonymous] // 誰でもアクセス可能
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        [AllowAnonymous] // 誰でもアクセス可能
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        [Authorize(Roles = "Owners")] // 管理者のみ
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owners")] // 管理者のみ
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);

            if(category.Count > 0)
            { // 削除時、カテゴリーに記事が存在したら、エラーを表示する
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }


            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
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
