using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StoreFront.DATA.EF;
using System.Drawing;
using StoreFront.UI.MVC.Utilities;
using StoreFront.UI.MVC.Models;

namespace StoreFront.UI.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private BoardGameStoreEntities db = new BoardGameStoreEntities();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.InventoryStatu).Include(p => p.Publisher);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //Add to Cart Funtionality
        public ActionResult AddToCart(int qty, int productID)
        {
            Dictionary<int, CartItemViewModel> shoppingCart = null;

            if(Session["cart"] != null)
            {
                shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];
            } else
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();
            }

            Product productName = db.Products.Where(x => x.ProductID == productID).FirstOrDefault();
            if (productName == null)
            {
                return RedirectToAction("Index");
            } else
            {
                CartItemViewModel item = new CartItemViewModel(qty, productName);
                if (shoppingCart.ContainsKey(productName.ProductID))
                {
                    shoppingCart[productName.ProductID].Qty += qty;
                } else
                {
                    shoppingCart.Add(productName.ProductID, item);
                }

                Session["cart"] = shoppingCart;

                Session["confirm"] = $"{productName.ProductName} (Quantity: {qty} added to cart";
            }
            return RedirectToAction("Index", "ShoppingCart");
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.InventoryStatus = new SelectList(db.InventoryStatus, "InventoryID", "InventoryName");
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "PublisherName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductID,ProductName,CategoryID,Price,UnitsSold,InventoryStatus,PublisherID")] Product product, HttpPostedFileBase productImage)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                string file = "NoImage.png";

                if (productImage != null)
                {
                    file = productImage.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (goodExtensions.Contains(ext))
                    {
                        if(productImage.ContentLength <= 4194304)
                        {
                            file = Guid.NewGuid() + ext;
                            #region Resize Image
                            string savePath = Server.MapPath("~/Content/images/productImages/");

                            Image convertedImage = Image.FromStream(productImage.InputStream);
                            int maxImageSize = 500;
                            int maxThumbSize = 100;

                            ImageService.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                            #endregion
                        }
                        product.ImagePath = file;
                    }
                }
                #endregion
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.InventoryStatus = new SelectList(db.InventoryStatus, "InventoryID", "InventoryName", product.InventoryStatus);
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "PublisherName", product.PublisherID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.InventoryStatus = new SelectList(db.InventoryStatus, "InventoryID", "InventoryName", product.InventoryStatus);
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "PublisherName", product.PublisherID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductID,ProductName,CategoryID,Price,UnitsSold,InventoryStatus,PublisherID")] Product product, HttpPostedFileBase productImage)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                string file = "NoImage.png";
                if (productImage != null)
                {
                    file = productImage.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (goodExtensions.Contains(ext))
                    {
                        if (productImage.ContentLength <= 4194304)
                        {
                            file = Guid.NewGuid() + ext;

                            #region Resize Image
                            string savePath = Server.MapPath("~/Content/images/productImages/");

                            Image convertedImage = Image.FromStream(productImage.InputStream);
                            int maxImageSize = 500;
                            int maxThumbSize = 100;

                            ImageService.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                            #endregion
                            #region deleting old image
                            if (product.ImagePath != null && product.ImagePath != "NoImage.png")
                            {
                                string path = Server.MapPath("~/Content/images/productImages/");
                                ImageService.Delete(path, product.ImagePath);
                            }
                            #endregion
                        }
                        product.ImagePath = file;
                    }
                }
                #endregion
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.InventoryStatus = new SelectList(db.InventoryStatus, "InventoryID", "InventoryName", product.InventoryStatus);
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "PublisherName", product.PublisherID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
