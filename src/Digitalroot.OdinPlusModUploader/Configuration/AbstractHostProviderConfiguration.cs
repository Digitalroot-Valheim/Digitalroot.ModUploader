using Digitalroot.OdinPlusModUploader.Interfaces;
using System;

namespace Digitalroot.OdinPlusModUploader.Configuration
{
  public class AbstractHostProviderConfiguration
  {
    /// <summary>
    /// Service URI
    /// </summary>
    protected internal Uri ServiceUri { get; init; }

    // ReSharper disable once MemberCanBeProtected.Global
    protected internal string BaseUrl { get; init; }

    /// <summary>
    /// Environment Configs Provider
    /// </summary>
    private readonly IConfigs _environmentConfigsProvider;

    /// <summary>
    /// File Configs Provider
    /// </summary>
    private readonly IConfigs _fileConfigsProvider;

    protected AbstractHostProviderConfiguration(IConfigs environmentConfigsProvider, IConfigs fileConfigsProvider)
    {
      _environmentConfigsProvider = environmentConfigsProvider ?? throw new ArgumentNullException(nameof(environmentConfigsProvider));
      _fileConfigsProvider        = fileConfigsProvider ?? throw new ArgumentNullException(nameof(fileConfigsProvider));
    }

    internal string GetDefaultConfigValue(string value)
    {
      return _environmentConfigsProvider.GetConfig(value)
             ?? _fileConfigsProvider.GetConfig(value);
    }
  }
}
