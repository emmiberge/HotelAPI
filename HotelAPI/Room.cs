using System;

public class Room
{
	public readonly int ID;
	public readonly int AmountOfBeds;
	public readonly double PricePerNight;

    public Room(int iD, int amountOfBeds, double pricePerNight)
    {
        ID = iD;
        AmountOfBeds = amountOfBeds;
        PricePerNight = pricePerNight;
    }
}
