"""Pydantic Models for the Pydantic Example

This file contains Pydantic models that are imported by the F# code.
These models demonstrate how to integrate with existing Python Pydantic
models from F#.
"""

from pydantic import BaseModel


class User(BaseModel):
    id: int
    name: str
    email: str | None = None
    age: int | None = None


class Product(BaseModel):
    id: int
    name: str
    description: str
    price: float
    in_stock: bool
    tags: list[str]


class CreateUserRequest(BaseModel):
    name: str
    email: str | None = None
    age: int | None = None


class CreateProductRequest(BaseModel):
    name: str
    description: str
    price: float
    in_stock: bool
    tags: list[str]
