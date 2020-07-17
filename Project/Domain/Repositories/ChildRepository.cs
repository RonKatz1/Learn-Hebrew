using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace Domain.Repositories
{
    public class ChildRepository:Repository
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Color { get; set; }
        public string Data { get; set; }
        public int ChildID { get; set; }

        public int Save(Child Child)
        {
            try
            {
                Entity.Child temp = this.LearnHebrewDB.Children.FirstOrDefault(x => x.ChildID == Child.ChildID);
                if (temp == null)
                {
                    this.LearnHebrewDB.Children.Add(Child);
                }
                else
                {
                    this.LearnHebrewDB.Entry(temp).CurrentValues.SetValues(Child);
                }
                
                this.LearnHebrewDB.SaveChanges();
                
                return Child.ChildID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public Child LoadByID(int ChildID)
        {
            return this.LearnHebrewDB.Children.Where(a => a.ChildID == ChildID).FirstOrDefault();
        }
        public Child LoadChildByNameandPassword(string name,string password)
        {
            int passwordInt = Convert.ToInt32(password);
            return this.LearnHebrewDB.Children.Where(x => x.Name ==name && x.ChildID == passwordInt).FirstOrDefault();
        }

        public List<Child> LoadAllChildsByIds(List<int> ChildsIDs)
        {
            var childs = this.LearnHebrewDB.Children.Where(c => ChildsIDs.Contains(c.ChildID));
            return childs != null && childs.Count() > 0 ? childs.ToList() : new List<Child>();
        }
    }
}
