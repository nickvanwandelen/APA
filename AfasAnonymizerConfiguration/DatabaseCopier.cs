using AfasAnonymizerConsole;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfasAnonymizerConfiguration
{
    public static class DatabaseCopier
    {
        public static void CopyProfitDatabase()
        {
            AnonConsole.WriteToConsole("Copying database");

            Server profitServer = new Server(new ServerConnection(Config.SERVERNAME));
            Server copyToServer = new Server(new ServerConnection("NL1-NWA"));
            Database profitDatabase = profitServer.Databases[Config.TARGETDATABASE];
            Database copiedDatabase = new Database(copyToServer, Config.COPYNAME);
            copiedDatabase.Create();


            Backup backup = new Backup();
            backup.Action = BackupActionType.Database;
            backup.BackupSetName = Config.TARGETDATABASE + "_Copy";
            backup.Fil

        }
    }
}
