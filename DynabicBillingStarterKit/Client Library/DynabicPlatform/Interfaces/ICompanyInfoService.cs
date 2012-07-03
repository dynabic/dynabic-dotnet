using DynabicPlatform.RestApiDataContract;

namespace DynabicPlatform.RestAPI.RestInterfaces
{
    public interface ICompanyInfoService
    {
        #region GET

        /// <summary>
        /// Retrieves Company info by it's API key
        /// </summary>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        /// <returns>A CompanyInfoResponse object containing all information about a Company.</returns>
        CompanyInfoResponse GetCompanyInfo(string format = "xml");

        #endregion GET

        #region PUT

        /// <summary>
        /// Updates information for a Company
        /// </summary>
        /// <param name="updatedCompanyInfo">The CompanyInfoRequest object containing information to be updated.</param>
        /// <param name="format">The format used for the data transfer (XML or JSON).</param>
        /// <returns>A CompanyInfoResponse object that corresponds to the updated CompanyInfo</returns>
        CompanyInfoResponse UpdateCompanyInfo(CompanyInfoRequest updatedCompanyInfo, string format = "xml");

        #endregion PUT

        #region DELETE

        ///// <summary>
        ///// Deletes a CompanyInfo
        ///// </summary>
        //void DeleteCompanyInfo();

        #endregion
    }
}
