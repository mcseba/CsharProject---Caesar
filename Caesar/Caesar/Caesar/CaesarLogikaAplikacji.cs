using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Caesar
{
    public class CaesarLogikaAplikacji
    {
        /// <summary>
        /// Przechowuje odczytany z pliku tekst do zaszyfrowania/deszyfrowania.
        /// </summary>
        public string input;

        private char[] alfabet = {'a', 'ą', 'b', 'c', 'ć', 'd', 'e', 'ę', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'ł', 'm', 'n', 'ń', 'o', 'ó'
        , 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'ź', 'ż'};

        /// <summary>
        /// Przechowuje gotowy tekst po konwersji (po zaszyfr./deszyfr.)
        /// </summary>
        public string output;

        /// <summary>
        /// Key - przechowuje wartość, która wskazuje o ile liter ma zostać przesunięty każdy 'char' w szyfrowanym tekście.
        /// </summary>
        private int key;

        /// <summary>
        /// Przechowuje ścieżkę wskazanego pliku.
        /// </summary>
        private string path;

        /// <summary>
        /// Odczytuje plik tekstowy ze ścieżki i zapisuje go do 'input' string.
        /// </summary>
        /// <param name="Path">Ścieżka pliku tekstowego</param>
        public void ReadTextFromFile(string tekst)
        {
            // this.input = tekst;
            StreamReader sr = new StreamReader(tekst);
            input = sr.ReadToEnd();
        }

        /// <summary>
        /// Zapisuje output do pliku tekstowego.
        /// </summary>
        /// <param name="nazwaNowegoPliku">Nazwa pliku do którego zostanie zapisany output programu</param>
        public void WriteTextToFile(string nazwaNowegoPliku)
        {
            string sciezka = Path.Combine(path, nazwaNowegoPliku);

            File.WriteAllText(sciezka, output);
        }

        /// <summary>
        /// Przypisuje klucz wpisany przez użytkownika aplikacji.
        /// </summary>
        /// <param name="i">Klucz</param>
        public void PobierzKlucz(int i)
        {
            this.key = i;
        }

        /// <summary>
        /// Algorytm do szyfrowania tekstu. 'Szyfr = (Char + Key) % 26'
        /// </summary>
        /// <param name="c">Char który zostaje przesunięty o 'key' wartość.</param>
        /// <param name="Key">Wartość o ile zostanie przesunięty char.</param>
        /// <returns></returns>
        public static char AlgorytmSzyfrujacy(char c, int Key)
        {
            if (!char.IsLetter(c))
            {
                return c;
            }
            char d = char.IsUpper(c) ? 'A' : 'a';
            return (char)((((c + Key) - d) % 26) + d);
        }

        /// <summary>
        /// Algorytm cezara do szyfrowania tekstu z polskimi znakami. 'Szyfr = (Char + Key) % 34'
        /// </summary>
        public void AlgorytmSzyfrujacy2()
        {
            char[] charArray = input.ToCharArray();
            char[] encryptedMessage = new char[charArray.Length];

            for (int i = 0; i < charArray.Length; i++)
            {
                if (!char.IsLetter(charArray[i]))
                {
                    encryptedMessage[i] = charArray[i];
                }
                else
                {
                    int index = Array.IndexOf(alfabet, charArray[i]);
                    int indexLetter = (index += key + 1) % 34;
                    encryptedMessage[i] = alfabet[indexLetter];
                }
            }
            output = String.Join("", encryptedMessage);
        }

        /// <summary>
        /// Szyfruje wczytany plik tekstowy używając AlgorytmSzyfrujący() do string output.
        /// </summary>
        public void Szyfruj()
        {
            output = string.Empty;

            foreach (char ch in input)
            {
                output += AlgorytmSzyfrujacy(ch, this.key);
            }
        }

        /// <summary>
        /// Algorytm do deszyfracji tekstu. 'Deszyfr = (Char - Key) % 26'
        /// </summary>
        /// <param name="c">Char który zostanie przesunięty o 'Key' wartość.</param>
        /// <param name="Key">Wartość o ile zostanie przesunięty char.</param>
        /// <returns></returns>
        public static char AlgorytmDeszyfrujacy(char c, int Key)
        {
            if (!char.IsLetter(c))
            {
                return c;
            }
            char d = char.IsUpper(c) ? 'A' : 'a';
            return (char)((((c + (26 - Key)) - d) % 26)  + d);
           // return Convert.ToChar((c - Key) % 26);
        }

        /// <summary>
        /// Algorytm cezara do szyfrowania tekstu z polskimi znakami. 'Szyfr = (Char - key) % 34'
        /// </summary>
        public void AlgorytmDeszyfrujacy2()
        {
            char[] charArray = input.ToCharArray();
            char[] encryptedMessage = new char[charArray.Length];

            for (int i = 0; i < charArray.Length; i++)
            {
                if (!char.IsLetter(charArray[i]))
                {
                    encryptedMessage[i] = charArray[i];
                }
                else
                {
                    int index = Array.IndexOf(alfabet, charArray[i]);
                    int indexLetter = (index += 34 - key - 1) % 34;
                    encryptedMessage[i] = alfabet[indexLetter];
                }
            }
            output = String.Join("", encryptedMessage);
        }

        /// <summary>
        /// Deszyfruje wczytany plik tekstowy używając AlgorytmDeszyfrujący() do string output.
        /// </summary>
        public void Deszyfruj()
        {
            output = string.Empty;

            foreach (char ch in input)
            {
                output += AlgorytmDeszyfrujacy(ch, this.key);
            }
        }
    }
}
