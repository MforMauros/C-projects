using AutoMapper;
using Chapter6C_Exercise.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter6C_Exercise.Pages.Boats;

public class IndexModel : PageModel
{
    public List<BoatReadOnlyDTO>? Boats { get; set; } = new();    
        public Error? ErrorObj { get; set; } = new();

        private readonly IMapper? _mapper;
        private readonly IBoatService? _boatService;

        public IndexModel(IMapper? mapper, IBoatService? boatService)
        {
            _mapper = mapper;
            _boatService = boatService;
        }
    public IActionResult OnGet()
    {
        try
            {
                ErrorObj = null;
                IList<Boat> boats = _boatService!.GetAllBoats();
                
                foreach (var boat in boats)
                {
                    BoatReadOnlyDTO? boatDTO = _mapper!.Map<BoatReadOnlyDTO>(boat);
                    Boats!.Add(boatDTO);
                }
            } catch (Exception e)
            {
                ErrorObj = new Error("", e.Message, "");
            }

            return Page();
    }
}
