using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebParser.Models
{
    public class KeywordsViewModel
    {
        public KeywordsViewModel()
            :this(new ReadOnlyDictionary<string, int>(new Dictionary<string, int>()))
        { }

        public KeywordsViewModel(IDictionary<string, int> keywords)
            :this(new ReadOnlyDictionary<string, int>(keywords))
        { }

        public KeywordsViewModel(ReadOnlyDictionary<string, int> keywords)
        {
            Keywords = keywords ?? throw new ArgumentNullException(nameof(keywords));
        }

        public ReadOnlyDictionary<string, int> Keywords { get; set; }
    }
}
