using DynabicPlatform.Classes;
using DynabicPlatform.RestAPI.RestInterfaces;
using DynabicPlatform.RestApiDataContract;

namespace DynabicPlatform.Services
{
    public class SettingService : ISettingsService
    {
        private readonly CommunicationLayer _service;
        private readonly string _gatewayURL;

        public SettingService(CommunicationLayer service)
        {
            _service = service;
            _gatewayURL = service.Environment.GatewayURL + "/settings";
        }

        /// <summary>
        /// Gets a list of Settings for a Site
        /// </summary>
        /// <param name="siteSubdomain">Site's subdomain</param>
        /// <param name="format">Format of the Response</param>
        /// <returns></returns>
        public SettingsList GetSettings(string siteSubdomain, string format = ContentFormat.XML)
        {
            return _service.Get<SettingsList>(string.Format("{0}/{1}.{2}", _gatewayURL, siteSubdomain, format));
        }

        /// <summary>
        /// Gets a Setting for a Site based on the Setting.Id
        /// </summary>
        /// <param name="siteSubdomain">Site's subdomain</param>
        /// <param name="settingId">ID of the Setting as Int32 Value</param>
        /// <param name="format">Format of the Response</param>
        /// <returns></returns>
        public SettingResponse GetSettingById(string siteSubdomain, string settingId, string format = ContentFormat.XML)
        {
            return _service.Get<SettingResponse>(string.Format("{0}/{1}/{2}.{3}", _gatewayURL, siteSubdomain, settingId, format));
        }

        /// <summary>
        /// Gets a Setting for a Site based on the Setting.Name
        /// </summary>
        /// <param name="siteSubdomain">Site's subdomain</param>
        /// <param name="settingName">Name of the Setting</param>
        /// <param name="format">Format of the Response</param>
        /// <returns></returns>
        public SettingResponse GetSettingByName(string siteSubdomain, string settingName, string format = ContentFormat.XML)
        {
            return _service.Get<SettingResponse>(string.Format("{0}/{1}/name={2}.{3}", _gatewayURL, siteSubdomain, settingName, format));
        }

        /// <summary>
        /// Updates the setting.
        /// </summary>
        /// <param name="siteSubdomain">The site subdomain.</param>
        /// <param name="settingId">The setting id.</param>
        /// <param name="settingRequest">The setting request.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public SettingResponse UpdateSetting(string siteSubdomain, string settingId, SettingRequest settingRequest, string format = ContentFormat.XML)
        {
            return _service.Put<SettingRequest, SettingResponse>(string.Format("{0}/{1}/{2}.{3}", _gatewayURL, siteSubdomain, settingId, format), settingRequest);
        }

        /// <summary>
        /// Updates the setting with explicit parameters.
        /// </summary>
        /// <param name="siteSubdomain">The site subdomain.</param>
        /// <param name="settingId">The setting id.</param>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="settingValue">The setting value.</param>
        /// <param name="settingDescription">The setting description.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public SettingResponse UpdateSettingWithExplicitParameters(string siteSubdomain, string settingId, string settingName, string settingValue, string settingDescription, string format = ContentFormat.XML)
        {
            return _service.Put<SettingResponse>(string.Format("{0}/{1}/id={2}/name={3}/value={4}/description={5}.{6}",
                _gatewayURL, siteSubdomain, settingId, settingName, settingValue, settingDescription, format));
        }

        /// <summary>
        /// Updates the setting with explicit parameters2.
        /// </summary>
        /// <param name="siteSubdomain">The site subdomain.</param>
        /// <param name="settingId">The setting id.</param>
        /// <param name="settingValue">The setting value.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public SettingResponse UpdateSettingWithExplicitParameters2(string siteSubdomain, string settingId, string settingValue, string format = ContentFormat.XML)
        {
            return _service.Put<SettingResponse>(string.Format("{0}/{1}/id={2}/value={3}.{4}", _gatewayURL, siteSubdomain, settingId, settingValue, format));
        }

        /// <summary>
        /// Gets the default settings.
        /// </summary>
        /// <param name="settingName">Name of the setting.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public SettingResponse GetDefaultSetting(string settingName, string format = ContentFormat.XML)
        {
            return _service.Get<SettingResponse>(string.Format("{0}/default/{1}.{2}", _gatewayURL, settingName, format));
        }
    }
}
