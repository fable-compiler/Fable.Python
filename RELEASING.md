# Releasing

This project uses [EasyBuild.ShipIt](https://github.com/easybuild-org/EasyBuild.ShipIt)
for release automation and [Conventional Commits](https://www.conventionalcommits.org/)
for versioning.

## Commit conventions

PR titles must follow the conventional commit format (enforced by CI):

| Prefix | Version bump | Example |
| --- | --- | --- |
| `feat:` | minor | `feat: add Pydantic field validators` |
| `fix:` | patch | `fix: correct FastAPI response type` |
| `feat!:` | major | `feat!: rename Flask decorator` |
| `chore:` | patch | `chore: update dependencies` |
| `docs:` | patch | `docs: update README` |
| `refactor:` | patch | `refactor: simplify JSON bindings` |

Other valid prefixes: `test`, `perf`, `ci`, `build`, `style`, `revert`.

## Creating a release

Releases are driven automatically by the `Publish NuGet` workflow on pushes to
`main`. ShipIt opens a `chore: release ...` PR that bumps the version in
`CHANGELOG.md`; merging that PR triggers the publish job, which packs and pushes
the NuGet package.

To run ShipIt locally (for example to preview the next version or cut a release
manually):

```bash
just shipit
```

This will:

1. Analyze commits since the last release
2. Determine the next semantic version
3. Update `CHANGELOG.md`
4. Create a GitHub release with the version tag (e.g. `v5.0.0-rc.3`)

Merging a ShipIt release PR (or publishing a release tag) triggers the workflow
to:

1. Pack the NuGet package (`Fable.Python`) using the version from `CHANGELOG.md`
2. Push it to nuget.org using the `NUGET_API_KEY` secret

## Prerequisites

- `NUGET_API_KEY` repository secret (glob pattern: `Fable.Python*`)
- `GITHUB_TOKEN` or `gh` CLI authenticated (for ShipIt to create releases)
