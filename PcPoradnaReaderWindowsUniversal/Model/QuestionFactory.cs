using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PcPoradnaReaderWindowsUniversal.Model
{
    class QuestionFactory
    {
        public static Question FromJsonToken (JToken item)
        {
            Question question = new Question();

            Type boolType = typeof(bool);

            foreach (JProperty property in item)
            {
                switch(property.Name)
                {
                    case "id":
                        int id = 0;
                        int.TryParse(property.Value.ToString(), out id);

                        question.Id = id;
                        break;

                    case "nick":
                        User author = new User() { Username = property.Value.ToString() };

                        question.Author = author;
                        break;

                    case "plain":
                        question.Text = property.Value.ToString();
                        break;

                    case "title":
                        question.Title = property.Value.ToString();
                        break;

                    case "created_on":
                        long created = 0;
                        long.TryParse(property.Value.ToString(), out created);

                        question.CreatedOn = TypeHelper.CreateDateTimeFromTicksEpoch(created);
                        break;

                    case "sticked":
                        question.IsSticked = TypeHelper.StringToBool(property.Value.ToString());
                        break;

                    case "locked":
                        question.IsLocked = TypeHelper.StringToBool(property.Value.ToString());
                        break;

                    case "deleted":
                        question.IsDeleted = TypeHelper.StringToBool(property.Value.ToString());
                        break;

                    case "category":
                        question.Category = property.Value.ToString();
                        break;
                }
            }

            return question;
        }
    }
}
