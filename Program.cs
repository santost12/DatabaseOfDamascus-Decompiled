using System;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Damascus
{
	internal class Program
	{
		private static string consoleTitle;

		private static int consoleWidth;

		private static int consoleHeight;

		private static string syntaxError;

		private static string fileNotFound;

		private static string folderNotFound;

		private static string formatError;

		private static string fileError;

		private static string mustConfirm;

		private static string yesNo;

		private static string sucess;

		private static string notSucess;

		private static string writing;

		private static string retrievingFileStart;

		private static char block;

		private static char topBlock;

		private static char bottomBlock;

		private static string retrievingFileEnd;

		private static string helpTitle;

		private static string needFile;

		private static bool open;

		private static int action;

		private static string waitText;

		private static string unrecognizedCMD;

		private static string[] command;

		private static string[] commandHelp;

		private static Random RNG;

		static Program()
		{
			Program.consoleTitle = "THE DATABASE OF DAMASCUS";
			Program.consoleWidth = 80;
			Program.consoleHeight = 24;
			Program.syntaxError = "SYNTAX ERROR";
			Program.fileNotFound = "FILE NOT FOUND";
			Program.folderNotFound = "FOLDER NOT FOUND";
			Program.formatError = "INCORRECT ID FORMAT";
			Program.fileError = "FILE ERROR";
			Program.mustConfirm = "CONFIRM ";
			Program.yesNo = "[Y/N]: ";
			Program.sucess = "DATABASE CREATED";
			Program.notSucess = "ERROR IN CREATING DATABASE AT ";
			Program.writing = "\tCREATING FILE: ";
			Program.retrievingFileStart = "\tRETRIEVING [";
			Program.block = '\u2588';
			Program.topBlock = '\u2580';
			Program.bottomBlock = '\u2584';
			Program.retrievingFileEnd = "]";
			Program.helpTitle = "DATABASE OF DAMASCUS COMAMNDS:";
			Program.needFile = "DESTINATION MUST BE SPECIFIED";
			Program.open = true;
			Program.action = 0;
			Program.waitText = "\n> ";
			Program.unrecognizedCMD = "\tNOT RECOGNIZED AS AN INTERNAL OR EXTERNAL COMMAND";
			Program.command = new string[] { "HELP", "EXIT", "GETID", "GETFILE", "GETRND", "GET", "CLS" };
			Program.commandHelp = new string[] { "\tDISPLAYS ALL COMMANDS", "\tCLOSES THE DATABASE", "\tGETID SRC [DEST]\n\tSRC\tSOURCE FILE LOCATTON\n\tDEST\tDESTINATION FILE LOCATION", "\tGETID SRC DEST\n\tSRC\tSOURCE FILE LOCATTON\n\tDEST\tDESTINATION FILE LOCATION", "\tGETRND SIZE DEST\n\tSIZE\tSIZE OF FILE IN BYTES\n\tDEST\tDESTINATION FILE LOCATION", "\tGET DEST\n\tDEST\tDESTINATION FOLDER LOCATION", "\tCLEARS THE SCREEN" };
			Program.RNG = new Random();
		}

		public Program()
		{
		}

		private static byte[] ASCIItoByte(byte[] b)
		{
			int num = 0;
			while (num < (int)b.Length)
			{
				if ((b[num] <= 47 || b[num] >= 71) && (b[num] >= 58 || b[num] <= 64))
				{
					b = new byte[0];
					break;
				}
				else
				{
					if (b[num] >= 65)
					{
						ref byte numPointer = ref b[num];
						numPointer = (byte)(numPointer - 55);
					}
					else
					{
						ref byte numPointer1 = ref b[num];
						numPointer1 = (byte)(numPointer1 - 48);
					}
					if (num == 1)
					{
						b[0] = (byte)(b[0] * 16 + b[1]);
						b[1] = 64;
					}
					num++;
				}
			}
			return b;
		}

		private static void colorize(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				switch (s[i])
				{
					case '0':
					case '5':
					case 'A':
					{
						Console.ForegroundColor = ConsoleColor.Gray;
						break;
					}
					case '1':
					case '6':
					case 'B':
					{
						Console.ForegroundColor = ConsoleColor.DarkGray;
						break;
					}
					case '2':
					case '7':
					case 'C':
					{
						Console.ForegroundColor = ConsoleColor.DarkBlue;
						break;
					}
					case '3':
					case '8':
					case 'D':
					{
						Console.ForegroundColor = ConsoleColor.DarkGreen;
						break;
					}
					case '4':
					case '9':
					case 'E':
					{
						Console.ForegroundColor = ConsoleColor.DarkYellow;
						break;
					}
					case ':':
					case ';':
					case '<':
					case '=':
					case '>':
					case '?':
					case '@':
					{
						Console.ForegroundColor = ConsoleColor.Black;
						break;
					}
					default:
					{
						goto case '@';
					}
				}
				char chr = Program.block;
				int num = Convert.ToInt32(s.Substring(i, 1), 16);
				if (num == 15)
				{
					chr = ' ';
				}
				else if (num >= 10)
				{
					chr = Program.bottomBlock;
				}
				else if (num >= 5)
				{
					chr = Program.topBlock;
				}
				Console.Write(chr);
			}
		}

		private static bool createDatabase(string folder)
		{
			bool flag;
			try
			{
				BigInteger bigInteger = 1;
				Console.WriteLine(string.Concat(Program.writing, "0"));
				BinaryWriter binaryWriter = new BinaryWriter(File.Open(string.Concat(folder, "\\0"), FileMode.Create));
				binaryWriter.Write(0);
				binaryWriter.Close();
				binaryWriter.Dispose();
				while (bigInteger > (long)0)
				{
					byte[] byteArray = bigInteger.ToByteArray();
					if (byteArray.Last<byte>() == 0)
					{
						byteArray = byteArray.Take<byte>((int)byteArray.Length - 1).ToArray<byte>();
					}
					string str = BitConverter.ToString(byteArray.Reverse<byte>().ToArray<byte>()).Replace("-", string.Empty);
					while (str.StartsWith("0"))
					{
						str = str.Substring(1);
					}
					Console.SetCursorPosition(0, Console.CursorTop - 1);
					Console.WriteLine(string.Concat(Program.writing, str));
					if (!File.Exists(string.Concat(folder, "\\", str)))
					{
						BinaryWriter binaryWriter1 = new BinaryWriter(File.Open(string.Concat(folder, "\\", str), FileMode.Create));
						binaryWriter1.Write(byteArray);
						binaryWriter1.Close();
						binaryWriter1.Dispose();
					}
					bigInteger = bigInteger++;
				}
				flag = true;
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception.ToString());
				flag = false;
			}
			return flag;
		}

		private static void drawTitle()
		{
			Program.colorize("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
			Program.colorize("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
			Program.colorize("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
			Program.colorize("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF");
			Program.colorize("FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF22FFFFFFFFFFFFFFFFFFFFFFFFFFF");
			Program.colorize("FFFFFFFFFFFFFFFF22FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF01FFFFFFFFFFFFFFFFFFFFFFFFFFF");
			Program.colorize("FFFFFFFFFFFFFFFF01FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF01FFFFFFFFFFFF22FFFFFFFFFFFFF");
			Program.colorize("FFFFFFFFFFFFFFFF01FFFFFFFFFFFFFFFF22FFFFFFFFFFFFFFF01FFFFFFFFFFFF01FFFFFFFFFFFFF");
			Program.colorize("FFFFFFFFFFFFFFFF01FFFFFFFFFFFFFFFF01FFFFFEFFFFFFFFF01FFFFFFFFFFFF01FFFFFFFFFFFFF");
			Program.colorize("FFFFFFFFFFFFFFFF01FFFFFFFFFFFFFFFF01FFFFF4FFFFFFFFF01FFFFFFFFFFFF01FFFFFFFFFFFFF");
			Program.colorize("FFFFFFFFFFFFFFFF01FFFFFFFFFFFFFFFF01FFCC222CCFF22FF01FFFFFFFFFFFF01FFFFFF22FFFFF");
			Program.colorize("FFFFFFFFFFFFFFFF01FFFFFFFFFFFFFFFF01C222222222C01FF01FFFFFFFFFFFF01FFFFFF01FFFFF");
			Program.colorize("FFFFFFFFFFFFFFFF01FFFFFFFFFFFFFFFF01B6B6B6B6B6B01FF01FFFFFFFFFFFF01FFFFFF01FFFFF");
			Program.colorize("FFFFFFFFFFFFFFFF01FFFFFFFFFFFF22FF010101010101501CC01FFFFFFFFFFFF01FFFFFF01FFFFF");
			Program.colorize("FFFFFFFFFFFFFFFF01FFFFFFFFFFFF01FC222222C101010C22201FFFFFFFFFFFF01FFFFFF01FFFFF");
			Program.colorize("FFFFFFFFFFFFFFFF01FFFFFFFFFFFF01222222222210106B6B601AFFFFFFFFFFF01FFFFFF01FFFFF");
			Program.colorize("FFFFFFFFFFFFFFFF01FFFFFFFC2CF100B6B6B6B6B6000C22CA601B01BFFFFFFFF01FFFFFF01FFFFD");
			Program.colorize("FFFFFFFFFFFFFFFF01FFFFFF2222200101010101010106B6B001B0B0B6BFFFFFF01FFFFFF01FFDD3");
			Program.colorize("FFFFFFFFFFFFFFFF01FFFFD8D8D8D8D80D80D8D8D8D8D8D8D8D1060B0601AFFFF01FFDD333333333");
			Program.colorize("FFFFFFFFFFFFFFFF01B116B6B6B6B6B6B6B6B6B6B6B6B6DD10101333333333333333333333333333");
			Program.colorize("FFFFFFFFFFFFFFF101010101010101010101010101013333B6B6B633333333333333333333333333");
			Program.colorize("FFFFFFFFFFFFFFF06060606060606063333333333333333333333333333333333333333333333333");
			Program.colorize("FFFFFFFFFFDDDD003333333333333333333333333333333333333333333333333333333333333333");
			Program.colorize("FFFDDDDD33333333333333333333333333333333333333333333333333333333333333333333333");
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.BackgroundColor = ConsoleColor.Black;
			Console.SetCursorPosition(0, 2);
			Console.Write(string.Concat("\t", Program.consoleTitle));
		}

		private static void Main(string[] args)
		{
			Program.setup();
			Program.drawTitle();
			Console.ReadKey();
			Console.Clear();
			Console.CursorVisible = true;
			while (Program.open)
			{
				Console.Write(Program.waitText);
				Program.action = Program.waitForInput(Console.ReadLine());
				if (Program.action == -1)
				{
					Program.open = false;
				}
				else if (Program.action != 0)
				{
					int num = Program.action;
				}
				else
				{
					Console.WriteLine(Program.unrecognizedCMD);
				}
			}
		}

		public static void setup()
		{
			Console.OutputEncoding = Encoding.Unicode;
			Console.InputEncoding = Encoding.Unicode;
			Console.SetWindowSize(Program.consoleWidth, Program.consoleHeight);
			Console.SetBufferSize(Program.consoleWidth, Program.consoleHeight * 100);
			Console.Title = Program.consoleTitle;
			Console.CursorVisible = false;
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.BackgroundColor = ConsoleColor.Black;
		}

		private static string stringOf(char c, int numberOfTimes)
		{
			string str = "";
			for (int i = 0; i < numberOfTimes; i++)
			{
				str = string.Concat(str, c.ToString());
			}
			return str;
		}

		private static int waitForInput(string input)
		{
			int num;
			string[] strArrays = input.Split(new char[] { ' ' });
			if (strArrays[0].ToLower() == Program.command[1].ToLower())
			{
				return -1;
			}
			if (strArrays[0].ToLower() == Program.command[0].ToLower())
			{
				Console.WriteLine(string.Concat("\n", Program.helpTitle));
				for (int i = 0; i < (int)Program.command.Length; i++)
				{
					Console.WriteLine(Program.command[i]);
					Console.WriteLine(string.Concat(Program.commandHelp[i], "\n"));
				}
				return 1;
			}
			if (strArrays[0].ToLower() == Program.command[2].ToLower())
			{
				Console.Write("\n");
				if ((int)strArrays.Length > 1 && !File.Exists(strArrays[1]))
				{
					Console.WriteLine(Program.fileNotFound);
					return 1;
				}
				if ((int)strArrays.Length != 2)
				{
					if ((int)strArrays.Length == 3)
					{
						try
						{
							BinaryReader binaryReader = new BinaryReader(File.Open(strArrays[1], FileMode.Open));
							BinaryWriter binaryWriter = new BinaryWriter(File.Open(strArrays[2], FileMode.Create));
							while (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
							{
								binaryWriter.Write(Encoding.ASCII.GetBytes(BitConverter.ToString(binaryReader.ReadBytes(1))));
							}
							Console.WriteLine(string.Concat("ID OF ", strArrays[1], " STORED IN ", strArrays[2]));
							binaryReader.Close();
							binaryWriter.Close();
							binaryReader.Dispose();
							binaryWriter.Dispose();
						}
						catch
						{
							Console.WriteLine(Program.fileError);
						}
						return 1;
					}
					Console.WriteLine(Program.syntaxError);
				}
				else
				{
					using (BinaryReader binaryReader1 = new BinaryReader(File.Open(strArrays[1], FileMode.Open)))
					{
						while (binaryReader1.BaseStream.Position != binaryReader1.BaseStream.Length)
						{
							Console.Write(BitConverter.ToString(binaryReader1.ReadBytes(1)));
						}
					}
					Console.Write("\n");
				}
				return 1;
			}
			if (strArrays[0].ToLower() == Program.command[3].ToLower())
			{
				if ((int)strArrays.Length != 3)
				{
					Console.WriteLine(string.Concat("\n", Program.syntaxError));
					return 1;
				}
				if (!File.Exists(strArrays[1]))
				{
					Console.WriteLine(Program.fileNotFound);
					return 1;
				}
				try
				{
					BinaryReader binaryReader2 = new BinaryReader(File.Open(strArrays[1], FileMode.Open));
					BinaryWriter binaryWriter1 = new BinaryWriter(File.Open(strArrays[2], FileMode.Create));
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.CursorVisible = false;
					Console.WriteLine("\n");
					while (binaryReader2.BaseStream.Position != binaryReader2.BaseStream.Length)
					{
						double position = (double)binaryReader2.BaseStream.Position / (double)binaryReader2.BaseStream.Length;
						byte[] numArray = Program.ASCIItoByte(binaryReader2.ReadBytes(2));
						if (numArray.Length != 0)
						{
							binaryWriter1.Write(numArray[0]);
							Console.SetCursorPosition(0, Console.CursorTop - 1);
							Console.WriteLine(string.Concat(Program.retrievingFileStart, Program.stringOf(Program.block, (int)Math.Round(position * 20)), Program.stringOf(' ', 20 - (int)Math.Round(position * 20)), Program.retrievingFileEnd));
						}
						else
						{
							Console.WriteLine(Program.formatError);
							break;
						}
					}
					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.CursorVisible = true;
					binaryReader2.Close();
					binaryWriter1.Close();
					binaryReader2.Dispose();
					binaryWriter1.Dispose();
				}
				catch
				{
					Console.WriteLine(string.Concat("\n", Program.fileError));
				}
				return 1;
			}
			if (strArrays[0].ToLower() != Program.command[4].ToLower())
			{
				if (strArrays[0].ToLower() != Program.command[5].ToLower())
				{
					if (strArrays[0].ToLower() == Program.command[6].ToLower())
					{
						Console.Clear();
						return 1;
					}
					Console.Write(input);
					return 0;
				}
				if ((int)strArrays.Length == 1)
				{
					Console.WriteLine(string.Concat("\n", Program.needFile));
					return 1;
				}
				if ((int)strArrays.Length != 2)
				{
					Console.WriteLine(string.Concat("\n", Program.syntaxError));
					return 1;
				}
				Console.Write(Program.mustConfirm);
				Console.ForegroundColor = ConsoleColor.Blue;
				Console.Write(Program.yesNo);
				Console.ForegroundColor = ConsoleColor.DarkGreen;
				if (Console.ReadLine().Substring(0, 1).ToLower() == "y")
				{
					if (!Directory.Exists(strArrays[1]))
					{
						try
						{
							Directory.CreateDirectory(strArrays[1]);
						}
						catch
						{
							Console.WriteLine(Program.folderNotFound);
							num = 1;
							return num;
						}
					}
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.CursorVisible = false;
					if (!Program.createDatabase(strArrays[1]))
					{
						Console.WriteLine(string.Concat(Program.notSucess, strArrays[1]));
					}
					else
					{
						Console.WriteLine(Program.sucess);
					}
					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.CursorVisible = true;
				}
				return 1;
			}
			else
			{
				if ((int)strArrays.Length != 3)
				{
					Console.WriteLine(string.Concat("\n", Program.syntaxError));
					return 1;
				}
				try
				{
					BigInteger bigInteger = BigInteger.Parse(strArrays[1]);
					BigInteger bigInteger1 = bigInteger;
					try
					{
						BinaryWriter binaryWriter2 = new BinaryWriter(File.Open(strArrays[2], FileMode.Create));
						Console.ForegroundColor = ConsoleColor.Blue;
						Console.CursorVisible = false;
						Console.WriteLine("\n");
						BigInteger bigInteger2 = bigInteger / 20;
						if (bigInteger2 == (long)0)
						{
							bigInteger2 = 1;
						}
						while (bigInteger1 != (long)0)
						{
							binaryWriter2.Write((byte)Program.RNG.Next(0, 256));
							bigInteger1 = bigInteger1--;
							BigInteger bigInteger3 = bigInteger1 / bigInteger2;
							if (bigInteger3 > (long)20)
							{
								bigInteger3 = 0;
							}
							Console.SetCursorPosition(0, Console.CursorTop - 1);
							Console.WriteLine(string.Concat(Program.retrievingFileStart, Program.stringOf(Program.block, 20 - (int)bigInteger3), Program.stringOf(' ', (int)bigInteger3), Program.retrievingFileEnd));
						}
						binaryWriter2.Close();
						binaryWriter2.Dispose();
						Console.ForegroundColor = ConsoleColor.DarkGreen;
						Console.CursorVisible = true;
						num = 1;
					}
					catch (Exception exception)
					{
						Console.WriteLine(Program.fileError);
						num = 1;
					}
				}
				catch
				{
					Console.WriteLine(string.Concat("\n", Program.syntaxError));
					num = 1;
				}
			}
			return num;
		}
	}
}
