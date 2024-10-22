using CustomerUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomerUI.Pages
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Customer Customer { get; set; }

        public async Task OnGetAsync(int id)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://localhost:5278/api/customer/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Customer = JsonSerializer.Deserialize<Customer>(json);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            using var httpClient = new HttpClient();
            var json = JsonSerializer.Serialize(Customer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"http://localhost:5278/api/customer", content);
            response.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }
    }
}
