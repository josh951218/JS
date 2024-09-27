using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Windows.Forms;

namespace JBS
{
    public class SendMail
    {
        string ConnectionString = "";
        string 來源資料夾 = @"\SDMail";
        string 目的資料夾 = @"\BKMail";
        string 寄件人帳號 = "";
        string 寄件人密碼 = "";
        string 收件人帳號 = "";
        string Title = "";
        bool flag = true;

        public SendMail(string connectionString, string title)
        {
            this.ConnectionString = connectionString;
            this.Title = title;

            using (SqlConnection cn = new SqlConnection(this.ConnectionString))
            using (SqlCommand cmd = cn.CreateCommand())
            {
                try
                {
                    cmd.CommandText = "Select * from SendMail";

                    cn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            this.寄件人帳號 = reader["sendid"].ToString();
                            this.寄件人密碼 = reader["sendpw"].ToString();
                            this.收件人帳號 = string.Join(",", reader["geter"].ToString().Split(Environment.NewLine.ToCharArray()));
                        }
                        flag = this.收件人帳號.Trim().Length > 0;
                    }
                }
                catch (Exception)
                {
                    flag = false;
                }
            }
        }

        public void Send()
        {
            //System.Threading.Tasks.Task.Factory.StartNew(() =>
            //{
                System.Threading.Thread.Sleep(1000);
                this.SendAndRemove();
                System.Threading.Thread.Sleep(1000);

                //for (int i = 0; i < 3; i++)
                //{
                    //this.SendAndRemove();
                    //System.Threading.Thread.Sleep(5000);
                //}
            //});
        }

        private void SendAndRemove()
        {
            try
            {
                if (flag == false)
                    return;

                var files = new DirectoryInfo(Application.StartupPath + this.來源資料夾).GetFiles();
                if (files.Length == 0)
                    return;

                using (MailMessage mail = new MailMessage())
                {
                    foreach (FileInfo file in files)
                    {
                        mail.SubjectEncoding = Encoding.Default;
                        mail.Subject = Title + " " + file.Name;
                        mail.Body = " ";
                        mail.IsBodyHtml = true;
                        mail.From = new MailAddress(寄件人帳號, " ", Encoding.Default);
                        mail.Sender = new MailAddress(寄件人帳號, " ", Encoding.Default);
                        mail.To.Add(收件人帳號);

                        mail.Attachments.Add(加入附件(file.FullName));

                        var smtp = "";
                        if (寄件人帳號.ToLower().Contains("gmail.com"))
                            smtp = "smtp.gmail.com";
                        if (寄件人帳號.ToLower().Contains("hotmail.com"))
                            smtp = "smtp.live.com";
                        if (寄件人帳號.ToLower().Contains("outlook.com"))
                            smtp = "smtp.live.com";
                        if (寄件人帳號.ToLower().Contains("yahoo.com"))
                            smtp = "smtp.mail.yahoo.com";

                        SmtpClient MySmtp = new SmtpClient(smtp, 587);
                        MySmtp.Credentials = new NetworkCredential(寄件人帳號, 寄件人密碼);
                        MySmtp.EnableSsl = true;
                        MySmtp.Send(mail);
                    }
                }

                this.RemoveFilse(files);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void RemoveFilse(FileInfo[] files)
        {
            var path = Application.StartupPath + 目的資料夾;
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            foreach (FileInfo file in files)
            {
                if (!File.Exists(path + @"\" + file.Name))
                    File.Move(file.FullName, path + @"\" + file.Name);
                else
                {
                    File.Delete(path + @"\" + file.Name);
                    File.Move(file.FullName, path + @"\" + file.Name);
                }
            }
        }

        private Attachment 加入附件(string filePath)
        {
            // Create  the file attachment for this e-mail message.
            Attachment data = new Attachment(filePath, MediaTypeNames.Application.Zip);

            data.NameEncoding = Encoding.Default;
            // Add time stamp information for the file.
            ContentDisposition disposition = data.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(filePath);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(filePath);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(filePath);
            // Add the file attachment to this e-mail message.
            return data;
        }
    }
}
