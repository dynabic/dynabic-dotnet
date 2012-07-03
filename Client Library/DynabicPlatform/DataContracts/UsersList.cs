using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    /// <summary>
    /// A list of DynabicUsers represented as a collection of UserResponse objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "users")]
    public class UsersList : Collection<UserResponse>
    {
    }
}