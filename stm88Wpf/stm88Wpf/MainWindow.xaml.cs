using System;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;

namespace Wpf.CartesianChart.BasicLine
{
    public partial class BasicLineExample : UserControl
    {
        public BasicLineExample()
        {
            InitializeComponent();
           
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {   
                    Title = "Channel 1",
                    Values = new ChartValues<double> { 0}
                },
                new LineSeries
                {
                    Title = "Channel 2",
                    Values = new ChartValues<double> { 0}
                }
            };

            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString("C");

            //modifying the series collection will animate and update the chart
            SeriesCollection.Add(new LineSeries
            {
                Values = new ChartValues<double> { 0 },
                LineSmoothness = 0 //straight lines, 1 really smooth lines
            });

            //modifying any series values will also animate and update the chart
            SeriesCollection[2].Values.Add(5d);

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private void startbtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            Random r1 = new Random();
            SeriesCollection[0].Values.Add(Convert.ToDouble(r1.Next(1,10)));
            Random r2 = new Random();
            SeriesCollection[1].Values.Add(Convert.ToDouble(r2.Next(1, 15)));
            Random r3 = new Random();
            SeriesCollection[2].Values.Add(Convert.ToDouble(r3.Next(1, 5)));

        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

      
    }
}
