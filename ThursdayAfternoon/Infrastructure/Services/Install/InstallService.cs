using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ThursdayAfternoon.Infrastructure.Data;

namespace ThursdayAfternoon.Infrastructure.Services.Install
{
    public class InstallService
    {
        private readonly IDbContext _dbContext;

        public InstallService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public void InstallData()
        {
            ExecuteSqlFile(WebHelper.MapPath("~/App_Data/install.sql"));
        }

        private void ExecuteSqlFile(string path)
        {
            var statements = new List<string>();

            using (var stream = File.OpenRead(path))
            {
                using (var reader = new StreamReader(stream))
                {
                    string statement;
                    while ((statement = ReadNextStatementFromStream(reader)) != null)
                    {
                        statements.Add(statement);
                    }
                }
            }

            foreach (string stmt in statements)
            {
                _dbContext.ExecuteSqlCommand(stmt);
            }
        }

        private string ReadNextStatementFromStream(StreamReader reader)
        {
            var sb = new StringBuilder();
            while (true)
            {
                string lineOfText = reader.ReadLine();
                if (lineOfText == null)
                {
                    return sb.Length > 0 ? sb.ToString() : null;
                }

                if (lineOfText.TrimEnd().ToUpper() == "GO")
                {
                    break;
                }

                sb.Append(lineOfText + Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}