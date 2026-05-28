using CoffeeShop;

internal class Tea : Drink
{
    public TeaType Type { get; set; }

    public override bool HasCoffeine
    {
        get
        {
            return Type == TeaType.Green || Type == TeaType.Black;
        }
    }
}