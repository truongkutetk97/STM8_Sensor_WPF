using System;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using System.IO;
using System.IO.Ports;

namespace Wpf.CartesianChart.BasicLine
{
    public partial class BasicLineExample : UserControl
    {
        SerialPort stm = new SerialPort();
        public BasicLineExample()
        {
            InitializeComponent();
      
            stm.PortName = "COM8";
            stm.BaudRate = 115200;
            
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Channel 1",
                    Values = new ChartValues<double> { 1}
                },
                new LineSeries
                {
                    Title = "Channel 2",
                    Values = new ChartValues<double> { 1}
                },
                new LineSeries
                {
                    Title = "Channel 3",
                    Values = new ChartValues<double> { 1}
                }
            };

            Labels = new[] { "0","0,5" };
          //  YFormatter = value => value.ToString("A");

            //modifying the series collection will animate and update the chart
           
            //modifying any series values will also animate and update the chart
            //SeriesCollection[2].Values.Add(5d);

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private void startbtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            stm.Open();
            stm.Write("j");
            stm.Close();
            Random r1 = new Random();
            SeriesCollection[0].Values.Add(Convert.ToDouble(r1.Next(1,10)));
            Random r2 = new Random();
            SeriesCollection[1].Values.Add(Convert.ToDouble(r2.Next(1, 15)));
            Random r3 = new Random();
            SeriesCollection[2].Values.Add(Convert.ToDouble(r3.Next(1, 5)));
            
        }

        

        private void optionbtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            stm.Open();
            stm.Write("i");
            //stm.r
            stm.Close();
            double dd = 123.1;
            txt1.Text =  dd.ToString();
            

        }
    }
}
