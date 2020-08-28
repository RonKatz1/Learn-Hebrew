using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.LearnHebrewEntities;

namespace BLL.Services
{
    public class ChildProgressServices
    {
        public static int Save(BLL.LearnHebrewEntities.ChildProgress childProgress)
        {

            Domain.Entity.ChildProgress childProgressEntity = new Domain.Entity.ChildProgress();
            childProgressEntity.ProgressID = childProgress.ProgressID;
            childProgressEntity.ChildID = childProgress.ChildID;
            childProgressEntity.Data = Serialization.Serialize2<BLL.LearnHebrewEntities.ChildProgress.ProgressData>(childProgress.Data);

            using (var repo = new Domain.Repositories.ChildProgressRepository())
            {
                childProgress.ProgressID = repo.Save(childProgressEntity);
            }

            return childProgress.ProgressID;
        }

        public static List<ChildProgress> LoadAllChildProgressesByChildID(int ChildID)
        {
            var entityProgresses = new List<Domain.Entity.ChildProgress>();
            var progresses = new List<ChildProgress>();
            using (Domain.Repositories.ChildProgressRepository repo = new Domain.Repositories.ChildProgressRepository())
            {
                entityProgresses = repo.LoadAllChildProgressesByChildID(ChildID);
            }
            foreach (var p in entityProgresses)
            {
                progresses.Add(ConvertEntityToBusiness(p));
            }
            return progresses;
        }

        public static ChildProgress ConvertEntityToBusiness(Domain.Entity.ChildProgress EProg)
        {
            ChildProgress BProg = new ChildProgress();
            try
            {
                BProg.ProgressID = EProg.ProgressID;
                BProg.ChildID = EProg.ChildID;
                BProg.Data = Serialization.Deserialize2<ChildProgress.ProgressDate>(EProg.Data);
                return BProg;
            }
            catch (Exception ex)
            {
                return new ChildProgress();
            }
        }

        public static Domain.Entity.ChildProgress ConvertBusinessToEntity(ChildProgress BProgress)
        {
            Domain.Entity.ChildProgress EProgress = new Domain.Entity.ChildProgress();
            try
            {
                EProgress.ChildID = BProgress.ChildID;
                EProgress.ProgressID = BProgress.ProgressID;
                EProgress.Data = Serialization.Serialize2(BProgress.Data);
                return EProgress;
            }
            catch (Exception ex)
            {
                return new Domain.Entity.ChildProgress();
            }
        }
    }
}
