using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using S_61.Basic;
using System.Xml.Linq;

namespace S_61.Basic
{
    public class AlterDB
    {
        SqlTransaction tn = null;
        bool IsTransactionOK = true;

        #region
        public bool v9()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        //瀏覽筆數
                        AddColumnAndDValue(cmd, "scrit", "searchcount", "int", "500");

                        //POS
                        AddColumn(cmd, "RSaleD", "KiTax", "NVARCHAR(1)");
                        AddColumn(cmd, "SaleDTemp", "KiTax", "NVARCHAR(1)");
                        //
                        AddColumn(cmd, "nullify", "IsTrans", "NVARCHAR(10)");
                        AddColumn(cmd, "RSale", "IsTrans", "NVARCHAR(10)");
                        AddColumn(cmd, "RSaleD", "IsTrans", "NVARCHAR(10)");
                        AddColumn(cmd, "RSaleBom", "IsTrans", "NVARCHAR(10)");
                        AddColumn(cmd, "adjubom", "IsTrans", "NVARCHAR(10)");
                        AddColumn(cmd, "SaleDTemp", "IsTrans", "NVARCHAR(10)");
                        //
                        AddColumn(cmd, "Saled", "Point", "NUMERIC(19,6)");
                        AddColumn(cmd, "Rsaled", "Point", "NUMERIC(19,6)");
                        AddColumn(cmd, "SaleDTemp", "Point", "NUMERIC(19,6)");
                        //
                        AddColumn(cmd, "saledTemp", "TempItNo", "NVARCHAR(20)");
                        AddColumn(cmd, "saledTemp", "TempItPkgQty", "NUMERIC(19,6)");
                        //
                        cmd.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'RSALE' AND COLUMN_NAME = 'SANO' AND DATA_TYPE= 'NVARCHAR' AND CHARACTER_MAXIMUM_LENGTH = 20;";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            ChangePKColumn(cmd, "RSale", "sano", "NVARCHAR(20)");
                            ChangeColumnType(cmd, "RSaleD", "sano", "NVARCHAR(20)");
                            ChangeColumnType(cmd, "RSaleBom", "sano", "NVARCHAR(20)");
                        }

