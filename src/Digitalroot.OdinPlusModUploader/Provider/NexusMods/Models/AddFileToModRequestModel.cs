using Digitalroot.OdinPlusModUploader.Provider.NexusMods.Enums;
using System;
using System.Text;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Models;

internal class AddFileToModRequestModel : CookieRequestModel
{
  public readonly int GameId;            // 3667+
  public readonly string Name;           // Upload Test+
  public readonly string FileVersion;    // 1.0.5+
  public readonly bool UpdateVersion;    // 1+
  public readonly CategoryName Category; // 1+
  public readonly bool NewExisting;      // 1
  public readonly uint? OldFileId;       // 7515
  public readonly bool RemoveOldVersion; // 1
  public readonly string BriefOverview;  // Your file description should go here.+
  public readonly bool RequirementPopUp; // 1 +
  // ReSharper disable once InconsistentNaming
  public readonly string FileUUID;     // bb5cf2cf-42ec-4877-95c8-65ab31391bc3+
  public readonly long FileSize;       // 14782+
  public readonly uint ModID;          // 1432+
  public readonly uint ID;             // 1432
  public readonly string Action;       // add +
  public readonly string UploadedFile; // 14782-727fdf8204465e77205adf507b9c4e89+
  public readonly string OriginalFile; // Digitalroot.Valheim.JVL.BT.Fix.v1.0.0.zip+
  public readonly bool SetAsMainNmm;   // 1 +
  public readonly bool RemoveNmmButton;  // 1 +

  /// <inheritdoc />
  internal AddFileToModRequestModel(string cookieNexusId
                                    , string cookiesid_develop
                                    , int gameId
                                    , string name
                                    , string fileVersion
                                    , bool updateVersion
                                    , CategoryName category
                                    , string briefOverview
                                    , bool setAsMainNmm
                                    , string fileUuid
                                    , long fileSize
                                    , uint modId
                                    , uint id
                                    , string action
                                    , string uploadedFile
                                    , string originalFile
                                    , bool requirementPopUp
                                    , uint? oldFileId
                                    , bool removeNmmButton
                                    , bool newExisting
                                    , bool removeOldVersion)
    : base(cookieNexusId, cookiesid_develop)
  {
    GameId = gameId;                     //
    Name = name;                         //
    FileVersion = fileVersion;           //
    UpdateVersion = updateVersion;       //
    Category = category;                 //
    BriefOverview = briefOverview;       //
    SetAsMainNmm = setAsMainNmm;         //
    FileUUID = fileUuid;                 //
    FileSize = fileSize;                 //
    ModID = modId;                       //
    ID = id;                             // 
    Action = action;                     //
    UploadedFile = uploadedFile;         //
    OriginalFile = originalFile;         //
    RequirementPopUp = requirementPopUp; //
    OldFileId = oldFileId;               //
    RemoveNmmButton = removeNmmButton;
    NewExisting = newExisting;
    RemoveOldVersion = removeOldVersion;
  }

  public override string ToString()
  {
    const string sep = " : ";
    StringBuilder sb = new("\n");
    sb.Append('\t').Append(nameof(GameId)).Append(sep).AppendLine(GameId.ToString());
    sb.Append('\t').Append(nameof(ModID)).Append(sep).AppendLine(ModID.ToString());
    sb.Append('\t').Append(nameof(OriginalFile)).Append(sep).AppendLine(OriginalFile);
    sb.Append('\t').Append(nameof(FileSize)).Append(sep).AppendLine(Utils.FileSizeFormatter.FormatSize(FileSize));
    sb.Append('\t').Append(nameof(FileVersion)).Append(sep).AppendLine(FileVersion);
    sb.Append('\t').Append(nameof(Name)).Append(sep).AppendLine(Name);
    sb.Append('\t').Append(nameof(BriefOverview)).Append(sep).AppendLine(BriefOverview);
    sb.Append('\t').Append(nameof(Action)).Append(sep).AppendLine(Action);
    sb.Append('\t').Append(nameof(FileUUID)).Append(sep).AppendLine(FileUUID);
    sb.Append('\t').Append(nameof(UploadedFile)).Append(sep).AppendLine(UploadedFile);
    sb.Append('\t').Append(nameof(UpdateVersion)).Append(sep).AppendLine(UpdateVersion.ToString());
    sb.Append('\t').Append(nameof(SetAsMainNmm)).Append(sep).AppendLine(SetAsMainNmm.ToString());
    sb.Append('\t').Append(nameof(RemoveNmmButton)).Append(sep).AppendLine(RemoveNmmButton.ToString());
    sb.Append('\t').Append(nameof(Category)).Append(sep).Append(Category.ToString()).Append(" (").Append(Convert.ToInt32(Category)).AppendLine(")");
    sb.Append('\t').Append(nameof(ID)).Append(sep).AppendLine(ID.ToString());
    sb.Append('\t').Append(nameof(RequirementPopUp)).Append(sep).AppendLine(RequirementPopUp.ToString());
    sb.Append('\t').Append(nameof(NewExisting)).Append(sep).AppendLine(NewExisting.ToString());
    sb.Append('\t').Append(nameof(RemoveOldVersion)).Append(sep).AppendLine(RemoveOldVersion.ToString());
    sb.Append('\t').Append(nameof(OldFileId)).Append(sep).AppendLine(OldFileId.ToString());

    return sb.ToString();
  }
}
