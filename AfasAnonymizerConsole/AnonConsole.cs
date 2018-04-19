using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfasAnonymizerConsole
{
    public class AnonConsole
    {
        private static string _message;

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        public static string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message += value;
                OnStaticPropertyChange("Message");
            }
        }

        public static void WriteToConsole(object message, int severityOfMessage = 0)
        {
            string _severity = "\n";

            switch (severityOfMessage)
            {
                case -1:
                    _severity += string.Empty;
                    break;
                case 0:
                    _severity += "INFO: ";
                    break;
                case 1:
                    _severity += "WARNING: ";
                    break;
                case 2:
                    _severity += "ERROR: ";
                    break;
                case 3:
                    _severity += "FATAL ERROR: ";
                    break;
                case 10:
                    _severity += "START: ";
                    break;
                case 11:
                    _severity += "COMPLETE: ";
                    break;
              
            }

            Message += _severity + message.ToString();
        }

        public static void ClearConsole()
        {
            _message = string.Empty;
            OnStaticPropertyChange("Message");
        }

        protected static void OnStaticPropertyChange(string name) => StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
    }
}
