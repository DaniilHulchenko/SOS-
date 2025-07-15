using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportSosGeneve
{
   public class Elauploadinfo
    {
       public string toOrganization  { get; set; }
       public bool toPatient { get; set; }
       public String documentReference { get; set; }
       public string correlationReference { get; set; }
       public string printLanguage { get; set; }
       public string postalDelivery { get; set; }
       public string toTrustCenter { get; set; }

       public void setParamettre(string to_Organisation, bool to_Patient, string document_Reference, string correlation_Reference, string print_Language, string postal_Delivery, string toTrust_Center)
       {
           toOrganization = to_Organisation;
           toPatient = to_Patient;
           documentReference = document_Reference;
           correlationReference = correlation_Reference;
           printLanguage = print_Language;
           postalDelivery = postal_Delivery;
           toTrustCenter = toTrust_Center;
       }

    }
}
