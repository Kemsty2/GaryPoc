using Elsa.Services;
using ElsaEdiBackend.Activities;

namespace ElsaEdiBackend.Bookmarks
{
    public class CreateFileBookmark : IBookmark
    {
    }

    public class CreateFileBookmarkProvider : BookmarkProvider<CreateFileBookmark, CreateFileActivity>
    {
        public override IEnumerable<BookmarkResult> GetBookmarks(BookmarkProviderContext<CreateFileActivity> context)
        {
            return new[] { Result(new CreateFileBookmark()) };
        }
    }
}