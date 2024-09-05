using Chapter6C_Exercise.Models;

namespace Chapter6C_Exercise.DAO
{
    public interface IBoatDAO
    {
        Boat? Insert(Boat? boat);
        Boat? Update(Boat? boat);
        void Delete(int id);
        Boat? GetById(int id);
        IList<Boat> GetAll();
    }
}

