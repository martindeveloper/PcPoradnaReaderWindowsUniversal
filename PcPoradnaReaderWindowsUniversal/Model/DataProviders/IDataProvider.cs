using System.Collections.Generic;
using System.Threading.Tasks;
using PcPoradnaReaderWindowsUniversal.Model.Questions;
using PcPoradnaReaderWindowsUniversal.Model.Replies;

namespace PcPoradnaReaderWindowsUniversal.Model.DataProviders
{
    interface IDataProvider
    {
        Task<IReadOnlyList<Question>> FetchLatestQuestionsAsync();

        Task<IReadOnlyList<Reply>> FetchRepliesAsync(Question question);

        void SetCategory(Categories.Category category);
    }
}
