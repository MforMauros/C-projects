using System.Transactions;
using AutoMapper;
using Chapter6C_Exercise.DAO;
using Chapter6C_Exercise.Models;

namespace Chapter6C_Exercise;

public class BoatServiceImpl : IBoatService
{

    private readonly IBoatDAO? _boatDAO;
    private readonly IMapper? _mapper;
    private readonly ILogger<BoatServiceImpl>? _logger;

    public BoatServiceImpl(IBoatDAO? boatDAO, IMapper? mapper, ILogger<BoatServiceImpl>? logger)
    {
        _boatDAO = boatDAO;
        _mapper = mapper;
        _logger = logger;
    }



    public Boat? DeleteBoat(int id)
    {
        Boat? boatToReturn = null;

        try
            {
                using TransactionScope scope = new();     
                boatToReturn = _boatDAO!.GetById(id);
                if (boatToReturn == null) return null;
                _boatDAO.Delete(id);
                scope.Complete();
                 
                _logger!.LogInformation("Delete Success");
                return boatToReturn;
            } catch (Exception e)
            {
                _logger!.LogError("An error occured while deleting boat: " + e.Message);
                throw;
            }
    }

    public IList<Boat> GetAllBoats()
    {
        try
            {
                IList<Boat> boats = _boatDAO!.GetAll();
                return boats;
            } catch (Exception e)
            {
                _logger!.LogError("An error occured while fetching boats: " + e.Message);
                throw;
            }
    }

    public Boat? GetBoat(int id)
    {
        try
            {
                return _boatDAO!.GetById(id);
            } catch (Exception e)
            {
                _logger!.LogError("An error occured while fetching a boat: " + e.Message);
                throw;
            }
    }

    public Boat? InsertBoat(BoatInsertDTO dto)
    {
        if (dto == null) return null;

        try
            {
                var boat = _mapper!.Map<Boat>(dto);
                using TransactionScope scope = new();
                Boat? insertedBoat = _boatDAO!.Insert(boat);
                scope.Complete();
                _logger!.LogInformation("Success in insert the boat");
                return insertedBoat;
            } catch (Exception e)
            {
                _logger!.LogError("An error occured while inserting a boat: " + e.Message);
                throw;
            }
    }

    public Boat? UpdateBoat(BoatUpdateDTO dto)
    {
        if (dto is null) return null;
        Boat? boatToReturn = null;

        try
            {
                var boat = _mapper!.Map<Boat>(dto);
                using TransactionScope scope = new();
                boatToReturn = _boatDAO!.Update(boat);
                scope.Complete();
                _logger!.LogInformation("Success in updating boat");
                return boatToReturn;           
            } catch (Exception e)
            {
                _logger!.LogError("An error occured while inserting a boat: " + e.Message);
                throw;
            }
    }
}
