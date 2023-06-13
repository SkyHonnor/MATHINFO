using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_MATINFO.Model
{
    public class Personnel : Crud<Personnel>
    {
        public int IdPersonnel { get; set; }

        public string NomPersonnel { get; set; }
        public string PrenomPersonnel { get; set; }
        public string MailPersonnel { get; set; }

        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Personnel> FindAll()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Personnel> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }
    }
}
