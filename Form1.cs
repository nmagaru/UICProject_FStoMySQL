using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        static Microsoft.FSharp.Collections.FSharpList<HW5P2Lib.HW5P2.Article> alldata;
        static MySqlConnection conn;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void task1fs_Click(object sender, EventArgs e)
        {
            string filename = fileText.Text;
            alldata = HW5P2Lib.HW5P2.readfile(filename);

            // F1 //
            // Take the user's input, get an integer to pass to F# code
            string stringInput = inputText.Text;
            int input = Int32.Parse(stringInput);

            // Call the library function
            string title = HW5P2Lib.HW5P2.getTitle(input, alldata);

            // Output the result
            outputText.Text = "Title: " + title;
        }

        private void task2fs_Click(object sender, EventArgs e)
        {
            string filename = fileText.Text;
            alldata = HW5P2Lib.HW5P2.readfile(filename);

            // F2 //
            // Take the user's input, get an integer to pass to F# code
            string stringInput = inputText.Text;
            int input = Int32.Parse(stringInput);

            // Call the library function
            int count = HW5P2Lib.HW5P2.wordCount(input, alldata);

            // Output the result
            outputText.Text = "Number of words in the article: " + count.ToString();
        }

        private void task3fs_Click(object sender, EventArgs e)
        {
            string filename = fileText.Text;
            alldata = HW5P2Lib.HW5P2.readfile(filename);

            // F3 //
            // Take the user's input, get an integer to pass to F# code
            string stringInput = inputText.Text;
            int input = Int32.Parse(stringInput);

            // Call the library function
            string month = HW5P2Lib.HW5P2.getMonthName(input, alldata);

            // Output the result
            outputText.Text = "Month of chosen article: " + month;

        }

        private void task4fs_Click(object sender, EventArgs e)
        {
            string filename = fileText.Text;
            alldata = HW5P2Lib.HW5P2.readfile(filename);

            // F4 //
            // Call the library function, and return result to be output
            Microsoft.FSharp.Collections.FSharpList<string> publishers = HW5P2Lib.HW5P2.publishers(alldata);

            // Output the result
            outputText.Text = "Unique Publishers:\r\n" + String.Join("\r\n", publishers);
        }

        private void task5fs_Click(object sender, EventArgs e)
        {
            string filename = fileText.Text;
            alldata = HW5P2Lib.HW5P2.readfile(filename);

            // F5 //
            // Call the library function, and return result to be output
            Microsoft.FSharp.Collections.FSharpList<string> countries = HW5P2Lib.HW5P2.countries(alldata);

            // Output the result
            outputText.Text = "Unique Countries:\r\n" + String.Join("\r\n", countries);
        }

        private void task6fs_Click(object sender, EventArgs e)
        {
            string filename = fileText.Text;
            alldata = HW5P2Lib.HW5P2.readfile(filename);

            // F6 //
            // Call the library function, and return result to be output
            double avg = HW5P2Lib.HW5P2.avgNewsguardscoreForArticles(alldata);

            // Output the result
            outputText.Text = "Average news guard score for all articles: " + String.Format("{0:F3}", avg);
        }

        private void task7fs_Click(object sender, EventArgs e)
        {
            string filename = fileText.Text;
            alldata = HW5P2Lib.HW5P2.readfile(filename);

            // F7 //
            // Call the library function, and return result to be output
            Microsoft.FSharp.Collections.FSharpList<Tuple<string, int>> nArticles = HW5P2Lib.HW5P2.numberOfArticlesEachMonth(alldata);

            // Output the result
            string output = HW5P2Lib.HW5P2.buildHistogram(nArticles, alldata.Length, "");
            outputText.Text = "Number of Articles for Each Month:\r\n" + output;
        }

        private void task8fs_Click(object sender, EventArgs e)
        {
            string filename = fileText.Text;
            alldata = HW5P2Lib.HW5P2.readfile(filename);

            // F8 //
            // Call the library function, and return result to be output
            Microsoft.FSharp.Collections.FSharpList<Tuple<string, double>> reliablepct = HW5P2Lib.HW5P2.reliableArticlePercentEachPublisher(alldata);

            // Output the result
            Microsoft.FSharp.Collections.FSharpList<string> lines1 = HW5P2Lib.HW5P2.printNamesAndPercentages(reliablepct);
            outputText.Text = "Percentage of Articles that are reliable for each publisher:\r\n";
            foreach (string line in lines1)
                outputText.Text = outputText.Text + line;
        }

        private void task9fs_Click(object sender, EventArgs e)
        {
            string filename = fileText.Text;
            alldata = HW5P2Lib.HW5P2.readfile(filename);

            // F9 //
            // Call the library function, and return result to be output
            Microsoft.FSharp.Collections.FSharpList<Tuple<string, double>> avgscore = HW5P2Lib.HW5P2.avgNewsguardscoreEachCountry(alldata, HW5P2Lib.HW5P2.countries(alldata));

            // Output the result
            Microsoft.FSharp.Collections.FSharpList<string> output = HW5P2Lib.HW5P2.printNamesAndFloats(avgscore);
            outputText.Text = "Average News Guard Score for Each Country:\r\n";
            foreach (string line in output)
                outputText.Text = outputText.Text + line;
        }

        private void task10fs_Click(object sender, EventArgs e)
        {
            string filename = fileText.Text;
            alldata = HW5P2Lib.HW5P2.readfile(filename);

            // F9 //
            // Call the library function, and return result to be output
            Microsoft.FSharp.Collections.FSharpList<Tuple<string, double>> avgscore = HW5P2Lib.HW5P2.avgNewsguardscoreEachBias(alldata);

            // Output the result
            string output = HW5P2Lib.HW5P2.buildHistogramFloat(avgscore, "");
            outputText.Text = "The Average News Guard Score for Each Political Bias Category:\r\n" + output;
        }

        private void establishConnection()
        {
            try
            {
                string connStr = "server=" + hostText.Text +
                                ";user=" + userText.Text +
                                ";database=" + dataText.Text +
                                ";port=" + portText.Text +
                                ";password=" + passText.Text; // change the database and password to test on your machine
                conn = new MySqlConnection(connStr);                                                     // must be these values when submitting to gradescope
                conn.Open();
            }
            catch (Exception ex)
            {
                outputText.Text = "Testing Connection to MySQL Server...\r\n";
                outputText.Text = outputText.Text + ex.ToString() + "\r\n";
                outputText.Text += "Could not connect to the server.\r\n";
                outputText.Text += "Check that the server is running,\r\n";
                outputText.Text += "that you have loaded the database hw5,\r\n";
                outputText.Text += "And that the password in establishConnection() matches your MySQL password.\r\n";
                return;
            }
        }

        private void task1sql_Click(object sender, EventArgs e)
        {
            // Establish Connection to MySQL Server
            establishConnection();

            string stringInput = inputText.Text;
            int nid = Int32.Parse(stringInput);

            try
            {
                // Write (copy from queries folder) the query, using the news id read from the user
                //      You can use @" to begin a raw string, which allows for multiple lines in the string
                string query = String.Format(@"
                                                SELECT title
                                                FROM news
                                                WHERE news_id = {0};
                                            ", nid);

                // Build a Command which holds the query and the location of the target server
                //      Use the static MySqlConnection object conn which was initialized and opened at the beginning of the application
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);

                // Retrieve the results into a DataReader
                //      There are many methods for executing a command, use the one which returns a DataReader for the simplest solution.
                MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader();

                // Output the header from the DataReader
                //     Use the .GetName function of the DataReader to get the column names for the header (just one column for function 1)
                outputText.Text = reader.GetName(0) + "\r\n";

                // Loop through the rows of the DataReader to output the values from the DataReader
                while (reader.Read())
                {
                    outputText.Text = outputText.Text + reader[0] + "\r\n";
                }
                /*
                        Inside the loop
                        We use the DataReader object to get the values in the rows matching the columns of the header ouput before the loop.
                         -> reader.GetType(index) //Where 'Type' in GetType is to be replaced by the actual type of the attribute.
                         -> GetString for string type, GetInt32 for Integer types etc.
                        For Example: 
                        Console.WriteLine(String.Format("{0}\t{1}", reader.GetString(index1), reader.GetInt32(index2)));
                */

                // Close the DataReader
                reader.Close();
            }
            catch (Exception ex)
            {
                outputText.Text = ex.ToString();
            }
        }

        private void task2sql_Click(object sender, EventArgs e)
        {
            // Establish Connection to MySQL Server
            establishConnection();

            MySqlDataReader reader;

            try
            {
                // Write (copy from queries folder) the query
                string query = String.Format(@" 
                                                SELECT news_id, LENGTH(body_text) AS length
                                                FROM news
                                                WHERE LENGTH(body_text)>100
                                                ORDER BY news_id;
                                            ");

                // Build a Command which holds the query and the location of the target server
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);

                // Retrieve the results into a DataReader
                reader = cmd.ExecuteReader();

                // Output the header from the DataReader
                outputText.Text = reader.GetName(0) + "\t" + reader.GetName(1) + "\r\n";

                // Loop through the rows of the DataReader to output the values from the DataReader
                while (reader.Read())
                {
                    outputText.Text = outputText.Text + reader.GetString(0) + "\t" + reader.GetInt32(1) + "\r\n";
                }

                // Close the DataReader
                reader.Close();
            }
            catch (Exception ex)
            {
                outputText.Text = ex.ToString();
            }
        }

        private void task3sql_Click(object sender, EventArgs e)
        {
            // Establish Connection to MySQL Server
            establishConnection();

            try
            {
                // Write (copy from queries folder) the query
                string query = String.Format(@"
                                                SELECT title, DATE_FORMAT(STR_TO_DATE(publish_date, '%c/%d/%y'), '%M') AS Month
                                                FROM news
                                                ORDER BY STR_TO_DATE(publish_date, '%m/%d/%y')
                                            ");

                // Build a Command which holds the query and the location of the target server
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);

                // Retrieve the results into a DataReader
                MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader();

                // Output the header from the DataReader
                outputText.Text = reader.GetName(0) + "\t" + reader.GetName(1) + "\r\n";

                // Loop through the rows of the DataReader to output the values from the DataReader
                while (reader.Read())
                {
                    outputText.Text = outputText.Text + reader.GetString(0) + "\t" + reader.GetString(1) + "\r\n";
                }

                // Close the DataReader
                reader.Close();
            }
            catch (Exception ex)
            {
                outputText.Text = ex.ToString();
            }
        }

        private void task4sql_Click(object sender, EventArgs e)
        {
            // Establish Connection to MySQL Server
            establishConnection();

            try
            {
                // Write (copy from queries folder) the query
                string query = String.Format(@"
                                                SELECT publisher
                                                FROM publisher_table
                                                JOIN news
                                                USING (publisher_id)
                                                GROUP BY publisher
                                                ORDER BY publisher;
                                            ");

                // Build a Command which holds the query and the location of the target server
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);

                // Retrieve the results into a DataReader
                MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader();

                // Output the header from the DataReader
                outputText.Text = reader.GetName(0) + "\r\n";

                // Loop through the rows of the DataReader to output the values from the DataReader
                while (reader.Read())
                {
                    outputText.Text = outputText.Text + reader.GetString(0) + "\r\n";
                }

                // Close the DataReader
                reader.Close();
            }
            catch (Exception ex)
            {
                outputText.Text = ex.ToString();
            }
        }

        private void task5sql_Click(object sender, EventArgs e)
        {
            // Establish Connection to MySQL Server
            establishConnection();

            try
            {
                // Write (copy from queries folder) the query
                string query = String.Format(@"
                                                SELECT country, COUNT(news_id) AS articleCount
                                                FROM country_table
                                                LEFT JOIN news
                                                USING (country_id)
                                                GROUP BY country
                                                ORDER BY articleCount DESC;
                                            ");

                // Build a Command which holds the query and the location of the target server
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);

                // Retrieve the results into a DataReader
                MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader();

                // Output the header from the DataReader
                outputText.Text = reader.GetName(0) + "\t" + reader.GetName(1) + "\r\n";

                // Loop through the rows of the DataReader to output the values from the DataReader
                while (reader.Read())
                {
                    outputText.Text = outputText.Text + reader.GetString(0) + "\t" + reader.GetString(1) + "\r\n";
                }

                // Close the DataReader
                reader.Close();
            }
            catch (Exception ex)
            {
                outputText.Text = ex.ToString();
            }
        }

        private void task6sql_Click(object sender, EventArgs e)
        {
            // Establish Connection to MySQL Server
            establishConnection();

            try
            {
                // Write (copy from queries folder) the query
                string query = String.Format(@"
                                                SELECT ROUND(AVG(news_guard_score),3) AS `Average Score`
                                                FROM news;
                                            ");

                // Build a Command which holds the query and the location of the target server
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);

                // Retrieve the results into a DataReader
                MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader();

                // Output the header from the DataReader
                outputText.Text = reader.GetName(0) + "\r\n";

                // Loop through the rows of the DataReader to output the values from the DataReader
                while (reader.Read())
                {
                    outputText.Text = outputText.Text + String.Format("{0:F3}", reader.GetFloat(0)) + "\r\n";
                }

                // Close the DataReader
                reader.Close();
            }
            catch (Exception ex)
            {
                outputText.Text = ex.ToString();
            }
        }

        private void task7sql_Click(object sender, EventArgs e)
        {
            // Establish Connection to MySQL Server
            establishConnection();

            try
            {
                // Write (copy from queries folder) the query
                string query = String.Format(@"
                                                SELECT month, numArticles, overall, ROUND(100*numArticles/overall,3) AS percentage
                                                FROM
                                                (
                                                SELECT month, monthnum, COUNT(publish_date) AS numArticles, overallCount AS overall
                                                FROM
                                                (
                                                SELECT DATE_FORMAT(STR_TO_DATE(publish_date, '%m/%d/%y'), '%M') AS month, 
                                                       DATE_FORMAT(STR_TO_DATE(publish_date, '%m/%d/%y'), '%m') AS monthnum,
	                                                   publish_date
                                                FROM news
                                                ) AS T1
                                                JOIN
                                                (
                                                SELECT COUNT(*) overallCount FROM news
                                                ) AS T2
                                                GROUP BY month, monthnum, overallCount
                                                ) AS T3
                                                ORDER BY monthnum;
                                            ");

                // Build a Command which holds the query and the location of the target server
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);

                // Retrieve the results into a DataReader
                MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader();

                // Output the header from the DataReader
                outputText.Text = reader.GetName(0) + "\t" + reader.GetName(1) + "\t" + reader.GetName(2) + "\t" + reader.GetName(3) + "\r\n";

                // Loop through the rows of the DataReader to output the values from the DataReader
                while (reader.Read())
                {
                    outputText.Text = outputText.Text + reader.GetString(0) + "\t" + reader.GetInt32(1) + "\t" + reader.GetInt32(2) + "\t" + String.Format("{0:F3}", reader.GetFloat(3)) + "\r\n";
                }

                // Close the DataReader
                reader.Close();
            }
            catch (Exception ex)
            {
                outputText.Text = ex.ToString();
            }
        }

        private void task8sql_Click(object sender, EventArgs e)
        {
            // Establish Connection to MySQL Server
            establishConnection();

            try
            {
                // Write (copy from queries folder) the query
                string query = String.Format(@"
                                                SELECT publisher, ROUND(AVG(reliability)*100, 3) AS percentage
                                                FROM news
                                                JOIN publisher_table
                                                USING (publisher_id)
                                                GROUP BY publisher
                                                ORDER BY percentage DESC, publisher;
                                            ");

                // Build a Command which holds the query and the location of the target server
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);

                // Retrieve the results into a DataReader
                MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader();

                // Output the header from the DataReader
                outputText.Text = reader.GetName(0) + "\t" + reader.GetName(1) + "\r\n";

                // Loop through the rows of the DataReader to output the values from the DataReader
                while (reader.Read())
                {
                    outputText.Text = outputText.Text + reader.GetString(0) + "\t" + String.Format("{0:F3}", reader.GetFloat(1)) + "\r\n";
                }

                // Close the DataReader
                reader.Close();
            }
            catch (Exception ex)
            {
                outputText.Text = ex.ToString();
            }
        }

        private void task9sql_Click(object sender, EventArgs e)
        {
            // Establish Connection to MySQL Server
            establishConnection();

            try
            {
                // Write (copy from queries folder) the query
                string query = String.Format(@"
                                                SELECT country, ROUND(AVG(news_guard_score),3) AS avg_news_score
                                                FROM news
                                                JOIN country_table
                                                USING (country_id)
                                                GROUP BY country
                                                ORDER BY AVG(news_guard_score) DESC, country ASC;
                                            ");

                // Build a Command which holds the query and the location of the target server
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);

                // Retrieve the results into a DataReader
                MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader();

                // Output the header from the DataReader
                outputText.Text = reader.GetName(0) + "\t" + reader.GetName(1) + "\r\n";

                // Loop through the rows of the DataReader to output the values from the DataReader
                while (reader.Read())
                {
                    outputText.Text = outputText.Text + reader.GetString(0) + "\t" + reader.GetFloat(1) + "\r\n";
                }

                // Close the DataReader
                reader.Close();
            }
            catch (Exception ex)
            {
                outputText.Text = ex.ToString();
            }
        }

        private void task10sql_Click(object sender, EventArgs e)
        {
            // Establish Connection to MySQL Server
            establishConnection();

            try
            {
                // Write (copy from queries folder) the query
                string query = String.Format(@"
                                                SELECT author, political_bias, COUNT(*) AS numArticles
                                                FROM news
                                                JOIN news_authors
                                                USING (news_id)
                                                JOIN author_table
                                                USING (author_id)
                                                JOIN political_bias_table
                                                USING (political_bias_id)
                                                GROUP BY author, political_bias
                                                ORDER BY author, COUNT(*) DESC, political_bias;
                                            ");

                // Build a Command which holds the query and the location of the target server
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySqlCommand(query, conn);

                // Retrieve the results into a DataReader
                MySql.Data.MySqlClient.MySqlDataReader reader = cmd.ExecuteReader();

                // Output the header from the DataReader
                outputText.Text = reader.GetName(0) + "\t" + reader.GetName(1) + "\t" + reader.GetName(2) + "\r\n";

                // Loop through the rows of the DataReader to output the values from the DataReader
                while (reader.Read())
                {
                    outputText.Text = outputText.Text + reader.GetString(0) + "\t" + reader.GetString(1) + "\t" + reader.GetInt32(2) + "\r\n";
                }

                // Close the DataReader
                reader.Close();
            }
            catch (Exception ex)
            {
                outputText.Text = ex.ToString();
            }
        }
    }
}
