using System;
using System.Windows;

namespace Devis.Controls
{
    public static class MessageBoxHelper
    {
        public static void ShowError(Exception exception)
        {
            MessageBox.Show(exception.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}