using RestSharp;

namespace Digitalroot.OdinPlusModUploader.Protocol;

public abstract class AbstractRequest : RestRequest
{
  protected AbstractRequest(string resource, Method method)
    : base(resource, method) { }
}
