using Digitalroot.OdinPlusModUploader.Interfaces;
using System;

namespace Digitalroot.OdinPlusModUploader.Configuration;

internal class EnvironmentConfigurationProvider : IConfigs
{
  #region Implementation of IConfigs

  /// <inheritdoc />
  string IConfigs.GetConfig(string keyName)
  {
    return Environment.GetEnvironmentVariable(keyName);
  }

  #endregion
}
