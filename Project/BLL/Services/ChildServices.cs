using BLL.LearnHebrewEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ChildServices
    {
        public static Child LoadChildByID(int ChildID)
        {
            using (Domain.Repositories.ChildRepository repo = new Domain.Repositories.ChildRepository())
            {
                return ConvertEntityToBusiness(repo.LoadByID(ChildID));
            }
        }
        public static Child LoadChildByNameandPassword(string NameQuery, string PasswordQuery)
        {
            using (Domain.Repositories.ChildRepository repo = new Domain.Repositories.ChildRepository())
            {
                return ConvertEntityToBusiness(repo.LoadChildByNameandPassword(NameQuery, PasswordQuery));
            }
        }

        public static List<Child> LoadAllChildsByIds(List<int> ChildsIDs)
        {
            try
            {
                var entityChilds = new List<Domain.Entity.Child>();
                var childs = new List<Child>();
                using (Domain.Repositories.ChildRepository repo = new Domain.Repositories.ChildRepository())
                {
                    entityChilds = repo.LoadAllChildsByIds(ChildsIDs);
                }
                foreach (var child in entityChilds)
                {
                    childs.Add(ConvertEntityToBusiness(child));
                }
                return childs;
            }
            catch (Exception ex)
            {
                return new List<Child>();
            }
        }

        public static int Save(Child Child)
        {
            Domain.Entity.Child ChildEntity = new Domain.Entity.Child();
            ChildEntity.ChildID = Child.ChildID;
            ChildEntity.Name = Child.Name;
            ChildEntity.Password = Child.Password;
            ChildEntity.Data = Serialization.Serialize2<BLL.LearnHebrewEntities.Child.ChildData>(Child.Data);

            using (var repo = new Domain.Repositories.ChildRepository())
            {
                Child.ChildID = repo.Save(ChildEntity);
            }

            return Child.ChildID;
        }

        public static Child ConvertEntityToBusiness(Domain.Entity.Child EChild)
        {
            Child BChild = new Child();
            try
            {
                BChild.ChildID = EChild.ChildID;
                BChild.Name = EChild.Name;
                BChild.Password = EChild.Password;
                BChild.Data = Serialization.Deserialize2<Child.ChildData>(EChild.Data);
                return BChild;
            }
            catch (Exception ex)
            {
                return new Child();
            }
        }

        public static Domain.Entity.Child ConvertBusinessToEntity(Child BChild)
        {
            Domain.Entity.Child EChild = new Domain.Entity.Child();
            try
            {
                EChild.ChildID = BChild.ChildID;
                EChild.Name = BChild.Name;
                EChild.Password = BChild.Password;
                EChild.Data = Serialization.Serialize2(BChild.Data);
                return EChild;
            }
            catch (Exception ex)
            {
                return new Domain.Entity.Child();
            }
        }
    }
}
