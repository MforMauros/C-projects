using Chapter6C_Exercise.Models;

namespace Chapter6C_Exercise;

public interface IBoatService
{
    Boat? InsertBoat(BoatInsertDTO dto);
    Boat? UpdateBoat(BoatUpdateDTO dto);
    Boat? DeleteBoat(int id);
    Boat? GetBoat(int id);
    IList<Boat> GetAllBoats();
}
