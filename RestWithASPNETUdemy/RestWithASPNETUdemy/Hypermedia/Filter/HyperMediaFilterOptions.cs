using RestWithASPNETUdemy.Hypermedia.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNETUdemy.Hypermedia.Filter
{
    public class HyperMediaFilterOptions
    {
        public List<IResponseEnricher> ContecntResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
