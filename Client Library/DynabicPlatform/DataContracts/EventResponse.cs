using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "event")]
    public class EventResponse
    {
        #region Data Members

        /// <summary>
        /// Event's unique identifier. It is generated and managed by database
        /// </summary>
        [DataMember(Name = "id")]
        public int Id { get; set; }

        /// <summary>
        /// Type of the Event
        /// </summary>
        [DataMember(Name = "event_type")]
        public string EventType { get; set; }

        /// <summary>
        /// Result of the Event
        /// </summary>
        [DataMember(Name = "event_result")]
        public string EventResult { get; set; }

        /// <summary>
        /// Date when the Event occured
        /// </summary>
        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Id of the Site in which the Event occured
        /// </summary>
        [DataMember(Name = "site_id")]
        public int SiteId { get; set; }

        /// <summary>
        /// A list of Event parameters
        /// </summary>
        [DataMember(Name = "parameters", IsRequired = false)]
        public EventParamList EventParams { get; set; }

        #endregion

        public EventResponse()
        {
            this.EventParams = new EventParamList();
            this.Date = DateTime.MinValue.ToUniversalTime();
        }
    }

    /// <summary>
    /// Event parameter
    /// </summary>
    [DataContract(Namespace = "v1.0", Name = "parameter")]
    public class EventParam
    {
        /// <summary>
        /// Id of the Event parameter
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Value of the Event parameter
        /// </summary>
        [DataMember(Name = "value")]
        public string Value { get; set; }
    }

    /// <summary>
    /// A list of Event parameters represented as a collection of EventParam objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "parameters")]
    public class EventParamList : Collection<EventParam>
    {
    }

}