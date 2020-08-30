using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Domain.Repositories
{
    public class ChildProgressRepository : Repository
    {
        public int ProgressID { get; set; }
        public int ChildID { get; set; }
        public string Data { get; set; }


        public int Save(ChildProgress progress)
        {
            try
            {
                Entity.ChildProgress temp = this.LearnHebrewDB.ChildProgresses.FirstOrDefault(x => x.ProgressID == progress.ProgressID);
                if (temp == null)
                {
                    this.LearnHebrewDB.ChildProgresses.Add(progress);
                }
                else
                {
                    this.LearnHebrewDB.Entry(temp).CurrentValues.SetValues(progress);
                }

                this.LearnHebrewDB.SaveChanges();

                return progress.ProgressID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<ChildProgress> LoadAllChildProgressesByChildID(int ChildID)
        {
            var temp = this.LearnHebrewDB.ChildProgresses.Where(p => p.ChildID == ChildID);
            return temp != null && temp.Count() > 0 ? temp.ToList() : new List<ChildProgress>();
        }
        public ChildProgress LoadSpecificChildProgressesByChildID(int childID, int progressID)
        {
            var temp = this.LearnHebrewDB.ChildProgresses.Where(p => p.ChildID == childID && p.ProgressID == progressID);
            return temp != null && temp.Count() > 0 ? temp.FirstOrDefault() : new ChildProgress();
        }
        public ChildProgress LoadSpecificChildProgressesByprogressID(int progressID)
        {
            var temp = this.LearnHebrewDB.ChildProgresses.Where(p => p.ProgressID == progressID);
            return temp != null && temp.Count() > 0 ? temp.FirstOrDefault() : new ChildProgress();
        }

    }
}
