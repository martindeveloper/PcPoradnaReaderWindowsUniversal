using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PcPoradnaReaderWindowsUniversal.TypeHelper;

namespace PcPoradnaReaderWindowsUniversal.Model
{
    class ReplyFactory
    {
        public static Reply FromJsonToken(JToken item)
        {
            Reply reply = new Reply();

            foreach(JProperty property in item)
            {
                switch (property.Name)
                {
                    case "id":
                        int id = 0;
                        int.TryParse(property.Value.ToString(), out id);

                        reply.Id = id;
                        break;

                    case "url":
                        reply.WebUrl = new Uri(property.Value.ToString());
                        break;

                    case "title":
                        reply.Title = property.Value.ToString();
                        break;

                    case "plain":
                        reply.Text = property.Value.ToString();
                        break;

                    case "nick":
                        User author = new User() { Username = property.Value.ToString() };

                        reply.Author = author;
                        break;

                    case "created_on":
                        long created = 0;
                        long.TryParse(property.Value.ToString(), out created);

                        reply.CreatedOn = CreateDateTimeFromTicksEpoch(created);
                        break;
                }
            }

            return reply;
        }
    }
}
