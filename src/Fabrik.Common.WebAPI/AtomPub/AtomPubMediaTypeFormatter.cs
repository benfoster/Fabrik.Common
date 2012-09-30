using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Fabrik.Common.WebAPI.AtomPub
{
    public class AtomPubMediaFormatter : BufferedMediaTypeFormatter
    {
        private const string AtomMediaType = "application/atom+xml";

        public AtomPubMediaFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(AtomMediaType));
            this.AddQueryStringMapping("format", "atom", AtomMediaType);
        }

        public override bool CanReadType(Type type)
        {
            return type.Implements<IPublicationCommand>();
        }

        public override bool CanWriteType(Type type)
        {
            return type.Implements<IPublication>() || type.Implements<IPublicationFeed>();
        }

        public override object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            Ensure.Argument.NotNull(type, "type");
            Ensure.Argument.NotNull(readStream, "readStream");
            
            HttpContentHeaders contentHeaders = content == null ? null : content.Headers;

            // If content length is 0 then return default value for this type
            if (contentHeaders != null && contentHeaders.ContentLength == 0)
            {
                return GetDefaultValueForType(type);
            }

            try
            {
                using (readStream)
                {
                    using (var reader = XmlReader.Create(readStream))
                    {
                        var formatter = new Atom10ItemFormatter();
                        formatter.ReadFrom(reader);

                        var command = Activator.CreateInstance(type);
                        ((IPublicationCommand)command)
                            .ReadSyndicationItem(formatter.Item);

                        return command;
                    }
                }
            }
            catch (Exception e)
            {
                if (formatterLogger == null)
                {
                    throw;
                }
                formatterLogger.LogError(String.Empty, e);
                return GetDefaultValueForType(type);
            }
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            Ensure.Argument.NotNull(value, "value");

            using (writeStream)
            {
                if (value is IPublicationFeed)
                {
                    WriteAtomFeed((IPublicationFeed)value, writeStream);
                }
                else
                {
                    WriteAtomEntry((IPublication)value, writeStream);
                }
            }
        }

        private void WriteAtomFeed(IPublicationFeed feed, Stream writeStream)
        {
            var formatter = new Atom10FeedFormatter(feed.Syndicate());

            using (var writer = XmlWriter.Create(writeStream))
            {
                formatter.WriteTo(writer);
            }
        }

        private void WriteAtomEntry(IPublication publication, Stream writeStream)
        {
            var entry = publication.Syndicate();

            var formatter = new Atom10ItemFormatter(entry);

            using (var writer = XmlWriter.Create(writeStream))
            {
                formatter.WriteTo(writer);
            }
        }
    }
}
