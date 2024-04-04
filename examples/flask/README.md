# Fable Python on Flask

## python dependencies

* python3
* pip must be installed, as we use pip virtual environments
* `python3 -m venv .venv`
* `source .venv/bin/activate`
* `which python`
* install dependencies for each example, e.g. flask and so on..
* `deactivate` to leave the venv

## Install Dependencies

```sh
> dotnet tool restore
> dotnet restore

> pip install flask
> rehash # so you can run flask below
```

## Build

```
> dotnet fable-py src
```

## Run

```sh
> cd src
> flask run
```