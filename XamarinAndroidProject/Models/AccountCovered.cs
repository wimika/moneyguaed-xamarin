using Java.Util;
using System.Collections.Generic;

namespace AndroidTestApp.Models
{
    public class AccountCovered
    {
        public List<PolicyDto> Policies { get; set; }       
    }

    public class PolicyDto
    {
        public int CoveredAccountId { get; set; }
    }
}