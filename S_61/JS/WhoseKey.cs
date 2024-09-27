using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace JBS.JS
{
    public class KeyMan
    {
        public string AppendMan { get; private set; }
        public string AppendTime { get; private set; }
        public string EditMan { get; private set; }
        public string EditTime { get; private set; }

        public KeyMan()
        {
            Clear();
        }

        public void Clear()
        {
            AppendMan = string.Empty;
            AppendTime = string.Empty;
            EditMan = string.Empty;
            EditTime = string.Empty;
        }
         
        public void Set(SqlDataReader reader)
        {
            this.AppendMan = reader["AppScNo"].ToString();
            this.AppendTime = reader["AppDate"].ToString();

            this.EditMan = reader["EdtScNo"].ToString();
            this.EditTime = reader["EdtDate"].ToString();
        }
    }
}
