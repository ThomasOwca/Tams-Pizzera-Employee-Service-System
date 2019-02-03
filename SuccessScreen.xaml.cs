// Author:  Thomas Owca
// Project: Tams' Pizzeria
// File:    SuccessScreen.xaml.cs

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
using System.Windows.Threading;

namespace Tams__Pizzeria
{
    /// <summary>
    /// This window is the success screen which is shown only when the log in is successful on the main window.
    /// It acts as display piece which in reality does nothing more than just allows the initialization of the next
    /// window which is the service window. It basically welcomes the user to the Employee Service System Portal for
    /// Tams' Pizzeria.
    /// </summary>
    public partial class SuccessScreen : Window
    {
        public SuccessScreen()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Kills any background process if the .exe gets closed 
            // but keeps running in the background.
            System.Diagnostics.Process.GetCurrentProcess().Kill(); 
        }
    }
}
