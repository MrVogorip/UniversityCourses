using System;
using System.Threading;
using System.Text;
using System.Windows;
using System.Net.Sockets;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using ChatClient.Pages;

namespace ChatClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Closed += delegate { chat.Disconnect(); };
            OpenPage(pages.login);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }
        private void DisconnectBtn_Click(object sender, RoutedEventArgs e)
        {
            chat.Disconnect();
        }

        private void OffPanelOn(object sender, System.Windows.Input.MouseEventArgs e)
        {
            chat.Disconnect();
            MenuExitClick(sender, e);
        }

        private void TrayPanelOn(object sender, System.Windows.Input.MouseEventArgs e)
        {
            CancelEventArgs cancelEventArgs = new CancelEventArgs();
            OnClosing(cancelEventArgs);
        }


        private void ShowHideMainWindow(object sender, RoutedEventArgs e)
        {
            TrayMenu.IsOpen = false;
            if (IsVisible)
            {
                Hide();
                (TrayMenu.Items[0] as System.Windows.Controls.MenuItem).Header = "Show";
            }
            else
            {
                Show();
                (TrayMenu.Items[0] as System.Windows.Controls.MenuItem).Header = "Hide";
                WindowState = CurrentWindowState;
                Activate();
            }
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
            if (this.WindowState == System.Windows.WindowState.Minimized)
            {
                Hide();
                (TrayMenu.Items[0] as System.Windows.Controls.MenuItem).Header = "Show";
            }
            else
            {
                CurrentWindowState = WindowState;
            }
        }
        private System.Windows.Forms.NotifyIcon TrayIcon = null;
        private System.Windows.Controls.ContextMenu TrayMenu = null;
        private WindowState fCurrentWindowState = WindowState.Normal;
        public WindowState CurrentWindowState
        {
            get { return fCurrentWindowState; }
            set { fCurrentWindowState = value; }
        }
        private bool fCanClose = false;
        public bool CanClose
        {
            get { return fCanClose; }
            set { fCanClose = value; }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!CanClose)
            {
                e.Cancel = true;
                CurrentWindowState = this.WindowState;
                (TrayMenu.Items[0] as System.Windows.Controls.MenuItem).Header = "Show";
                Hide();
            }
            else
            {
                TrayIcon.Visible = false;
            }
        }

        private void MenuExitClick(object sender, RoutedEventArgs e)
        {
            CanClose = true;
            Close();
        }

        private bool createTrayIcon()
        {
            bool result = false;
            if (TrayIcon == null)
            {
                TrayIcon = new NotifyIcon();
                TrayIcon.Icon = Properties.Resources.icon;
                TrayIcon.Text = "Here is tray icon text.";
                TrayMenu = Resources["TrayMenu"] as System.Windows.Controls.ContextMenu;
                TrayIcon.Click += delegate (object sender, EventArgs e)
                {
                    if ((e as System.Windows.Forms.MouseEventArgs).Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        ShowHideMainWindow(sender, null);
                    }
                    else
                    {
                        TrayMenu.IsOpen = true;
                        Activate();
                    }
                };
                result = true;
            }
            else
            {
                result = true;
            }
            TrayIcon.Visible = true;
            return result;
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            createTrayIcon();
        }


        private void NameBox_TextChanged(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
        chat chat = new chat();
        login login = new login();
        public enum pages
        {
            login,
            chat,
        }
        public void OpenPage(pages pages)
        {
            if (pages == pages.login)
            {
                frame.Navigate(login._login(this, chat));
            }
            if (pages == pages.chat)
            {
                frame.Navigate(chat._chat(this, login));
            }
        }
     
    }
}
