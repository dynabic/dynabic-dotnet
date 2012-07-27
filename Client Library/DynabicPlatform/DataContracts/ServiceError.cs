using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    [DataContract(Namespace = "v1.0", Name = "service_error")]
    public class ServiceError
    {
        [DataMember(Name = "description")]
        public string ErrorDescription { get; set; }
    }
}
