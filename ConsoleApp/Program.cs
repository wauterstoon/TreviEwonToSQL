using Data.repos;
using M2WebLibrary;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class program
    {
        public static string getTagType(string tag)
        {
            if (tag.Contains("DB_"))
            {
                int frst_ = tag.IndexOf("_");
                string firstPart = tag.Substring(0, frst_ + 1);
                string tagNameWithoutDb_ = tag.Replace(firstPart, "");
                tag = tagNameWithoutDb_;
                int scnd_ = tag.IndexOf("_");
                string tagType = tag.Substring(0, scnd_);
                return tagType;
            }
            else
            {
                int frst_ = tag.IndexOf("_");
                string firstPart = tag.Substring(0, frst_);
                return firstPart;
            }
        }

        public static string getTagName(string tag)
        {
            if (tag.Contains("DB_")) {
                int frst_ = tag.IndexOf("_");
                string firstPart = tag.Substring(0, frst_ + 1);
                string tagNameWithoutDb_ = tag.Replace(firstPart, "");
                tag = tagNameWithoutDb_;
                int scnd_ = tag.IndexOf("_");
                string tagType = tag.Substring(0, scnd_+1);
                string tagName = tag.Replace(tagType, "");
                return tagName;
            }
            else
            {
                int frst_ = tag.IndexOf("_");
                string firstPart = tag.Substring(0, frst_+1);
                string tagName = tag.Replace(firstPart, "");
                return tagName;
            }
        }
        static void Main(string[] args)
        {
            Domain.Entities.Tag t;
            TagRepo tagRepo = new TagRepo();
            Domain.Entities.Ewon e;
            EwonRepo ewonRepo = new EwonRepo();
            DateTime timeFromLastRow = tagRepo.getLastRowTime();
            List<string> tagNames = new List<string>();
            List<string> tagDesc = new List<string>();

            var m2web = new M2Web
            {
                Talk2MDevId = "bcc74db5-e311-476b-b17d-88b20a40dd1e",
                AccountName = "Trevi",
                Username = "toonw",
                Password = "608WyMes9C7",
            };
            m2web.LoadEwons();
            var onlineEwons = m2web.Ewons.Where(ewon => ewon.Status == EwonStatus.Online);
           


            foreach (var ewon in onlineEwons)
            {
                e = new Domain.Entities.Ewon(ewon.Name, ewon.Description);
                //ewonRepo.AddEwon(e);
            }
            var ewonName = "MBR";
            var myEwon = m2web.GetEwon(ewonName);
            myEwon.Username = "Trevi";
            myEwon.Password = "6321";

           
            myEwon.LoadTags();

            foreach (var tg in myEwon.Tags)
            {
                string tgnaam = tg.ToString().Substring(0, tg.ToString().IndexOf("("));
                tagNames.Add(tgnaam);
                t = new Domain.Entities.Tag(tgnaam);
                tagDesc.Add(tg.description);
            }

            
            


            for (int i = 0; i < tagNames.Count(); i++)
            {
                
                string tempDesc = tagDesc.ElementAt(i);
                string tempTag = tagNames.ElementAt(i);
                string tagType = getTagType(tempTag);
                string tagName = getTagName(tempTag);

                foreach (var item in myEwon.GetTagHistory(tempTag))
                {
                    if (item.Timestamp >= timeFromLastRow)
                    {
                        t = new Domain.Entities.Tag(tagName, item.Timestamp, item.Value, tempDesc, tagType);
                        tagRepo.AddTag(t);
                    }
                }
            }
            


        }

     

    }
}
