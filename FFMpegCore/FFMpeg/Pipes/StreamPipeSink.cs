﻿using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FFMpegCore.Pipes
{
    public class StreamPipeSink : IPipeSink
    {
        public Func<Stream, CancellationToken, Task> Writer { get; }
        public int BlockSize { get; set; } = 4096;
        public string Format { get; set; } = string.Empty;

        public StreamPipeSink(Func<Stream, CancellationToken, Task> writer)
        {
            Writer = writer;
        }
        public StreamPipeSink(Stream destination)
        {
            Writer = (inputStream, cancellationToken) => inputStream.CopyToAsync(destination, BlockSize, cancellationToken);
        }

        public Task ReadAsync(System.IO.Stream inputStream, CancellationToken cancellationToken) 
            => Writer(inputStream, cancellationToken);

        public string GetFormat() => Format;
    }
}