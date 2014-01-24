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
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Check Your Connection please!");
            }
         
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {

           
                var todoItem = new TodoItem {  Town= tbx_town.Text , Near_Famous = tbx_near.Text , Latitude = lll ,Longitude = loo  };
            InsertTodoItem(todoItem);
        
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