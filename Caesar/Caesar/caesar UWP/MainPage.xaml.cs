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
using System.Threading;
using System.Threading.Tasks;
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
        /// Ustawia nazwę wybranego pliku w TextBoxie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WybierzPlikButton_Click(object sender, RoutedEventArgs e)
        {
            caesarLogika.PickAndLoadAsync();
        }

        /// <summary>
        /// Pobiera klucz do przesuwania 'char' o zadaną liczbę.
        /// </summary>
        private void KeyReader()
        {
            caesarLogika.PobierzKlucz(klucz.Text);
        }

        /// <summary>
        /// Button "SzyfrujButtonClick" szyfruje załadowany plik.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SzyfrujButton_Click(object sender, RoutedEventArgs e)
        {
            KeyReader();
            caesarLogika.AlgorytmSzyfrujacy();

        }

        /// <summary>
        /// Button "DeszyfrujButtonClick" deszyfruje załadowany plik.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeszyfrujButton_Click(object sender, RoutedEventArgs e)
        {
            KeyReader();
            caesarLogika.AlgorytmDeszyfrujacy();
        }

        /// <summary>
        /// Button "SaveToFileButton" zapisuje plik po uprzednim transponowaniu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToFileButton_Click(object sender, RoutedEventArgs e)
        {
            caesarLogika.SaveToFileAsync(nazwaPlikuTextBox.Text);
        }
    }
}
