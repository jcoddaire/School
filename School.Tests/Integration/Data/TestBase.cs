using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using School.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Tests.Integration.Data
{
    [ExcludeFromCodeCoverage]
    public abstract class TestBase
    {        
        private ISchoolData _repository = null;

        //TODO: update this to an App.config file or similar. Right now .NET core does not support App.config files in mstest.
        //https://github.com/Microsoft/vstest/issues/1758
        const string TEST_CONNECTION_STRING = @"Data Source=JCoddaire\JCoddaire;Initial Catalog=School;Integrated Security=True;MultipleActiveResultSets=True;App=EntityFramework";

        internal ISchoolData Repository
        {
            get
            {
                if (_repository == null)
                {
                    var optionsBuilder = new DbContextOptionsBuilder<SchoolContext>();
                    optionsBuilder.UseSqlServer(TEST_CONNECTION_STRING);
                    var db = new SchoolContext(optionsBuilder.Options);                    
                    _repository = new SchoolServiceRepository(db);
                }

                return _repository;
            }
        }
    }
}
