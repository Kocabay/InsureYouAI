using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InsureYouAI.Controllers
{
    public class CategoryController : Controller
    {
        // Veritabanı işlemleri için kullanılacak DbContext nesnesi
        private readonly InsureContext _context;

        // Dependency Injection ile InsureContext sınıfı alınır
        public CategoryController(InsureContext context)
        {
            _context = context;
        }

        // Tüm kategorileri listeleyen action
        public IActionResult CategoryList()
        {
            // Categories tablosundaki tüm kayıtları listeye çeker
            var values = _context.Categories.ToList();
            // Listeyi View’a gönderir
            return View(values);
        }

        // Yeni kategori ekleme sayfasını açar (GET)
        [HttpGet]
        public IActionResult CreateCategory()
        {
            // Boş form sayfasını döndürür
            return View();
        }

        // Formdan gelen kategori verisini kaydeder (POST)
        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            // Gelen category nesnesini veritabanına ekler
            _context.Categories.Add(category);
            // Değişiklikleri veritabanına kaydeder
            _context.SaveChanges();
            // İşlem sonrası kategori listesine yönlendirir
            return RedirectToAction("CategoryList");
        }

        // Güncellenecek kategori bilgilerini getirir (GET)
        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            // Id’ye göre ilgili kategori kaydını bulur
            var value = _context.Categories.Find(id);
            // Bulunan veriyi güncelleme formuna gönderir
            return View(value);
        }

        // Güncellenmiş kategori bilgilerini kaydeder (POST)
        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            // Category nesnesini güncelleme olarak işaretler
            _context.Categories.Update(category);
            // Değişiklikleri veritabanına kaydeder
            _context.SaveChanges();
            // Güncelleme sonrası kategori listesine yönlendirir
            return RedirectToAction("CategoryList");
        }

        // Seçilen kategoriyi siler
        public IActionResult DeleteCategory(int id)
        {
            // Silinecek kategoriyi id’ye göre bulur
            var value = _context.Categories.Find(id);
            // Bulunan kaydı veritabanından siler
            _context.Categories.Remove(value);
            // Silme işlemini veritabanına kaydeder
            _context.SaveChanges();
            // Silme sonrası kategori listesine yönlendirir
            return RedirectToAction("CategoryList");
        }
    }
}
