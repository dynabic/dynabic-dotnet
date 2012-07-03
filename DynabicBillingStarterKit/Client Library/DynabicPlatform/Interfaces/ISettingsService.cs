using DynabicPlatform.RestApiDataContract;

namespace DynabicPlatform.RestAPI.RestInterfaces
{
    public interface ISettingsService
    {
        #region GET

        /// <summary>
        /// Retrieves all set Settings for a Site
        /// </summary>
        /// <param name="siteSubdomain">The subdomain of the Site for which Settings are set. </param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns>A SettingsList object containing all Settings for a Site</returns>
        SettingsList GetSettings(string siteSubdomain, string format = "xml");

        /// <summary>
        /// Retrieves a Setting for a Site based on the Setting.Id
        /// </summary>
        /// <param name="siteSubdomain">The subdomain of the Site for which Settings are set. </param>
        /// <param name="settingId">Setting's unique identifier</param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns>A SettingResponse object containing the requested Setting</returns>
        SettingResponse GetSettingById(string siteSubdomain, string settingId, string format = "xml");

        /// <summary>
        /// Retrieves a Setting for a Site based on the Setting.Id
        /// </summary>
        /// <param name="siteSubdomain">The subdomain of the Site for which Settings are set. </param>
        /// <param name="settingName">Setting's name</param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns>A SettingResponse object containing the requested Setting</returns>
        SettingResponse GetSettingByName(string siteSubdomain, string settingName, string format = "xml");

        #endregion

        #region PUT

        /// <summary>
        /// Updates a Setting for a Site.
        /// </summary>
        /// <param name="siteSubdomain">The subdomain of the Site for which Setting is set. </param>
        /// <param name="settingId">Setting's unique identifier to update</param>
        /// <param name="settingRequest">SettingResquest object containing information about the Setting to update</param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns> A SettingResponse object containing information about the updated Setting.</returns>
        SettingResponse UpdateSetting(string siteSubdomain, string settingId, SettingRequest settingRequest, string format = "xml");

        /// <summary>
        /// Updates a Setting for a Site.
        /// </summary>
        /// <param name="siteSubdomain">The subdomain of the Site for which Setting is set. </param>
        /// <param name="settingId">Setting's unique identifier to update</param>
        /// <param name="settingName">Setting's name</param>
        /// <param name="settingValue">Setting's values</param>
        /// <param name="settingDescription">(Optional).A memo text describing the Setting's purpouse.</param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns> A SettingResponse object containing information about the updated Setting.</returns>
        SettingResponse UpdateSettingWithExplicitParameters(string siteSubdomain, string settingId, string settingName, string settingValue, string settingDescription, string format = "xml");

        /// <summary>
        /// Updates a Setting for a Site.
        /// </summary>
        /// <param name="siteSubdomain">The subdomain of the Site for which Setting is set. </param>
        /// <param name="settingId">Setting's unique identifier to update</param>
        /// <param name="settingValue">Setting's values</param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns> A SettingResponse object containing information about the updated Setting.</returns>        
        SettingResponse UpdateSettingWithExplicitParameters2(string siteSubdomain, string settingId, string settingValue, string format = "xml");

        #endregion

        #region Default Settings

        #region GET

        /// <summary>
        /// Retrieves a Default Setting using Setting's name
        /// </summary>
        /// <param name="settingName">Name of the Setting</param>
        /// <param name="format">The format used for the data transfer (XML or JSON). </param>
        /// <returns>A SettingResponse object containing the requested Setting</returns>
        SettingResponse GetDefaultSetting(string settingName, string format = "xml");

        #endregion

        #endregion
    }
}
