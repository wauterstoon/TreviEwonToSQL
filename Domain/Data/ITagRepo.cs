using Domain.Entities;

namespace Domain.Data
{
    public interface ITagRepo
    {
        void AddTag(Tag tag);
        IReadOnlyList<Tag> getAllTags();
        DateTime getLastRowTime();
        List<Tag> getAllValuesByTagName(string tagName,DateTime date);


    }
}
