using Digitalroot.ModUploader.Enums;
using Pastel;
using System;

namespace Digitalroot.ModUploader.Common;

/// <summary>
/// Source: https://github.com/Digitalroot/digitalroot-valheim-mods/blob/main/Digitalroot.Valheim.Common/Core/Singleton.cs
/// Permission granted to OdinPlus under 
/// License: "GNU Affero General Public License v3.0"
/// License ref: https://github.com/Digitalroot/digitalroot-valheim-mods/blob/main/LICENSE
/// </summary>
/// <typeparam name="TSingletonSubClass"></typeparam>
public abstract class Singleton<TSingletonSubClass> where TSingletonSubClass : Singleton<TSingletonSubClass>, new()
{
  public static TSingletonSubClass Instance => Nested._instance;

  // ReSharper disable once ClassNeverInstantiated.Local
  private class Nested
  {
    static Nested()
    {
    }

    // ReSharper disable once InconsistentNaming
    internal static readonly TSingletonSubClass _instance = InstantiateInstance();

    private static TSingletonSubClass InstantiateInstance()
    {
      TSingletonSubClass instance;
      try
      {
        instance = new TSingletonSubClass();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Failed while initializing singleton of type: {typeof(TSingletonSubClass).FullName}: {ex.Message}".Pastel(ColorOptions.ErrorColor));
        Console.WriteLine(ex.StackTrace.Pastel(ColorOptions.ErrorColor));
        throw;
      }

      return instance;
    }
  }
}
