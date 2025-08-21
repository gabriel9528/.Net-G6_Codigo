using System.Data;
using System.Data.SqlClient;

namespace ModelConnected
{
    public partial class Form1 : Form
    {
        private SqlConnection connection = new SqlConnection();
        private string connectionString = @"Data Source=DESKTOP-HUO2FBT\SQLEXPRESS;
                                            Initial Catalog=G6_Model_Connected;
                                            Integrated Security=true";

        private SqlCommand command;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshDataFlightAirPlaneType();
            RefreshDataFlight();
        }

        #region Flight
        private void RefreshDataFlightAirPlaneType()
        {
            connection.ConnectionString = connectionString;
            command = connection.CreateCommand();

            try
            {
                connection.Open();
                command.CommandText = "Select * from FlightInfo";
                SqlDataReader readerFlight = command.ExecuteReader();
                DataTable dataTableFlight = new DataTable();
                dataTableFlight.Load(readerFlight);

                dataGridView1.DataSource = dataTableFlight;

                comboBoxAirplaneType.DataSource = dataTableFlight;
                comboBoxAirplaneType.DisplayMember = "AirplaneType";
                comboBoxAirplaneType.ValueMember = "Id";

                readerFlight.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
        private void RefreshDataFlight()
        {
            connection.ConnectionString = connectionString;
            command = connection.CreateCommand();
            try
            {
                connection.Open();
                command.CommandText = "Select * from FlightInfo";

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    comboBoxSelect.DataSource = dataTable;
                    comboBoxSelect.DisplayMember = "FlightNumber";
                    comboBoxSelect.ValueMember = "Id";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
        private void LoadFlightDetails(int flightId)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.ConnectionString = connectionString;
                connection.Open();
            }

            command = connection.CreateCommand();

            try
            {
                string query = "Select * from FlightInfo where Id = @FlightId";
                command.CommandText = query;
                command.Parameters.AddWithValue("@FlightId", flightId);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    textBoxAirline.Text = reader["Airline"].ToString();
                    textBoxDestination.Text = reader["Destination"].ToString();
                    textBoxFlightNumber.Text = reader["FlightNumber"].ToString();

                    string airplaneType = reader["AirplaneType"].ToString();
                    comboBoxAirplaneType.SelectedValue = airplaneType;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
                command.Dispose();
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataFlight();
        }

        private bool isAirplaneTypeSelectedChanging = false;
        private void comboBoxSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isAirplaneTypeSelectedChanging)
            {
                DataRowView selectedRow = comboBoxSelect.SelectedItem as DataRowView;
                if (selectedRow != null)
                {
                    int selectedFlightById = Convert.ToInt32(selectedRow["Id"]);
                    if (selectedFlightById > 0)
                    {
                        LoadFlightDetails(selectedFlightById);
                        string airplaneType = selectedRow["AirplaneType"].ToString();

                        for (int i = 0; i < comboBoxAirplaneType.Items.Count; i++)
                        {
                            DataRowView item = comboBoxAirplaneType.Items[i] as DataRowView;
                            if (item["AirplaneType"].ToString() == airplaneType)
                            {
                                comboBoxAirplaneType.SelectedIndex = i;
                                break;
                            }
                        }
                    }

                }
            }
        }

        #endregion

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            string airline = textBoxAirline.Text;
            string destination = textBoxDestination.Text;
            string flightNumber = textBoxFlightNumber.Text;
            //string airplaneType = comboBoxAirplaneType.SelectedItem.ToString();
            string airplaneType = (comboBoxAirplaneType.SelectedItem as DataRowView)?["AirPlaneType"].ToString();


            if (string.IsNullOrWhiteSpace(airline) || string.IsNullOrWhiteSpace(destination) ||
                string.IsNullOrWhiteSpace(flightNumber) || string.IsNullOrWhiteSpace(airplaneType))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            connection.ConnectionString = connectionString;
            command = new SqlCommand("Insert into FlightInfo (Airline, FlightNumber, Destination, AirplaneType) " +
                "Values (@Airline, @FlightNumber, @Destination, @AirplaneType)", connection);
            command.Parameters.AddWithValue("@Airline", airline);
            command.Parameters.AddWithValue("@FlightNumber", flightNumber);
            command.Parameters.AddWithValue("@Destination", destination);
            command.Parameters.AddWithValue("@AirplaneType", airplaneType);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Flight added successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
                RefreshDataFlightAirPlaneType();
                RefreshDataFlight();
                connection.Dispose();
            }
        }
    }
}
 