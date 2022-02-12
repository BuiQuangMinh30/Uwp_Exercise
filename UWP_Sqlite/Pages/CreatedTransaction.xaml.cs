using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public sealed partial class CreatedTransaction : Page
    {
        private string checkedCalendarDatePicker;
        public CreatedTransaction()
        {
            this.InitializeComponent();
        }
        private void CalendarDatePicker_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            CalendarDatePicker cdp = sender as CalendarDatePicker;
            checkedCalendarDatePicker = cdp.Date.Value.ToString("dd-MM-yyyy");
        }

        private async void InsertForTransaction(object sender, RoutedEventArgs e)
        {
            Person personal = new Person()
            {
                Name = name.Text,
                Detail = detail.Text,
                Description = description.Text,
                Money = double.Parse(money.Text),
                CreatedDate = DateTime.Parse(checkedCalendarDatePicker),
                Category = int.Parse(category.Text),
            };

            ContentDialog contentDialog = new ContentDialog();
            if (DBInitialize.InsertTables(personal))
            {
                contentDialog.Title = "Acction success";
                contentDialog.Content = "Them thanh cong";

                name.Text = "";
                detail.Text = "";
                description.Text = "";
                money.Text = "";
                category.Text = "";
            }
            else
            {
                contentDialog.Title = "Acction fail";
                contentDialog.Content = "Them that bai";
            }
            contentDialog.CloseButtonText = "OK";
            await contentDialog.ShowAsync();
        }

        private void TextMustBeNumber(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void ListTransactionButton(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.DataPage));
        }
    }
}
