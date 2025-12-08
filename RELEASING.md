# Releasing Fable.Python

This document describes the release process for Fable.Python and how to keep versions in sync with Fable.

## Version Synchronization with Fable

Fable.Python versions should stay in sync with Fable. For example, if Fable releases `5.0.0-alpha.20`, Fable.Python should release `5.0.0-alpha.20`.

## Release Process

This project uses [release-please](https://github.com/googleapis/release-please) to automate releases. Release-please creates and maintains a release PR that updates automatically as commits are merged to main.

### Standard Release

When the Fable.Python version naturally aligns with Fable (no version override needed):

1. Merge the release-please PR
2. The GitHub Action will create a release and tag

### Forcing a Specific Version

When release-please calculates a different version than Fable (e.g., due to `feat:` commits bumping the minor version), you need to override it:

1. Add `release-as` to `release-please-config.json`:

```json
{
  "packages": {
    ".": {
      "release-type": "simple",
      "release-as": "5.0.0-alpha.20",
      ...
    }
  }
}
```

2. Update `.release-please-manifest.json` if needed:

```json
{
  ".": "5.0.0-alpha.19"
}
```

The manifest represents the *current* released version. Release-please uses this as the baseline.

3. Commit and push to main (or create a PR)

4. Release-please will update its PR to use the specified version

5. **Important**: After the release, remove `release-as` from the config so future releases calculate versions normally

### Hotfix Releases

If you need to release a fix after syncing with Fable (e.g., a bug found in `5.0.0-alpha.20`):

Use the format `5.0.0-alpha.20.1`, `5.0.0-alpha.20.2`, etc. This:

- Clearly shows the relationship to the base version
- Sorts correctly in package managers
- Resets when the next Fable version is released

## Configuration Files

- `release-please-config.json` - Release-please configuration (release type, version overrides, extra files to update)
- `.release-please-manifest.json` - Tracks the current released version

## Conventional Commits

Release-please uses [conventional commits](https://www.conventionalcommits.org/) to determine version bumps:

- `feat:` - Bumps minor version (in pre-major, due to `bump-minor-pre-major: true`)
- `fix:` - Bumps patch version
- `chore:`, `docs:`, `style:`, `refactor:`, `test:` - No version bump

When syncing with Fable, the automatic version calculation may not match Fable's version, which is why `release-as` is sometimes needed.
