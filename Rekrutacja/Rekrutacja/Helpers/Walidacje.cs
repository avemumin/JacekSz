using System;
using System.Linq;

namespace Rekrutacja.Helpers
{
  /// <summary>
  /// Klasa na potrzeby walidacyjne np pól i wartości które przechowują
  /// </summary>
  public class Walidacje
  {
    /// <summary>
    /// Tablica dozowolonych operatorów
    /// </summary>
    private static readonly string[] dozwolone = { "+", "-", "*", "/" };

    /// <summary>
    /// Metoda sprawdzająca czy w polu operator nie wkradło się coś niedozwolonego.
    /// </summary>
    /// <param name="op">Wartość którą sprawdzamy.</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static bool SprawdzOperator(string op)
    {
      if (string.IsNullOrEmpty(op) || !dozwolone.Contains(op))
        throw new Exception($"Dozwolone symbole operacji to + - * /");

      return true;
    }


  }
}
