using Chapter6C_Exercise.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter6C_Exercise.Pages.Boats
{
    public class DeleteModel : PageModel
    {
        public List<Error> ErrorArray { get; set; } = new();
        private readonly IBoatService _boatService;

        public DeleteModel(IBoatService boatService)
        {
            _boatService = boatService;
        }

        public void OnGet(int id)
        {
            try
            {
                Boat? boat = _boatService?.DeleteBoat(id);
                Response.Redirect("/Boats/getall");
            }
            catch (Exception e)
            {
                ErrorArray.Add(new Error("", e.Message, ""));
            }
        }
    }
}
