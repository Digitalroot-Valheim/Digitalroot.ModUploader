using Digitalroot.ModUploader.Provider.NexusMods.Models;
using RestSharp;
using System;

namespace Digitalroot.ModUploader.Provider.NexusMods.Protocol;

internal class AddFileToModRequest : AbstractAuthorizedRequest
{
  public AddFileToModRequest(AddFileToModRequestModel addFileToModRequestModel)
    : base(addFileToModRequestModel, "Core/Libs/Common/Managers/Mods?AddFile", Method.Post)
  {
    this.AddParameter("game_id", addFileToModRequestModel.GameId);
    this.AddParameter("name", addFileToModRequestModel.Name);
    this.AddParameter("file-version", addFileToModRequestModel.FileVersion);
    this.AddParameter("update-version", Convert.ToInt32(addFileToModRequestModel.UpdateVersion));
    this.AddParameter("category", Convert.ToInt32(addFileToModRequestModel.Category));
    this.AddParameter("brief-overview", addFileToModRequestModel.BriefOverview); // None? Can be empty
    this.AddParameter("set_as_main_nmm", Convert.ToInt32(addFileToModRequestModel.SetAsMainNmm));
    this.AddParameter("file_uuid", addFileToModRequestModel.FileUUID);
    this.AddParameter("file_size", addFileToModRequestModel.FileSize.ToString());
    this.AddParameter("mod_id", addFileToModRequestModel.ModID.ToString());
    this.AddParameter("id", addFileToModRequestModel.ID.ToString());
    this.AddParameter("action", addFileToModRequestModel.Action);
    this.AddParameter("uploaded_file", addFileToModRequestModel.UploadedFile);
    this.AddParameter("original_file", addFileToModRequestModel.OriginalFile);
    this.AddParameter("requirements_pop_up", Convert.ToInt32(addFileToModRequestModel.RequirementPopUp));
    this.AddParameter("remove_nmm_button", Convert.ToInt32(addFileToModRequestModel.RemoveNmmButton));
    if (addFileToModRequestModel.UpdateVersion)
    {
      this.AddParameter("new-existing", Convert.ToInt32(addFileToModRequestModel.NewExisting));
      this.AddParameter("remove-old-version", Convert.ToInt32(addFileToModRequestModel.RemoveOldVersion));
      if (addFileToModRequestModel.OldFileId.HasValue)
      {
        this.AddParameter("old_file_id", addFileToModRequestModel.OldFileId.Value); // 7515
      }
    }
    
  }
}
