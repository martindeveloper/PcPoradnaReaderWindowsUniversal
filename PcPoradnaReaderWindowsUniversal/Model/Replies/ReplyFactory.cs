using Newtonsoft.Json.Linq;
using System;
using PcPoradnaReaderWindowsUniversal.Model.Users;
using static PcPoradnaReaderWindowsUniversal.Model.TypeHelper;

namespace PcPoradnaReaderWindowsUniversal.Model.Replies
{
    class ReplyFactory
    {
        public static Reply FromJsonToken(JToken item)
        {
            Reply reply = new Reply();

            foreach(JToken jToken in item)
            {
                JProperty property = (JProperty) jToken;

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
                    default:

                        break;
                }
            }

            return reply;
        }
    }
}
