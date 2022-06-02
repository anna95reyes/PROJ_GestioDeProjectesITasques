using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NBA_BD.db
{
    public class MySqlDBContext : DbContext
    {
        private String server;
        private String dataBase;
        private String user;
        private String password;

        public void connexio(string server, string dataBase, string user, string password)
        {
            this.server = server;
            this.dataBase = dataBase;
            this.user = user;
            this.password = password;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            //optionBuilder.UseMySQL("Server=" + server + ";Database=" + dataBase + ";UID=" + user + ";Password=" + password);

            optionBuilder.UseMySQL("Server=51.68.224.27;Database=dam2_areyes;UID=dam2-areyes;Password=2681X");
        }
    }
}
