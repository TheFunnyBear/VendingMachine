using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachine.Model
{
    [ComplexType]
    public sealed class Drink : IEquatable<Drink>
    {
        [NotMapped]
        public string Name { get; set; }

        [NotMapped]
        public int PictureId { get; set; }

        [NotMapped]
        public Money Price { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (obj is Drink)
            {
                Drink drink = obj as Drink;
                return (this.Price == drink.Price && this.Name == drink.Name);
            }

            return false;
        }

        public bool Equals(Drink drink)
        {
            if ((object)drink == null) return false;

            return (this.Price == drink.Price && this.Name == drink.Name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public override string ToString()
        {
            return $"Name = [{Name}] Price = [{Price}]";
        }

        [JsonIgnore]
        public string Serialized
        {
            get { return JsonConvert.SerializeObject(this); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }

                var jData = JsonConvert.DeserializeObject<Drink>(value);
                Name = jData.Name;
                PictureId = jData.PictureId;
                Price = jData.Price;
            }
        }
    }
}
