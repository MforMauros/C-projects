using AutoMapper;
using Chapter6C_Exercise.Models;

namespace Chapter6C_Exercise;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<BoatInsertDTO, Boat>().ReverseMap();
        CreateMap<BoatUpdateDTO, Boat>().ReverseMap();
        CreateMap<BoatReadOnlyDTO, Boat>().ReverseMap();
    }
}
