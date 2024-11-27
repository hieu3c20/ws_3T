using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;
using System.Data;
using System.Text.RegularExpressions;

namespace Notification
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine(string.Format("=== TMV Business Trip Online ({0}) ===", DateTime.Now.ToString("dd-MM-yyyy HH:mm")));
                Console.WriteLine("===================================================");
                //get data
                Console.WriteLine("Getting list of BT need to return wifi device...");
                string body = GetData();
                Console.WriteLine("--- Getting done! ---");
                Console.WriteLine("------------------------------------------------");
                if (string.IsNullOrEmpty(body))
                {
                    Console.WriteLine("--- No data found! ---");
                }
                else
                {
                    //send email
                    Console.WriteLine("Sending notification emails...");

                    string from = "BTS Support <no-reply@bts.com.vn>";
                    string to = GetReceivers();
                    string cc = "";
                    string bcc = "sycuongdx@toyotavn.com.vn";
                    string subject = "[BTS] List of Wifi Devices must be returned";
                    string attachment = "";
                    var client = new TMVEmail.EmailServiceSoapClient();
                    client.SendEmail(from, to, cc, bcc, subject, body, attachment);
                    //
                    Console.WriteLine("--- Emails sent! ---");
                }
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("Shutting down the application (in 5)...");
                Thread.Sleep(1000);
                Console.WriteLine("Shutting down the application (in 4)...");
                Thread.Sleep(1000);
                Console.WriteLine("Shutting down the application (in 3)...");
                Thread.Sleep(1000);
                Console.WriteLine("Shutting down the application (in 2)...");
                Thread.Sleep(1000);
                Console.WriteLine("Shutting down the application (in 1)...");
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine("===================================================");
                Console.WriteLine("=== !ERROR! ===");
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        static DateTime GetServerDate()
        {
            using (SqlConnection conn = BTSConnection)
            {
                using (var command = new SqlCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "select getdate()";
                    command.Connection = conn;
                    command.Connection.Open();
                    DateTime sysdate = Convert.ToDateTime(command.ExecuteScalar());
                    return sysdate;
                }
            }
        }

        static string GetReceivers()
        {
            using (SqlConnection conn = BTSConnection)
            {
                using (var command = new SqlCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "select [Text] from tbl_Lookup where Code = 'EMAIL_ADDRESS' and Value = 'wifi_receiver' and Active = 1";
                    command.Connection = conn;
                    command.Connection.Open();
                    string receivers = command.ExecuteScalar().ToString();
                    receivers = Regex.Replace(receivers, @"\s", "");
                    receivers = receivers.Replace(";", ",").Trim(new char[] { ',' });
                    return receivers;
                }
            }
        }

        static string GetData()
        {
            //get data
            var dt = new DataTable();
            using (var command = new SqlCommand())
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "GetWifiDeviceBT";
                command.Connection = BTSConnection;
                var adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
            }
            //
            if (dt.Rows.Count > 0)
            {
                var builder = new StringBuilder();
                builder.Append(string.Format("<h3 style='font-size: 14px;'>List of BT request having Wifi Devices must be returned on <span style='text-decoration: underline'>{0:dd-MM-yyyy}</span>:</h3>", GetServerDate()));
                builder.Append("<table style='table-layout: fixed; border-collapse: collapse; font-size: 12px;'>");
                builder.Append("<tr>");
                builder.Append("<th style='border: 1px solid #ccc; padding: 3px;'>BT No</th>");
                builder.Append("<th style='border: 1px solid #ccc; padding: 3px;'>Employee</th>");
                builder.Append("<th style='border: 1px solid #ccc; padding: 3px;'>Departure</th>");
                builder.Append("<th style='border: 1px solid #ccc; padding: 3px;'>Return</th>");
                builder.Append("<th style='border: 1px solid #ccc; padding: 3px;'>Positon</th>");
                builder.Append("<th style='border: 1px solid #ccc; padding: 3px;'>Destination</th>");
                builder.Append("<th style='border: 1px solid #ccc; padding: 3px;'>Country</th>");
                //builder.Append("<th style='border: 1px solid #ccc; padding: 3px;'>Price</th>");
                //builder.Append("<th style='border: 1px solid #ccc;>VAT</th>");
                //builder.Append("<th style='border: 1px solid #ccc; padding: 3px;'>Advance Status</th>");
                //builder.Append("<th style='border: 1px solid #ccc; padding: 3px;'>Expense Status</th>");
                builder.Append("</tr>");
                foreach (DataRow dr in dt.Rows)
                {
                    builder.Append("<tr>");
                    builder.Append(string.Format("<td style='border: 1px solid #ccc; padding: 3px;'>{0}</td>", dr["BTNo"]));
                    builder.Append(string.Format("<td style='border: 1px solid #ccc; padding: 3px;'>{0}</td>", dr["Employee"]));
                    builder.Append(string.Format("<td style='border: 1px solid #ccc; padding: 3px;'>{0:dd-MM-yyyy HH:mm}</td>", dr["DepartureDate"]));
                    builder.Append(string.Format("<td style='border: 1px solid #ccc; padding: 3px;'>{0:dd-MM-yyyy HH:mm}</td>", dr["ReturnDate"]));
                    builder.Append(string.Format("<td style='border: 1px solid #ccc; padding: 3px;'>{0}</td>", dr["Position"]));
                    builder.Append(string.Format("<td style='border: 1px solid #ccc; padding: 3px;'>{0}</td>", dr["Destination"]));
                    builder.Append(string.Format("<td style='border: 1px solid #ccc; padding: 3px;'>{0}</td>", dr["Country"]));
                    //builder.Append(string.Format("<td style='border: 1px solid #ccc; padding: 3px;'>{0}</td>", dr["UnitPrice"]));
                    //builder.Append(string.Format("<td style='border: 1px solid #ccc; padding: 3px;'>{0}</td>", dr["VAT"]));
                    //builder.Append(string.Format("<td style='border: 1px solid #ccc; padding: 3px;'>{0}</td>", dr["AdvanceStatus"]));
                    //builder.Append(string.Format("<td style='border: 1px solid #ccc; padding: 3px;'>{0}</td>", dr["ExpenseStatus"]));
                    builder.Append("</tr>");
                }
                builder.Append("</table>");
                return builder.ToString();
            }
            else
            {
                return "";
            }
        }

        static SqlConnection BTSConnection
        {
            get
            {
                string connStr = ConfigurationManager.ConnectionStrings["BusinessTripConnectionString"].ToString();
                SqlConnection conn = new SqlConnection(connStr);
                return conn;
            }
        }
    }
}
