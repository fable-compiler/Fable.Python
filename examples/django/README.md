# Fable Python on Django

## Info

The project is a copy of Django's `python manage.py startproject tproj` output.
The compiled version should be almost identical to the above command.


## Install Dependencies

```sh
> dotnet tool restore
> dotnet restore

> pip3 install -r Django
```

## Build

```
> dotnet fable-py
```

## Run

```sh
> python3 manage.py runserver
```

Visit http://127.0.0.1:8000/

