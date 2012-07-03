using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace DynabicPlatform.RestApiDataContract
{
    /// <summary>
    /// A list of Events represented as a collection of EventResponse objects
    /// </summary>
    [CollectionDataContract(Namespace = "v1.0", Name = "events")]
    public class EventsList : Collection<EventResponse>
    {
    }
}