# Digitalroot Mod Uploader

## Introduction

This is a command-line tool that can be used to upload mod files to an existing mod on Nexus Mods.

## Installation

You can either download the binary for your platform from the Releases section on the right, or install it using the `dotnet` CLI:

```bash
dotnet tool install -g Digitalroot.ModUploader
# then run with
drmu
```

## Configuration and Usage

While you can pass all values at the command line. 
Please use environment variables for the sensitive information. 

> You have been warned!

### Environment Variables
- __NEXUSMOD_API_KEY__ 
- __NEXUSMOD_SESSION_COOKIE__

All Commands support the `-?, -h, --help` options to show help and usage information

### Commands:

###### Digitalroot.ModUploader.exe -h
```bat
Description:
  Uploads mods to NexusMods

Usage:
  Digitalroot.ModUploader.exe [command] [options]

Options:
  --version       Show version information
  -?, -h, --help  Show help and usage information

Commands:
  nexusmods, nx     nexusmods.com commands.
```
---

###### Digitalroot.ModUploader.exe nx -h
```bat
Description:
  nexusmods.com commands.

Usage:
  Digitalroot.ModUploader.exe nexusmods [command] [options]

Options:
  -?, -h, --help  Show help and usage information

Commands:
  check                           Check that an API Key and Cookie are valid.
  upload <mod-id> <archive-file>  Upload a file of 20.0GB or less to nexusmods.com
```
---

###### Digitalroot.ModUploader.exe check -h
```bat
Description:
  Check that an API Key and Cookie are valid.

Usage:
  Digitalroot.ModUploader.exe nexusmods check [options]

Options:
  -k, --key <key>                                               Api Key, ENV: NEXUSMOD_API_KEY
  -nmsc, --nexusmods_session_cookie <nexusmods_session_cookie_> Session Cookie, ENV: NEXUSMOD_SESSION_COOKIE
  -?, -h, --help                                                Show help and usage information
```
---

###### Digitalroot.ModUploader.exe upload -h
```
Description:
  Upload a file of 20.0GB or less to nexusmods.com

Usage:
  Digitalroot.ModUploader.exe nexusmods upload [<mod-id> <archive-file>] [options]

Arguments:
  <mod-id>        Nexus mod id.
  <archive-file>  File to upload.

Options:
  -f, --file-name <file-name> (REQUIRED)                               Name for the file on Nexus Mods
  -v, --version <version> (REQUIRED)                                   Version for your uploaded file.
  -t, --category <Archived|Main|Misc|Old|Optional|Updates> (REQUIRED)  Mod file category [default: Main]
  -d, --description <description>                                      description
  -g, --game <game>                                                    Game mod is for. [default: valheim]
  -dmfu, --disable-main-file-update                                    Skips replacing an existing file in the 'Main' category with the new one. [default: False]
  -dfa, --disable-file-archive                                         Skips archiving older versions of the existing file. [default: False]
  -ddwm, --disable-download-with-manager                               Removes the 'Download With Manager' button. [default: False]
  -dvu, --disable-version-update                                       Skips updating mod's main version to match this file's version. [default: False]
  -dmv, --disable-main-vortex                                          Skips setting file as the main Vortex file. [default: False]
  -drpu, --disable-requirements-pop-up                                 Skips informing downloaders of this mod's requirements before they attempt to download this file [default: False]
  -k, --key <key>                                                      Api Key, ENV: NEXUSMOD_API_KEY
  -nmsc, --nexusmods_session_cookie <nexusmods_session_cookie>         Session Cookie, ENV: NEXUSMOD_SESSION_COOKIE
  -?, -h, --help                                                       Show help and usage information
```

#### Examples
```bash
nexusmods check -k "MyVeryLongNexusApiKey" -nmsc "6c1ae4818867700000XX804f1f55ae72"
```
---
```bash
nexusmods upload 1303 Digitalroot.Valheim.JVL.BT.Fix.v1.0.6.zip -v 1.0.6 -f TestFile -t Main -ddwm -dmv -d "My Test File"
```
##### Note: Strings can also be wrapped in quotes. The following command is the same as the example above it.
```bash
nexusmods upload 1303 "Digitalroot.Valheim.JVL.BT.Fix.v1.0.6.zip" -v "1.0.6" -f "TestFile" -t Main -ddwm -dmv -d "My Test File"
```
---
## FAQ
### Nexus Mods
##### Q1: Where do I find out more info about the Nexus Mods' Public API?
> [Nexus Mods Public API](https://app.swaggerhub.com/apis-docs/NexusMods/nexus-mods_public_api_params_in_form_data/1.0)

##### Q2: Does Nexus Mods' API Rate Limit? 
> Yes, see. [Nexus Mods Public API](https://app.swaggerhub.com/apis-docs/NexusMods/nexus-mods_public_api_params_in_form_data/1.0)

##### Q3: Where do I get my Nexus Mods' API key? 
> [My Nexus account page](https://www.nexusmods.com/users/myaccount?tab=api%20access)

##### Q4: Where do I get my Nexus Mods' Session Cookies? 
> From your browser. This [site](https://www.cookieyes.com/how-to-check-cookies-on-your-website-manually/) covers how to do it in most browers.
> The cookie you are looking for is called `nexusmods_session`. It is a hex value that looks like `6c1ae4818867700000XX804f1f55ae72`.

 
<br />
<p align="center">
<b>Digitalroot can be found in the Valhalla Legends Discord</b><br /><br />
  <a href="https://discord.gg/SsMW3rm67u" target="_blank"><img src="https://digitalroot.net/img/vl/vl_logo_125x154.png"></a>
</p>
