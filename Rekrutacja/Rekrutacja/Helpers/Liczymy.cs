using System;
using static Rekrutacja.Workers.Template.TemplateWorker;

namespace Rekrutacja.Helpers
{
  /// <summary>
  /// Klasa z metodami obliczeń
  /// </summary>
  public class Liczymy
  {
    /// <summary>
    /// Metoda Oblicz wartość
    /// </summary>
    /// <param name="obj">Obiekt TemplateWorkerParametry.</param>
    /// <returns></returns>
    /// <exception cref="DivideByZeroException">Gdy chcemy dzielić przez 0.</exception>
    /// <exception cref="InvalidOperationException">Gdy nie zdefiniowany typ operacji.</exception>
    public static double ObliczWartosc(TemplateWorkerParametry obj)
    {
      switch (obj.Operacja)
      {
        case "+":
          return obj.WartoscA + obj.WartoscB;
        case "-":
          return obj.WartoscA - obj.WartoscB;
        case "*":
          return obj.WartoscA * obj.WartoscB;
        case "/":
          if (obj.WartoscB == 0.0) throw new DivideByZeroException();
          return obj.WartoscA / obj.WartoscB;
        default:
          throw new InvalidOperationException("Nierozpoznana operacja");
      }
    }
  }
}
