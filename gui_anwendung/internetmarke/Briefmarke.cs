/*
* Quelle:
* https://www.deutschepost.de/de/i/internetmarke-porto-drucken/downloads.html
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Net;
using System.Security.Cryptography;
using internetmarke.webservice;

namespace internetmarke
{
    public class Briefmarke
    {

        [System.Web.Services.WebMethod]
        [System.Web.Services.Protocols.SoapHeader("markeHeader", Direction = System.Web.Services.Protocols.SoapHeaderDirection.InOut)]
        public string PostInternetmarke()
        {
            try
            {
                OneClickForAppPortTypeV3Client client = new OneClickForAppPortTypeV3Client();
                client.Endpoint.Behaviors.Add(new CustomBehavior());

                AuthenticateUserRequestType user = new AuthenticateUserRequestType()
                {
                    username = "XXXX",
                    password = "XXXXX"
                };

                client.Open();
                //den Token für 1x Tag gültig abrufen
                var userSettings = client.authenticateUser(user);

                var cart = new ShoppingCartPDFRequestType();
                cart.userToken = userSettings.userToken.ToString();
                cart.ppl = 32; //Aktuelle Version der CSV Produktpreisliste
                
                var position = new ShoppingCartPDFPosition();
                position.productCode = 1; //ProduktID Spalte 3 in CSV List
                position.imageID = 0;
                position.voucherLayout = VoucherLayout.FrankingZone;
                //cart.positions = new ShoppingCartPosition[] { position };
                
                cart.total = 62; //Preis aus CSV

                var marke = client.checkoutShoppingCartPDF(cart);
                String txt;
                using (WebClient w = new WebClient())
                {
                    var daten = w.DownloadData(marke.link);
                    var saveFile = AppDomain.CurrentDomain.BaseDirectory + "briefmarke.zip";
                    System.IO.File.WriteAllBytes(saveFile, daten);
                    txt = daten.ToString();
                }
                return txt;
            }
            catch (Exception ex)
            {
                return ex.Message + Environment.NewLine + ex.ToString();
            }
            finally
           {                                
            }
            
        }
    }//end class

}//end namespace
