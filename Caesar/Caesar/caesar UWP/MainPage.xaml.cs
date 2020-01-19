using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Caesar;
using Windows.UI.Popups;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace caesar_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        CaesarLogikaAplikacji caesarLogika = new CaesarLogikaAplikacji();
        /// <summary>
        /// Wybiera plik i go ładuje do logiki aplikacji do późniejszego przetworzenia.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            caesarLogika.PickAndLoad();
        }

        /// <summary>
        /// Pobiera klucz do przesuwania 'char' o zadaną liczbę.
        /// </summary>
        private void KeyReader()
        {
            caesarLogika.PobierzKlucz(Convert.ToInt32(klucz.Text));
        }

        private async void SzyfrujButton_Click(object sender, RoutedEventArgs e)
        {
            caesarLogika.AlgorytmSzyfrujacy();
            var dialog = new MessageDialog("Zaszyfrowano");
            await dialog.ShowAsync();

        }

        private async void DeszyfrujButton_Click(object sender, RoutedEventArgs e)
        {
            caesarLogika.AlgorytmDeszyfrujacy();
            var dialog = new MessageDialog("Zdeszyfrowano");
            await dialog.ShowAsync();

        }

        private void SaveToFileButton_Click(object sender, RoutedEventArgs e)
        {
            caesarLogika.SaveToFile(nazwaPlikuTextBox.Text);
        }
    }
}
