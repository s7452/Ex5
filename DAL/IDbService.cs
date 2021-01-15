using Ex5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ex5.DAL
{
    public interface IDbService
    {
        public IEnumerable<Student> GetStudents();
    }
}
