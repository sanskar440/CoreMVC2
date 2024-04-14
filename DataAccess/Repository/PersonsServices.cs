using DataAccess.Models;
using DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient; 
using System.Data;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccess.Repository
{
    public class PersonsServices : IPersonsInterface
    {
        //public List<Persons> GetPersons()
        //{
        //    var personlist = new List<Persons>();

        //    using (SqlConnection conn = new SqlConnection("Data Source = db.satva.solutions, 59763; Initial Catalog = sanskarDB; User ID = sa; Password = Fishy1213#;MultipleActiveResultSets=true;TrustServerCertificate=true"))
        //    {
        //        SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Persons", conn);
        //        conn.Open();

        //        using (SqlDataReader reader = sqlCommand.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                Persons person = new Persons
        //                {
        //                    PersonID = Convert.ToInt32(reader["PersonID"]),
        //                    FirstName = reader["FirstName"].ToString(),
        //                    LastName = reader["LastName"].ToString(),
        //                    City = reader["City"].ToString(),
        //                    Address = reader["Address"].ToString()
        //                };

        //                personlist.Add(person);
        //            }
        //        }
        //    }

        //    return personlist;
        //}

        //public string ConnectionString { get; set; } = string.Empty;
        //public PersonsServices(string connectionString)
        //{
        //    ConnectionString = connectionString;
        //}



        public List<Persons> GetPersons(string search, int page, int pageSize, string sortby, string orderBy)
        {
            List<Persons> persons = new List<Persons>();

            using (SqlConnection conn = new SqlConnection("Data Source = db.satva.solutions, 59763; Initial Catalog = IdentityCustomDemoSanskar; User ID = sa; Password = Fishy1213#;MultipleActiveResultSets=true;TrustServerCertificate=true"))

            {
                // Create a SqlCommand object to execute the stored procedure
                using (SqlCommand command = new SqlCommand("GetPerson", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters to the command
                    command.Parameters.AddWithValue("@SearchKeyword", search);
                    command.Parameters.AddWithValue("@PageNumber", page);
                    command.Parameters.AddWithValue("@PageSize", pageSize);
                    command.Parameters.AddWithValue("@SortColumn", sortby);
                    command.Parameters.AddWithValue("@SortOrder", orderBy);

                    // Open the connection
                    conn.Open();

                    // Execute the command
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Read the results and populate the list of persons
                        while (reader.Read())
                        {
                            Persons person = new Persons
                            {
                                PersonID = Convert.ToInt32(reader["PersonID"]),
                                FirstName = Convert.ToString(reader["FirstName"]),
                                LastName = Convert.ToString(reader["LastName"]),
                                City = Convert.ToString(reader["City"]),
                                Address = Convert.ToString(reader["Address"])
                            };

                            persons.Add(person);
                        }
                    }
                }
            }

            return persons;
        }


        public bool IsFirstNameExists(string firstName)
        {
            bool exists = false;
            using (SqlConnection conn = new SqlConnection("Data Source = db.satva.solutions, 59763; Initial Catalog = IdentityCustomDemoSanskar; User ID = sa; Password = Fishy1213#;MultipleActiveResultSets=true;TrustServerCertificate=true"))

            // Your database connection logic
            {
                conn.Open();

                // Check if the first name already exists in the Persons table
                string query = "SELECT COUNT(*) FROM Persons WHERE FirstName = @FirstName";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    int count = (int)cmd.ExecuteScalar();

                    // If count is greater than 0, it means the first name exists
                    exists = (count > 0);
                }
            }

            return exists;
        }


        public int insertpersons(Persons model)
        {
            var returnvalue = 0;
            using (SqlConnection conn = new SqlConnection("Data Source = db.satva.solutions, 59763; Initial Catalog = IdentityCustomDemoSanskar; User ID = sa; Password = Fishy1213#;MultipleActiveResultSets=true;TrustServerCertificate=true"))
            {
                SqlCommand sqlcommand = new SqlCommand();
                sqlcommand.CommandText = "InsertPersons";
                sqlcommand.CommandType = CommandType.StoredProcedure;
                sqlcommand.Parameters.AddWithValue("@FirstName", SqlDbType.VarChar).Value = model.FirstName;
                sqlcommand.Parameters.AddWithValue("@LastName", SqlDbType.VarChar).Value = model.LastName;
                sqlcommand.Parameters.AddWithValue("@Address", SqlDbType.VarChar).Value = model.Address;
                sqlcommand.Parameters.AddWithValue("@City", SqlDbType.VarChar).Value = model.City;
                sqlcommand.Connection = conn;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                returnvalue = sqlcommand.ExecuteNonQuery();
                conn.Close();
            }

            return returnvalue;
        }


        public int UpdatePersons(Persons model)
        {
            var returnvalue = 0;
            using (SqlConnection conn = new SqlConnection("Data Source = db.satva.solutions, 59763; Initial Catalog = IdentityCustomDemoSanskar; User ID = sa; Password = Fishy1213#;MultipleActiveResultSets=true;TrustServerCertificate=true"))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "NewUpdatePerson";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PersonID", SqlDbType.Int).Value = model.PersonID;
                cmd.Parameters.AddWithValue("@FirstName", SqlDbType.VarChar).Value = model.FirstName;
                cmd.Parameters.AddWithValue("@LastName", SqlDbType.VarChar).Value = model.LastName;
                cmd.Parameters.AddWithValue("@Address", SqlDbType.VarChar).Value = model.Address;
                cmd.Parameters.AddWithValue("@City", SqlDbType.VarChar).Value = model.City;
                //cmd.Parameters.AddWithValue("@PersonID", model.PersonID);
                //cmd.Parameters.AddWithValue("@FirstName", model.FirstName);
                //cmd.Parameters.AddWithValue("@LastName", model.LastName);
                //cmd.Parameters.AddWithValue("@Address", model.Address);
                //cmd.Parameters.AddWithValue("@City", model.City);
                cmd.Connection = conn;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                returnvalue = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return returnvalue;
        }




        public Persons GetUpdatePersons(int id)
        {
            Persons person = new Persons();
            using (SqlConnection conn = new SqlConnection("Data Source = db.satva.solutions, 59763; Initial Catalog = IdentityCustomDemoSanskar; User ID = sa; Password = Fishy1213#;MultipleActiveResultSets=true;TrustServerCertificate=true"))
            {
                SqlCommand cmd = new SqlCommand("GetPersonByID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PersonID", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    person.PersonID = Convert.ToInt32(reader["PersonID"]);
                    person.FirstName = reader["FirstName"].ToString();
                    person.LastName = reader["LastName"].ToString();
                    person.City = reader["City"].ToString();
                    person.Address = reader["Address"].ToString();
                }
            }
            return person;
        }


        //public Persons GetUpdatePerson(int? id)
        //{
        //    Persons person = null;
        //    string sqlQuery = "SELECT * FROM Persons WHERE PersonID = @PersonID";

        //    using (SqlConnection conn = new SqlConnection("Data Source = db.satva.solutions, 59763; Initial Catalog = sanskarDB; User ID = sa; Password = Fishy1213#;MultipleActiveResultSets=true;TrustServerCertificate=true"))
        //    {
        //        SqlCommand cmd = new SqlCommand(sqlQuery, conn);
        //        cmd.Parameters.AddWithValue("@PersonID", id);

        //        conn.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        if (reader.Read())
        //        {
        //            person = new Persons
        //            {
        //                PersonID = Convert.ToInt32(reader["PersonID"]),
        //                FirstName = reader["FirstName"].ToString(),
        //                LastName = reader["LastName"].ToString(),
        //                City = reader["City"].ToString(),
        //                Address = reader["Address"].ToString()
        //            };
        //        }
        //    }

        //    return person;
        //}




        public void DeletePerson(int? id)
        {
            if (id == null)
            {
                throw new ArgumentException("ID cannot be null.");
            }

            var connectionString = "Data Source=db.satva.solutions,59763;Initial Catalog=IdentityCustomDemoSanskar;User ID=sa;Password=Fishy1213#;MultipleActiveResultSets=true;TrustServerCertificate=true";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "spDeletePersons";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PersonID", id.Value);

                cmd.Connection = con;

                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                cmd.ExecuteNonQuery();
                con.Close();
            }
        }




        Persons IPersonsInterface.GetUpdatePersons(int id)
        {
            throw new NotImplementedException();
        }

        int IPersonsInterface.DeletePerson(int id)
        {
            throw new NotImplementedException();
        }

        List<Persons> IPersonsInterface.GetPersons(string search, int page, int pageSize, string sortby, string orderBy)
        {
            throw new NotImplementedException();
        }
    }
}



