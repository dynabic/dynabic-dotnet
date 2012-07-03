using System;
using DynabicPlatform.Classes;
using NUnit.Framework;

namespace DynabicPlatform.Tests
{
    public class CompanyInfoServiceTests : AssertionHelper
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

        [Test]
        public void GetCompanyInfo()
        {
            var company = _gateway.CompanyInfo.GetCompanyInfo();
            Assert.IsNotNull(company);
        }

        [Test]
        public void UpdateCompanyInfo()
        {
            var company = _gateway.CompanyInfo.GetCompanyInfo();
            Assert.IsNotNull(company);
            company.Email += DateTime.Now.ToString();
            company.Name += DateTime.Now.ToString();
            company.Phone += DateTime.Now.ToString();

            var updatedCompany = _gateway.CompanyInfo.UpdateCompanyInfo(company);
            Assert.IsNotNull(updatedCompany);
            Assert.AreEqual(company.Name, updatedCompany.Name);

            // restore previous values
            company.Email = company.Email;
            company.Name = company.Name;
            company.Phone = company.Phone;
            updatedCompany = _gateway.CompanyInfo.UpdateCompanyInfo(company);
            Assert.IsNotNull(updatedCompany);
            Assert.AreEqual(company.Name, company.Name);
        }
    }
}
