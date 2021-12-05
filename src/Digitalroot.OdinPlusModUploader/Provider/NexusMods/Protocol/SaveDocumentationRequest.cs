using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

internal class SaveDocumentationRequest : AbstractAuthorizedRequest
{
  public SaveDocumentationRequest(SaveDocumentationRequestModel saveDocumentationRequestModel)
    : base(saveDocumentationRequestModel, "Core/Libs/Common/Managers/Mods?SaveDocumentation", Method.POST)
  {
    AddParameter("game_id", saveDocumentationRequestModel.GameId);
    AddParameter("id", saveDocumentationRequestModel.ModId);
    AddParameter("input-method", saveDocumentationRequestModel.InputMethod);
    AddParameter("new-change[]", saveDocumentationRequestModel.NewChange);
    AddParameter("new-version[]", saveDocumentationRequestModel.NewVersion);
    AddParameter("action", saveDocumentationRequestModel.Action);
    AddParameter("readme-file", saveDocumentationRequestModel.ReadMeFile);
    
  }
}
