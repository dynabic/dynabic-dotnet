using System;
using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    [DataContract(Namespace = "v1.0", Name = "application_in_company")]
    public class ApplicationInCompanyResponse
    {
        #region DataMembers

        /// <summary>
        /// Unique identifier of Application in Company. It is generated and managed by database.
        /// </summary>
        [DataMember(Name = "id", IsRequired = true)]
        public int Id { get; set; }

        /// <summary>
        /// Application's unique identifier
        /// </summary>
        [DataMember(Name = "application_id", IsRequired = true)]
        public int ApplicationId { get; set; }

        /// <summary>
        /// Date when the Application was added to the Company
        /// </summary>
        [DataMember(Name = "added", IsRequired = true)]
        public DateTime AddedOn { get; set; }

        /// <summary>
        /// Date when the Application was last time updated
        /// </summary>
        [DataMember(Name = "updated", IsRequired = true)]
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Shows if the Application is active
        /// </summary>
        [DataMember(Name = "active", IsRequired = true)]
        public bool IsActive { get; set; }

        #endregion

        public ApplicationInCompanyResponse()
        {
            this.AddedOn = DateTime.MinValue.ToUniversalTime();
            this.UpdatedOn = DateTime.MinValue.ToUniversalTime();
        }
    }
}
