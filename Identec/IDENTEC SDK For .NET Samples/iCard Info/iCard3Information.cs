using System;
using IDENTEC.Readers;
using IDENTEC.Tags;


namespace iCard_Info
{

	class NewTest
	{
		[STAThread]
		static void Main(string[] args)
		{
			iCard3 myReader = new iCard3();
			try
			{
				if (myReader.Connect())
				{
					Console.WriteLine("i-Card Information: " + myReader.Information);
				}
				else
				{
					Console.WriteLine("Error reading card: " + myReader.StatusMessage);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("An exception was thrown : " + ex);
			}
			Console.WriteLine("Press Enter to continue...");
			Console.ReadLine();                
		}
	}
}