
using System;
using NUnit.Framework;
using Rekrutacja.Helpers;
using static Rekrutacja.Workers.Template.TemplateWorker;

namespace Rekrutacja.Tests.HelpersTest
{
  [TestFixture]
  public class LiczymyTests
  {
    [Test]
    public void Oblicz_Jest_Poprawne()
    {
      TemplateWorkerParametry gdyPlus = new TemplateWorkerParametry(null)
      {
        WartoscA = 1,
        WartoscB = 2,
        Operacja = "+"
      };
      TemplateWorkerParametry gdyMinus = new TemplateWorkerParametry(null)
      {
        WartoscA = 1,
        WartoscB = 2,
        Operacja = "-"
      };
      TemplateWorkerParametry gdyRazy = new TemplateWorkerParametry(null)
      {
        WartoscA = 2,
        WartoscB = 3,
        Operacja = "*"
      };
      TemplateWorkerParametry gdyDziel = new TemplateWorkerParametry(null)
      {
        WartoscA = 4,
        WartoscB = 2,
        Operacja = "/"
      };

      Assert.AreEqual(3, Liczymy.ObliczWartosc(gdyPlus));
      Assert.AreEqual(-1, Liczymy.ObliczWartosc(gdyMinus));
      Assert.AreEqual(6, Liczymy.ObliczWartosc(gdyRazy));
      Assert.AreEqual(2, Liczymy.ObliczWartosc(gdyDziel));
    }

    [Test]
    public void GdyDziel_PrzezZero()
    {
      TemplateWorkerParametry gdyDziel = new TemplateWorkerParametry(null)
      {
        WartoscA = 4,
        WartoscB = 0,
        Operacja = "/"
      };

      var msg = Assert.Throws<DivideByZeroException>(() => Liczymy.ObliczWartosc(gdyDziel));
      Assert.AreEqual("Nastąpiła próba podzielenia przez zero.", msg.Message);
    }

    [Test]
    public void GdyNieZnanyOperator()
    {
      TemplateWorkerParametry operatorek = new TemplateWorkerParametry(null)
      {
        WartoscA = 4,
        WartoscB = 2,
        Operacja = "k"
      };

      var msg = Assert.Throws<InvalidOperationException>(() => Liczymy.ObliczWartosc(operatorek));
      Assert.AreEqual("Nierozpoznana operacja", msg.Message);
    }
  }
}
