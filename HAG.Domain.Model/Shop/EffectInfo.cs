using Fox.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HAG.Domain.Model.Shop
{
    public class EffectInfo
    {
        [DataMapping("Id")]
        public int Id { get; set; }

        [DataMapping("Name")]
        public string Name { get; set; }

        [DataMapping("Description")]
        public string Description { get; set; }

        [DataMapping("Minute")]
        public int Minute { get; set; }

        [DataMapping("Image")]
        public string Image { get; set; }

        [DataMapping("Cost")]
        public int Cost { get; set; }
    }
}
