using InsureYouAI.Context;
using InsureYouAI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace InsureYouAI.Controllers
{
    public class TestimonialController : Controller
    {
        private readonly InsureContext _context;
        public TestimonialController(InsureContext context)
        {
            _context = context;
        }
        public IActionResult TestimonialList()
        {
            var values = _context.Testimonials.ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateTestimonial()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTestimonial(Testimonial testimonial)
        {
            _context.Testimonials.Add(testimonial);
            _context.SaveChanges();
            return RedirectToAction("TestimonialList");
        }

        [HttpGet]
        public IActionResult UpdateTestimonial(int id)
        {
            var value = _context.Testimonials.Find(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateTestimonial(Testimonial testimonial)
        {
            _context.Testimonials.Update(testimonial);
            _context.SaveChanges();
            return RedirectToAction("TestimonialList");
        }

        public IActionResult DeleteTestimonial(int id)
        {
            var value = _context.Testimonials.Find(id);
            _context.Testimonials.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("TestimonialList");
        }

        public async Task<IActionResult> CreateTestimonialWithClaudeAI() // Claude AI kullanarak testimonial oluşturacak async action
        {
            string apiKey = "key"; // Anthropic (Claude) API anahtarı

            string propmt = "Bir sigorta şirketi için müşteri deneyimlerine dair yorum oluşturmak istiyorum yani ingilizce karşılığı ile : testimonial Bu alanda türkçe olarak 6 tane yorum, 6 tane müşteri adı ve soyadı , bu müşterilerin ünvanı olsun. Buna göre içerigi hazırla.";
            // Claude AI'ya gönderilecek kullanıcı prompt'u (istek metni)

            using var client = new HttpClient(); // HTTP istekleri yapmak için HttpClient nesnesi oluşturulur

            client.BaseAddress = new Uri("https://api.anthropic.com/"); // Claude API'nin temel adresi tanımlanır
            client.DefaultRequestHeaders.Add("x-api-key", apiKey); // API anahtarı header olarak eklenir
            client.DefaultRequestHeaders.Add("anthropic-version", "2023-06-01"); // Claude API versiyonu belirtilir
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // İsteğin JSON formatında olacağı belirtilir

            var requestBody = new // Claude API'ye gönderilecek request body nesnesi
            {
                model = "claude-3-opus-20240229", // Kullanılacak Claude model versiyonu
                max_tokens = 512,                // Modelin üretebileceği maksimum token sayısı
                temperature = 0.7,               // Yanıtın yaratıcılık seviyesi (0-1 arası)
                messages = new[]                 // Claude Messages API formatı
                {
            new
            {
                role = "user",           // Mesajın kullanıcıdan geldiği belirtilir
                content = propmt         // Prompt içeriği gönderilir
            }
        }
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(requestBody) // Request body JSON string'e çevrilir
            );

            var response = await client.PostAsync("v1/messages", jsonContent);
            // Claude API'ye POST isteği gönderilir

            if (!response.IsSuccessStatusCode) // API isteği başarısızsa
            {
                ViewBag.testimonials = new List<string>
        {
            $"Claude API isteği başarısız oldu. Durum Kodu: {response.StatusCode}"
            // Hata mesajı ViewBag ile View'a gönderilir
        };

                return View(); // View döndürülür
            }
            else // API isteği başarılıysa
            {
                var responseString = await response.Content.ReadAsStringAsync();
                // API'den dönen JSON response string olarak alınır

                using var doc = JsonDocument.Parse(responseString);
                // JSON string JsonDocument nesnesine parse edilir

                var fullText = doc.RootElement
                    .GetProperty("content")[0] // Claude response içindeki content array'inin ilk elemanı
                    .GetProperty("text")       // Üretilen metnin olduğu alan
                    .GetString();              // String olarak alınır

                var testimonials = fullText.Split('\n') // Metin satırlara bölünür
                    .Where(x => !string.IsNullOrEmpty(x)) // Boş satırlar filtrelenir
                    .Select(x => x.TrimStart('1', '2', '3', '4', '5', '.', ' '))
                    // Numara ve nokta gibi karakterler baştan temizlenir
                    .ToList(); // Listeye çevrilir

                ViewBag.testimonials = testimonials; // Temizlenmiş testimonial listesi View'a gönderilir
            }

            return View(); // Son olarak View döndürülür
        }
    }
}
