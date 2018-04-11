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
        }

        #region AnonymizeHandling
        private void HandleTestConnection(object sender, RoutedEventArgs e)
        {
            ClearConsole();
            WriteToConsole("Testing connection");
            int resultCode = DatabaseTester.TestConnection(this.ServerNameInput.Text, this.TargetDatabaseNameInput.Text, this.ComparisonDatabaseNameInput.Text, (bool)this.ShouldCompareInput.IsChecked);

            switch (resultCode)
            {
                case -1:
                    WriteToConsole("Niet alle velden zijn ingevuld", 2);
                    StartAnonymizing.IsEnabled = false;
                    break;
                case 0:
                    WriteToConsole("Connectie naar server en database(s) succesvol!", 0);
                    StartAnonymizing.IsEnabled = true;
                    break;
                case 1:
                    WriteToConsole("Connectie naar server " + ServerNameInput.Text + " gefaald", 2);
                    StartAnonymizing.IsEnabled = false;
                    break;
                case 2:
                    WriteToConsole("Connectie naar " + this.TargetDatabaseNameInput.Text + " gefaald", 2);
                    StartAnonymizing.IsEnabled = false;
                    break;
                case 3:
                    WriteToConsole("Connectie naar " + this.ComparisonDatabaseNameInput.Text + " gefaald", 2);
                    break;
                case 5:
                    WriteToConsole("Connectie naar beide databases gefaald", 2);
                    StartAnonymizing.IsEnabled = false;
                    break;
            }
        }

        private void HandleStartAnonymizing(object sender, RoutedEventArgs e)
        {
            WriteToConsole("Appelsap");
        }
        #endregion

        #region LoggingHandling
        /// <summary>
        /// Writes an object.ToString() to the logging output for the user to see.
        /// </summary>
        /// <param name="objectToWrite">The object who's ToString() will be showed</param>
        /// <param name="severityOfMessage">The severity of the message
        /// -1 = no severity prefix
        /// 0 (default) = INFO.
        /// 1 = WARNING.
        /// 2 = ERROR.
        /// 3 = FATAL ERROR.
        /// </param>
        public void WriteToConsole(object objectToWrite, int severityOfMessage = 0)
        {
            string _severity = string.Empty;
            switch (severityOfMessage)
            {
                case -1:
                    _severity = string.Empty;
                    break;
                case 0:
                    _severity = "INFO: ";
                    break;
                case 1:
                    _severity = "WARNING: ";
                    break;
                case 2:
                    _severity = "ERROR: ";
                    break;
                case 3:
                    _severity = "FATAL ERROR: ";
                    break;
            }

            this.LogFeedback.Text += "\n" + _severity + objectToWrite.ToString();
        }

        public void ClearConsole()
        {
            this.LogFeedback.Text = string.Empty;
        }

        private void ClearConsole(object sender, RoutedEventArgs e)
        {
            this.LogFeedback.Text = string.Empty;
        }
        #endregion

        #region Other
        private void HandleChangeShouldCompare(object sender, RoutedEventArgs e)
        {
            if(ShouldCompareInput.IsChecked == null)
            {
                ComparisonDatabaseNameInput.IsEnabled = false;
            }
            else
            {
                ComparisonDatabaseNameInput.IsEnabled = (bool)!ShouldCompareInput.IsChecked;
            }
        }
        #endregion
    }
}
