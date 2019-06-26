using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace TestSakila
{
    public partial class SakilaSearch : System.Web.UI.Page
    {

        string dbConn = ConfigurationManager.ConnectionStrings["SakilaConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable actors = new DataTable();

            //Populate the drop down list
            if (!IsPostBack)
            {
                using (SqlConnection con = new SqlConnection(dbConn))
                {
                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter("SELECT actor_id, CONCAT(first_name, ' ', last_name) AS Result FROM actor", con);
                        adapter.Fill(actors);

                        ActorList.DataSource = actors;
                        ActorList.DataValueField = "actor_id";
                        ActorList.DataTextField = "Result";
                        ActorList.DataBind();
                    }
                    catch (SqlException ex)
                    {
                        ExceptionLabel.Text = "Unable to connect to Sakila database! Error message:  " + ex.Message;
                    }
                }
            }
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sqlCon = new SqlConnection(dbConn))
            {
                try
                {
                    //Query to display films from selected actor

                    SqlCommand sqlCom = new SqlCommand($@"SELECT a.first_name, a.last_name, fi.title, COUNT(*) as pop
                                                            FROM actor as a 
                                                            INNER JOIN film_actor as fa ON a.actor_id = fa.actor_id 
                                                            INNER JOIN film as fi ON fi.film_id = fa.film_id
                                                            INNER JOIN inventory as i ON fi.film_id = i.film_id
                                                            INNER JOIN rental as r ON r.inventory_id = i.inventory_id 
                                                            WHERE a.actor_id = @actorList
                                                            GROUP BY a.first_name, a.last_name, fi.title
                                                            HAVING COUNT(*) > 0
                                                            ORDER BY pop DESC", sqlCon);

                    sqlCom.Parameters.AddWithValue("@actorList", ActorList.SelectedValue);

                    SqlDataAdapter sda = new SqlDataAdapter(sqlCom);
                    sda.Fill(dt);

                    //Sort datatable the bind to grid view
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                }
                catch (SqlException ex)
                {
                    ExceptionLabel.Text = "Unable to connect to Sakila database! Error message:  " + ex.Message;
                }
            }
        }         
    }
}