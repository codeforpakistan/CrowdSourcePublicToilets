using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace publicToilet
{
    public partial class Spam : PhoneApplicationPage
    {
        public Spam()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string town = "";
            if (NavigationContext.QueryString.TryGetValue("town", out town))
            {
                tbl_town.Text = town;
            }
            string asist = "";
            if (NavigationContext.QueryString.TryGetValue("asist", out asist))
            {
                tbl_assist.Text = asist;
            }
            string lat = "";
            if (NavigationContext.QueryString.TryGetValue("lat", out lat))
            {
                tbl_latitude.Text = lat;
            }
            string lon = "";
            if (NavigationContext.QueryString.TryGetValue("lon", out lat))
            {
                tbl_longitude.Text = lon;
            }

        
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            if (tbx_cnic.Text == "" || tbx_name.Text == "" || tbx_Report.Text == "")
            {
                MessageBox.Show("Enter the fields please!");
            }
            else 
            {
                if (cx_visited.IsChecked == true)
                {

                    MessageBox.Show("Reported, Thanks for your contribution!");
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                    
                }
                else 
                {
                    MessageBox.Show("You must have visited the toilet to report!");
                    NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

                }
            }
        }

    }
}