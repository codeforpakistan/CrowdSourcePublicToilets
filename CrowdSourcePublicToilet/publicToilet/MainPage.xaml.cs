using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.WindowsAzure.MobileServices;
using Windows.Devices.Geolocation;
using System.Device.Location;
using System.Collections.ObjectModel;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Controls.Maps.Platform;
using Microsoft.Phone.Controls.Maps;
using Microsoft.Phone.Maps.Controls;

namespace publicToilet
{
    public partial class LandingPage : PhoneApplicationPage
    {
        private MobileServiceCollection<TodoItem, TodoItem> items;
        private MobileServiceCollection<TodoItem, TodoItem> Gen;
        private ObservableCollection<TodoItem> Nearitems = new ObservableCollection<TodoItem>();
        private ObservableCollection<TodoItem> Generic = new ObservableCollection<TodoItem>();
        private ObservableCollection<Map007> mapList = new ObservableCollection<Map007>();
        private IMobileServiceTable<TodoItem> todoTable = App.MobileService.GetTable<TodoItem>();


        double d1;
        double d2;
        double l;
        double lo;

        public void getMapsData() 
        {

            foreach (TodoItem item in items) 
            {
                if (item.Latitude == "") 
                {
                    l = 0.0;

                }
                else 
                {
                    l = Convert.ToDouble(item.Latitude);
                }
                if (item.Longitude == "")
                {
                    lo = 0.0;

                }
                else 
                {
                    lo = Convert.ToDouble(item.Longitude);
                }

                Pushpin pin1 = new Pushpin();
                pin1.Location = new GeoCoordinate(l,lo);
                pin1.Content = item.Town;
                mapReal.Children.Add(pin1);
            }
             
        }
        public LandingPage()
        {
            InitializeComponent();
            
            getCords();
           
        
        }



        string lll;
        string loo;

        
        public async void getCords()
        {
            try
            {
                Geolocator locator = new Geolocator();

                Geoposition position = await locator.GetGeopositionAsync();

                Geocoordinate coordinate = position.Coordinate;
                lll = coordinate.Latitude.ToString();
                loo = coordinate.Longitude.ToString();
                string Location = "Latitude = " + coordinate.Latitude + " Longitude = " + coordinate.Longitude;
                mapReal.ZoomLevel = 10;
                mapReal.Center = new GeoCoordinate(Convert.ToDouble(lll), Convert.ToDouble(Convert.ToDouble(loo)));
               
                mapReal.ZoomBarVisibility = System.Windows.Visibility.Visible;
               
                RefreshMyList();
                
            
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Check Your Connection please!");
            }
           
        
        }

      

           
        
        


        
        private async void RefreshMyList()
        {
            // This code refreshes the entries in the list view be querying the TodoItems table.
            // The query excludes completed TodoItems
            try
            {
                items = await todoTable
                    .Where(todoItem => todoItem.Complete == false)
                    .ToCollectionAsync();

                if (items.Count != 0)
                {
                    if (Nearitems.Count != 0) 
                    {
                        Nearitems.Clear();
                    }

                    foreach (TodoItem td in items)
                    {
                        d1=Convert.ToDouble(td.Latitude);
                        d2 = Convert.ToDouble(td.Longitude);
                       double finalDistance = DistanceInMetres(Convert.ToDouble(lll),Convert.ToDouble(loo),d1,d2);
                       if (finalDistance < 500) 
                       {
                           Nearitems.Add(td);
                       }
                    
                    }
                }
                getMapsData();            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading items", MessageBoxButton.OK);
            }

            lala_NearList.ItemsSource = Nearitems;
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Tag.xaml", UriKind.Relative));
        }

        private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        {
            RefreshMyList();
        }


        public static Double DistanceInMetres(double lat1, double lon1, double lat2, double lon2)
        {

            if (lat1 == lat2 && lon1 == lon2)
                return 0.0;

            var theta = lon1 - lon2;

            var distance = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) +
                           Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) *
                           Math.Cos(deg2rad(theta));

            distance = Math.Acos(distance);
            if (double.IsNaN(distance))
                return 0.0;

