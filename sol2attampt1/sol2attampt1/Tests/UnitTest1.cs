using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace sol2attampt1.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void UnitTest12()
        {
            double total = 988889;
            bool vipClient = false;
            if(total > 1000 || vipClient)
            {
                total = total * 1.1;
                Console.Out.Write("Скидка 10%" + total); 
            }
            
        }

        
    }
}
