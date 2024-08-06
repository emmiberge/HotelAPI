using HotelAPI.Model;
using System;

public class TestData
{
	public static Room[] GetTestData()
	{
		Room[] rooms = { new Room(1, 2, 300.0m)};
		return rooms;
	}
}