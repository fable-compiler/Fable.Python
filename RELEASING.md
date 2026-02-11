# Releasing Fable.Python

## Version Format

Fable.Python uses the version format `X.Y.Z-alpha.N.P` where:

- `X.Y.Z-alpha.N` matches the Fable version (e.g., `5.0.0-alpha.22`)
- `P` is the patch version for Fable.Python releases (0, 1, 2, etc.)

Example: `5.0.0-alpha.22.0`, `5.0.0-alpha.22.1`, `5.0.0-alpha.22.2`

## Release Process

1. Go to [GitHub Releases](https://github.com/fable-compiler/Fable.Python/releases)
2. Click **"Draft a new release"**
3. Create a new tag in the format `v5.0.0-alpha.22.0` (with `v` prefix)
4. Set the release title (e.g., `5.0.0-alpha.22.0`)
5. Write release notes (or use "Generate release notes")
6. For pre-release versions, check **"Set as a pre-release"**
7. Click **"Publish release"**

The publish workflow will automatically build and push the NuGet package.

## Syncing with a New Fable Version

When Fable releases a new version (e.g., `5.0.0-alpha.23`):

1. Update the codebase to work with the new Fable version
2. Create a release with tag `v5.0.0-alpha.23.0`

## Version History Example

- `5.0.0-alpha.21.0` (initial sync with Fable 5.0.0-alpha.21)
- `5.0.0-alpha.21.1` (first patch)
- `5.0.0-alpha.21.2` (second patch)
- `5.0.0-alpha.22.0` (sync with Fable 5.0.0-alpha.22)
- `5.0.0-alpha.22.1` (first patch for alpha.22)
