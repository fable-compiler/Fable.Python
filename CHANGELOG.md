---
last_commit_released: deed5e74cbc33a582d1e22ee0a1af6a41ab2bb10
name: Fable.Python
---

# Changelog

All notable changes to this project will be documented in this file.

## 5.0.0-rc.4 - 2026-04-17

### 🚀 Features

* *(math)* Add missing math functions (log/base, modf, frexp, ldexp, remainder, isclose, nextafter, ulp, exp2, cbrt) (#255) ([9687aac](https://github.com/fable-compiler/Fable.Python/commit/9687aac9c192254bf46d31636b2526c17eeeee49))
* *(stdlib)* Add itertools module bindings (#256) ([6a4d816](https://github.com/fable-compiler/Fable.Python/commit/6a4d816591d55615d549dc80232d8ef5265ff9a0))

### 🐞 Bug Fixes

* *(math)* Fix factorial signature and add missing constants/functions (#251) ([7b32de0](https://github.com/fable-compiler/Fable.Python/commit/7b32de02ee687f6d833dcb21caae026e3448fb99))
* *(math)* Correct type signatures for copysign, fmod and curried bindings (#253) ([8f33975](https://github.com/fable-compiler/Fable.Python/commit/8f33975daa8c5a46f8f2c3d1fc8fc1751ca97ab9))

<strong><small>[View changes on Github](https://github.com/fable-compiler/Fable.Python/compare/66e56764fefe824e1b2a28635245820c2dc92a34..deed5e74cbc33a582d1e22ee0a1af6a41ab2bb10)</small></strong>

## Unreleased

### 🐞 Bug Fixes

* Fix `math.factorial` binding: changed signature from `float -> float` to `int -> int` to match Python 3.12+ where float arguments raise `TypeError`. Fixes test to use integer literals.
* Fix `math.copysign` binding: `y` parameter was incorrectly typed as `int`, now correctly `float`
* Fix `math.fmod` binding: parameters were incorrectly typed as `int -> int -> int`, now correctly `float * float -> float`
* Fix `math.comb`, `math.pow`, `math.atan2`, `math.dist` bindings: converted curried parameter syntax to tupled for consistency

### ✨ Enhancements

* Add missing `math` module constants: `pi`, `e`, `tau`, `inf`, `nan`
* Add missing `math` module functions: `sqrt`, `degrees`, `radians`, `trunc`, `hypot`, `fsum`, `isqrt`, `prod`, `perm`, `acosh`, `asinh`, `atanh`, `cosh`, `sinh`, `tanh`, `erf`, `erfc`, `gamma`, `lgamma`
* Fix `math.dist` signature to accept float arrays (for multi-dimensional distance)

## 5.0.0-rc.3 - 2026-04-16

### 🐞 Bug Fixes

* Bump Fable.Core to 5.0.0-rc.2 and ShipIt to 2.0.2 (#243) ([cb06fd6](https://github.com/fable-compiler/Fable.Python/commit/cb06fd6fc338fde73e8eb3c3720ed20bfdd17287))

<strong><small>[View changes on Github](https://github.com/fable-compiler/Fable.Python/compare/8223cc8eb8b2dcaa02c1a19566c5ed5d73242b11..66e56764fefe824e1b2a28635245820c2dc92a34)</small></strong>

## 5.0.0-rc.2 - 2026-03-09

Last release on the legacy tag-based workflow. Matches [Fable.Python 5.0.0-rc.2 on NuGet](https://www.nuget.org/packages/Fable.Python/5.0.0-rc.2).

## [5.0.0-alpha.21.5](https://github.com/fable-compiler/Fable.Python/compare/v5.0.0-alpha.21.4...v5.0.0-alpha.21.5) (2025-12-21)

### Bug Fixes

* remove XUnit dependency from Testing module ([3a35e44](https://github.com/fable-compiler/Fable.Python/commit/3a35e4404624a0c2c13b8c869ff987ac7c81a7d7))

## [5.0.0-alpha.21.4](https://github.com/fable-compiler/Fable.Python/compare/v5.0.0-alpha.21.3...v5.0.0-alpha.21.4) (2025-12-21)

### Features

* add Fable.Python.Testing module for cross-platform testing ([#186](https://github.com/fable-compiler/Fable.Python/issues/186)) ([2356705](https://github.com/fable-compiler/Fable.Python/commit/235670557cfb1913b9e67f7ad2e0fd4772a6de6b))

## [5.0.0-alpha.21.3](https://github.com/fable-compiler/Fable.Python/compare/v5.0.0-alpha.21.2...v5.0.0-alpha.21.3) (2025-12-19)

### Features

* add Fable.Types module for runtime type detection ([#184](https://github.com/fable-compiler/Fable.Python/issues/184)) ([6e1e902](https://github.com/fable-compiler/Fable.Python/commit/6e1e902a0203d24206c3a7e6719383e464193fd5))

## [5.0.0-alpha.21.2](https://github.com/fable-compiler/Fable.Python/compare/v5.0.0-alpha.21.1...v5.0.0-alpha.21.2) (2025-12-18)

### Features

* add typed array support to Json serialization ([#182](https://github.com/fable-compiler/Fable.Python/issues/182)) ([ef67b4b](https://github.com/fable-compiler/Fable.Python/commit/ef67b4ba9258fde6a3f4ff3db0b843228953e7bc))

## [5.0.0-alpha.21.1](https://github.com/fable-compiler/Fable.Python/compare/v5.0.0-alpha.21.0...v5.0.0-alpha.21.1) (2025-12-18)

### Features

* Add Json static class with Fable-aware serialization ([#175](https://github.com/fable-compiler/Fable.Python/issues/175)) ([1eb5005](https://github.com/fable-compiler/Fable.Python/commit/1eb500523c6d6f43247bc3510a6045ec8d9d9d4e))

## [5.0.0-alpha.21.0](https://github.com/fable-compiler/Fable.Python/compare/v5.0.0-alpha.20.2...v5.0.0-alpha.21.0) (2025-12-16)

### Features

* Add exception types ([#173](https://github.com/fable-compiler/Fable.Python/issues/173)) ([72f09b2](https://github.com/fable-compiler/Fable.Python/commit/72f09b2ff6e208e3ab057430a355814611abd46c))
* FastAPI async handlers ([#174](https://github.com/fable-compiler/Fable.Python/issues/174)) ([26cec1f](https://github.com/fable-compiler/Fable.Python/commit/26cec1f239f9244a7c1da1d00bbf1a479596bb3c))

### Miscellaneous Chores

* sync with Fable 5.0.0-alpha.21 ([fd2685c](https://github.com/fable-compiler/Fable.Python/commit/fd2685c2f992c2d8058ac1bc1261d647271359df))

## [5.0.0-alpha.20.2](https://github.com/fable-compiler/Fable.Python/compare/v5.0.0-alpha.20.1...v5.0.0-alpha.20.2) (2025-12-09)

### Features

* add Python stdlib bindings for logging, random, and expand string module ([#166](https://github.com/fable-compiler/Fable.Python/issues/166)) ([709d6c2](https://github.com/fable-compiler/Fable.Python/commit/709d6c2b29199965926ff639727fdcc1bb2e1fb8))

## [5.0.0-alpha.20.1](https://github.com/fable-compiler/Fable.Python/compare/v5.0.0-alpha.20...v5.0.0-alpha.20.1) (2025-12-08)

### Bug Fixes

* relax FSharp.Core dependency to >= 5.0.0 ([#163](https://github.com/fable-compiler/Fable.Python/issues/163)) ([10eb65b](https://github.com/fable-compiler/Fable.Python/commit/10eb65b22a157078e1b66bd8fb202b0cd2acbedc))

## [5.0.0-alpha.20](https://github.com/fable-compiler/Fable.Python/compare/v4.0.0...v5.0.0-alpha.20) (2025-12-08)

### Features

* add write ([334e800](https://github.com/fable-compiler/Fable.Python/commit/334e80089c081bda25633f83dae037fc6c8fe6f5))
* Added bindings for Pydantic and FastAPI + examples ([#151](https://github.com/fable-compiler/Fable.Python/issues/151)) ([826629e](https://github.com/fable-compiler/Fable.Python/commit/826629e465fca15d444a4ca37b851b8aab488f9a))
* Fable v5 ([#147](https://github.com/fable-compiler/Fable.Python/issues/147)) ([abf8e6a](https://github.com/fable-compiler/Fable.Python/commit/abf8e6a1f7bbc2eae152431d04d6b8b5675f7795))

### Bug Fixes

* handle return type correctly ([a634168](https://github.com/fable-compiler/Fable.Python/commit/a6341684ac8bb3f1b244f448c22e1fe6f208fbc0))
* update pyproject toml details ([#62](https://github.com/fable-compiler/Fable.Python/issues/62)) ([b511351](https://github.com/fable-compiler/Fable.Python/commit/b5113514ca6b4a9ba00734e9b3caaf363f3385af))
* use plain int/string types for open ([b202a25](https://github.com/fable-compiler/Fable.Python/commit/b202a25bd7f48538fadc50125294a9252714d364))
* use string types for open ([f211f8b](https://github.com/fable-compiler/Fable.Python/commit/f211f8bb9445dd6930926fe48aab6a69720dd30f))
