using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

public class Car
{
    public Car()
	{
	}

    public int ID { get; set; }
    public string VIN { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public string Color { get; set; }
    public int Mileage { get; set; }
    public string Accidents { get; set; }
    public string TotalDamageToDate { get; set; }
    public int Price { get; set; }
    public string ImagePath { get; set; }
    public string SpecialFeatures { get; set; }

    public static List<Car> all()
    {
        List<Car> cars = new List<Car>();

        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=AspAssignment3;Integrated Security=True;");
        SqlCommand command = new SqlCommand();
        SqlDataReader reader;

        try
        {
            con.Open();
            command.Connection = con;
            command.CommandText = "select * from cars";
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Car car = new Car();
                car.ID = Convert.ToInt32(reader.GetValue(0));
                car.VIN = reader.GetValue(1).ToString();
                car.Make = reader.GetValue(2).ToString();
                car.Model = reader.GetValue(3).ToString();
                car.Year = Convert.ToInt32(reader.GetValue(4));
                car.Color = reader.GetValue(5).ToString();
                car.Mileage = Convert.ToInt32(reader.GetValue(6));
                car.Accidents = reader.GetValue(7).ToString();
                car.TotalDamageToDate = reader.GetValue(8).ToString();
                car.Price = Convert.ToInt32(reader.GetValue(9));
                car.ImagePath = reader.GetValue(10).ToString();
                car.SpecialFeatures = reader.GetValue(11).ToString();

                cars.Add(car);
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
        return cars;
    }

    public bool save()
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=AspAssignment3;Integrated Security=True;");
        SqlCommand command = new SqlCommand();

        try
        {
            con.Open();
            command.Connection = con;
            if (this.ID > 0)
            {
                command.CommandText = "update cars set vin='" + this.VIN + "', make='" + this.Make + "', model='" + this.Model + "', year=" + this.Year + ", color='" + this.Color + "', mileage=" + this.Mileage + ", total_damage_to_date='" + this.TotalDamageToDate + "', price=" + this.Price + ", image_path='" + this.ImagePath + "', special_features='" + this.SpecialFeatures + "', accidents='" + this.Accidents + "' WHERE id=" + this.ID;
            }
            else
            {
                command.CommandText = "insert into cars (vin, make, model, year, color, mileage, total_damage_to_date, price, image_path, special_features, accidents) values ('" + this.VIN + "', '" + this.Make + "', '" + this.Model + "', '" + this.Year + "', '" + this.Color + "', " + this.Mileage + ", '" + this.TotalDamageToDate + "', " + this.Price + ", '" + this.ImagePath + "', '" + this.SpecialFeatures + "', '" + this.Accidents + "')";
            }
           command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.Write(ex);
        }
        finally
        {
            con.Close();
        }
        return true;
    }

    public static Car findByID(int id)
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=AspAssignment3;Integrated Security=True;");
        SqlCommand command = new SqlCommand();
        SqlDataReader reader;

        Car car = new Car();
        try
        {
            con.Open();
            command.Connection = con;
            command.CommandText = "select * from cars where id=" + id;
            reader = command.ExecuteReader();
            
            while (reader.Read())
            {
                car.ID = Convert.ToInt32(reader.GetValue(0));
                car.VIN = reader.GetValue(1).ToString();
                car.Make = reader.GetValue(2).ToString();
                car.Model = reader.GetValue(3).ToString();
                car.Year = Convert.ToInt32(reader.GetValue(4));
                car.Color = reader.GetValue(5).ToString();
                car.Mileage = Convert.ToInt32(reader.GetValue(6));
                car.Accidents = reader.GetValue(7).ToString();
                car.TotalDamageToDate = reader.GetValue(8).ToString();
                car.Price = Convert.ToInt32(reader.GetValue(9));
                car.ImagePath = reader.GetValue(10).ToString();
                car.SpecialFeatures = reader.GetValue(11).ToString();
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
        return car;
    }

    public static bool delete(int id)
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=AspAssignment3;Integrated Security=True;");

        Car car = new Car();
        try
        {
            con.Open();
            using (SqlCommand command = new SqlCommand("delete from cars where id=" + id, con))
            {
                command.ExecuteNonQuery();
            }
            con.Close();

        }
        catch (Exception ex)
        {
            Console.Write(ex);
        }
        finally
        {
            con.Close();
        }
        return true;
    }

    public static List<Car> findByMulti(string make, string model, string year, string fromPrice, string toPrice)
    {
        List<Car> cars = new List<Car>();

        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=AspAssignment3;Integrated Security=True;");
        SqlCommand command = new SqlCommand();
        SqlDataReader reader;

        try
        {
            con.Open();
            command.Connection = con;
            Dictionary<string, string> keysDictionary = new Dictionary<string, string>();
            if (make != "") {
                keysDictionary.Add("make", make);
            }
            if (model != "") {
                keysDictionary.Add("model", model);
            }
            if (year != "") {
                keysDictionary.Add("year", year);
            }
            if (fromPrice != "" && toPrice != "") {
                keysDictionary.Add("fromPrice", fromPrice);
                keysDictionary.Add("toPrice", toPrice);
            }

            string searchStr = "";

            foreach (var element in keysDictionary) {
                if (searchStr.Length == 0) {
                    searchStr += " where ";
                }
                
                switch (element.Key)
                {
                    case "make":
                    case "model":
                        if (searchStr.Length > 7) {
                            searchStr += " and (" + element.Key + "='" + element.Value + "')";
                        } else {
                            searchStr += "(" + element.Key + "='" + element.Value + "')";
                        }
                        break;
                    case "year":
                        if (searchStr.Length > 7) {
                            searchStr += " and (" + element.Key + "=" + element.Value + ")";
                        } else {
                            searchStr += "(" + element.Key + "=" + element.Value + ")";
                        }
                        break;
                    case "fromPrice":
                        if (searchStr.Length > 7) {
                            searchStr += " price between " + element.Value + " and " + keysDictionary["toPrice"];
                        } else {
                            searchStr += "price between " + element.Value + " and " + keysDictionary["toPrice"];
                        }
                        break;

                }

            }

            command.CommandText = "select * from cars" + searchStr;

            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Car car = new Car();
                car.ID = Convert.ToInt32(reader.GetValue(0));
                car.VIN = reader.GetValue(1).ToString();
                car.Make = reader.GetValue(2).ToString();
                car.Model = reader.GetValue(3).ToString();
                car.Year = Convert.ToInt32(reader.GetValue(4));
                car.Color = reader.GetValue(5).ToString();
                car.Mileage = Convert.ToInt32(reader.GetValue(6));
                car.Accidents = reader.GetValue(7).ToString();
                car.TotalDamageToDate = reader.GetValue(8).ToString();
                car.Price = Convert.ToInt32(reader.GetValue(9));
                car.ImagePath = reader.GetValue(10).ToString();
                car.SpecialFeatures = reader.GetValue(11).ToString();

                cars.Add(car);
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
        return cars;
    }
}