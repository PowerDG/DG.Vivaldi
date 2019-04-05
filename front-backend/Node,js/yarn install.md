# [yarn install](https://yarnpkg.com/en/docs/cli/install)

`yarn install` is used to install all dependencies for a project. This is most commonly used when you have just checked out code for a project, or when another developer on the project has added a new dependency that you need to pick up.

If you are used to using npm you might be expecting to use `--save` or `--save-dev`. These have been replaced by `yarn add` and `yarn add --dev`. For more information, see [the `yarn add` documentation](https://yarnpkg.com/en/docs/cli/add).

Running `yarn` with no command will run `yarn install`, passing through any provided flags.

If you need reproducible dependencies, which is usually the case with the continuous integration systems, you should pass `--frozen-lockfile` flag.

##### `yarn install` 

Install all the dependencies listed within `package.json` in the local `node_modules` folder.

The `yarn.lock` file is utilized as follows:

- If `yarn.lock` is present and is enough to satisfy all the dependencies listed in `package.json`, the exact versions recorded in `yarn.lock` are installed, and `yarn.lock` will be unchanged. Yarn will not check for newer versions.
- If `yarn.lock` is absent, or is *not* enough to satisfy all the dependencies listed in `package.json` (for example, if you manually add a dependency to `package.json`), Yarn looks for the newest versions available that satisfy the constraints in `package.json`. The results are written to `yarn.lock`.

If you want to ensure `yarn.lock` is not updated, use `--frozen-lockfile`.

##### `yarn install --check-files` 

Verifies that already installed files in `node_modules` did not get removed.

##### `yarn install --flat` 

Install all the dependencies, but only allow one version for each package. On the first run this will prompt you to choose a single version for each package that is depended on at multiple version ranges. These will be added to your `package.json` under a `resolutions` field.

```
"resolutions": {
  "package-a": "2.0.0",
  "package-b": "5.0.0",
  "package-c": "1.5.2"
}
```

##### `yarn install --force` 

This refetches all packages, even ones that were previously installed.

##### `yarn install --har` 

Outputs an [HTTP archive](https://en.wikipedia.org/wiki/.har) from all the network requests performed during the installation. HAR files are commonly used to investigate network performance, and can be analyzed with tools such as [Google’s HAR Analyzer](https://toolbox.googleapps.com/apps/har_analyzer/) or [HAR Viewer](http://www.softwareishard.com/blog/har-viewer/).

##### `yarn install --ignore-scripts` 

Do not execute any scripts defined in the project package.json and its dependencies.

##### `yarn install --modules-folder <path>` 

Specifies an alternate location for the `node_modules` directory, instead of the default `./node_modules`.

##### `yarn install --no-lockfile` 

Don’t read or generate a `yarn.lock` lockfile.

##### `yarn install --production[=true|false]` 

Yarn will not install any package listed in `devDependencies` if the `NODE_ENV` environment variable is set to `production`. Use this flag to instruct Yarn to ignore `NODE_ENV` and take its production-or-not status from this flag instead.

> **Notes:** `--production` is the same as `--production=true`. `--prod` is an alias of `--production`.

##### `yarn install --pure-lockfile` 

Don’t generate a `yarn.lock` lockfile.

##### `yarn install --focus` 

Shallowly installs a package’s sibling workspace dependencies underneath its `node_modules` folder. This allows you to run that workspace without building the other workspaces it depends on.

Must be run inside an individual workspace in a workspaces project.  Can not be run in a non-workspaces project or at the root of a  workspaces project.

[Learn more about focused workspaces.](https://yarnpkg.com/blog/2018/05/18/focused-workspaces/)

##### `yarn install --frozen-lockfile` 

Don’t generate a `yarn.lock` lockfile and fail if an update is needed.

##### `yarn install --silent` 

Run yarn install without printing installation log.

##### `yarn install --ignore-engines` 

Ignore engines check.

##### `yarn install --ignore-optional` 

Don’t install optional dependencies.

##### `yarn install --offline` 

Run yarn install in offline mode.

##### `yarn install --non-interactive` 

Disable interactive prompts, like when there’s an invalid version of a dependency.

##### `yarn install --update-checksums` 

Update checksums in the `yarn.lock` lockfile if there’s a mismatch between them and their package’s checksum.

##### `yarn install --audit` 

Checks for known security issues with the installed packages. A count of found issues will be added to the output. Use the `yarn audit`  command for additional details. Unlike npm, which automatically runs an  audit on every install, yarn will only do so when requested. (This may  change in a later update as the feature is proven to be stable.)

##### `yarn install --no-bin-links` 

Prevent yarn from creating symlinks for any binaries the package might contain.