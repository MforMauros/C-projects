using AutoMapper;
using Chapter6C_Exercise;
using Chapter6C_Exercise.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter6C_Exercise.Pages.Boats
{
    public class UpdateModel : PageModel
    {
        public BoatUpdateDTO? BoatUpdateDTO{ get; set; } = new();
        public List<Error> ErrorArray { get; set; } = new();

        public readonly IBoatService? _boatService;
        public readonly IValidator<BoatUpdateDTO>? _boatUpdateValidator;
        public readonly IMapper? _mapper;

        public UpdateModel(IBoatService? boatService, IValidator<BoatUpdateDTO>? boatUpdateValidator,
            IMapper? mapper)
        {
            _boatService = boatService;
            _boatUpdateValidator = boatUpdateValidator;
            _mapper = mapper;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                Boat? boat = _boatService!.GetBoat(id);
                BoatUpdateDTO = _mapper!.Map<BoatUpdateDTO>(boat);
            } catch (Exception e)
            {
                ErrorArray.Add(new Error("", e.Message, ""));
            }
            return Page();
        }

        public void OnPost(BoatUpdateDTO dto)
        {
            BoatUpdateDTO = dto;

            var validationResult = _boatUpdateValidator!.Validate(dto);

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
                Boat? boat = _boatService!.UpdateBoat(dto);
                Response.Redirect("/Boats/getall");
            }
            catch (Exception e)
            {
                ErrorArray!.Add(new Error("", e.Message, ""));
            }
        }



        // public BoatUpdateDTO? BoatUpdateDTO { get; set; } = new();
        // public List<Error> ErrorArray { get; set; } = new();

        // public readonly IBoatService? _boatService;
        // public readonly IValidator<BoatUpdateDTO>? _boatUpdateValidator;
        // public readonly IMapper? _mapper;

        // public UpdateModel(IBoatService? boatService, IValidator<BoatUpdateDTO>? boatUpdateValidator,
        //     IMapper? mapper)
        // {
        //     _boatService = boatService;
        //     _boatUpdateValidator = boatUpdateValidator;
        //     _mapper = mapper;
        // }

        // public IActionResult OnGet(int id)
        // {
        //     try
        //     {
        //         Boat? boat = _boatService!.GetBoat(id);
        //         BoatUpdateDTO = _mapper!.Map<BoatUpdateDTO>(boat);
        //     } catch (Exception e)
        //     {
        //         ErrorArray.Add(new Error("", e.Message, ""));
        //     }
        //     return Page();
        // }

        // public void OnPost(BoatUpdateDTO dto)
        // {
        //     BoatUpdateDTO = dto;

        //     var validationResult = _boatUpdateValidator!.Validate(dto);

        //     if (!validationResult.IsValid)
        //     {
        //         foreach (var error in validationResult.Errors)
        //         {
        //             ErrorArray!.Add(new Error(error.ErrorCode, error.ErrorMessage, error.PropertyName));
        //         }
        //         return Page();
        //     }

        //     try
        //     {
        //         Boat? boat = _boatService!.UpdateBoat(dto);
        //         // Response.Redirect("/Boats/getall");
        //         return RedirectToPage("/Boats/getall");

        //     }
        //     catch (Exception e)
        //     {
        //         ErrorArray!.Add(new Error("", e.Message, ""));
        //     }
        // }
    }
}
