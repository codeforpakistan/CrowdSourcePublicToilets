using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using publicToilet.Resources;
using Windows.Devices.Geolocation;

namespace publicToilet
{
   

    public partial class MainPage : PhoneApplicationPage
    {
        // MobileServiceCollectionView implements ICollectionView (useful for databinding to lists) and 
        // is integrated with your Mobile Service to make it easy to bind your data to the ListView
        private MobileServiceCollection<TodoItem, TodoItem> items;

        private IMobileServiceTable<TodoItem> todoTable = App.MobileService.GetTable<TodoItem>();



        private async void RefreshMyList()
        {
            // This code refreshes the entries in the list view be querying the TodoItems table.
            // The query excludes completed TodoItems
            try
            {
                items = await todoTable
                    .Where(todoItem => todoItem.Complete == false)
                    .ToCollectionAsync();

        
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading items", MessageBoxButton.OK);
            }

        }

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            getCords();
        }

        private async void InsertTodoItem(TodoItem todoItem)
        {
            // This code inserts a new TodoItem into the database. When the operation completes
            // and Mobile Services has assigned an Id, the item is added to the CollectionView
            await todoTable.InsertAsync(todoItem);
            items.Add(todoItem);
        }

        private async void RefreshTodoItems()
        {
        }

        private async void UpdateCheckedTodoItem(TodoItem item)
        {
            // This code takes a freshly completed TodoItem and updates the database. When the MobileService 
            // responds, the item is removed from the list 
            await todoTable.UpdateAsync(item);
            items.Remove(item);
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            //RefreshTodoItems();
            NavigationService.Navigate(new Uri("/LandingPage.xaml", UriKind.Relative));
        
        }
        string lll;
        string loo;
        public async  void getCords()
        {

            try
            {
                Geolocator locator = new Geolocator();

                Geoposition position = await locator.GetGeopositionAsync();

                Geocoordinate coordinate = position.Coordinate;
                lll = coordinate.Latitude.ToString();
                loo = coordinate.Longitude.ToString();
                tbx_latitude.Text = lll;
                tbx_longitude.Text = loo;
                RefreshMyList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Check Your Connection please!");
            }
         
        }

        bool babych= false;
        bool dis = false;
        bool fem = false;
        bool mal= false;
        bool unis= false;
        bool payment = false;
        bool wheel= false;
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

            if (cx_babychange.IsChecked == true)
            {
                babych = true;
            }
            else 
            {
                babych = false;
            }

            if (cx_disables.IsChecked == true)
            {
                dis = true;
            }
            else
            {
                dis = false;
            }

            if (cx_female.IsChecked == true)
            {
                fem = true;
            }
            else
            {
                fem = false;
            }


            if (cx_male.IsChecked == true)
            {
                mal = true;
            }
            else
            {
                mal = false;
            }


            if (cx_unisex.IsChecked == true)
            {
                unis = true;
            }
            else
            {
                unis = false;
            }


            if (cx_payment.IsChecked == true)
            {
                payment = true;
            }
            else
            {
                payment = false;
            }


            if (cx_wheelchair.IsChecked == true)
            {
                wheel = true;
            }
            else
            {
                wheel = false;
            }

            if (lll== null) 
            {
                lll = "0.0";
            }
            if (loo == null)
            {
                loo = "0.0";
            }

                var todoItem = new TodoItem { Complete = false, Text = "toilet", Town= tbx_town.Text , Near_Famous = tbx_near.Text , Latitude = lll ,Longitude = loo, BabyChange = babych, Disables = dis , Female = fem , Male = mal , Payment = payment , Unisex = unis , WheelChair = wheel };
            InsertTodoItem(todoItem);
            MessageBox.Show("Toilet has been tagged!");
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void CheckBoxComplete_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            TodoItem item = cb.DataContext as TodoItem;
            item.Complete = true;
            UpdateCheckedTodoItem(item);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RefreshTodoItems();
        }


    }
}