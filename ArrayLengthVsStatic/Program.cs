using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace ArrayLengthVsStatic
{
    //                              Method |      Mean |     Error |    StdDev |
    //------------------------------------ |----------:|----------:|----------:|
    //                  GetLengthFromArray |  5.625 ns | 0.0824 ns | 0.0771 ns |
    //                 GetLengthFromStatic |  5.130 ns | 0.0317 ns | 0.0297 ns |
    //        WriteResponseLengthFromArray | 15.534 ns | 0.2032 ns | 0.1901 ns |
    //       WriteResponseLengthFromStatic | 15.774 ns | 0.1061 ns | 0.0993 ns |
    // WriteResponseLengthFromCachedStatic | 15.723 ns | 0.1673 ns | 0.1483 ns |

    public class ArrayLengthVsStatic
    {
        private static readonly byte[] _helloWorldPayload = Encoding.UTF8.GetBytes("Hello, World!");
        private static readonly int _helloWorldPayloadLength = _helloWorldPayload.Length;

        private static readonly HttpResponse _response = new MockHttpResponse();

        [Benchmark]
        public static int GetLengthFromArray()
        {
            return _helloWorldPayload.Length;
        }

        [Benchmark]
        public static int GetLengthFromStatic()
        {
            return _helloWorldPayloadLength;
        }

        [Benchmark]
        public static Task WriteResponseLengthFromArray()
        {
            var payloadLength = _helloWorldPayload.Length;
            _response.StatusCode = 200;
            _response.ContentType = "text/plain";
            _response.ContentLength = payloadLength;
            return _response.Body.WriteAsync(_helloWorldPayload, 0, payloadLength);
        }

        [Benchmark]
        public static Task WriteResponseLengthFromStatic()
        {
            _response.StatusCode = 200;
            _response.ContentType = "text/plain";
            _response.ContentLength = _helloWorldPayloadLength;
            return _response.Body.WriteAsync(_helloWorldPayload, 0, _helloWorldPayloadLength);
        }

        [Benchmark]
        public static Task WriteResponseLengthFromCachedStatic()
        {
            var payloadLength = _helloWorldPayloadLength;
            _response.StatusCode = 200;
            _response.ContentType = "text/plain";
            _response.ContentLength = payloadLength;
            return _response.Body.WriteAsync(_helloWorldPayload, 0, payloadLength);
        }

    }

    public class MockHttpResponse : HttpResponse
    {
        // Implemented
        public override Stream Body { get; set; } = Stream.Null;
        public override long? ContentLength { get; set; }
        public override string ContentType { get; set; }
        public override int StatusCode { get; set; }

        // NotImplemented
        public override HttpContext HttpContext => throw new NotImplementedException();

        public override IHeaderDictionary Headers => throw new NotImplementedException();

        public override IResponseCookies Cookies => throw new NotImplementedException();

        public override bool HasStarted => throw new NotImplementedException();

        public override void OnCompleted(Func<object, Task> callback, object state)
        {
            throw new NotImplementedException();
        }

        public override void OnStarting(Func<object, Task> callback, object state)
        {
            throw new NotImplementedException();
        }

        public override void Redirect(string location, bool permanent)
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var sumary = BenchmarkRunner.Run<ArrayLengthVsStatic>();
        }
    }
}
