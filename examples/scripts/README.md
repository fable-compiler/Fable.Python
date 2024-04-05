# Compile and Run a Python Script

```
dotnet tool restore
```

then run

```
dotnet fsi run_flask_app.fsx flask_sample.fsx
```

this will run using `pip-run` for now requirements.txt manual file is supported but inference via `pipreqs` is not working as expected.

## python dependencies

* python3
* pip
* `pipx` > required, used to install global python packages. DEPENDENCY, [install instructions here](https://pipx.pypa.io/stable/) or for mac via brew.
* `pipreqs` for package requiremnts inference : `pipx install pipreqs` will be executed. (not working yet)
* `pip-run` for running with defualt one shot option, which runs your script one time in a temp env (can be installed with pipx), `pipx install pip-run` will be executed

## pip-run

* [pip-run](https://github.com/jaraco/pip-run/blob/main/README.rst) is used to run all script in "isolation" taking care of creatinv venv and installing libs in temp dirs and removing them after execution