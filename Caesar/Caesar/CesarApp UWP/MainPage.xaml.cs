using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Caesar;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CesarApp_UWP
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

        public async void Button_Click(object sender, RoutedEventArgs e)
        {
            //wybór pliku
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            openPicker.FileTypeFilter.Add(".txt");

            StorageFile file = await openPicker.PickSingleFileAsync();
            string fileName = file.Name;

           
            
            CaesarLogikaAplikacji caesarLogika = new CaesarLogikaAplikacji();
            caesarLogika.input  = await FileIO.ReadTextAsync(file);
       
   

            // pobranie klucza
            int key = Convert.ToInt32(keyBox.Text);
            caesarLogika.PobierzKlucz(key);
            caesarLogika.Szyfruj();
           
            
            var dialoga = new MessageDialog("Gdzie zapisać?" + caesarLogika.output);
            await dialoga.ShowAsync();
            // zapis pliku 
            var savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation =
                PickerLocationId.Desktop;
            //jaki typ pliku mamy do wyboru 
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            // domysna nazwa nowego pliku
            savePicker.SuggestedFileName = "Zaszyfrowany plik " +  fileName;
            //
            StorageFile savedfile = await savePicker.PickSaveFileAsync();

            // zapobiegamy zmienienie pliku przez inne aplikacje dopóki nie skończymy zmieniac tego pliku 
            CachedFileManager.DeferUpdates(savedfile);
            // wpisanie outputa do pliku 
            await FileIO.WriteTextAsync(savedfile, caesarLogika.output);
            // zawiadamiamy Windowsz ze skończylismy prace nad plikiem i ze inne aplikacje moga go teraz zmieniac 
            Windows.Storage.Provider.FileUpdateStatus status =
                await CachedFileManager.CompleteUpdatesAsync(savedfile);
            //ostatnie okienko z outputem
            var dialog = new MessageDialog(caesarLogika.output);
            await dialog.ShowAsync();



        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CaesarLogikaAplikacji caesarLogika = new CaesarLogikaAplikacji();
            string path = box.Text;
            caesarLogika.ReadTextFromFile(path);
            

        }
    }
}
