using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_MATINFO.Model
{
    public class Materiel : Crud<Materiel>
    {
        public int IdMateriel { get; set; }
        public int FKIdCategorie { get; set; }

        public string NomMateriel { get; set; }
        public string CodeBarre { get; set; }
        public string ReferenceConstructeur { get; set; }

        public Categorie Categorie { get; set; }

        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Materiel> FindAll()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<Materiel> FindBySelection(string criteres)
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
    }
}
