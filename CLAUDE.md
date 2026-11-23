# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

DSComponents — DSGridView, a fast data grid for iOS and Android, plus its shared libraries. Five NuGet packages:

- `DSoft.Datatypes` (`DSoft.Datatypes/`), `DSoft.Datatypes.Grid` (`DSoft.Datatypes.Grid/`) and `DSoft.Themes` (`DSoft.Themes/`) — shared data types and theming, `netstandard2.0`.
- `DSoft.DSGrid` (`DSoft.UI.iOS/`, assembly `DSoft.UI`) — the iOS grid, `net10.0-ios;net11.0-ios`.
- `DSoft.DSGrid.Android` (`DSoft.UI.Android/`) — the Android grid, `net10.0-android;net11.0-android`.

Samples: `SampleiOS` and `SampleAndroid` (same target frameworks). `DSoft.UI.Mac`, `DSoft.UI.Calendar`, `DSoft.Datatypes.Calendar` and `DSComponentsSampleMac` are old non-SDK Xamarin projects that are not in `DSComponents.sln` and are not built or released; leave them alone.

There are no automated tests.

## Build

```bash
dotnet workload restore DSComponents.sln     # android and ios workloads
dotnet restore DSComponents.sln
dotnet build DSComponents.sln -c Release
```

- Build with the .NET 10 and .NET 11 SDKs (`global.json` rolls forward to the latest major). The three `netstandard2.0` libraries build anywhere. The iOS projects need the iOS workload (and Xcode on a Mac for a full build); the Android projects need the Android workload. On a host without the workloads, build the libraries individually, for example `dotnet build DSoft.Datatypes/DSoft.Datatypes.csproj -c Release`.
- `net11.0-android` needs a minimum Android API of 24 (`SampleAndroid` sets it per target framework).
- `GeneratePackageOnBuild=true` (`Directory.Build.props`), so every Release build produces `.nupkg` files with `README.md` and `DSIcon.png`. Release builds add SourceLink and an embedded PDB.
- Assemblies are strong-name signed with each project's `DSoft.snk`; do not remove those files. Versions are not set in the csproj files: the release workflow injects them.

## Branching

- `development` is the working branch: branch from it and open pull requests into it. Never commit or open a pull request directly against `main`.
- `main` is the release branch. Every push to `main` runs `release.yml`, which publishes the packages to nuget.org and creates a GitHub release, so only merge `development` into `main` when a release is intended, and only when asked to release.

## CI

GitHub Actions (`.github/workflows/`) on `windows-latest`, with the workloads restored by `dotnet workload restore`:

- `ci.yml` builds Release for pull requests into `main` and `development`; it publishes nothing.
- `release.yml` runs on every push to `main` (changes only to Markdown or workflow files are skipped; run it by hand to test a workflow change). It builds Release as `3.2.<yyMM>.<run number>` plus `RELEASE_SUFFIX`, which is empty so releases are stable (set it to `-prerelease` for a prerelease), pushes all five packages to nuget.org with Trusted Publishing (OIDC, `NUGET_USER` secret, `nuget` environment), then tags the commit `v<version>` and creates a GitHub release (marked prerelease only while the suffix is set) with the packages attached.
