using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Media.Imaging;
using WeatherInformationApp.Models;

namespace WeatherInformationApp
{
    public partial class MainWindow : Window
    {
        string url;
        string city;
        RootObject weatherResponse;
        int error = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        void Request(string city)
        {
            url = $"http://api.openweathermap.org/data/2.5/forecast?q={city}&units=metric&appid=50e7abc043c19f1c07479477507f1c70";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                string response;
                using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = streamReader.ReadToEnd();
                }
                weatherResponse = JsonConvert.DeserializeObject<RootObject>(response);
            }
            catch (WebException exeption)
            {
                city = "";
                ++error;
                MessageBox.Show("Не верное значение");
            }
           
        }
        int count = 0;

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            city = cityTextBox.Text;
            Request(city);
            cityTextBox.Text = "";

            if (count == 0)
            {
                Fill();
                ++count;
            }
            else
            {
                Zeroing();
                Fill();
            }
        }

        string uri = "https://openweathermap.org/img/w/";
        void Fill()
        {
            if (error==0)
            {
                cityTxtBlock.Text = city.ToUpperInvariant();
            }

            try
            {
                iconWeatherDay1.Source = new BitmapImage(new Uri(uri + weatherResponse.list[0].Weather[0].Icon + ".png"));
                iconWeatherDay2.Source = new BitmapImage(new Uri(uri + weatherResponse.list[8].Weather[0].Icon + ".png"));
                iconWeatherDay3.Source = new BitmapImage(new Uri(uri + weatherResponse.list[16].Weather[0].Icon + ".png"));
                iconWeatherDay4.Source = new BitmapImage(new Uri(uri + weatherResponse.list[24].Weather[0].Icon + ".png"));
                iconWeatherDay5.Source = new BitmapImage(new Uri(uri + weatherResponse.list[32].Weather[0].Icon + ".png"));

                tempDay1TextBlock.Text += weatherResponse.list[0].Main.Temp;
                tempDay2TextBlock.Text += weatherResponse.list[8].Main.Temp;
                tempDay3TextBlock.Text += weatherResponse.list[16].Main.Temp;
                tempDay4TextBlock.Text += weatherResponse.list[24].Main.Temp;
                tempDay5TextBlock.Text += weatherResponse.list[32].Main.Temp;

                pressDay1TextBlock.Text += weatherResponse.list[0].Main.Pressure;
                pressDay2TextBlock.Text += weatherResponse.list[8].Main.Pressure;
                pressDay3TextBlock.Text += weatherResponse.list[16].Main.Pressure;
                pressDay4TextBlock.Text += weatherResponse.list[24].Main.Pressure;
                pressDay5TextBlock.Text += weatherResponse.list[32].Main.Pressure;

                humidityDay1TextBlock.Text += weatherResponse.list[0].Main.Humidity;
                humidityDay2TextBlock.Text += weatherResponse.list[8].Main.Humidity;
                humidityDay3TextBlock.Text += weatherResponse.list[16].Main.Humidity;
                humidityDay4TextBlock.Text += weatherResponse.list[24].Main.Humidity;
                humidityDay5TextBlock.Text += weatherResponse.list[32].Main.Humidity;

                speedDay1TextBlock.Text += weatherResponse.list[0].Wind.Speed;
                speedDay2TextBlock.Text += weatherResponse.list[8].Wind.Speed;
                speedDay3TextBlock.Text += weatherResponse.list[16].Wind.Speed;
                speedDay4TextBlock.Text += weatherResponse.list[24].Wind.Speed;
                speedDay5TextBlock.Text += weatherResponse.list[32].Wind.Speed;
            }
            catch (NullReferenceException exeption)
            {
                MessageBox.Show(exeption.ToString());
            }
            error = 0;
        }
        void Zeroing()
        {
            cityTxtBlock.Text = "";
            iconWeatherDay1.Source = new BitmapImage(new Uri(uri));
            iconWeatherDay2.Source = new BitmapImage(new Uri(uri));
            iconWeatherDay3.Source = new BitmapImage(new Uri(uri));
            iconWeatherDay4.Source = new BitmapImage(new Uri(uri));
            iconWeatherDay5.Source = new BitmapImage(new Uri(uri));

            tempDay1TextBlock.Text = "Температура: ";
            tempDay2TextBlock.Text = "Температура: ";
            tempDay3TextBlock.Text = "Температура: ";
            tempDay4TextBlock.Text = "Температура: ";
            tempDay5TextBlock.Text = "Температура: ";

            pressDay1TextBlock.Text = "Давление: ";
            pressDay2TextBlock.Text = "Давление: ";
            pressDay3TextBlock.Text = "Давление: ";
            pressDay4TextBlock.Text = "Давление: ";
            pressDay5TextBlock.Text = "Давление: ";

            humidityDay1TextBlock.Text = "Влажность: ";
            humidityDay2TextBlock.Text = "Влажность: ";
            humidityDay3TextBlock.Text = "Влажность: ";
            humidityDay4TextBlock.Text = "Влажность: ";
            humidityDay5TextBlock.Text = "Влажность: ";

            speedDay1TextBlock.Text = "Скорость ветра: ";
            speedDay2TextBlock.Text = "Скорость ветра: ";
            speedDay3TextBlock.Text = "Скорость ветра: ";
            speedDay4TextBlock.Text = "Скорость ветра: ";
            speedDay5TextBlock.Text = "Скорость ветра: ";
        }
    }
}
