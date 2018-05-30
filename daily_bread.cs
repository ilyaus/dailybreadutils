using System;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Xml;
using System.Globalization;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


namespace DailyBreadUtil
{
	/// <summary>
	/// Summary description for daily_bread.
	/// </summary>
	public class daily_bread
	{
        string strTmpFolder = "c:\\Temp\\db\\";
        db_data [] data = new db_data [500];
        string[] gl_dbLines;

		int dbCount = 0;
        int gl_index = 0;

        private configuration config = new configuration();

        const int TITLE     = 100;
        const int PASSAGE   = 101;
        const int DATE      = 102;
        const int KEYVERSE  = 103;
        const int PTITLE    = 104;
        const int PCONTENT  = 105;
        const int PRAYER    = 106;
        const int ONEWORD   = 107;

        const int SUBTITLE  = 108;
        const int INFO      = 109;
        const int INTRO     = 110;
        const int CONTENTS  = 111;
        const int KVERSETXT = 112;

        const int PCONTENT_V2    = 115;
        const int KVERSETXT_V2   = 116;
        const int PRAYER_V2      = 117;


        string[] bbooks = {
            "Genesis",
            "Exodus",
            "Leviticus",
            "Numbers",
            "Deuteronomy",
            "Joshua",
            "Judges",
            "Ruth",
            "1 Samuel",
            "2 Samuel",
            "1 Kings",
            "2 Kings",
            "1 Chronicles",
            "2 Chronicles",
            "Ezra",
            "Nehemiah",
            "Esther",
            "Job",
            "Psalm",
            "Proverbs",
            "Ecclesiastes",
            "Song of Songs",
            "Isaiah",
            "Jeremiah",
            "Lamentations",
            "Ezekiel",
            "Daniel",
            "Hosea",
            "Joel",
            "Amos",
            "Obadiah",
            "Jonah",
            "Micah",
            "Nahum",
            "Habakkuk",
            "Zephaniah",
            "Haggai",
            "Zechariah",
            "Malachi",
            "Matthew",
            "Mark",
            "Luke",
            "John",
            "Acts",
            "Romans",
            "1 Corinthians",
            "2 Corinthians",
            "Galatians",
            "Ephesians",
            "Philippians",
            "Colossians",
            "1 Thessalonians",
            "2 Thessalonians",
            "1 Timothy",
            "2 Timothy",
            "Titus",
            "Philemon",
            "Hebrews",
            "James",
            "1 Peter",
            "2 Peter",
            "1 John",
            "2 John",
            "3 John",
            "Jude",
            "Revelation",  
        };

		public daily_bread()
		{
		}
        
        public bool ParseDB(string dbPage)
        {
            parsing_util dbParser = new parsing_util(-1);

            gl_dbLines = dbParser.SplitPageByLine(dbPage);

            if (data[dbCount] == null) data[dbCount] = new db_data();

            gl_index = 0;

            DB_Structure();

            return true;
        }

        
        public void DB_Structure()
        {
            int intNumOfParagraphs = countParagraphs(gl_dbLines);

            if (CheckAhead() == CONTENTS)
                return;

            if (CheckAhead() == INTRO)
            {
                match(INTRO);
            }
            else
            {
                match(DATE);
                match(TITLE);
                
                //match(SUBTITLE);
                
                match(PASSAGE);
                
                //match(DATE);
                //match(INFO);
                //match(KEYVERSE);
                
                match(KVERSETXT_V2);

                /*
                while (CheckAhead() == PTITLE)
                {
                    match(PTITLE);
                    match(PCONTENT);
                }
                */

                for (int i = 0; i < intNumOfParagraphs; i++)
                {
                    match(PCONTENT_V2);
                }

                match(PRAYER_V2);

                //match(PRAYER);
                
                match(ONEWORD);

                dbCount++;
            }
        }

