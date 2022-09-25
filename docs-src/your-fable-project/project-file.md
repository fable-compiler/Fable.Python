# The project file

Unlike Python, F# projects require all sources to be listed **in compilation order** in an `.fsproj` file. This may look
quite restrictive at first, but it does have [some
advantages](https://fsharpforfunandprofit.com/posts/cyclic-dependencies/). Since an F# project takes its roots from the
.NET ecosystem, we need to follow a few obligatory steps in order to add a file to an F# project.

:::{note}
Many F# IDEs already provide commands to perform operations like creating a project or adding/removing a file. The steps 
below are only necessary if you want to do this manually.
:::

<ul class="textual-steps">
<li>

**Create an .fsproj file**

`.fsproj` files are in XML format. This may look a bit old-fashioned, but luckily the basic schema for F# projects has become much simpler in recent versions. Now the skeleton for most projects looks like this:

```xml
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <TargetFramework>netstandard2.0</TargetFramework>
    </PropertyGroup>
    <ItemGroup>
        <!-- List your source files here -->
    </ItemGroup>
    <ItemGroup>
        <!-- List your package references here if you don't use Paket -->
    </ItemGroup>
</Project>
```

</li>

<li>

**Add an .fs file to the project**

F# source files end with the `.fs` extension. To include a new one in the project, you only need to add it to the `.fsproj` using a relative path with the following tag:

```xml
<ItemGroup>
    <Compile Include="path/to/my/File.fs" />
<ItemGroup>
```

For example, if we have have an app with two files, named `MyAwesomeFeature.fs` and `App.fs` (the last one contains the entry point), it will look like this

```xml
<ItemGroup>
    <Compile Include="MyAwesomeFeature.fs" />
    <Compile Include="App.fs" />
<ItemGroup>
```

:::{note}
Please be aware that in F#, **file order is important.** For instance, if `App.fs` calls `MyAwesomeFeature.fs`, then you must place `MyAwesomeFeature.fs` above `App.fs`.
:::

Let's add another file, `Authentication.fs`, located in another folder `Shared` which is at the same depth as our `src` folder (this can happen, for example, if the file is shared with another project, like a server). Let's see the current state of our project tree:

```
myproject
    |_ src
        |_ MyAwesomeFeature.fs
        |_ App.fs
        |_ App.fsproj
    |_ Shared
        |_ Authentication.fs
```

This can be expressed in the project file as:

```xml
<ItemGroup>
    <Compile Include="../Shared/Authentication.fs" />
    <Compile Include="MyAwesomeFeature.fs" />
    <Compile Include="App.fs" />
<ItemGroup>
```

</li>
</ul>

An important thing to note is Fable will translate F# source files to [ES2015 modules](https://exploringjs.com/es6/ch_modules.html) using JS `import`. This means that files which are not referenced by any other **will be ignored** (except the last file) including side effects. This is different behavior from .NET, where all files are compiled and executed, regardless of their relationship with the rest of the project.
