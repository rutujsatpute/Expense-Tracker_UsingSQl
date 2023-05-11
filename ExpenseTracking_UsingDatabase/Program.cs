using System.Data.SqlClient;
using System.Diagnostics;
using System.Data.Common;

namespace ExpenseTracking_UsingDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Data Source=IN-PF2HZG00; Initial Catalog=Northwind; Integrated Security=true");
            
            string ans = "";
            do
            {
                Console.WriteLine("Welcome to Expense Tracker App");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Choose the Operation you would like to perform:");
                Console.WriteLine("1.Add Transaction");
                Console.WriteLine("2.View Expenses");
                Console.WriteLine("3.View Income");
                Console.WriteLine("4.Check Available Balance");
                Console.WriteLine();
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand($"insert into Transactions values(@title, @description, @amount, @date)", con);

                            Console.WriteLine("Enter Title: ");
                            string Title = Console.ReadLine();
                            Console.WriteLine("Enter Description: ");
                            string Description = Console.ReadLine();
                            Console.WriteLine("Enter amount -ve for Expense: ");
                            int Amount = Convert.ToInt16(Console.ReadLine());
                            Console.WriteLine("Enter Date (dd-mm-yyyy): ");
                            string Dates = Console.ReadLine();
                            cmd.Parameters.AddWithValue("@title", Title);
                            cmd.Parameters.AddWithValue("@description", Description);
                            cmd.Parameters.AddWithValue("@amount", Amount);
                            cmd.Parameters.AddWithValue("@date", Dates);
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Record saved successfully");
                            con.Close();
                            break;
                        }
                    case 2:
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Select * from Transactions where amount like '-%'", con);
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                Console.WriteLine($"{dr[0]} | {dr[1]} | {dr[2]} | {dr[3]} | {dr[4]}");
                            }
                            con.Close();
                            break;
                        }
                    case 3:
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Select * from Transactions where amount not like '-%'", con);
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                Console.WriteLine($"{dr[0]} | {dr[1]} | {dr[2]} | {dr[3]} | {dr[4]}");
                            }
                            con.Close();
                            break;
                        }
                    case 4:
                        {
                            con.Open();
                            con.Close();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong Choice Entered");
                            break;
                        }
                }
                Console.WriteLine("Do you wish to continue? [y/n]");
                ans = Console.ReadLine();
            } while (ans.ToLower() == "y");

        }
    }
}