        void match(int key)
        {
            string tmpString;
            string[] splittedLines;

            for (; gl_dbLines[gl_index].Trim() == ""; gl_index++) ;

            switch (key)
            {
                case INTRO:
                    data[dbCount].fintro.title = gl_dbLines[gl_index++].Trim();

                    for (; gl_index < gl_dbLines.Length; gl_index++)
                    {
                        if (gl_dbLines[gl_index].Trim() != "")
                        {
                            data[dbCount].fintro.content[data[dbCount].fintro.numofparagraphs++] = gl_dbLines[gl_index].Trim();
                        }
                    }

                    break;
                case TITLE:
                    data[dbCount].Title = gl_dbLines[gl_index++].Trim();

                    break;
                case SUBTITLE:
                    if (CheckAhead() != PASSAGE)
                    {
                        data[dbCount].SubTitle = gl_dbLines[gl_index++].Trim();
                    }

                    break;
                case PASSAGE:
                    string tbook = "";
                    string tver = "";

                    tmpString = gl_dbLines[gl_index++].Trim();
                    tmpString = tmpString.Replace("     ", "\t");
                    splittedLines = tmpString.Split('\t');

                    data[dbCount].Passage = splittedLines[0].Trim();

                    splittedLines = splittedLines[0].Split(':');

                    if (splittedLines.Length == 1)
                    {
                        splittedLines = splittedLines[0].Split(' ');

                        tbook = splittedLines[0].Trim();
                        tver = splittedLines[1].Trim();
                    }
                    else if (splittedLines.Length > 0)
                    {
                        for (int j = 1; j < splittedLines.Length; j++)
                            tver = tver + splittedLines[j] + ":";

                        tver = tver.Remove(tver.Length - 1);

                        splittedLines = splittedLines[0].Split(' ');

                        tver = splittedLines[splittedLines.Length - 1] + ":" + tver;

                        for (int j = 0; j < splittedLines.Length - 1; j++)
                            tbook = tbook + splittedLines[j] + " ";
                    }
                                       
                    data[dbCount].fpassage.db_book = tbook.Trim();
                    data[dbCount].fpassage.db_npassage = tver.Trim();

                    break;
                case DATE:
                    tmpString = gl_dbLines[gl_index++].Trim();
                    tmpString = tmpString.Replace("     ", "\t");
                    splittedLines = tmpString.Split('\t');

                    data[dbCount].Date = splittedLines[0].Trim();

                    splittedLines = splittedLines[0].Trim().Split(' ');

                    data[dbCount].fdate.db_month = splittedLines[1].Trim();
                    data[dbCount].fdate.db_ndate = splittedLines[2].Trim();
                    data[dbCount].fdate.db_day = GetCompleteDay(splittedLines[0].Trim());

                    /*
                    data[dbCount].Date = splittedLines[splittedLines.Length - 1].Trim();

                    splittedLines = splittedLines[splittedLines.Length - 1].Trim().Split(' ');

                    data[dbCount].fdate.db_month = splittedLines[1].Trim();
                    data[dbCount].fdate.db_ndate = splittedLines[2].Trim();
                    data[dbCount].fdate.db_day = GetCompleteDay(splittedLines[0].Trim());
                    */

                    break;
                case INFO:
                    if (CheckAhead() != KEYVERSE)
                    {
                        data[dbCount].fpassage.db_npassage_info = gl_dbLines[gl_index++].Trim();
                    }

                    break;
                case KEYVERSE:
                    tmpString = gl_dbLines[gl_index++].Trim();
                    splittedLines = tmpString.Split(':');

                    if (splittedLines[0].ToLower().Contains("key verse"))
                    {
                        data[dbCount].fkeyverse.db_book = data[dbCount].fpassage.db_book;

                        if (splittedLines.Length == 3)
                        {
                            data[dbCount].fkeyverse.db_chapter = splittedLines[1].Trim();
                            data[dbCount].fkeyverse.db_verses = splittedLines[2].Trim();
                            data[dbCount].CompleteKeyVerse = data[dbCount].fpassage.db_book + " " + splittedLines[1].Trim() + ":" + splittedLines[2].Trim();
                        }
                        else if (splittedLines.Length == 2)
                        {
                            data[dbCount].fkeyverse.db_chapter = "";
                            data[dbCount].fkeyverse.db_verses = splittedLines[1].Trim();
                            data[dbCount].CompleteKeyVerse = data[dbCount].fpassage.db_book + " " + splittedLines[1].Trim();
                        }
                        else
                        {
                            data[dbCount].fkeyverse.db_chapter = "";
                            data[dbCount].fkeyverse.db_verses = "";
                            data[dbCount].CompleteKeyVerse = data[dbCount].fpassage.db_book + "NEED UPDDATE" ;
                        }

                        //data[dbCount].KeyVerseText = GetKVText(dbCount);
                    }

                    break;
                case KVERSETXT:
                    tmpString = gl_dbLines[gl_index++].Trim();

                    data[dbCount].fkeyverse.db_ktext = tmpString;
                    data[dbCount].KeyVerseText = tmpString;
                    
                    break;
                case KVERSETXT_V2:
                    tmpString = gl_dbLines[gl_index++].Trim();

                    string strVerseText = tmpString.Substring(0, tmpString.LastIndexOf('(')).Trim();
                    string strVerseNum = tmpString.Substring(tmpString.LastIndexOf('(') + 1, (tmpString.LastIndexOf(')') - tmpString.LastIndexOf('(')) - 1);

                    if (strVerseText.Substring(0, 1) == "\"") strVerseText = strVerseText.Substring(1);
                    if (strVerseText.Substring(strVerseText.Length - 1) == "\"") strVerseText = strVerseText.Substring(0, strVerseText.Length - 1);

                    data[dbCount].fkeyverse.db_ktext = strVerseText;
                    data[dbCount].KeyVerseText = strVerseText;

                    splittedLines = strVerseNum.Split(':');

                    if (splittedLines.Length > 1)
                    {
                        data[dbCount].fkeyverse.db_chapter = splittedLines[0].Trim();
                        data[dbCount].fkeyverse.db_verses = splittedLines[1].Trim();
                    }
                    else
                    {
                        string[] stmp = data[dbCount].fpassage.db_npassage.Split(':');

                        if (stmp.Length == 2)
                        {
                            data[dbCount].fkeyverse.db_chapter = stmp[0].Trim();
                        }
                        else
                        {
                            data[dbCount].fkeyverse.db_chapter = "";
                        }

                        data[dbCount].fkeyverse.db_verses = splittedLines[0].Trim();
                    }

                    if (data[dbCount].fkeyverse.db_chapter.Equals(""))
                    {
                        data[dbCount].CompleteKeyVerse = data[dbCount].fpassage.db_book + " " +
                                                         data[dbCount].fkeyverse.db_verses;
                    }
                    else
                    {
                        data[dbCount].CompleteKeyVerse = data[dbCount].fpassage.db_book + " " +
                                                         data[dbCount].fkeyverse.db_chapter + ":" +
                                                         data[dbCount].fkeyverse.db_verses;
                    }

                    break;
                case PTITLE:
                        tmpString = gl_dbLines[gl_index++].Trim();
                        splittedLines = tmpString.Split('.');

                        data[dbCount].fcontent.paragraphs[data[dbCount].fcontent.numofparagraphs] = new db_paragraph();
                        data[dbCount].fcontent.paragraphs[data[dbCount].fcontent.numofparagraphs].num = int.Parse(splittedLines[0]);

                        for (int j = 1; j < splittedLines.Length; j++)
                        {
                            data[dbCount].fcontent.paragraphs[data[dbCount].fcontent.numofparagraphs].title += splittedLines[j].Trim() + ".";
                        }

                        //data[dbCount].fcontent.paragraphs[data[dbCount].fcontent.numofparagraphs].title = splittedLines[1].Trim();
                    break;
                case PCONTENT:
                    bool boolContentEnded = false;
                    do
                    {
                        data[dbCount].fcontent.paragraphs[data[dbCount].fcontent.numofparagraphs].content += gl_dbLines[gl_index++].Trim();

                        if (CheckAhead() != PTITLE && CheckAhead() != PRAYER)
                        {
                            data[dbCount].fcontent.paragraphs[data[dbCount].fcontent.numofparagraphs].content += "\r\n";
                        }
                        else
                        {
                            boolContentEnded = true;
                        }
                    } while (boolContentEnded == false);

                    data[dbCount].fcontent.numofparagraphs++;

                    break;
                case PCONTENT_V2:
                    data[dbCount].fcontent.paragraphs[data[dbCount].fcontent.numofparagraphs] = new db_paragraph();
                    data[dbCount].fcontent.paragraphs[data[dbCount].fcontent.numofparagraphs].content += gl_dbLines[gl_index++].Trim();
                    data[dbCount].fcontent.numofparagraphs++;

                    break;
                case PRAYER:
                    tmpString = gl_dbLines[gl_index++].Trim();
                    splittedLines = tmpString.Split(':');

                    data[dbCount].Prayer = splittedLines[1].Trim();

                    break;
                case PRAYER_V2:
                    tmpString = gl_dbLines[gl_index++].Trim();
                    data[dbCount].Prayer = tmpString;

                    break;
                case ONEWORD:
                    tmpString = gl_dbLines[gl_index++].Trim();
                    splittedLines = tmpString.Split(':');

                    data[dbCount].OneWord = splittedLines[1].Trim();

                    break;
                default:
                    break;
            }
        }

        public int CheckAhead()
        {
            int local_index = gl_index;
            int i;
            string[] splittedLines;

            for (; gl_dbLines[local_index].Trim() == ""; local_index++) ;

            splittedLines = gl_dbLines[local_index].Trim().Split(' ');

            if (gl_dbLines[local_index].IndexOf('.') != -1)
                if (int.TryParse(gl_dbLines[local_index].Substring(0, gl_dbLines[local_index].IndexOf('.')), out i) == true) return PTITLE;
            
            if (splittedLines[0].ToLower().Contains("key")) return KEYVERSE;
            if (splittedLines[0].ToLower().Contains("prayer")) return PRAYER;
            if (splittedLines[0].ToLower().Contains("one")) return ONEWORD;
            if (splittedLines[0].ToLower().Contains("introduction")) return INTRO;
            if (splittedLines[0].Contains("CONTENTS")) return CONTENTS;

            splittedLines = gl_dbLines[local_index].Split(':');

            if (splittedLines.Length == 1)
            {
                splittedLines = splittedLines[0].Split(' ');

                if (CheckBibleBook(splittedLines[0].Trim())) return PASSAGE;
            }

            else if (splittedLines.Length > 0)
            {
                string tstr = "";

                splittedLines = splittedLines[0].Split(' ');

                for (int j = 0; j < splittedLines.Length - 1; j++)
                    tstr = tstr + splittedLines[j] + " ";

                if (CheckBibleBook(tstr.Trim())) return PASSAGE;
            }

            return -1;
        }

        public int GetDB_Count()
		{
			return dbCount;
		}

        public string GetCompleteDay(string d)
        {
            string rstr = null;

            if (d.ToLower().Contains("mon")) rstr = "Monday";
            else if (d.ToLower().Contains("tue")) rstr = "Tuesday";
            else if (d.ToLower().Contains("wed")) rstr = "Wednesday";
            else if (d.ToLower().Contains("thu")) rstr = "Thursday";
            else if (d.ToLower().Contains("fri")) rstr = "Friday";
            else if (d.ToLower().Contains("sat")) rstr = "Saturday";
            else if (d.ToLower().Contains("sun")) rstr = "Sunday";

            return rstr;
        }

        public int GetMonthNumber(string m)
        {
            switch (m.Trim().ToLower())
            {
                case "january":
                case "jan.":
                    return 1;
                case "february":
                case "feb.":
                    return 2;              
                case "march":
                case "mar.":
                    return 3; 
                case "april":
                case "apr.":
                    return 4; 
                case "may":
                    return 5; 
                case "june":
                case "jun.":
                    return 6;
                case "july":
                case "jul.":
                    return 7;
                case "august":
                case "aug.":
                    return 8;
                case "september":
                case "sept.":
                    return 9;
                case "october":
                case "oct.":
                    return 10;
                case "november":
                case "nov.":
                    return 11;
                case "december":
                case "dec.":
                    return 12;
                default:
                    return -1;
            }
        }

        public bool CheckBibleBook(string b)
        {
            for (int i = 0; i <= bbooks.Length - 1; i++)
                if (bbooks[i].ToLower().Equals(b.ToLower().Trim())) return true;
            
            return false;
        }

