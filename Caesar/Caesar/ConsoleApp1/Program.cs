using System;
using Caesar;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            CaesarLogikaAplikacji caesar = new CaesarLogikaAplikacji();
            /*caesar.ReadTextFromFile("C:/Users/sebar/source/repos/CsharProject---Caesar/Caesar/Caesar/ConsoleApp1/tekst.txt");
            Console.WriteLine(caesar.input);
            caesar.PobierzKlucz(3);
            caesar.Szyfruj();
            Console.WriteLine(caesar.output);
            caesar.WriteTextToFile("jebacgit.txt"); */

            caesar.ReadTextFromFile("C:/Users/sebar/source/repos/CsharProject---Caesar/Caesar/Caesar/ConsoleApp1/jebacgit.txt");
            caesar.PobierzKlucz(3);
            caesar.Deszyfruj();
            Console.WriteLine(caesar.output);
        }
    }
}
