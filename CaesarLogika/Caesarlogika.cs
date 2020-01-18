using System;
using System.IO;
using System.Text;


namespace Caesar
{
    public class CaesarLogikaAplikacji
    {
        /// <summary>
        /// Przechowuje odczytany z pliku tekst do zaszyfrowania/deszyfrowania.
        /// </summary>
        public string input;

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
        public string path;

        /// <summary>
        /// Odczytuje plik tekstowy ze ścieżki i zapisuje go do 'input' string.
        /// </summary>
        /// <param name="Path">Ścieżka pliku tekstowego</param>
        public void ReadTextFromFile(string sciezkaPliku)
        {
            this.path = Path.GetDirectoryName(sciezkaPliku);

            StreamReader sr = new StreamReader(sciezkaPliku);
            this.input = sr.ReadToEnd();
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
            //return Convert.ToChar((c + Key));
            return (char)((((c + Key) - d) % 26) + d);
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
            return (char)((((c + (26 - Key)) - d) % 26) + d);
            // return Convert.ToChar((c - Key) % 26);
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
