# Releasing Fable.Python

This document describes the release process for Fable.Python and how to keep versions in sync with Fable.

## Version Synchronization with Fable

Fable.Python versions should stay in sync with Fable. For example, if Fable releases `5.0.0-alpha.21`, Fable.Python should release `5.0.0-alpha.21.0`.

## Release Process

This project uses [release-please](https://github.com/googleapis/release-please) to automate releases. Release-please creates and maintains a release PR that updates automatically as commits are merged to main.

The configuration uses `"versioning": "always-bump-patch"` which means every release only increments the patch version (e.g., `5.0.0-alpha.21.0` → `5.0.0-alpha.21.1` → `5.0.0-alpha.21.2`).

### Standard Release (Patch Bump)

For normal bug fixes and improvements:

1. Merge PRs with conventional commit messages (`fix:`, `feat:`, `chore:`, etc.)
2. Release-please automatically creates/updates a release PR
3. Merge the release-please PR when ready
4. The GitHub Action will create a release and tag

The patch version increments automatically regardless of commit type.

### Syncing with a New Fable Version

When Fable releases a new version (e.g., `5.0.0-alpha.21`), use the `Release-As` footer to override the version:

#### In the commit message

```text
chore: sync with Fable 5.0.0-alpha.21

Release-As: 5.0.0-alpha.21.0
```

#### In the PR description

Add this line anywhere in the PR body:

```text
Release-As: 5.0.0-alpha.21.0
```

Release-please will use the specified version instead of bumping the patch.

### Hotfix Releases

After syncing with a Fable version, subsequent patches are automatic:

- `5.0.0-alpha.21.0` (initial sync with Fable)
- `5.0.0-alpha.21.1` (first hotfix)
- `5.0.0-alpha.21.2` (second hotfix)
- etc.

This clearly shows the relationship to the base Fable version and sorts correctly in package managers.

## Configuration Files

- `release-please-config.json` - Release-please configuration (release type, versioning strategy)
- `.release-please-manifest.json` - Tracks the current released version

## Conventional Commits

This project uses [conventional commits](https://www.conventionalcommits.org/):

- `feat:` - New features
- `fix:` - Bug fixes
- `chore:`, `docs:`, `style:`, `refactor:`, `test:` - Other changes

With `"versioning": "always-bump-patch"`, all commit types result in a patch bump only.
