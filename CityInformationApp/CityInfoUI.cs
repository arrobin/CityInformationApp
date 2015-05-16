using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CityInformationApp
{
    public partial class CityInfoUI : Form
    {
        City aCity = new City();
        private bool isUpdateMode = false;
        private int cityId = 0;
        public CityInfoUI()
        {
            InitializeComponent();
            ShowCityInfoInListView();
        }



        private void saveButton_Click(object sender, EventArgs e)
        {
            aCity.name = cityNameTextBox.Text;
            aCity.about = aboutTextBox.Text;
            aCity.country = countryTextBox.Text;

            if (isUpdateMode)
            {
                string msg = aCity.UpdateCity(aCity,cityId);
                MessageBox.Show(msg);
                ShowCityInfoInListView();
                saveButton.Text = "Save";
                cityId = 0;
                isUpdateMode = false;
                cityNameTextBox.Enabled = true;
            }
            else
            {
                string message = aCity.SaveCityInfornamtion(aCity);
                MessageBox.Show(message);
                ShowCityInfoInListView();
                cityNameTextBox.Clear();
                aboutTextBox.Clear();
                countryTextBox.Clear();
            }

            
        }

        private void ShowCityInfoInListView()
        {
            double serialNo = 1;
            showCityListView.Items.Clear();
            List<City> cities = aCity.GetAllCityInformation();
            foreach (var city in cities)
            {
                
                ListViewItem item = new ListViewItem(serialNo.ToString());
                item.SubItems.Add(city.name);
                item.SubItems.Add(city.about);
                item.SubItems.Add(city.country);

                showCityListView.Items.Add(item);
                serialNo++;


            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string searchText = searchTextBox.Text;

            if (searchByCityRadioButton.Checked)
            {
                ShowSearchByCityInListView(searchText);
            }
            else if(searchByCountryRadioButton.Checked)
            {
                ShowSearchByCountryItemInListView(searchText);
            }
        }

        private void ShowSearchByCityInListView(string searchText)
        {
            double serialNo = 1;
            showCityListView.Items.Clear();
            List<City> cities = aCity.GetAllSearechInformationByCity(searchText);
            foreach (var city in cities)
            {

                ListViewItem item = new ListViewItem(serialNo.ToString());
                item.SubItems.Add(city.name);
                item.SubItems.Add(city.about);
                item.SubItems.Add(city.country);

                showCityListView.Items.Add(item);
                serialNo++;


            }

        }

        private void ShowSearchByCountryItemInListView(string searchText)
        {
            double serialNo = 1;
            showCityListView.Items.Clear();

            List<City> cities = aCity.GetAllSearechInformationByCountry(searchText);
            foreach (var city in cities)
            {

                ListViewItem item = new ListViewItem(serialNo.ToString());
                item.SubItems.Add(city.name);
                item.SubItems.Add(city.about);
                item.SubItems.Add(city.country);

                showCityListView.Items.Add(item);
                serialNo++;


            }
        }

        private void showCityListView_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem item = showCityListView.SelectedItems[0];

            int id = int.Parse(item.Text.ToString());

            City city = aCity.GetCityById(id);

            if (city != null)
            {
                isUpdateMode = true;
                saveButton.Text = "Update";
                cityId = city.id;
               
                cityNameTextBox.Enabled = false;
                cityNameTextBox.Text = city.name;
                aboutTextBox.Text = city.about;
                countryTextBox.Text = city.country;
                
            }
        }
    }
}
