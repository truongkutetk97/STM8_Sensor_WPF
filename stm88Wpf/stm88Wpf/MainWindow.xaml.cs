using System;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using System.IO;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;
namespace Wpf.CartesianChart.BasicLine
{
    public partial class BasicLineExample : UserControl
    {
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        bool datarcv = false;
        int n = 0;
        UInt16 offsetvalue = 512;
        double scalevalue = 37.04;
        double sensor;
        double ampere;
        string show;
        SerialPort stm = new SerialPort();
        double lbl = 0;
        string[] lbll;
        public BasicLineExample()
        {
            
            InitializeComponent();
            timer.Tick += timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(100);
           

            
            //string[] ports = SerialPort.GetPortNames();
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
          
           Labels = new[] { "0" };
           // Labels[1] = "1";
          //  YFormatter = value => value.ToString("A");

            //modifying the series collection will animate and update the chart
           
            //modifying any series values will also animate and update the chart
            //SeriesCollection[2].Values.Add(5d);
            
            DataContext = this;
          
            
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private void timer_Tick(object sender, EventArgs e)
        {
            stm.Open();
            string[] arrList = stm.ReadLine().Split('\n');
            if (arrList[0].Contains("adc") == true)
            {

                arrList[0].Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
               // txt1.Text = arrList[0].Trim().Remove(0, 3);
                sensor = Convert.ToDouble(arrList[0].Replace("adc",""));
                ampere = (sensor - offsetvalue) * scalevalue; //1024 * 5120000 / 135; 
                datarcv = false;
                Random r1 = new Random();
                if (n >= 17)
                    SeriesCollection[0].Values.RemoveAt(0);
                else n++;
                SeriesCollection[0].Values.Add(ampere);
                
                txt1.Text = Convert.ToInt16(ampere).ToString() + "mA";
            } 
            stm.Close();

        }
        private void startbtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
           
           
            offsetvalue = Convert.ToUInt16( sensor); 
            
           

        }

        

        private void optionbtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            stm.PortName = cbx1.Text;
            stm.BaudRate = 115200;
            timer.Start();
            stm.Open();
            stm.Write("j");
            stm.Close();
        }

        

        private void cbx1_SelectionChanged(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cbx1.Items.Clear();
            foreach (string comport in ports)
            {
                cbx1.Items.Add(comport);
            }
        }

      
    }
}
