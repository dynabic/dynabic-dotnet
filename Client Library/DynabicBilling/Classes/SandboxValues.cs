#pragma warning disable 1591

using System;

namespace DynabicBilling
{
    /// <summary>
    /// Contains some test CC numbers accepted by gateways
    /// </summary>
    public class SandboxValues
    {
        /// <summary>
        /// Contains test values for Credit Card numbers accepted by different gateways
        /// </summary>
        public class CreditCardNumber
        {
            /// <summary>
            /// Contains test values for Credit Card numbers accepted by PayFlowPro gateway
            /// </summary>
            public static class PayFlowPro
            {
                public static readonly String AMERICAN_EXPRESS = "378282246310005";
                public static readonly String AMERICAN_EXPRESS_2 = "371449635398431";
                public static readonly String AMERICAN_EXPRESS_CORPORATE = "378734493671000";
                public static readonly String DINERS_CLUB = "30569309025904";
                public static readonly String DINERS_CLUB_2 = "38520000023237";
                public static readonly String DISCOVER = "6011111111111117";
                public static readonly String DISCOVER_2 = "6011000990139424";
                public static readonly String JCB = "3530111333300000";
                public static readonly String JCB_2 = "3566002020360505";
                public static readonly String MASTER_CARD = "5555555555554444";
                public static readonly String MASTER_CARD_2 = "5105105105105100";
                public static readonly String VISA = "4111111111111111";
                public static readonly String VISA_2 = "4012888888881881";
                public static readonly String VISA_3 = "422222222222";
            }

            /// <summary>
            /// Contains test values for Credit Card numbers accepted by Authorize.Net gateway
            /// </summary>
            public static class AuthorizeNet
            {
                public static readonly String AMERICAN_EXPRESS = "370000000000002";
                public static readonly String DISCOVER = "6011000000000012";
                public static readonly String VISA = "4007000000027";
                public static readonly String VISA_2 = "4012888818888";
                public static readonly String JCB = "3088000000000017";
                public static readonly String DINERS_CLUB = "38000000000006";
                public static readonly String CARTE_BLANCHE = "38000000000006";
                public static readonly String MASTER_CARD = "5424000000000015";
            }

            /// <summary>
            /// Contains test values for Credit Card numbers accepted by DynabicTest gateway
            /// </summary>
            public static class DynabicTest
            {
                public static readonly String AMERICAN_EXPRESS = "1000000000001111";
                public static readonly String CARTE_BLANCHE = "2000000000002222";
                public static readonly String DINERS_CLUB = "3000000000003333";
                public static readonly String DISCOVER = "4000000000004444";
                public static readonly String JCB = "5000000000005555";
                public static readonly String MASTER_CARD = "6000000000006666";
                public static readonly String VISA = "7000000000007777";                
            }

            /// <summary>
            /// Contains test values for Credit Card numbers accepted by Nab Transact gateway
            /// </summary>
            public static class NabTransactTest
            {
                public static readonly String VISA = "4012888888881881";
                //"4444333322221111"; this one seems not to pass the validation
            }
        }
    }
}
