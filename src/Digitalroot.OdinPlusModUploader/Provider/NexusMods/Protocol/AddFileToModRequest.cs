using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;
using RestSharp;
using System;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Protocol;

internal class AddFileToModRequest : AbstractAuthorizedRequest
{
  public AddFileToModRequest(AddFileToModRequestModel addFileToModRequestModel)
    : base(addFileToModRequestModel, "Core/Libs/Common/Managers/Mods?AddFile", Method.POST)
  {
    AddParameter("game_id", addFileToModRequestModel.GameId);
    AddParameter("name", addFileToModRequestModel.Name);
    AddParameter("file-version", addFileToModRequestModel.FileVersion);
    AddParameter("update-version", Convert.ToInt32(addFileToModRequestModel.UpdateVersion));
    AddParameter("category", Convert.ToInt32(addFileToModRequestModel.Category));
    AddParameter("brief-overview", addFileToModRequestModel.BriefOverview); // None? Can be empty
    AddParameter("set_as_main_nmm", Convert.ToInt32(addFileToModRequestModel.SetAsMainNmm));
    AddParameter("file_uuid", addFileToModRequestModel.FileUUID);
    AddParameter("file_size", addFileToModRequestModel.FileSize.ToString());
    AddParameter("mod_id", addFileToModRequestModel.ModID.ToString());
    AddParameter("id", addFileToModRequestModel.ID.ToString());
    AddParameter("action", addFileToModRequestModel.Action);
    AddParameter("uploaded_file", addFileToModRequestModel.UploadedFile);
    AddParameter("original_file", addFileToModRequestModel.OriginalFile);
    AddParameter("requirements_pop_up", Convert.ToInt32(addFileToModRequestModel.RequirementPopUp));
    AddParameter("remove_nmm_button", Convert.ToInt32(addFileToModRequestModel.RemoveNmmButton));
    if (addFileToModRequestModel.UpdateVersion)
    {
      AddParameter("new-existing", Convert.ToInt32(addFileToModRequestModel.NewExisting));
      AddParameter("remove-old-version", Convert.ToInt32(addFileToModRequestModel.RemoveOldVersion));
      if (addFileToModRequestModel.OldFileId != null) AddParameter("old_file_id", addFileToModRequestModel.OldFileId); // 7515
    }
    
  }
}
