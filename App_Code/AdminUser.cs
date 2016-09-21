using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

public class AdminUser
{
    public AdminUser()
	{
	}

    public int ID { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public static AdminUser findByUserName(string userName)
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=AspAssignment3;Integrated Security=True;");
        SqlCommand command = new SqlCommand();
        SqlDataReader reader;

        AdminUser user = new AdminUser();
        try
        {
            con.Open();
            command.Connection = con;
            command.CommandText = "select * from users where user_name='" + userName + "'";
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                user.ID = Convert.ToInt32(reader.GetValue(0));
                user.UserName = reader.GetValue(1).ToString();
                user.Password = reader.GetValue(2).ToString();
                user.FirstName = reader.GetValue(3).ToString();
                user.LastName = reader.GetValue(3).ToString();
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex);
        }
        finally
        {
            con.Close();
        }
        return user;
    }

    public static AdminUser findByID(int id)
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=AspAssignment3;Integrated Security=True;");
        SqlCommand command = new SqlCommand();
        SqlDataReader reader;

        AdminUser user = new AdminUser();
        try
        {
            con.Open();
            command.Connection = con;
            command.CommandText = "select * from users where id=" + id;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                user.ID = Convert.ToInt32(reader.GetValue(0));
                user.UserName = reader.GetValue(1).ToString();
                user.Password = reader.GetValue(2).ToString();
                user.FirstName = reader.GetValue(3).ToString();
                user.LastName = reader.GetValue(3).ToString();
            }
        }
        catch (Exception ex)
        {
            Console.Write(ex);
        }
        finally
        {
            con.Close();
        }
        return user;
    }
}