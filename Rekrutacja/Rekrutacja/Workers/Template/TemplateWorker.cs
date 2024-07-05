using System;
using Rekrutacja.Helpers;
using Rekrutacja.Workers.Template;
using Soneta.Business;
using Soneta.Kadry;
using Soneta.Tools;
using Soneta.Types;

//Rejetracja Workera - Pierwszy TypeOf określa jakiego typu ma być wyświetlany Worker, Drugi parametr wskazuje na jakim Typie obiektów będzie wyświetlany Worker
[assembly: Worker(typeof(TemplateWorker), typeof(Pracownicy))]
namespace Rekrutacja.Workers.Template
{
  public class TemplateWorker
  {
    //Aby parametry działały prawidłowo dziedziczymy po klasie ContextBase
    public class TemplateWorkerParametry : ContextBase
    {
      [Priority(1)]
      [Caption("A")]
      public double WartoscA { get; set; }

      [Priority(2)]
      [Caption("B")]
      public double WartoscB { get; set; }

      [Priority(3)]
      [Caption("Data obliczeń")]
      public Date DataObliczen { get; set; }

      [Priority(4)]
      [Caption("Operacja")]
      [MaxLength(1)]
      public string Operacja { get; set; }

      public TemplateWorkerParametry(Context context) : base(context)
      {
        this.DataObliczen = Date.Today;
        this.WartoscA = 0;
        this.WartoscB = 0;
      }

    }
    //Obiekt Context jest to pudełko które przechowuje Typy danych, aktualnie załadowane w aplikacji
    //Atrybut Context pobiera z "Contextu" obiekty które aktualnie widzimy na ekranie
    [Context]
    public Context Cx { get; set; }
    //Pobieramy z Contextu parametry, jeżeli nie ma w Context Parametrów mechanizm sam utworzy nowy obiekt oraz wyświetli jego formatkę
    [Context]
    public TemplateWorkerParametry Parametry { get; set; }
    //Atrybut Action - Wywołuje nam metodę która znajduje się poniżej
    [Action("Kalkulator",
       Description = "Prosty kalkulator ",
       Priority = 10,
       Mode = ActionMode.ReadOnlySession,
       Icon = ActionIcon.Accept,
       Target = ActionTarget.ToolbarWithText)]
    public void WykonajAkcje()
    {
      //Włączenie Debug, aby działał należy wygenerować DLL w trybie DEBUG
      DebuggerSession.MarkLineAsBreakPoint();
      //Pobieranie danych z Contextu

      //reprezentuje słownik zaznaczonych elementów na gridzie zrzutowanych na typ tablicy Pracownik[] celem iteracji
      var zaznaczeni = this.Cx[$"{nameof(Pracownik)}[]"] as Pracownik[];

      //może da się odklikać zaznaczenie nie wiem ale jak by nic w słowniku nie było to szkoda iść dalej
      if (zaznaczeni == null || !Walidacje.SprawdzOperator(this.Parametry.Operacja))
        return;

      var wynik = Liczymy.ObliczWartosc(this.Parametry);

      foreach (var p in zaznaczeni)
      {
        Pracownik pracownik = null;
        if (this.Cx.Contains(typeof(Pracownik[])))
        {
          pracownik = p;
        }

        //Modyfikacja danych
        //Aby modyfikować dane musimy mieć otwartą sesję, któa nie jest read only
        using (Session nowaSesja = this.Cx.Login.CreateSession(false, false, "ModyfikacjaPracownika"))
        {
          //Otwieramy Transaction aby można było edytować obiekt z sesji
          using (ITransaction trans = nowaSesja.Logout(true))
          {
            //Pobieramy obiekt z Nowo utworzonej sesji
            var pracownikZSesja = nowaSesja.Get(pracownik);
            //Features - są to pola rozszerzające obiekty w bazie danych, dzięki czemu nie jestesmy ogarniczeni to kolumn jakie zostały utworzone przez producenta
            pracownikZSesja.Features["DataObliczen"] = this.Parametry.DataObliczen;
            pracownikZSesja.Features["Wynik"] = wynik;
            //Zatwierdzamy zmiany wykonane w sesji
            trans.CommitUI();
          }
          //Zapisujemy zmiany
          nowaSesja.Save();
        }
      }
    }
  }
}