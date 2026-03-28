using layout.service;
using layout.view.Main_Window;
using layout.view.Nguoidung;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace layout.view.nguoidung
{
    /// <summary>
    /// Interaction logic for nguoidungPage.xaml
    /// </summary>
    public partial class nguoidungPage : Page
    {
        private readonly Nguoidungservice service = new Nguoidungservice();

        public nguoidungPage()
        {
            InitializeComponent();
            loadNguoiDung();
        }

        private void loadNguoiDung()
        {
            DataTable data = service.getAllNguoidung();
            NguoidungGrid.ItemsSource = data.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new Themnguoidung());
            }
        }

        private void btnDetail(object sender, RoutedEventArgs e)
        {
            DataRowView row = getRowFromActionButton(sender);
            if (row == null)
            {
                return;
            }

            int userId = Convert.ToInt32(row["ma_nguoidung"]);
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new UserDetailPage(userId));
            }
        }

        private void btnUpdate(object sender, RoutedEventArgs e)
        {
            DataRowView row = getRowFromActionButton(sender);
            if (row == null)
            {
                return;
            }

            int userId = Convert.ToInt32(row["ma_nguoidung"]);
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new UpdateNguoidungPage(userId));
            }
        }

        private void btnDelete(object sender, RoutedEventArgs e)
        {
            DataRowView row = getRowFromActionButton(sender);
            if (row == null)
            {
                return;
            }

            int userId = Convert.ToInt32(row["ma_nguoidung"]);
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new DeleteNguoidungPage(userId));
            }
        }

        private DataRowView getRowFromActionButton(object sender)
        {
            Button actionButton = sender as Button;
            if (actionButton == null)
            {
                return null;
            }

            return actionButton.DataContext as DataRowView;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new AdminDashboardPage());
            }
        }
    }
}
