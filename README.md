# ArrayLengthVsStatic

``` ini

BenchmarkDotNet=v0.10.13, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.309)
Intel Xeon CPU E5-1650 0 3.20GHz, 1 CPU, 12 logical cores and 6 physical cores
Frequency=3117486 Hz, Resolution=320.7713 ns, Timer=TSC
.NET Core SDK=2.1.300-preview2-008396
  [Host]     : .NET Core 2.1.0-preview2-26325-03 (CoreCLR 4.6.26325.03, CoreFX 4.6.26325.02), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.0-preview2-26325-03 (CoreCLR 4.6.26325.03, CoreFX 4.6.26325.02), 64bit RyuJIT


```
|                              Method |      Mean |     Error |    StdDev |
|------------------------------------ |----------:|----------:|----------:|
|                  GetLengthFromArray |  5.625 ns | 0.0824 ns | 0.0771 ns |
|                 GetLengthFromStatic |  5.130 ns | 0.0317 ns | 0.0297 ns |
|        WriteResponseLengthFromArray | 15.534 ns | 0.2032 ns | 0.1901 ns |
|       WriteResponseLengthFromStatic | 15.774 ns | 0.1061 ns | 0.0993 ns |
| WriteResponseLengthFromCachedStatic | 15.723 ns | 0.1673 ns | 0.1483 ns |
