# Compile and Run a Python Script

```
dotnet tool restore
```

then run

```
dotnet fable --lang Python --noCache && rm app.py && mv *.py app.py && flask run
```

## python dependencies

* python3
* script specific dependencies can be inferred from imports

## local dependencies (optional)
* pip must be installed, as we use pip virtual environments
* `python3 -m venv .venv`
* `source .venv/bin/activate`
* `which python`
* `pip install flask`
* install dependencies for each example, e.g. flask and so on..
* `deactivate` to leave the venv and return to the normal py environment
