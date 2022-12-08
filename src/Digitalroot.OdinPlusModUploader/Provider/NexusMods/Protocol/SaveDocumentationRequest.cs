using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using RestSharp;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

internal class SaveDocumentationRequest : AbstractAuthorizedRequest
{
  public SaveDocumentationRequest(SaveDocumentationRequestModel saveDocumentationRequestModel)
    : base(saveDocumentationRequestModel, "Core/Libs/Common/Managers/Mods?SaveDocumentation", Method.Post)
  {
    this.AddParameter("game_id", saveDocumentationRequestModel.GameId);
    this.AddParameter("id", saveDocumentationRequestModel.ModId);
    this.AddParameter("input-method", saveDocumentationRequestModel.InputMethod);
    this.AddParameter("new-change[]", saveDocumentationRequestModel.NewChange);
    this.AddParameter("new-version[]", saveDocumentationRequestModel.NewVersion);
    this.AddParameter("action", saveDocumentationRequestModel.Action);
    this.AddParameter("readme-file", saveDocumentationRequestModel.ReadMeFile);
    
  }
}
