using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JE.MyControl;
using S_61.Basic;


namespace JE.SOther
{
    public partial class FrmDesp : Formbase
    {
        public string TResult { get; private set; }
        public string TSeekNo { private get; set; }

        List<TextBoxbase> list;
        public DataRow dr;

        public FrmDesp(bool AllowAppend, FormStyle style = FormStyle.Max)
        {
            InitializeComponent();
            this.Style = style;
            list = panelT1.Controls.OfType<TextBoxbase>().ToList();
            if (AllowAppend)
                list.ForEach(t => t.ReadOnly = false);
            else
                btnSave.Visible = false;
        }

        private void FrmDesp_Load(object sender, EventArgs e)
        {
            writeToText();
            SetUDF();
        }

        void writeToText()
        {
            if (dr == null)
            {
                this.TSeekNo = "";
                list.ForEach(t => t.Clear());
            }
            else
            {
                ItDesp1.Text = dr["ItDesp1"].ToString();
                ItDesp2.Text = dr["ItDesp2"].ToString();
                ItDesp3.Text = dr["ItDesp3"].ToString();
                ItDesp4.Text = dr["ItDesp4"].ToString();
                ItDesp5.Text = dr["ItDesp5"].ToString();
                ItDesp6.Text = dr["ItDesp6"].ToString();
                ItDesp7.Text = dr["ItDesp7"].ToString();
                ItDesp8.Text = dr["ItDesp8"].ToString();
                ItDesp9.Text = dr["ItDesp9"].ToString();
                ItDesp10.Text = dr["ItDesp10"].ToString();
            }
        }

        void SetUDF()
        {
            //載入系統資訊
            //自定欄位,資料庫有值才改,沒值則預設
            var li = panelT1.Controls.OfType<LabelT>().ToList();
            for (int i = 1; i <= 10; i++)
            {
                var label = li.Find(r => r.Name == "labelT" + i.ToString());
                var text = Common.dtSysSettings.Rows[0]["ItDesp" + i.ToString()].ToString();
                if (text.Length > 0) label.Text = text;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            list.ForEach(r => dr[r.Name] = r.Text);
            this.DialogResult = DialogResult.OK;
        }
    }
}
