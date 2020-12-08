using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Hypermedia.Enricher
{
    public class BookEnricher : ContentResponseEnricher<BookVO>
    {
        private readonly object _lock = new object();

        protected override Task EnrichModel(BookVO content, IUrlHelper urlHelper)
        {
            var path = "api/books/v1";
            string link = GetLink(content.Id, urlHelper, path);

            foreach (var elemento in Enum.GetValues(typeof(HttpActionVerb)))
            {
                addVerb(content, link, elemento.ToString());
            }


            return null;
        }

        private void addVerb(BookVO content, string link, string verb)
        {
            var type = "";
            switch (verb)
            {
                case HttpActionVerb.GET:
                    type = RelatResponseTypeFormationType.DefaultGet;
                    break;

                case HttpActionVerb.POST:
                    type = RelatResponseTypeFormationType.DefaultPost;
                    break;

                case HttpActionVerb.DELETE:
                    type = RelatResponseTypeFormationType.DefaultPost;
                    break;

                case HttpActionVerb.PUT:
                    type = RelatResponseTypeFormationType.DefaultPut;
                    break;

                case HttpActionVerb.PATCH:
                    type = RelatResponseTypeFormationType.DefaultPatch;
                    break;
            }

            content.Links.Add(new HyperMediaLink()
            {
                Action = verb,
                Href = link,
                Rel = RelationType.self,
                Type = type
            });
        }

        private string GetLink(long id, IUrlHelper urlHelper, string path)
        {
            lock (_lock)
            {
                var url = new { controller = path, id = id };
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
            }
        }
    }
}
