using Digitalroot.ModUploader.Interfaces;

namespace Digitalroot.ModUploader.Configuration;

internal class FileConfigurationProvider : IConfigs
{
  #region Implementation of IConfigs

  /// <inheritdoc />
  string IConfigs.GetConfig(string keyName)
  {
    return null; // ToDo: Implement
  }

  #endregion
}
