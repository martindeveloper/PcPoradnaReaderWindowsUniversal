using PcPoradnaReaderWindowsUniversal.Model.Questions;
using PcPoradnaReaderWindowsUniversal.Model.Replies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PcPoradnaReaderWindowsUniversal.Model
{
    interface IDataProvider
    {
        Task<IReadOnlyList<Question>> FetchLatestQuestionsAsync();

        Task<IReadOnlyList<Reply>> FetchRepliesAsync(Question question);

        void SetCategory(Categories.Category category);
    }
}
