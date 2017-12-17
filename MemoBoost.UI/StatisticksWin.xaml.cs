using LiveCharts;
using LiveCharts.Wpf;
using MemoBoost.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MemoBoost.UI
{
    /// <summary>
    /// Логика взаимодействия для StatisticksWin.xaml
    /// </summary>
    public partial class StatisticksWin : Window
    {     
        public StatisticksWin()
        {
            InitializeComponent();
        }

        private void YearButton_Click(object sender, RoutedEventArgs e)
        {
            ColumnsChart cc = new ColumnsChart();
            cc.Labels = new string[3];
            cc.SeriesCollection = new SeriesCollection();
            ColumnSeries css = new ColumnSeries { Title = "Studied", Values = new ChartValues<int>() };
            ColumnSeries csr = new ColumnSeries { Title = "Reviewed", Values = new ChartValues<int>() };
            for (int i = 2; i >= 0; i--)
            {
                var styear = Factory.Default.GetStsRepository().Items.Where(s => s.Date.Year == DateTime.Now.AddYears(-i).Year & s.UserID == StudySession.Default.CurrentUserID).Sum(o => o.Studied);
                var revyear = Factory.Default.GetStsRepository().Items.Where(s => s.Date.Year == DateTime.Now.AddYears(-i).Year & s.UserID == StudySession.Default.CurrentUserID).Sum(o => o.Reviewed);
                cc.Labels[2 - i] = DateTime.Now.AddYears(-i).Year.ToString();
                css.Values.Add(styear);
                csr.Values.Add(revyear);
            }
            cc.SeriesCollection.Add(css);
            cc.SeriesCollection.Add(csr);
            DataContext = cc;
        }

        private void MonthButton_Click(object sender, RoutedEventArgs e)
        {
            ColumnsChart cc = new ColumnsChart();
            cc.Labels = new string[12];
            cc.SeriesCollection = new SeriesCollection();
            ColumnSeries css = new ColumnSeries { Title = "Studied", Values = new ChartValues<int>() };
            ColumnSeries csr = new ColumnSeries { Title = "Reviewed", Values = new ChartValues<int>() };
            for (int i = 11; i >= 0; i--)
            {
                var stmonth = Factory.Default.GetStsRepository().Items.Where(s => s.Date.Year == DateTime.Now.Year & s.Date.Month == DateTime.Now.AddMonths(-i).Month & s.UserID==StudySession.Default.CurrentUserID).Sum(o => o.Studied);
                var revmonth= Factory.Default.GetStsRepository().Items.Where(s => s.Date.Year == DateTime.Now.Year & s.Date.Month == DateTime.Now.AddMonths(-i).Month & s.UserID == StudySession.Default.CurrentUserID).Sum(o => o.Reviewed);
                cc.Labels[11 - i] = DateTime.Now.AddMonths(-i).Month.ToString();
                css.Values.Add(stmonth);
                csr.Values.Add(revmonth);
            }
            cc.SeriesCollection.Add(css);
            cc.SeriesCollection.Add(csr);
            DataContext = cc;
        }

        private void WeekButton_Click(object sender, RoutedEventArgs e)
        {
            ColumnsChart cc = new ColumnsChart();
            cc.Labels = new string[7];
            cc.SeriesCollection = new SeriesCollection();
            ColumnSeries css = new ColumnSeries { Title = "Studied", Values=new ChartValues<int>()};
            ColumnSeries csr = new ColumnSeries { Title = "Reviewed", Values = new ChartValues<int>() };
            for (int i = 6; i >= 0; i--)
            {
                var st = Factory.Default.GetStsRepository().Items.FirstOrDefault(s => s.Date.ToShortDateString() == DateTime.Now.AddDays(-i).ToShortDateString() & s.UserID == StudySession.Default.CurrentUserID);
                cc.Labels[6 - i] = DateTime.Now.AddDays(-i).ToShortDateString();
                if (st != null)
                {
                    css.Values.Add(st.Studied);
                    csr.Values.Add(st.Reviewed);
                }
                else
                {
                    css.Values.Add(0);
                    csr.Values.Add(0);
                }
            }
            cc.SeriesCollection.Add(css);
            cc.SeriesCollection.Add(csr);
            DataContext = cc;
        }

        private void TodayButton_Click(object sender, RoutedEventArgs e)
        {
            ColumnsChart cc = new ColumnsChart();
            cc.Labels = new string[1];
            cc.SeriesCollection = new SeriesCollection();
            var st =Factory.Default.GetStsRepository().Items.FirstOrDefault(s => s.Date.ToShortDateString() == DateTime.Now.ToShortDateString() & s.UserID == StudySession.Default.CurrentUserID);
            if (st != null)
            {
                ColumnSeries css = new ColumnSeries { Title = "Studied", Values = new ChartValues<int> { st.Studied } };
                ColumnSeries csr = new ColumnSeries { Title = "Reviewed", Values = new ChartValues<int> { st.Reviewed } };
                cc.SeriesCollection.Add(css);
                cc.SeriesCollection.Add(csr);
            }
            this.DataContext = cc;     
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;
            int n = Convert.ToInt32(b.Tag);
            ColumnsChart cc = new ColumnsChart();
            cc.Labels = new string[n];
            cc.SeriesCollection = new SeriesCollection();
            ColumnSeries css = new ColumnSeries { Title = "Studied", Values = new ChartValues<int>() };
            ColumnSeries csr = new ColumnSeries { Title = "Reviewed", Values = new ChartValues<int>() };
            for (int i = n-1; i >= 0; i--)
            {
                if (n == 12)
                {
                    var stmonth = Factory.Default.GetStsRepository().Items.Where(s => s.Date.Year == DateTime.Now.Year & s.Date.Month == DateTime.Now.AddMonths(-i).Month).Sum(o => o.Studied);
                    var revmonth = Factory.Default.GetStsRepository().Items.Where(s => s.Date.Year == DateTime.Now.Year & s.Date.Month == DateTime.Now.AddMonths(-i).Month).Sum(o => o.Reviewed);
                    cc.Labels[11 - i] = DateTime.Now.AddMonths(-i).ToShortDateString();
                    css.Values.Add(stmonth);
                    csr.Values.Add(revmonth);
                }
                else
                {
                    var styear = Factory.Default.GetStsRepository().Items.Where(s => s.Date.Year == DateTime.Now.AddYears(-i).Year).Sum(o => o.Studied);
                    var revyear = Factory.Default.GetStsRepository().Items.Where(s => s.Date.Year == DateTime.Now.AddYears(-i).Year).Sum(o => o.Reviewed);
                    cc.Labels[2 - i] = DateTime.Now.AddYears(-i).Year.ToString();
                    css.Values.Add(styear);
                    csr.Values.Add(revyear);
                }
            }
            cc.SeriesCollection.Add(css);
            cc.SeriesCollection.Add(csr);
            DataContext = cc;

        }
    }
}
