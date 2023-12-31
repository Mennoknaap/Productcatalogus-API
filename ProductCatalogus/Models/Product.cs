using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalogus
{

    public class Product
    {
        [Key] public string ?Code { get; set; }

        public string Naam { get; set; }

        public int PotMaat { get; set; }

        public int PlantHoogte { get; set; }

        public string ?Kleur { get; set; }

        public int ProductGroep { get; set; }
    }
}