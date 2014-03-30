using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDeletor
{
	class Program
	{
		static void Main(string[] args)
		{
			string folder = args[0];
			int nDays = Convert.ToInt32(args[1]);
			int ifBiggerThanInMB = Convert.ToInt32(args[2]);
			string extension = args[3];
			bool verbose = args[4] == "true";

			var files = Directory.EnumerateFiles(folder);
			long spaceSaved = (long) 0.0;

			foreach (var file in files.Where(f => f.EndsWith(extension)))
			{
				FileInfo fi = new FileInfo(file);
				DateTime dt = fi.LastWriteTime;
				var sizeInMB = (long)(fi.Length / 1000000.0);

				bool deleteFile = dt < DateTime.Now.AddDays(-nDays) && sizeInMB > ifBiggerThanInMB;

				if(verbose)
					Console.WriteLine(deleteFile
					? string.Format("{0} - {1} - {2} - {3}", file, dt.ToShortDateString(), sizeInMB, "deleted")
					: string.Format("{0} - {1} - {2} - {3}", file, dt.ToShortDateString(), sizeInMB, "kept"));

				if (deleteFile)
				{
					spaceSaved += sizeInMB;
					File.Delete(file);
				}

			}

			if (verbose)
			{
				Console.WriteLine("");
				Console.WriteLine(string.Format("{0} MB saved !", spaceSaved));
				Console.Read();
			}
		}
	}
}
