using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;

namespace JBS.S6
{
    public partial class FrmFixSQLIndex : Formbase
    {
        public FrmFixSQLIndex()
        {
            InitializeComponent();
            //pVar.FrmFixSQLIndex = this;
        }

        private void FrmFixSQLIndex_Load(object sender, EventArgs e)
        {

        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            btnFix.Enabled = false;
            try
            {
                //取得目前工作站數
                //using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                //using (SqlCommand cmd = cn.CreateCommand())
                //using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                //{
                //    DataTable dtcount = new DataTable();
                //    cmd.CommandText = " select hostprocess 主機,net_address 位址 from master..sysprocesses where program_name = 'JBS' ";
                //    da.Fill(dtcount);

                //    DataTable temp = dtcount.Clone();
                //    for (int i = 0; i < dtcount.Rows.Count; i++)
                //    {
                //        var row = temp.AsEnumerable().Where(r => r["主機"].ToString() == dtcount.Rows[i]["主機"].ToString() && r["位址"].ToString() == dtcount.Rows[i]["位址"].ToString());
                //        if (row.Count() == 0)
                //            temp.ImportRow(dtcount.Rows[i]);
                //    }
                //    temp.AcceptChanges();
                //    if (temp.Rows.Count > 1)
                //    {
                //        MessageBox.Show("請所有正在使用進銷存的工作站都必須退出,暫停作業！" + Environment.NewLine + "才可以進行資料重整作業！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return;
                //    }
                //}

                var SQLbasic = " WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ;" + Environment.NewLine;
                var SQLstr = ""
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='quote'  And name='INoD')   CREATE NONCLUSTERED INDEX [INoD] ON   [dbo].[quote]  ([quno]   DESC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='quoted' And name='INoA')   CREATE NONCLUSTERED INDEX [INoA] ON   [dbo].[quoted] ([quno]    ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='quoted' And name='ICunoA') CREATE NONCLUSTERED INDEX [ICunoA] ON [dbo].[quoted] ([cuno]    ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='quoted' And name='ICunoD') CREATE NONCLUSTERED INDEX [ICunoD] ON [dbo].[quoted] ([cuno]   DESC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='quoted' And name='IItnoA') CREATE NONCLUSTERED INDEX [IItnoA] ON [dbo].[quoted] ([itno]    ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='quoted' And name='IItnoD') CREATE NONCLUSTERED INDEX [IItnoD] ON [dbo].[quoted] ([itno]   DESC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='quoted' And name='IDateA') CREATE NONCLUSTERED INDEX [IDateA] ON [dbo].[quoted] ([qudate]  ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='quoted' And name='IDateD') CREATE NONCLUSTERED INDEX [IDateD] ON [dbo].[quoted] ([qudate] DESC)" + SQLbasic

                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='order'  And name='INoD')   CREATE NONCLUSTERED INDEX [INoD] ON   [dbo].[order]  ([orno]   DESC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='orderd' And name='INoA')   CREATE NONCLUSTERED INDEX [INoA] ON   [dbo].[orderd] ([orno]    ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='orderd' And name='ICunoA') CREATE NONCLUSTERED INDEX [ICunoA] ON [dbo].[orderd] ([cuno]    ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='orderd' And name='ICunoD') CREATE NONCLUSTERED INDEX [ICunoD] ON [dbo].[orderd] ([cuno]   DESC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='orderd' And name='IItnoA') CREATE NONCLUSTERED INDEX [IItnoA] ON [dbo].[orderd] ([itno]    ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='orderd' And name='IItnoD') CREATE NONCLUSTERED INDEX [IItnoD] ON [dbo].[orderd] ([itno]   DESC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='orderd' And name='IDateA') CREATE NONCLUSTERED INDEX [IDateA] ON [dbo].[orderd] ([ordate]  ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='orderd' And name='IDateD') CREATE NONCLUSTERED INDEX [IDateD] ON [dbo].[orderd] ([ordate] DESC)" + SQLbasic

                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='sale' And name='INoD')    CREATE NONCLUSTERED INDEX [INoD] ON   [dbo].[sale]  ([sano]   DESC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='saled' And name='INoA')   CREATE NONCLUSTERED INDEX [INoA] ON   [dbo].[saled] ([sano]    ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='saled' And name='ICunoA') CREATE NONCLUSTERED INDEX [ICunoA] ON [dbo].[saled] ([cuno]    ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='saled' And name='ICunoD') CREATE NONCLUSTERED INDEX [ICunoD] ON [dbo].[saled] ([cuno]   DESC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='saled' And name='IItnoA') CREATE NONCLUSTERED INDEX [IItnoA] ON [dbo].[saled] ([itno]    ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='saled' And name='IItnoD') CREATE NONCLUSTERED INDEX [IItnoD] ON [dbo].[saled] ([itno]   DESC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='saled' And name='IDateA') CREATE NONCLUSTERED INDEX [IDateA] ON [dbo].[saled] ([sadate]  ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='saled' And name='IDateD') CREATE NONCLUSTERED INDEX [IDateD] ON [dbo].[saled] ([sadate] DESC)" + SQLbasic;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = SQLstr;
                    cmd.ExecuteNonQuery();
                }

                SQLbasic = " WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY] ;" + Environment.NewLine;
                SQLstr = ""
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='fquot'  And name='INoD')   CREATE NONCLUSTERED INDEX [INoD] ON   [dbo].[fquot]  ([fqno]   DESC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='fquotd' And name='INoA')   CREATE NONCLUSTERED INDEX [INoA] ON   [dbo].[fquotd] ([fqno]    ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='fquotd' And name='IFanoA') CREATE NONCLUSTERED INDEX [IFanoA] ON [dbo].[fquotd] ([fano]    ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='fquotd' And name='IFanoD') CREATE NONCLUSTERED INDEX [IFanoD] ON [dbo].[fquotd] ([fano]   DESC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='fquotd' And name='IItnoA') CREATE NONCLUSTERED INDEX [IItnoA] ON [dbo].[fquotd] ([itno]    ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='fquotd' And name='IItnoD') CREATE NONCLUSTERED INDEX [IItnoD] ON [dbo].[fquotd] ([itno]   DESC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='fquotd' And name='IDateA') CREATE NONCLUSTERED INDEX [IDateA] ON [dbo].[fquotd] ([fqdate]  ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='fquotd' And name='IDateD') CREATE NONCLUSTERED INDEX [IDateD] ON [dbo].[fquotd] ([fqdate] DESC)" + SQLbasic

                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='ford'  And name='INoD')   CREATE NONCLUSTERED INDEX [INoD] ON   [dbo].[ford]  ([fono]   DESC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='fordd' And name='INoA')   CREATE NONCLUSTERED INDEX [INoA] ON   [dbo].[fordd] ([fono]    ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='fordd' And name='IFanoA') CREATE NONCLUSTERED INDEX [IFanoA] ON [dbo].[fordd] ([fano]    ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='fordd' And name='IFanoD') CREATE NONCLUSTERED INDEX [IFanoD] ON [dbo].[fordd] ([fano]   DESC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='fordd' And name='IItnoA') CREATE NONCLUSTERED INDEX [IItnoA] ON [dbo].[fordd] ([itno]    ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='fordd' And name='IItnoD') CREATE NONCLUSTERED INDEX [IItnoD] ON [dbo].[fordd] ([itno]   DESC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='fordd' And name='IDateA') CREATE NONCLUSTERED INDEX [IDateA] ON [dbo].[fordd] ([fodate]  ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='fordd' And name='IDateD') CREATE NONCLUSTERED INDEX [IDateD] ON [dbo].[fordd] ([fodate] DESC)" + SQLbasic

                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='bshop' And name='INoD')    CREATE NONCLUSTERED INDEX [INoD] ON   [dbo].[bshop]  ([bsno]   DESC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='bshopd' And name='INoA')   CREATE NONCLUSTERED INDEX [INoA] ON   [dbo].[bshopd] ([bsno]    ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='bshopd' And name='IFanoA') CREATE NONCLUSTERED INDEX [IFanoA] ON [dbo].[bshopd] ([fano]    ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='bshopd' And name='IFanoD') CREATE NONCLUSTERED INDEX [IFanoD] ON [dbo].[bshopd] ([fano]   DESC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='bshopd' And name='IItnoA') CREATE NONCLUSTERED INDEX [IItnoA] ON [dbo].[bshopd] ([itno]    ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='bshopd' And name='IItnoD') CREATE NONCLUSTERED INDEX [IItnoD] ON [dbo].[bshopd] ([itno]   DESC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='bshopd' And name='IDateA') CREATE NONCLUSTERED INDEX [IDateA] ON [dbo].[bshopd] ([bsdate]  ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='bshopd' And name='IDateD') CREATE NONCLUSTERED INDEX [IDateD] ON [dbo].[bshopd] ([bsdate] DESC)" + SQLbasic

                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='item' And name='ItNou') CREATE NONCLUSTERED INDEX [ItNou] ON [dbo].[item] ([itnoudf] ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='bomd' And name='bItNo') CREATE NONCLUSTERED INDEX [bItNo] ON [dbo].[bomd] ([boitno] ASC)" + SQLbasic

                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='receivd' And name='INo') CREATE NONCLUSTERED INDEX [INo] ON [dbo].[receivd] ([reno] ASC)" + SQLbasic
                + " IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='payabld' And name='INo') CREATE NONCLUSTERED INDEX [INo] ON [dbo].[payabld] ([pano] ASC)" + SQLbasic;
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = SQLstr;
                    cmd.ExecuteNonQuery();
                }


                var SQLstr1 = ""
                            + " DECLARE @dbid int "
                            + "     SET @dbid = DB_ID() "
                            + " SELECT 'ALTER INDEX [' + ix.name + '] ON [' + s.name + '].[' + t.name + '] ' +"
                            + "        CASE"
                            + "               WHEN ps.avg_fragmentation_in_percent > 15"
                            + "               THEN 'REBUILD'"
                            + "               ELSE 'REORGANIZE'"
                            + "        END +"
                            + "        CASE"
                            + "               WHEN pc.partition_count > 1"
                            + "               THEN ' PARTITION = ' + CAST(ps.partition_number AS nvarchar(MAX))"
                            + "               ELSE ''"
                            + "        END,"
                            + "        avg_fragmentation_in_percent"
                            + " FROM   sys.indexes AS ix"
                            + "        INNER JOIN sys.tables t"
                            + "        ON     t.object_id = ix.object_id"
                            + "        INNER JOIN sys.schemas s"
                            + "        ON     t.schema_id = s.schema_id"
                            + "        INNER JOIN"
                            + "               (SELECT object_id                   ,"
                            + "                       index_id                    ,"
                            + "                       avg_fragmentation_in_percent,"
                            + "                       partition_number"
                            + "               FROM    sys.dm_db_index_physical_stats (@dbid, NULL, NULL, NULL, NULL)"
                            + "               ) ps"
                            + "        ON     t.object_id = ps.object_id"
                            + "           AND ix.index_id = ps.index_id"
                            + "        INNER JOIN"
                            + "               (SELECT  object_id,"
                            + "                        index_id ,"
                            + "                        COUNT(DISTINCT partition_number) AS partition_count"
                            + "               FROM     sys.partitions"
                            + "               GROUP BY object_id,"
                            + "                        index_id"
                            + "               ) pc"
                            + "        ON     t.object_id              = pc.object_id"
                            + "           AND ix.index_id              = pc.index_id"
                            + " WHERE  ps.avg_fragmentation_in_percent > 10"
                            + "    AND ix.name IS NOT NULL";

                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.CommandTimeout = 0;

                    cmd.CommandText = SQLstr1;
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0) return;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmd.CommandText = dt.Rows[i][0].ToString().Trim();
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("資料重整完成！", "訊息視窗", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                btnFix.Enabled = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
