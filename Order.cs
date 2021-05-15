using System;

public class Order
{
	public int CustomerID { get; set; }
	public int OrderID { get; set; }
	public DateTime OrderDate { get; set; }
	public DateTime FilledDate { get; set; }
	public string Status { get; set; }
	public int Amount { get; set; }
}
