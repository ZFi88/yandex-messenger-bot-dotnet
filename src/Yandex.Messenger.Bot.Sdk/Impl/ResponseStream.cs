namespace Yandex.Messenger.Bot.Sdk.Impl;

/// <summary>
/// Represents a stream which owns the http response and disposes it together with the content stream.
/// </summary>
internal sealed class ResponseStream : Stream
{
    private readonly HttpResponseMessage _response;
    private readonly Stream _inner;
    private bool _disposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResponseStream"/> class.
    /// </summary>
    /// <param name="response">The http response which owns the content stream.</param>
    /// <param name="inner">The content stream.</param>
    public ResponseStream(HttpResponseMessage response, Stream inner)
    {
        _response = response;
        _inner = inner;
    }

    /// <inheritdoc/>
    public override bool CanRead => _inner.CanRead;

    /// <inheritdoc/>
    public override bool CanSeek => _inner.CanSeek;

    /// <inheritdoc/>
    public override bool CanWrite => _inner.CanWrite;

    /// <inheritdoc/>
    public override long Length => _inner.Length;

    /// <inheritdoc/>
    public override long Position
    {
        get => _inner.Position;
        set => _inner.Position = value;
    }

    /// <inheritdoc/>
    public override void Flush()
    {
        _inner.Flush();
    }

    /// <inheritdoc/>
    public override int Read(byte[] buffer, int offset, int count)
    {
        return _inner.Read(buffer, offset, count);
    }

    /// <inheritdoc/>
    public override long Seek(long offset, SeekOrigin origin)
    {
        return _inner.Seek(offset, origin);
    }

    /// <inheritdoc/>
    public override void SetLength(long value)
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc/>
    public override void Write(byte[] buffer, int offset, int count)
    {
        _inner.Write(buffer, offset, count);
    }

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _inner.Dispose();
                _response.Dispose();
            }

            _disposed = true;
        }

        base.Dispose(disposing);
    }
}
