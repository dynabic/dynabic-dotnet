using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    [DataContract(Namespace = "v1.0", Name = "user_api_keys")]
    public class UserApiKeysResponse
    {
        [DataMember(Name = "public_key")]
        public string PublicKey { get; set; }

        [DataMember(Name = "private_key")]
        public string PrivateKey { get; set; }
    }
}
