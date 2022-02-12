using SQLitePCL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP_Sqlite
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DBInitialize.CreateTables();
            this.Loaded += MainPage_Loaded;
           
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            List<Person> persons = DBInitialize.GetList();
            foreach (Person person in persons)
            {
                ListBoxItem item = new ListBoxItem();
                item.DataContext = person;
                item.ContentTemplate = Application.Current.Resources["PersonListItem"] as DataTemplate;
                PersonList.Items.Add(item);
            }
        }

        /*
        private async void AddToDB_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DBInitialize.InsertTables(txtName.Text, txtDesc.Text, 
                    Convert.ToDouble(txtMoney.Text),
                    Convert.ToDateTime(txtDate.Date.ToString("dd-MM-yyyy")),
                   Convert.ToInt32(txtCategory.Text));
                txtName.Text = "";
                txtDesc.Text = "";
                txtMoney.Text = "";
                txtDate.SelectedDate = null;
                txtCategory.Text = "";
            }
            catch(Exception ex)
            {
                MessageDialog msg = new MessageDialog(ex.Message);
                await msg.ShowAsync();
            }
        }
        */
    }
}
