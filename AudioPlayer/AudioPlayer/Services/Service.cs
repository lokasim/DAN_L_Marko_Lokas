using AudioPlayer.Models;
using System;
using System.Linq;
using Xceed.Wpf.Toolkit;

namespace AudioPlayer.Services
{
    class Service
    {
        public tblUser GetUsername(string username)
        {
            try
            {
                using (AudioPlayerEntities context = new AudioPlayerEntities())
                {
                    tblUser user = (from e in context.tblUsers where e.UsernameUser.Equals(username) select e).First();


                    return user;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        public tblUser AddUser(tblUser user)
        {
            try
            {

                using (AudioPlayerEntities context = new AudioPlayerEntities())
                {
                    if (user.UserID == 0)
                    {
                        tblUser newUser = new tblUser
                        {
                            UserID = user.UserID,
                            UsernameUser = user.UsernameUser,
                            PasswordUser = user.PasswordUser
                        };

                        context.tblUsers.Add(newUser);
                        context.SaveChanges();
                        user.UserID = newUser.UserID;
                        return user;
                    }
                    else
                    {
                        tblUser userToEdit = (from r in context.tblUsers where r.UserID == user.UserID select r).First();
                        
                        userToEdit.UsernameUser = user.UsernameUser;
                        userToEdit.PasswordUser = user.PasswordUser;
                        context.SaveChanges();
                        return user;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nešto je pošlo po zlu prilikom registracije", "Greška");
                System.Diagnostics.Debug.WriteLine("Exception: " + ex.Message.ToString());
                return null;
                throw;

            }
        }
    }
}
