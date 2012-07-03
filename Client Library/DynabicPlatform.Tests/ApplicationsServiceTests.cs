using DynabicPlatform.Classes;
using DynabicPlatform.Exceptions;
using DynabicPlatform.RestApiDataContract;
using NUnit.Framework;

namespace DynabicPlatform.Tests
{
    public class ApplicationsServiceTests : AssertionHelper
    {
        private PlatformGateway _gateway;

        [SetUp]
        public void Init()
        {
#if DEBUG
            _gateway = new PlatformGateway(PlatformEnvironment.DEVELOPMENT, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
#else
            _gateway = new PlatformGateway(PlatformEnvironment.QA, Constants.PUBLIC_KEY, Constants.PRIVATE_KEY);
#endif
        }

        #region Helpers

        private ApplicationsInCompanyList InternalGetCompanyApplications()
        {
            var applications = _gateway.Application.GetCompanyApplications();
            Assert.IsNotNull(applications);
            Assert.Greater(applications.Count, 0);
            return applications;
        }

        #endregion Helpers

        [Test]
        public void SetApplicationStatus()
        {
            var applications = InternalGetCompanyApplications();

            int appId = applications[0].ApplicationId;
            _gateway.Application.SetApplicationStatus(appId.ToString(), "false");
            _gateway.Application.SetApplicationStatus(appId.ToString(), applications[0].IsActive.ToString());
        }

        [Test]
        public void GetAllApplications()
        {
            var applications = _gateway.Application.GetAllApplications();
            Assert.IsNotNull(applications);
        }

        [Test]
        public void GetCompanyApplications()
        {
            InternalGetCompanyApplications();
        }

        [Test]
        public void GetCompanyApplicationById()
        {
            var compApplications = InternalGetCompanyApplications();

            int appId = compApplications[0].ApplicationId;
            var application = _gateway.Application.GetCompanyApplicationById(appId.ToString());
            Assert.IsNotNull(application);
            Assert.AreEqual(appId, application.ApplicationId);
        }

        [Test]
        public void GetCompanyApplicationByName()
        {
            var billingApp = _gateway.Application.GetCompanyApplicationByName("Billing");
            Assert.IsNotNull(billingApp);
        }

        [Test]
        public void GetApplicationCurrentPlan()
        {
            var compApplications = InternalGetCompanyApplications();

            foreach (var application in compApplications)
            {
                int appId = application.ApplicationId;
                try
                {
                    var plan = _gateway.Application.GetApplicationCurrentPlan(appId.ToString());
                    Assert.IsNotNull(plan);
                    break;
                }
                catch (NotFoundException)
                {
                }
            }
        }
    }
}
