using System;

namespace ItemRest
{
    public class Item
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }


        public void ValidateName()
        {
            if (Name == null) throw new ArgumentNullException();
            if (Name.Length < 2) throw new ArgumentException();
        }
        public void ValidatePrice()
        {
            if (Price < 0) throw new ArgumentOutOfRangeException();
        }
        public void Validate()
        {
            ValidateName();
            ValidatePrice();
        }
        public override string ToString()
        {
            return $"{Id}, {Name}: {Price}kr.";
        }

        //is the specified object equal to the current object.
        public override bool Equals(object? obj)
        {
            //obj is specified object
            if (obj == null) return false;
            if (obj.GetType() != typeof(Item)) return false;

            //her bliver den smidt i en Item refference
            //for at få adgang til de andre props
            Item Item = (Item)obj;
            if (Item.Name != Name) return false;
            if (Item.Price != Price) return false;
            if (Item.Id != Id) return false;
            return true;
        }
        //equal tjekker kun null, type, id:
        //public override bool Equals(object obj)
        //{

        //    if (obj == null || GetType() != obj.GetType())
        //    {
        //        return false;
        //    }
        //    return this.Id == ((Item)obj).Id;
        //}

        //det er hvis man hurtigt skal tjekke om to objecter er ens
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
