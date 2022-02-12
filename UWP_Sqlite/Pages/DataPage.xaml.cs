using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_Sqlite.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DataPage : Page
    {
        public List<Person> listPerson;
        public Person personal;
        private string checkedStartDate;
        private string checkedEndDate;
        public DataPage()
        {
            this.InitializeComponent();
            this.Loaded += DataPage_Loaded;
        }

        private void DataPage_Loaded(object sender, RoutedEventArgs e)
        {
            listPerson = DBInitialize.GetList();
            Debug.WriteLine(listPerson);
            ListDataGridTransaction.ItemsSource = listPerson;
            btnTotalMoney.Text = DBInitialize.totalMoney.ToString();
        }
        private void CreateTransactionButton(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.CreatedTransaction));
        }

        private void ListDataGridTransaction_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            personal = ListDataGridTransaction.SelectedItem as Person;
            if (personal != null)
            {
                btnName.Text = personal.Name.ToString();
                btnDescription.Text = personal.Description.ToString();
                btnDetail.Text = personal.Detail.ToString();
                btnMoney.Text = Convert.ToDouble(personal.Money).ToString("#,###", cul.NumberFormat) + " USD";
                btnCreatedDate.Text = personal.CreatedDate.ToString("dd-MM-yyyy");
                btnCategory.Text = personal.Category.ToString();

            }
            else
            {
                btnName.Text = "";
                btnDescription.Text = "";
                btnDetail.Text = "";
                btnMoney.Text = "";
                btnCreatedDate.Text = "";
                btnCategory.Text = "";
            }
        }

        private void Search_Category(object sender, RoutedEventArgs e)
        {
            personal = ListDataGridTransaction.SelectedItem as Person;
            listPerson = DBInitialize.ListTransactionByCategory(Convert.ToInt32(btnCategory));
            Debug.WriteLine(listPerson);
            ListDataGridTransaction.ItemsSource = listPerson;
        }
    }
}
