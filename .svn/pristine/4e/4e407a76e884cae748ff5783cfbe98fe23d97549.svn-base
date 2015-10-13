using System.Windows;
using System.Windows.Controls;
using Devis.Tools;
using Devis.ViewModels;

namespace Devis.Controls
{
    public  class LevelIconTemplateSelector : DataTemplateSelector
    {
        public DataTemplate FirstLevel { get; set; }
        public DataTemplate SecondLevel { get; set; }
        public DataTemplate OtherLevel { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var row =  UITools.FindAncestor<DataGridRow>(container);
            if (row == null)
                return OtherLevel;

            var line = row.DataContext as LineViewModel;
            if (line == null)
                return OtherLevel;

            switch (line.Level)
            {
                case 1:
                    return FirstLevel;
                case 2 :
                    return SecondLevel;
                default:
                    return OtherLevel;
            }
        }
    }
}
