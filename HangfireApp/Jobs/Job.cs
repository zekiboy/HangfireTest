using System;
using System.Net;
using System.Net.Mail;

namespace HangfireApp.Jobs
{
	public class Job
	{


		public Job()
		{
		}

        public void DbControl()
        {
            //PROJEYİ ÇALIŞTIRDIĞIMDA MAİLİ GÖNDERDİ, CONTROLLER İÇİNDE ÇAĞIRMAMA GEREK KALMADI
            SendEmail();
        }

        // public void SendEmail(string email, string subject, string body)
        public void SendEmail()
        {
            SmtpClient smtp = new SmtpClient();

            smtp.Host = "smtp.office365.com";
            smtp.Port = 587;
            //GMail SMTP sunucusuna erişeceğimiz Port numarasını belirtiyoruz.
            smtp.EnableSsl = true;
            //GMail SMTP sunucusuna bağlanacağımız protokolü belirliyoruz.True da https, False da http protokolü kullanılır.


            NetworkCredential kullanicibilgi = new NetworkCredential("zekiiboy@hotmail.com", "marleylolo34");
            //NetworkCredential tipinden bir nesne oluşturup, kullanıcı bilgilerimizi giriyoruz.
            smtp.Credentials = kullanicibilgi;
            //Bu kullanıcı bilgilerini Credentials ile ilgili SMTP bağlantısına atıyoruz.


            MailAddress gonderen = new MailAddress("zekiiboy@hotmail.com", "Zeki Mail Test");
            //Maili atacak kişinin adresi
            MailAddress alici = new MailAddress("zekiiboy@gmail.com");
            //Maili alacak kişinin adresini yazdık.
            MailMessage mail = new MailMessage(gonderen, alici);
            //MailMessage nesnemizi oluşturduk.MailAddress tipinden istediği gonderen ve alici nesnelerini, bu constructorından bağladık.
            mail.Subject = "Mesajın Konusu";
            mail.Body = "Mesajın İçeriği";
            mail.IsBodyHtml = true;
            //Mail'de html kod kullanılsın mı?True evet,False hayır.
            smtp.Send(mail);
            //Son olarak SmtpClient tipindeki smtp nesnemiz sayesinde, MailMessage tipindeki mail nesnemiz ilgili adrese, gonderen adıyla iletiliyor.
        }

    }
}

