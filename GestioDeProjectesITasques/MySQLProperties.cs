using NBA_BD.db;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace DB_MySQL.db
{
    public class MySQLProperties
    {
        private ApplicationDataContainer localSettings;

        private TextBox server;
        private TextBox dataBase;
        private TextBox user;
        private PasswordBox password;

        public MySQLProperties()
        {
            
            localSettings = ApplicationData.Current.LocalSettings;
            // Save a setting locally on the device
            if (localSettings != null)
            {
                localSettings.Values["server"] = "51.68.224.27";
                localSettings.Values["dataBase"] = "dam2_areyes";
                localSettings.Values["user"] = "dam2-areyes";
                localSettings.Values["password"] = "2681X";
            }
            
            if (localSettings != null)
            {
                server.Text = localSettings.Values["server"] as string;
                dataBase.Text = localSettings.Values["dataBase"] as string;
                user.Text = localSettings.Values["user"] as string;
                password.Password = localSettings.Values["password"] as string;
            }

        }

    }
}
