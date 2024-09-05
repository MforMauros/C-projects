using AutoMapper;
using Chapter6C_Exercise.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter6C_Exercise.Pages.Boats
{
    public class CreateModel : PageModel
    {

    public List<Error>? ErrorArray { get; set; } = new();
        public BoatInsertDTO? BoatInsertDTO { get; set; } = new();

        private readonly IBoatService? _boatService;
        private readonly IValidator<BoatInsertDTO>? _boatInsertValidator;

        public CreateModel(IBoatService? boatService, IValidator<BoatInsertDTO>? boatInsertValidator,
            IMapper mapper)
        {
            _boatService = boatService;
            _boatInsertValidator = boatInsertValidator;
        }

        public void OnGet()
        {
        }

        public void OnPost(BoatInsertDTO dto)
        { 
            BoatInsertDTO = dto;

            var validationResult = _boatInsertValidator!.Validate(dto);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ErrorArray!.Add(new Error(error.ErrorCode, error.ErrorMessage, error.PropertyName));
                }
                return;
            }

            try
            {
                Boat? boat = _boatService!.InsertBoat(dto);
                Response.Redirect("/Boats/getall");
            } catch (Exception e)
            {
                ErrorArray!.Add(new Error("", e.Message, ""));
            }
        }
    }
}
