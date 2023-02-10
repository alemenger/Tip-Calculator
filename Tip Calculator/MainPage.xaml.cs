using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Tip_Calculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private SolidColorBrush errorBrush = new SolidColorBrush(Windows.UI.Colors.Red);
        private Brush correctBrush = null;
        private double percent;

        public MainPage()
        {
            this.InitializeComponent();
            if (correctBrush == null)
                correctBrush = txtInput.Foreground;
            percent = 18;
        }
        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtInput.Foreground = correctBrush;
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Text = "";
            lblTipAmount.Text = "Tip: $0.00";
            ddlTip.SelectedIndex = 3;
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            //First check if anything has been entered
            if (!String.IsNullOrEmpty(txtInput.Text))
            {
                double billAmount;
                double tip;
                //See if you can convert it to Double
                if (!Double.TryParse(txtInput.Text, out billAmount))
                {
                    txtInput.Foreground = errorBrush;
                }
                else if (billAmount < 0)//Check for negative amount
                {
                    txtInput.Foreground = errorBrush;
                }
                else
                {
                    myStoryboard.Begin();
                    tip = TipCalc.tip(billAmount, percent);
                    lblTipAmount.Text = "Tip: " + tip.ToString("c");
                }
            }
        }

        private void ddlTip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox ddl = (ComboBox)sender;
            ComboBoxItem selPercent = (ComboBoxItem)ddl.SelectedItem;
            percent = Convert.ToDouble(selPercent.Content.ToString());
        }
    }
}
