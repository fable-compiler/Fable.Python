# Fable Python on Django

## Credits

https://github.com/rnevius/minimal-django

## Install Dependencies

```sh
> dotnet tool restore
> dotnet restore

> pip install -r Django
```

## Build

```
> dotnet fable-py
```

## Run

Note tht the first argument is skipped, because of `main(sys.argv[1:])` for some reason
```sh
> python3 program.py skipped runserver
```

Visit http://127.0.0.1:8000/

