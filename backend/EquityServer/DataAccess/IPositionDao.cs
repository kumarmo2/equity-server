
using EquityServer.Models;

namespace EquityServer.DataAccess;


public interface IPositionDao
{
    Task Create(Position position);
    Task<List<Position>> GetAll();

}
