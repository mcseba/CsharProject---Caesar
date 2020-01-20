using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;

namespace Caesar
{


    public class CaesarLogikaAplikacji
    {
        private char[] alfabet = new char[] {'a', 'ą', 'b', 'c', 'ć', 'd', 'e', 'ę', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'ł',
                                                'm', 'n', 'ń', 'o', 'ó', 'p', 'q', 'r', 's', 'ś', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'ź', 'ż' };

        /// <summary>
        /// Przechowuje odczytany z pliku tekst do zaszyfrowania/deszyfrowania.
        /// </summary>
        private string input;

        /// <summary>
        /// Przechowuje gotowy tekst po konwersji (po zaszyfr./deszyfr.)
        /// </summary>
        public string output;

        /// <summary>
        /// Key - przechowuje wartość, która wskazuje o ile liter ma zostać przesunięty każdy 'char' w szyfrowanym tekście.
        /// </summary>
        private int key;

        /// <summary>
        /// Użytkownik wybiera plik tekstowy i ładuje go do string input w celu późniejszego przetworzenia.
        /// </summary>
        public async void PickAndLoadAsync()
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.Desktop;
            picker.FileTypeFilter.Add(".txt");

            StorageFile file = await picker.PickSingleFileAsync();
            if (file == null)
            {
                var message = new MessageDialog("Nie wybrano pliku.");
                await message.ShowAsync();
            }
            else
            {
                input = await FileIO.ReadTextAsync(file);
                var dialog = new MessageDialog($"Wybrano plik: {file.Path}");
                await dialog.ShowAsync();
            }
        }

        /// <summary>
        /// Zapisz transponowany tekst do pliku.
        /// </summary>
        /// <param name="nazwaNowegoPliku">Nazwa pod jakim tekst ma zostać zapisany</param>
        public async void SaveToFileAsync(string nazwaNowegoPliku)
        {
            if (output == null || output == "")
            {
                var dialog = new MessageDialog("Brak danych do zapisania.Wykonaj działanie na pliku.");
                await dialog.ShowAsync();
            }
            else
            {
                var savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                savePicker.FileTypeChoices.Add("plaintext", new List<string>() { ".txt" });
                savePicker.SuggestedFileName = nazwaNowegoPliku;
                StorageFile savedFile = await savePicker.PickSaveFileAsync();
                if (savedFile == null)
                {
                    var message = new MessageDialog("Nie wybrano pliku.");
                    await message.ShowAsync();
                }
                else
                {
                    CachedFileManager.DeferUpdates(savedFile);
                    await FileIO.WriteTextAsync(savedFile, output);
                    Windows.Storage.Provider.FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(savedFile);
                }
            }
        }

        /// <summary>
        /// Przypisuje klucz wpisany przez użytkownika aplikacji.
        /// </summary>
        /// <param name="klucz">Klucz</param>
        public async void PobierzKlucz(string klucz)
        {      
            if (klucz == null || klucz == "")
            {
                var dialog = new MessageDialog("Wprowadź poprawny klucz.");
                await dialog.ShowAsync();
            }
            else
                this.key = Convert.ToInt32(klucz);                        
        }

        /// <summary>
        /// Algorytm szyfrujący Cezara, zwraca zaszyfrowany tekst do output.
        /// Szyfr = (Char + Key) % 36
        /// </summary>
        public async void AlgorytmSzyfrujacy()
        {
            if (input == null || input == "")
            {
                var dialog = new MessageDialog("Wybierz poprawny plik tekstowy.");
                await dialog.ShowAsync();
            }
            else
            {
                char[] charArray = input.ToCharArray();
                char[] encryptedChar = new char[input.Length];

                for (int i = 0; i < charArray.Length; i++)
                {
                    if (!char.IsLetter(charArray[i]))
                    {
                        encryptedChar[i] = charArray[i];
                    }
                    else
                    {
                        int index = Array.IndexOf(alfabet, char.ToLower(charArray[i]));
                        int newCharIndex = (index += (key + 1)) % 36;
                        encryptedChar[i] = char.IsUpper(charArray[i]) ? char.ToUpper(alfabet[newCharIndex]) : alfabet[newCharIndex];
                    }
                    output = String.Join("", encryptedChar);
                }
                var dialog = new MessageDialog("Zaszyfrowano");
                await dialog.ShowAsync();
            }
        }

        /// <summary>
        /// Algorytm deszyfrujący Cezara, zwraca zaszyfrowany tekst do output.
        /// Szyfr = (Char + 36 - Key) % 36;
        /// </summary>
        public async void AlgorytmDeszyfrujacy()
        {
            if (input == null || input == "")
            {
                var mess = new MessageDialog("Wybierz poprawny plik tekstowy.");
                await mess.ShowAsync();
            }
            else
            {
                char[] charArray = input.ToCharArray();
                char[] encryptedChar = new char[input.Length];

                for (int i = 0; i < charArray.Length; i++)
                {
                    if (!char.IsLetter(charArray[i]))
                    {
                        encryptedChar[i] = charArray[i];
                    }
                    else
                    {
                        int index = Array.IndexOf(alfabet, char.ToLower(charArray[i]));
                        int newCharIndex = (index += (36 - key - 1)) % 36;
                        encryptedChar[i] = char.IsUpper(charArray[i]) ? char.ToUpper(alfabet[newCharIndex]) : alfabet[newCharIndex];
                    }
                    output = String.Join("", encryptedChar);
                }
                var dialog = new MessageDialog("Zdeszyfrowano");
                await dialog.ShowAsync();
            }
        }
    }
}
