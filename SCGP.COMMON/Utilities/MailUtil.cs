using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SCGP.COA.COMMON.Utilities
{

    /// <summary>
    /// This class represent all Mail utilities
    /// </summary>
    public class MailUtil
    {
        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        public static bool SendMail(string smtp_host, int smtp_port, string from, string to, string subject, string body, List<Attachment> attach, out string error)
        {
            return SendMail(smtp_host, smtp_port, false, null, null, from, to, subject, body, false, attach, out error);
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isBodyHtml">if set to <c>true</c> [is body HTML].</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        public static bool SendMail(string smtp_host, int smtp_port, string from, string to, string subject, string body, bool isBodyHtml, List<Attachment> attach, out string error)
        {
            return SendMail(smtp_host, smtp_port, false, null, null, from, to, subject, body, isBodyHtml, attach, out error);
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isBodyHtml">if set to <c>true</c> [is body HTML].</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        public static bool SendMail(string smtp_host, int smtp_port, bool isAuthenByUserPassword, string User, string Password, string from, string to, string subject, string body, bool isBodyHtml, List<Attachment> AttachList, out string error)
        {
            bool isSuccess = false;

            try
            {
                error = "";

                //(1) Create the MailMessage instance 
                MailMessage mm = new MailMessage();

                //(2) Assign the MailMessage's properties 
                mm.From = new MailAddress(from);
                string[] strToMultiple = to.Split(';');

                if (strToMultiple.Length > 0)
                {
                    for (int i = 0; i < strToMultiple.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(strToMultiple[i]))
                        {
                            mm.To.Add(new MailAddress(strToMultiple[i]));
                        }
                    }
                }

                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = isBodyHtml;

                //(3) Create the SmtpClient object 
                SmtpClient smtp = new SmtpClient(smtp_host, smtp_port);
                smtp.UseDefaultCredentials = false;
                if (isAuthenByUserPassword)
                {
                    smtp.Credentials = new NetworkCredential(User, Password);
                }
                //smtp.Timeout = 100000;

                //(4) Attachment file if have
                #region Set Mail Attachment
                if (AttachList != null)
                {
                    foreach (Attachment attachItem in AttachList)
                    {
                        mm.Attachments.Add(attachItem);
                    }
                }
                #endregion Set Mail Attachment

                //(5) Send the MailMessage (will use the Web.config settings) 
                smtp.Send(mm);

                isSuccess = true;

            }
            catch (SmtpException smtpEx)
            {
                error = smtpEx.ToString();

                //Log in Error Level
                //NLogger.Instance.Error(smtpEx);
            }
            catch (Exception generalEx)
            {
                error = generalEx.ToString();

                //Log in Error Level
                //NLogger.Instance.Error(generalEx);
            }

            return isSuccess;
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isBodyHtml">if set to <c>true</c> [is body HTML].</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        public static bool SendMail(string smtp_host, int smtp_port, string from, string to, string cc, string subject, string body, bool isBodyHtml,bool isEnableSsl, List<Attachment> attach, out string error)
        {
            return SendMail(smtp_host, smtp_port, false, null, null, from, to, cc, subject, body, isBodyHtml,isEnableSsl, attach, out error);
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="isBodyHtml">if set to <c>true</c> [is body HTML].</param>
        /// <param name="error">The error.</param>
        /// <returns></returns>
        public static bool SendMail(string smtp_host, int smtp_port, bool isAuthenByUserPassword, string User, string Password, string from, string to, string cc, string subject, string body, bool isBodyHtml,bool isEnableSsl, List<Attachment> AttachList, out string error)
        {
            bool isSuccess = false;

            try
            {
                error = "";

                //(1) Create the MailMessage instance 
                MailMessage mm = new MailMessage();

                //(2) Assign the MailMessage's properties 
                mm.From = new MailAddress(from);
                string[] strToMultiple = to.Split(';');
                string[] strCcMultiple = cc.Split(';');

                if (strToMultiple.Length > 0)
                {
                    for (int i = 0; i < strToMultiple.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(strToMultiple[i]))
                        {
                            mm.To.Add(new MailAddress(strToMultiple[i]));
                        }
                    }
                }
                if (strCcMultiple.Length > 0)
                {
                    for (int i = 0; i < strCcMultiple.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(strCcMultiple[i]))
                        {
                            mm.CC.Add(new MailAddress(strCcMultiple[i]));
                        }
                    }
                }

                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = isBodyHtml;
                //mm.HeadersEncoding = Encoding.UTF8;
                //mm.SubjectEncoding = Encoding.UTF8;
                mm.BodyEncoding = Encoding.UTF8;

                //(3) Create the SmtpClient object 
                SmtpClient smtp = new SmtpClient(smtp_host, smtp_port);
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = isEnableSsl;
                if (isAuthenByUserPassword)
                {
                    smtp.Credentials = new NetworkCredential(User, Password);
                }
                //smtp.Timeout = 100000;
                //smtp.DeliveryFormat = SmtpDeliveryFormat.International;

                //(4) Attachment file if have
                #region Set Mail Attachment
                if (AttachList != null && AttachList.Count > 0)
                {
                    foreach (Attachment attachItem in AttachList)
                    {
                        mm.Attachments.Add(attachItem);
                    }
                }
                #endregion Set Mail Attachment

                //(5) Send the MailMessage (will use the Web.config settings) 
                smtp.Send(mm);

                isSuccess = true;

            }
            catch (SmtpException smtpEx)
            {
                error = smtpEx.ToString();

                //Log in Error Level
                //NLogger.Instance.Error(smtpEx);
            }
            catch (Exception generalEx)
            {
                error = generalEx.ToString();

                //Log in Error Level
                //NLogger.Instance.Error(generalEx);
            }

            return isSuccess;
        }

    }

}
