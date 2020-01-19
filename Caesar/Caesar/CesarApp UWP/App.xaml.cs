using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Caesar;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CesarApp_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        CaesarLogikaAplikacji caesarLogika = new CaesarLogikaAplikacji();
        public BlankPage1()
        {
            this.InitializeComponent();
        }

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

        private void SzyfrujButton_Click(object sender, RoutedEventArgs e)
        {
            caesarLogika.AlgorytmSzyfrujacy();
        }

        private void DeszyfrujButton_Click(object sender, RoutedEventArgs e)
        {
            caesarLogika.AlgorytmDeszyfrujacy();
        }

        private void SaveToFileButton_Click(object sender, RoutedEventArgs e)
        {
            caesarLogika.SaveToFile(nazwaPlikuTextBox.Text);
        }
    }
}
