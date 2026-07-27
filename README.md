# VetCheckup
## NuGet publishing

A pull request into `main` runs **Validate pull request**. When that pull
request is merged, **Publish NuGet packages after merge** tests the solution,
packages only the affected library and its dependants, and publishes them to
the configured NuGet feed. Direct pushes do not run either workflow.

Before the first release, create a GitHub Environment named `nuget` and add
these environment secrets:

- `NUGET_SOURCE` — the v3 endpoint for the feed (for NuGet.org:
  `https://api.nuget.org/v3/index.json`)
- `NUGET_API_KEY` — a push-capable API key for that feed

The workflow creates a unique patch version from the package baseline plus the
Git commit height. With the current `1.0.3` baseline, it produces versions such
as `1.0.127`; rerunning a workflow for the same commit is safe. To begin a new
major or minor release line, change the `PackageVersion` baseline in all three
library project files before merging (for example, to `1.1.0`).
