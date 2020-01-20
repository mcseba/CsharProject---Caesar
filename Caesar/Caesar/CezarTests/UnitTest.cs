

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Caesar;


namespace CezarTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

        }

        [TestMethod]
        public void test()
        {
            CaesarLogikaAplikacji caesarLogika = new CaesarLogikaAplikacji();
            caesarLogika.input = "abc";
            caesarLogika.PobierzKlucz("3");
             caesarLogika.AlgorytmSzyfrujacy();
            
            
            Assert.AreEqual("ćeę", caesarLogika.output);
            
        }
       
             [TestMethod]
        public void PobierzKlucz_TakieSame()
        {
            CaesarLogikaAplikacji caesarLogika = new CaesarLogikaAplikacji();
            
            caesarLogika.PobierzKlucz("3");



            Assert.AreEqual(3, caesarLogika.key);

        }
        [TestMethod]
        public void PobierzKlucz_Inne()
        {
            CaesarLogikaAplikacji caesarLogika = new CaesarLogikaAplikacji();
            
            caesarLogika.PobierzKlucz("4");



            Assert.AreNotEqual(3, caesarLogika.key);

        }
        [TestMethod]

        public void Deszyfruj()
        {
            CaesarLogikaAplikacji caesarLogika = new CaesarLogikaAplikacji();
            caesarLogika.PobierzKlucz("3");
            caesarLogika.input = "ćeę";
            caesarLogika.AlgorytmDeszyfrujacy();

            Assert.AreEqual("abc" , caesarLogika.output);
        }
    }
    

}
