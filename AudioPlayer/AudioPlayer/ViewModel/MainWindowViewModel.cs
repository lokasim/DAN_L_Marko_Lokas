using AudioPlayer.Command;
using AudioPlayer.Models;
using AudioPlayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AudioPlayer.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        readonly MainWindow mainWindow;
        public static bool loggedBool = false;

        #region Properties
        private List<tblUser> userList;
        public List<tblUser> UserList
        {
            get
            {
                return userList;
            }
            set
            {
                userList = value;
                OnPropertyChanged("UserList");
            }
        }

        private tblUser user;
        public tblUser User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                OnPropertyChanged("User");
            }
        }
        private bool isUpdateUser;
        public bool IsUpdateUser
        {
            get
            {
                return isUpdateUser;
            }
            set
            {
                isUpdateUser = value;
            }
        }
        #endregion

        public MainWindowViewModel(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            user = new tblUser();
        }

        private ICommand exit;
        public ICommand Exit
        {
            get
            {
                if (exit == null)
                {
                    exit = new RelayCommand(param => ExitExecute(), param => CanExitExecute());
                }
                return exit;
            }
        }

        /// <summary>
        /// Exit application
        /// </summary>
        private void ExitExecute()
        {
            MessageBoxResult dialog = Xceed.Wpf.Toolkit.MessageBox.Show("Da li želite napustiti aplikaciju?", "Izlaz", MessageBoxButton.YesNo);

            if (dialog == MessageBoxResult.Yes)
            {
                Environment.Exit(0);
            }
        }

        private bool CanExitExecute()
        {
            return true;
        }

        private ICommand login;
        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(param => LoginExecute(), param => CanLoginExecute());
                }
                return login;
            }
        }

        /// <summary>
        /// Method for login employee or manager
        /// </summary>
        private void LoginExecute()
        {
           
        }

        private bool CanLoginExecute()
        {
            return true;
        }

        private ICommand backToLogin;
        public ICommand BackToLogin
        {
            get
            {
                if (backToLogin == null)
                {
                    backToLogin = new RelayCommand(param => BackLoginExecute(), param => CanBackLoginExecute());
                }
                return backToLogin;
            }
        }

        //Logout Admin
        private void BackLoginExecute()
        {
            mainWindow.NameTextBox.Text = "";
            mainWindow.passwordBox.Password = "";
            mainWindow.login.Visibility = Visibility.Visible;
            mainWindow.Images0.Visibility = Visibility.Collapsed;
            mainWindow.Images1.Visibility = Visibility.Visible;
            mainWindow.SignUp.Visibility = Visibility.Collapsed;
            mainWindow.NameTextBox.Focus();
            mainWindow.txtKorisnickoIme.Text = "";
            mainWindow.txtLozinkaRegistracija.Text = "";
            mainWindow.txtReLozinkaRegistracija.Text = "";

            return;
        }
        private bool CanBackLoginExecute()
        {
            return true;
        }

        private ICommand signUp;
        public ICommand SignUp
        {
            get
            {
                if (signUp == null)
                {
                    signUp = new RelayCommand(param => SignUpExecute(), param => CanSignUpExecute());
                }
                return signUp;
            }
        }
        //Create new employee/manager
        private void SignUpExecute()
        {
            try
            {
                Service s = new Service();
                
                string user = this.User.UsernameUser;

                //uniqueness check username
                tblUser User = s.GetUsername(user);

                if (User != null)
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Korisničko ime već postoji u bazi, pokušajte sa drugim.", "Korisničko ime");
                    return;
                }

                string password = this.User.PasswordUser;
                char[] passwordChar = password.ToCharArray();

                int upperLetter = 0;
                for (int i = 0; i < passwordChar.Length; i++)
                {
                    if (Char.IsUpper(passwordChar[i]))
                    {
                        upperLetter++;
                    }
                }
                
                if(upperLetter < 2)
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Lozinka mora da sadrži minimum dva velika slova.", "Lozinka");
                    return;
                }

                // Hash Password
                var hasher = new SHA256Managed();
                var unhashed = Encoding.Unicode.GetBytes(this.User.PasswordUser);
                var hashed = hasher.ComputeHash(unhashed);
                var hashedPassword = Convert.ToBase64String(hashed);

                this.User.PasswordUser = hashedPassword;

                s.AddUser(this.User);
                IsUpdateUser = true;

                string poruka = "Add User: " + this.User.UsernameUser;
                Xceed.Wpf.Toolkit.MessageBox.Show(poruka, "successfully added employee", MessageBoxButton.OK);
                
                mainWindow.txtKorisnickoIme.Text = "";
                mainWindow.txtLozinkaRegistracija.Text = "";
                mainWindow.txtReLozinkaRegistracija.Text = "";
                mainWindow.NameTextBox.Focus();
            }
            catch (Exception ex)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show(ex.ToString());
            }
        }
        

        private bool CanSignUpExecute()
        {
            if (String.IsNullOrEmpty(user.UsernameUser) ||
                String.IsNullOrEmpty(user.PasswordUser))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
