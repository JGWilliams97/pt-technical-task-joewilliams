using CustomerUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CustomerUI.Pages
{
    public class IndexModel : PageModel
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();

        public async Task OnGetAsync()
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://localhost:5278/api/customer");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            Customers = JsonSerializer.Deserialize<List<Customer>>(json);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.DeleteAsync($"http://localhost:5278/api/customer/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }
            else
            {
                return Page();
            }
        }
    }
}