                        //材績
                        AddColumn(cmd, "Scrit", "Formula", "NVARCHAR(3)");
                        AddColumn(cmd, "RSaled", "mqty", "NUMERIC(19,6)");
                        AddColumn(cmd, "RSaled", "munit", "NVARCHAR(10)");
                        AddColumn(cmd, "RSaled", "mlong", "NUMERIC(19,6)");
                        AddColumn(cmd, "RSaled", "mwidth1", "NUMERIC(19,6)");
                        AddColumn(cmd, "RSaled", "mwidth2", "NUMERIC(19,6)");
                        AddColumn(cmd, "RSaled", "mwidth3", "NUMERIC(19,6)");
                        AddColumn(cmd, "RSaled", "mwidth4", "NUMERIC(19,6)");
                        AddColumn(cmd, "RSaled", "mformula", "NUMERIC(4,3)");
                        //
                        AddColumn(cmd, "BShopd", "mqty", "NUMERIC(19,6)");
                        AddColumn(cmd, "BShopd", "munit", "NVARCHAR(10)");
                        AddColumn(cmd, "BShopd", "mlong", "NUMERIC(19,6)");
                        AddColumn(cmd, "BShopd", "mwidth1", "NUMERIC(19,6)");
                        AddColumn(cmd, "BShopd", "mwidth2", "NUMERIC(19,6)");
                        AddColumn(cmd, "BShopd", "mwidth3", "NUMERIC(19,6)");
                        AddColumn(cmd, "BShopd", "mwidth4", "NUMERIC(19,6)");
                        AddColumn(cmd, "BShopd", "mformula", "NUMERIC(4,3)");
                        //
                        AddColumn(cmd, "RShopd", "mqty", "NUMERIC(19,6)");
                        AddColumn(cmd, "RShopd", "munit", "NVARCHAR(10)");
                        AddColumn(cmd, "RShopd", "mlong", "NUMERIC(19,6)");
                        AddColumn(cmd, "RShopd", "mwidth1", "NUMERIC(19,6)");
                        AddColumn(cmd, "RShopd", "mwidth2", "NUMERIC(19,6)");
                        AddColumn(cmd, "RShopd", "mwidth3", "NUMERIC(19,6)");
                        AddColumn(cmd, "RShopd", "mwidth4", "NUMERIC(19,6)");
                        AddColumn(cmd, "RShopd", "mformula", "NUMERIC(4,3)");
                        //
                        cmd.CommandText = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = 'ORDER' AND COLUMN_NAME = 'MeOther' AND DATA_TYPE= 'TEXT';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "  alter table dbo.[order] alter column MeOther text;";
                            cmd.CommandText += " alter table dbo.[order] alter column MeMain text;";
                            cmd.CommandText += " alter table dbo.[order] alter column MePrint text;";
                            cmd.ExecuteNonQuery();
                        }
                        //
                        AddColumnAndDValue(cmd, "draw", "totmnyb", "NUMERIC(19,6)", "0");
                        AddColumnAndDValue(cmd, "drawd", "costb", "NUMERIC(19,6)", "0");
                        AddColumnAndDValue(cmd, "drawd", "mnyb", "NUMERIC(19,6)", "0");
                        //
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'borr';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = " CREATE TABLE [dbo].[borr]("
                            + " [bono] [nvarchar](20)  COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                            + " [bodate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [bodate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [bodate2] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [cono] [nvarchar](2) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [coname1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [coname2] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [fano] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [faname1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [faname2] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [fatel] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [faper1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [stno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [stname] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [emno] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [emname] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [xa1no] [nvarchar](3) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [xa1name] [nvarchar](12) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [xa1par] [numeric](11, 4) NULL,"
                            + " [taxmnyf] [numeric](19, 6) NULL,"
                            + " [taxmny] [numeric](19, 6) NULL,"
                            + " [taxmnyb] [numeric](19, 6) NULL,"
                            + " [x3no] [numeric](1, 0) NULL,"
                            + " [rate] [numeric](4, 3) NULL,"
                            + " [tax] [numeric](19, 6) NULL,"
                            + " [totmny] [numeric](19, 6) NULL,"
                            + " [taxb] [numeric](19, 6) NULL,"
                            + " [totmnyb] [numeric](19, 6) NULL,"
                            + " [recordno] [numeric](10, 0) NULL,"
                            + " [bomemo] [nvarchar](60) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [bomemo1] [text] NULL,"
                            + " CONSTRAINT [PK_borr] PRIMARY KEY CLUSTERED "
                            + " ("
                            + " [bono] ASC"
                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + " ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }
                        //
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES  where TABLE_NAME = N'borrd';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE [dbo].[borrd]("
                            + "[boid] [int] IDENTITY(1,1) NOT NULL,"
                            + "[bono] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[bodate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[bodate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[bodate2] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[cono] [nvarchar](2) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[fano] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[stno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[emno] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[xa1no] [nvarchar](3) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[xa1par] [numeric](11, 4) NULL,"
                            + "[itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itname] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[ittrait] [numeric](1, 0) NULL,"
                            + "[itunit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itpkgqty] [numeric](19, 4) NULL,"
                            + "[qty] [numeric](19, 4) NULL,"
                            + "[price] [numeric](19, 4) NULL,"
                            + "[prs] [numeric](4, 3) NULL,"
                            + "[rate] [numeric](4, 3) NULL,"
                            + "[taxprice] [numeric](19, 4) NULL,"
                            + "[mny] [numeric](19, 4) NULL,"
                            + "[priceb] [numeric](19, 4) NULL,"
                            + "[taxpriceb] [numeric](19, 4) NULL,"
                            + "[mnyb] [numeric](19, 4) NULL,"
                            + "[memo] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[lowzero] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[bomid] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[bomrec] [numeric](10, 0) NULL,"
                            + "[recordno] [numeric](10, 0) NULL,"
                            + "[sltflag] [bit] NULL,"
                            + "[extflag] [bit] NULL,"
                            + "[itdesp1] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itdesp2] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itdesp3] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itdesp4] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itdesp5] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itdesp6] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itdesp7] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itdesp8] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itdesp9] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itdesp10] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "CONSTRAINT [PK_borrd] PRIMARY KEY CLUSTERED "
                            + "("
                            + "[boid] ASC"
                            + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + ") ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }
                        //
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES  where TABLE_NAME = N'borrbom';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE [dbo].[borrbom]("
                            + "[bono] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[bomid] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[bomrec] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itname] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itunit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itqty] [numeric](20, 4) NULL,"
                            + "[itpareprs] [numeric](20, 4) NULL,"
                            + "[itpkgqty] [numeric](20, 4) NULL,"
                            + "[itrec] [numeric](6, 0) NULL,"
                            + "[itprice] [numeric](20, 6) NULL,"
                            + "[itprs] [numeric](4, 3) NULL,"
                            + "[itmny] [numeric](20, 6) NULL,"
                            + "[itnote] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itsource] [numeric](1, 0) NULL,"
                            + "[itbuypri] [numeric](20, 6) NULL,"
                            + "[itbuymny] [numeric](20, 6) NULL"
                            + ") ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }
                        //借出借入
                        cmd.CommandText = "SELECT COUNT(*) from stkroom where stno=N'BIN'";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = " insert into stkroom(stno,stname,sttrait,ip)values(N'BIN',N'借入倉庫',4,'');";
                            cmd.CommandText += "insert into stkroom(stno,stname,sttrait,ip)values(N'BOUT',N'借出倉庫',2,'');";
                            cmd.ExecuteNonQuery();

                            DataTable temp = new DataTable();
                            cmd.CommandText = "select itno,itname from item order by itno";
                            SqlDataAdapter dd = new SqlDataAdapter(cmd);
                            dd.Fill(temp);

                            DataTable temp1 = new DataTable();
                            cmd.CommandText = "select * from stock";
                            dd = new SqlDataAdapter(cmd);
                            dd.Fill(temp1);
                            temp1.Clear();

                            for (int i = 0; i < temp.Rows.Count; i++)
                            {
                                DataRow rw1 = temp1.NewRow();
                                rw1["itno"] = temp.Rows[i]["itno"].ToString().Trim();
                                rw1["stno"] = "BIN";
                                rw1["stname"] = "借入倉庫";
                                rw1["sttrait"] = "4";
                                rw1["itqtyf"] = 0;
                                rw1["itqty"] = 0;
                                DataRow rw2 = temp1.NewRow();
                                rw2["itno"] = temp.Rows[i]["itno"].ToString().Trim();
                                rw2["stno"] = "BOUT";
                                rw1["stname"] = "借出倉庫";
                                rw2["sttrait"] = "2";
                                rw2["itqtyf"] = 0;
                                rw2["itqty"] = 0;
                                temp1.Rows.Add(rw1);
                                temp1.Rows.Add(rw2);
                            }
                            using (SqlCommandBuilder builder = new SqlCommandBuilder(dd))
                            {
                                dd.InsertCommand = builder.GetInsertCommand();
                                dd.Update(temp1);
                            }
                        }

                        //
                        AddColumn(cmd, "Saled", "Punit", "nvarchar(4)");
                        AddColumn(cmd, "Saled", "Pqty", "NUMERIC(19, 4)");
                        AddColumn(cmd, "RSaled", "Punit", "nvarchar(4)");
                        AddColumn(cmd, "RSaled", "Pqty", "NUMERIC(19, 4)");
                        AddColumn(cmd, "BShopd", "Punit", "nvarchar(4)");
                        AddColumn(cmd, "BShopd", "Pqty", "NUMERIC(19, 4)");
                        AddColumn(cmd, "RShopd", "Punit", "nvarchar(4)");
                        AddColumn(cmd, "RShopd", "Pqty", "NUMERIC(19, 4)");
                        //
                        AddColumn(cmd, "borrd", "stname", "NVARCHAR(10)");
                        AddColumn(cmd, "borr", "AppScNo", "NVARCHAR(10)");
                        AddColumn(cmd, "borr", "AppDate", "NVARCHAR(20)");
                        AddColumn(cmd, "borr", "EdtScNo", "NVARCHAR(10)");
                        AddColumn(cmd, "borr", "EdtDate", "NVARCHAR(20)");
                        AddColumn(cmd, "borr", "stnoo", "NVARCHAR(10)");
                        AddColumn(cmd, "borr", "stnameo", "NVARCHAR(10)");
                        AddColumn(cmd, "borrd", "stnoo", "NVARCHAR(10)");
                        AddColumn(cmd, "borrd", "stnameo", "NVARCHAR(10)");
                        //
                        AddColumn(cmd, "lendd", "stname", "NVARCHAR(10)");
                        AddColumn(cmd, "lend", "AppScNo", "NVARCHAR(10)");
                        AddColumn(cmd, "lend", "AppDate", "NVARCHAR(20)");
                        AddColumn(cmd, "lend", "EdtScNo", "NVARCHAR(10)");
                        AddColumn(cmd, "lend", "EdtDate", "NVARCHAR(20)");
                        AddColumn(cmd, "lend", "stnoi", "NVARCHAR(10)");
                        AddColumn(cmd, "lend", "stnamei", "NVARCHAR(10)");
                        AddColumn(cmd, "lendd", "stnoi", "NVARCHAR(10)");
                        AddColumn(cmd, "lendd", "stnamei", "NVARCHAR(10)");
                        //
                        AddColumn(cmd, "orderd", "Punit", "nvarchar(4)");
                        AddColumn(cmd, "orderd", "Pqty", "NUMERIC(19, 4)");
                        AddColumn(cmd, "orderd", "mqty", "NUMERIC(19,6)");
                        AddColumn(cmd, "orderd", "munit", "NVARCHAR(10)");
                        AddColumn(cmd, "orderd", "mlong", "NUMERIC(19,6)");
                        AddColumn(cmd, "orderd", "mwidth1", "NUMERIC(19,6)");
                        AddColumn(cmd, "orderd", "mwidth2", "NUMERIC(19,6)");
                        AddColumn(cmd, "orderd", "mwidth3", "NUMERIC(19,6)");
                        AddColumn(cmd, "orderd", "mwidth4", "NUMERIC(19,6)");
                        AddColumn(cmd, "orderd", "mformula", "NUMERIC(4,3)");
                        //
                        AddColumn(cmd, "fordd", "Punit", "nvarchar(4)");
                        AddColumn(cmd, "fordd", "Pqty", "NUMERIC(19, 4)");
                        AddColumn(cmd, "fordd", "mqty", "NUMERIC(19,6)");
                        AddColumn(cmd, "fordd", "munit", "NVARCHAR(10)");
                        AddColumn(cmd, "fordd", "mlong", "NUMERIC(19,6)");
                        AddColumn(cmd, "fordd", "mwidth1", "NUMERIC(19,6)");
                        AddColumn(cmd, "fordd", "mwidth2", "NUMERIC(19,6)");
                        AddColumn(cmd, "fordd", "mwidth3", "NUMERIC(19,6)");
                        AddColumn(cmd, "fordd", "mwidth4", "NUMERIC(19,6)");
                        AddColumn(cmd, "fordd", "mformula", "NUMERIC(4,3)");
                        //
                        AddColumn(cmd, "item", "Punit", "nvarchar(4)");
                        AddColumn(cmd, "orderd", "Pformula", "nvarchar(3)");
                        AddColumn(cmd, "fordd", "Pformula", "nvarchar(3)");
                        AddColumn(cmd, "saled", "Pformula", "nvarchar(3)");
                        AddColumn(cmd, "rsaled", "Pformula", "nvarchar(3)");
                        AddColumn(cmd, "bshopd", "Pformula", "nvarchar(3)");
                        AddColumn(cmd, "rshopd", "Pformula", "nvarchar(3)");
                        //
                        cmd.CommandText = " SELECT COUNT(*) FROM scritd where taname='調撥資料瀏覽'";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = " update Saled set Pqty = qty;";
                            cmd.CommandText += "update Saled set Punit = itunit;";
                            cmd.ExecuteNonQuery();
                            //
                            cmd.CommandText = " update RSaled set Pqty = qty;";
                            cmd.CommandText += "update RSaled set Punit = itunit;";
                            cmd.ExecuteNonQuery();
                            //
                            cmd.CommandText = " update BShopd set Pqty = qty;";
                            cmd.CommandText += "update BShopd set Punit = itunit;";
                            cmd.ExecuteNonQuery();
                            //
                            cmd.CommandText = " update RShopd set Pqty = qty;";
                            cmd.CommandText += "update RShopd set Punit = itunit;";
                            cmd.ExecuteNonQuery();
                            //
                            cmd.CommandText = "update orderd set Pqty = qty;";
                            cmd.CommandText += "update orderd set Punit = itunit;";
                            cmd.ExecuteNonQuery();
                            //
                            cmd.CommandText = " update fordd set Pqty = qty;";
                            cmd.CommandText += "update fordd set Punit = itunit;";
                            cmd.ExecuteNonQuery();
                            //
                            cmd.CommandText = "  Update scritd set taname='訂單資料瀏覽' where taname='客戶訂單資料報表';";
                            cmd.CommandText += " Update scritd set taname='報價資料瀏覽' where taname='客戶報價資料報表';";
                            cmd.CommandText += " Update scritd set taname='採購資料瀏覽' where taname='廠商採購資料報表';";
                            cmd.CommandText += " Update scritd set taname='詢價資料瀏覽' where taname='廠商詢價資料報表';";
                            cmd.CommandText += " Update scritd set taname='業務員未交貨訂單' where taname='業務員-未交貨訂單';";
                            cmd.CommandText += " Update scritd set taname='採購別進貨明細表' where taname='採購單進貨明細表';";
                            cmd.CommandText += " Update scritd set taname='銷(退)貨資料瀏覽' where taname='銷(退)貨資料報表';";
                            cmd.CommandText += " Update scritd set taname='進(退)貨資料瀏覽' where taname='進(退)貨資料報表';";
                            cmd.CommandText += " Update scritd set taname='領料資料瀏覽' where taname='領料資料報表';";
                            cmd.CommandText += " Update scritd set taname='入庫資料瀏覽' where taname='入庫資料報表';";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = " SELECT SCNO FROM SCRITD GROUP BY SCNO";
                            using (DataTable table = new DataTable())
                            {
                                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                                {
                                    da.Fill(table);
                                }
                                foreach (DataRow rw in table.Rows)
                                {
                                    cmd.CommandText = "  insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','調撥資料瀏覽','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','特價區間建檔','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','寄庫領出作業','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','寄庫資料瀏覽','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','寄庫作業系統','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','盤點資料轉出','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','庫存盤點系統','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','帳齡分析應收帳款','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','業務別-已收帳款','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','帳齡分析應付帳款','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','採購員-已付帳款','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','銷項發票瀏覽作業','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','資料比對作業','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','產品編號修改','V','V','V','V','V','V','V','V','')";
                                    cmd.ExecuteNonQuery();
                                }
                                foreach (DataRow rw in table.Rows)
                                {
                                    cmd.CommandText = "  insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','職謂建檔作業','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','貨幣建檔作業','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','專案建檔作業','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','區域建檔作業','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','結帳類別建檔','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','送貨方式建檔','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','報價類別建檔','V','V','V','V','V','V','V','V','')";
                                    cmd.ExecuteNonQuery();
                                }
                                foreach (DataRow rw in table.Rows)
                                {
                                    cmd.CommandText = "  insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','客戶類別建檔','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','客戶資料瀏覽','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','廠商類別建檔','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','廠商資料瀏覽','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','產品類別建檔','V','V','V','V','V','V','V','V','')";
                                    cmd.CommandText += " insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','產品資料瀏覽','V','V','V','V','V','V','V','V','')";
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        //
                        AddColumnAndDValue(cmd, "systemset", "DBqty", "NUMERIC(1,0)", "1");
                        //
                        cmd.CommandText = "update systemset set vers ='10.0'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v10()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        ////瀏覽筆數
                        AddColumn(cmd, "speciald", "Reason", "NVARCHAR(10)");
                        AddColumn(cmd, "speciald", "GroupID", "NVARCHAR(10)");
                        AddColumn(cmd, "speciald", "singleprice", "NUMERIC(19,6)");
                        //
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES  where TABLE_NAME = N'SpecialGroup';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = ""
                            + " CREATE TABLE [dbo].[SpecialGroup]("
                            + " [groupid] [int] IDENTITY(1,1) NOT NULL,"
                            + " [qty] [numeric](19, 6) NULL,"
                            + " [price] [numeric](19, 6) NULL,"
                            + " [singleprice] [numeric](19, 6) NULL,"
                            + " CONSTRAINT [PK_SpecialGroup] PRIMARY KEY CLUSTERED "
                            + " ("
                            + " [groupid] ASC"
                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + " ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        AddColumnAndDValue(cmd, "Systemset", "ItIME", "NVARCHAR(10)", "注音速查");
                        AddColumn(cmd, "saledTemp", "groupid", "NUMERIC(19,6)");
                        //
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES  where TABLE_NAME = N'SaleClass';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = ""
                            + " CREATE TABLE [dbo].[SaleClass]("
                            + " [ScNo] [nvarchar](4) NOT NULL,"
                            + " [ScName] [nvarchar](20) NULL,"
                            + " CONSTRAINT [PK_SaleClass] PRIMARY KEY CLUSTERED "
                            + " ("
                            + " [ScNo] ASC"
                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + " ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        AddColumn(cmd, "Item", "ScNo", "NVARCHAR(4)");

                        ChangeColumnType(cmd, "adjust", "stno", "NVARCHAR(10)");
                        ChangeColumnType(cmd, "adjustd", "stno", "NVARCHAR(10)");
                        ChangeColumnType(cmd, "draw", "stnoo", "NVARCHAR(10)");
                        ChangeColumnType(cmd, "draw", "stnoi", "NVARCHAR(10)");
                        ChangeColumnType(cmd, "drawd", "stnoo", "NVARCHAR(10)");
                        ChangeColumnType(cmd, "drawd", "stnoi", "NVARCHAR(10)");
                        ChangeColumnType(cmd, "garner", "stnoo", "NVARCHAR(10)");
                        ChangeColumnType(cmd, "garner", "stnoi", "NVARCHAR(10)");
                        ChangeColumnType(cmd, "garnerd", "stnoo", "NVARCHAR(10)");
                        ChangeColumnType(cmd, "garnerd", "stnoi", "NVARCHAR(10)");
                        ChangeColumnType(cmd, "allot", "stnoo", "NVARCHAR(10)");
                        ChangeColumnType(cmd, "allot", "stnoi", "NVARCHAR(10)");
                        ChangeColumnType(cmd, "allotd", "stnoo", "NVARCHAR(10)");
                        ChangeColumnType(cmd, "allotd", "stnoi", "NVARCHAR(10)");

                        AddColumnAndDValue(cmd, "scrit", "IsBiggerWord", "NVARCHAR(1)", "1");

                        AddColumnAndDValue(cmd, "Cust", "WebID", "NVARCHAR(10)", "");
                        AddColumnAndDValue(cmd, "Fact", "WebID", "NVARCHAR(10)", "");
                        AddColumnAndDValue(cmd, "Fact", "WebPassWord", "NVARCHAR(20)", "");

                        ChangeColumnType(cmd, "weborder", "ortrnflag", "bit");
                        ChangeColumnType(cmd, "weborder", "oroverflag", "bit");

                        cmd.CommandText = "Select count(*) from syscolumns a left join sysobjects b on (a.[id]=b.[id]) where b.[name] = 'order' and  a.[name]='NetNo'";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = " ALTER TABLE dbo.[order] ADD NetNo NVARCHAR(16) COLLATE Chinese_Taiwan_Stroke_BIN NULL;"
                                            + " ALTER TABLE dbo.[order] ALTER COLUMN NetNo NVARCHAR(16) COLLATE Chinese_Taiwan_Stroke_BIN;";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'rlend';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = " CREATE TABLE [dbo].[rlend]("
                            + " [leno] [nvarchar](20)  COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                            + " [ledate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [ledate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [ledate2] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [cono] [nvarchar](2) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [coname1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [coname2] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [cuno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [cuname1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [cuname2] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [cutel] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [cuper1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [stno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [stname] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [emno] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [emname] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [xa1no] [nvarchar](3) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [xa1name] [nvarchar](12) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [xa1par] [numeric](11, 4) NULL,"
                            + " [taxmnyf] [numeric](19, 6) NULL,"
                            + " [taxmny] [numeric](19, 6) NULL,"
                            + " [taxmnyb] [numeric](19, 6) NULL,"
                            + " [x3no] [numeric](1, 0) NULL,"
                            + " [rate] [numeric](4, 3) NULL,"
                            + " [tax] [numeric](19, 6) NULL,"
                            + " [totmny] [numeric](19, 6) NULL,"
                            + " [taxb] [numeric](19, 6) NULL,"
                            + " [totmnyb] [numeric](19, 6) NULL,"
                            + " [recordno] [numeric](10, 0) NULL,"
                            + " [lememo] [nvarchar](60) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [lememo1] [text] NULL,"
                            + " [AppScNo] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [AppDate] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [EdtScNo] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [EdtDate] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [stnoi] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [stnamei] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " CONSTRAINT [PK_rlend] PRIMARY KEY CLUSTERED "
                            + " ("
                            + " [leno] ASC"
                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + " ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES  where TABLE_NAME = N'rlendd';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE [dbo].[rlendd]("
                            + "[leid] [int] IDENTITY(1,1) NOT NULL,"
                            + "[leno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[ledate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[ledate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[ledate2] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[cono] [nvarchar](2) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[cuno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[stno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[emno] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[xa1no] [nvarchar](3) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[xa1par] [numeric](11, 4) NULL,"
                            + "[itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itname] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[ittrait] [numeric](1, 0) NULL,"
                            + "[itunit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itpkgqty] [numeric](19, 4) NULL,"
                            + "[qty] [numeric](19, 4) NULL,"
                            + "[price] [numeric](19, 4) NULL,"
                            + "[prs] [numeric](4, 3) NULL,"
                            + "[rate] [numeric](4, 3) NULL,"
                            + "[taxprice] [numeric](19, 4) NULL,"
                            + "[mny] [numeric](19, 4) NULL,"
                            + "[priceb] [numeric](19, 4) NULL,"
                            + "[taxpriceb] [numeric](19, 4) NULL,"
                            + "[mnyb] [numeric](19, 4) NULL,"
                            + "[memo] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[lowzero] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[bomid] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[bomrec] [numeric](10, 0) NULL,"
                            + "[recordno] [numeric](10, 0) NULL,"
                            + "[sltflag] [bit] NULL,"
                            + "[extflag] [bit] NULL,"
                            + "[itdesp1] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itdesp2] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itdesp3] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itdesp4] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itdesp5] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itdesp6] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itdesp7] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itdesp8] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itdesp9] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itdesp10] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [stname] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [stnoi] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [stnamei] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "CONSTRAINT [PK_rlendd] PRIMARY KEY CLUSTERED "
                            + "("
                            + "[leid] ASC"
                            + ")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + ") ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }
                        //
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES  where TABLE_NAME = N'rlendbom';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE [dbo].[rlendbom]("
                            + "[leno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[bomid] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[bomrec] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itname] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itunit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itqty] [numeric](20, 4) NULL,"
                            + "[itpareprs] [numeric](20, 4) NULL,"
                            + "[itpkgqty] [numeric](20, 4) NULL,"
                            + "[itrec] [numeric](6, 0) NULL,"
                            + "[itprice] [numeric](20, 6) NULL,"
                            + "[itprs] [numeric](4, 3) NULL,"
                            + "[itmny] [numeric](20, 6) NULL,"
                            + "[itnote] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itsource] [numeric](1, 0) NULL,"
                            + "[itbuypri] [numeric](20, 6) NULL,"
                            + "[itbuymny] [numeric](20, 6) NULL"
                            + ") ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES  where TABLE_NAME = N'RBorr';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE [dbo].[RBorr]("
                            + " [bono] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN  NOT NULL, "
                            + " [bodate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [bodate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [bodate2] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [cono] [nvarchar](2) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [coname1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [coname2] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [fano] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [faname1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [faname2] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [fatel1] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [faper1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [stno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [stname] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [emno] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [emname] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [xa1no] [nvarchar](3) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [xa1name] [nvarchar](12) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [xa1par] [numeric](11, 4) NULL, "
                            + " [taxmnyf] [numeric](19, 6) NULL, "
                            + " [taxmny] [numeric](19, 6) NULL, "
                            + " [taxmnyb] [numeric](19, 6) NULL, "
                            + " [x3no] [numeric](1, 0) NULL, "
                            + " [rate] [numeric](4, 3) NULL, "
                            + " [tax] [numeric](19, 6) NULL, "
                            + " [totmny] [numeric](19, 6) NULL, "
                            + " [taxb] [numeric](19, 6) NULL, "
                            + " [totmnyb] [numeric](19, 6) NULL, "
                            + " [recordno] [numeric](10, 0) NULL, "
                            + " [bomemo] [nvarchar](60) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [bomemo1] [text] NULL, "
                            + " [AppScNo] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [AppDate] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [EdtScNo] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [EdtDate] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [stnoo] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [stnameo] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " CONSTRAINT [PK_RBorr] PRIMARY KEY CLUSTERED "
                            + " ("
                            + " [bono] ASC"
                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + " ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES  where TABLE_NAME = N'RBorrD';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE [dbo].[RBorrD]("
                            + " [boid] [int] IDENTITY(1,1) NOT NULL, "
                            + " [bono] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [bodate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [bodate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [bodate2] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [cono] [nvarchar](2) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [fano] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [stno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [emno] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [xa1no] [nvarchar](3) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [xa1par] [numeric](11, 4) NULL, "
                            + " [itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [itname] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [ittrait] [numeric](1, 0) NULL, "
                            + " [itunit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [itpkgqty] [numeric](19, 4) NULL, "
                            + " [qty] [numeric](19, 4) NULL, "
                            + " [price] [numeric](19, 4) NULL, "
                            + " [prs] [numeric](4, 3) NULL, "
                            + " [rate] [numeric](4, 3) NULL, "
                            + " [taxprice] [numeric](19, 4) NULL, "
                            + " [mny] [numeric](19, 4) NULL, "
                            + " [priceb] [numeric](19, 4) NULL, "
                            + " [taxpriceb] [numeric](19, 4) NULL, "
                            + " [mnyb] [numeric](19, 4) NULL, "
                            + " [memo] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [lowzero] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [bomid] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [bomrec] [numeric](10, 0) NULL, "
                            + " [recordno] [numeric](10, 0) NULL, "
                            + " [sltflag] [bit] NULL, "
                            + " [extflag] [bit] NULL, "
                            + " [itdesp1] [nvarchar](40)  COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [itdesp2] [nvarchar](40)  COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [itdesp3] [nvarchar](40)  COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [itdesp4] [nvarchar](40)  COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [itdesp5] [nvarchar](40)  COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [itdesp6] [nvarchar](40)  COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [itdesp7] [nvarchar](40)  COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [itdesp8] [nvarchar](40)  COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [itdesp9] [nvarchar](40)  COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [itdesp10] [nvarchar](40) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [stname] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [stnoo] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " [stnameo] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL, "
                            + " CONSTRAINT [PK_RBorrD] PRIMARY KEY CLUSTERED "
                            + " ("
                            + " [boid] ASC"
                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + " ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES  where TABLE_NAME = N'RBorrBom';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE [dbo].[RBorrBom]("
                            + " [bono] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [bomid] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                            + " [bomrec] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                            + " [itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                            + " [itname] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                            + " [itunit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                            + " [itqty] [numeric](20, 4) NULL,"
                            + " [itpareprs] [numeric](20, 4) NULL,"
                            + " [itpkgqty] [numeric](20, 4) NULL,"
                            + " [itrec] [numeric](6, 0) NULL,"
                            + " [itprice] [numeric](20, 6) NULL,"
                            + " [itprs] [numeric](4, 3) NULL,"
                            + " [itmny] [numeric](20, 6) NULL,"
                            + " [itnote] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                            + " [itsource] [numeric](1, 0) NULL,"
                            + " [itbuypri] [numeric](20, 6) NULL,"
                            + " [itbuymny] [numeric](20, 6) NULL"
                            + " ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        AddColumn(cmd, "RLendD", "LendNo", "NVARCHAR(20)");
                        ChangeColumnName(cmd, "Borr", "faname", "faname1");
                        ChangeColumnName(cmd, "Borr", "fatel", "fatel1");
                        AddColumn(cmd, "LendD", "OrNo", "NVARCHAR(20)");

                        AddColumn(cmd, "Lend", "leoverflag", "bit");
                        AddColumn(cmd, "Borr", "booverflag", "bit");
                        AddColumn(cmd, "RBorrD", "BorrNo", "NVARCHAR(20)");

                        cmd.CommandText = " SELECT COUNT(*) FROM scritd where taname='銷售類別建檔'";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = " SELECT SCNO FROM SCRITD GROUP BY SCNO";
                            using (DataTable table = new DataTable())
                            {
                                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                                {
                                    da.Fill(table);
                                }
                                foreach (DataRow rw in table.Rows)
                                {
                                    cmd.CommandText = "  insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','銷售類別建檔','V','V','V','V','V','V','V','V','')";
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        cmd.CommandText = "(Select count(*) from syscolumns a left join sysobjects b on (a.[id]=b.[id]) where b.[name] = 'LendD' and  a.[name]='qtynotout')";

                        if (cmd.ExecuteScalar().ToString() == "0")
                        {
                            cmd.CommandText = "ALTER TABLE LendD ADD qtynotout NUMERIC(19, 4)";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "update LendD set qtynotout = qty";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "ALTER TABLE BorrD ADD qtynotout NUMERIC(19, 4)";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "update BorrD set qtynotout = qty";
                            cmd.ExecuteNonQuery();
                        }

                        AddColumnAndDValue(cmd, "RLendD", "lenoid", "NVARCHAR(40)", "");
                        AddColumnAndDValue(cmd, "RBorrD", "Borrid", "NVARCHAR(40)", "");



                        AddColumnAndDValue(cmd, "saled", "leno", "NVARCHAR(20)", "");
                        AddColumnAndDValue(cmd, "SaleD", "leid", "NVARCHAR(40)", "");
                        AddColumnAndDValue(cmd, "BShopD", "bono", "NVARCHAR(20)", "");
                        AddColumnAndDValue(cmd, "BShopD", "boid", "NVARCHAR(40)", "");

                        AddColumnAndDValue(cmd, "Systemset", "CloseDate", "NVARCHAR(10)", "");
                        AddColumnAndDValue(cmd, "Systemset", "CloseDate1", "NVARCHAR(10)", "");
                        ChangeColumnType(cmd, "[order]", "spname", "NVARCHAR(20)");
                        ChangeColumnType(cmd, "ford", "spname", "NVARCHAR(20)");


                        cmd.CommandText = "(Select count(*) from syscolumns a left join sysobjects b on (a.[id]=b.[id]) where b.[name] = 'Quoted' and  a.[name]='Pqty')";
                        if (cmd.ExecuteScalar().ToString() == "0")
                        {
                            AddColumnAndDValue(cmd, "Quoted", "Punit", "NVARCHAR(4)", "");
                            AddColumn(cmd, "Quoted", "Pqty", "NUMERIC(19, 4)");
                            cmd.CommandText = " Update Quoted set pqty = qty where pqty is null";
                            cmd.ExecuteNonQuery();
                        }
                        AddColumn(cmd, "Quoted", "mwidth1", "NUMERIC(19,6)");
                        AddColumn(cmd, "Quoted", "mwidth2", "NUMERIC(19,6)");
                        AddColumn(cmd, "Quoted", "mwidth3", "NUMERIC(19,6)");
                        AddColumn(cmd, "Quoted", "mwidth4", "NUMERIC(19,6)");
                        AddColumn(cmd, "Quoted", "Pformula", "NVARCHAR(3)");

                        cmd.CommandText = "update systemset set vers ='11.0'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v11()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        ChangeColumnName(cmd, "scrit", "IsBiggerWord", "StopInvMode");

                        cmd.CommandText = " SELECT COUNT(*) FROM scritd where taname='POS前台銷售報表'";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = " SELECT SCNO FROM SCRITD GROUP BY SCNO";
                            using (DataTable table = new DataTable())
                            {
                                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                                {
                                    da.Fill(table);
                                }
                                foreach (DataRow rw in table.Rows)
                                {
                                    cmd.CommandText = "  insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','POS前台銷售報表','V','V','V','V','V','V','V','V','')";
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        cmd.CommandText = "select COUNT(*) from INFORMATION_SCHEMA.KEY_COLUMN_USAGE where SUBSTRING(CONSTRAINT_NAME,1,2) = 'PK' and TABLE_NAME = N'stkcost'";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "ALTER TABLE stkcost alter column itno nvarchar(20) COLLATE Chinese_Taiwan_Stroke_BIN not null;";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "ALTER TABLE stkcost alter column stno nvarchar(10) COLLATE Chinese_Taiwan_Stroke_BIN not null;";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = "select COUNT(*) from INFORMATION_SCHEMA.KEY_COLUMN_USAGE where SUBSTRING(CONSTRAINT_NAME,1,2) = 'PK' and TABLE_NAME = N'stkcost'";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "ALTER TABLE stkcost ADD CONSTRAINT pk_stkcostID PRIMARY KEY (itno,stno);";
                            cmd.ExecuteNonQuery();
                        }

                        AddColumnAndDValue(cmd, "systemset", "JieZ", "NVARCHAR(2)", "OK");

                        cmd.CommandText = "update systemset set vers ='11.0'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v111()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.CommandText = "update systemset set vers ='11.1'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v112()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.CommandText = "(Select count(*) from syscolumns a left join sysobjects b on (a.[id]=b.[id]) where b.[name] = 'receiv' and  a.[name]='xa1par')";
                        if (cmd.ExecuteScalar().ToString() == "0")
                        {
                            AddColumn(cmd, "receiv", "xa1par", "NUMERIC(19, 4)");
                            cmd.CommandText = " Update receiv set receiv.xa1par = D.xa1par from (select reno,xa1par from receivd group by reno,xa1par)D where receiv.reno=D.reno";
                            cmd.ExecuteNonQuery();
                        }
                        cmd.CommandText = "update systemset set vers ='11.2'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v113()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        AddColumnAndDValue(cmd, "Systemset", "JZOK", "NVARCHAR(2)", "");

                        cmd.CommandText = "update systemset set vers ='11.3'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v114()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.CommandText = "(Select count(*) from syscolumns a left join sysobjects b on (a.[id]=b.[id]) where b.[name] = 'payabl' and  a.[name]='xa1par')";
                        if (cmd.ExecuteScalar().ToString() == "0")
                        {
                            AddColumn(cmd, "payabl", "xa1par", "NUMERIC(19, 4)");
                            cmd.CommandText = " Update payabl set payabl.xa1par = D.xa1par from (select pano,xa1par from payabld group by pano,xa1par)D where payabl.pano=D.pano";
                            cmd.ExecuteNonQuery();
                        }
                        cmd.CommandText = "(Select count(*) from syscolumns a left join sysobjects b on (a.[id]=b.[id]) where b.[name] = 'scrit' and  a.[name]='CanEditPOS')";
                        if (cmd.ExecuteScalar().ToString() == "0")
                        {
                            AddColumn(cmd, "scrit", "CanEditPOS", "NVARCHAR(1)");
                            cmd.CommandText = " Update scrit set CanEditPOS = '2' where CanEditPOS is null;";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = "update systemset set vers ='11.4'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v115()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.CommandText = "select count(name) as 是否有欄位 from syscolumns where id=(select id from sysobjects where name=N'fquotd') and name=N'FqID'";
                        if (cmd.ExecuteScalar().ToString() == "0")
                        {
                            cmd.CommandText = "alter table fquotd add FqID int identity(1,1) NOT NULL PRIMARY KEY";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = "update systemset set vers ='11.5'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v116()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES  where TABLE_NAME = N'InvMode';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = ""
                                            + " CREATE TABLE [dbo].[InvMode]( "
                                            + " 	[ImNo] [nvarchar](2) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                                            + " 	[ImName] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " 	[InvNoS] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " 	[InvNoE] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " 	[InvNoC] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + "  CONSTRAINT [PK_InvMode] PRIMARY KEY CLUSTERED "
                                            + " ("
                                            + " 	[ImNo] ASC"
                                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                            + " ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = "update systemset set vers ='11.6'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v117()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        AddColumn(cmd, "cust", "ImNo", "NVARCHAR(2)");
                        ChangeColumnType(cmd, "systemset", "JZOK", "NVARCHAR(10)");

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " where TABLE_NAME = N'tablestyle';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = " CREATE TABLE [dbo].[tablestyle]("
                                            + " [id] [int] IDENTITY(1,1) NOT NULL,"
                                            + " [表單名稱] [nvarchar](30)  COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                                            + " [使用者名稱] [nvarchar](10)  COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                                            + " [資料行名稱] [nvarchar](20)  COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                                            + " [綁定值名稱] [nvarchar](20)  COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [初始順序] [NUMERIC](2,0)   NOT NULL,"
                                            + " [自定順序] [NUMERIC](2,0)   NOT NULL,"
                                            + " [初始文字] [nvarchar](20)  COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                                            + " [自定文字] [nvarchar](20)  COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                                            + " [初始寬度] [NUMERIC](5,0)   NOT NULL,"
                                            + " [自定寬度] [NUMERIC](5,0)   NOT NULL,"
                                            + " [自定顯示] [nvarchar](10)  COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                                            + " CONSTRAINT [PK_tablestyle] PRIMARY KEY CLUSTERED "
                                            + " ("
                                            + " [id] ASC"
                                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                            + " ) ON [PRIMARY] ";
                            cmd.ExecuteNonQuery();


                        }


                        cmd.CommandText = "update systemset set vers ='11.7'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v118()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;



                        cmd.CommandText = "update systemset set vers ='11.8'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v119()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        AddColumnAndDValue(cmd, "systemset", "Update_IP", "NVARCHAR(20)", "");
                        AddColumnAndDValue(cmd, "systemset", "Update_Folder", "NVARCHAR(20)", "");
                        AddColumnAndDValue(cmd, "systemset", "Update_Account", "NVARCHAR(20)", "");
                        AddColumnAndDValue(cmd, "systemset", "Update_Pwd", "NVARCHAR(20)", "");

                        cmd.CommandText = "update systemset set vers ='11.9'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v120()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;



                        cmd.CommandText = "update systemset set vers ='12.0'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v121()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;



                        cmd.CommandText = "update systemset set vers ='12.1'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v122()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        AddColumnAndDValue(cmd, "systemset", "LendToSaleMode", "NVARCHAR(1)", "1");

                        cmd.CommandText = "select count(name) as 是否有欄位 from syscolumns where id=(select id from sysobjects where name=N'item') and name=N'ItBarName1'";
                        if (cmd.ExecuteScalar().ToString() == "0")
                        {
                            cmd.CommandText = "  ALTER TABLE [item] ADD ItBarName1 NVARCHAR(16) COLLATE Chinese_Taiwan_Stroke_BIN;";
                            cmd.CommandText += " ALTER TABLE [item] ADD ItBarName2 NVARCHAR(16) COLLATE Chinese_Taiwan_Stroke_BIN;";
                            cmd.ExecuteNonQuery();

                            DataTable ditem = new DataTable();
                            cmd.Parameters.AddWithValue("itno", "");
                            cmd.Parameters.AddWithValue("ItBarName1", "");
                            cmd.Parameters.AddWithValue("ItBarName2", "");
                            cmd.CommandText = " Select itno,itname from item ";
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(ditem);
                            }
                            var itname = "";
                            var bar1 = "";
                            for (int i = 0; i < ditem.Rows.Count; i++)
                            {
                                cmd.Parameters["itno"].Value = ditem.Rows[i]["itno"].ToString().Trim();
                                itname = ditem.Rows[i]["itname"].ToString().Trim();
                                bar1 = itname.GetUTF8(15);
                                cmd.Parameters["ItBarName1"].Value = bar1;
                                cmd.Parameters["ItBarName2"].Value = (new string(itname.Skip(bar1.Length).ToArray())).GetUTF8(15);

                                cmd.CommandText = " update item set ItBarName1=(@ItBarName1),ItBarName2=(@ItBarName2) where itno=(@itno) ";
                                cmd.ExecuteNonQuery();
                            }
                        }

                        cmd.CommandText = "update systemset set vers ='12.2'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v123()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.CommandText = "  Update item set ItBarName1='',ItBarName2='';";
                        cmd.CommandText += " ALTER TABLE [item] ALTER column ItBarName1 NVARCHAR(15) COLLATE Chinese_Taiwan_Stroke_BIN;";
                        cmd.CommandText += " ALTER TABLE [item] ALTER column ItBarName2 NVARCHAR(15) COLLATE Chinese_Taiwan_Stroke_BIN;";
                        cmd.ExecuteNonQuery();

                        DataTable ditem = new DataTable();
                        cmd.Parameters.AddWithValue("itno", "");
                        cmd.Parameters.AddWithValue("ItBarName1", "");
                        cmd.Parameters.AddWithValue("ItBarName2", "");
                        cmd.CommandText = " Select itno,itname from item ";
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(ditem);
                        }
                        var itname = "";
                        var bar1 = "";
                        for (int i = 0; i < ditem.Rows.Count; i++)
                        {
                            cmd.Parameters["itno"].Value = ditem.Rows[i]["itno"].ToString().Trim();
                            itname = ditem.Rows[i]["itname"].ToString().Trim();
                            bar1 = itname.GetUTF8(15);
                            cmd.Parameters["ItBarName1"].Value = bar1;
                            cmd.Parameters["ItBarName2"].Value = (new string(itname.Skip(bar1.Length).ToArray())).GetUTF8(15);

                            cmd.CommandText = " update item set ItBarName1=(@ItBarName1),ItBarName2=(@ItBarName2) where itno=(@itno) ";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = "update systemset set vers ='12.3'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v124()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;



                        cmd.CommandText = "update systemset set vers ='12.4'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v125()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        AddColumn(cmd, "rlendd", "isFromSale", "NVARCHAR(10)");

                        cmd.CommandText = "update systemset set vers ='12.5'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        #endregion
        #region 126~154
        public bool v126()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        AddColumnAndDValue(cmd, "payabl", "feemny", "numeric(19, 6)", "0");

                        cmd.CommandText = "update systemset set vers ='12.6'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v127()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        #region 銷項發票作業
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " where TABLE_NAME = N'saleinv';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE [dbo].[saleinv]("
                            + " [inno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                            + " [indate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [indate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [cono] [nvarchar](2) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [coname1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [cuno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [cuname1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [invtaxno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [x5no] [numeric](1, 0) NULL,"
                            + " [x3no] [numeric](1, 0) NULL,"
                            + " [invname] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [invaddr1] [nvarchar](60) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [invalid] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [tax] [numeric](19, 6) NULL,"
                            + " [taxmny] [numeric](19, 6) NULL,"
                            + " [totmny] [numeric](19, 6) NULL,"
                            + " [rate] [numeric](4, 3) NULL,"
                            + " [xa1par] [numeric](11, 4) NULL,"
                            + " [inmemo] [nvarchar](60) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [recordno] [numeric](10, 0) NULL,"
                            + " PRIMARY KEY CLUSTERED "
                            + " ("
                            + " [inno] ASC"
                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + " ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " where TABLE_NAME = N'saleinvd';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE [dbo].[saleinvd]("
                            + "[inid] [int] IDENTITY(1,1) NOT NULL,"
                            + "[inno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                            + "[indate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[indate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[cono] [nvarchar](2) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[cuno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[x5no] [numeric](1, 0) NULL,"
                            + "[itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itname] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itunit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[punit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[bomid] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[bomrec] [numeric](10, 0) NULL,"
                            + "[ittrait] [numeric](1, 0) NULL,"
                            + "[itpkgqty] [numeric](19, 4) NULL,"
                            + "[pqty] [numeric](19, 4) NULL,"
                            + "[qty] [numeric](19, 4) NULL,"
                            + "[price] [numeric](19, 6) NULL,"
                            + "[prs] [numeric](4, 3) NULL,"
                            + "[rate] [numeric](4, 3) NULL,"
                            + "[xa1par] [numeric](11, 4) NULL,"
                            + "[taxprice] [numeric](19, 6) NULL,"
                            + "[mny] [numeric](19, 6) NULL,"
                            + "[priceb] [numeric](19, 6) NULL,"
                            + "[taxpriceb] [numeric](19, 6) NULL,"
                            + "[mnyb] [numeric](19, 6) NULL,"
                            + "[memo] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[recordno] [numeric](10, 0) NULL,"
                            + " CONSTRAINT [PK_saleinvd] PRIMARY KEY CLUSTERED "
                            + " ("
                            + " [inid] ASC"
                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + " ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " where TABLE_NAME = N'saleinvbom';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE saleinvbom("
                            + "[inno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[BomID] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                            + "[BomRec] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itname] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itunit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itqty] [numeric](19, 6) NULL,"
                            + "[itpareprs] [numeric](19, 6) NULL,"
                            + "[itpkgqty] [numeric](19, 6) NULL,"
                            + "[itrec] [numeric](6, 0) NULL,"
                            + "[itprice] [numeric](19, 6) NULL,"
                            + "[itprs] [numeric](4, 3) NULL,"
                            + "[itmny] [numeric](19, 6) NULL,"
                            + "[itnote] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[ItSource] [numeric](1, 0) NULL,"
                            + "[ItBuyPri] [numeric](19, 6) NULL,"
                            + "[ItBuyMny] [numeric](19, 6) NULL,"
                            + "[inid] [int] IDENTITY(1,1) NOT NULL,"
                            + "PRIMARY KEY CLUSTERED "
                            + "("
                            + "[inid] ASC"
                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + " ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }
                        #endregion

                        #region 進項發票作業
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " where TABLE_NAME = N'bshopinv';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE [dbo].[bshopinv]("
                            + " [inno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                            + " [indate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [indate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [cono] [nvarchar](2) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [coname1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [fano] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [faname1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [invtaxno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [x5no] [numeric](1, 0) NULL,"
                            + " [x3no] [numeric](1, 0) NULL,"
                            + " [invname] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [invaddr1] [nvarchar](60) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [invalid] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [tax] [numeric](19, 6) NULL,"
                            + " [taxmny] [numeric](19, 6) NULL,"
                            + " [totmny] [numeric](19, 6) NULL,"
                            + " [rate] [numeric](4, 3) NULL,"
                            + " [xa1par] [numeric](11, 4) NULL,"
                            + " [inmemo] [nvarchar](60) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [recordno] [numeric](10, 0) NULL,"
                            + " PRIMARY KEY CLUSTERED "
                            + " ("
                            + " [inno] ASC"
                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + " ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " where TABLE_NAME = N'bshopinvd';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE [dbo].[bshopinvd]("
                            + "[inid] [int] IDENTITY(1,1) NOT NULL,"
                            + "[inno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                            + "[indate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[indate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[cono] [nvarchar](2) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[fano] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[x5no] [numeric](1, 0) NULL,"
                            + "[itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itname] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itunit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[punit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[bomid] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[bomrec] [numeric](10, 0) NULL,"
                            + "[ittrait] [numeric](1, 0) NULL,"
                            + "[itpkgqty] [numeric](19, 4) NULL,"
                            + "[pqty] [numeric](19, 4) NULL,"
                            + "[qty] [numeric](19, 4) NULL,"
                            + "[price] [numeric](19, 6) NULL,"
                            + "[prs] [numeric](4, 3) NULL,"
                            + "[rate] [numeric](4, 3) NULL,"
                            + "[xa1par] [numeric](11, 4) NULL,"
                            + "[taxprice] [numeric](19, 6) NULL,"
                            + "[mny] [numeric](19, 6) NULL,"
                            + "[priceb] [numeric](19, 6) NULL,"
                            + "[taxpriceb] [numeric](19, 6) NULL,"
                            + "[mnyb] [numeric](19, 6) NULL,"
                            + "[memo] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[recordno] [numeric](10, 0) NULL,"
                            + " CONSTRAINT [PK_bshopinvd] PRIMARY KEY CLUSTERED "
                            + " ("
                            + " [inid] ASC"
                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + " ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " where TABLE_NAME = N'bshopinvbom';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE bshopinvbom("
                            + "[inno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[BomID] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                            + "[BomRec] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itname] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itunit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itqty] [numeric](19, 6) NULL,"
                            + "[itpareprs] [numeric](19, 6) NULL,"
                            + "[itpkgqty] [numeric](19, 6) NULL,"
                            + "[itrec] [numeric](6, 0) NULL,"
                            + "[itprice] [numeric](19, 6) NULL,"
                            + "[itprs] [numeric](4, 3) NULL,"
                            + "[itmny] [numeric](19, 6) NULL,"
                            + "[itnote] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[ItSource] [numeric](1, 0) NULL,"
                            + "[ItBuyPri] [numeric](19, 6) NULL,"
                            + "[ItBuyMny] [numeric](19, 6) NULL,"
                            + "[inid] [int] IDENTITY(1,1) NOT NULL,"
                            + "PRIMARY KEY CLUSTERED "
                            + "("
                            + "[inid] ASC"
                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + " ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }
                        #endregion

                        ChangeColumnType(cmd, "empl", "emdeno", "NVARCHAR(10)");
                        AddColumnAndDValue(cmd, "systemset", "X3Forward", "NVARCHAR(1)", "2");
                        AddColumnAndDValue(cmd, "systemset", "InvUsed", "NVARCHAR(1)", "2");


                        cmd.CommandText = "update systemset set vers ='12.7'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v128()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        AddColumn(cmd, "saled", "cyno", "NVARCHAR(20)");
                        AddColumn(cmd, "bshopd", "cyno", "NVARCHAR(20)");

                        #region garnerd gaid
                        cmd.CommandText = "select count(name) as 是否有欄位 from syscolumns where id=(select id from sysobjects where name=N'garnerd') and name=N'gaid'";
                        if (cmd.ExecuteScalar().ToString() == "0")
                        {
                            cmd.CommandText = "alter table garnerd add gaid int identity(1,1) NOT NULL PRIMARY KEY";
                            cmd.ExecuteNonQuery();
                        }
                        #endregion

                        #region GarnBom gaid
                        cmd.CommandText = "select count(name) as 是否有欄位 from syscolumns where id=(select id from sysobjects where name=N'GarnBom') and name=N'gaid'";
                        if (cmd.ExecuteScalar().ToString() == "0")
                        {
                            cmd.CommandText = "alter table GarnBom add gaid int identity(1,1) NOT NULL PRIMARY KEY";
                            cmd.ExecuteNonQuery();
                        }
                        #endregion

                        #region allotd alid
                        cmd.CommandText = "select count(name) as 是否有欄位 from syscolumns where id=(select id from sysobjects where name=N'allotd') and name=N'alid'";
                        if (cmd.ExecuteScalar().ToString() == "0")
                        {
                            cmd.CommandText = "alter table allotd add alid int identity(1,1) NOT NULL PRIMARY KEY";
                            cmd.ExecuteNonQuery();
                        }
                        #endregion

                        #region AlloBom alid
                        cmd.CommandText = "select count(name) as 是否有欄位 from syscolumns where id=(select id from sysobjects where name=N'AlloBom') and name=N'alid'";
                        if (cmd.ExecuteScalar().ToString() == "0")
                        {
                            cmd.CommandText = "alter table AlloBom add alid int identity(1,1) NOT NULL PRIMARY KEY";
                            cmd.ExecuteNonQuery();
                        }
                        #endregion

                        #region drawd drid
                        cmd.CommandText = "select count(name) as 是否有欄位 from syscolumns where id=(select id from sysobjects where name=N'drawd') and name=N'drid'";
                        if (cmd.ExecuteScalar().ToString() == "0")
                        {
                            cmd.CommandText = "alter table drawd add drid int identity(1,1) NOT NULL PRIMARY KEY";
                            cmd.ExecuteNonQuery();
                        }
                        #endregion

                        #region DrawBom drid
                        cmd.CommandText = "select count(name) as 是否有欄位 from syscolumns where id=(select id from sysobjects where name=N'DrawBom') and name=N'drid'";
                        if (cmd.ExecuteScalar().ToString() == "0")
                        {
                            cmd.CommandText = "alter table DrawBom add drid int identity(1,1) NOT NULL PRIMARY KEY";
                            cmd.ExecuteNonQuery();
                        }
                        #endregion

                        #region 銷項發票作業
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " where TABLE_NAME = N'saledis';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE [dbo].[saledis]("
                            + " [inno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                            + " [indate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [indate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [cono] [nvarchar](2) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [coname1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [cuno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [cuname1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [invtaxno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [x5no] [numeric](1, 0) NULL,"
                            + " [x3no] [numeric](1, 0) NULL,"
                            + " [invname] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [invaddr1] [nvarchar](60) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [invalid] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [tax] [numeric](19, 6) NULL,"
                            + " [taxmny] [numeric](19, 6) NULL,"
                            + " [totmny] [numeric](19, 6) NULL,"
                            + " [rate] [numeric](4, 3) NULL,"
                            + " [xa1par] [numeric](11, 4) NULL,"
                            + " [inmemo] [nvarchar](60) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [recordno] [numeric](10, 0) NULL,"
                            + " PRIMARY KEY CLUSTERED "
                            + " ("
                            + " [inno] ASC"
                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + " ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " where TABLE_NAME = N'saledisd';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE [dbo].[saledisd]("
                            + "[inid] [int] IDENTITY(1,1) NOT NULL,"
                            + "[inno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                            + "[indate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[indate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[cono] [nvarchar](2) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[cuno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[x5no] [numeric](1, 0) NULL,"
                            + "[itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itname] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itunit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[punit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[bomid] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[bomrec] [numeric](10, 0) NULL,"
                            + "[ittrait] [numeric](1, 0) NULL,"
                            + "[itpkgqty] [numeric](19, 4) NULL,"
                            + "[pqty] [numeric](19, 4) NULL,"
                            + "[qty] [numeric](19, 4) NULL,"
                            + "[price] [numeric](19, 6) NULL,"
                            + "[prs] [numeric](4, 3) NULL,"
                            + "[rate] [numeric](4, 3) NULL,"
                            + "[xa1par] [numeric](11, 4) NULL,"
                            + "[taxprice] [numeric](19, 6) NULL,"
                            + "[mny] [numeric](19, 6) NULL,"
                            + "[priceb] [numeric](19, 6) NULL,"
                            + "[taxpriceb] [numeric](19, 6) NULL,"
                            + "[mnyb] [numeric](19, 6) NULL,"
                            + "[memo] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[recordno] [numeric](10, 0) NULL,"
                            + " CONSTRAINT [PK_saledisd] PRIMARY KEY CLUSTERED "
                            + " ("
                            + " [inid] ASC"
                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + " ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " where TABLE_NAME = N'saledisbom';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE saledisbom("
                            + "[inno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[BomID] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                            + "[BomRec] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itname] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itunit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itqty] [numeric](19, 6) NULL,"
                            + "[itpareprs] [numeric](19, 6) NULL,"
                            + "[itpkgqty] [numeric](19, 6) NULL,"
                            + "[itrec] [numeric](6, 0) NULL,"
                            + "[itprice] [numeric](19, 6) NULL,"
                            + "[itprs] [numeric](4, 3) NULL,"
                            + "[itmny] [numeric](19, 6) NULL,"
                            + "[itnote] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[ItSource] [numeric](1, 0) NULL,"
                            + "[ItBuyPri] [numeric](19, 6) NULL,"
                            + "[ItBuyMny] [numeric](19, 6) NULL,"
                            + "[inid] [int] IDENTITY(1,1) NOT NULL,"
                            + "PRIMARY KEY CLUSTERED "
                            + "("
                            + "[inid] ASC"
                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + " ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }
                        #endregion

                        #region 進項發票作業
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " where TABLE_NAME = N'bshopdis';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE [dbo].[bshopdis]("
                            + " [inno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                            + " [indate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [indate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [cono] [nvarchar](2) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [coname1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [fano] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [faname1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [invtaxno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [x5no] [numeric](1, 0) NULL,"
                            + " [x3no] [numeric](1, 0) NULL,"
                            + " [invname] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [invaddr1] [nvarchar](60) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [invalid] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [tax] [numeric](19, 6) NULL,"
                            + " [taxmny] [numeric](19, 6) NULL,"
                            + " [totmny] [numeric](19, 6) NULL,"
                            + " [rate] [numeric](4, 3) NULL,"
                            + " [xa1par] [numeric](11, 4) NULL,"
                            + " [inmemo] [nvarchar](60) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + " [recordno] [numeric](10, 0) NULL,"
                            + " PRIMARY KEY CLUSTERED "
                            + " ("
                            + " [inno] ASC"
                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + " ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " where TABLE_NAME = N'bshopdisd';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE [dbo].[bshopdisd]("
                            + "[inid] [int] IDENTITY(1,1) NOT NULL,"
                            + "[inno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                            + "[indate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[indate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[cono] [nvarchar](2) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[fano] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[x5no] [numeric](1, 0) NULL,"
                            + "[itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itname] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itunit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[punit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[bomid] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[bomrec] [numeric](10, 0) NULL,"
                            + "[ittrait] [numeric](1, 0) NULL,"
                            + "[itpkgqty] [numeric](19, 4) NULL,"
                            + "[pqty] [numeric](19, 4) NULL,"
                            + "[qty] [numeric](19, 4) NULL,"
                            + "[price] [numeric](19, 6) NULL,"
                            + "[prs] [numeric](4, 3) NULL,"
                            + "[rate] [numeric](4, 3) NULL,"
                            + "[xa1par] [numeric](11, 4) NULL,"
                            + "[taxprice] [numeric](19, 6) NULL,"
                            + "[mny] [numeric](19, 6) NULL,"
                            + "[priceb] [numeric](19, 6) NULL,"
                            + "[taxpriceb] [numeric](19, 6) NULL,"
                            + "[mnyb] [numeric](19, 6) NULL,"
                            + "[memo] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[recordno] [numeric](10, 0) NULL,"
                            + " CONSTRAINT [PK_bshopdisd] PRIMARY KEY CLUSTERED "
                            + " ("
                            + " [inid] ASC"
                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + " ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " where TABLE_NAME = N'bshopdisbom';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE bshopdisbom("
                            + "[inno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[BomID] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                            + "[BomRec] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itname] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itunit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itqty] [numeric](19, 6) NULL,"
                            + "[itpareprs] [numeric](19, 6) NULL,"
                            + "[itpkgqty] [numeric](19, 6) NULL,"
                            + "[itrec] [numeric](6, 0) NULL,"
                            + "[itprice] [numeric](19, 6) NULL,"
                            + "[itprs] [numeric](4, 3) NULL,"
                            + "[itmny] [numeric](19, 6) NULL,"
                            + "[itnote] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[ItSource] [numeric](1, 0) NULL,"
                            + "[ItBuyPri] [numeric](19, 6) NULL,"
                            + "[ItBuyMny] [numeric](19, 6) NULL,"
                            + "[inid] [int] IDENTITY(1,1) NOT NULL,"
                            + "PRIMARY KEY CLUSTERED "
                            + "("
                            + "[inid] ASC"
                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                            + " ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }
                        #endregion

                        cmd.CommandText = " SELECT SCNO FROM SCRITD GROUP BY SCNO";
                        using (DataTable table = new DataTable())
                        {
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(table);
                            }
                            foreach (DataRow rw in table.Rows)
                            {
                                cmd.CommandText = "  insert into scritd (scno,taname,sc01,sc02,sc03,sc04,sc05,sc06,sc07,sc08,sc09) values('" + rw["scno"].ToString().Trim() + "','年度統計報表','V','V','V','V','V','V','V','V','')";
                                cmd.ExecuteNonQuery();
                            }
                        }

                        cmd.CommandText = "update systemset set vers ='12.8'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v129()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        AddColumn(cmd, "saledis", "didate", "NVARCHAR(10)");
                        AddColumn(cmd, "saledis", "didate1", "NVARCHAR(10)");
                        AddColumn(cmd, "saledisd", "didate", "NVARCHAR(10)");
                        AddColumn(cmd, "saledisd", "didate1", "NVARCHAR(10)");

                        AddColumn(cmd, "bshopdis", "didate", "NVARCHAR(10)");
                        AddColumn(cmd, "bshopdis", "didate1", "NVARCHAR(10)");
                        AddColumn(cmd, "bshopdisd", "didate", "NVARCHAR(10)");
                        AddColumn(cmd, "bshopdisd", "didate1", "NVARCHAR(10)");

                        cmd.CommandText = " update scritd set taname = '倉庫編號修改' where taname = '產品編號修改' and taform is null";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "update systemset set vers ='12.9'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v130()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        AddColumn(cmd, "bshopinv", "InputDay", "NVARCHAR(20)");
                        AddColumn(cmd, "bshopdis", "InputDay", "NVARCHAR(20)");
                        AddColumn(cmd, "saleinv", "InputDay", "NVARCHAR(20)");
                        AddColumn(cmd, "saledis", "InputDay", "NVARCHAR(20)");

                        cmd.CommandText = @"
                        Update bshopinv set InputDay = '' where InputDay is null;
                        Update bshopdis set InputDay = '' where InputDay is null;
                        Update saleinv  set InputDay = '' where InputDay is null;
                        Update saledis  set InputDay = '' where InputDay is null; ";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "update systemset set vers ='13.0'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v131()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.CommandText = "select count(name) as 是否有欄位 from syscolumns where id=(select id from sysobjects where name=N'saledis') and name=N'dino'";
                        if (cmd.ExecuteScalar().ToString() == "0")
                        {
                            AddColumn(cmd, "saledis", "dino", "NVARCHAR(16)");
                            AddColumn(cmd, "saledisd", "dino", "NVARCHAR(16)");
                            AddColumn(cmd, "saledisbom", "dino", "NVARCHAR(16)");

                            AddColumn(cmd, "bshopdis", "dino", "NVARCHAR(16)");
                            AddColumn(cmd, "bshopdisd", "dino", "NVARCHAR(16)");
                            AddColumn(cmd, "bshopdisbom", "dino", "NVARCHAR(16)");

                            DataTable temp = new DataTable();
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                cmd.CommandText = "Select * from saledis";
                                da.Fill(temp);
                            }

                            var date = Date.GetDateTime(1);
                            for (int i = 0; i < temp.Rows.Count; i++)
                            {
                                var inno = temp.Rows[i]["inno"].ToString().Trim();
                                var dino = date + (i + 1).ToString().PadLeft(4, '0');

                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("inno", inno);
                                cmd.Parameters.AddWithValue("dino", dino);

                                cmd.CommandText = @"
                                Update saledis    set dino = @dino where inno = @inno;
                                Update saledisd   set dino = @dino where inno = @inno;
                                Update saledisbom set dino = @dino where inno = @inno; ";
                                cmd.ExecuteNonQuery();
                            }

                            DataTable temp1 = new DataTable();
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                cmd.CommandText = "Select * from bshopdis";
                                da.Fill(temp1);
                            }

                            for (int i = 0; i < temp1.Rows.Count; i++)
                            {
                                var inno = temp1.Rows[i]["inno"].ToString().Trim();
                                var dino = date + (i + 1).ToString().PadLeft(4, '0');

                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("inno", inno);
                                cmd.Parameters.AddWithValue("dino", dino);

                                cmd.CommandText = @"
                                Update bshopdis    set dino = @dino where inno = @inno;
                                Update bshopdisd   set dino = @dino where inno = @inno;
                                Update bshopdisbom set dino = @dino where inno = @inno; ";
                                cmd.ExecuteNonQuery();
                            }

                            DataTable temp2 = new DataTable();
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "sp_pkeys";

                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("table_name", "saledis");
                                da.Fill(temp2);
                            }

                            var PK_NAME = temp2.Rows[0]["PK_NAME"].ToString().Trim();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "ALTER TABLE [dbo].[saledis] DROP CONSTRAINT " + PK_NAME;
                            cmd.ExecuteNonQuery();

                            DataTable temp3 = new DataTable();
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.CommandText = "sp_pkeys";

                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("table_name", "bshopdis");
                                da.Fill(temp3);
                            }

                            PK_NAME = temp3.Rows[0]["PK_NAME"].ToString().Trim();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "ALTER TABLE [dbo].[bshopdis] DROP CONSTRAINT " + PK_NAME;
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = @"Alter Table saledis  Alter Column dino nvarchar(16) COLLATE Chinese_Taiwan_Stroke_BIN not null";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = @"Alter Table bshopdis Alter Column dino nvarchar(16) COLLATE Chinese_Taiwan_Stroke_BIN not null";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = "update systemset set vers ='13.1'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v132()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.CommandText = @"Alter Table saledis add primary key (dino);";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = @"Alter Table bshopdis add primary key (dino);";
                        cmd.ExecuteNonQuery();

                        //ChangeColumnType(cmd, "cust", "WebID", "NVARCHAR(250)");
                        //ChangeColumnType(cmd, "cust", "WebPassWord", "NVARCHAR(20)");

                        cmd.CommandText = "update systemset set vers ='13.2'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v133()
        {
            try
            {
                var config = Environment.CurrentDirectory + @"\JS.exe.config";
                if (System.IO.File.Exists(config) == false)
                {
                    MessageBox.Show("找不到config檔");
                    return false;
                }

                var xml = XDocument.Load(config);
                var runtime = xml.Descendants("runtime").FirstOrDefault();
                if (runtime == null)
                {
                    XNamespace name = "urn:schemas-microsoft-com:asm.v1";

                    var probing = new XElement(name + "probing", new XAttribute("privatePath", "DLL"));
                    var assemblyBinding = new XElement(name + "assemblyBinding", probing);

                    runtime = new XElement("runtime", assemblyBinding);

                    var configuration = xml.Descendants("configuration").FirstOrDefault();
                    configuration.Add(runtime);

                    xml.Save(config);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }

            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;



                        cmd.CommandText = "update systemset set vers ='13.3'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v134()
        {
            InitializeDatabase();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " where TABLE_NAME = N'batch';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE batch("
                            + "[invno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                            + "[invdate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[invdate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[x5no] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[x5name] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[x3no] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[x3name] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[taxmny] [decimal](20, 10) NULL,"
                            + "[tax] [decimal](20, 10) NULL,"
                            + "[totmny] [decimal](20, 10) NULL,"
                            + "[invmemo] [nvarchar](60) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[cuno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[cuname1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[invtaxno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[invname] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[invaddr1] [nvarchar](60) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[cuname2] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "CONSTRAINT [PK_batch] PRIMARY KEY CLUSTERED "
                            + "("
                            + "[invno] ASC"
                            + ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]"
                            + ") ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " where TABLE_NAME = N'batchd';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE batchd("
                            + "[id] [int] IDENTITY(1,1) NOT NULL,"
                            + "[invno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[kind] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[sano] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[itname] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[qty] [decimal](20, 10) NULL,"
                            + "[itunit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[prs] [decimal](20, 10) NULL,"
                            + "[price] [decimal](20, 10) NULL,"
                            + "[taxprice] [decimal](20, 10) NULL,"
                            + "[mny] [decimal](20, 10) NULL,"
                            + "[itpkgqty] [decimal](20, 10) NULL,"
                            + "[memo] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[recordno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "CONSTRAINT [PK_batchd] PRIMARY KEY CLUSTERED "
                            + "("
                            + "[id] ASC"
                            + ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]"
                            + ") ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " where TABLE_NAME = N'MoneyBoxLog';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE MoneyBoxLog("
                            + "[id] [int] IDENTITY(1,1) NOT NULL,"
                            + "[machine] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[shno]    [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[shname]  [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[emno]    [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[emname]  [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[opdate]  [nvarchar](10) NULL,"
                            + "[opdate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "[memo]    [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                            + "CONSTRAINT [PK_MoneyBoxLog] PRIMARY KEY CLUSTERED "
                            + "("
                            + "[id] ASC"
                            + ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]"
                            + ") ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " Where TABLE_NAME = N'SendMail';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = @"
                            CREATE TABLE [dbo].[SendMail](
	                            [id] [int] IDENTITY(1,1) NOT NULL,
	                            [sendid] [nvarchar](100) NULL,
	                            [sendpw] [nvarchar](100) NULL,
	                            [geter] [text] NULL,
                             CONSTRAINT [PK_SendMail] PRIMARY KEY CLUSTERED 
                            (
	                            [id] ASC
                            )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                            ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        AddColumnAndDValue(cmd, "scrit", "CanCelPrompt", "NVARCHAR(1)", "2");

                        AddColumn(cmd, "saleinv", "PrDate", "NVARCHAR(10)");
                        AddColumn(cmd, "saleinvd", "PrDate", "NVARCHAR(10)");
                        AddColumn(cmd, "saledis", "PrDate", "NVARCHAR(10)");
                        AddColumn(cmd, "saledisd", "PrDate", "NVARCHAR(10)");
                        AddColumn(cmd, "bshopinv", "PrDate", "NVARCHAR(10)");
                        AddColumn(cmd, "bshopinvd", "PrDate", "NVARCHAR(10)");
                        AddColumn(cmd, "bshopdis", "PrDate", "NVARCHAR(10)");
                        AddColumn(cmd, "bshopdisd", "PrDate", "NVARCHAR(10)");

                        AddColumn(cmd, "saleinv", "PrDate1", "NVARCHAR(10)");
                        AddColumn(cmd, "saleinvd", "PrDate1", "NVARCHAR(10)");
                        AddColumn(cmd, "saledis", "PrDate1", "NVARCHAR(10)");
                        AddColumn(cmd, "saledisd", "PrDate1", "NVARCHAR(10)");
                        AddColumn(cmd, "bshopinv", "PrDate1", "NVARCHAR(10)");
                        AddColumn(cmd, "bshopinvd", "PrDate1", "NVARCHAR(10)");
                        AddColumn(cmd, "bshopdis", "PrDate1", "NVARCHAR(10)");
                        AddColumn(cmd, "bshopdisd", "PrDate1", "NVARCHAR(10)");

                        AddColumnAndDValue(cmd, "sale", "offline", "NVARCHAR(20)", "x");
                        AddColumnAndDValue(cmd, "rsale", "offline", "NVARCHAR(20)", "x");
                        AddColumnAndDValue(cmd, "receiv", "offline", "NVARCHAR(20)", "x");
                        AddColumnAndDValue(cmd, "sale", "online", "NVARCHAR(20)", "x");
                        AddColumnAndDValue(cmd, "rsale", "online", "NVARCHAR(20)", "x");
                        AddColumnAndDValue(cmd, "receiv", "online", "NVARCHAR(20)", "x");
                        AddColumn(cmd, "oustkd", "instkdBomId", "NVARCHAR(30)");
                        AddColumn(cmd, "sale", "die", "NVARCHAR(8)");

                        cmd.CommandText = "update systemset set vers ='13.4'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v135()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " Where TABLE_NAME = N'DeliveryAddress';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)//DeliveryAddress ， 指送Table
                        {
                            cmd.CommandText = @"CREATE TABLE [dbo].[DeliveryAddress](
                                [id] [int] IDENTITY(1,1)    NOT NULL,  
                                [cuno] [nvarchar] (10)      COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL ,  
                                [DaNo] [nvarchar] (10)      COLLATE Chinese_Taiwan_Stroke_BIN , 
                                [name] [nvarchar] (50)      COLLATE Chinese_Taiwan_Stroke_BIN , 
	                            [Zip] [nvarchar](3)         COLLATE Chinese_Taiwan_Stroke_BIN NULL,  
	                            [Addr] [nvarchar](60)     COLLATE Chinese_Taiwan_Stroke_BIN NULL,  
                                [per1][nvarchar](10)      COLLATE Chinese_Taiwan_Stroke_BIN NULL,  
	                            [Tel] [nvarchar](20)      COLLATE Chinese_Taiwan_Stroke_BIN NULL,  
	                            [DefaultPrint] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN NULL, 
 CONSTRAINT [PK_DeliveryAddress] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
";
                            cmd.ExecuteNonQuery();


                        }
                        //銷貨_指送地址
                        AddColumn(cmd, "sale", "Adper1", "NVARCHAR(10)");
                        AddColumn(cmd, "sale", "Adtel", "NVARCHAR(20)");
                        AddColumn(cmd, "sale", "CuAddr", "NVARCHAR(60)");
                        cmd.CommandText = "Select count(*) from syscolumns a left join sysobjects b on (a.[id]=b.[id]) where b.[name] = 'sale' and  a.[name]='AdAddr'";
                        var count = cmd.ExecuteScalar().ToDecimal();
                        if (count == 0)
                        {
                            AddColumn(cmd, "sale", "AdAddr", "NVARCHAR(60)");
                            cmd.CommandText = "Update sale Set sale.Adaddr = cust.cuaddr1 ,sale.adtel = cust.cutel1,sale.adper1 =SUBSTRING(cust.cuper1,1,10) from sale inner join cust on sale.cuno=cust.cuno";
                            cmd.ExecuteNonQuery();
                        }
                        AddColumn(cmd, "saled", "AdAddr",  "NVARCHAR(60)");  //指送地址
                        AddColumn(cmd, "saled", "Adper1",  "NVARCHAR(10)");  //指送負責人
                        AddColumn(cmd, "saled", "Adtel",   "NVARCHAR(20)");  //指送電話
                        ChangeColumnType(cmd, "saled", "Adtel", "NVARCHAR(20)");
                        AddColumn(cmd, "saled", "AdName", "NVARCHAR(50)");   //指送公司名稱

                        //訂單_指送地址
                        cmd.CommandText = "Select count(*) from syscolumns a left join sysobjects b on (a.[id]=b.[id]) where b.[name] = 'order' and  a.[name]='AdAddr'";
                        var count_ = cmd.ExecuteScalar().ToDecimal();
                        if (count_ == 0)
                        {
                            cmd.CommandText = " ALTER TABLE dbo.[order] ADD AdAddr NVARCHAR(60) COLLATE Chinese_Taiwan_Stroke_BIN NULL;"
                            + " ALTER TABLE dbo.[order] ALTER COLUMN AdAddr NVARCHAR(60) COLLATE Chinese_Taiwan_Stroke_BIN;";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "Update [order] Set Adaddr = cust.cuaddr1 from [order] inner join cust on [order].cuno=cust.cuno";
                            cmd.ExecuteNonQuery();
                        }

                        AddColumn(cmd, "orderd", "AdAddr", "NVARCHAR(60)");  //指送地址
                        AddColumn(cmd, "orderd", "Adper1", "NVARCHAR(10)");  //指送負責人
                        AddColumn(cmd, "orderd", "Adtel",  "NVARCHAR(20)");   //指送電話
                        AddColumn(cmd, "orderd", "AdName", "NVARCHAR(50)");  //指送公司名稱

                        AddColumn(cmd, "nullify", "die", "NVARCHAR(4)");
                        AddColumn(cmd, "posinv", "die", "NVARCHAR(4)");

                        AddColumnAndDValue(cmd, "Systemset", "LowStockMode", "NVARCHAR(1)", "1");
                        AddColumn(cmd, "scrit", "InvSalePort", "NVARCHAR(30)");
                        cmd.CommandText = "update systemset set vers ='13.5'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v136()
        {
            InitializeDatabase();
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        AddColumn(cmd, "sale", "dietime", "NVARCHAR(20)");
                        AddColumn(cmd, "sale", "dietime", "NVARCHAR(20)");
                        AddColumn(cmd, "sale", "dietime", "NVARCHAR(20)");

                        AddColumn(cmd, "nullify", "dietime", "NVARCHAR(20)");
                        AddColumnAndDValue(cmd, "systemset", "defaultAddr", "NVARCHAR(1)", "1");
                        AddColumn(cmd, "saled", "NetNo", "NVARCHAR(16)");
                        AddColumn(cmd, "weborder", "CuAddr1", "NVARCHAR(60)");
                        AddColumn(cmd, "systemset", "bookNo", "NVARCHAR(20)");
                        AddColumn(cmd, "MachineSet", "InvNoWarning", "NUMERIC(2,0)");
                        AddColumnAndDValue(cmd, "item", "itdisc", "NUMERIC(3,0)", "100");
                        AddColumnAndDValue(cmd, "MachineSet", "X3No", "NVARCHAR(1)", "2");
                        AddColumnAndDValue(cmd, "Scrit", "CanEditX3No", "NVARCHAR(1)", "2");
                        AddColumn(cmd, "MachineSet", "HalfPort", "NVARCHAR(30)");
                        AddColumn(cmd, "MachineSet", "PaperPort", "NVARCHAR(30)");

                        AddColumn(cmd, "Sale", "SaPayment" , "NVARCHAR(60)");
                        AddColumn(cmd, "Rsale", "SaPayment", "NVARCHAR(60)");
                        AddColumn(cmd, "Cust", "CustSource", "NVARCHAR(8)");
                        cmd.CommandText = "Select count(*) from syscolumns a left join sysobjects b on (a.[id]=b.[id]) where b.[name] = 'order' and  a.[name]='CardNo'";
                        var count_ = cmd.ExecuteScalar().ToDecimal();
                        if (count_ == 0)
                        {
                            cmd.CommandText = " ALTER TABLE dbo.[order] ADD CardNo NVARCHAR(20) COLLATE Chinese_Taiwan_Stroke_BIN NULL;";
                        }
                        cmd.ExecuteNonQuery();

                        AddColumn(cmd, "WebOrder", "CardNo", "NVARCHAR(20)");

                        cmd.CommandText = @"update posinv set die = memo where len(memo)>0 and memo='作廢' and die is null";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = @"update sale set sale.die = posinv.memo from posinv left join sale on posinv.sano = sale.sano where len(posinv.memo)>0 and posinv.memo='作廢' and sale.die is null";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Select Count(*) from machineset";
                        var count = cmd.ExecuteScalar().ToDecimal();
                        if (count == 5)
                        {
                            for (int i = 5; i < 10; i++)
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("machine", i + 1);
                                cmd.Parameters.AddWithValue("X3No", "2");
                                cmd.CommandText = "Insert into MachineSet (machine,X3No) Values (@machine,@X3No)";
                                cmd.ExecuteNonQuery();
                            }
                        }

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                + " Where TABLE_NAME = N'itemTempPrice';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = @" 
    CREATE TABLE [dbo].[itemTempPrice](
	    [itno]      [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,
	    [itname]    [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	    [itunit]    [nvarchar](4)  COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	    [itunitp]   [nvarchar](4)  COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	    [itprice]   [numeric](20, 6) NULL,
	    [itprice1]  [numeric](20, 6) NULL,
	    [itprice2]  [numeric](20, 6) NULL,
	    [itprice3]  [numeric](20, 6) NULL,
	    [itprice4]  [numeric](20, 6) NULL,
	    [itprice5]  [numeric](20, 6) NULL,
	    [itpricep]  [numeric](20, 6) NULL,
	    [itpricep1] [numeric](20, 6) NULL,
	    [itpricep2] [numeric](20, 6) NULL,
	    [itpricep3] [numeric](20, 6) NULL,
	    [itpricep4] [numeric](20, 6) NULL,
	    [itpricep5] [numeric](20, 6) NULL,
	    [itbuypri]  [numeric](20, 6) NULL,
	    [itbuyprip] [numeric](20, 6) NULL,
        CONSTRAINT [PK_itemTempPrice] PRIMARY KEY CLUSTERED 
    (
	    [itno] ASC
    )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
    ) ON [PRIMARY] ";
                            cmd.ExecuteNonQuery();
                        } 


                        cmd.CommandText = "update systemset set vers ='13.6'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v137()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        AddColumn(cmd, "FQuot", "PhotoPath", "NVARCHAR(100)");
                        AddColumn(cmd, "Ford", "PhotoPath", "NVARCHAR(100)");
                        AddColumn(cmd, "BShop", "PhotoPath", "NVARCHAR(100)");
                        AddColumn(cmd, "RShop", "PhotoPath", "NVARCHAR(100)");
                        AddColumn(cmd, "Rsale", "PhotoPath", "NVARCHAR(100)");
                        AddColumn(cmd, "sale", "PhotoPath", "NVARCHAR(100)");
                        AddColumn(cmd, "quote", "PhotoPath", "NVARCHAR(100)");
                        //訂單_指送地址
                        cmd.CommandText = "Select count(*) from syscolumns a left join sysobjects b on (a.[id]=b.[id]) where b.[name] = 'order' and  a.[name]='PhotoPath'";
                        var count__ = cmd.ExecuteScalar().ToDecimal();
                        if (count__ == 0)
                        {
                            cmd.CommandText = " ALTER TABLE dbo.[order] ADD PhotoPath NVARCHAR(100) COLLATE Chinese_Taiwan_Stroke_BIN NULL;"
                                            + " ALTER TABLE dbo.[order] ALTER COLUMN PhotoPath NVARCHAR(100) COLLATE Chinese_Taiwan_Stroke_BIN;";
                            cmd.ExecuteNonQuery();
                        }

                        AddColumn(cmd, "sale", "die", "NVARCHAR(8)");
                        AddColumn(cmd, "nullify", "die", "NVARCHAR(4)");
                        AddColumn(cmd, "posinv", "die", "NVARCHAR(4)");
                        AddColumnAndDValue(cmd, "MachineSet", "InvContentKind", "NVARCHAR(2)", "1");

                        cmd.CommandText = "update systemset set vers ='13.7'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v138()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {

                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;


                        #region 發票系統銷項發票TABLE
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " where TABLE_NAME = N'sinv';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE [dbo].[sinv]("
	                                        + " [invid] [nvarchar](14) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                                            + " [invno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [invdate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [invdate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [x5no] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [x5name] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [x3no] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [x3name] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [cuno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [cuname1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [cuname2] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [invname] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [invtaxno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [invaddr1] [nvarchar](60) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [taxmny] [decimal](20, 10) NULL,"
                                            + " [tax] [decimal](20, 10) NULL,"
                                            + " [totmny] [decimal](20, 10) NULL,"
                                            + " [invmemo] [nvarchar](60) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [invkind] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [otherno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [passmode] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [specialtax] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [seno] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [einvstate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [einvchange] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [einv] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " CONSTRAINT [PK_sinv_1] PRIMARY KEY CLUSTERED " 
                                            + " ("
	                                        + " [invid] ASC"
                                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                            + " ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }
                        #endregion

                        #region 發票系統銷項發票明細TABLE

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " where TABLE_NAME = N'sinvd';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = "CREATE TABLE [dbo].[sinvd]("
                                            + " [id] [int] IDENTITY(1,1) NOT NULL,"
                                            + " [invid] [nvarchar](14) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                                            + " [itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [itname] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [qty] [decimal](20, 10) NULL,"
                                            + " [itunit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [price] [decimal](20, 10) NULL,"
                                            + " [taxprice] [decimal](20, 10) NULL,"
                                            + " [mny] [decimal](20, 10) NULL,"
                                            + " [itpkgqty] [decimal](20, 10) NULL,"
                                            + " [memo] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [recordno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [prs] [decimal](4, 3) NULL,"
                                            + " CONSTRAINT [PK_SInvD] PRIMARY KEY CLUSTERED "
                                            + " ("
                                            + " [id] ASC"
                                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                            + " ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        #endregion

                        #region 發票系統進項發票TABLE
                         cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                         + " where TABLE_NAME = N'binv';";
                         if (cmd.ExecuteScalar().ToDecimal() == 0)
                         {
                             cmd.CommandText = "CREATE TABLE [dbo].[binv]("
                                             + " [invid] [nvarchar](14) COLLATE Chinese_Taiwan_Stroke_BIN  NOT NULL,"
                                             + " [invno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " [invdate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " [invdate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " [x5no] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " [x5name] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " [x3no] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " [x3name] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " [fano] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " [faname2] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " [faname1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " [faper] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " [fatel1] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " [faaddr1] [nvarchar](60) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " [invname] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " [invtaxno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " [invaddr1] [nvarchar](60) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
	                                         + " [taxmny] [decimal](20, 10) NULL,"
	                                         + " [tax] [decimal](20, 10) NULL,"
	                                         + " [totmny] [decimal](20, 10) NULL,"
                                             + " [invmemo] [nvarchar](60) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " [invkind] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " [otherno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " [customsno] [nvarchar](14) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " [sub] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " [IsExpense] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN  NULL,"
                                             + " CONSTRAINT [PK_binv] PRIMARY KEY CLUSTERED "
                                             + " ("
	                                         + " [invid] ASC"
                                             + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                             + " ) ON [PRIMARY]";
                             cmd.ExecuteNonQuery();
                         }

                        #endregion

                        #region 發票系統進項發票明細TABLE

                         cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                         + " where TABLE_NAME = N'binvd';";
                         if (cmd.ExecuteScalar().ToDecimal() == 0)
                         {
                             cmd.CommandText = "CREATE TABLE [dbo].[binvd]("
                                             + " [id] [int] IDENTITY(1,1) NOT NULL,"
                                             + " [invid] [nvarchar](14) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [itname] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [qty] [decimal](20, 10) NULL,"
                                             + " [itunit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [price] [decimal](20, 10) NULL,"
                                             + " [taxprice] [decimal](20, 10) NULL,"
                                             + " [mny] [decimal](20, 10) NULL,"
                                             + " [itpkgqty] [decimal](20, 10) NULL,"
                                             + " [memo] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [recordno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [prs] [decimal](4, 3) NULL,"
                                             + " CONSTRAINT [PK_binvd] PRIMARY KEY CLUSTERED "
                                             + " ("
                                             + " [id] ASC"
                                             + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                             + " ) ON [PRIMARY]";
                             cmd.ExecuteNonQuery();
                         }

                         #endregion

                        #region 進項批開TABLE
                         cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                         + " where TABLE_NAME = N'binvbatch';";
                         if (cmd.ExecuteScalar().ToDecimal() == 0)
                         {
                             cmd.CommandText = "CREATE TABLE [dbo].[binvbatch]("
                                             + " [invno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,"
                                             + " [invdate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [invdate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [x5no] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [x5name] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [x3no] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [x3name] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [invmemo] [nvarchar](60) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [fano] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [faname1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [faname2] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [invtaxno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [invname] [nvarchar](50) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [invaddr1] [nvarchar](60) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
	                                         + " [taxmny] [decimal](20, 10) NULL,"
	                                         + " [tax] [decimal](20, 10) NULL,"
	                                         + " [totmny] [decimal](20, 10) NULL,"
                                             + " [invkind] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [sub] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [customsno] [nvarchar](14) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " CONSTRAINT [PK_binvbatch] PRIMARY KEY CLUSTERED "
                                             + " ("
	                                         + " [invno] ASC"
                                             + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                             + " ) ON [PRIMARY]";
                             cmd.ExecuteNonQuery();
                         }

                        #endregion

                        #region 進項批開明細TABLE
                         cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                         + " where TABLE_NAME = N'binvbatchd';";
                         if (cmd.ExecuteScalar().ToDecimal() == 0)
                         {
                             cmd.CommandText = "CREATE TABLE [dbo].[binvbatchd]("
                                             + " [id] [int] IDENTITY(1,1) NOT NULL,"
                                             + " [invno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [kind] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [bsno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [itname] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [qty] [decimal](20, 10) NULL,"
                                             + " [itunit] [nvarchar](4) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [prs] [decimal](20, 10) NULL,"
                                             + " [price] [decimal](20, 10) NULL,"
                                             + " [taxprice] [decimal](20, 10) NULL,"
                                             + " [mny] [decimal](20, 10) NULL,"
                                             + " [itpkgqty] [decimal](20, 10) NULL,"
                                             + " [memo] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " [recordno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                             + " CONSTRAINT [PK_binvbatchd] PRIMARY KEY CLUSTERED "
                                             + " ("
                                             + " [id] ASC"
                                             + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                             + " ) ON [PRIMARY]";
                             cmd.ExecuteNonQuery();
                         }
                        #endregion

                        AddColumn(cmd, "sale", "invkind", "nvarchar(20)");
                        AddColumn(cmd, "sale", "passmode", "nvarchar(1)");
                        AddColumn(cmd, "sale", "specialtax", "nvarchar(1)");
                        AddColumn(cmd, "rsale", "invkind", "nvarchar(20)");
                        AddColumn(cmd, "rsale", "passmode", "nvarchar(1)");
                        AddColumn(cmd, "rsale", "specialtax", "nvarchar(1)");

                        AddColumn(cmd, "bshop", "invkind", "nvarchar(20)");
                        AddColumn(cmd, "bshop", "customsno", "nvarchar(14)");
                        AddColumn(cmd, "bshop", "sub", "nvarchar(1)");
                        AddColumn(cmd, "bshop", "invbatch", "numeric(1, 0)");
                        AddColumn(cmd, "bshop", "invbatflg", "numeric(1, 0)");

                        AddColumn(cmd, "rshop", "invkind", "nvarchar(20)");
                        AddColumn(cmd, "rshop", "customsno", "nvarchar(14)");
                        AddColumn(cmd, "rshop", "sub", "nvarchar(1)");
                        AddColumn(cmd, "rshop", "invbatch", "numeric(1, 0)");
                        AddColumn(cmd, "rshop", "invbatflg", "numeric(1, 0)");

                        AddColumn(cmd, "batch", "invkind", "nvarchar(20)");
                        AddColumn(cmd, "batch", "passmode", "nvarchar(1)");
                        AddColumn(cmd, "batch", "specialtax", "nvarchar(1)");

                        AddColumn(cmd, "scrit", "ScInvoic7", "NVARCHAR(10)");
                        AddColumn(cmd, "scrit", "ScInvoic7e", "NVARCHAR(10)");
                        AddColumn(cmd, "scrit", "ScInvoic8", "NVARCHAR(10)");
                        AddColumn(cmd, "scrit", "ScInvoic8e", "NVARCHAR(10)");
                        AddColumnAndDValue(cmd, "scrit", "einvuse", "NVARCHAR(1)", "2");

                        AddColumnAndDValue(cmd, "cust", "einv", "NVARCHAR(1)", "2");
                        AddColumnAndDValue(cmd, "cust", "einvchange", "NVARCHAR(10)", "存證");

                        AddColumn(cmd, "sale", "einvstate", "NVARCHAR(10)");
                        AddColumnAndDValue(cmd, "sale", "einvchange", "NVARCHAR(10)", "");
                        AddColumnAndDValue(cmd, "sale", "einv", "NVARCHAR(1)", "2");

                        //電子發票欄位
                        cmd.CommandText = "Select COUNT(*) from XX05 where X5No = '7' and  X5Name='一般電子發票'";
                        var count7 = cmd.ExecuteScalar().ToDecimal();
                        if (count7 == 0)
                        {
                            cmd.CommandText = "INSERT INTO xx05 (X5No, X5Name) VALUES ('7', '一般電子發票')";
                        }
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "Select COUNT(*) from XX05 where X5No = '8' and  X5Name='特種電子發票'";
                        var count8 = cmd.ExecuteScalar().ToDecimal();
                        if (count8 == 0)
                        {
                            cmd.CommandText = "INSERT INTO xx05 (X5No, X5Name) VALUES ('8', '特種電子發票')";
                        }
                        cmd.ExecuteNonQuery();

                        AddColumn(cmd, "rsale", "einvstate", "NVARCHAR(10)");
                        AddColumnAndDValue(cmd, "rsale", "einvchange", "NVARCHAR(10)", "");
                        AddColumnAndDValue(cmd, "rsale", "einv", "NVARCHAR(1)", "2");


                        //單據的附件檔案
                        #region 單據的附件檔案
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " Where TABLE_NAME = N'AffixFile';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)//AffixFile ， 附件檔案Table
                        {
                            cmd.CommandText = @"CREATE TABLE [dbo].[AffixFile](
                                [id] [int] IDENTITY(1,1)      NOT NULL,  
                                [DaType] [nvarchar] (10)      COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL ,  
                                [Datano] [nvarchar] (20)      COLLATE Chinese_Taiwan_Stroke_BIN , 
                                [DaAdd] [nvarchar] (100)      COLLATE Chinese_Taiwan_Stroke_BIN , 
	                            [Dadetail] [nvarchar](100)    COLLATE Chinese_Taiwan_Stroke_BIN NULL,  
	                            CONSTRAINT [PK_AffixFile] PRIMARY KEY CLUSTERED ( [id] ASC ) WITH 
                                (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, 
                                ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] ";
                            cmd.ExecuteNonQuery();
                        }

                        DataTable DaFile = new DataTable();
                        List<string> list = new List<string>       { "FQuot","Ford","Bshop","Rshop","Rsale","sale","quote","order" };
                        List<string> listno = new List<string>     { "fqno","fono","bsno","bsno","sano","sano","quno","orno" };
                        List<string> listDaType = new List<string> { "詢價單","採購單","進貨單","進退單","銷退單","銷貨單","報價單","訂價單" };

                        for (int j = 0; j < list.Count; j++)
                        {
                            DaFile.Columns.Clear();
                            DaFile.Clear();
                            string table = list[j].ToString();
                            string Datano = listno[j].ToString();
                            cmd.CommandText = "Select " + Datano + " , photoPath from [" + table + "] where PhotoPath != '' and " + Datano + "!='' ";
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(DaFile);
                            var FQuotpathcount = DaFile.Rows.Count;
                            if (FQuotpathcount != 0)
                            {
                                for (int i = 0; i < FQuotpathcount; i++)
                                {
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("Datano1", DaFile.Rows[i][0].ToString());
                                    cmd.Parameters.AddWithValue("DaType", listDaType[j].ToString());
                                    cmd.Parameters.AddWithValue("PhotoPath", DaFile.Rows[i]["PhotoPath"].ToString());
                                    cmd.CommandText = "INSERT INTO AffixFile (DaType, Datano , DaAdd , Dadetail ) VALUES (@DaType, @Datano1 , @PhotoPath ,'')";
                                    cmd.ExecuteNonQuery();
                                    //cmd.CommandText = @" update [" + table + "] SET PhotoPath='' WHERE " + Datano + " = @Datano1";
                                    //cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        #endregion
                        #region Turnkey11張表
                        //Turnkey11張表
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                       + " Where TABLE_NAME = N'FROM_CONFIG';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = @"CREATE TABLE [dbo].[FROM_CONFIG](
                                [TRANSPORT_ID]         varchar(10),
                                [TRANSPORT_PASSWORD]   varchar(45),
                                [PARTY_ID]             varchar(10) NOT NULL,
                                [PARTY_DESCRIPTION]    varchar(200),
                                [ROUTING_ID]           varchar(39),
                                [ROUTING_DESCRIPTION]  varchar(200),
                                [SIGN_ID]              varchar(4),
                                [SUBSTITUTE_PARTY_ID]   varchar(10),
                                CONSTRAINT [FROM_CONFIG_PK1] UNIQUE NONCLUSTERED (
                                [PARTY_ID] ASC   
                                ) ON [PRIMARY] ) ";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = @"CREATE  INDEX [FROM_CONFIG_INDEX1] ON [FROM_CONFIG]([SUBSTITUTE_PARTY_ID]) ";
                            cmd.ExecuteNonQuery();
                        }

                         cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                       + " Where TABLE_NAME = N'TURNKEY_TRANSPORT_CONFIG';";
                         if (cmd.ExecuteScalar().ToDecimal() == 0)
                         {
                             cmd.CommandText = @"CREATE TABLE [dbo].[TURNKEY_TRANSPORT_CONFIG](
                                [TRANSPORT_ID] varchar(10) NOT NULL,
                                [TRANSPORT_PASSWORD] varchar(60) NOT NULL,
                                CONSTRAINT [TURNKEY_TRANSPORT_CONFIG_PK1] UNIQUE NONCLUSTERED (
                                [TRANSPORT_ID] ASC   
                                ) ON [PRIMARY] )";
                             cmd.ExecuteNonQuery();
                         }

                         cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " Where TABLE_NAME = N'TURNKEY_USER_PROFILE';";
                         if (cmd.ExecuteScalar().ToDecimal() == 0)
                         {
                             cmd.CommandText = @"CREATE TABLE [dbo].[TURNKEY_USER_PROFILE](
                                  [USER_ID] varchar(10) NOT NULL,
                                  [USER_PASSWORD] varchar(100) NOT NULL,
                                  [USER_ROLE] varchar(2),
                                  CONSTRAINT [TURNKEY_USER_PROFILE_PK1] UNIQUE NONCLUSTERED (
                                  [USER_ID]  ASC       
                                 ) ON [PRIMARY] ) ";
                             cmd.ExecuteNonQuery();
                             cmd.CommandText = @"INSERT INTO [TURNKEY_USER_PROFILE] ([USER_ID],[USER_PASSWORD],[USER_ROLE]) VALUES
                                 ('ADMIN','ADMIN','0')";
                             cmd.ExecuteNonQuery();
                         }
                         cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " Where TABLE_NAME = N'SCHEDULE_CONFIG';";
                         if (cmd.ExecuteScalar().ToDecimal() == 0)
                         {
                             cmd.CommandText = @"CREATE TABLE [dbo].[SCHEDULE_CONFIG](
                               [TASK]                varchar(30) NOT NULL,
                               [ENABLE]               varchar(1),
                               [SCHEDULE_TYPE]        varchar(10),
                               [SCHEDULE_WEEK]        varchar(15),
                               [SCHEDULE_TIME]        varchar(50),
                               [SCHEDULE_PERIOD]      varchar(10),
                               [SCHEDULE_RANGE]       varchar(15),
                               CONSTRAINT [SCHEDULE_CONFIG_PK1] UNIQUE NONCLUSTERED (
                               [TASK]  ASC
                               ) ON [PRIMARY] )";
                             cmd.ExecuteNonQuery();
                         }

                         cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " Where TABLE_NAME = N'SIGN_CONFIG';";
                         if (cmd.ExecuteScalar().ToDecimal() == 0)
                         {
                             cmd.CommandText = @"CREATE TABLE [dbo].[SIGN_CONFIG](
                              [SIGN_ID] varchar(4) NOT NULL,
                              [SIGN_TYPE] varchar(10)  DEFAULT NULL,
                              [PFX_PATH] varchar(100)  DEFAULT NULL,
                              [SIGN_PASSWORD] varchar(60)  DEFAULT NULL,
                              CONSTRAINT [SIGN_CONFIG_PK1] UNIQUE NONCLUSTERED (
                               [SIGN_ID] ASC 
                               ) ON [PRIMARY]) ";
                             cmd.ExecuteNonQuery();
                         }

                         cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                         + " Where TABLE_NAME = N'TASK_CONFIG';";
                         if (cmd.ExecuteScalar().ToDecimal() == 0)
                         {
                             cmd.CommandText = @"CREATE TABLE [dbo].[TASK_CONFIG](
                                [CATEGORY_TYPE]        varchar(5) NOT NULL,
                                [PROCESS_TYPE]         varchar(10) NOT NULL,
                                [TASK]                 varchar(15) NOT NULL,
                                [SRC_PATH]             varchar(200),
                                [TARGET_PATH]          varchar(200),
                                [FILE_FORMAT]          varchar(20),
                                [VERSION]              varchar(5),
                                [ENCODING]             varchar(15),
                                [TRANS_CHINESE_DATE]   varchar(1),
                                CONSTRAINT [TASK_CONFIG_PK1] UNIQUE NONCLUSTERED (
                                [CATEGORY_TYPE], [PROCESS_TYPE], [TASK]  ASC  
                                ) ON [PRIMARY]  )";
                             cmd.ExecuteNonQuery();
                         }

                         cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                         + " Where TABLE_NAME = N'TO_CONFIG';";
                         if (cmd.ExecuteScalar().ToDecimal() == 0)
                         {
                             cmd.CommandText = @"CREATE TABLE [dbo].[TO_CONFIG](
                                [PARTY_ID]             varchar(10) NOT NULL,
                                [PARTY_DESCRIPTION]    varchar(200),
                                [ROUTING_ID]           varchar(39),
                                [ROUTING_DESCRIPTION]  varchar(200),
                                [FROM_PARTY_ID]        varchar(10),
                                CONSTRAINT [TO_CONFIG_PK1] UNIQUE NONCLUSTERED ([FROM_PARTY_ID], [PARTY_ID] ASC 
                                ) ON [PRIMARY] ) ";
                             cmd.ExecuteNonQuery();
                         }
                         cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                          + " Where TABLE_NAME = N'TURNKEY_MESSAGE_LOG';";
                         if (cmd.ExecuteScalar().ToDecimal() == 0)
                         {
                             cmd.CommandText = @"CREATE TABLE [dbo].[TURNKEY_MESSAGE_LOG](
                                [SEQNO] varchar(8) NOT NULL,
                                [SUBSEQNO] varchar(5) NOT NULL,
                                [UUID] varchar(40) DEFAULT NULL,
                                [MESSAGE_TYPE] varchar(10) DEFAULT NULL,
                                [CATEGORY_TYPE] varchar(5) DEFAULT NULL,
                                [PROCESS_TYPE] varchar(10) DEFAULT NULL,
                                [FROM_PARTY_ID] varchar(10) DEFAULT NULL,
                                [TO_PARTY_ID] varchar(10) DEFAULT NULL,
                                [MESSAGE_DTS] varchar(17) DEFAULT NULL,
                                [CHARACTER_COUNT] varchar(10) DEFAULT NULL,
                                [STATUS] varchar(5) DEFAULT NULL,
                                [IN_OUT_BOUND] varchar(1) DEFAULT NULL,
                                [FROM_ROUTING_ID] varchar(39) DEFAULT NULL,
                                [TO_ROUTING_ID] varchar(39) DEFAULT NULL,
                                [INVOICE_IDENTIFIER] varchar(30) DEFAULT NULL,
                                CONSTRAINT [TURNKEY_MESSAGE_LOG_PK1] UNIQUE NONCLUSTERED (
                                [SEQNO], [SUBSEQNO]     ASC   
                                ) ON [PRIMARY] ) ";
                             cmd.ExecuteNonQuery();
                             cmd.CommandText = @"CREATE  INDEX [TURNKEY_MESSAGE_LOG_INDEX1] ON [TURNKEY_MESSAGE_LOG]([MESSAGE_DTS])";
                             cmd.ExecuteNonQuery();
                             cmd.CommandText = @"CREATE  INDEX [TURNKEY_MESSAGE_LOG_INDEX2] ON [TURNKEY_MESSAGE_LOG]([UUID])";
                             cmd.ExecuteNonQuery();
                         }
                         cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                          + " Where TABLE_NAME = N'TURNKEY_MESSAGE_LOG_DETAIL';";
                         if (cmd.ExecuteScalar().ToDecimal() == 0)
                         {
                             cmd.CommandText = @"CREATE TABLE [dbo].[TURNKEY_MESSAGE_LOG_DETAIL](
                                [SEQNO]                varchar(8) NOT NULL,
                                [SUBSEQNO]             varchar(5) NOT NULL,
                                [PROCESS_DTS]          varchar(17),
                                [TASK]                 varchar(30) NOT NULL,
                                [STATUS]               varchar(5),
                                [FILENAME]             varchar(300),
                                [UUID]                 varchar(40),
                                CONSTRAINT [TURNKEY_MESSAGE_LOG_DETAIL_PK1] UNIQUE NONCLUSTERED (
                                [SEQNO], [SUBSEQNO], [TASK] ASC 
                                ) ON [PRIMARY])  ";
                             cmd.ExecuteNonQuery();
                             cmd.CommandText = @"CREATE  INDEX [TURNKEY_MESSAGE_LOG_DETAIL_INDEX1] ON [TURNKEY_MESSAGE_LOG_DETAIL]([FILENAME])";
                             cmd.ExecuteNonQuery();
                         }
                         cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                           + " Where TABLE_NAME = N'TURNKEY_SEQUENCE';";
                         if (cmd.ExecuteScalar().ToDecimal() == 0)
                         {
                             cmd.CommandText = @"CREATE TABLE [dbo].[TURNKEY_SEQUENCE](
                                [SEQUENCE] varchar(8) NOT NULL,
                                CONSTRAINT [TURNKEY_SEQUENCE_PK1] UNIQUE NONCLUSTERED (
                                [SEQUENCE] ASC     
                                ) ON [PRIMARY] ) ";
                             cmd.ExecuteNonQuery();
                         }
                         cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                            + " Where TABLE_NAME = N'TURNKEY_SYSEVENT_LOG';";
                         if (cmd.ExecuteScalar().ToDecimal() == 0)
                         {
                             cmd.CommandText = @"CREATE TABLE [dbo].[TURNKEY_SYSEVENT_LOG](
                                [EVENTDTS]             varchar(17) NOT NULL,
                                [PARTY_ID]             varchar(10),
                                [SEQNO]                varchar(8),
                                [SUBSEQNO]             varchar(5),
                                [ERRORCODE]            varchar(4),
                                [UUID]                 varchar(40),
                                [INFORMATION1]         varchar(100),
                                [INFORMATION2]         varchar(100),
                                [INFORMATION3]         varchar(100),
                                [MESSAGE1]             varchar(100),
                                [MESSAGE2]             varchar(100),
                                [MESSAGE3]             varchar(100),
                                [MESSAGE4]             varchar(100),
                                [MESSAGE5]             varchar(100),
                                [MESSAGE6]             varchar(100),
                                CONSTRAINT [TURNKEY_SYSEVENT_LOG_PK1] UNIQUE NONCLUSTERED (
                                [EVENTDTS] ASC
                                ) ON [PRIMARY] ) ";
                             cmd.ExecuteNonQuery();
                             cmd.CommandText = @"CREATE  INDEX [TURNKEY_SYSEVENT_LOG_INDEX1] ON [TURNKEY_SYSEVENT_LOG]([SEQNO],[SUBSEQNO])";
                             cmd.ExecuteNonQuery();
                             cmd.CommandText = @"CREATE  INDEX [TURNKEY_SYSEVENT_LOG_INDEX2] ON [TURNKEY_SYSEVENT_LOG]([UUID])";
                             cmd.ExecuteNonQuery();
                         }


                        #endregion

                        #region 電子發票用-1109
                        //B2C_POS欄位
                        AddColumnAndDValue(cmd, "sale", "carriertype", "NVARCHAR(10)", "");//載具編號
                        AddColumnAndDValue(cmd, "sale", "carrierid1", "NVARCHAR(20)", "");//載具顯碼
                        AddColumnAndDValue(cmd, "sale", "carrierid2", "NVARCHAR(20)", "");//載具隱碼
                        AddColumnAndDValue(cmd, "sale", "npoban", "NVARCHAR(20)", "");//愛心碼
                        AddColumnAndDValue(cmd, "sale", "printmark", "NVARCHAR(1)", "");//列印註記

                        AddColumnAndDValue(cmd, "rsale", "carriertype", "NVARCHAR(10)", "");//載具編號
                        AddColumnAndDValue(cmd, "rsale", "carrierid1", "NVARCHAR(20)", "");//載具顯碼
                        AddColumnAndDValue(cmd, "rsale", "carrierid2", "NVARCHAR(20)", "");//載具隱碼
                        AddColumnAndDValue(cmd, "rsale", "npoban", "NVARCHAR(20)", "");//愛心碼
                        AddColumnAndDValue(cmd, "rsale", "printmark", "NVARCHAR(1)", "");//列印註記

                        AddColumnAndDValue(cmd, "posinv", "carriertype", "NVARCHAR(10)", "");//載具編號
                        AddColumnAndDValue(cmd, "posinv", "carrierid1", "NVARCHAR(20)", "");//載具顯碼
                        AddColumnAndDValue(cmd, "posinv", "carrierid2", "NVARCHAR(20)", "");//載具隱碼
                        AddColumnAndDValue(cmd, "posinv", "npoban", "NVARCHAR(20)", "");//愛心碼
                        AddColumnAndDValue(cmd, "posinv", "printmark", "NVARCHAR(1)", "");//列印註記

                        AddColumnAndDValue(cmd, "posinv", "einv", "NVARCHAR(1)", "");//是否使用電子發票
                        AddColumnAndDValue(cmd, "posinv", "einvstate", "NVARCHAR(10)", "");//發票狀態
                        AddColumnAndDValue(cmd, "posinv", "einvchange", "NVARCHAR(10)", "");//發票上傳方式
                        AddColumnAndDValue(cmd, "posinv", "invrandom", "NVARCHAR(4)", "");//發票隨機碼

                        AddColumnAndDValue(cmd, "sinv", "carriertype", "NVARCHAR(10)", "");//載具編號
                        AddColumnAndDValue(cmd, "sinv", "carrierid1", "NVARCHAR(20)", "");//載具顯碼
                        AddColumnAndDValue(cmd, "sinv", "carrierid2", "NVARCHAR(20)", "");//載具隱碼
                        AddColumnAndDValue(cmd, "sinv", "npoban", "NVARCHAR(20)", "");//愛心碼
                        AddColumnAndDValue(cmd, "sinv", "printmark", "NVARCHAR(1)", "");//列印註記

                        AddColumnAndDValue(cmd, "sinv", "einvB2CPrint", "NVARCHAR(2)", "");
                        AddColumnAndDValue(cmd, "sale", "einvB2CPrint", "NVARCHAR(2)", "");
                        AddColumnAndDValue(cmd, "rsale", "einvB2CPrint", "NVARCHAR(2)", "");
                        AddColumnAndDValue(cmd, "posinv", "einvB2CPrint", "NVARCHAR(2)", "");
                        AddColumnAndDValue(cmd, "TURNKEY_MESSAGE_LOG", "Reback", "NVARCHAR(1)", "");//註記是否回覆過

                        //進項電子發票
                        AddColumn(cmd, "bshop", "einvstate", "NVARCHAR(10)");
                        AddColumnAndDValue(cmd, "bshop", "einvchange", "NVARCHAR(10)", "");
                        AddColumn(cmd, "rshop", "einvstate", "NVARCHAR(10)");
                        AddColumnAndDValue(cmd, "rshop", "einvchange", "NVARCHAR(10)", "");
                   
                  
                        #endregion

                        //廠商特價區間
                        AddColumn(cmd, "Special", "Cuno"  , "NVARCHAR(10)");
                        AddColumn(cmd, "Special", "Cux1No", "NVARCHAR(2)");

                        //發票防伪碼
                        AddColumnAndDValue(cmd, "sale", "invrandom", "NVARCHAR(4)", "");
                        AddColumnAndDValue(cmd, "rsale", "invrandom", "NVARCHAR(4)", "");
                        AddColumnAndDValue(cmd, "bshop", "invrandom", "NVARCHAR(4)", "");
                        AddColumnAndDValue(cmd, "rshop", "invrandom", "NVARCHAR(4)", "");

                        //進項電子發票
                        AddColumn(cmd, "bshop", "einvstate", "NVARCHAR(10)");
                        AddColumnAndDValue(cmd, "bshop", "einvchange", "NVARCHAR(10)", "");
                        AddColumn(cmd, "rshop", "einvstate", "NVARCHAR(10)");
                        AddColumnAndDValue(cmd, "rshop", "einvchange", "NVARCHAR(10)", "");
                       

                        cmd.CommandText = "update systemset set vers ='13.8'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v139()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        //批次作業
                        #region  BatchInformation
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'BatchInformation';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = @"
CREATE TABLE [dbo].[BatchInformation](
	[Bno] [int] IDENTITY(1,1) NOT NULL,
	[Itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,
	[Fano] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,
	[Batchno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,
	[Date] [nvarchar](8) NULL,
	[Date1] [nvarchar](8) NULL,
	[Qty] [numeric](20, 4) NULL,
 CONSTRAINT [PK_BatchInformation] PRIMARY KEY CLUSTERED 
(
	[Itno] ASC,
	[Fano] ASC,
	[Batchno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();

                            CerateNonCLUSTERED(cmd, "BatchInformation", "Bno");

                        }


                        #endregion
                        #region  BatchStock
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'BatchStock';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = @"
CREATE TABLE [dbo].[BatchStock](
	[Bno] [int] NOT NULL,
	[Stno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,
	[itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	[StnoQty] [numeric](20, 4) NULL,
 CONSTRAINT [PK_BatchStock] PRIMARY KEY CLUSTERED 
(
	[Bno] ASC,
	[Stno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();

                        }
                        #endregion
                        #region  BatchProcess_Saled
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'BatchProcess_Saled';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText =
@"
CREATE TABLE [dbo].[BatchProcess_Saled](
	[Bno] [int] NULL,
	[bomid] [nvarchar](30)  COLLATE Chinese_Taiwan_Stroke_BIN  NOT NULL,
	[rec] [numeric](6, 0)  NOT NULL,
	[qty] [numeric](20, 4) NULL,
	[Cuno] [nvarchar](10)  COLLATE Chinese_Taiwan_Stroke_BIN  NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_BatchProcess_Saled] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
";
                            cmd.ExecuteNonQuery();

                            CerateNonCLUSTERED(cmd, "BatchProcess_Saled", "Bno");
                        }
                        #endregion
                        #region  BatchProcess_rSaleD
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'BatchProcess_rSaleD';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText =
@"
CREATE TABLE [dbo].[BatchProcess_rSaleD](
	[Bno] [int] NULL,
	[bomid] [nvarchar](30)  COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,
	[rec] [numeric](6, 0) NOT NULL,
	[qty] [numeric](20, 4) NULL,
	[Cuno] [nvarchar](10)  COLLATE Chinese_Taiwan_Stroke_BIN  NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_BatchProcess_rSaleD] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();

                            CerateNonCLUSTERED(cmd, "BatchProcess_rSaleD", "Bno");
                        }
                        #endregion
                        #region  BatchProcess_Drawd
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'BatchProcess_Drawd';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText =
@"
CREATE TABLE [dbo].[BatchProcess_Drawd](
	[Bno] [int] NULL,
	[bomid] [nvarchar](30)  COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,
	[rec] [numeric](6, 0) NOT NULL,
	[qty] [numeric](20, 4) NULL,
	[Cuno] [nvarchar](10)  COLLATE Chinese_Taiwan_Stroke_BIN  NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_BatchProcess_Drawd] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();

                            CerateNonCLUSTERED(cmd, "BatchProcess_Drawd", "Bno");
                        }
                        #endregion
                        #region  BatchProcess_Drawbom
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'BatchProcess_Drawbom';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText =
@"
CREATE TABLE [dbo].[BatchProcess_Drawbom](
	[Bno] [int] NULL,
	[bomid] [nvarchar](30)  COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,
	[rec] [numeric](6, 0) NOT NULL,
	[qty] [numeric](20, 4) NULL,
	[Cuno] [nvarchar](10)  COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_BatchProcess_Drawbom] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();

                            CerateNonCLUSTERED(cmd, "BatchProcess_Drawbom", "Bno");
                        }
                        #endregion
                        #region  BatchProcess_Garnerd
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'BatchProcess_Garnerd';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText =
@"
CREATE TABLE [dbo].[BatchProcess_Garnerd](
	[Bno] [int] NULL,
	[bomid] [nvarchar](30)  COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,
	[rec] [numeric](6, 0) NOT NULL,
	[qty] [numeric](20, 4) NULL,
	[Cuno] [nvarchar](10)  COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_BatchProcess_Garnerd] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();

                            CerateNonCLUSTERED(cmd, "BatchProcess_Garnerd", "Bno");
                        }
                        #endregion
                        #region  BatchProcess_GarnerBom
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'BatchProcess_GarnerBom';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText =
@"
CREATE TABLE [dbo].[BatchProcess_GarnerBom](
	[Bno] [int] NULL,
	[bomid] [nvarchar](30)  COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,
	[rec] [numeric](6, 0) NOT NULL,
	[qty] [numeric](20, 4) NULL,
	[Cuno] [nvarchar](10)  COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_BatchProcess_GarnerBom] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();

                            CerateNonCLUSTERED(cmd, "BatchProcess_GarnerBom", "Bno");
                        }
                        #endregion
                        #region  BatchProcess_bShopd
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'BatchProcess_bShopd';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText =
@"
CREATE TABLE [dbo].[BatchProcess_bShopd](
	[Bno] [int] NULL,
	[bomid] [nvarchar](30)  COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,
	[rec] [numeric](6, 0) NOT NULL,
	[qty] [numeric](20, 4) NULL,
	[Cuno] [nvarchar](10)  COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_BatchProcess_bShopd] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();

                            CerateNonCLUSTERED(cmd, "BatchProcess_bShopd", "Bno");
                        }
                        #endregion
                        #region  BatchProcess_RShop
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'BatchProcess_rShopD';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText =
@"
CREATE TABLE [dbo].[BatchProcess_rShopD](
	[Bno] [int] NULL,
	[bomid] [nvarchar](30)  COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,
	[rec] [numeric](6, 0) NOT NULL,
	[qty] [numeric](20, 4) NULL,
	[Cuno] [nvarchar](10)  COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_BatchProcess_rShopD] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();

                            CerateNonCLUSTERED(cmd, "BatchProcess_rShopD", "Bno");
                        }
                        #endregion
                        #region  BatchProcess_SaleBom
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'BatchProcess_SaleBom';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText =
@"
CREATE TABLE [dbo].[BatchProcess_SaleBom](
	[Bno] [int] NULL,
	[bomid] [nvarchar](30)  COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,
	[rec] [numeric](6, 0) NOT NULL,
	[qty] [numeric](20, 4) NULL,
	[Cuno] [nvarchar](10)  COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_BatchProcess_SaleBom] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();

                            CerateNonCLUSTERED(cmd, "BatchProcess_SaleBom", "Bno");
                        }
                        #endregion
                        #region  BatchProcess_rSaleBom
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'BatchProcess_rSaleBom';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText =
@"
CREATE TABLE [dbo].[BatchProcess_rSaleBom](
	[Bno] [int] NULL,
	[bomid] [nvarchar](30)  COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,
	[rec] [numeric](6, 0) NOT NULL,
	[qty] [numeric](20, 4) NULL,
	[Cuno] [nvarchar](10)  COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_BatchProcess_rSaleBom] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();

                            CerateNonCLUSTERED(cmd, "BatchProcess_rSaleBom", "Bno");
                        }
                        #endregion

                        #region  客戶/廠商 型號
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                        + " where TABLE_NAME = N'standard';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = " CREATE TABLE [dbo].[standard]("
                                            + " [id] [int] IDENTITY(1,1) NOT NULL,"
                                            + " [kind] [nvarchar](10)  COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [cfno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " [standard] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                                            + " CONSTRAINT [PK_standard] PRIMARY KEY CLUSTERED "
                                            + " ("
                                            + " [id] ASC"
                                            + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                                            + " ) ON [PRIMARY] ";
                            cmd.ExecuteNonQuery();
                        }

                        AddColumnAndDValue(cmd, "saled", "standard", "NVARCHAR(20)", "");//銷貨
                        AddColumnAndDValue(cmd, "rsaled", "standard", "NVARCHAR(20)", "");//銷退
                        AddColumnAndDValue(cmd, "quoted", "standard", "NVARCHAR(20)", "");//報價   
                        AddColumnAndDValue(cmd, "orderd", "standard", "NVARCHAR(20)", "");//訂單
                        /*
                         * 各單據的型號欄位增加->在報表輸出列印時,如果廠商或客戶有各自型號就把產品編號改成standard型號欄位AddColumnAndDValue(cmd, "saleinvd", "standard", "NVARCHAR(20)", "");
                        AddColumnAndDValue(cmd, "bshopinvd", "standard", "NVARCHAR(20)", ""); 
                        AddColumnAndDValue(cmd, "bshopd", "standard", "NVARCHAR(20)", "");
                        AddColumnAndDValue(cmd, "rshopd", "standard", "NVARCHAR(20)", "");
                        AddColumnAndDValue(cmd, "fquotd", "standard", "NVARCHAR(20)", "");
                        AddColumnAndDValue(cmd, "fordd", "standard", "NVARCHAR(20)", "");
                        AddColumnAndDValue(cmd, "standard", "price", "numeric(20, 10)", "0");若有需要再增加
                         */
                        #endregion
                        //帳款歸屬-客戶-請款客戶

                        AddColumnAndDValue(cmd, "cust", "payerno", "NVARCHAR(10)","");
                        AddColumnAndDValue(cmd, "sale", "payerno", "NVARCHAR(10)", "");
                        AddColumnAndDValue(cmd, "rsale", "payerno", "NVARCHAR(10)", "");
                        AddColumnAndDValue(cmd, "receivd", "payerno", "NVARCHAR(10)", "");
                        
                        #region 請款客戶(補)
                        cmd.CommandText = " SELECT * FROM sale where payerno = '' OR payerno is null";
                        DataTable salepayerno = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(salepayerno);
                            cmd.Parameters.AddWithValue("cupayerno", "");
                            cmd.Parameters.AddWithValue("sanotemp", "");
                            for (int i = 0; i < salepayerno.Rows.Count; i++)
                            {
                                if (salepayerno.Rows[i]["payerno"].ToString().Trim() == "")
                                {
                                    cmd.Parameters["cupayerno"].Value = salepayerno.Rows[i]["cuno"].ToString();
                                    cmd.Parameters["sanotemp"].Value = salepayerno.Rows[i]["sano"].ToString();
                                    cmd.CommandText = "update sale set payerno=(@cupayerno) where sano=(@sanotemp)";
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        salepayerno.Clear();
                        cmd.CommandText = " SELECT * FROM rsale where payerno = '' OR payerno is null ";
                        DataTable rsalepayerno = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(rsalepayerno);
                            cmd.Parameters.AddWithValue("rcupayerno", "");
                            cmd.Parameters.AddWithValue("rsanotemp", "");
                            for (int i = 0; i < rsalepayerno.Rows.Count; i++)
                            {
                                if (rsalepayerno.Rows[i]["payerno"].ToString().Trim() == "")
                                {
                                    cmd.Parameters["rcupayerno"].Value = rsalepayerno.Rows[i]["cuno"].ToString();
                                    cmd.Parameters["rsanotemp"].Value = rsalepayerno.Rows[i]["sano"].ToString();
                                    cmd.CommandText = "update rsale set payerno=(@rcupayerno) where sano=(@rsanotemp)";
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        rsalepayerno.Clear();
                        salepayerno.Dispose();
                        rsalepayerno.Dispose();
                        cmd.CommandText = " SELECT * FROM cust where payerno = ''";
                        DataTable custpayerno = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(custpayerno);
                            cmd.Parameters.AddWithValue("cunotemp", "");
                            for (int i = 0; i < rsalepayerno.Rows.Count; i++)
                            {
                                if (custpayerno.Rows[i]["payerno"].ToString().Trim() == "")
                                {
                                    cmd.Parameters["cunotemp"].Value = rsalepayerno.Rows[i]["cuno"].ToString();
                                    cmd.CommandText = "update cust set payerno=(@cunotemp) where cuno=(@cunotemp)";
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        rsalepayerno.Clear();
                        #endregion
                        //是否啟用批次管理
                        AddColumnAndDValue(cmd, "systemset", "UsingBatch ", "numeric(1, 0)", "1");
                        //資料庫結構:品名規格長度
                        AddColumnAndDValue(cmd, "systemset", "ItNameLenth", "numeric(3, 0)", "30");
                        //資料庫結構:自訂編號長度
                        AddColumnAndDValue(cmd, "systemset", "ItNoUdfLenth", "numeric(3, 0)", "20");

                        ChangeColumnType(cmd, "Item", "ItBarName1", "nvarchar(16)");
                        ChangeColumnType(cmd, "Item", "ItBarName2", "nvarchar(16)");

                        //資料庫結構:單據常用單位
                        AddColumn(cmd, "Item", "StNo", "nvarchar(10)");

                        //bom 加入規格說明
                        AddColumn(cmd, "bomd", "itdesp1", "nvarchar(40)");
                        AddColumn(cmd, "bomd", "itdesp2", "nvarchar(40)");
                        AddColumn(cmd, "bomd", "itdesp3", "nvarchar(40)");
                        AddColumn(cmd, "bomd", "itdesp4", "nvarchar(40)");
                        AddColumn(cmd, "bomd", "itdesp5", "nvarchar(40)");
                        AddColumn(cmd, "bomd", "itdesp6", "nvarchar(40)");
                        AddColumn(cmd, "bomd", "itdesp7", "nvarchar(40)");
                        AddColumn(cmd, "bomd", "itdesp8", "nvarchar(40)");
                        AddColumn(cmd, "bomd", "itdesp9", "nvarchar(40)");
                        AddColumn(cmd, "bomd", "itdesp10", "nvarchar(40)");
                        cmd.CommandText = "update systemset set vers ='13.9'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v140()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.CommandText = "update systemset set vers ='14.0'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v141()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.CommandText = "update systemset set vers ='14.1'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v142()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;
                        #region SQL Update : 如果有單據未收金額不相同於已收帳款之未收金額，則根據單據未收金額填回已收帳款之未收金額
                        string str_根據單據未收金額填回已收帳款之未收金額 = @"IF (select count(*) from 
(SELECT * FROM Receivd where Bracket!='期初') as a
left join 
(
SELECT AcctMny,sano,Bracket='銷貨' FROM SALE	
union all
SELECT AcctMny,sano,Bracket='銷退' FROM RSALE
)b
on a.bracket = b.bracket and a.sano = b.sano
where a.acctmny != b.acctmny) > 0
BEGIN

update Receivd set Receivd.AcctMny = z.acctmny 
from  
(
	select a.Bracket,a.sano,a.reno,b.acctmny from 
	(SELECT * FROM Receivd where Bracket!='期初') as a
	left join 
	(
	SELECT AcctMny,sano,Bracket='銷貨' FROM SALE	
	union all
	SELECT AcctMny,sano,Bracket='銷退' FROM RSALE
	)b --單據
	on a.bracket = b.bracket and a.sano = b.sano
	where a.acctmny != b.acctmny
)as z --更新資料
where 
     Receivd.Bracket = z.Bracket and  
     Receivd.sano = z.sano       and 
	 Receivd.reno = z.reno 
--PRINT 'Boolean_expression is true.'
EnD
--else 
--PRINT 'Boolean_expression is not true.'
";
                        SQL.ExecuteNonQuery(str_根據單據未收金額填回已收帳款之未收金額,null,null,cmd);
                        #endregion
                        #region SQL Update :應付
                        string str_根據單據未付金額填回已付帳款之未付金額 = @"
IF (select count(*) from  
(SELECT * FROM Payabld where Bracket!='期初') as a
left join 
(
SELECT AcctMny,Bsno,Bracket='進貨' FROM bShop	
union all
SELECT AcctMny,Bsno,Bracket='退貨' FROM RShop
)b
on a.bracket = b.bracket and a.Bsno = b.Bsno
where a.acctmny != b.acctmny)  > 0
BEGIN

update Payabld set Payabld.AcctMny = z.acctmny 
from  
(
	(select a.Bracket,a.Bsno,a.pano,b.acctmny from  
	(SELECT * FROM Payabld where Bracket!='期初') as a
	left join 
	(
	SELECT AcctMny,Bsno,Bracket='進貨' FROM bShop	
	union all
	SELECT AcctMny,Bsno,Bracket='退貨' FROM RShop
	)b
	on a.bracket = b.bracket and a.Bsno = b.Bsno
	where a.acctmny != b.acctmny) 
)as z --更新資料
where 
     Payabld.Bracket = z.Bracket and  
     Payabld.Bsno = z.Bsno       and 
	 Payabld.pano = z.pano 
--PRINT 'Boolean_expression is true.'
EnD
--else 
--PRINT 'Boolean_expression is not true.
";
                        SQL.ExecuteNonQuery(str_根據單據未付金額填回已付帳款之未付金額, null, null, cmd);
                        #endregion
                        AddColumnAndDValue(cmd, "quote", "ischeck", "NVARCHAR(1)", "0");
                        AddColumnAndDValue(cmd, "scrit", "ischeck", "nvarchar(1)", "2");

                        cmd.CommandText = "update systemset set vers ='14.2'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v143()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        AddColumnAndDValue(cmd, "quote", "ischeck", "NVARCHAR(1)", "0");
                        AddColumnAndDValue(cmd, "quote", "ischeck", "NVARCHAR(1)", "0");

                        AddColumnAndDValue(cmd, "nullify", "einvstate", "nvarchar(10)","");
                        AddColumnAndDValue(cmd, "nullify", "einvchange", "nvarchar(10)","");

                        #region  Einvsetup
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'Einvsetup';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = @"CREATE TABLE [dbo].[Einvsetup](
                        	[EinvTitle] [nvarchar](68) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
                        	[EinvStore] [nvarchar](68) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
                        	[EinvUnno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
                        	[EinvTaxNo] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
                        	[EinvTel] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
                        	[EinvAddress] [nvarchar](68) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
                        	[EinvMemo1] [nvarchar](68) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
                        	[EinvMemo2] [nvarchar](68) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
                        	[Einvid] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,
                        	[ScInvoic7] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
                        	[ScInvoic7e] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
                        	[ScInvoic8] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
                        	[ScInvoic8e] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
                         CONSTRAINT [PK_Einvsetup] PRIMARY KEY CLUSTERED 
                        (
                        	[Einvid] ASC
                        )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
                        ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }
                        #endregion

                        AddColumn(cmd, "Saled", "mnytaxin", "numeric(19, 6)");//B2C-turnkey-mnytaxin
                        AddColumn(cmd, "sinvd", "mnytaxin", "numeric(19, 6)");//B2C-turnkey-mnytaxin
                        AddColumn(cmd, "rsaled", "mnytaxin", "numeric(19, 6)");//B2C-turnkey-mnytaxin
                        AddColumn(cmd, "MachineSet", "POSLayout", "numeric(1, 0)");//POS版面挑選
                        AddColumnAndDValue(cmd, "MachineSet", "User_Einv", "NVARCHAR(10)", "");//發票獨立
                        AddColumnAndDValue(cmd, "scrit", "User_Einv", "NVARCHAR(10)", "");//發票獨立



                        cmd.CommandText = "update systemset set vers ='14.3'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v144()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.CommandText = "update systemset set vers ='14.4'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v145()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.CommandText = "update systemset set vers ='14.5'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v146()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        //4-27
                        AddColumnAndDValue(cmd, "sale", "User_Einv", "NVARCHAR(10)", "");//發票獨立
                        AddColumnAndDValue(cmd, "sale", "iTitle", "nvarchar(68)", "");
                        AddColumnAndDValue(cmd, "rsale", "User_Einv", "NVARCHAR(10)", "");//發票獨立
                        AddColumnAndDValue(cmd, "rsale", "iTitle", "nvarchar(68)", "");
                        AddColumnAndDValue(cmd, "Einvsetup", "ScInvoic7b", "NVARCHAR(10)", "");//發票獨立
                        AddColumnAndDValue(cmd, "Einvsetup", "ScInvoic8b", "NVARCHAR(10)", "");//發票獨立

                        //5-6 異動庫存單據修改-> 修改中是1,無修改是0
                        AddColumnAndDValue(cmd, "sale", "IsModify", "NVARCHAR(1)", "0");
                        AddColumnAndDValue(cmd, "rsale", "IsModify", "NVARCHAR(1)", "0");
                        AddColumnAndDValue(cmd, "bshop", "IsModify", "NVARCHAR(1)", "0");
                        AddColumnAndDValue(cmd, "RShop", "IsModify", "NVARCHAR(1)", "0");
                        AddColumnAndDValue(cmd, "draw", "IsModify", "NVARCHAR(1)", "0");
                        AddColumnAndDValue(cmd, "allot", "IsModify", "NVARCHAR(1)", "0");
                        AddColumnAndDValue(cmd, "adjust", "IsModify", "NVARCHAR(1)", "0");
                        AddColumnAndDValue(cmd, "garner", "IsModify", "NVARCHAR(1)", "0");
                        AddColumnAndDValue(cmd, "InStk", "IsModify", "NVARCHAR(1)", "0");
                        AddColumnAndDValue(cmd, "rlend", "IsModify", "NVARCHAR(1)", "0");
                        AddColumnAndDValue(cmd, "lend", "IsModify", "NVARCHAR(1)", "0");
                        AddColumnAndDValue(cmd, "Borr", "IsModify", "NVARCHAR(1)", "0");
                        AddColumnAndDValue(cmd, "RBorr", "IsModify", "NVARCHAR(1)", "0");
                        AddColumnAndDValue(cmd, "quote", "IsModify", "NVARCHAR(1)", "0");
                        AddColumnAndDValue(cmd, "ford", "IsModify", "NVARCHAR(1)", "0");
                        AddColumnAndDValue(cmd, "fquot", "IsModify", "NVARCHAR(1)", "0");
                        
                        cmd.CommandText = "select count(name) as 是否有欄位 from syscolumns where id=(select id from sysobjects where name=N'order') and name=N'IsModify'";
                        if (cmd.ExecuteScalar().ToString() == "0")
                        {
                            cmd.CommandText = "ALTER TABLE [order] ADD  IsModify NVARCHAR(1) COLLATE Chinese_Taiwan_Stroke_BIN";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "update [order] set IsModify ='0'";
                            cmd.ExecuteNonQuery();
                        }

                        AddColumnAndDValue(cmd, "Iv", "IsModify", "NVARCHAR(1)", "0");

                        AddColumnAndDValue(cmd, "cust", "spno", "NVARCHAR(4)", "");//客戶專案編號
                        AddColumnAndDValue(cmd, "cust", "spname", "NVARCHAR(20)", "");//客戶專案編號

                        cmd.CommandText = "update systemset set vers ='14.6'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v147()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.CommandText = "update systemset set vers ='14.7'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v148()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        #region  批號調整作業
                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'BatchProcess_adjust';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = @"CREATE TABLE [dbo].[BatchProcess_adjust](
	                        [adno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,
	                        [addate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	                        [addate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	                        [stno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	                        [stname] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	                        [emno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	                        [emname] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	                        [admemo] [nvarchar](68) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	                        [appscno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	                        [appdate] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	                        [edtscno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	                        [edtdate] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	                        [recordno] [numeric](10, 0) NULL,
	                        [IsModify] [nvarchar](1) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	                        [admemo1] [text] NULL,
                         CONSTRAINT [PK_BatchProcess_adjust] PRIMARY KEY CLUSTERED 
                        (
	                        [adno] ASC
                        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                        ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }

                        cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES where TABLE_NAME = N'BatchProcess_adjustd';";
                        if (cmd.ExecuteScalar().ToDecimal() == 0)
                        {
                            cmd.CommandText = @"CREATE TABLE [dbo].[BatchProcess_adjustd](
	                        [adno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	                        [addate] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	                        [addate1] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	                        [stno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	                        [itno] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	                        [itname] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	                        [stkqty] [numeric](20, 4) NULL,
	                        [qty] [numeric](20, 4) NULL,
	                        [realqty] [numeric](20, 4) NULL,
	                        [memo] [nvarchar](20) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	                        [bomid] [nvarchar](30) COLLATE Chinese_Taiwan_Stroke_BIN NOT NULL,
	                        [rec] [numeric](6, 0) NULL,
	                        [recordno] [numeric](10, 0) NULL,
	                        [Cuno] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
	                        [Bno] [int] NULL,
	                        [fano] [nvarchar](10) COLLATE Chinese_Taiwan_Stroke_BIN NULL,
                         CONSTRAINT [PK_BatchProcess_adjustd] PRIMARY KEY CLUSTERED 
                        (
	                        [bomid] ASC
                        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                        ) ON [PRIMARY]";
                            cmd.ExecuteNonQuery();
                        }
                        #endregion

                        cmd.CommandText = "update systemset set vers ='14.8'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v149()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.CommandText = "update systemset set vers ='14.9'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v150()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        AddColumnAndDValue(cmd, "MachineSet", "cardPort", "NVARCHAR(30)", "");//刷卡機
                        AddColumnAndDValue(cmd, "fquot", "IsModify", "NVARCHAR(1)", "0");

                        cmd.CommandText = @"update sale set IsModify ='0' 
                        update rsale set IsModify ='0' 
                        update bshop set IsModify ='0' 
                        update rshop set IsModify ='0'
                        update draw set IsModify ='0' 
                        update allot set IsModify ='0' 
                        update adjust set IsModify ='0' 
                        update garner set IsModify ='0'
                        update InStk set IsModify ='0' 
                        update rlend set IsModify ='0' 
                        update lend set IsModify ='0' 
                        update Borr set IsModify ='0'
                        update RBorr set IsModify ='0'
                        update quote set IsModify ='0' 
                        update ford set IsModify ='0' 
                        update fquot set IsModify ='0' 
                        update [order] set IsModify ='0'
                        update Iv set IsModify ='0'  
                        update BatchProcess_adjust set IsModify ='0' 
                        ";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "update systemset set vers ='15.0'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v151()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        AddColumnAndDValue(cmd, "systemset", "Einvusen", "NVARCHAR(3)", "0");//電子發票使用的家數

                        cmd.CommandText = @"update sale set IsModify ='0' 
                        update rsale set IsModify ='0' 
                        update bshop set IsModify ='0' 
                        update rshop set IsModify ='0'
                        update draw set IsModify ='0' 
                        update allot set IsModify ='0' 
                        update adjust set IsModify ='0' 
                        update garner set IsModify ='0'
                        update InStk set IsModify ='0' 
                        update rlend set IsModify ='0' 
                        update lend set IsModify ='0' 
                        update Borr set IsModify ='0'
                        update RBorr set IsModify ='0'
                        update quote set IsModify ='0' 
                        update ford set IsModify ='0' 
                        update fquot set IsModify ='0' 
                        update [order] set IsModify ='0'
                        update Iv set IsModify ='0'  
                        update BatchProcess_adjust set IsModify ='0' 
                        ";
                        cmd.ExecuteNonQuery();

                        AddColumnAndDValue(cmd, "order", "orpickingflag", "bit", "0");//電商撿貨註記
                        AddColumnAndDValue(cmd, "systemset", "weborder", "numeric(1, 0)", "1");//網路訂單開啟註記

                        cmd.CommandText = "update systemset set vers ='15.1'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v152()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.CommandText = @"update sale set IsModify ='0' 
                        update rsale set IsModify ='0' 
                        update bshop set IsModify ='0' 
                        update rshop set IsModify ='0'
                        update draw set IsModify ='0' 
                        update allot set IsModify ='0' 
                        update adjust set IsModify ='0' 
                        update garner set IsModify ='0'
                        update InStk set IsModify ='0' 
                        update rlend set IsModify ='0' 
                        update lend set IsModify ='0' 
                        update Borr set IsModify ='0'
                        update RBorr set IsModify ='0'
                        update quote set IsModify ='0' 
                        update ford set IsModify ='0' 
                        update fquot set IsModify ='0' 
                        update [order] set IsModify ='0'
                        update Iv set IsModify ='0'  
                        update BatchProcess_adjust set IsModify ='0' 
                        ";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "update systemset set vers ='15.2'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v153()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        AddColumnAndDValue(cmd, "scrit", "sc_MachineSet", "NVARCHAR(10)", "");//機台號碼

                        cmd.CommandText = "update systemset set vers ='15.3'";
                        cmd.ExecuteNonQuery();



                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v154()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        RemovePKColumn(cmd, "Einvsetup", "Einvsetup");//0726Einvsetup欄位
                        ChangeColumnType(cmd, "Einvsetup", "Einvid", "NVARCHAR(10)");
                        AddPKColumn(cmd, "Einvsetup", "Einvid", "NVARCHAR(10)");

                        cmd.CommandText = "update systemset set vers ='15.4'";
                        cmd.ExecuteNonQuery();



                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        #endregion

        public bool v155()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.CommandText = "update systemset set vers ='15.5'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v156()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;
                        ChangeColumnType(cmd, "sale", "samemo", "NVARCHAR(70)");

                        AddColumnAndDValue(cmd, "cust", "detailmemo", "text","");
                        AddColumnAndDValue(cmd, "fact", "detailmemo", "text", "");
                        cmd.CommandText = "update systemset set vers ='15.6'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v157()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;
                        cmd.CommandText = "update systemset set vers ='15.7'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v158()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;
                        cmd.CommandText = "update systemset set vers ='15.8'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }
        public bool v159()
        {
            using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
            {
                try
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        IsTransactionOK = true;
                        tn = cn.BeginTransaction();
                        cmd.Transaction = tn;

                        cmd.CommandText = @"update sale set payerno=cuno where  (payerno is null or payerno='');
                                            update rsale set payerno=cuno where  (payerno is null or payerno='')";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "update systemset set vers ='15.9'";
                        cmd.ExecuteNonQuery();

                        if (IsTransactionOK)
                        {
                            tn.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    if (tn.IsNotNull()) tn.Rollback();
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    if (tn.IsNotNull()) tn.Dispose();
                }
            }
        }



        void  InitializeDatabase()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Common.sqlConnString))
                using (SqlCommand cmd = cn.CreateCommand())
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    cn.Open();
                    cmd.CommandText = " SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES"
                                            + " WHERE TABLE_NAME = N'MachineSet';";
                    if (cmd.ExecuteScalar().ToDecimal() == 0)
                    {
                        cmd.CommandText = "CREATE TABLE MachineSet("
                        + "[TicketPort]    [NVARCHAR](20)   COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                        + "[TicketPortBox] [NVARCHAR](2)    COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                        + "[InvPort]       [NVARCHAR](20)   COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                        + "[InvPortBox]    [NVARCHAR](2)    COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                        + "[MoneyPort]     [NVARCHAR](20)   COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                        + "[VideoPort]     [NVARCHAR](20)   COLLATE Chinese_Taiwan_Stroke_BIN NULL,"
                        + "[maid] [int] IDENTITY(1,1) NOT NULL,"
                        + "PRIMARY KEY CLUSTERED "
                        + "("
                        + "[maid] ASC"
                        + " )WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]"
                        + " ) ON [PRIMARY]";
                        cmd.ExecuteNonQuery();
                    }

                    this.AddColumn(cmd, "MachineSet", "machine", "NVARCHAR(10)");

                    cmd.CommandText = "Select Count(*) from MachineSet";
                    var obj = cmd.ExecuteScalar().ToDecimal();
                    if (obj == 0)
                    {
                        cmd.Parameters.AddWithValue("machine", 1);

                        for (int i = 0; i < 5; i++)
                        {
                            cmd.Parameters["machine"].Value = i + 1;
                            cmd.CommandText = "Insert into MachineSet (machine) Values (@machine)";
                            cmd.ExecuteNonQuery();
                        }
                    }

                    this.AddColumn(cmd, "MachineSet", "X5No", "NVARCHAR(1)");
                    this.AddColumn(cmd, "MachineSet", "InvNoS", "NVARCHAR(10)");
                    cmd.CommandText = "Update MachineSet SET X5No = 3 Where X5No IS Null;";
                    cmd.ExecuteNonQuery();

                    this.AddColumn(cmd, "MachineSet", "InvT1", "NVARCHAR(40)");
                    this.AddColumn(cmd, "MachineSet", "InvT2", "NVARCHAR(40)");
                    this.AddColumn(cmd, "MachineSet", "InvT3", "NVARCHAR(40)");
                    this.AddColumn(cmd, "MachineSet", "InvT4", "NVARCHAR(40)");
                    this.AddColumn(cmd, "MachineSet", "InvT5", "NVARCHAR(40)");

                    this.ChangeColumnType(cmd, "MachineSet", "TicketPort", "NVARCHAR(30)");
                    this.ChangeColumnType(cmd, "MachineSet", "InvPort", "NVARCHAR(30)");
                    this.ChangeColumnType(cmd, "MachineSet", "MoneyPort", "NVARCHAR(30)");
                    this.ChangeColumnType(cmd, "MachineSet", "VideoPort", "NVARCHAR(30)");

                    this.AddColumn(cmd, "MachineSet", "OffLinePort", "NVARCHAR(30)");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        void ChangeColumnName(SqlCommand cmd, string TTable, string OldName, string NewName)
        {
            try
            {
                cmd.CommandText = "select count(name) as 是否有欄位 from syscolumns where id=(select id from sysobjects where name=N'" + TTable + "') and name=N'" + OldName + "'";
                if (cmd.ExecuteScalar().ToString() == "1")
                {
                    cmd.CommandText = "EXEC sp_rename \'" + TTable + "." + OldName + "\', \'" + NewName + "\', \'COLUMN\';";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                IsTransactionOK = false;
                MessageBox.Show(ex.ToString());
            }
        }

        void ChangeColumnType(SqlCommand cmd, string TTable, string TColumn, string TType)
        {
            try
            {
                cmd.CommandText = "select count(name) as 是否有欄位 from syscolumns where id=(select id from sysobjects where name=N'" + TTable.Replace('[', ' ').Replace(']', ' ').Trim() + "') and name=N'" + TColumn + "'";
                if (cmd.ExecuteScalar().ToString() == "1")
                {
                    cmd.CommandText = "ALTER TABLE " + TTable + " ALTER COLUMN " + TColumn + " " + TType;
                    if (TType.ToUpper().Contains("NVARCHAR"))
                        cmd.CommandText += " COLLATE Chinese_Taiwan_Stroke_BIN";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                IsTransactionOK = false;
                MessageBox.Show(ex.ToString());
            }
        }

        void AddColumn(SqlCommand cmd, string TTable, string TColumn, string TType)
        {
            try
            {
                cmd.CommandText = "IF (Select count(*) from syscolumns a left join sysobjects b on (a.[id]=b.[id]) where b.[name] = '" + TTable + "' and  a.[name]='" + TColumn + "') = 0 "
                           + " BEGIN "
                           + " ALTER TABLE [" + TTable + "] ADD " + TColumn + " " + TType;

                if (TType.ToUpper().Contains("NVARCHAR")) cmd.CommandText += " COLLATE Chinese_Taiwan_Stroke_BIN";

                cmd.CommandText += " NULL END ";
                cmd.ExecuteNonQuery();
                if (TType.ToUpper().Contains("NVARCHAR"))
                {
                    cmd.CommandText = "ALTER TABLE [" + TTable + "] ALTER COLUMN " + TColumn + " " + TType + " COLLATE Chinese_Taiwan_Stroke_BIN";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                IsTransactionOK = false;
                MessageBox.Show(ex.ToString());
            }
        }

        void AddColumnAndDValue(SqlCommand cmd, string TTable, string TColumn, string TType, string TValue)
        {
            try
            {
                cmd.CommandText = "select count(name) as 是否有欄位 from syscolumns where id=(select id from sysobjects where name=N'" + TTable + "') and name=N'" + TColumn + "'";
                if (cmd.ExecuteScalar().ToString() == "0")
                {
                    cmd.CommandText = "ALTER TABLE [" + TTable + "] ADD " + TColumn + " " + TType;
                    if (TType.ToUpper().Contains("NVARCHAR"))
                        cmd.CommandText += " COLLATE Chinese_Taiwan_Stroke_BIN";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "update [" + TTable + "] set " + TColumn + " ='" + TValue + "'";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                IsTransactionOK = false;
                MessageBox.Show(ex.ToString());
            }
        }

        void RemovePKColumn(SqlCommand cmd, string TTable)
        {
            try
            {
                bool boolean = false;
                cmd.CommandText = "select COLUMN_NAME from INFORMATION_SCHEMA.KEY_COLUMN_USAGE where SUBSTRING(CONSTRAINT_NAME,1,2) = 'PK' and TABLE_NAME = N'" + TTable + "'";
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) boolean = true;
                }
                if (boolean)
                {
                    cmd.CommandText = "ALTER TABLE " + TTable + " DROP CONSTRAINT pk_" + TTable;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                IsTransactionOK = false;
                MessageBox.Show(ex.ToString());
            }
        }

        void AddPKColumn(SqlCommand cmd, string TTable, string TColumn, string TType)
        {
            bool flag = false;
            try
            {
                cmd.CommandText = "select count(name) as 是否有欄位 from syscolumns where id=(select id from sysobjects where name=N'" + TTable + "') and name=N'" + TColumn + "'";
                if (cmd.ExecuteScalar().ToString() == "1")
                {
                    cmd.CommandText = "ALTER TABLE " + TTable + " ALTER COLUMN " + TColumn + " " + TType;
                    if (TType.ToUpper().Contains("NVARCHAR"))
                        cmd.CommandText += " COLLATE Chinese_Taiwan_Stroke_BIN  NOT NULL";
                    else
                        cmd.CommandText += " NOT NULL";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "select COLUMN_NAME from INFORMATION_SCHEMA.KEY_COLUMN_USAGE where SUBSTRING(CONSTRAINT_NAME,1,2) = 'PK' and TABLE_NAME = N'" + TTable + "'";
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        flag = reader.HasRows;
                    }
                    if (!flag)
                    {
                        //cmd.CommandText = "ALTER TABLE " + TTable + " ADD PRIMARY KEY (" + TColumn + ")";
                        cmd.CommandText = "ALTER TABLE " + TTable + " ADD "
                        + "CONSTRAINT PK_" + TTable + " PRIMARY KEY CLUSTERED "
                        + " ("
                        + TColumn
                        + " ) ON [PRIMARY] ";
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                IsTransactionOK = false;
                MessageBox.Show(ex.ToString());
            }
        }

        void ChangePKColumn(SqlCommand cmd, string TTable, string TColumn, string TType)
        {
            try
            {
                string temp = "";
                cmd.CommandText = "SELECT CONSTRAINT_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME ='" + TTable + "'";
                temp = cmd.ExecuteScalar().ToString();
                cmd.CommandText = "  ALTER TABLE " + TTable + " DROP CONSTRAINT " + temp + ";";

                if (TType.ToUpper().Contains("NVARCHAR")) TType += " COLLATE Chinese_Taiwan_Stroke_BIN";

                cmd.CommandText += " ALTER TABLE " + TTable + " ALTER COLUMN " + TColumn + " " + TType + " NOT NULL;";
                cmd.CommandText += " ALTER TABLE " + TTable + " ADD PRIMARY KEY (" + TColumn + ");";
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                IsTransactionOK = false;
                MessageBox.Show(ex.ToString());
            }
        }

        void RemovePKColumn(SqlCommand cmd, string TTable, string name)
        {
            try
            {
                bool boolean = false;
                cmd.CommandText = "select COLUMN_NAME from INFORMATION_SCHEMA.KEY_COLUMN_USAGE where SUBSTRING(CONSTRAINT_NAME,1,2) = 'PK' and TABLE_NAME = N'" + TTable + "'";
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows) boolean = true;
                }
                if (boolean)
                {
                    cmd.CommandText = "ALTER TABLE " + TTable + " DROP CONSTRAINT pk_" + name;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                IsTransactionOK = false;
                MessageBox.Show(ex.ToString());
            }
        }

        void CerateNonCLUSTERED(SqlCommand cmd, string TTable, string TColumn,string Orderby = "Asc") 
        {

            cmd.CommandText = @"
   IF NOT EXISTS (select * from(select object_name(object_id) tableName,name,type_desc from sys.indexes)A where A.tableName='" + TTable + "'  And name='" +TColumn+ "') "
 + " CREATE NONCLUSTERED INDEX [" + TColumn + "] ON   [dbo].[" + TTable + "]  ([" + TColumn + "]   " + Orderby + ") "
 + " WITH (STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]" 
;
            cmd.ExecuteNonQuery();

        }
    }

    public static class JEInitialize
    {
        public static bool IsRunTime = false;
        public static Font ControlFontSize = new Font("細明體", 12F);
        public static Font LabelMenyFontSize = new Font("細明體", 16F, FontStyle.Bold);
        public static int CharWidth = 8;

        public static void SetControlFontSize()
        {
            var width = Screen.PrimaryScreen.Bounds.Width;
            var ratio = Screen.PrimaryScreen.Bounds.Width.ToDecimal() / Screen.PrimaryScreen.Bounds.Height.ToDecimal();
            if (width <= 800)
            {
                CharWidth = 6;
                ControlFontSize = new Font("細明體", 9F);
                LabelMenyFontSize = new Font("細明體", 12F, FontStyle.Bold);
            }
            else if (width <= 1024)
            {
                CharWidth = 8;
                ControlFontSize = new Font("細明體", 12F);
                LabelMenyFontSize = new Font("細明體", 16F, FontStyle.Bold);
            }
            else if (width <= 1152)
            {
                CharWidth = 9;
                ControlFontSize = new Font("細明體", 13F);
                LabelMenyFontSize = new Font("細明體", 17F, FontStyle.Bold);
            }
            else if (width <= 1280)
            {
                CharWidth = 10;
                ControlFontSize = new Font("細明體", 14F);
                LabelMenyFontSize = new Font("細明體", 19F, FontStyle.Bold);
            }
            else if (width <= 1360)
            {
                CharWidth = 11;
                ControlFontSize = new Font("細明體", 16F);
                LabelMenyFontSize = new Font("細明體", 21F, FontStyle.Bold);
            }
            else //if (width <= 1440)
            {
                CharWidth = 11;
                ControlFontSize = new Font("細明體", 16F);
                LabelMenyFontSize = new Font("細明體", 21F, FontStyle.Bold);
            }
            //else if (width <= 1480)
            //{
            //    CharWidth = 12;
            //    ControlFontSize = new Font("細明體", 18F);
            //    LabelMenyFontSize = new Font("細明體", 23F, FontStyle.Bold);
            //}
            //else if (width <= 1600)
            //{
            //    CharWidth = 13;
            //    ControlFontSize = new Font("細明體", 19F);
            //    LabelMenyFontSize = new Font("細明體", 25F, FontStyle.Bold);
            //}
            //else if (width <= 1720)
            //{
            //    CharWidth = 14;
            //    ControlFontSize = new Font("細明體", 20F);
            //    LabelMenyFontSize = new Font("細明體", 26F, FontStyle.Bold);
            //}
            //else if (width <= 1840)
            //{
            //    CharWidth = 15;
            //    ControlFontSize = new Font("細明體", 22F);
            //    LabelMenyFontSize = new Font("細明體", 29F, FontStyle.Bold);
            //}
            //else
            //{
            //    CharWidth = 16;
            //    ControlFontSize = new Font("細明體", 23F);
            //    LabelMenyFontSize = new Font("細明體", 30F, FontStyle.Bold);
            //}
        }

        public static void UpdateDataBase(decimal currentVer, decimal dbVer)
        {
            //先取得資料庫目前版本,更新範圍為: 資料庫版本 <--> 程式版本
            bool flag = true;
            if (dbVer == 0) return;
            if (currentVer > dbVer)
            {
                if (dbVer == 9) flag = new AlterDB().v9();
                else if (dbVer == 10) flag = new AlterDB().v10();
                else if (dbVer == 11) flag = new AlterDB().v11();
                else if (dbVer == 11.1M) flag = new AlterDB().v111();
                else if (dbVer == 11.2M) flag = new AlterDB().v112();
                else if (dbVer == 11.3M) flag = new AlterDB().v113();
                else if (dbVer == 11.4M) flag = new AlterDB().v114();
                else if (dbVer == 11.5M) flag = new AlterDB().v115();
                else if (dbVer == 11.6M) flag = new AlterDB().v116();
                else if (dbVer == 11.7M) flag = new AlterDB().v117();
                else if (dbVer == 11.8M) flag = new AlterDB().v118();
                else if (dbVer == 11.9M) flag = new AlterDB().v119();
                else if (dbVer == 12.0M) flag = new AlterDB().v120();
                else if (dbVer == 12.1M) flag = new AlterDB().v121();
                else if (dbVer == 12.2M) flag = new AlterDB().v122();
                else if (dbVer == 12.3M) flag = new AlterDB().v123();
                else if (dbVer == 12.4M) flag = new AlterDB().v124();
                else if (dbVer == 12.5M) flag = new AlterDB().v125();
                else if (dbVer == 12.6M) flag = new AlterDB().v126();
                else if (dbVer == 12.7M) flag = new AlterDB().v127();
                else if (dbVer == 12.8M) flag = new AlterDB().v128();
                else if (dbVer == 12.9M) flag = new AlterDB().v129();
                else if (dbVer == 13.0M) flag = new AlterDB().v130();
                else if (dbVer == 13.1M) flag = new AlterDB().v131();
                else if (dbVer == 13.2M) flag = new AlterDB().v132();
                else if (dbVer == 13.3M) flag = new AlterDB().v133();
                else if (dbVer == 13.4M) flag = new AlterDB().v134();
                else if (dbVer == 13.5M) flag = new AlterDB().v135();
                else if (dbVer == 13.6M) flag = new AlterDB().v136();
                else if (dbVer == 13.7M) flag = new AlterDB().v137();
                else if (dbVer == 13.8M) flag = new AlterDB().v138();
                else if (dbVer == 13.9M) flag = new AlterDB().v139();
                else if (dbVer == 14.0M) flag = new AlterDB().v140();
                else if (dbVer == 14.1M) flag = new AlterDB().v141();
                else if (dbVer == 14.2M) flag = new AlterDB().v142();
                else if (dbVer == 14.3M) flag = new AlterDB().v143();
                else if (dbVer == 14.4M) flag = new AlterDB().v144();
                else if (dbVer == 14.5M) flag = new AlterDB().v145();
                else if (dbVer == 14.6M) flag = new AlterDB().v146();
                else if (dbVer == 14.7M) flag = new AlterDB().v147();
                else if (dbVer == 14.8M) flag = new AlterDB().v148();
                else if (dbVer == 14.9M) flag = new AlterDB().v149();
                else if (dbVer == 15.0M) flag = new AlterDB().v150();
                else if (dbVer == 15.1M) flag = new AlterDB().v151();
                else if (dbVer == 15.2M) flag = new AlterDB().v152();
                else if (dbVer == 15.3M) flag = new AlterDB().v153();
                else if (dbVer == 15.4M) flag = new AlterDB().v154();
                else if (dbVer == 15.5M) flag = new AlterDB().v155();
                else if (dbVer == 15.6M) flag = new AlterDB().v156();
                else if (dbVer == 15.7M) flag = new AlterDB().v157();
                else if (dbVer == 15.8M) flag = new AlterDB().v158();
                else if (dbVer == 15.9M) flag = new AlterDB().v159();
                if (flag == false) MessageBox.Show("資料庫更新失敗！");
                else
                {
                    if (dbVer == 9) dbVer = 10M;
                    else if (dbVer == 10) dbVer = 11M;
                    else if (dbVer >= 11) dbVer = Common.GetDBVers() + 0.1M;
                    UpdateDataBase(currentVer, dbVer);
                }
            }
            else if (currentVer == dbVer)
            {
                if (dbVer == 9) flag = new AlterDB().v9();
                else if (dbVer == 10) flag = new AlterDB().v10();
                else if (dbVer == 11) flag = new AlterDB().v11();
                else if (dbVer == 11.1M) flag = new AlterDB().v111();
                else if (dbVer == 11.2M) flag = new AlterDB().v112();
                else if (dbVer == 11.3M) flag = new AlterDB().v113();
                else if (dbVer == 11.4M) flag = new AlterDB().v114();
                else if (dbVer == 11.5M) flag = new AlterDB().v115();
                else if (dbVer == 11.6M) flag = new AlterDB().v116();
                else if (dbVer == 11.7M) flag = new AlterDB().v117();
                else if (dbVer == 11.8M) flag = new AlterDB().v118();
                else if (dbVer == 11.9M) flag = new AlterDB().v119();
                else if (dbVer == 12.0M) flag = new AlterDB().v120();
                else if (dbVer == 12.1M) flag = new AlterDB().v121();
                else if (dbVer == 12.2M) flag = new AlterDB().v122();
                else if (dbVer == 12.3M) flag = new AlterDB().v123();
                else if (dbVer == 12.4M) flag = new AlterDB().v124();
                else if (dbVer == 12.5M) flag = new AlterDB().v125();
                else if (dbVer == 12.6M) flag = new AlterDB().v126();
                else if (dbVer == 12.7M) flag = new AlterDB().v127();
                else if (dbVer == 12.8M) flag = new AlterDB().v128();
                else if (dbVer == 12.9M) flag = new AlterDB().v129();
                else if (dbVer == 13.0M) flag = new AlterDB().v130();
                else if (dbVer == 13.1M) flag = new AlterDB().v131();
                else if (dbVer == 13.2M) flag = new AlterDB().v132();
                else if (dbVer == 13.3M) flag = new AlterDB().v133();
                else if (dbVer == 13.4M) flag = new AlterDB().v134();
                else if (dbVer == 13.5M) flag = new AlterDB().v135();
                else if (dbVer == 13.6M) flag = new AlterDB().v136();
                else if (dbVer == 13.7M) flag = new AlterDB().v137();
                else if (dbVer == 13.8M) flag = new AlterDB().v138();
                else if (dbVer == 13.9M) flag = new AlterDB().v139();
                else if (dbVer == 14.0M) flag = new AlterDB().v140();
                else if (dbVer == 14.1M) flag = new AlterDB().v141();
                else if (dbVer == 14.2M) flag = new AlterDB().v142();
                else if (dbVer == 14.3M) flag = new AlterDB().v143();
                else if (dbVer == 14.4M) flag = new AlterDB().v144();
                else if (dbVer == 14.5M) flag = new AlterDB().v145();
                else if (dbVer == 14.6M) flag = new AlterDB().v146();
                else if (dbVer == 14.7M) flag = new AlterDB().v147();
                else if (dbVer == 14.8M) flag = new AlterDB().v148();
                else if (dbVer == 14.9M) flag = new AlterDB().v149();
                else if (dbVer == 15.0M) flag = new AlterDB().v150();
                else if (dbVer == 15.1M) flag = new AlterDB().v151();
                else if (dbVer == 15.2M) flag = new AlterDB().v152();
                else if (dbVer == 15.3M) flag = new AlterDB().v153();
                else if (dbVer == 15.4M) flag = new AlterDB().v154();
                else if (dbVer == 15.5M) flag = new AlterDB().v155();
                else if (dbVer == 15.6M) flag = new AlterDB().v156();
                else if (dbVer == 15.7M) flag = new AlterDB().v157();
                else if (dbVer == 15.8M) flag = new AlterDB().v158();
                else if (dbVer == 15.9M) flag = new AlterDB().v159();
            }
        }
    }
}
