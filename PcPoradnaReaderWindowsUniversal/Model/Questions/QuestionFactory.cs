using Newtonsoft.Json.Linq;
using PcPoradnaReaderWindowsUniversal.Model.Users;
using static PcPoradnaReaderWindowsUniversal.TypeHelper;

namespace PcPoradnaReaderWindowsUniversal.Model.Questions
{
    class QuestionFactory
    {
        public static Question FromJsonToken (JToken item)
        {
            Question question = new Question();

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

                        question.CreatedOn = CreateDateTimeFromTicksEpoch(created);
                        break;

                    case "sticked":
                        question.IsSticked = StringToBool(property.Value.ToString());
                        break;

                    case "locked":
                        question.IsLocked = StringToBool(property.Value.ToString());
                        break;

                    case "deleted":
                        question.IsDeleted = StringToBool(property.Value.ToString());
                        break;

                    case "category":
                        question.Category = property.Value.ToString();
                        break;

                    case "url_json":
                        question.RepliesApiUrl = property.Value.ToString();
                        break;
                }
            }

            return question;
        }
    }
}
