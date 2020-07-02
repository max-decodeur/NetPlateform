using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HeavyClient.ViewModel;
using HeavyClient.View;
using HeavyClient.Model;

namespace HeavyClient
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private object _view;
        public object currentView { get => _view; set => this.setView(value); }

        public MainWindow()
        {
            // Set LoginView as default view.
            this.currentView = new LoginView();

            Client model = Client.Instance;
            model.mainWindow = this;

            //emulate message received
            //List<string> filesContent = new List<string>();
            //filesContent.Add(new FormattedFile("D:\\Projet\\A4-3 Dominante Dev\\FICHIERS CRYTES\\PA.txt").serialize());
            //Message message = new Message("decrypt", "0.1", filesContent.ToArray());
            //model.processReceivedMessage(message.serialize());

            //model.decryptFile(new FormattedFile("D:\\Projet\\A4-3 Dominante Dev\\FICHIERS CRYTES\\PB.txt"));

            InitializeComponent();

            // debug auto-close
            //this.Close();
        }

        private void setView(object view)
        {
            this._view = view;
            DataContext = view;
        }

        private void rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public bool tryToLogin(string login, string username)
        {
            return true;
        }
    }
}
