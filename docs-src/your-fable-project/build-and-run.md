# Build & run

Your project is ready? Then it's time to build and run it.

This process, however, is different depending on whether we are still in development or we are already preparing the project to deploy it to production. In development we don't care so much about optimizations and we want fast builds so we can see the results of our changes on-screen almost immediately. While for production we can endure a slower build in order to get JS code that is more optimized.

Webpack can be a bit difficult to configure. Most of the [samples](https://github.com/fable-compiler/fable3-samples) have a `webpack.config.js` file that you can use for reference. And there's also a [webpack config template for Fable](https://github.com/fable-compiler/webpack-config-template/blob/master/webpack.config.js) that is prepared to work in most projects.

## Development

Even if we already have our own server, when developing our frontend we want a server that is capable of detecting changes in the code and load them without restarting the app! This is what [webpack-dev-server](https://github.com/webpack/webpack-dev-server) does, so normally we'll be running it together with Fable in watch mode, as in `dotnet fable watch src --run webpack serve src/App.fs.js --mode development`

> In most Fable projects, we use a `webpack.config.js` file to pass arguments to Webpack so you will only see the `webpack serve` command.

If you use [npm-scripts](https://docs.npmjs.com/misc/scripts) as shortcuts for common actions, it's customary to use `npm start` to run in development mode. Example of `package.json`:

```json
  "scripts": {
    "start": "dotnet fable watch src --run webpack serve"
  },
```

> If you also have your own server, you probably want to redirect API calls to it. You can use the [devServer.proxy](https://webpack.js.org/configuration/dev-server#devserverproxy) Webpack configuration option for that.

The webpack-dev-server will keep running until you kill the process, picking up the changes in your code after you save a file. If you use [Hot Module Replacement](https://elmish.github.io/hmr/) it will try to inject the changes without restarting the app. Otherwise it will just refresh the web page.

Note that webpack-dev-server serves the generated files from memory and doesn't actually write to disk. We will do that in the next step: building for production!

## Production

When preparing the files for deployment, we don't need a special server. We can call Webpack CLI directly in production mode to enable optimizations like [JS minification](https://webpack.js.org/configuration/optimization#optimizationminimize): `dotnet fable src --run webpack --mode production`.

> As before, in most configurations you don't need to set the `--mode` argument explicitly. And it's also customary to have a "build" (or sometimes "deploy") npm-script for this operation:

```json
  "scripts": {
    "start": "dotnet fable watch src --run webpack serve",
    "build": "dotnet fable src --run webpack"
  },
```

Webpack will run only once and will write the generated files in the [output.path](https://webpack.js.org/configuration/output#outputpath). This is the directory you have to deploy to your host!

## I don't want to use Webpack

Then, depending on the sample you've chosen, build options may differ. Please refer to the README file for the chosen sample to get more information.
