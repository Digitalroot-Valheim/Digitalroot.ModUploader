// ReSharper disable MemberCanBePrivate.Global
namespace Digitalroot.ModUploader.Provider.NexusMods.Models;

// ReSharper disable once ClassNeverInstantiated.Global
internal class SaveDocumentationRequestModel : CookieRequestModel
{
  public readonly uint ModId;
  public readonly uint GameId;
  public readonly int InputMethod;
  public readonly string NewVersion;
  public readonly string NewChange;
  public readonly string Action;
  public readonly string ReadMeFile;

  internal SaveDocumentationRequestModel(string cookieNexusId
                                         , string cookiesid_develop
                                         , uint modId
                                         , uint gameId
                                         , string newVersion
                                         , string newChange
                                         , int inputMethod = 0
                                         , string action = "save"
                                         , string readMeFile = "")
    : base(cookieNexusId, cookiesid_develop)
  {
    ModId = modId;
    GameId = gameId;
    InputMethod = inputMethod;
    NewVersion = newVersion;
    NewChange = newChange;
    Action = action;
    ReadMeFile = readMeFile;
  }
}
