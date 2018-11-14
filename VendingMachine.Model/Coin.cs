using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachine.Model
{
    [ComplexType]
    public sealed class Coin : IEquatable<Coin>
    {
        [NotMapped]
        public string Name { get; set; }

        [NotMapped]
        public int PictureId { get; set; }

        [NotMapped]
        public Money Nominal { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            if (obj is Coin)
            {
                Coin coin = obj as Coin;
                return (this.Nominal == coin.Nominal && this.Name == coin.Name);
            }

            return false;
        }

        public bool Equals(Coin coin)
        {
            if ((object)coin == null) return false;

            return (this.Nominal == coin.Nominal && this.Name == coin.Name);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"Name = [{Name}] Nominal = [{Nominal}]";
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

                var jData = JsonConvert.DeserializeObject<Coin>(value);
                Name = jData.Name;
                PictureId = jData.PictureId;
                Nominal = jData.Nominal;
            }
        }
    }
}
