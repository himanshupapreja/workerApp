using System;
using System.Collections.Generic;
using System.Text;

namespace Worker_7ERFAcraft.Models
{
    public class TermsConditionsModel:wsBase
    {
        public TermsData termsData { get; set; }
    }

    public class TermsData
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
