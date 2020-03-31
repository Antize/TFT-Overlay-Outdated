using System.Collections;
using System.Xml.Serialization;

namespace Antize_TFT.Class
{
    [XmlRoot("UserItems")]
    public class UserItems
    {
        public UserItems()
        {
            this.ChampionItems = new ArrayList();
        }

        public ArrayList ChampionItems
        {
            get; set;
        }
    }
}