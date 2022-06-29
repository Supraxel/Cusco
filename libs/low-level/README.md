# Cusco.LowLevel

Various utility classes to ease interaction with native libraries.

Major classes to discover the library:
- `PinnedInstance`, to store managed objects in native libraries' `void* userdata`
- `UnsafePointer`, `UnsafeMutablePointer` and `UnsafeView` structs, for strongly typed unmanaged memory (as in [Swift](https://developer.apple.com/documentation/swift/manual-memory-management))
- `Atomic`s and `OnceFlag`
