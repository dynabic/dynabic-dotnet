using System.Runtime.Serialization;

namespace DynabicBilling.RestApiDataContract
{
    [DataContract(Namespace = "v1.0", Name = "service_result")]
    public class ServiceResult
    {
        [DataMember(Name = "result", IsRequired = false)]
        public string Result { get; set; }

        [DataMember(Name = "description", IsRequired = false)]
        public string Description { get; set; }
    }
}
