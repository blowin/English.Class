using System.Net;

namespace English.Class.FastEndpoint.Extension;

public static class HttpStatusCodeExt
{
    public static int AsInt(this HttpStatusCode self) => (int)self;
}