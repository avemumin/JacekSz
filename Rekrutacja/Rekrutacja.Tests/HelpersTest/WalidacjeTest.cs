using System;
using NUnit.Framework;
using Rekrutacja.Helpers;


namespace Rekrutacja.Tests.HelpersTest
{
  [TestFixture]
  public class WalidacjeTest
  {
    [Test]
    public void SprawdzOperator_GdyPoprawny()
    {
      string opP = "+";
      string opM = "-";
      string opMn = "*";
      string opD = "/";

      Assert.AreEqual(true, Walidacje.SprawdzOperator(opP));
      Assert.AreEqual(true, Walidacje.SprawdzOperator(opM));
      Assert.AreEqual(true, Walidacje.SprawdzOperator(opMn));
      Assert.AreEqual(true, Walidacje.SprawdzOperator(opD));
    }

    [Test]
    public void SprawdzOperator_GdyNiePoprawny()
    {
      string zly = "2";
      var blad = Assert.Throws<Exception>(() => Walidacje.SprawdzOperator(zly));
      Assert.AreEqual("Dozwolone symbole operacji to + - * /", blad.Message);
    }
  }
}
