using System;
using System.IO;
using System.Text;

namespace DailyBreadUtil
{
	/// <summary>
	/// Summary description for parsing_util.
	/// </summary>
	public class parsing_util
	{
        private frmDBValidator vForm = new frmDBValidator();
        private int expDBNumber;
        private int NumberofDBPages = 0;
        private string tmpFileName = "___tmp_DailyBread.tmp";

        public int GetPageCount
        {
            get
            {
                return NumberofDBPages;
            }
        }


		public parsing_util(int dbNum)
		{
            expDBNumber = dbNum;
		}

		public string [] SplitFileByPage(string FileName)
		{
			StreamReader fileReader = null;
            BinaryReader bfileReader = null;
            BinaryWriter bfileWriter = null;

            string[] dblines;
            string[] dbpages = new string[500];

            int NumRead = 0;
            int uDBLinesCount = 0;
            int i = 0;
            int j = 0;

			try
			{
                if (File.Exists(tmpFileName)) File.Delete(tmpFileName);

                bfileReader = new BinaryReader(File.Open(FileName, FileMode.Open, FileAccess.Read));
                bfileWriter = new BinaryWriter(File.Open(tmpFileName, FileMode.OpenOrCreate, FileAccess.Write));

                StringBuilder fileContent = new StringBuilder ();

				char[] buffer = new char[32768];
                byte[] bbuffer = new byte[32768];
				
                while (bfileReader.PeekChar() != -1)
                {
                    bbuffer = bfileReader.ReadBytes(bbuffer.Length);

                    PrintTemp(bbuffer, bfileWriter);
				}

                bfileWriter.Close();

                fileReader = new StreamReader(tmpFileName);

                while ((NumRead = fileReader.ReadBlock(buffer, 0, buffer.Length)) > 0)
                {
                    fileContent.Append(buffer, 0, NumRead);
                    buffer = new char[32768];
                }

				fileContent = fileContent.Replace("\r\n", "\n");

                dblines = fileContent.ToString().Split('\n');

                uDBLinesCount = ValidateDB(dblines);

                for (i = 0, j = 0; i <= uDBLinesCount; )
                {
                    fileContent.Length = 0;

                    do
                    {
                        fileContent.Append(dblines[i++]);
                        fileContent.Append('\n');

                    } while (!PageEnd(dblines, i));

                    if (fileContent.ToString().Trim().Length != 0)
                        dbpages[j++] = fileContent.ToString();
                }

                NumberofDBPages = j - 1;

                return dbpages;
			}
			catch (Exception ex)
			{
				throw (new IOException ("Unable to read " + FileName, ex));
			}
			finally
			{
                if (fileReader != null)
                {
                    fileReader.Close();
                    //File.Delete(tmpFileName);
                }
			}
		}

        public bool PageEnd(string[] p, int i)
        {
            try
            {
                if (p[i - 1].ToLower().ToLower().StartsWith("one word"))
                    return true;

                //if (i < (p.Length - 1))
                //    if (IsIntro(p[i + 1].Trim()) || IsDate(p[i + 1].Trim())) //if (IsTitle(p[i + 1].Trim()))
                //        return true;

                if (i < (p.Length - 1))
                    if (IsIntro(p[i].Trim()) || IsDate(p[i].Trim())) //if (IsTitle(p[i + 1].Trim()))
                        return true;

                return false;
            }
            catch (Exception ex)
            {
                throw (new IndexOutOfRangeException("Index out of range", ex));
            }
        }

        public bool IsTitle(string t)
        {
            try
            {
                if (t.Trim().Length == 0)
                    return false;

                for (int i = 0; i < t.Length; i++)
                    if ((int)t[i] >= 97 && (int)t[i] <= 122)
                        return false;

                return true;
            }
            catch (Exception ex)
            {
                throw (new IndexOutOfRangeException("Index out of range", ex));
            }
        }

        public bool IsIntro(string t)
        {
            if (t.Trim().Length == 0)
                return false;

            if (t.IndexOf("INTRODUCTION") == -1)
                return false;

            return true;
        }

