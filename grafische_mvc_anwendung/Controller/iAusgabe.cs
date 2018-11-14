using System;
using KRTool.Model;

namespace KRTool.Controller
{
  public  interface iAusgabe
    {
        void SetController(AusgabeController controller);
             void AddToAusgabe(String txt);
    }
}
