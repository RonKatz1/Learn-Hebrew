using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.LearnHebrewEntities;

namespace BLL.Services
{
    public class AdultServices
    {
        public static Adult LoadAdultByID(int AdultID)
        {
            using (Domain.Repositories.AdultRepository repo = new Domain.Repositories.AdultRepository())
            {
                return ConvertEntityToBusiness(repo.LoadByID(AdultID));
            }
        }

        public static List<Adult> LoadAll()
        {
            List<Domain.Entity.Adult> entities = new List<Domain.Entity.Adult>();
            List<Adult> adults = new List<Adult>();
            using (Domain.Repositories.AdultRepository repo = new Domain.Repositories.AdultRepository())
            {
                entities = repo.LoadAll();
            }
            foreach(var entity in entities)
            {
                adults.Add(ConvertEntityToBusiness(entity));
            }
            return adults;
        }

        public static Adult LoadAdultByNameAndPassword(string name, string password)
        {
            using (Domain.Repositories.AdultRepository repo = new Domain.Repositories.AdultRepository())
            {
                return ConvertEntityToBusiness(repo.LoadByNameAndPassword(name, password));
            }
        }

        public static int Save(Adult adult)
        {
            Domain.Entity.Adult adultEntity = new Domain.Entity.Adult();
            adultEntity.AdultID = adult.AdultID;
            adultEntity.Name = adult.Name;
            adultEntity.Password = adult.Password;
            adultEntity.Data = Serialization.Serialize2<BLL.LearnHebrewEntities.Adult.AdultData>(adult.Data);

            using (var repo = new Domain.Repositories.AdultRepository())
            {
                adult.AdultID = repo.Save(adultEntity);
            }

            return adult.AdultID;
        }

        public static Adult ConvertEntityToBusiness(Domain.Entity.Adult EAdult)
        {
            Adult BAdult = new Adult();
            try
            {
                BAdult.AdultID = EAdult.AdultID;
                BAdult.Name = EAdult.Name;
                BAdult.Password = EAdult.Password;
                BAdult.Data = Serialization.Deserialize2<Adult.AdultData>(EAdult.Data);
                return BAdult;
            }
            catch(Exception ex)
            {
                return new Adult();
            }
        }

        public static Domain.Entity.Adult ConvertBusinessToEntity(Adult BAdult)
        {
            Domain.Entity.Adult EAdult = new Domain.Entity.Adult();
            try
            {
                EAdult.AdultID = BAdult.AdultID;
                EAdult.Name = BAdult.Name;
                EAdult.Password = BAdult.Password;
                EAdult.Data = Serialization.Serialize2(BAdult.Data);
                return EAdult;
            }
            catch (Exception ex)
            {
                return new Domain.Entity.Adult();
            }
        }
    }
}
