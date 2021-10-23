# Fable Python on Django



## Info

The project is a copy of Django's `python manage.py startproject testproj` output.
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

Note tht the first argument is skipped, because of `main(sys.argv[1:])` for some reason
```sh
> python3 manage.py s runserver
```

Visit http://127.0.0.1:8000/

