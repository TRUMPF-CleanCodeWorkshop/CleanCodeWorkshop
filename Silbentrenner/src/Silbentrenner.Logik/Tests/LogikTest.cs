using NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace Silbentrenner.Logik
{
    [TestFixture]
    public class LogikTest
    {

        [Test]
        public void ErsetzeCRLFDurchSpace_zwei_Returns()
        {
            var text = "123" + Environment.NewLine + "ab" + Environment.NewLine;



        }

        [Test]
        public void Bereinigen_Von_Leerzeichen()
        {
            
        }

        [Test]
        public void Woerter_In_Silben_Trennen()
        {
            
        }
    }
}
