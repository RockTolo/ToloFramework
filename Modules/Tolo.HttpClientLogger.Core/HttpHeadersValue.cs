﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections;
using System.Net.Http.Headers;
using System.Text;

namespace Tolo.HttpClientLogger
{
    internal sealed class HttpHeadersValue : IReadOnlyList<KeyValuePair<string, object>>
    {
        private readonly Kind _kind;
        private readonly Func<string, bool> _shouldRedactHeaderValue;

        private string? _formatted;
        private List<KeyValuePair<string, object>>? _values;

        public HttpHeadersValue(Kind kind, HttpHeaders headers, HttpHeaders? contentHeaders, Func<string, bool> shouldRedactHeaderValue)
        {
            _kind = kind;
            _shouldRedactHeaderValue = shouldRedactHeaderValue;

            Headers = headers;
            ContentHeaders = contentHeaders;
        }

        public HttpHeaders Headers { get; }

        public HttpHeaders? ContentHeaders { get; }

        private List<KeyValuePair<string, object>> Values
        {
            get
            {
                if (_values == null)
                {
                    var values = new List<KeyValuePair<string, object>>();

                    foreach (KeyValuePair<string, IEnumerable<string>> kvp in Headers)
                    {
                        values.Add(new KeyValuePair<string, object>(kvp.Key, kvp.Value));
                    }

                    if (ContentHeaders != null)
                    {
                        foreach (KeyValuePair<string, IEnumerable<string>> kvp in ContentHeaders)
                        {
                            values.Add(new KeyValuePair<string, object>(kvp.Key, kvp.Value));
                        }
                    }

                    _values = values;
                }

                return _values;
            }
        }

        public KeyValuePair<string, object> this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new IndexOutOfRangeException(nameof(index));
                }

                return Values[index];
            }
        }

        public int Count => Values.Count;

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Values.GetEnumerator();
        }

        public override string ToString()
        {
            if (_formatted == null)
            {
                var builder = new StringBuilder();
                
                for (int i = 0; i < Values.Count; i++)
                {
                    KeyValuePair<string, object> kvp = Values[i];
                    builder.Append(kvp.Key);
                    builder.Append(": ");

                    if (_shouldRedactHeaderValue(kvp.Key))
                    {
                        builder.Append('*');
                        builder.AppendLine();
                    }
                    else
                    {
                        foreach (object value in (IEnumerable<object>)kvp.Value)
                        {
                            builder.Append(value);
                            builder.Append(", ");
                        }

                        // Remove the extra ', '
                        builder.Remove(builder.Length - 2, 2);
                        builder.AppendLine();
                    }
                }

                _formatted = builder.ToString();
            }

            return _formatted;
        }

        public enum Kind
        {
            Request,
            Response,
        }
    }
}
