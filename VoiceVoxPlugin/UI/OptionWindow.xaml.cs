﻿using System.IO;
using System.Windows;
using Microsoft.Win32;
using VoiceVoxPlugin.Properties;
using VoiceVoxPlugin.ViewModel;

namespace VoiceVoxPlugin.UI
{
    /// <summary>
    /// OptionWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class OptionWindow 
    {
        public OptionWindow()
        {
            InitializeComponent();
            ViewModel.ExePath = Settings.Default.ExePath;
            ViewModel.ExitWhenFinished = Settings.Default.ExitWhenFinished;
            ViewModel.VoiceVoxTimeout = Settings.Default.VoiceVoxTimeout;
        }

        private OptionWindowViewModel ViewModel => DataContext as OptionWindowViewModel;
        public bool ExitWhenFinished => ViewModel.ExitWhenFinished;
        public string ExePath => ViewModel.ExePath;
        public int VoiceVoxTimeout => ViewModel.VoiceVoxTimeout;

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "VOICE VOX の実行ファイルの場所を指定してください",
                Filter = "実行ファイル|*.exe",
            };

            if (!(dialog.ShowDialog() ?? false))
            {
                return;
            }

            TextBox.Text = dialog.FileName;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists(ViewModel.ExePath))
            {
                MessageBox.Show("VOICE VOX実行ファイルが見つかりません", "設定エラー", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            Settings.Default.ExePath = ViewModel.ExePath;
            Settings.Default.ExitWhenFinished = ViewModel.ExitWhenFinished;
            Settings.Default.VoiceVoxTimeout = ViewModel.VoiceVoxTimeout;

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
