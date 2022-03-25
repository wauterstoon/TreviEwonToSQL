using Domain.Data;
using Domain.Entities;

namespace Domain.Managers
{
    public class TagManager
    {
        private ITagRepo tagRepo;
        public TagManager(ITagRepo tagRepo)
        {
            this.tagRepo = tagRepo;
        }
        public IReadOnlyList<Tag> GetAllTags()
        {
            try
            {
                return tagRepo.getAllTags();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public List<Tag> getAllValuesByTagName(string tagName, DateTime date)
        {
            try
            {
                return tagRepo.getAllValuesByTagName(tagName,date);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public DateTime getLastRowTime()
        {
            try
            {
                return tagRepo.getLastRowTime();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }



    }
}
