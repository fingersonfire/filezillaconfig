namespace FileZillaConfig {
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Controls;
    using FileZillaConfig.Poco;

    public partial class MainWindow : Window {
        private Config _config = Factory.Config();

        public MainWindow() {
            InitializeComponent();

            Paths.RootMkdir();
            this.DataContext = _config;
            SettingsTab.Content = Views.Settings.Panel();
            DownloadsComboBox.DataContext = Paths.DownloadsDirs();
        }

        #region Events
        private void FileSaveButton_Click(object sender , RoutedEventArgs e) {
            _config.Save();
        }

        private void RestartService(object sender , RoutedEventArgs e) {
            Service.Restart();
        }

        private void UsersComboBox_SelectionChanged(object sender , SelectionChangedEventArgs e) {
            var user = UsersComboBox.SelectedItem as User;
            EditPanel.Visibility = (user == null) ? Visibility.Collapsed : Visibility.Visible;
        }

        private void UserDeleteButton_Click(object sender , RoutedEventArgs e) {
            var user = UsersComboBox.SelectedItem as User;
            if(user == null) return;

            _config.Users.Remove(user);
            Paths.UserRmdir(user.Name);
            UsersComboBox.Items.Refresh();
            if(_config.Users.Count == 0) {
                UsersComboBox.SelectedItem = null;
            } else {
                UsersComboBox.SelectedIndex = 0;
            }
        }
        private static readonly Regex NAME_PATTERN = new Regex(@"^[a-z][a-z0-9]+$" , RegexOptions.Compiled);
        private void UserAddButton_Click(object sender , RoutedEventArgs e) {
            var name = NewUserTextBox.Text;
            if(! NAME_PATTERN.IsMatch(name)) return;

            NewUserTextBox.Text = "";
            if(!_config.Users.Exists(u => u.Name == name)) {
                var user = Factory.User(name);
                _config.Users.Add(user);
                UsersComboBox.Items.Refresh();
                Paths.UserMkdir(name);
            }
            UsersComboBox.SelectedIndex = _config.Users.FindIndex(u => u.Name == name);
        }

        private void PasswordButton_Click(object sender , RoutedEventArgs e) {
            var md5 = XFactory.MD5Hash(PasswordTextBox.Text);
            var user = UsersComboBox.SelectedItem as User;
            user.Option.Pass = md5;
            MD5PasswordTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
        }

        private void DirDeleteButton_Click(object sender , RoutedEventArgs e) {
            var user = UsersComboBox.SelectedItem as User;
            var permission = DirsComboBox.SelectedItem as Permission;
            user.Permissions.Remove(permission);
            DirsComboBox.Items.Refresh();
            PermissionsControl.Items.Refresh();
            if(user.Permissions.Count == 0) {
                DirsComboBox.SelectedItem = null;
            } else {
                DirsComboBox.SelectedIndex = 0;
            }
        }
        private void DirAddButton_Click(object sender , RoutedEventArgs e) {
            if(DownloadsComboBox.SelectedItem == null) return;

            var insert_dir = DownloadsComboBox.SelectedValue.ToString();
            var user = UsersComboBox.SelectedItem as User;

            if(!user.Permissions.Exists(p => p.Dir == insert_dir)) {
                var permission = Factory.Permission(user.Name , insert_dir);
                user.Permissions.Add(permission);
                DirsComboBox.Items.Refresh();
                PermissionsControl.Items.Refresh();
            }
            DirsComboBox.SelectedIndex = user.Permissions.FindIndex(p => p.Dir == insert_dir);
        }
        #endregion
    }

    internal class AliasConverter : System.Windows.Data.IValueConverter {
        public object Convert(object value , System.Type targetType , object parameter , System.Globalization.CultureInfo culture) {
            var list = value as List<string>;
            var str = list==null ? "" : list.FirstOrDefault();
            return str;
        }

        public object ConvertBack(object value , System.Type targetType , object parameter , System.Globalization.CultureInfo culture) {
            throw new System.NotImplementedException();
        }
    }
}