		public void GetDBData ()
		{
			for (int index = 0; index < dbCount; index++)
			{
				//Console.WriteLine (data [index].Introduction);
				//Console.WriteLine (data [index].Title);
				//Console.WriteLine (data [index].Date + " " + data [index].Title);
				//Console.WriteLine (data [index].Passage);
				//Console.WriteLine (data [index].KeyVerse);
				//Console.WriteLine (data [index].Body);
				//Console.WriteLine (data [index].Prayer);
				//Console.WriteLine (data [index].OneWord);
				//Console.WriteLine ("---------------------------");
			}
		}

        public void UpdateDB(int year)
        {
            string first_month = null;
            int update_year = 0;
            int tyear;

            first_month = data[0].fdate.db_month.ToLower();

            for (int i = 0; i < dbCount; i++)
            {
                if (first_month != data[i].fdate.db_month.ToLower())
                {
                    if (data[i].fdate.db_month.ToLower() == "january") update_year = 1;
                }

                tyear = year + update_year;
                data[i].fdate.db_year = tyear.ToString();
            }
        }

        public void MakeDBFiles()
        {
            int i;
            StringBuilder tHTMLText = new StringBuilder();
            StringBuilder tRSSText = new StringBuilder();

            for (int index = 0; index < dbCount; index++)
            {
                FileStream file = new FileStream(strTmpFolder + data[index].fdate.db_month + "." + data[index].fdate.db_ndate + ".html", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(file);

                tHTMLText.Length = 0;
                tRSSText.Length = 0;

                tHTMLText.Append("<script language=\"JavaScript\">\n");
                tHTMLText.Append("<!--\n");
                tHTMLText.Append("function NewWin(t) {NewBibleWin = open(t,\"Passage\",\"resizable=yes,scrollbars=yes,width=600,height=450\");}\n");
                tHTMLText.Append("// -->\n");
                tHTMLText.Append("</script>\n");

                if (data[index].fintro.numofparagraphs > 0)
                {
                    tHTMLText.Append("<div class=\"messagetitle\" align=\"center\">" + data[index].fintro.title + "</div>");
                    tHTMLText.Append("<br /><br />");

                    tRSSText.Append("<b><p align=\"center\"><h3>" + data[index].fintro.title + "</h3></p></b>");
                    tRSSText.Append("<br /><br />");

                    for (i = 1; i <= data[index].fintro.numofparagraphs; i++)
                    {
                        tHTMLText.Append(data[index].fintro.content[i - 1]);
                        tHTMLText.Append("<br /><br />");

                        tRSSText.Append(data[index].fintro.content[i - 1]);
                        tRSSText.Append("<br /><br />");

                    }
                }

                tHTMLText.Append("<div class=\"messagetitle\" align=\"center\">" + data[index].Title + "</div>");

                tHTMLText.Append("<table border=\"0\" width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">");
                tHTMLText.Append("<tr><td align=left><a onclick=\"NewWin('http://www.biblegateway.net/passage/index.php?language=English&search=" + data[index].fpassage.db_book + "+" + data[index].fpassage.db_npassage + "&version=31&interface=print" + "')\" href=\"#\">" + data[index].Passage + "</a>&nbsp;" + data[index].fpassage.db_npassage_info + "</td><td align=right class=\"board_list\">" + data[index].Date + "</td></tr>");
                tHTMLText.Append("</table>");

                tRSSText.Append("<b><p align=\"center\"><h3>" + data[index].Title + "</h3></p></b>");

                tRSSText.Append("<a target=\"Passage\" href=\"http://www.biblegateway.net/passage/index.php?language=English&search=" + data[index].fpassage.db_book + "+" + data[index].fpassage.db_npassage + "&version=31&interface=print\">" + data[index].Passage + "</a>&nbsp;" + data[index].fpassage.db_npassage_info + "<br />");

                //tRSSText.Append("<table border=\"0\" width=\"80%\" cellspacing=\"0\" cellpadding=\"0\">");
                //tRSSText.Append("<tr><td align=left><a target=\"Passage\" href=\"http://www.biblegateway.net/passage/index.php?language=English&search=" + data[index].fpassage.db_book + "+" + data[index].fpassage.db_npassage + "&version=31&interface=print\">" + data[index].Passage + "</a>&nbsp;" + data[index].fpassage.db_npassage_info + "</td><td align=right class=\"board_list\">" + data[index].Date + "</td></tr>");
                //tRSSText.Append("</table>");


                if (data[index].fkeyverse.db_verses.Contains(","))
                {
                    tHTMLText.Append("Key Verses: " + ((data[index].fkeyverse.db_chapter.ToString().Trim() == "")?(data[index].fkeyverse.db_chapter + ":"):"") + data[index].fkeyverse.db_verses);

                    tRSSText.Append("Key Verses: " + data[index].fkeyverse.db_chapter + ":" + data[index].fkeyverse.db_verses);
                }

                else
                {
                    
                    //tHTMLText.Append("Key Verse: " + data[index].fkeyverse.db_chapter + ":" + data[index].fkeyverse.db_verses);
                    tHTMLText.Append("Key Verse: " + ((data[index].fkeyverse.db_chapter.ToString().Trim() != "") ? (data[index].fkeyverse.db_chapter + ":") : "") + data[index].fkeyverse.db_verses);

                    tRSSText.Append("Key Verse: " + data[index].fkeyverse.db_chapter + ":" + data[index].fkeyverse.db_verses);
                }
                
                tHTMLText.Append("<br /><div align=justify>");

                tRSSText.Append("<br /><div align=justify>");

                if (data[index].fcontent.numofparagraphs > 0)
                {
                    for (i = 1; i <= data[index].fcontent.numofparagraphs; i++)
                    {
                        //tHTMLText.Append("<b>" + i.ToString() + ". " + data[index].fcontent.paragraphs[i - 1].title + "</b>");
                        tHTMLText.Append("<br />" + data[index].fcontent.paragraphs[i - 1].content + "<br />");

                        //tRSSText.Append("<b>" + i.ToString() + ". " + data[index].fcontent.paragraphs[i - 1].title + "</b>");
                        tRSSText.Append("<br />" + data[index].fcontent.paragraphs[i - 1].content + "<br /><br />");
                    }
                }

                tHTMLText.Append("<br /></div>");
                tHTMLText.Append("<i>Prayer: " + data[index].Prayer + "</i>");
                tHTMLText.Append("<br /><br />");
                tHTMLText.Append("<b>One Word: " + data[index].OneWord + "</b>");

                tRSSText.Append("<br /></div>");
                tRSSText.Append("<i>Prayer: " + data[index].Prayer + "</i>");
                tRSSText.Append("<br /><br />");
                tRSSText.Append("<b>One Word: " + data[index].OneWord + "</b>");

                data[index].HTMLText = tHTMLText.ToString();
                data[index].RSSText = tRSSText.ToString();

                sw.Write(tHTMLText.ToString());

                sw.Close();
                file.Close();
            }

        }

        /*
         * mode = 1 - create text only RSS feed
         * mode = 2 - create graphical RSS feed
         */
        
        public void MakeRSSFiles(int mode)
        {
            XmlDocument xmldoc;
            //XmlNode xmlnode;

            XmlElement xmlRootElem;
 
            XmlElement xmlChannel;

            XmlElement xmlcTitle;
            XmlElement xmlcLink;
            XmlElement xmlcDescription;
            XmlElement xmlcLang;
            XmlElement xmlcCreator;
            XmlElement xmlcRights;
            XmlElement xmlcDate;
            XmlElement xmlcuPeriod;
            XmlElement xmlcuFreq;
            XmlElement xmlcuBase;

            XmlElement xmlItem;

            XmlElement xmliTitle;
            XmlElement xmliDescription;
            XmlElement xmliGuid;
            //XmlElement xmliContent;
            XmlElement xmliRights;
            XmlElement xmliDate;

            XmlText xmltext;

            String xmlData = "<?xml version=\"1.0\" ?><rss version=\"2.0\" xmlns:dc=\"http://purl.org/dc/elements/1.1/\" xmlns:sy=\"http://purl.org/rss/1.0/modules/syndication/\" xmlns:admin=\"http://webns.net/mvcb/\" xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\" xmlns:content=\"http://purl.org/rss/1.0/modules/content/\"></rss>";

            string DBData_Start = null;
            string DBData_End   = null;
            string xmlFileExt   = null;

            DateTime pubDate;
            string xmlpubDate   = null;

            if (mode == 2)
            {
                DBData_Start    = "<table border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\"><tr><td align=\"left\"><img src=\"http://www.ubf.org/global/images/dailybread_title_rss.jpg\" alt=\"Sign Up and Subscribe!\" border=\"0\"></a></td></tr><tr><td><div>";
                DBData_End      = "</div><p align=\"center\"></td></tr><tr><td height=\"1\"></td></tr><tr><td height=\"10\"></td></tr></table>";
                xmlFileExt      = "_gr.xml";
            }
            else
            {
                DBData_Start    = "";
                DBData_End      = "";
                xmlFileExt      = ".xml";
            }

            for (int index = 0; index < dbCount; index++)
            {
                pubDate = new DateTime(int.Parse(data[index].fdate.db_year), GetMonthNumber(data[index].fdate.db_month), int.Parse(data[index].fdate.db_ndate)); 
                xmlpubDate = pubDate.ToString("u").Substring(0, pubDate.ToString("u").IndexOf(' '));
                xmldoc = new XmlDocument();

                //xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
                //xmldoc.AppendChild(xmlnode);

                //xmlRootElem = xmldoc.CreateElement("", "rss", "");
                //xmldoc.AppendChild(xmlRootElem);

                xmldoc.Load(new StringReader(xmlData));
                
                xmlRootElem = xmldoc.DocumentElement;

                xmlChannel = xmldoc.CreateElement("", "channel", "");

                xmlcTitle = xmldoc.CreateElement("", "title", "");
                xmltext = xmldoc.CreateTextNode("UBF Daily Bread");
                xmlcTitle.AppendChild(xmltext);
                xmlChannel.AppendChild(xmlcTitle);

                xmlcLink = xmldoc.CreateElement("", "link", "");
                xmltext = xmldoc.CreateTextNode("http://www.ubf.org");
                xmlcLink.AppendChild(xmltext);
                xmlChannel.AppendChild(xmlcLink);

                xmlcDescription = xmldoc.CreateElement("", "description", "");
                xmltext = xmldoc.CreateTextNode("UBF Daily Bread");
                xmlcDescription.AppendChild(xmltext);
                xmlChannel.AppendChild(xmlcDescription);

                xmlcLang = xmldoc.CreateElement("dc", "language", "http://purl.org/dc/elements/1.1/");
                xmltext = xmldoc.CreateTextNode("en-us");
                xmlcLang.AppendChild(xmltext);
                xmlChannel.AppendChild(xmlcLang);

                xmlcCreator = xmldoc.CreateElement("dc", "creator", "http://purl.org/dc/elements/1.1/");
                xmltext = xmldoc.CreateTextNode("UBF");
                xmlcCreator.AppendChild(xmltext);
                xmlChannel.AppendChild(xmlcCreator);

                xmlcRights = xmldoc.CreateElement("dc", "rights", "http://purl.org/dc/elements/1.1/");
                xmltext = xmldoc.CreateTextNode("Copyright 2014");
                xmlcRights.AppendChild(xmltext);
                xmlChannel.AppendChild(xmlcRights);

                xmlcDate = xmldoc.CreateElement("dc", "date", "http://purl.org/dc/elements/1.1/");
                //xmlcDate.InnerText = pubDate.ToString("s") + "Z"; 
                xmlcDate.InnerText = xmlpubDate;
                xmlChannel.AppendChild(xmlcDate);

                xmlcuPeriod = xmldoc.CreateElement("sy", "updatePeriod", "http://purl.org/rss/1.0/modules/syndication/");
                xmltext = xmldoc.CreateTextNode("daily");
                xmlcuPeriod.AppendChild(xmltext);
                xmlChannel.AppendChild(xmlcuPeriod);

                xmlcuFreq = xmldoc.CreateElement("sy", "updateFrequency", "http://purl.org/rss/1.0/modules/syndication/");
                xmltext = xmldoc.CreateTextNode("1");
                xmlcuFreq.AppendChild(xmltext);
                xmlChannel.AppendChild(xmlcuFreq);

                //xmlcuBase = xmldoc.CreateElement("sy", "updateBase", "http://purl.org/rss/1.0/modules/syndication/");
                //xmltext = xmldoc.CreateTextNode("2000-01-01T12:00+00:00");
                //xmlcuBase.AppendChild(xmltext);
                //xmlChannel.AppendChild(xmlcuBase);

                xmlItem = xmldoc.CreateElement("", "item", "");

                xmliTitle = xmldoc.CreateElement("", "title", "");
                xmliTitle.InnerText = "Daily Bread";
                xmlItem.AppendChild(xmliTitle);

                xmliDescription = xmldoc.CreateElement("", "description", "");

                try
                {
                    xmliDescription.InnerText = DBData_Start + data[index].RSSText.ToString() + DBData_End;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                xmlItem.AppendChild(xmliDescription);

                xmliGuid = xmldoc.CreateElement("", "guid", "");
                xmliGuid.InnerText = "http://www.ubf.org/daily_bread.php";
                xmlItem.AppendChild(xmliGuid);

                //xmliContent = xmldoc.CreateElement("content", "encoded", "http://purl.org/rss/1.0/modules/content/");
                //xmliContent.InnerText = data[index].Body;
                //xmlItem.AppendChild(xmliContent);

                xmliRights = xmldoc.CreateElement("dc", "rights", "http://purl.org/dc/elements/1.1/");
                xmliRights.InnerText = "Daily Bread";
                xmlItem.AppendChild(xmliRights);

                xmliDate = xmldoc.CreateElement("dc", "date", "http://purl.org/dc/elements/1.1/");
                xmliDate.InnerText = xmlpubDate;
                //xmliDate.InnerText = pubDate.ToString("s") + "Z";
                xmlItem.AppendChild(xmliDate);

                xmlChannel.AppendChild(xmlItem);
                xmlRootElem.AppendChild(xmlChannel);

                try
                {
                    xmldoc.Save(strTmpFolder + data[index].fdate.db_month + "." + data[index].fdate.db_ndate + xmlFileExt);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }       
        }

        public void MakeXMLFile_v1()
        {
            XmlDocument xmldoc;
            XmlNode xmlnode;
            XmlElement xmlRootElem;
            XmlElement xmlDBElem;

            XmlElement xmlDB_date;
            XmlElement xmlDB_title;
            XmlElement xmlDB_passage;
            XmlElement xmlDB_keyverse;
            XmlElement xmlDB_keyversetext;
            XmlElement xmlDB_content;
            XmlElement xmlDB_prayer;
            XmlElement xmlDB_oneword;
            
            XmlText xmltext;

            xmldoc = new XmlDocument();
            string temp_string_holder = "";

            xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xmldoc.AppendChild(xmlnode);

            xmlRootElem = xmldoc.CreateElement("", "ubf_db", "");
            xmldoc.AppendChild(xmlRootElem);

            for (int index = 0; index < dbCount; index++)
            {
                xmlDBElem = xmldoc.CreateElement("", "ubf_db_one_day", "");
                xmldoc.ChildNodes.Item(1).AppendChild(xmlDBElem);

                DateTime d = DateTime.Parse(data[index].Date);

                xmlDB_date = xmldoc.CreateElement("", "db_date", "");

                xmltext = xmldoc.CreateTextNode(d.ToString("MM/dd/yyyy"));
                xmlDB_date.AppendChild(xmltext);
                xmlDBElem.AppendChild(xmlDB_date);

                if (data[index].fintro.numofparagraphs > 0)
                {
                    XmlElement xmlDB_intro_title = xmldoc.CreateElement("", "db_intro_title", "");
                    xmltext = xmldoc.CreateTextNode(data[index].fintro.title);
                    xmlDB_intro_title.AppendChild(xmltext);
                    xmlDBElem.AppendChild(xmlDB_intro_title);

                    XmlElement xmlDB_intro_text = xmldoc.CreateElement("", "db_intro_text", "");

                    temp_string_holder = "";

                    for (int i = 0; i < data[index].fintro.numofparagraphs; i++)
                    {
                        temp_string_holder += data[index].fintro.content[i] + ((i == (data[index].fintro.numofparagraphs - 1)) ? "" : "\n\n");
                    }

                    xmltext = xmldoc.CreateTextNode(temp_string_holder);
                    xmlDB_intro_text.AppendChild(xmltext);
                    xmlDBElem.AppendChild(xmlDB_intro_text);
                }
                
                xmlDB_title = xmldoc.CreateElement("", "db_title", "");
                xmltext = xmldoc.CreateTextNode(data[index].Title);
                xmlDB_title.AppendChild(xmltext);
                xmlDBElem.AppendChild(xmlDB_title);

                // ----- Passage Verses ------------------------
                xmlDB_passage = xmldoc.CreateElement("", "db_verse", "");

                XmlElement xml_verse = xmldoc.CreateElement("db_book"); 
                xmltext = xmldoc.CreateTextNode(data[index].fpassage.db_book);
                xml_verse.AppendChild(xmltext);
                xmlDB_passage.AppendChild(xml_verse);

                xml_verse = xmldoc.CreateElement("db_from_ch");
                xmltext = xmldoc.CreateTextNode(getPassageFromCh(data[index].fpassage.db_npassage));
                xml_verse.AppendChild(xmltext);
                xmlDB_passage.AppendChild(xml_verse);

                xml_verse = xmldoc.CreateElement("db_from_vs");
                xmltext = xmldoc.CreateTextNode(getPassageFromVs(data[index].fpassage.db_npassage));
                xml_verse.AppendChild(xmltext);
                xmlDB_passage.AppendChild(xml_verse);

                xml_verse = xmldoc.CreateElement("db_to_ch");
                xmltext = xmldoc.CreateTextNode(getPassageToCh(data[index].fpassage.db_npassage));
                xml_verse.AppendChild(xmltext);
                xmlDB_passage.AppendChild(xml_verse);

                xml_verse = xmldoc.CreateElement("db_to_vs");
                xmltext = xmldoc.CreateTextNode(getPassageToVs(data[index].fpassage.db_npassage));
                xml_verse.AppendChild(xmltext);
                xmlDB_passage.AppendChild(xml_verse);

                xmlDBElem.AppendChild(xmlDB_passage);
                // -----------------------------



                xmlDB_keyverse = xmldoc.CreateElement("", "db_key_verse", "");
                xmltext = xmldoc.CreateTextNode(data[index].fkeyverse.db_chapter + ":" + data[index].fkeyverse.db_verses);
                xmlDB_keyverse.AppendChild(xmltext);
                xmlDBElem.AppendChild(xmlDB_keyverse);

                xmlDB_keyversetext = xmldoc.CreateElement("", "db_front_text", "");
                xmlDB_keyversetext.InnerText = "\"" + data[index].KeyVerseText + "\"";
                xmlDBElem.AppendChild(xmlDB_keyversetext);

                xmlDB_content = xmldoc.CreateElement("", "db_body", "");

                temp_string_holder = "";

                for (int i = 0; i < data[index].fcontent.numofparagraphs; i++)
                {
                    temp_string_holder += data[index].fcontent.paragraphs[i].content + ((i == (data[index].fcontent.numofparagraphs - 1)) ? "" : "\n\n");
                }

                xmltext = xmldoc.CreateTextNode(temp_string_holder);
                xmlDB_content.AppendChild(xmltext);
                xmlDBElem.AppendChild(xmlDB_content);

                xmlDB_prayer = xmldoc.CreateElement("", "db_prayer", "");
                xmltext = xmldoc.CreateTextNode(data[index].Prayer);
                xmlDB_prayer.AppendChild(xmltext);
                xmlDBElem.AppendChild(xmlDB_prayer);

                xmlDB_oneword = xmldoc.CreateElement("", "db_ow", "");
                xmltext = xmldoc.CreateTextNode(data[index].OneWord);
                xmlDB_oneword.AppendChild(xmltext);
                xmlDBElem.AppendChild(xmlDB_oneword);
            }

            try
            {
                xmldoc.Save(@"C:\Temp\db_file_v1.xml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void MakeXMLFile_v2()
        {
            XmlDocument xmldoc;
            XmlNode xmlnode;
            XmlElement xmlRootElem;
            XmlElement xmlDBElem;

            XmlElement xmlDB_date;
            XmlElement xmlDB_title;
            XmlElement xmlDB_passage;
            XmlElement xmlDB_keyverse;
            XmlElement xmlDB_keyversetext;
            XmlElement xmlDB_content;
            XmlElement xmlDB_prayer;
            XmlElement xmlDB_oneword;

            XmlText xmltext;

            xmldoc = new XmlDocument();
            string temp_string_holder = "";

            xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xmldoc.AppendChild(xmlnode);

            xmlRootElem = xmldoc.CreateElement("", "ubf_db", "");
            xmldoc.AppendChild(xmlRootElem);

            for (int index = 0; index < dbCount; index++)
            {
                xmlDBElem = xmldoc.CreateElement("", "ubf_db_one_day", "");
                xmldoc.ChildNodes.Item(1).AppendChild(xmlDBElem);

                DateTime d = DateTime.Parse(data[index].Date);

                xmlDB_date = xmldoc.CreateElement("", "field_daily_bread_date", "");

                xmltext = xmldoc.CreateTextNode(d.ToString("MM/dd/yyyy"));
                xmlDB_date.AppendChild(xmltext);
                xmlDBElem.AppendChild(xmlDB_date);

                if (data[index].fintro.numofparagraphs > 0)
                {
                    // --------------------------

                    XmlElement xmlDB_intro_group = xmldoc.CreateElement("", "group_daily_bread_introduction", "");

                    XmlElement xmlDB_intro_title = xmldoc.CreateElement("", "field_daily_bread_intro_title", "");
                    xmltext = xmldoc.CreateTextNode(data[index].fintro.title);
                    xmlDB_intro_title.AppendChild(xmltext);
                    xmlDB_intro_group.AppendChild(xmlDB_intro_title);

                    XmlElement xmlDB_intro_text = xmldoc.CreateElement("", "field_daily_bread_intro_body", "");

                    temp_string_holder = "";

                    for (int i = 0; i < data[index].fintro.numofparagraphs; i++)
                    {
                        temp_string_holder += data[index].fintro.content[i] + ((i == (data[index].fintro.numofparagraphs - 1)) ? "" : "\n\n");
                    }

                    xmltext = xmldoc.CreateTextNode(temp_string_holder);
                    xmlDB_intro_text.AppendChild(xmltext);
                    xmlDB_intro_group.AppendChild(xmlDB_intro_text);

                    xmlDBElem.AppendChild(xmlDB_intro_group);

                }

                xmlDB_title = xmldoc.CreateElement("", "title", "");
                xmltext = xmldoc.CreateTextNode(data[index].Title);
                xmlDB_title.AppendChild(xmltext);
                xmlDBElem.AppendChild(xmlDB_title);

                // ----- Passage Verses ------------------------
                xmlDB_passage = xmldoc.CreateElement("", "field_daily_bread_verse", "");

                XmlElement xml_verse = xmldoc.CreateElement("db_book");
                xmltext = xmldoc.CreateTextNode(data[index].fpassage.db_book);
                xml_verse.AppendChild(xmltext);
                xmlDB_passage.AppendChild(xml_verse);

                xml_verse = xmldoc.CreateElement("db_from_ch");
                xmltext = xmldoc.CreateTextNode(getPassageFromCh(data[index].fpassage.db_npassage));
                xml_verse.AppendChild(xmltext);
                xmlDB_passage.AppendChild(xml_verse);

                xml_verse = xmldoc.CreateElement("db_from_vs");
                xmltext = xmldoc.CreateTextNode(getPassageFromVs(data[index].fpassage.db_npassage));
                xml_verse.AppendChild(xmltext);
                xmlDB_passage.AppendChild(xml_verse);

                xml_verse = xmldoc.CreateElement("db_to_ch");
                xmltext = xmldoc.CreateTextNode(getPassageToCh(data[index].fpassage.db_npassage));
                xml_verse.AppendChild(xmltext);
                xmlDB_passage.AppendChild(xml_verse);

                xml_verse = xmldoc.CreateElement("db_to_vs");
                xmltext = xmldoc.CreateTextNode(getPassageToVs(data[index].fpassage.db_npassage));
                xml_verse.AppendChild(xmltext);
                xmlDB_passage.AppendChild(xml_verse);

                xmlDBElem.AppendChild(xmlDB_passage);
                // -----------------------------

                // ----- Passage Verses ------------------------
                xmlDB_passage = xmldoc.CreateElement("", "field_daily_bread_key_verse", "");

                xml_verse = xmldoc.CreateElement("db_book");
                xmltext = xmldoc.CreateTextNode(data[index].fpassage.db_book);
                xml_verse.AppendChild(xmltext);
                xmlDB_passage.AppendChild(xml_verse);

                xml_verse = xmldoc.CreateElement("db_from_ch");
                xmltext = xmldoc.CreateTextNode(getPassageFromCh(data[index].fkeyverse.db_chapter + ":" + data[index].fkeyverse.db_verses));
                xml_verse.AppendChild(xmltext);
                xmlDB_passage.AppendChild(xml_verse);

                xml_verse = xmldoc.CreateElement("db_from_vs");
                xmltext = xmldoc.CreateTextNode(getPassageFromVs(data[index].fkeyverse.db_chapter + ":" + data[index].fkeyverse.db_verses));
                xml_verse.AppendChild(xmltext);
                xmlDB_passage.AppendChild(xml_verse);

                xml_verse = xmldoc.CreateElement("db_to_ch");
                xmltext = xmldoc.CreateTextNode(getPassageToCh(data[index].fkeyverse.db_chapter + ":" + data[index].fkeyverse.db_verses));
                xml_verse.AppendChild(xmltext);
                xmlDB_passage.AppendChild(xml_verse);

                xml_verse = xmldoc.CreateElement("db_to_vs");
                xmltext = xmldoc.CreateTextNode(getPassageToVs(data[index].fkeyverse.db_chapter + ":" + data[index].fkeyverse.db_verses));
                xml_verse.AppendChild(xmltext);
                xmlDB_passage.AppendChild(xml_verse);

                xmlDBElem.AppendChild(xmlDB_passage);
                // -----------------------------

                xmlDB_keyversetext = xmldoc.CreateElement("", "field_daily_bread_front_page_txt", "");
                xmlDB_keyversetext.InnerText = "\"" + data[index].KeyVerseText + "\"";
                xmlDBElem.AppendChild(xmlDB_keyversetext);

                xmlDB_content = xmldoc.CreateElement("", "body", "");

                temp_string_holder = "";

                for (int i = 0; i < data[index].fcontent.numofparagraphs; i++)
                {
                    temp_string_holder += data[index].fcontent.paragraphs[i].content + ((i == (data[index].fcontent.numofparagraphs - 1)) ? "" : "\n\n");
                }

                xmltext = xmldoc.CreateTextNode(temp_string_holder);
                xmlDB_content.AppendChild(xmltext);
                xmlDBElem.AppendChild(xmlDB_content);

                xmlDB_prayer = xmldoc.CreateElement("", "field_daily_bread_prayer", "");
                xmltext = xmldoc.CreateTextNode(data[index].Prayer);
                xmlDB_prayer.AppendChild(xmltext);
                xmlDBElem.AppendChild(xmlDB_prayer);

                xmlDB_oneword = xmldoc.CreateElement("", "field_daily_bread_one_word", "");
                xmltext = xmldoc.CreateTextNode(data[index].OneWord);
                xmlDB_oneword.AppendChild(xmltext);
                xmlDBElem.AppendChild(xmlDB_oneword);
            }

            try
            {
                xmldoc.Save(@"C:\Temp\db_file_v2.xml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void MakeXMLFile_v3()
        {
            XmlDocument xmldoc;
            XmlNode xmlnode;
            XmlElement xmlRootElem;
            XmlElement xmlDBElem;

            XmlElement xmlDB_date;
            XmlElement xmlDB_title;
            XmlElement xmlDB_passage;
            XmlElement xmlDB_keyverse;
            XmlElement xmlDB_keyversetext;
            XmlElement xmlDB_content;
            XmlElement xmlDB_prayer;
            XmlElement xmlDB_oneword;

            XmlText xmltext;
            string temp_string_holder = "";

            xmldoc = new XmlDocument();
            XmlDeclaration xmlDecl = xmldoc.CreateXmlDeclaration("1.0", "UTF-8", null) ;


            //xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            //xmldoc.AppendChild(xmlnode);

            xmlRootElem = xmldoc.CreateElement("", "ubf_db", "");
            xmldoc.AppendChild(xmlRootElem);
            xmldoc.InsertBefore(xmlDecl, xmlRootElem);

            for (int index = 0; index < dbCount; index++)
            {
                xmlDBElem = xmldoc.CreateElement("", "ubf_db_one_day", "");
                xmldoc.ChildNodes.Item(1).AppendChild(xmlDBElem);

                DateTime d = DateTime.Parse(data[index].Date + "," + data[index].fdate.db_year);

                xmlDB_date = xmldoc.CreateElement("", "field_daily_bread_date", "");

                xmltext = xmldoc.CreateTextNode(d.ToString("MM/dd/yyyy"));
                xmlDB_date.AppendChild(xmltext);
                xmlDBElem.AppendChild(xmlDB_date);

                if (data[index].fintro.numofparagraphs > 0)
                {
                    // --------------------------

                    XmlElement xmlDB_intro_group = xmldoc.CreateElement("", "group_daily_bread_introduction", "");

                    XmlElement xmlDB_intro_title = xmldoc.CreateElement("", "field_daily_bread_intro_title", "");
                    xmltext = xmldoc.CreateTextNode(data[index].fintro.title);
                    xmlDB_intro_title.AppendChild(xmltext);
                    xmlDB_intro_group.AppendChild(xmlDB_intro_title);

                    XmlElement xmlDB_intro_text = xmldoc.CreateElement("", "field_daily_bread_intro_body", "");

                    temp_string_holder = "";

                    for (int i = 0; i < data[index].fintro.numofparagraphs; i++)
                    {
                        temp_string_holder += data[index].fintro.content[i] + ((i == (data[index].fintro.numofparagraphs - 1)) ? "" : "\n\n");
                    }

                    xmltext = xmldoc.CreateTextNode(temp_string_holder);
                    xmlDB_intro_text.AppendChild(xmltext);
                    xmlDB_intro_group.AppendChild(xmlDB_intro_text);

                    xmlDBElem.AppendChild(xmlDB_intro_group);

                }

                xmlDB_title = xmldoc.CreateElement("", "title", "");
                xmltext = xmldoc.CreateTextNode(data[index].Title);
                xmlDB_title.AppendChild(xmltext);
                xmlDBElem.AppendChild(xmlDB_title);

                // ----- Passage Verses ------------------------
                xmlDB_passage = xmldoc.CreateElement("", "field_daily_bread_verse", "");
                //xmltext = xmldoc.CreateTextNode("<script language=\"JavaScript\">function NewWin(t) {NewBibleWin = open(t,\"Passage\",\"resizable=yes,scrollbars=yes,width=600,height=450\");}</script><a onclick=\"NewWin('http://www.biblegateway.net/passage/index.php?language=English&search=" + data[index].fpassage.db_book + "+" + data[index].fpassage.db_npassage + "&version=31&interface=print" + "')\" href=\"#\">" + data[index].Passage + "</a>&nbsp;" + data[index].fpassage.db_npassage_info);
                xmltext = xmldoc.CreateTextNode("<a href=\"http://www.biblegateway.net/passage/index.php?language=English&search=" + data[index].fpassage.db_book + "+" + data[index].fpassage.db_npassage + "&version=31&interface=print\">" + data[index].Passage + "</a>&nbsp;" + data[index].fpassage.db_npassage_info);
                xmlDB_passage.AppendChild(xmltext);
                xmlDBElem.AppendChild(xmlDB_passage);
                // -----------------------------

                // ----- Passage Verses ------------------------
                xmlDB_passage = xmldoc.CreateElement("", "field_daily_bread_key_verse", "");
                //xmltext = xmldoc.CreateTextNode(data[index].fpassage.db_book + " " + data[index].fkeyverse.db_chapter + ":" + data[index].fkeyverse.db_verses);
                xmltext = xmldoc.CreateTextNode(data[index].fpassage.db_book + " " + ((data[index].fkeyverse.db_chapter.ToString().Trim() != "") ? (data[index].fkeyverse.db_chapter + ":") : "") + data[index].fkeyverse.db_verses);
                xmlDB_passage.AppendChild(xmltext);
                xmlDBElem.AppendChild(xmlDB_passage);
                // -----------------------------

                xmlDB_keyversetext = xmldoc.CreateElement("", "field_daily_bread_front_page_txt", "");
                xmlDB_keyversetext.InnerText = "\"" + data[index].KeyVerseText + "\"";
                xmlDBElem.AppendChild(xmlDB_keyversetext);

                xmlDB_content = xmldoc.CreateElement("", "body", "");

                temp_string_holder = "";

                for (int i = 0; i < data[index].fcontent.numofparagraphs; i++)
                {
                    temp_string_holder += data[index].fcontent.paragraphs[i].content + ((i == (data[index].fcontent.numofparagraphs - 1)) ? "" : "\n\n");
                }

                xmltext = xmldoc.CreateTextNode(temp_string_holder);
                xmlDB_content.AppendChild(xmltext);
                xmlDBElem.AppendChild(xmlDB_content);

                xmlDB_prayer = xmldoc.CreateElement("", "field_daily_bread_prayer", "");
                xmltext = xmldoc.CreateTextNode(data[index].Prayer);
                xmlDB_prayer.AppendChild(xmltext);
                xmlDBElem.AppendChild(xmlDB_prayer);

                xmlDB_oneword = xmldoc.CreateElement("", "field_daily_bread_one_word", "");
                xmltext = xmldoc.CreateTextNode(data[index].OneWord);
                xmlDB_oneword.AppendChild(xmltext);
                xmlDBElem.AppendChild(xmlDB_oneword);
            }

            try
            {
                xmldoc.Save(@"C:\Temp\db_file_v3.xml");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


		public void MakeDBIndex (string year)
		{
			FileStream file = new FileStream (strTmpFolder + "index.html", FileMode.OpenOrCreate, FileAccess.Write);
			StreamWriter sw = new StreamWriter (file);
			string strBGColor;
			
			sw.WriteLine ("<html><head></head><body>");
			sw.WriteLine ("<center>");
            sw.Write("<font size=5>Daily Bread</font><br /><font size=-1>" + data[0].fdate.db_month + " " + data[0].fdate.db_ndate + ", " + data[0].fdate.db_year + " - " + data[dbCount - 1].fdate.db_month + " " + data[dbCount - 1].fdate.db_ndate + ", " + data[dbCount - 1].fdate.db_year + "</font><br /><br />");
			sw.WriteLine ("<table border=0 width=\"100%\">");
			
			for (int index = 0; index < dbCount; index++)
			{
				if (index % 2 == 0) strBGColor = "#d8d8d8";
				else strBGColor = "#ffffff";
				
				sw.WriteLine ("<tr bgcolor=" + strBGColor + "><td align=left><a href='" + data [index].fdate.db_month + "." + data [index].fdate.db_ndate + ".html" + "'>" + data [index].Date + "</td><td align=right>" + data [index].Passage + "</td></tr>");
			}
			sw.WriteLine ("</table>");
			sw.WriteLine ("</center>");
			sw.WriteLine ("</body></html>");

			sw.Close ();
			file.Close ();
		}

        public string GetKVText(int c)
        {

            string kvBook;
            string kvChapter;
            string kvContent = "";
            string strSQL;

            string[] kvVerses;
            string[] splittedLine;

            string dbLocation = @"C:\Documents and Settings\ilya\My Documents\Visual Studio 2005\Projects\webtest\bin\Debug\Bible_NIV.mdb";

            OleDbConnection dbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbLocation);
            OleDbCommand dbCommand;
            OleDbDataReader dbReader;
            
            dbConn.Open();

            //kvBook = data[c].fpassage.db_book;
            //kvChapter = data[c].fpassage.db_chapter;

            //splittedLine = data[c].CompleteKeyVerse.Split(':');
            //kvChapter = splittedLine[0].Trim();

            //splittedLine = data[c].fpassage.db_verses.Split(',');
            kvBook = data[c].fkeyverse.db_book;
            kvChapter = data[c].fkeyverse.db_chapter;

            splittedLine = data[c].fkeyverse.db_verses.Split(',');

            kvVerses = new string[splittedLine.Length];

            for (int i = 0; i < splittedLine.Length; i++)
            {
                kvVerses[i] = splittedLine[i].Trim();
            }

            strSQL = "SELECT Verse, VerseNumber FROM qry_Verse WHERE BookName = \"" + kvBook + "\" AND ChapterNumber = " + kvChapter.Trim() + " AND (";
            
            for (int i = 0; i < kvVerses.Length; i++)
            {
                if (i != 0)
                {
                    strSQL += " OR ";
                }
                strSQL += "VerseNumber = " + kvVerses[i].Trim();
            }
            
            strSQL += ");";
            kvContent = "";
            
            try
            {   
                dbCommand = new OleDbCommand(strSQL, dbConn);
                dbReader = dbCommand.ExecuteReader();

                while (dbReader.Read())
                {    
                    kvContent += dbReader.GetString(0) + " ";
                }

                dbReader.Close();
                
            }
            catch (OleDbException e)
            {
                System.Console.WriteLine("Error: {0}", e.Errors[0].Message);
            }
            
            kvContent = kvContent.Trim();
            
            kvContent = kvContent.Replace("'s", "$1");            
            kvContent = kvContent.Replace("s'", "$2");
            kvContent = kvContent.Replace("'", "$3");
            kvContent = kvContent.Replace("\"", "'");
            kvContent = kvContent.Replace("$3", "\"");
            kvContent = kvContent.Replace("$1", "'s");
            kvContent = kvContent.Replace("#2", "s'");

            dbConn.Close();

            return kvContent;
        }

        public void MakeXMLTemplate (string year)
		{
            string kvBook;
            string kvChapter;
            string kvContent = "";
            string strSQL;

            string[] kvVerses;

            string[] splittedLine;

            //string dbLocation = @"C:\Users\Ilya\Documents\Visual Studio 2010\Projects\DailyBreadUtil\Bible_NIV.mdb";
            string strConnectionString = config.ConnectionString();

            SqlDataReader kv_data = null;
            data_manager conn = new data_manager();

            //OleDbConnection dbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbLocation);
            //OleDbCommand dbCommand;
            //OleDbDataReader dbReader;

            //dbConn.Open();

            FileStream file = new FileStream (strTmpFolder + "keyverse_template.xml", FileMode.OpenOrCreate, FileAccess.Write);
			StreamWriter sw = new StreamWriter (file);

			sw.WriteLine ("<keyverses>");

			for (int index = 0; index < dbCount; index++)
            {
                bool need_to_find_kverse = true;

                if (need_to_find_kverse == true) 
                {
                    //splittedLine = data[index].CompleteKeyVerse.Split(' ');
                    kvBook = data[index].fkeyverse.db_book;

                    //splittedLine = data[index].KeyVerse.Split(':');
                    //kvChapter = splittedLine[1].Trim();
                    kvChapter = data[index].fkeyverse.db_chapter;

                    //splittedLine = splittedLine[2].Split(',');
                    splittedLine = data[index].fkeyverse.db_verses.Split(',');
                    kvVerses = new string[splittedLine.Length];

                    for (int i = 0; i < splittedLine.Length; i++)
                    {
                        kvVerses[i] = splittedLine[i].Trim();
                    }

                    //strSQL = "SELECT Verse, VerseNumber FROM qry_Verse WHERE BookName = \"" + kvBook + "\" AND ChapterNumber = " + kvChapter.Trim() + " AND (";
                    
                    strSQL = "SELECT strVerse, intVerseNumber FROM qryBibleVerse WHERE chvBookName = '" + kvBook + "' AND intChapterNumber = " + kvChapter.Trim() + " AND (";

                    for (int i = 0; i < kvVerses.Length; i++)
                    {
                        if (i != 0)
                        {
                            strSQL += " OR ";
                        }
                        strSQL += "intVerseNumber = " + kvVerses[i].Trim();
                    }
                    strSQL += ");";

                    kvContent = "";

                    try
                    {
                        //dbCommand = new OleDbCommand(strSQL, dbConn);
                        //dbReader = dbCommand.ExecuteReader();

                        //while (dbReader.Read())
                        //{
                        //    kvContent += dbReader.GetString(0) + " ";
                        //    //kvContent += "<span style='font-size: 0.65em; font-weight: bold; vertical-align: text-top;'>" + dbReader.GetInt32(1) + "</span>" + dbReader.GetString(0) + " ";
                        //}

                        //dbReader.Close();

                        //kv_data = conn.ExecuteQuery(strConnectionString, strSQL);

                        //while (kv_data.Read())
                        //{
                        //    kvContent += kv_data.GetString(0) + " ";
                        //}

                    }
                    catch (Exception e)
                    {
                        //System.Console.WriteLine("Error: {0}", e.Errors[0].Message);
                    }

                    kvContent = kvContent.Trim();

                    /*
                    if (kvContent.Length > 0)
                    {
                        if (kvContent[0] == '\"')
                            kvContent = kvContent.Remove(0, 1);
                        if (kvContent[kvContent.Length - 1] == '\"')
                            kvContent = kvContent.Remove(kvContent.Length - 1);
                    }
                    */

                    kvContent = kvContent.Replace("'s", "$1");
                    kvContent = kvContent.Replace("s'", "$2");
                    kvContent = kvContent.Replace("'", "$3");
                    kvContent = kvContent.Replace("\"", "'");
                    kvContent = kvContent.Replace("$3", "\"");
                    kvContent = kvContent.Replace("$1", "'s");
                    kvContent = kvContent.Replace("$2", "s'");

                    //data[index].KeyVerseText = kvContent.Trim();
                    //data[index].fkeyverse.db_ktext = kvContent.Trim();
                }

                sw.WriteLine("<verse>");
                sw.WriteLine("\t<date>" + data[index].fdate.db_month + " " + data[index].fdate.db_ndate + ", " + year + "</date>");
                sw.WriteLine("\t<passage>" + data[index].CompleteKeyVerse + "</passage>");
                sw.WriteLine("\t<content>" + data[index].fkeyverse.db_ktext + "</content>");
                sw.WriteLine("</verse>");

            }

            sw.WriteLine("</keyverses>");

			sw.Close();
			file.Close();
            conn.Close();
		}

        private string getPassageBookName(string s)
        {
            return "";
        }

        private string getPassageFromCh(string s)
        {
            string[] tmp1 = s.Split('-');
            string[] tmp2 = tmp1[0].Split(':');

            return getOnlyNumbers(tmp2[0]);
        }

        private string getPassageFromVs(string s)
        {
            string[] tmp1 = s.Split('-');
            string[] tmp2 = tmp1[0].Split(':');

            return getOnlyNumbers(tmp2[1]);
        }

        private string getPassageToCh(string s)
        {
            string[] tmp1 = s.Split('-');
            string[] tmp2 = tmp1[0].Split(':');

            if (tmp1.Length > 1)
            {
                string[] tmp3 = tmp1[1].Split(':');
                return getOnlyNumbers((tmp3.Length == 1) ? tmp2[0] : tmp3[0]);
            }
            else
            {
                return getOnlyNumbers(tmp2[0]);
            }
        }

        private string getPassageToVs(string s)
        {
            string[] tmp1 = s.Split('-');
            string[] tmp2 = tmp1[0].Split(':');

            if (tmp1.Length > 1)
            {
                string[] tmp3 = tmp1[1].Split(':');
                return getOnlyNumbers((tmp3.Length == 1) ? tmp3[0] : tmp3[1]);
            }
            else
            {
                return getOnlyNumbers(tmp2[1]);
            }
        }

        private string getOnlyNumbers(string s)
        {
            string sPattern = "(\\d+).*";
            Regex r = new Regex(sPattern);

            Match m = r.Match(s);

            if (m.Success)
            {
                return m.Groups[1].ToString();
            }

            return s;
        }

        private int countParagraphs(string[] s)
        {
            int intValidLines = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Trim().Length > 0) intValidLines++;
            }

    
            return (intValidLines > 6) ? intValidLines - 6 : -1;
        }

        public int MakeTestPages()
        {
            FileStream file = new FileStream("db_list.php", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter sw = new StreamWriter(file);
            string strBGColor;

            sw.WriteLine("<html><head></head><body>");
            sw.WriteLine("<table border=0>");

            for (int index = 0; index < dbCount; index++)
            {
                if (index % 2 == 0) strBGColor = "#d8d8d8";
                else strBGColor = "#ffffff";

                sw.WriteLine("<tr bgcolor=" + strBGColor + "><td align=left><a href='daily_bread_contents.php?date=\n<?php\n print strtotime(\"" + data[index].fdate.db_month + " " + data[index].fdate.db_ndate + "," + data[index].fdate.db_year + "\");\n?>\n' target='db_view'>" + data[index].Date + "</td><td align=right>" + data[index].Passage + "</td></tr>");
            }

            sw.WriteLine("</table>");
            sw.WriteLine("</center>");
            sw.WriteLine("</body></html>");

            sw.Close();
            file.Close();

            return 1;
        }


		private class db_data 
		{
            public string Introduction;
            public string Title;
            public string SubTitle;
			public string Date;
			public string Passage;
			public string KeyVerse;
            public string KeyVerseText;
			public string CompleteKeyVerse;
			public string Body; 
			public string Prayer;
			public string OneWord;
            public string HTMLText;
            public string RSSText;

            public db_intro fintro          = new db_intro();
            public db_date fdate            = new db_date();
			public db_passage fpassage      = new db_passage();
            public db_keyverse fkeyverse    = new db_keyverse();
            public db_content fcontent      = new db_content();

			public db_data ()
			{
				Introduction                = null;
				Title                       = null;
                SubTitle                    = null;
				Date                        = null;
				Passage                     = null;
				KeyVerse                    = null;
                KeyVerseText                = null;
				CompleteKeyVerse            = null;
				Body                        = null;
				Prayer                      = null;
				OneWord                     = null;
                HTMLText                    = null;
                RSSText                     = null;
			}
		}
		
		private class db_date
		{
			public string db_day;
			public string db_month;
			public string db_ndate;
            public string db_year;

			public db_date ()
			{
				db_day                      = null;
				db_month                    = null;
				db_ndate                    = null;
                db_year                     = null;
			}
		}

		private class db_passage
		{
			public string db_book;
			public string db_npassage;
            public string db_chapter;
            public string db_verses;
			public string db_npassage_info;

			public db_passage ()
			{
				db_book                     = null;
				db_npassage                 = null;
                db_chapter                  = null;
                db_verses                   = null;
				db_npassage_info            = null;
			}
		}

        public class db_keyverse
        {
            public string db_book;
            public string db_chapter;
            public string db_verses;
            public string db_ktext;

            public db_keyverse()
            {
                db_book                     = null;
                db_chapter                  = null;
                db_verses                   = null;
                db_ktext                    = null;
            }
        }

        public class db_paragraph
        {
            public int num;
            public string title;
            public string content;

            public db_paragraph()
            {
                num = 0;
                title                       = null;
                content                     = null;
            }
        }

        public class db_content
        {
            public db_paragraph[] paragraphs;
            public int numofparagraphs;

            public db_content()
            {
                paragraphs                  = new db_paragraph[6];
                numofparagraphs             = 0;
            }
        }

        public class db_intro
        {
            public int numofparagraphs;
            public string title;
            public string[] content;

            public db_intro()
            {
                numofparagraphs             = 0;
                content                     = new string[20];
            }
        }
	}
}
