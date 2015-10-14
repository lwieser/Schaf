﻿using Devis.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Devis.Controls
{
    /// <summary>
    /// Logique d'interaction pour QuoteHeaderClient.xaml
    /// </summary>
    public partial class QuoteHeaderClient : UserControl
    {
        public QuoteHeaderClient()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
