using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataTypes
{
    public class PersonService
    {
        private const string FILE_NAME = "Person.dat";

        public PersonService()
        {

        }

        public string SavePerson(Person person)
        {
            try
            {
                Person personDto = new Person();
                personDto.Id = person.Id;
                personDto.Name = person.Name;   
                personDto.Address = person.Address;

                using (FileStream fs = new FileStream(FILE_NAME, FileMode.CreateNew))
                {
                    using (BinaryWriter w = new BinaryWriter(fs))
                    {
                        w.Write(personDto.Id);
                        w.Write(personDto.Name);
                        w.Write(personDto.Address);
                    }
                }

                return "Success";
            }   
            catch
            {
                return "Error";
            }
        }

        public string UpdatePerson(Person person)
        {
            try
            {
                List<string> current = null;
                using (FileStream fs = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader r = new BinaryReader(fs))
                    {
                        foreach (var line in File.ReadAllLines(FILE_NAME))
                        {
                            if (line.Contains("Name") && current == null)
                                current = new List<string>();
                            else if (line.Contains("Address") && current != null)
                            {
                                //groups.Add(current);
                                current = null;
                            }
                            if (current != null)
                                current.Add(line);
                        }
                    }
                }

                return "Success";
            }
            catch
            {
                return "Error";
            }
        }
    }
}