        public bool IsDate(string t)
        {
            string[] splittedText;

            splittedText = t.Split(' ');
            
            if (splittedText.Length != 3)
                return false;

            if (IsDay(splittedText[0].Trim()) && IsMonth(splittedText[1].Trim()) && IsNDate(splittedText[2].Trim()))
                return true;

            return false;
        }

        public bool IsDay(string t)
        {
            if (t.ToLower().StartsWith("monday"))
                return true;
            else if (t.ToLower().StartsWith("tuesday"))
                return true;
            else if (t.ToLower().StartsWith("wednesday"))
                return true;
            else if (t.ToLower().StartsWith("thursday"))
                return true;
            else if (t.ToLower().StartsWith("friday"))
                return true;
            else if (t.ToLower().StartsWith("saturday"))
                return true;
            else if (t.ToLower().StartsWith("sunday"))
                return true;

            return false;
        }

        public bool IsMonth(string t)
        {
            if (t.ToLower().StartsWith("january"))
                return true;
            else if (t.ToLower().StartsWith("february"))
                return true;
            else if (t.ToLower().StartsWith("march"))
                return true;
            else if (t.ToLower().StartsWith("april"))
                return true;
            else if (t.ToLower().StartsWith("may"))
                return true;
            else if (t.ToLower().StartsWith("june"))
                return true;
            else if (t.ToLower().StartsWith("july"))
                return true;
            else if (t.ToLower().StartsWith("june"))
                return true;
            else if (t.ToLower().StartsWith("august"))
                return true;
            else if (t.ToLower().StartsWith("september"))
                return true;
            else if (t.ToLower().StartsWith("october"))
                return true;
            else if (t.ToLower().StartsWith("november"))
                return true;
            else if (t.ToLower().StartsWith("december"))
                return true;

            return false;
        }

        public bool IsNDate(string t)
        {
            int i;

            if (int.TryParse(t, out i) == true)
                if (i > 0 && i < 32)
                    return true;
            return false;
        }

		public string [] SplitPageByLine(string FilePage)
		{
			return FilePage.ToString ().Split ('\n');
		}

        public bool PrintTemp(byte[] s, BinaryWriter w)
        {
            int i;
            bool triple_dot = false;

            for (i = 0; i < s.Length; i++)
            {
                if (((int)s[i] < 32 || (int)s[i] > 126) && (((int)s[i] != 9) && ((int)s[i] != 10) && ((int)s[i] != 13) && ((int)s[i] != 30)))
                {
                    switch((int)s[i])
                    {
                        case 145:
                            s[i] = (byte)'\'';
                            break;
                        case 146:
                            s[i] = (byte)'\'';
                            break;
                        case 147:
                            s[i] = (byte)'\"';
                            break;
                        case 148:
                            s[i] = (byte)'\"';
                            break;
                        case 150:
                            s[i] = (byte)'-';
                            break;
                        case 151:
                            s[i] = (byte)'-';
                            break;
                        case 160:
                            s[i] = (byte)' ';
                            break;
                        case 133:
                            s[i] = (byte)'.';
                            triple_dot = true;
                            break;
                        default:
                            s[i] = (byte)vForm.RaplaceChar((char)s[i], i);
                            break;
                    }
                }
            
                w.Write(s[i]);

                if (triple_dot == true)
                {
                    w.Write((byte)'.');
                    w.Write((byte)'.');

                    triple_dot = false;
                }
            }
            
            return true;
        }

        public int ValidateDB(string[] db)
        {
            int i;
            int rValue = -1;

            int kCount = 0;
            int pCount = 0;
            int oCount = 0;

            for (i = 0; i < db.Length; i++)
            {
                if (db[i].ToLower().Trim().StartsWith("key verse"))
                    kCount++;
                else if (db[i].ToLower().Trim().StartsWith("prayer"))
                    pCount++;
                else if (db[i].ToLower().Trim().StartsWith("one word"))
                {
                    oCount++;
                    rValue = i;
                }
            }

            return rValue;
        }
	}
}
