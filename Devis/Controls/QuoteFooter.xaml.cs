using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Devis.Annotations;
using Devis.Models;
using Devis.Tools;
using Devis.ViewModels;

namespace Devis.Controls
{
    /// <summary>
    /// Logique d'interaction pour QuoteFooter.xaml
    /// </summary>
    public partial class QuoteFooter 
    {
        public QuoteFooter()
        {
            InitializeComponent();
            Container.DataContext = new QuoteFooterViewModel();
        }

    }
}
