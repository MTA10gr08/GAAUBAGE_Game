using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAAUBAGE_Game.API.Models
{
    public class Categories
    {
        public string[] BackgroundCategories { get; set; }
        public string[] ContextCategories { get; set; }
        public TrashCategory[] TrashCategories { get; set; }

        public partial class TrashCategory
        {
            public string Name { get; set; }
            public string[] SubCategories { get; set; }
        }
    }
}

