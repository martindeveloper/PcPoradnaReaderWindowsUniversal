using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcPoradnaReaderWindowsUniversal.Model
{
    interface IDataProvider
    {
        Task<IReadOnlyList<Question>> FetchLatestQuestionsAsync();

        Task<IReadOnlyList<Reply>> FetchRepliesAsync(Question question);
    }
}
