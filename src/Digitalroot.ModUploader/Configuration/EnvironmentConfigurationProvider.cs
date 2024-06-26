﻿using Digitalroot.ModUploader.Interfaces;
using System;

namespace Digitalroot.ModUploader.Configuration;

internal class EnvironmentConfigurationProvider : IConfigs
{
  #region Implementation of IConfigs

  /// <inheritdoc />
  string IConfigs.GetConfig(string keyName)
  {
    var value = Environment.GetEnvironmentVariable(keyName);
    return value;
  }

  #endregion
}
