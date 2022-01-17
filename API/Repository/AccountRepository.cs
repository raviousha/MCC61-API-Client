using API.Context;
using API.Models;
using API.ViewModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Repository
{
    public class AccountRepository : GeneralRepository<MyContext, Account, String>
    {
        private readonly MyContext myContext;
        private readonly Random random = new Random();
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public int Login(LoginVM loginVM)
        {
            try
            {
                var emp = myContext.Employees.FirstOrDefault(e => e.Email == loginVM.email);
                if (emp == null)
                {
                    return 3;
                }

                var acc = myContext.Accounts.FirstOrDefault(a => a.NIK == emp.NIK);

                var emailValidation = emp.Email == loginVM.email;
                var passwordValidation = BCrypt.Net.BCrypt.Verify(loginVM.password, acc.password);

                if (emailValidation && passwordValidation)
                {
                    return 1;
                }
                else if (emailValidation && !passwordValidation)
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int ForgotPassword(ForgotVM forgotVM)
        {
            var emp = myContext.Employees.FirstOrDefault(e => e.Email == forgotVM.email);
            if (emp == null)
            {
                return 2;
            }

            var acc = myContext.Accounts.AsNoTracking().FirstOrDefault(a => a.NIK == emp.NIK);
            if (acc != null)
            {
                myContext.Entry(acc).State = EntityState.Detached;

                acc.NIK = emp.NIK;
                acc.OTP = random.Next(100000, 999999); ;
                acc.expiredToken = DateTime.Now.AddMinutes(5);
                acc.isUsed = false;

                myContext.Entry(acc).State = EntityState.Modified;
                var result = myContext.SaveChanges();
                return result;
            }
            else
            {
                return 0;
            }
        }

        public int ChangePassword(ChangePassVM changePassVM)
        {
            var time = DateTime.Now;

            var emp = myContext.Employees.FirstOrDefault(e => e.Email == changePassVM.email);
            if (emp == null)
            {
                return 5;
            }

            var acc = myContext.Accounts.FirstOrDefault(e => e.NIK == emp.NIK);
            if (acc != null)
            {
                if (acc.OTP == changePassVM.OTP)
                {
                    if (time < acc.expiredToken)
                    {
                        if (acc.isUsed != true)
                        {
                            acc.password = BCrypt.Net.BCrypt.HashPassword(changePassVM.password);
                            acc.isUsed = true;

                            myContext.Entry(acc).State = EntityState.Modified;
                            var result = myContext.SaveChanges();
                            return result;
                        }
                        else
                        {
                            return 4;
                        }
                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    return 3;
                }
            }
            else
            {
                return 0;
            }
        }

        public void SendMail(ForgotVM forgotVM)
        {
            var emp = myContext.Employees.FirstOrDefault(e => e.Email == forgotVM.email);
            var acc = myContext.Accounts.FirstOrDefault(a => a.NIK == emp.NIK);
            var time = acc.expiredToken != null ? acc.expiredToken.Value.ToString("HH:mm dd/MM/yyyy") : "n/a";

            // create message
            var sendEmail = new MimeMessage();
            sendEmail.Sender = MailboxAddress.Parse("moviesmanias11@gmail.com");
            sendEmail.Sender.Name = "MCC-61 API";
            sendEmail.From.Add(sendEmail.Sender);
            sendEmail.To.Add(MailboxAddress.Parse(forgotVM.email));
            sendEmail.Subject = "OTP for Changing Password";
            sendEmail.Body = new TextPart(TextFormat.Html)
            {
                Text = $"Hello, {emp.firstName} {emp.lastName}" +
                $"<br>" +
                $"We’ve received a password change request for your account. To complete your request, enter the following verification code:" +
                $"<br>" +
                $"<p style=text-align:left;font-size:30px;font-weight:bold;>{acc.OTP}</p>" +
                $"<br>" +
                $"The OTP will be valid until {time}"
            };

            // send email
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587);

                // Note: since we don't have an OAuth2 token, disable the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                {
                    client.Authenticate("moviesmanias11@gmail.com", "transtool");
                }

                client.Send(sendEmail);
                client.Disconnect(true);
            }
        }

        public IEnumerable<object> GetRoles(LoginVM loginVM)
        {
            var empList = myContext.Employees;
            var arList = myContext.AccountRoles;
            var rList = myContext.Roles;

            var query = from emp in empList
                        join ar in arList
                        on emp.NIK equals ar.AccountsNIK
                        join r in rList
                        on ar.RolesId equals r.Id
                        where emp.Email == loginVM.email
                        select r.name;

            return query;
        }
    }
}