            distance = rad2deg(distance);
            distance = distance * 60.0 * 1.1515 * 1609.344;

            return (distance);
        }

        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        private async void Unisex_Click(object sender, RoutedEventArgs e)
        {
            if (km5.IsChecked == true || km10.IsChecked == true || all.IsChecked == true)
            {

                try
                {
                    Gen = await todoTable
                        .Where(todoItem => todoItem.Unisex == true)
                        .ToCollectionAsync();
                    // MessageBox.Show("kamyab");

                    if (Gen.Count != 0)
                    {
                        if (Generic.Count != 0)
                        {
                            Generic.Clear();
                        }
                        if (km10.IsChecked == true || km5.IsChecked == true)
                        {
                            foreach (TodoItem td in Gen)
                            {
                                d1 = Convert.ToDouble(td.Latitude);
                                d2 = Convert.ToDouble(td.Longitude);
                                double finalDistance = DistanceInMetres(Convert.ToDouble(lll), Convert.ToDouble(loo), d1, d2);
                                if (finalDistance < 800)
                                {
                                    Generic.Add(td);
                                }

                            }
                        }
                        else
                        {
                            foreach (TodoItem td in Gen)
                            {

                                Generic.Add(td);

                            }
                        }
                        lala_GenericList.ItemsSource = Generic;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Check Your Connection!");
                }
            }
            else
            {
                MessageBox.Show("Please select the distance");
            }
        }

        private async void disable_Click(object sender, RoutedEventArgs e)
        {
            if (km5.IsChecked == true || km10.IsChecked == true || all.IsChecked == true)
            {

                try
                {
                    Gen = await todoTable
                        .Where(todoItem => todoItem.Disables == true)
                        .ToCollectionAsync();
                    // MessageBox.Show("kamyab");

                    if (Gen.Count != 0)
                    {
                        if (Generic.Count != 0)
                        {
                            Generic.Clear();
                        }
                        if (km10.IsChecked == true || km5.IsChecked == true)
                        {
                            foreach (TodoItem td in Gen)
                            {
                                d1 = Convert.ToDouble(td.Latitude);
                                d2 = Convert.ToDouble(td.Longitude);
                                double finalDistance = DistanceInMetres(Convert.ToDouble(lll), Convert.ToDouble(loo), d1, d2);
                                if (finalDistance < 800)
                                {
                                    Generic.Add(td);
                                }

                            }
                        }
                        else
                        {
                            foreach (TodoItem td in Gen)
                            {

                                Generic.Add(td);

                            }
                        }
                        lala_GenericList.ItemsSource = Generic;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Check Your Connection!");
                }
            }
            else
            {
                MessageBox.Show("Please select the distance");
            }
      
        }
        
        
        /// <summary>
        /// Summary ye hai beta je k ye app shugal he shugal mai ban gai hai :P
        /// Bigar gayi wo muj sai kal , 
        /// Mazak hi mazak mai
        /// jo mai nai you kaha chawal
        /// Mazak hi mazak mai
        /// Kici ki aik wife aur kici ki aik be nahe
        /// wo le Uri double double mazak hi mazak mai :P lalallalalalla 
        /// gagagagagaga
        /// Durrrr fittay muuu Geo Location
        /// </summary>
        /// 

        private async void Children_Click(object sender, RoutedEventArgs e)
        {
            if (km5.IsChecked == true || km10.IsChecked == true || all.IsChecked == true)
            {

                try
                {
                    Gen = await todoTable
                        .Where(todoItem => todoItem.BabyChange == true)
                        .ToCollectionAsync();
                    // MessageBox.Show("kamyab");

                    if (Gen.Count != 0)
                    {
                        if (Generic.Count != 0)
                        {
                            Generic.Clear();
                        }
                        if (km10.IsChecked == true || km5.IsChecked == true)
                        {
                            foreach (TodoItem td in Gen)
                            {
                                d1 = Convert.ToDouble(td.Latitude);
                                d2 = Convert.ToDouble(td.Longitude);
                                double finalDistance = DistanceInMetres(Convert.ToDouble(lll), Convert.ToDouble(loo), d1, d2);
                                if (finalDistance < 800)
                                {
                                    Generic.Add(td);
                                }

                            }
                        }
                        else
                        {
                            foreach (TodoItem td in Gen)
                            {

                                Generic.Add(td);

                            }
                        }
                        lala_GenericList.ItemsSource = Generic;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Check Your Connection!");
                }
            }
            else
            {
                MessageBox.Show("Please select the distance");
            }
      
        }

        private async void Money_Click(object sender, RoutedEventArgs e)
        {
            if (km5.IsChecked == true || km10.IsChecked == true || all.IsChecked == true)
            {

                try
                {
                   
                    Gen = await todoTable
                        .Where(todoItem => todoItem.Payment == true)
                        .ToCollectionAsync();
                    // MessageBox.Show("kamyab");

                    if (Gen.Count != 0)
                    {
                        if (Generic.Count != 0)
                        {
                            Generic.Clear();
                        }
                        if (km10.IsChecked == true || km5.IsChecked == true)
                        {
                            foreach (TodoItem td in Gen)
                            {
                                d1 = Convert.ToDouble(td.Latitude);
                                d2 = Convert.ToDouble(td.Longitude);
                                double finalDistance = DistanceInMetres(Convert.ToDouble(lll), Convert.ToDouble(loo), d1, d2);
                                if (finalDistance < 800)
                                {
                                    Generic.Add(td);
                                }

                            }
                        }
                        else
                        {
                            foreach (TodoItem td in Gen)
                            {

                                Generic.Add(td);

                            }
                        }
                        lala_GenericList.ItemsSource = Generic;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Check Your Connection!");
                }
            }
            else
            {
                MessageBox.Show("Please select the distance");
            }
        }

        private async void Parking_Click(object sender, RoutedEventArgs e)
        {
            if (km5.IsChecked == true || km10.IsChecked == true || all.IsChecked == true)
            {

                try
                {
                    Gen = await todoTable
                        .Where(todoItem => todoItem.Complete == false)
                        .ToCollectionAsync();
                    // MessageBox.Show("kamyab");

                    if (Gen.Count != 0)
                    {
                        if (Generic.Count != 0)
                        {
                            Generic.Clear();
                        }
                        if (km10.IsChecked == true || km5.IsChecked == true)
                        {
                            foreach (TodoItem td in Gen)
                            {
                                d1 = Convert.ToDouble(td.Latitude);
                                d2 = Convert.ToDouble(td.Longitude);
                                double finalDistance = DistanceInMetres(Convert.ToDouble(lll), Convert.ToDouble(loo), d1, d2);
                                if (finalDistance < 800)
                                {
                                    Generic.Add(td);
                                }

                            }
                        }
                        else
                        {
                            foreach (TodoItem td in Gen)
                            {

                                Generic.Add(td);

                            }
                        }


                    }
                    lala_GenericList.ItemsSource = Generic;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Check Your Connection!");
                }
            }
            else
            {
                MessageBox.Show("Please select the distance");
            }
        }

        private void sms_menuItem_Click(object sender, RoutedEventArgs e)
        {
            TodoItem selectedItem = (sender as MenuItem).DataContext as TodoItem;
            SmsComposeTask smsComposeTask = new SmsComposeTask();
            smsComposeTask.Body = selectedItem.Town + "\n" + selectedItem.Near_Famous + "\n" + selectedItem.Latitude + "\n" + selectedItem.Longitude+"\n\n"+"via OpenPublicToilets";
            smsComposeTask.Show();

        }

        private void bing_menuItem_Click(object sender, RoutedEventArgs e)
        {
               TodoItem selectedItem = (sender as MenuItem).DataContext as TodoItem;
         
            EmailComposeTask task = new EmailComposeTask();
            task.Body = selectedItem.Town + "\n" + selectedItem.Near_Famous + "\n" + selectedItem.Latitude + "\n" + selectedItem.Longitude + "\n\n" + "via OpenPublicToilets";
            task.Subject = "Hey I found a toilet";
            task.Show();
        }

       
    }
}