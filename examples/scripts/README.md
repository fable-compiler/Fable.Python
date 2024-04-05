# Compile and Run a Python Script

```
dotnet tool restore
```

then run

```
dotnet fsi run_flask_app.fsx flask_sample.fsx [additional,pip,dependecies,comma,separated]
```

## python dependencies

* python3
* pip

## local dependencies are installed using pip venv

* pip must be installed, as we use pip virtual environments
* `python3 -m venv .venv`
* `source .venv/bin/activate`
* `which python`
* `pip install flask`
* install dependencies for each example, e.g. flask and so on..
* `deactivate` to leave the venv and return to the normal py environment
