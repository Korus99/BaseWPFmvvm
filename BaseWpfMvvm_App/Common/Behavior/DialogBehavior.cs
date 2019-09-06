using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BaseWpfMvvm_App.Common.Control;

namespace BaseWpfMvvm_App.Common.Behavior
{
    public class DialogBehavior
    {
        #region Help Dialog
        /// <summary>
        ///     Help Dialog
        /// </summary>
        public static readonly DependencyProperty HelpDialogVisibleProperty = DependencyProperty.RegisterAttached(
            "HelpDialogVisible",
            typeof(bool), typeof(DialogBehavior), new PropertyMetadata(false, OnHelpDialogVisibleChange));

        private static HelpDialog HelpDialogWindow { get; set; }

        // Help Dialog
        public static void SetHelpDialogVisible(DependencyObject source, bool value)
        {
            source.SetValue(HelpDialogVisibleProperty, value);
        }

        public static bool GetHelpDialogVisible(DependencyObject source)
        {
            return (bool)source.GetValue(HelpDialogVisibleProperty);
        }

        private static void OnHelpDialogVisibleChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is Window parent))
            {
                return;
            }
            // when DialogVisible is set to true we create a new dialog
            // box and set its DataContext to that of its parent
            if ((bool)e.NewValue)
            {
                HelpDialogWindow = new HelpDialog
                {
                    DataContext = parent.DataContext,
                    Width = 350,
                    Height = 350,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                HelpDialogWindow.ShowDialog();
            }
            else
            {
                HelpDialogWindow.Close();
            }
        }

        #endregion

    }
}
