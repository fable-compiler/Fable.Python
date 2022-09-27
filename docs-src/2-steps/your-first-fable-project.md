# Start a new project

Now we're ready, let's start a new project using Fable!

## Use a Fable template

The easiest way to get started with Fable is to use a template (learn more about [dotnet
templates](https://docs.microsoft.com/en-us/dotnet/core/tools/custom-templates#installing-a-template)). For a minimal
Fable project, create and navigate to a new directory and run the following commands (first one is only needed if you
haven't installed the template yet in your system).

1. `dotnet new --install Fable.Template`
2. `dotnet new fable`

The rest of this document applies to Fable.Template. Alternatively, if you want specific examples or a more
comprehensive template with more tooling and libraries installed, please check one of the following:

- Clone the [Fable 3 samples](https://github.com/fable-compiler/fable3-samples) repository to learn how to use Fable
  with different kinds of apps (browser, nodejs, testing, etc)
- Build a [React](https://reactjs.org/) app in F# with [Feliz
  template](https://zaid-ajaj.github.io/Feliz/#/Feliz/ProjectTemplate)
- Write a frontend app fully in F# without JS dependencies with
  [Sutil](https://davedawkins.github.io/Sutil/#documentation-installation)
- Get up to speed with [SAFE Template](https://safe-stack.github.io/docs/quickstart/) which covers both the frontend and
  backend sides of your app

## Install dependencies

**Python dependencies** are listed in the `pyproject.toml` file. You can run `poetry install`, which will download the
needed packages to the `.venv` folder and create a lock file.

**.NET dependencies** are listed in the `src/App.fsproj` file. You can install them by running `dotnet restore src`, but
this is already automatically done by Fable.

:::{note}
Lock files (like `poetry.lock` if you're using poetry) should be committed to ensure reproducible builds whenever
anybody clones your repo. For .NET dependencies you can create a lock file by using [Paket](https://fsprojects.github.io/Paket/).
:::

## Build & run the app

Here we go. If you've already installed Python dependencies, just run `python app.py` (or `poetry run python app.py` if
you are using poetry). After a few seconds, your web server app will be running in the background until you kill it.

- You can access your project at [http://localhost:8080/](http://localhost:8080/) with your favorite browser.
- The server is in “watch” mode. Each time you save an `.fs` F# source file, Fable rebuilds the project automatically.
  If the build succeeds, you will see your changes in the browser without refreshing the page; if not, nothing changes
  in the browser, and you can see the build errors in the server’s console output.

:::{note} The `npm start` command is just an alias for `dotnet fable watch src --run webpack-dev-server`, check the
"scripts" section of the `package.json` file. Please also check the `README.md` file shipped with the template to get
up-to-date instructions. :::
