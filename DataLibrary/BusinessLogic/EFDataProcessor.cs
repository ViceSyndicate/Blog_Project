using DataLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DataAccess;

namespace DataLibrary.BusinessLogic
{
    internal class EFDataProcessor
    {
        EFBlogContext _dbContext;
        public EFDataProcessor() {
            _dbContext = new EFBlogContext();
        }
    }
}
