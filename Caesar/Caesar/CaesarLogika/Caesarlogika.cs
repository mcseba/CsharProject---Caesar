using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Windows.Storage;
using Windows.Storage.Pickers;

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
        private string output;

        /// <summary>
        /// Key - przechowuje wartość, która wskazuje o ile liter ma zostać przesunięty każdy 'char' w szyfrowanym tekście.
        /// </summary>
        private int key;

        /// <summary>
        /// Użytkownik wybiera plik tekstowy i ładuje go do string input w celu późniejszego przetworzenia.
        /// </summary>
        public async void PickAndLoad()
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.Desktop;
            picker.FileTypeFilter.Add(".txt");

            StorageFile file = await picker.PickSingleFileAsync();
            input = await FileIO.ReadTextAsync(file);
        }

        public async void SaveToFile(string nazwaNowegoPliku)
        {
            var savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
            savePicker.FileTypeChoices.Add("plaintext", new List<string>() { ".txt" });
            savePicker.SuggestedFileName = nazwaNowegoPliku;
            StorageFile savedFile = await savePicker.PickSaveFileAsync();
            CachedFileManager.DeferUpdates(savedFile);
            await FileIO.WriteTextAsync(savedFile, output);
            Windows.Storage.Provider.FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(savedFile);
        }

        /// <summary>
        /// Przypisuje klucz wpisany przez użytkownika aplikacji.
        /// </summary>
        /// <param name="klucz">Klucz</param>
        public void PobierzKlucz(int klucz)
        {
            this.key = klucz;
        }

        /// <summary>
        /// Algorytm szyfrujący Cezara, zwraca zaszyfrowany tekst do output.
        /// Szyfr = (Char + Key) % 36
        /// </summary>
        public void AlgorytmSzyfrujacy()
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
                    int index = Array.IndexOf(alfabet, charArray[i]);
                    int newCharIndex = (index += key + 1) % 36;
                    encryptedChar[i] = char.IsUpper(charArray[i]) ? char.ToUpper(alfabet[newCharIndex]) : alfabet[newCharIndex];
                }
                output = String.Join("", encryptedChar);
            }
        }

        /// <summary>
        /// Algorytm deszyfrujący Cezara, zwraca zaszyfrowany tekst do output.
        /// Szyfr = (Char + 36 - Key) % 36;
        /// </summary>
        public void AlgorytmDeszyfrujacy()
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
                    int index = Array.IndexOf(alfabet, charArray[i]);
                    int newCharIndex = (index += 36 - key - 1) % 36;
                    encryptedChar[i] = char.IsUpper(charArray[i]) ? char.ToUpper(alfabet[newCharIndex]) : alfabet[newCharIndex];
                }
                output = String.Join("", encryptedChar);
            }
        }
    }
}
