using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;

namespace Fabrik.Common.WebAPI.AtomPub
{
    public class AtomPubMediaFormatter : MediaTypeFormatter
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

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            Ensure.Argument.NotNull(type, "type");
            Ensure.Argument.NotNull(readStream, "readStream");
            
            return TaskHelpers.RunSynchronously<object>(() =>
            {
                HttpContentHeaders contentHeaders = content == null ? null : content.Headers;

                // If content length is 0 then return default value for this type
                if (contentHeaders != null && contentHeaders.ContentLength == 0)
                {
                    return GetDefaultValueForType(type);
                }

                try
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
                catch (Exception e)
                {
                    if (formatterLogger == null)
                    {
                        throw;
                    }
                    formatterLogger.LogError(String.Empty, e);
                    return GetDefaultValueForType(type);
                }
            });
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            return TaskHelpers.RunSynchronously(() =>
            {
                if (value is IPublicationFeed)
                {
                    WriteAtomFeed((IPublicationFeed)value, writeStream);
                }
                else
                {
                    WriteAtomEntry((IPublication)value, writeStream);
                }
            });
        }

        private void WriteAtomFeed(IPublicationFeed feed, Stream writeStream)
        {
            var atomEntries = feed.Items.Select(i => i.Syndicate());

            var atomFeed = new SyndicationFeed
            {
                Title = new TextSyndicationContent(feed.Title),
                Items = atomEntries
            };

            atomFeed.Authors.Add(new SyndicationPerson { Name = feed.Author });

            var formatter = new Atom10FeedFormatter(atomFeed);

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
