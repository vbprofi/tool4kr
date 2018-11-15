using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Security.Cryptography;

namespace internetmarke
{
    public class CustomMessageInspector : IDispatchMessageInspector, IClientMessageInspector
    {
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            MessageBuffer buffer = request.CreateBufferedCopy(Int32.MaxValue);
            request = buffer.CreateMessage();

            string PARTNER_ID = "XXX";
            string REQUEST_TIMESTAMP = DateTime.Now.ToString("DDMMYYYY-HHMMSS");
            string KEY_PHASE = "1";
            string SCHLUESSEL_DPWN_MEINMARKTPLATZ = "XXXX";
            string PARTNER_SIGNATURE = "";

            for (; ; )
            {
                string berechnung = PARTNER_ID + "::" + REQUEST_TIMESTAMP + "::" + KEY_PHASE + "::" + SCHLUESSEL_DPWN_MEINMARKTPLATZ;

                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] textToHash = Encoding.Default.GetBytes(berechnung);
                byte[] result = md5.ComputeHash(textToHash);

                string sresult = System.BitConverter.ToString(result).ToLower();

                if (sresult.Length == 47)
                {
                    PARTNER_SIGNATURE = sresult.Replace("-", string.Empty).Remove(8);
                    break;
                }
            }

            MessageHeader partner = MessageHeader.CreateHeader("PARTNER_ID", string.Empty, PARTNER_ID);
            MessageHeader requestTimestamp = MessageHeader.CreateHeader("REQUEST_TIMESTAMP", string.Empty, REQUEST_TIMESTAMP);
            MessageHeader keyPhase = MessageHeader.CreateHeader("KEY_PHASE", string.Empty, KEY_PHASE);
            MessageHeader partnerSignature = MessageHeader.CreateHeader("PARTNER_SIGNATURE", string.Empty, PARTNER_SIGNATURE);

            request.Headers.Add(partner);
            request.Headers.Add(requestTimestamp);
            request.Headers.Add(keyPhase);
            request.Headers.Add(partnerSignature);

            return null;
        }

    }//end class
}
