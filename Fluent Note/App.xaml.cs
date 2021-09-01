using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Fluent_Note
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void EventSetter_OnHandler(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is TextBox)
            {
                (sender as TextBox).SelectAll();
            }
        }
    }
}
