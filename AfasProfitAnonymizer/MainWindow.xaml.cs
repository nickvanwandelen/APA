using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using AfasAnonymizerConsole;
using AfasAnonymizerConfiguration;

namespace AfasProfitAnonymizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = AnonConsole.Message;
        }


        #region AnonymizeHandling
        private void HandleTestConnection(object sender, RoutedEventArgs e)
        {
            AnonConsole.ClearConsole();

            bool comparison = (bool)ShouldCompareInput.IsChecked;
            if (!comparison)
            {
                try
                {
                    int percentage = Int32.Parse(ComparisonPercentageInput.Text);

                    if (percentage <= 0 || percentage > 100)
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    AnonConsole.WriteToConsole("Percentage is niet een geldig nummer", 2);
                    return;
                }
            }
            

            AnonConsole.WriteToConsole("Testing connection");
            int resultCode = DatabaseTester.TestConnection(this.ServerNameInput.Text, this.TargetDatabaseNameInput.Text, this.ComparisonDatabaseNameInput.Text, (bool)this.ShouldCompareInput.IsChecked);

            switch (resultCode)
            {
                case -1:
                    AnonConsole.WriteToConsole("Niet alle velden zijn ingevuld", 2);
                    StartAnonymizing.IsEnabled = false;
                    break;
                case 0:
                    AnonConsole.WriteToConsole("Connectie naar server en database(s) succesvol!", 0);
                    StartAnonymizing.IsEnabled = true;
                    break;
                case 1:
                    AnonConsole.WriteToConsole("Connectie naar server " + ServerNameInput.Text + " gefaald", 2);
                    StartAnonymizing.IsEnabled = false;
                    break;
                case 2:
                    AnonConsole.WriteToConsole("Connectie naar " + this.TargetDatabaseNameInput.Text + " gefaald", 2);
                    StartAnonymizing.IsEnabled = false;
                    break;
                case 3:
                    AnonConsole.WriteToConsole("Connectie naar " + this.ComparisonDatabaseNameInput.Text + " gefaald", 2);
                    StartAnonymizing.IsEnabled = false;
                    break;
                case 5:
                    AnonConsole.WriteToConsole("Connectie naar beide databases gefaald", 2);
                    StartAnonymizing.IsEnabled = false;
                    break;
            }
            StartAnonymizing.IsEnabled = true;
        }

        private void HandleStartAnonymizing(object sender, RoutedEventArgs e)
        {
            ServerNameInput.IsEnabled = false;
            TargetDatabaseNameInput.IsEnabled = false;
            ComparisonDatabaseNameInput.IsEnabled = false;
            ShouldCompareInput.IsEnabled = false;
            ShouldCreateCopy.IsEnabled = false;

            Config.SERVERNAME = ServerNameInput.Text;
            Config.TARGETDATABASE = TargetDatabaseNameInput.Text;
            Config.COMPAREDATABASE = ComparisonDatabaseNameInput.Text;

            if (ComparisonPercentageInput.Text.Equals(string.Empty))
            {
                Config.PERCENTAGEMATCH = -1;
            }
            else
            {
                Config.PERCENTAGEMATCH = Int32.Parse(ComparisonPercentageInput.Text);
            }
            

            bool comparison = (bool)ShouldCompareInput.IsChecked;
            bool copy = (bool)ShouldCreateCopy.IsChecked;

            Config.SHOULDCOMPAREDATABASE = comparison;
            Config.SHOULDCOPYDATABASE = copy;
            Config.Start();
        }
        #endregion

        #region LoggingHandling
        private void ClearConsole(object sender, RoutedEventArgs e)
        {
            AnonConsole.ClearConsole();
        }
        #endregion

        #region Other
        private void HandleChangeShouldCompare(object sender, RoutedEventArgs e)
        {
            if(ShouldCompareInput.IsChecked == null)
            {
                ComparisonDatabaseNameInput.IsEnabled = false;
                ComparisonPercentageInput.IsEnabled = false;
            }
            else
            {
                ComparisonDatabaseNameInput.IsEnabled = (bool)!ShouldCompareInput.IsChecked;
                ComparisonPercentageInput.IsEnabled = (bool)!ShouldCompareInput.IsChecked;
            }
        }

        private void CheckForNumberInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            TextBox tb = sender as TextBox;
            e.Handled = Regex.IsMatch(tb.Text, "[^0-9.-]+");
        }
        #endregion
    }
}
