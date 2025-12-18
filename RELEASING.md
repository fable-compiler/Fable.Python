# Releasing Fable.Python

This document describes the release process for Fable.Python and how to keep versions in sync with Fable.

## Version Format

Fable.Python uses the version format `X.Y.Z-alpha.N.P` where:

- `X.Y.Z-alpha.N` matches the Fable version (e.g., `5.0.0-alpha.21`)
- `P` is the patch version for Fable.Python releases (0, 1, 2, etc.)

Example: `5.0.0-alpha.21.0`, `5.0.0-alpha.21.1`, `5.0.0-alpha.21.2`

## Release Process

This project uses [release-please](https://github.com/googleapis/release-please) to automate releases. Release-please creates and maintains a release PR that updates automatically as commits are merged to main.

**Important:** Due to the custom version format, use `Release-As:` to specify the version.

### Setting the Version

Add `Release-As:` in the commit message or PR description:

#### In the commit message

```text
feat: add new feature

Release-As: 5.0.0-alpha.21.1
```

#### In the PR description

Add this line anywhere in the PR body:

```text
Release-As: 5.0.0-alpha.21.1
```

Release-please will use the specified version for the release.

### Syncing with a New Fable Version

When Fable releases a new version (e.g., `5.0.0-alpha.22`):

1. Update the codebase to work with the new Fable version
2. Use `Release-As: 5.0.0-alpha.22.0` in the commit or PR
3. Merge the release-please PR

### Version History Example

- `5.0.0-alpha.21.0` (initial sync with Fable 5.0.0-alpha.21)
- `5.0.0-alpha.21.1` (first patch)
- `5.0.0-alpha.21.2` (second patch)
- `5.0.0-alpha.22.0` (sync with Fable 5.0.0-alpha.22)
- `5.0.0-alpha.22.1` (first patch for alpha.22)

## Configuration Files

- `release-please-config.json` - Release-please configuration (release type, tag format)
- `.release-please-manifest.json` - Tracks the current released version

## Conventional Commits

This project uses [conventional commits](https://www.conventionalcommits.org/):

- `feat:` - New features
- `fix:` - Bug fixes
- `chore:`, `docs:`, `style:`, `refactor:`, `test:` - Other changes
