using System;
using KRTool.Model;
using System.Threading;
using System.Threading.Tasks;

namespace KRTool.Controller
{
  public  interface iAusgabe
    {
        void SetController(AusgabeController controller);
             void AddToAusgabe(String txt);
    }
}
