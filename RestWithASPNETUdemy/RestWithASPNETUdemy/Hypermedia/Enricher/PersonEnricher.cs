using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Hypermedia.Enricher
{
    public class PersonEnricher : ContentResponseEnricher<PersonVO>
    {
        private readonly object _lock = new object();

        protected override Task EnrichModel(PersonVO content, IUrlHelper urlHelper)
        {
            var path = "api/person/v1";
            string link = GetLink(content.Id, urlHelper, path);


            Type type = typeof(HttpActionVerb);
            FieldInfo[] fields = type.GetFields();

            foreach (FieldInfo elemento in fields)
            {
                addVerb(content, link, elemento.Name);
            }


            return null;
        }

        private void addVerb(PersonVO content, string link, string verb)
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
