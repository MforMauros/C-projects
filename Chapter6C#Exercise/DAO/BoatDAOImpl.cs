using System.Data.SqlClient;
using Chapter6C_Exercise.DAO;
using Chapter6C_Exercise.Models;

namespace Chapter6C_Exercise;

public class BoatDAOImpl : IBoatDAO
{
    public void Delete(int id)
    {
        string sql = "DELETE FROM BOATS WHERE ID = @id";

        using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();

            using SqlCommand command = new(sql, conn);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
    }

    public IList<Boat> GetAll()
    {
        string sql = "SELECT * FROM BOATS";
        var boats = new List<Boat>();
        Boat? boat;

        using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();

            using SqlCommand command = new(sql, conn);
            using SqlDataReader reader = command.ExecuteReader(); 

            while (reader.Read())
            {
                boat = new()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    Boatname = reader.GetString(reader.GetOrdinal("BOATNAME")),
                    Price = reader.GetInt32(reader.GetOrdinal("PRICE")),
                    Region = reader.GetString(reader.GetOrdinal("REGION"))
                };
                boats.Add(boat);
            }
            return boats;
    }

    public Boat? GetById(int id)
    {
        string? sql = "SELECT * FROM BOATS WHERE ID = @id";
        Boat? boat = null;

        using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();

            using SqlCommand command = new(sql, conn);
            command.Parameters.AddWithValue("@id", id);

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                boat = new()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    Boatname = reader.GetString(reader.GetOrdinal("BOATNAME")),
                    Price = reader.GetInt32(reader.GetOrdinal("PRICE")),
                    Region = reader.GetString(reader.GetOrdinal("REGION"))
                };
            }
            return boat;
    }

    public Boat? Insert(Boat? boat)
    {
        if (boat == null) return null;
        string sql = "INSERT INTO BOATS (BOATNAME, PRICE, REGION) VALUES (@boatname, @price, @region); " +
                "SELECT SCOPE_IDENTITY();";
        Boat? boatToReturn = null;
        int insertedId = 0;

        using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();

            using SqlCommand command = new(sql, conn);
            command.Parameters.AddWithValue("@boatname", boat.Boatname);
            command.Parameters.AddWithValue("@price", boat.Price);
            command.Parameters.AddWithValue("@region", boat.Region);

            object insertedObj = command.ExecuteScalar();
            if (insertedObj is not null)
            {
                if (!int.TryParse(insertedObj.ToString(), out insertedId))
                {
                    throw new Exception("Error in insert id");
                }
            }

            string? sqlSelect = "SELECT * FROM BOATS WHERE ID = @id";

            using SqlCommand sqlCommand = new(sqlSelect, conn);
            sqlCommand.Parameters.AddWithValue("@id", insertedId);

            using SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.Read())
            {
                boatToReturn = new()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("ID")),
                    Boatname = reader.GetString(reader.GetOrdinal("BOATNAME")),
                    Price = reader.GetInt32(reader.GetOrdinal("PRICE")),
                    Region = reader.GetString(reader.GetOrdinal("REGION"))
                };
            }
            return boatToReturn;
    }

    public Boat? Update(Boat? boat)
    {
        if (boat == null) return null;
        string sql = "UPDATE BOATS SET BOATNAME = @boatname, PRICE = @price, REGION = WHERE ID = @id";
        Boat? boatToReturn = boat;

        using SqlConnection? conn = DBHelper.GetConnection();
            if (conn is not null) conn.Open();

            using SqlCommand command = new(sql, conn);
            command.Parameters.AddWithValue("@boatname", boat.Boatname);
            command.Parameters.AddWithValue("@price", boat.Price);
            command.Parameters.AddWithValue("@region", boat.Region);
            command.Parameters.AddWithValue("@id", boat.Id);

            command.ExecuteNonQuery();
            return boatToReturn;        
    }
}
