using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace ModelDisconnected
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> listAircrafts = new List<string>();

        DataSet1 dataSet1 = new DataSet1();

        #region FlightInfo

        DataSet1TableAdapters.FlightInfoTableAdapter flightInfoTableAdapter =
            new DataSet1TableAdapters.FlightInfoTableAdapter();

        DataSet1.FlightInfoDataTable flightInfoDataTable =
            new DataSet1.FlightInfoDataTable();

        #endregion


        #region UserInfo

        DataSet1TableAdapters.UserInfoTableAdapter userInfoTableAdapter =
            new DataSet1TableAdapters.UserInfoTableAdapter();

        DataSet1.UserInfoDataTable userInfoDataTable =
            new DataSet1.UserInfoDataTable();

        #endregion
        public MainWindow()
        {
            InitializeComponent();

            listAircrafts.Clear();
            listAircrafts.Add("Boeing 737");
            listAircrafts.Add("Boeing 747");
            listAircrafts.Add("Boeing 757");
            listAircrafts.Add("Boeing 767");
            listAircrafts.Add("Boeing 777");
            listAircrafts.Add("AirBus 123");
            listAircrafts.Add("AirBus 124");
            listAircrafts.Add("AirBus 125");

            comboBoxAircraft.ItemsSource = listAircrafts;

            RefreshDataGridFlight();

        }

        private void RefreshDataGridFlight()
        {
            flightInfoTableAdapter.Fill(dataSet1.FlightInfo);

            flightInfoTableAdapter.Fill(flightInfoDataTable);

            dataGridFlights.ItemsSource = flightInfoDataTable;

            comboBoxFlights.ItemsSource = flightInfoDataTable;
            comboBoxFlights.DisplayMemberPath = "FlightNumber";
            comboBoxFlights.SelectedValuePath = "Id";

            comboBoxNextFlight.ItemsSource = flightInfoDataTable;
            comboBoxNextFlight.DisplayMemberPath = "FlightNumber";
            comboBoxNextFlight.SelectedValuePath = "Id";

        }

        #region Flight

        private void btnAddFlight_Click(object sender, RoutedEventArgs e)
        {
            DataSet1.FlightInfoRow flightInfoRow = dataSet1.FlightInfo.NewFlightInfoRow();
            flightInfoRow.Airline = txtAirline.Text;
            flightInfoRow.FlightNumber = txtFlightNumber.Text;
            flightInfoRow.Destination = txtDestination.Text;
            flightInfoRow.AirPlaneType = comboBoxAircraft.SelectedItem.ToString();

            dataSet1.FlightInfo.Rows.Add(flightInfoRow);
            flightInfoTableAdapter.Update(dataSet1.FlightInfo);

            RefreshDataGridFlight();
        }

        private void btnUpdateFlight_Click(object sender, RoutedEventArgs e)
        {
            int selectedFlight = (int)comboBoxFlights.SelectedValue;
            DataRow[] dataRows = dataSet1.FlightInfo.Select("Id=" + selectedFlight);

            dataRows[0]["Airline"] = txtAirline.Text;
            dataRows[0]["FlightNumber"] = txtFlightNumber.Text;
            dataRows[0]["Destination"] = txtDestination.Text;
            dataRows[0]["AirPlaneType"] = comboBoxAircraft.SelectedItem.ToString();

            flightInfoTableAdapter.Update(dataSet1.FlightInfo);

            RefreshDataGridFlight();

        }

        private void btnDeleteFlight_Click(object sender, RoutedEventArgs e)
        {
            int selectedFlight = (int)comboBoxFlights.SelectedValue;
            DataRow[] dataRows = dataSet1.FlightInfo.Select("Id=" + selectedFlight);
            foreach (DataRow row in dataRows)
            {
                row.Delete();
            }

            flightInfoTableAdapter.Update(dataSet1.FlightInfo);

            RefreshDataGridFlight();

        }

        private void comboBoxFlights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedFlight = (int)comboBoxFlights.SelectedValue;

            DataRow[] dataRows = dataSet1.FlightInfo.Select("Id=" + selectedFlight);

            if(dataRows.Length > 0)
            {
                DataRow row = dataRows[0];
                txtAirline.Text = row["Airline"].ToString();
                txtFlightNumber.Text = row["FlightNumber"].ToString();
                txtDestination.Text = row["Destination"].ToString();
                comboBoxAircraft.SelectedItem = row["AirPlaneType"].ToString();
            }

        }

        #endregion
    }
}