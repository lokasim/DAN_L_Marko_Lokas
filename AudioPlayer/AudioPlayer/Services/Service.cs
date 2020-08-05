using AudioPlayer.Models;
using System;
using System.Linq;
using Xceed.Wpf.Toolkit;

namespace AudioPlayer.Services
{
    class Service
    {
        /// <summary>
        /// Checks if there is a username in the database
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Method for adding new User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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
            }
        }

        /// <summary>
        /// Return username and password on database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public tblUser GetUsernamePassword(string username, string password)
        {
            try
            {
                using (AudioPlayerEntities context = new AudioPlayerEntities())
                {
                    tblUser user = (from e in context.tblUsers where e.UsernameUser.Equals(username) where e.PasswordUser.Equals(password) select e).First();
                    
                    return user;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
