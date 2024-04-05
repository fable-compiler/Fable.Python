from __future__ import annotations
from flask import Flask
from typing import Any
from fable_modules.fable_library.list import (of_array, of_seq, singleton, map)
from fable_modules.fable_library.string_ import join
from fable_modules.fable_library.types import to_string
from fable_modules.fable_library.util import (to_enumerable, ignore)
from fable_modules.fable_python.flask.flask import Flask as Flask_1
from fable_modules.feliz_view_engine.interop import (Interop_mkAttr, Interop_mkText)
from fable_modules.feliz_view_engine.style_types import (IStyleAttribute, StyleAttribute)
from fable_modules.feliz_view_engine.view_engine import (ReactElement, IReactProperty, Render_htmlDocument_78F28865)

def View_title(str_1: str) -> ReactElement:
    return ReactElement(0, "p", of_array([Interop_mkAttr("class", join(" ", to_enumerable(["title"]))), Interop_mkText(str_1)]))


def View_subTitle(str_1: str) -> ReactElement:
    return ReactElement(0, "p", of_array([Interop_mkAttr("class", join(" ", to_enumerable(["subtitle"]))), Interop_mkText(str_1)]))


View_model: dict[str, Any] = {
    "Author": "dag@brattli.net",
    "Banner": "https://unsplash.it/1200/900?random",
    "Brand": "public/favicon.png",
    "Description": "Demo Website, Fable Python running on Flask!",
    "PermaLink": "https://fable.io",
    "Title": "Fable Python |> F# ♥️ Python"
}

View_head: ReactElement = ReactElement(0, "head", singleton(IReactProperty(1, of_seq(to_enumerable([ReactElement(0, "title", singleton(Interop_mkText(View_model["Title"]))), ReactElement(1, "meta", singleton(Interop_mkAttr("charset", "utf-8"))), ReactElement(1, "meta", of_array([Interop_mkAttr("name", "author"), Interop_mkAttr("content", View_model["Author"])])), ReactElement(1, "meta", of_array([Interop_mkAttr("name", "description"), Interop_mkAttr("content", View_model["Description"])])), ReactElement(1, "meta", of_array([Interop_mkAttr("http-equiv", "content-type"), Interop_mkAttr("content", "text/html"), Interop_mkAttr("charset", "utf-8")])), ReactElement(1, "meta", of_array([Interop_mkAttr("name", "viewport"), Interop_mkAttr("content", "width=device-width, initial-scale=1")])), ReactElement(1, "meta", of_array([Interop_mkAttr("http-equiv", "Cache-Control"), Interop_mkAttr("content", "no-cache, no-store, must-revalidate")])), ReactElement(1, "meta", of_array([Interop_mkAttr("http-equiv", "Pragma"), Interop_mkAttr("content", "no-cache")])), ReactElement(1, "meta", of_array([Interop_mkAttr("http-equiv", "Expires"), Interop_mkAttr("content", "0")])), ReactElement(1, "link", of_array([Interop_mkAttr("rel", "icon"), Interop_mkAttr("href", "public/favicon.ico")])), ReactElement(1, "link", of_array([Interop_mkAttr("rel", "stylesheet"), Interop_mkAttr("href", "https://cdnjs.cloudflare.com/ajax/libs/bulma/0.9.3/css/bulma.min.css"), Interop_mkAttr("crossorigin", "anonymous")]))])))))

View_body: ReactElement = ReactElement(0, "div", singleton(IReactProperty(1, of_seq(to_enumerable([View_title(View_model["Title"]), View_subTitle(View_model["Description"])])))))

View_section: ReactElement = ReactElement(0, "section", of_array([Interop_mkAttr("class", join(" ", to_enumerable(["hero", "is-fullheight-with-navbar"]))), Interop_mkAttr("style", join(";", map(to_string, of_array([StyleAttribute(0, "background-image", ("url(\'" + View_model["Banner"]) + "\')"), StyleAttribute(0, "background-position", "center"), StyleAttribute(0, "background-size", "cover")])))), IReactProperty(1, of_seq(to_enumerable([ReactElement(0, "div", of_array([Interop_mkAttr("class", join(" ", to_enumerable(["hero-body", "is-dark"]))), IReactProperty(1, of_seq(to_enumerable([ReactElement(0, "div", of_array([Interop_mkAttr("class", join(" ", to_enumerable(["container"]))), IReactProperty(1, of_seq(to_enumerable([View_body])))]))])))]))])))]))

View_html: ReactElement = ReactElement(0, "html", singleton(IReactProperty(1, of_seq(to_enumerable([View_head, ReactElement(0, "body", singleton(IReactProperty(1, of_seq(to_enumerable([View_section])))))])))))

def render_view(__unit: None=None) -> str:
    return Render_htmlDocument_78F28865(View_html)


app: Flask = Flask((__name__), static_url_path="/public")

def _arrow32(__unit: None=None) -> str:
    return render_view()


ignore(app.route("/")(_arrow32))

