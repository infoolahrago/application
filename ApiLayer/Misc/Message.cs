using System.Linq;
using Olahrago.ApiLayer.Misc.Interface;
using Olahrago.ApiLayer.Model;


namespace Olahrago.ApiLayer.Misc
{
    public class Message : IMessage
    {
        public string GetMessage(string key)
        {
            string result = string.Empty;

            using (var db = new OlahragoContext())
            {
                result = db.ApplicationMessage.Where(am => am.Key.Equals(key)).FirstOrDefault().Value;
            }

            return result;
        }

        public string GetMessageApp(string key)
        {
            string result = string.Empty;
            int counter = 0;

            string[] keySplit = key.Split(".");

            foreach(var item in keySplit)
            {
                string firstAlphabet = item.Substring(0, 1).ToUpper();
                string nextAlphabet = item.Substring(1,item.Length -1);

                if (counter != (keySplit.Length - 1))
                {
                    result += string.Concat(firstAlphabet, nextAlphabet, ' ');
                }
                else
                {
                    result += string.Concat(firstAlphabet, nextAlphabet);
                }
                counter++;
            }

            return result;
        }
    }
}
