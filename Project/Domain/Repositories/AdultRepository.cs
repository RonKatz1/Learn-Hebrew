using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Domain.Repositories
{
    public class AdultRepository : Repository
    {
        public int AdultID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Data { get; set; }


        public int Save(Adult Adult)
        {
            //LearnHebrewDbContext db = new LearnHebrewDbContext();
            try
            {
                Entity.Adult temp = this.LearnHebrewDB.Adults.FirstOrDefault(x => x.AdultID == Adult.AdultID);
                if(temp == null)
                {
                    this.LearnHebrewDB.Adults.Add(Adult);
                }
                else
                {
                    this.LearnHebrewDB.Entry(temp).CurrentValues.SetValues(Adult);
                }

                this.LearnHebrewDB.SaveChanges();

                return Adult.AdultID;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public Adult LoadByID(int AdultID)
        {
            return this.LearnHebrewDB.Adults.Where(a => a.AdultID == AdultID).FirstOrDefault();
        }

        public Adult LoadByNameAndPassword(string Name, string Password)
        {
            return this.LearnHebrewDB.Adults.Where(a => a.Name == Name && a.Password == Password).FirstOrDefault();

        }

        public Adult LoadAdultByName(string Name)
        {
            return this.LearnHebrewDB.Adults.Where(a => a.Name == Name).FirstOrDefault();
        }

        public List<Adult> LoadAll()
        {
            return this.LearnHebrewDB.Adults.ToList();
        }
    }
}
