using Digitalroot.OdinPlusModUploader.Configuration;
using Digitalroot.OdinPlusModUploader.Enums;
using System;

namespace Digitalroot.OdinPlusModUploader.Provider.NexusMods.Configuration
{
  internal class NexusModsHostProviderConfiguration : AbstractHostProviderConfiguration
  {
    public string Game { get; set; }
    public int ModId { get; set; }
    public string FileName { get; set; }
    public string FileDescription { get; set; }
    public string PreviousFile { get; set; }
    public string Cookies { get; set; }

    /// <summary>
    /// Api Service URI
    /// </summary>
    protected internal Uri ServiceApiUri => new($"https://api.{BaseUrl}");

    /// <summary>
    /// Upload Service URI
    /// </summary>
    protected internal Uri ServiceUploadUri => new($"https://upload.{BaseUrl}");

    internal NexusModsHostProviderConfiguration()
      : base(ConfigurationProviderFactory.CreateInstance(ConfigurationProvider.Environment)
             , ConfigurationProviderFactory.CreateInstance(ConfigurationProvider.File))
    {
      BaseUrl = "nexusmods.com";
      ServiceUri = new($"https://www.{BaseUrl}");
    }
  }
}
