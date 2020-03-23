using System;
using System.Collections.Generic;
using System.Text;

namespace Worker_7ERFAcraft.Models
{
    public class PrivacyPolicyModel : wsBase
    {
        public TermsData privacyPolicyData { get; set; }
    }

    public class PrivacyPolicyData
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
