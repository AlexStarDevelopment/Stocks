using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WealthManager.PL;
using System.Net.Mail;
using System.Net;

namespace WealthManager.BL
{
    public class CUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public void SendEmail()
        {
            try
            {
                string to = "alexstar1408@gmail.com";
                string from = "alexstar1408@gmail.com";
                string subject = "Do Not Reply - Wealth Manager Notification";
                string body = @"You have ner transactions to view!";
                MailMessage message = new MailMessage(from, to, subject, body);
                System.Net.NetworkCredential auth = new System.Net.NetworkCredential("alexstar1408@gmail.com", "aorvlycgeupwksmn");
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Timeout = 100000;
                // Credentials are necessary if the server requires the client 
                // to authenticate before it will send e-mail on the client's behalf.
                client.Credentials = CredentialCache.DefaultNetworkCredentials;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                message.IsBodyHtml = true;
                client.Credentials = auth;

                client.Send(message);



                //    MailMessage mail = new MailMessage("alexstar1408@gmail.com", "alexstar1408@gmail.com", "New Transaction!", "You added a new transaction!");
                //System.Net.NetworkCredential auth = new System.Net.NetworkCredential("alexstar1408@gmail.com", "");
                //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                //client.EnableSsl = false;
                //client.UseDefaultCredentials = false;
                //mail.IsBodyHtml = true;
                //client.Credentials = auth;
                //client.Send(mail);
            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw;
            }
        }

        #region Login ASP Method
        public bool Login()
        {
            try
            {
                if (Email != string.Empty && Password != string.Empty)
                {
                    WealthDataContext oDc = new WealthDataContext();
                    tblUser otblUser = oDc.tblUsers.FirstOrDefault(u => u.Email == Email);

                    if (otblUser != null)
                    {
                        tblUser ouser = oDc.tblUsers.FirstOrDefault(u => u.Password == GetHash());

                        if (ouser != null)
                        {
                            FirstName = ouser.FirstName;
                            LastName = ouser.LastName;
                            Password = ouser.Password;
                            Email = ouser.Email;
                            Id = ouser.Id;

                            //set the app to the user 
                            CLogin.set(Id);

                            return true;
                        }
                        else
                        {
                            oDc = null;
                            throw new Exception("Password is incorrect");
                        }
                    }
                    else
                    {
                        oDc = null;
                        throw new Exception("Email: " + Email + " could not be found");
                    }

                }
                else
                {
                    throw new Exception("User ID and Password cannok be blank. Try again");
                }
            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }
        }
        //password hashing
        private string GetHash()
        {
            using (var hash = new System.Security.Cryptography.SHA1Managed())
            {
                var hashbytes = System.Text.Encoding.UTF8.GetBytes(Password);
                return Convert.ToBase64String(hash.ComputeHash(hashbytes));
            }
        }
        #endregion

        #region Constructors
        public void Load()
        {

        }

        // Default Constructor
        public CUser()
        {

        }

        // Custom Constructor
        public CUser(Guid id, string firstName, string lastName, string email, string password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        //login constructor 
        public CUser(string userId, string userPass)
        {
            Email = userId;
            Password = userPass;

        }

        public CUser(string firstName)
        {
            FirstName = firstName;
        } 
        #endregion

        public bool Login(string userId, string userPass)
        {
            Email = userId;
            Password = userPass;

            try
            {
                if (Email != string.Empty && Password != string.Empty)
                {
                    WealthDataContext oDc = new WealthDataContext();
                    tblUser user = oDc.tblUsers.FirstOrDefault(p => p.Email == this.Email);

                    if (user != null)
                    {                     
                       
                        tblUser user2 = oDc.tblUsers.FirstOrDefault(p => p.Password == this.GetHash());

                        if (user2 != null)
                        {
                            FirstName = user.FirstName;
                            LastName = user.LastName;
                            Password = user.Password;
                            Email = user.Email;
                            Id = user.Id;

                            //set the app to the user 
                            CLogin.set(Id);

                            return true;
                        }
                        else
                        {
                            oDc = null;
                            throw new Exception("Credentials are incorrect");
                        }
                    }
                    else
                    {
                        oDc = null;
                        throw new Exception("User ID: " + Email + " could not be found");
                    }

                }
                else
                {
                    throw new Exception("User ID and Password cannok be blank. Try again");
                }
            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }
        }

        public void Map(tblUser user)
        {
            user.Id = this.Id;
            user.Email = this.Email;
            user.FirstName = this.FirstName;
            user.LastName = this.LastName;
            user.Password = this.Password;
        }

        public void Insert()
        {
            try
            {
                WealthDataContext oDc = new WealthDataContext();
                tblUser user = new tblUser
                {
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    Email = this.Email,
                    Password = this.GetHash(),
                    Id = Guid.NewGuid()
                };

                oDc.tblUsers.InsertOnSubmit(user);
                oDc.SubmitChanges();


            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }
        }

        public void Update()
        {
            try
            {
                WealthDataContext oDc = new WealthDataContext();

                tblUser user = oDc.tblUsers.FirstOrDefault(p => p.Id == this.Id);

                if (user != null)
                {
                    user.FirstName = this.FirstName;
                    user.LastName = this.LastName;
                    user.Email = this.Email;
                    user.Password = this.Password;
                    oDc.SubmitChanges();
                }
                else
                {
                    throw new Exception("Row not found. (Id : " + this.Id + ")");
                }


            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }
        }

        public void LoadById()
        {
            try
            {
                WealthDataContext oDc = new WealthDataContext();
                tblUser user = oDc.tblUsers.FirstOrDefault(p => p.Id == this.Id);

                if (user != null)
                {
                    this.Id = user.Id;
                    this.FirstName = user.FirstName;
                    this.LastName = user.LastName;
                    this.Email = user.Email;
                    user.Password = user.Password;
                }
            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }
        }

        public void Delete()
        {
            try
            {
                WealthDataContext oDc = new WealthDataContext();
                tblUser user = oDc.tblUsers.FirstOrDefault(p => p.Id == this.Id);

                if (user != null)
                {
                    oDc.tblUsers.DeleteOnSubmit(user);
                    oDc.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                CErrorLog err = new CErrorLog();
                err.LogError(ex.Message);
                throw ex;
            }
        }
    }
}
