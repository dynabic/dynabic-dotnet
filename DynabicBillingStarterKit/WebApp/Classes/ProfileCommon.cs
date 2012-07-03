using System.Web.Profile;

namespace WebApp.Classes
{
    public class ProfileCommon : ProfileBase
    {
        public static ProfileCommon GetUserProfile(string userName)
        {
            return Create(userName) as ProfileCommon;
        }

        //the ProductId that current user has a Subscription for
        [SettingsAllowAnonymous(false)]
        public int CurrentPlanId
        {
            get { return (int)base["CurrentPlanId"]; }
            set { base["CurrentPlanId"] = value; Save(); }
        }

        //the User's CustomerReferenceId
        [SettingsAllowAnonymous(false)]
        public string CustomerReferenceId
        {
            get { return (string)base["CustomerReferenceId"]; }
            set { base["CustomerReferenceId"] = value; Save(); }
        }
    }
}