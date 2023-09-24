namespace ItemRest
{
    public class Pet : Item
    {
        public string Species { get; set; }
        public int Age { get; set; }

        //public Pet(string name, int price, string species, int age)
        //    : base(name, price)
        //{
        //    Species = species;
        //    Age = age;
        //}
    }
}
