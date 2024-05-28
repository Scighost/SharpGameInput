# SharpGameInput

C# interop bindings for [GameInput](https://learn.microsoft.com/en-us/gaming/gdk/_content/gc/input/overviews/input-overview).

This layer exposes the API in a near-1:1 fashion. It is very raw, you will need unsafe code for some APIs. Some adjustments were made to be more idiomatic where possible, but it is otherwise used in an identical fashion to how it would be used in C++.

All COM interop mechanisms used are written from scratch for maximum control, as neither the built-in COM interop mechanisms nor the source-generated ones have first-class support for manually releasing an interface instance. This also allows for some performance tuning, as a lot of overhead can be cut out, and e.g. avoiding a class instance allocation for every call that produces an `IGameInputReading` is an easy improvement now.

## License

This project is licensed under the MIT license. See [LICENSE.md](LICENSE.md) for details.
