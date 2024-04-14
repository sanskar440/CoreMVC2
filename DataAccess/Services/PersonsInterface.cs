using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public  interface IPersonsInterface
    {
        int insertpersons(Persons model);
        List<Persons> GetPersons(string search = "", int page = 1, int pageSize = 20, string sortby = "FirstName", string orderBy = "ASC");
        int UpdatePersons(Persons model);
        Persons GetUpdatePersons(int id);
        int DeletePerson(int id);


        
    }
}
