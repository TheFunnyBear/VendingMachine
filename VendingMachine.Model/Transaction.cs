using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VendingMachine.Model
{
    public sealed class Transaction
    {
        [Key]
        public int Id { get; set; }

        [NotMapped]
        public List<Coin> InsertedCoins { get; set; }

        [NotMapped]
        public List<Drink> PurchasedProducts { get; set; }

        [NotMapped]
        public List<Coin> ChangeCoins { get; set; }

        [NotMapped]
        public Money SendToPhone { get; set; }

        [NotMapped]
        public string PhoneNumber { get; set; }

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

                var jTransaction = JsonConvert.DeserializeObject<Transaction>(value);
                InsertedCoins.Clear();
                InsertedCoins.AddRange(jTransaction.InsertedCoins);

                PurchasedProducts.Clear();
                PurchasedProducts.AddRange(jTransaction.PurchasedProducts);

                ChangeCoins.Clear();
                ChangeCoins.AddRange(jTransaction.ChangeCoins);

                SendToPhone = jTransaction.SendToPhone;
                PhoneNumber = jTransaction.PhoneNumber;
            }
        }

        public Transaction()
        {
            InsertedCoins = new List<Coin>();
            PurchasedProducts = new List<Drink>();
            ChangeCoins = new List<Coin>();
            SendToPhone = new Money();
        }

        public Money GetInsertedMoney()
        {
            var insertedMoney = new Money();
            foreach (var insertedCoin in InsertedCoins)
            {
                insertedMoney += insertedCoin.Nominal;
            }

            return insertedMoney;
        }

        public Money GetPurchasedPrice()
        {
            var purchasedPrice = new Money();
            foreach (var purchasedProduct in PurchasedProducts)
            {
                purchasedPrice += purchasedProduct.Price;
            }
            return purchasedPrice;
        }

        public Money GetChangeMoney()
        {
            var change = new Money();
            foreach (var changeCoin in ChangeCoins)
            {
                change += changeCoin.Nominal;
            }
            return change;
        }

        public Money GetBalance()
        {
            var insertedMoney = GetInsertedMoney();
            var purchasedPrice = GetPurchasedPrice();
            var changeMoney = GetChangeMoney();
            return insertedMoney - purchasedPrice - changeMoney;
        }
    }
}
