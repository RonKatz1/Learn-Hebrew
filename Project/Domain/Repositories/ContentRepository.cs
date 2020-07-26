using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Domain.Repositories
{
    public class ContentRepository : Repository
    {
        public int ContentID { get; set; }
        public int AdultID { get; set; }
        public string Word { get; set; }
        public string Data { get; set; }

        public int Save(Content content)
        {
            try
            {
                //Entity.Content temp = this.LearnHebrewDB.Contents.FirstOrDefault(x => x.ContentID == content.ContentID);
                Entity.Content temp = this.LearnHebrewDB.Contents.Where(x => x.ContentID == content.ContentID).FirstOrDefault();
                if (temp == null)
                {
                    this.LearnHebrewDB.Contents.Add(content);
                }
                else
                {
                    this.LearnHebrewDB.Entry(temp).CurrentValues.SetValues(content);
                }

                this.LearnHebrewDB.SaveChanges();

                return content.ContentID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public Content LoadByID(int ContentID)
        {
            return this.LearnHebrewDB.Contents.Where(c => c.ContentID == ContentID).FirstOrDefault();
        }

        public List<Content> LoadAllContents()
        {
            return this.LearnHebrewDB.Contents.ToList();
        }
        
    }
}
