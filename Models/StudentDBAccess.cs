using MySql.Data.MySqlClient;
using System.Data;
namespace adoProject.Models;
public class StudentDBAccess
{
    MySql.Data.MySqlClient.MySqlConnection myConnection = new MySql.Data.MySqlClient.MySqlConnection("Server=localhost;Database=ADOProject;User=root;Password=root");
    public string AddStudentData(Student student)
    {
        try
        {
            string query = "INSERT INTO Students (FirstName, LastName, Sex, DateOfBirth, Address, CreatedDate) VALUES (@FirstName, @LastName, @Sex, @DateOfBirth, @Address, @CreatedDate)";

            myConnection.Open();

            using (MySqlCommand cmd = new MySqlCommand(query, myConnection))
            {
                cmd.Parameters.AddWithValue("@FirstName", student.FirstName);
                cmd.Parameters.AddWithValue("@LastName", student.LastName);
                cmd.Parameters.AddWithValue("@Sex", student.Sex);
                cmd.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth);
                cmd.Parameters.AddWithValue("@Address", student.Address);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                cmd.ExecuteNonQuery();
            }

            myConnection.Close();
            return ("Data added successfully to database");

        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            return (ex.Message.ToString());
        }
    }

    public List<Student> GetStudentData()
    {
        List<Student> students = new List<Student>();
        try
        {
            myConnection.Open();
            MySqlCommand cmd = new MySqlCommand("Select * from Students", myConnection);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Student s = new Student();
                s.Id = Convert.ToInt32(reader["Id"]);
                s.FirstName = reader["FirstName"].ToString();
                s.LastName = reader["LastName"].ToString();

                int genderType = Convert.ToInt32(reader["Sex"]);
                if (genderType == 0)
                {
                    s.Sex = Gender.Male;
                }
                else
                {
                    s.Sex = Gender.Female;
                }

                string? dob = reader["DateOfBirth"].ToString();
                DateTime date;
                bool res = DateTime.TryParse(dob, out date);
                if (!res)
                {
                    throw new DataException("Could Not parse the data to date time format");
                }
                s.DateOfBirth = date;

                s.Address = reader["Address"].ToString();
                students.Add(s);
            }
            reader.Close();
            myConnection.Close();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            Console.WriteLine(ex.Message);
        }
        catch (DataException ex)
        {
            if (myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
            }
            Console.WriteLine(ex.Message);
        }
        return students;
    }

    public void DeleteStudentData(int id){
      try{
        string query = "Delete from Students where Id = @id";
        myConnection.Open();
        using(MySqlCommand cmd = new MySqlCommand(query, myConnection)){
          cmd.Parameters.AddWithValue("@id", id);
          cmd.ExecuteNonQuery();
        }
        myConnection.Close();
      }catch(MySql.Data.MySqlClient.MySqlException ex){
        if(myConnection.State == ConnectionState.Open){
          myConnection.Close();
        }
        Console.WriteLine(ex.Message);
      }
    }

}
