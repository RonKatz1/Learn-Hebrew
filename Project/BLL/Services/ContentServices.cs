using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.LearnHebrewEntities;

namespace BLL.Services
{
    public class ContentServices
    {

        public static int Save(Content content)
        {
            try
            {
                Domain.Entity.Content contentEntity = new Domain.Entity.Content();
                contentEntity.AdultID = content.AdultID;
                contentEntity.ContentID = content.ContentID;
                contentEntity.Word = content.Word;
                contentEntity.Data = Serialization.Serialize2<BLL.LearnHebrewEntities.ContentData>(content.Data);

                using (var repo = new Domain.Repositories.ContentRepository())
                {
                    content.ContentID = repo.Save(contentEntity);
                }

                return content.ContentID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public static Content LoadByID(int ContentID)
        {
            using (Domain.Repositories.ContentRepository repo = new Domain.Repositories.ContentRepository())
            {
                return ConvertEntityToBusiness(repo.LoadByID(ContentID));
            }
        }

        public static List<Content> LoadAllContents()
        {
            using (Domain.Repositories.ContentRepository repo = new Domain.Repositories.ContentRepository())
            {
                List<Content> contents = new List<Content>();
                var entities = repo.LoadAllContents();
                foreach (var entity in entities)
                {
                    contents.Add(ConvertEntityToBusiness(entity));
                }
                return contents;
            }
        }

        public static string GetFilePath(BLL.LearnHebrewEntities.ContentFile file)
        {
            var localPath = "/ContentFiles/";
            var filePath = file.Code + "." + file.Extention;

            return localPath + filePath;
        }

        public static Content ConvertEntityToBusiness(Domain.Entity.Content EContent)
        {
            Content BContent = new Content();
            try
            {
                BContent.AdultID = EContent.AdultID;
                BContent.ContentID = EContent.ContentID;
                BContent.Word = EContent.Word;
                BContent.Data = Serialization.Deserialize2<ContentData>(EContent.Data);
                return BContent;
            }
            catch (Exception ex)
            {
                return new Content();
            }
        }

        public static Domain.Entity.Content ConvertBusinessToEntity(Content BContent)
        {
            Domain.Entity.Content EContent = new Domain.Entity.Content();
            try
            {
                EContent.AdultID = BContent.AdultID;
                EContent.ContentID = BContent.ContentID;
                EContent.Word = BContent.Word;
                EContent.Data = Serialization.Serialize2(BContent.Data);
                return EContent;
            }
            catch (Exception ex)
            {
                return new Domain.Entity.Content();
            }
        }
    }
}
